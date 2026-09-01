using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Masmorra
{
    /// <summary>
    /// O JOGO EM VOLTA. ESTE ARQUIVO JA ESTA PRONTO - voce nao mexe aqui.
    ///
    /// Ele cuida do que nao e o assunto de hoje: ler o teclado, criar os
    /// monstros, mover os Labels na tela e escrever no log. Tudo o que ele
    /// PENSA, ele pergunta para o Jogo.cs - e o Jogo.cs e o seu arquivo.
    ///
    /// Se voce abrir este arquivo procurando o que fazer, esta no lugar
    /// errado: o seu trabalho e o Jogo.cs, e la estao os 10 TODO.
    /// </summary>
    public partial class frmJogo : Form
    {
        // -----------------------------------------------------------------
        // SE O EMOJI APARECER COMO QUADRADINHO NA SUA MAQUINA, troque as
        // duas linhas abaixo por "@" e "M". O jogo funciona exatamente
        // igual - muda so o desenho.
        // -----------------------------------------------------------------
        private const string SIMBOLO_HEROI   = "\U0001F9D9";   // mago
        private const string SIMBOLO_MONSTRO = "\U0001F479";   // ogro

        private const int CELULA = 48;
        private const int MONSTROS_PARA_VENCER = 10;

        // O Random e criado UMA vez, aqui. Criar um Random novo a cada
        // sorteio faz sair o mesmo numero varias vezes seguidas.
        private Random sorteio = new Random();

        private Personagem heroi = new Personagem();

        // Dois vetores que andam juntos: a posicao 3 dos dois fala do
        // MESMO monstro - o de dentro do jogo e o que aparece na tela.
        private List<Personagem> monstros = new List<Personagem>();
        private List<Label> corpos = new List<Label>();

        private int xp = 0;
        private int derrotados = 0;
        private bool acabou = false;

        public frmJogo()
        {
            InitializeComponent();
        }

        private void frmJogo_Shown(object sender, EventArgs e)
        {
            heroi.Nome = "Voce";
            heroi.Simbolo = SIMBOLO_HEROI;
            heroi.Vida = 20;
            heroi.VidaMaxima = 20;
            heroi.Forca = 5;
            heroi.Defesa = 0;
            heroi.Coluna = 0;
            heroi.Linha = 0;

            lblHeroi.Text = SIMBOLO_HEROI;
            ColocarNaTela(lblHeroi, heroi.Coluna, heroi.Linha);

            Escrever("Voce entrou na masmorra. Derrote " + MONSTROS_PARA_VENCER + " monstros.");
            Escrever("Use as setas do teclado para andar.");

            NascerMonstro();
            NascerMonstro();
            NascerMonstro();

            AtualizarPainel();
        }

        // ------------------------------------------------------------- teclado

        private void frmJogo_KeyDown(object sender, KeyEventArgs e)
        {
            if (acabou == true)
            {
                return;
            }

            int novaColuna = heroi.Coluna;
            int novaLinha = heroi.Linha;

            if (e.KeyCode == Keys.Left)  { novaColuna = novaColuna - 1; }
            else if (e.KeyCode == Keys.Right) { novaColuna = novaColuna + 1; }
            else if (e.KeyCode == Keys.Up)    { novaLinha = novaLinha - 1; }
            else if (e.KeyCode == Keys.Down)  { novaLinha = novaLinha + 1; }
            else { return; }

            // A PRIMEIRA PERGUNTA AO SEU CODIGO. Enquanto o TODO 1 nao
            // estiver escrito, isto devolve false e o heroi nao sai do lugar.
            if (Jogo.PodeAndar(novaColuna, novaLinha) == false)
            {
                return;
            }

            int alvo = MonstroEm(novaColuna, novaLinha);

            if (alvo >= 0)
            {
                Combater(alvo);
                return;
            }

            heroi.Coluna = novaColuna;
            heroi.Linha = novaLinha;
            ColocarNaTela(lblHeroi, heroi.Coluna, heroi.Linha);
        }

        // ------------------------------------------------------------- a luta

        private void Combater(int indice)
        {
            Personagem monstro = monstros[indice];

            // As COPIAS. A sua luta acontece sobre elas, e nao sobre os
            // personagens de verdade: assim, se o seu while nao terminar,
            // o jogo continua inteiro.
            Personagem copiaHeroi = heroi.Copiar();
            Personagem copiaMonstro = monstro.Copiar();

            Execucao exec = Sandbox.Rodar("Lutar", delegate ()
            {
                return Jogo.Lutar(copiaHeroi, copiaMonstro);
            });

            if (exec.EstourouOTempo == true)
            {
                Escrever(">> A luta nao terminou.");
                Escrever(Sandbox.Recado(exec));
                return;
            }

            if (exec.Falha != null)
            {
                Escrever(">> A luta quebrou: " + exec.Falha.Message);
                return;
            }

            string relato = exec.Valor;

            if (relato == null || relato == "")
            {
                Escrever(">> O Lutar nao devolveu relato nenhum. Confira o TODO 5.");
                return;
            }

            // Deu certo: o resultado da copia vira o resultado de verdade.
            heroi.Vida = copiaHeroi.Vida;
            monstro.Vida = copiaMonstro.Vida;

            Escrever(relato);

            if (Jogo.EstaVivo(monstro.Vida) == false)
            {
                int ganho = 5 + monstro.Forca;
                xp = xp + ganho;
                derrotados = derrotados + 1;

                Escrever(monstro.Nome + " caiu. +" + ganho + " XP.");

                pnlMapa.Controls.Remove(corpos[indice]);
                corpos[indice].Dispose();
                corpos.RemoveAt(indice);
                monstros.RemoveAt(indice);

                SubirDeNivelSePuder();

                if (derrotados < MONSTROS_PARA_VENCER)
                {
                    NascerMonstro();
                }
            }

            AtualizarPainel();
            ConferirFim();
        }

        private void SubirDeNivelSePuder()
        {
            int nivel = Jogo.CalcularNivel(xp);

            int vidaMaxima = Jogo.VidaMaximaDoNivel(nivel);
            int forca = Jogo.ForcaDoNivel(nivel);

            // Enquanto o TODO 7 e o TODO 8 nao estiverem escritos eles
            // devolvem 0, e o heroi ficaria sem vida nenhuma. Ate la, o
            // jogo mantem os valores que ja tinha.
            if (vidaMaxima > 0)
            {
                if (vidaMaxima > heroi.VidaMaxima)
                {
                    heroi.Vida = heroi.Vida + (vidaMaxima - heroi.VidaMaxima);
                    Escrever("Voce subiu para o nivel " + nivel + "!");
                }
                heroi.VidaMaxima = vidaMaxima;
            }

            if (forca > 0)
            {
                heroi.Forca = forca;
            }
        }

        // --------------------------------------------------------- os monstros

        private void NascerMonstro()
        {
            int coluna = 0;
            int linha = 0;
            bool achou = false;
            int tentativas = 0;

            // Sorteia uma casa vazia. O limite de tentativas existe para
            // este laco nunca ser infinito, mesmo com o mapa cheio.
            while (achou == false && tentativas < 200)
            {
                coluna = sorteio.Next(0, Jogo.COLUNAS);
                linha = sorteio.Next(0, Jogo.LINHAS);
                tentativas = tentativas + 1;

                bool ocupado = (coluna == heroi.Coluna && linha == heroi.Linha)
                            || MonstroEm(coluna, linha) >= 0;

                if (ocupado == false)
                {
                    achou = true;
                }
            }

            if (achou == false)
            {
                return;
            }

            Personagem m = new Personagem();
            m.Nome = SortearNome();
            m.Simbolo = SIMBOLO_MONSTRO;
            m.Forca = sorteio.Next(3, 7) + derrotados / 3;
            m.Defesa = sorteio.Next(0, 3);
            m.VidaMaxima = sorteio.Next(8, 15) + derrotados;
            m.Vida = m.VidaMaxima;
            m.Coluna = coluna;
            m.Linha = linha;

            Label corpo = new Label();
            corpo.Text = SIMBOLO_MONSTRO;
            corpo.Font = new Font("Segoe UI Emoji", 20F);
            corpo.ForeColor = Color.White;
            corpo.BackColor = Color.Transparent;
            corpo.Size = new Size(CELULA, CELULA);
            corpo.TextAlign = ContentAlignment.MiddleCenter;

            monstros.Add(m);
            corpos.Add(corpo);

            pnlMapa.Controls.Add(corpo);
            ColocarNaTela(corpo, coluna, linha);
        }

        private string SortearNome()
        {
            string[] nomes = { "Goblin", "Rato gigante", "Esqueleto", "Lobo", "Ogro", "Morcego" };
            return nomes[sorteio.Next(0, nomes.Length)];
        }

        private int MonstroEm(int coluna, int linha)
        {
            for (int i = 0; i < monstros.Count; i++)
            {
                if (monstros[i].Coluna == coluna && monstros[i].Linha == linha)
                {
                    return i;
                }
            }

            return -1;
        }

        // ------------------------------------------------------------- a tela

        private void ColocarNaTela(Label alvo, int coluna, int linha)
        {
            alvo.Location = new Point(coluna * CELULA, linha * CELULA);
        }

        private void AtualizarPainel()
        {
            int nivel = Jogo.CalcularNivel(xp);

            lblNivel.Text = "Nivel " + nivel;
            lblVida.Text = "Vida " + heroi.Vida + " / " + heroi.VidaMaxima;
            lblForca.Text = "Forca " + heroi.Forca;
            lblXp.Text = "XP " + xp;
            lblPlacar.Text = "Monstros derrotados: " + derrotados + " de " + MONSTROS_PARA_VENCER;

            lblBarra.Text = Jogo.BarraDeVida(heroi.Vida, heroi.VidaMaxima);
            lblSituacao.Text = Jogo.Situacao(heroi.Vida, heroi.VidaMaxima);
        }

        private void ConferirFim()
        {
            if (Jogo.EstaVivo(heroi.Vida) == false)
            {
                acabou = true;
                Escrever("=== VOCE CAIU. Fim de jogo. ===");
                lblAjuda.Text = "Fim de jogo. Feche e rode de novo com F5.";
                return;
            }

            if (derrotados >= MONSTROS_PARA_VENCER)
            {
                acabou = true;
                Escrever("=== VOCE LIMPOU A MASMORRA! ===");
                lblAjuda.Text = "Voce venceu. Feche e rode de novo com F5.";
            }
        }

        private void Escrever(string linha)
        {
            lstLog.Items.Add(linha);
            lstLog.TopIndex = lstLog.Items.Count - 1;
        }
    }
}
