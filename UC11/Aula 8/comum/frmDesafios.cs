using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Acesso
{
    /// <summary>
    /// A JANELA DOS 20 DESAFIOS - uma aba por desafio.
    ///
    /// Cada aba so fecha quando as QUATRO perguntas do corretor dao verde:
    /// o metodo tem corpo, o metodo esta certo, o teste existe e passa, e o
    /// teste cobre as fronteiras.
    ///
    /// A aba seguinte destrava quando a anterior fecha, dentro do bloco: o
    /// login comeca no desafio 1, o cadastro no 11, e os dois comecos nascem
    /// destravados.
    ///
    /// UC11 - Aulas 8 e 9.
    /// </summary>
    public class frmDesafios : Form
    {
        private static readonly Color VERDE = Color.FromArgb(0, 110, 70);
        private static readonly Color VERMELHO = Color.FromArgb(150, 40, 60);
        private static readonly Color CINZA = Color.FromArgb(120, 120, 128);
        private static readonly Color ROXO = Color.FromArgb(92, 45, 145);

        private readonly List<Desafio> desafios = Conteudo.Todos();
        private readonly List<bool> resolvido = new List<bool>();
        private readonly List<Label> selos = new List<Label>();
        private readonly List<Label> paineis = new List<Label>();

        private readonly List<SplitContainer> divisores = new List<SplitContainer>();

        private TabControl abas;
        private Label rodape;

        public frmDesafios()
        {
            Text = "Desafios de acesso - UC11";
            ClientSize = new Size(1100, 680);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.White;
            Font = new Font("Segoe UI", 9F);
            MinimumSize = new Size(900, 560);

            rodape = new Label();
            rodape.Dock = DockStyle.Bottom;
            rodape.Height = 34;
            rodape.TextAlign = ContentAlignment.MiddleLeft;
            rodape.Padding = new Padding(12, 0, 0, 0);
            rodape.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            rodape.BackColor = Color.FromArgb(244, 241, 249);

            abas = new TabControl();
            abas.Dock = DockStyle.Fill;
            abas.DrawMode = TabDrawMode.OwnerDrawFixed;
            abas.SizeMode = TabSizeMode.Fixed;
            abas.ItemSize = new Size(52, 28);
            abas.DrawItem += PintarAba;
            abas.Selecting += AoTrocarDeAba;
            // NAO reconferir ao trocar de aba. O Conferir() roda o codigo do aluno nos
            // 20 desafios, e trocar de aba e o que ele mais faz - so para LER o
            // enunciado do desafio seguinte. Com um metodo lento no meio, cada leitura
            // custava segundos de janela parada, e o painel ja esta preenchido desde a
            // ultima conferida. Quem manda reconferir e o F5 e o botao.

            MontarAbas();

            Controls.Add(abas);
            Controls.Add(rodape);

            Shown += delegate { AjustarDivisores(); Conferir(); };
            Resize += delegate { AjustarDivisores(); };
        }

        private void MontarAbas()
        {
            foreach (Desafio d in desafios)
            {
                TabPage pagina = new TabPage(" " + d.Numero + " ");
                pagina.BackColor = Color.White;

                // NADA de Panel1MinSize/Panel2MinSize/SplitterDistance aqui:
                // um SplitContainer recem-criado tem 150px, e qualquer um dos
                // tres dispara a validacao e derruba o programa. Tudo isso vai
                // para o AjustarDivisores, que roda quando ha largura de verdade.
                SplitContainer div = new SplitContainer();
                div.Dock = DockStyle.Fill;
                divisores.Add(div);

                MontarEnunciado(div.Panel1, d);
                MontarResultado(div.Panel2, d);

                pagina.Controls.Add(div);
                abas.TabPages.Add(pagina);
                resolvido.Add(false);
            }
        }

        private void MontarEnunciado(Control painel, Desafio d)
        {
            painel.BackColor = Color.FromArgb(250, 249, 252);
            painel.Padding = new Padding(16);

            Label titulo = new Label();
            titulo.Text = "Desafio " + d.Numero + " - " + d.Titulo;
            titulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            titulo.ForeColor = ROXO;
            titulo.Dock = DockStyle.Top;
            titulo.Height = 34;

            Label enunciado = new Label();
            enunciado.Text = d.Enunciado;
            enunciado.Dock = DockStyle.Top;
            enunciado.Height = 74;
            enunciado.Font = new Font("Segoe UI", 10F);

            Label assinatura = new Label();
            assinatura.Text = d.Assinatura;
            assinatura.Dock = DockStyle.Top;
            assinatura.Height = 56;
            assinatura.Font = new Font("Consolas", 9.5F);
            assinatura.BackColor = Color.FromArgb(238, 235, 245);
            assinatura.Padding = new Padding(8, 6, 8, 6);

            Label teste = new Label();
            teste.Text = "O teste tem de se chamar:  " + d.NomeDoTeste;
            teste.Dock = DockStyle.Top;
            teste.Height = 30;
            teste.Font = new Font("Consolas", 9.5F, FontStyle.Bold);
            teste.ForeColor = ROXO;

            Label depende = new Label();
            depende.Text = d.Depende == null
                ? "Nao depende de nenhum outro desafio."
                : "Este metodo pergunta para o desafio " + d.Depende
                  + ". Se aquele estiver errado, este quebra junto.";
            depende.Dock = DockStyle.Top;
            depende.Height = 42;
            depende.ForeColor = CINZA;

            Label fronteiras = new Label();
            string txt = "O seu teste e OBRIGADO a cobrir:\r\n";
            foreach (Fronteira f in d.Fronteiras) { txt = txt + "   - " + f.Descricao + "\r\n"; }
            fronteiras.Text = txt;
            fronteiras.Dock = DockStyle.Fill;
            fronteiras.Font = new Font("Segoe UI", 9.5F);

            painel.Controls.Add(fronteiras);
            painel.Controls.Add(depende);
            painel.Controls.Add(teste);
            painel.Controls.Add(assinatura);
            painel.Controls.Add(enunciado);
            painel.Controls.Add(titulo);
        }

        private void MontarResultado(Control painel, Desafio d)
        {
            painel.BackColor = Color.White;
            painel.Padding = new Padding(16);

            Label selo = new Label();
            selo.Dock = DockStyle.Top;
            selo.Height = 44;
            selo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            selo.TextAlign = ContentAlignment.MiddleLeft;
            selos.Add(selo);

            Button conferir = new Button();
            conferir.Text = "Conferir de novo  (so muda depois do F5)";
            conferir.Dock = DockStyle.Top;
            conferir.Height = 32;
            conferir.Click += delegate { Conferir(); };

            Label corpo = new Label();
            corpo.Dock = DockStyle.Fill;
            corpo.Font = new Font("Consolas", 9F);
            corpo.TextAlign = ContentAlignment.TopLeft;
            paineis.Add(corpo);

            painel.Controls.Add(corpo);
            painel.Controls.Add(conferir);
            painel.Controls.Add(selo);
        }

        /// <summary>
        /// SplitterDistance so vale depois que o controle tem largura de
        /// verdade - por isso isto roda no Shown e no Resize, nunca na
        /// montagem. A licao esta escrita no Destravadas.cs da corrida, e eu
        /// aprendi de novo na marra.
        /// </summary>
        private void AjustarDivisores()
        {
            foreach (SplitContainer div in divisores)
            {
                if (div.Width < 620) { continue; }

                try
                {
                    div.Panel1MinSize = 240;
                    div.Panel2MinSize = 260;

                    int desejado = (int)(div.Width * 0.42);
                    int maximo = div.Width - div.Panel2MinSize - div.SplitterWidth - 1;

                    if (desejado < div.Panel1MinSize) { desejado = div.Panel1MinSize; }
                    if (desejado > maximo) { desejado = maximo; }
                    if (desejado < div.Panel1MinSize) { continue; }

                    div.SplitterDistance = desejado;
                }
                catch (InvalidOperationException) { /* janela pequena demais */ }
            }
        }

        // ------------------------------------------------------------- correcao
        private void Conferir()
        {
            int prontos = 0;

            for (int i = 0; i < desafios.Count; i++)
            {
                ResultadoDesafio r = Corretor.Conferir(desafios[i]);
                resolvido[i] = r.Resolvido;
                if (r.Resolvido) { prontos = prontos + 1; }

                selos[i].Text = Selo(r);
                selos[i].ForeColor = r.Resolvido ? VERDE : VERMELHO;
                paineis[i].Text = Detalhe(desafios[i], r);
            }

            int destravadas = 0;
            for (int i = 0; i < desafios.Count; i++)
            {
                if (Liberada(i)) { destravadas = destravadas + 1; }
            }

            rodape.Text = "  " + prontos + " de " + desafios.Count + " desafios resolvidos"
                        + "        abas destravadas: " + destravadas;
            Text = "Desafios de acesso - UC11   -   " + prontos + " de " + desafios.Count;
            abas.Invalidate();
        }

        /// <summary>
        /// A cadeia de destravamento, e ela roda dentro do BLOCO.
        ///
        /// Sao dois blocos: o login, que abre no desafio 1, e o cadastro, que
        /// abre no 11. Dentro de um bloco vale a regra de sempre - a aba so
        /// abre com a anterior fechada - mas o comeco de cada bloco nasce
        /// destravado.
        ///
        /// Isto e por causa de turma noturna. As duas noites usam o mesmo
        /// pacote, e sem esta divisao quem faltasse na noite do login chegaria
        /// na do cadastro tendo de fechar dez abas antes de alcancar a turma.
        /// O aviso do README da UC continua valendo: quem falta perde a
        /// tecnica, nao o material - e nao pode perder a noite seguinte junto.
        /// </summary>
        private bool Liberada(int i)
        {
            int inicio = InicioDoBloco(i);
            if (i == inicio) { return true; }

            for (int k = inicio; k < i; k++)
            {
                if (!resolvido[k]) { return false; }
            }

            return true;
        }

        private int InicioDoBloco(int i)
        {
            int inicio = 0;

            for (int k = 0; k <= i && k < desafios.Count; k++)
            {
                if (desafios[k].AbreBloco) { inicio = k; }
            }

            return inicio;
        }

        private static string Selo(ResultadoDesafio r)
        {
            if (r.Resolvido) { return "RESOLVIDO - a proxima aba destravou"; }

            if (!r.MetodoTemCorpo) { return "O metodo ainda nao tem corpo"; }
            if (!r.MetodoOk) { return r.CasosPassaram + " de " + r.Casos.Count + " casos passando"; }
            if (!r.TesteExiste) { return "O metodo esta certo - falta o teste"; }
            if (r.FronteirasFaltando.Count > 0) { return "O teste existe, mas nao cobre tudo"; }

            return "O teste esta vermelho";
        }

        private static string Detalhe(Desafio d, ResultadoDesafio r)
        {
            string t = "1) O METODO TEM CORPO ....... " + (r.MetodoTemCorpo ? "sim" : "NAO") + "\r\n";
            t = t + "2) O METODO ESTA CERTO ...... " + (r.MetodoOk ? "sim" : "NAO")
                  + "   (" + r.CasosPassaram + " de " + r.Casos.Count + ")\r\n";
            t = t + "3) O TESTE EXISTE E PASSA ... "
                  + (r.TesteExiste ? (r.TestePassa ? "sim" : "NAO") : "NAO EXISTE")
                  + "   (" + r.LinhasDoTeste + " DataRow)\r\n";
            t = t + "4) COBRE AS FRONTEIRAS ...... "
                  + (r.FronteirasFaltando.Count == 0 ? "sim" : "NAO") + "\r\n\r\n";

            if (r.FronteirasFaltando.Count > 0)
            {
                t = t + "Falta no seu teste:\r\n";
                foreach (string f in r.FronteirasFaltando) { t = t + "   - " + f + "\r\n"; }
                t = t + "\r\n";
            }

            if (r.ErroDoTeste != null) { t = t + r.ErroDoTeste + "\r\n\r\n"; }

            foreach (ResultadoCaso c in r.Casos)
            {
                t = t + (c.Passou ? "  [ok] " : "  [--] ") + c.Descricao + "\r\n";

                if (!c.Passou)
                {
                    t = t + "         esperado: " + c.Esperado + "\r\n";
                    t = t + "         obtido:   " + c.Obtido + "\r\n";
                    if (c.Erro != null) { t = t + "         " + c.Erro + "\r\n"; }
                }
            }

            return t;
        }

        // ------------------------------------------------------------- abas
        private void AoTrocarDeAba(object remetente, TabControlCancelEventArgs e)
        {
            int destino = e.TabPageIndex;
            if (destino <= 0) { return; }

            if (Liberada(destino)) { return; }

            e.Cancel = true;

            // a que falta e a primeira aberta do bloco, e nao a de tras
            int falta = InicioDoBloco(destino);
            while (falta < destino && resolvido[falta]) { falta = falta + 1; }

            MessageBox.Show(
                "Esta aba ainda esta travada.\r\n\r\n" +
                "Resolva o desafio " + (falta + 1) + " para destravar o " + (falta + 2) + ".",
                "Aba travada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void PintarAba(object remetente, DrawItemEventArgs e)
        {
            bool feito = e.Index < resolvido.Count && resolvido[e.Index];
            bool liberada = e.Index < desafios.Count && Liberada(e.Index);

            Color fundo = feito ? Color.FromArgb(230, 244, 238)
                                : (liberada ? Color.White : Color.FromArgb(238, 238, 240));
            Color letra = feito ? VERDE : (liberada ? ROXO : CINZA);

            using (SolidBrush b = new SolidBrush(fundo)) { e.Graphics.FillRectangle(b, e.Bounds); }

            TextRenderer.DrawText(e.Graphics, (e.Index + 1).ToString(),
                new Font("Segoe UI", 9F, feito ? FontStyle.Bold : FontStyle.Regular),
                e.Bounds, letra);
        }
    }
}
