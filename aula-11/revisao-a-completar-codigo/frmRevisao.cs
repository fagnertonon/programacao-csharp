using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Revisao
{
    // VARIANTE A - o aluno completa os metodos em Desafios.cs e roda com F5.
    // O app chama o que ele escreveu e compara com o esperado.
    //
    // O lado esquerdo (o ensino) vem de ../comum/PainelEnsino.cs e e igual nas
    // tres variantes. O que este arquivo faz de proprio e so o lado do desafio.

    public class frmRevisao : Form
    {
        private const int LARG_ENSINO = 640;
        private const int LARG_DESAFIO = 500;

        private Conteudo conteudo;
        private TabControl abas;
        private Label rodape;

        private readonly List<Label> selos = new List<Label>();
        private readonly List<Panel> painelResultado = new List<Panel>();
        private readonly List<bool> resolvido = new List<bool>();
        private readonly List<SplitContainer> divisores = new List<SplitContainer>();

        public frmRevisao()
        {
            try
            {
                conteudo = Conteudo.Carregar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Revisao", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Load += delegate { Close(); };
                return;
            }

            MontarJanela();
            MontarAbas();
            AbasPintadas.Ligar(abas, EstadoDaAba);
            CorrigirTudo();
            Pintura.Suavizar(this);
        }

        private void MontarJanela()
        {
            Text = conteudo.Aula + " - " + conteudo.Titulo + "  [A: completar o codigo]";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(1280, 720);
            MinimumSize = new Size(1060, 640);
            BackColor = Color.White;

            Panel cabecalho = new Panel();
            cabecalho.Dock = DockStyle.Top;
            cabecalho.Height = 62;
            cabecalho.BackColor = Paleta.Dark;

            Label titulo = new Label();
            titulo.Text = "Revisao geral das aulas";
            titulo.Font = UI.H1;
            titulo.ForeColor = Color.White;
            titulo.AutoSize = true;
            titulo.Location = new Point(18, 10);
            cabecalho.Controls.Add(titulo);

            Label sub = new Label();
            sub.Text = conteudo.Aula + " - " + conteudo.Data
                       + "   |   complete o metodo em Desafios.cs, salve e rode com F5";
            sub.Font = UI.Mini;
            sub.ForeColor = Paleta.PurpleLt;
            sub.AutoSize = true;
            sub.Location = new Point(20, 38);
            cabecalho.Controls.Add(sub);

            Button btnNaoCai = new Button();
            btnNaoCai.Text = "O que NAO entra nesta revisao";
            btnNaoCai.Font = UI.Mini;
            btnNaoCai.FlatStyle = FlatStyle.Flat;
            btnNaoCai.ForeColor = Color.White;
            btnNaoCai.BackColor = Paleta.Purple;
            btnNaoCai.FlatAppearance.BorderColor = Paleta.PurpleLt;
            btnNaoCai.Size = new Size(190, 30);
            btnNaoCai.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnNaoCai.Location = new Point(ClientSize.Width - 210, 16);
            btnNaoCai.Click += delegate { MostrarNaoCai(); };
            cabecalho.Controls.Add(btnNaoCai);

            rodape = new Label();
            rodape.Dock = DockStyle.Bottom;
            rodape.Height = 30;
            rodape.TextAlign = ContentAlignment.MiddleLeft;
            rodape.Padding = new Padding(16, 0, 0, 0);
            rodape.Font = UI.Mini;
            rodape.BackColor = Paleta.Tint;
            rodape.ForeColor = Paleta.Body;

            abas = new TabControl();
            abas.Dock = DockStyle.Fill;
            abas.Font = new Font("Segoe UI", 9.5F);
            abas.Padding = new Point(14, 6);
            abas.Selecting += Abas_Selecting;

            Controls.Add(abas);
            Controls.Add(rodape);
            Controls.Add(cabecalho);
        }

        private void MostrarNaoCai()
        {
            string texto = "Estes assuntos NAO entram nesta revisao" + ":\r\n\r\n";
            foreach (string item in conteudo.ForaDaRevisao)
            {
                texto += "   -  " + item + "\r\n";
            }
            texto += "\r\nSe voce viu alguma dessas coisas pesquisando por conta, otimo - guarde\r\n";
            texto += "para depois. Hoje o assunto sao as oito abas.";

            MessageBox.Show(texto, "Fora desta revisao", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MontarAbas()
        {
            for (int i = 0; i < conteudo.Topicos.Count; i++)
            {
                Topico t = conteudo.Topicos[i];

                TabPage pagina = new TabPage();
                pagina.Text = "  " + t.Numero + ". " + t.Titulo + "  ";
                pagina.BackColor = Color.White;

                SplitContainer div = new SplitContainer();
                div.Dock = DockStyle.Fill;
                div.SplitterWidth = 6;
                div.BackColor = Paleta.Line;
                div.Panel1.BackColor = Color.White;
                div.Panel2.BackColor = Paleta.Tint;
                div.Panel1.AutoScroll = true;
                div.Panel2.AutoScroll = true;
                divisores.Add(div);

                PainelEnsino.Montar(div.Panel1, t, LARG_ENSINO);
                MontarDesafio(div.Panel2, t);

                pagina.Controls.Add(div);
                abas.TabPages.Add(pagina);

                resolvido.Add(false);
            }
        }

        private void MontarDesafio(Panel destino, Topico t)
        {
            FlowLayoutPanel fluxo = UI.NovoFluxo(destino, LARG_DESAFIO);

            fluxo.Controls.Add(UI.Titulo("Desafio " + t.Numero,
                "complete em Desafios.cs e rode com F5", LARG_DESAFIO));

            Label selo = new Label();
            selo.AutoSize = false;
            selo.Width = LARG_DESAFIO;
            selo.Height = 34;
            selo.TextAlign = ContentAlignment.MiddleCenter;
            selo.Font = UI.H2;
            selo.Margin = new Padding(0, 0, 0, 10);
            fluxo.Controls.Add(selo);
            selos.Add(selo);

            fluxo.Controls.Add(UI.Paragrafo(t.Desafio.Enunciado, LARG_DESAFIO));

            fluxo.Controls.Add(UI.Subtitulo("O metodo que voce completa"));
            fluxo.Controls.Add(UI.Codigo(t.Desafio.Assinatura, LARG_DESAFIO));

            Button btnDica = UI.Botao("Ver a dica", 120, false);
            btnDica.Click += delegate
            {
                MessageBox.Show(t.Desafio.Dica, "Dica do desafio " + t.Numero,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            fluxo.Controls.Add(btnDica);

            Button btnTestar = UI.Botao("Testar o meu codigo", LARG_DESAFIO, true);
            btnTestar.Click += delegate { CorrigirTudo(); };
            fluxo.Controls.Add(btnTestar);

            Panel resultados = new Panel();
            resultados.Width = LARG_DESAFIO;
            resultados.AutoSize = true;
            resultados.Margin = new Padding(0, 0, 0, 16);
            fluxo.Controls.Add(resultados);
            painelResultado.Add(resultados);
        }

        private void CorrigirTudo()
        {
            for (int i = 0; i < conteudo.Topicos.Count; i++)
            {
                ResultadoTopico r = Corretor.Corrigir(conteudo.Topicos[i]);
                resolvido[i] = r.Resolvido;
                PintarSelo(selos[i], r);
                PintarResultados(painelResultado[i], r);
            }

            AtualizarAbas();

            // Os rotulos de resultado nascem aqui, entao o & tem que ser
            // desligado depois deles - senao "&&" vira "&" na tela.
            UI.DesligarMnemonicos(this);
        }

        private void PintarSelo(Label selo, ResultadoTopico r)
        {
            if (r.Resolvido)
            {
                selo.Text = "RESOLVIDO - a proxima aba destravou";
                selo.BackColor = Paleta.MintBg;
                selo.ForeColor = Paleta.Mint;
            }
            else
            {
                // Cuidado ao ler este numero: um metodo ainda vazio devolve o
                // valor padrao do tipo (0, false, "") e ja acerta algum teste
                // por acidente. So vale 100%.
                selo.Text = r.Passaram + " de " + r.Total + " testes passando";
                selo.BackColor = Paleta.AmberBg;
                selo.ForeColor = Paleta.Amber;
            }
        }

        private void PintarResultados(Panel destino, ResultadoTopico r)
        {
            destino.Controls.Clear();
            int y = 0;

            Label cab = new Label();
            cab.Text = "O que o corretor testou";
            cab.Font = UI.H2;
            cab.ForeColor = Paleta.Purple;
            cab.AutoSize = true;
            cab.Location = new Point(0, y);
            destino.Controls.Add(cab);
            y += 26;

            foreach (ResultadoTeste teste in r.Testes)
            {
                Panel linha = new Panel();
                linha.Width = LARG_DESAFIO - 10;
                linha.BackColor = teste.Passou ? Paleta.MintBg : Paleta.RedBg;
                linha.Location = new Point(0, y);

                Label marca = new Label();
                marca.Text = teste.Passou ? "OK" : "X";
                marca.Font = UI.MonoB;
                marca.ForeColor = teste.Passou ? Paleta.Mint : Paleta.Red;
                marca.AutoSize = true;
                marca.Location = new Point(8, 8);
                linha.Controls.Add(marca);

                Label desc = new Label();
                desc.Text = teste.Descricao;
                desc.Font = UI.Mini;
                desc.ForeColor = Paleta.Ink;
                desc.AutoSize = true;
                desc.MaximumSize = new Size(LARG_DESAFIO - 70, 0);
                desc.Location = new Point(42, 8);
                linha.Controls.Add(desc);

                int alt = Math.Max(30, desc.PreferredHeight + 14);

                if (!teste.Passou)
                {
                    Label det = new Label();
                    det.Text = "esperado: [" + teste.Esperado + "]     obtido: [" + teste.Obtido + "]";
                    det.Font = UI.Mono;
                    det.ForeColor = Paleta.Red;
                    det.AutoSize = true;
                    det.MaximumSize = new Size(LARG_DESAFIO - 70, 0);
                    det.Location = new Point(42, alt);
                    linha.Controls.Add(det);
                    alt += det.PreferredHeight + 4;

                    if (!string.IsNullOrEmpty(teste.Erro))
                    {
                        Label err = new Label();
                        err.Text = teste.Erro;
                        err.Font = UI.Mini;
                        err.ForeColor = Paleta.Amber;
                        err.AutoSize = true;
                        err.MaximumSize = new Size(LARG_DESAFIO - 70, 0);
                        err.Location = new Point(42, alt);
                        linha.Controls.Add(err);
                        alt += err.PreferredHeight + 4;
                    }
                }

                linha.Height = alt + 6;
                destino.Controls.Add(linha);
                y += linha.Height + 6;
            }

            destino.Height = y + 8;
        }

        private EstadoAba EstadoDaAba(int indice)
        {
            if (resolvido[indice]) { return EstadoAba.Resolvida; }

            int liberadas = Destravadas.Contar(resolvido, abas.TabPages.Count);
            return indice < liberadas ? EstadoAba.Liberada : EstadoAba.Travada;
        }

        private void AtualizarAbas()
        {
            int liberadas = Destravadas.Contar(resolvido, abas.TabPages.Count);

            for (int i = 0; i < abas.TabPages.Count; i++)
            {
                Topico t = conteudo.Topicos[i];
                abas.TabPages[i].Text = AbasPintadas.Marcar(EstadoDaAba(i), t.Numero, t.Titulo);
            }
            abas.Invalidate();

            int feitos = 0;
            foreach (bool b in resolvido) { if (b) { feitos++; } }

            rodape.Text = "Resolvidos: " + feitos + " de " + resolvido.Count
                        + "     |     abas liberadas: " + liberadas
                        + "     |     complete os metodos em Desafios.cs, salve e rode de novo com F5";
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Destravadas.AjustarDivisores(divisores, LARG_DESAFIO + 40);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Destravadas.AjustarDivisores(divisores, LARG_DESAFIO + 40);
        }

        private void Abas_Selecting(object sender, TabControlCancelEventArgs e)
        {
            Destravadas.BloquearSeTravada(e, resolvido, conteudo);
        }
    }
}
