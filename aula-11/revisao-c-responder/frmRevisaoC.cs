using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Revisao
{
    // VARIANTE C - o aluno responde sem digitar codigo.
    //
    // O codigo aparece na tela e ele escolhe a alternativa certa. E a variante
    // mais rapida em sala: nao exige digitar codigo nem recompilar.
    //
    // O lado do ensino e o mesmo das outras duas: ../comum/PainelEnsino.cs.

    public class frmRevisaoC : Form
    {
        private const int LARG_ENSINO = 640;
        private const int LARG_DESAFIO = 500;

        private Conteudo conteudo;
        private TabControl abas;
        private Label rodape;

        private readonly List<bool> resolvido = new List<bool>();
        private readonly List<SplitContainer> divisores = new List<SplitContainer>();
        private readonly List<Label> retorno = new List<Label>();
        private readonly List<bool[]> certasPorAba = new List<bool[]>();

        public frmRevisaoC()
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
            AtualizarAbas();
            UI.DesligarMnemonicos(this);
            Pintura.Suavizar(this);
        }

        private void MontarJanela()
        {
            Text = conteudo.Aula + " - " + conteudo.Titulo + "  [C: responder]";
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
                       + "   |   leia a explicacao e escolha a resposta certa";
            sub.Font = UI.Mini;
            sub.ForeColor = Paleta.PurpleLt;
            sub.AutoSize = true;
            sub.Location = new Point(20, 38);
            cabecalho.Controls.Add(sub);

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
            abas.Selecting += delegate (object s, TabControlCancelEventArgs e)
            {
                Destravadas.BloquearSeTravada(e, resolvido, conteudo);
            };

            Controls.Add(abas);
            Controls.Add(rodape);
            Controls.Add(cabecalho);
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
                MontarPergunta(div.Panel2, t, i);

                pagina.Controls.Add(div);
                abas.TabPages.Add(pagina);

                resolvido.Add(false);
            }
        }

        private void MontarPergunta(Panel destino, Topico t, int indice)
        {
            FlowLayoutPanel fluxo = UI.NovoFluxo(destino, LARG_DESAFIO);

            int quantas = t.Quiz.Count;

            fluxo.Controls.Add(UI.Titulo(quantas == 1 ? "Pergunta " + t.Numero : "Perguntas",
                quantas == 1
                    ? "leia a explicacao ao lado antes de responder"
                    : "sao " + quantas + " aqui - a aba destrava quando as duas estiverem certas",
                LARG_DESAFIO));

            // Uma lista de acertos por pergunta. A aba so destrava com todas.
            bool[] acertou = new bool[quantas];
            List<Label> retornos = new List<Label>();

            for (int q = 0; q < quantas; q++)
            {
                Quiz pergunta = t.Quiz[q];
                int indiceQ = q;

                if (quantas > 1)
                {
                    fluxo.Controls.Add(UI.Subtitulo("pergunta " + (q + 1) + " de " + quantas));
                }

                fluxo.Controls.Add(UI.Paragrafo(pergunta.Pergunta, LARG_DESAFIO));

                // Cada pergunta precisa do seu proprio painel: RadioButtons no
                // mesmo container se excluem entre si, e com duas perguntas
                // soltas marcar uma desmarcaria a resposta da outra.
                Panel grupo = new Panel();
                grupo.Width = LARG_DESAFIO;
                grupo.Margin = new Padding(0, 0, 0, 6);
                grupo.BackColor = Color.Transparent;

                List<RadioButton> opcoes = new List<RadioButton>();
                int y = 0;

                for (int i = 0; i < pergunta.Alternativas.Count; i++)
                {
                    RadioButton r = new RadioButton();
                    r.Text = "  " + pergunta.Alternativas[i];
                    r.Font = UI.Texto;
                    r.ForeColor = Paleta.Ink;
                    r.AutoSize = false;
                    r.Width = LARG_DESAFIO;
                    r.Height = Math.Max(32, TextRenderer.MeasureText(r.Text,
                        UI.Texto, new Size(LARG_DESAFIO - 40, 0), TextFormatFlags.WordBreak).Height + 14);
                    r.Location = new Point(0, y);
                    r.Tag = i;
                    grupo.Controls.Add(r);
                    opcoes.Add(r);
                    y += r.Height + 4;
                }

                grupo.Height = y;
                fluxo.Controls.Add(grupo);

                Label resposta = new Label();
                resposta.AutoSize = false;
                resposta.Width = LARG_DESAFIO;
                resposta.Height = 60;
                resposta.TextAlign = ContentAlignment.MiddleLeft;
                resposta.Padding = new Padding(12, 0, 12, 0);
                resposta.Font = UI.H2;
                resposta.Margin = new Padding(0, 6, 0, 10);
                // Nasce escondido: um rotulo vazio de 60px abria um vao entre
                // a pergunta 1 e a pergunta 2.
                resposta.Visible = false;
                retornos.Add(resposta);
                retorno.Add(resposta);

                Button btn = UI.Botao(
                    quantas == 1 ? "Conferir a resposta" : "Conferir a pergunta " + (q + 1),
                    LARG_DESAFIO, true);

                btn.Click += delegate
                {
                    int escolhida = -1;
                    foreach (RadioButton r in opcoes)
                    {
                        if (r.Checked) { escolhida = (int)r.Tag; }
                    }

                    resposta.Visible = true;

                    if (escolhida < 0)
                    {
                        resposta.Text = "Escolha uma alternativa primeiro.";
                        resposta.BackColor = Paleta.Tint;
                        resposta.ForeColor = Paleta.Body;
                        return;
                    }

                    if (escolhida == pergunta.Certa)
                    {
                        acertou[indiceQ] = true;
                        resposta.Text = "Certo!  " + t.FolhaRevisao;
                        resposta.BackColor = Paleta.MintBg;
                        resposta.ForeColor = Paleta.Mint;
                        foreach (RadioButton r in opcoes) { r.Enabled = false; }
                    }
                    else
                    {
                        resposta.Text = "Ainda nao. Releia a explicacao ao lado e tente de novo.";
                        resposta.BackColor = Paleta.RedBg;
                        resposta.ForeColor = Paleta.Red;
                    }

                    bool todas = true;
                    foreach (bool a in acertou) { if (!a) { todas = false; } }
                    resolvido[indice] = todas;

                    AtualizarAbas();
                    UI.DesligarMnemonicos(this);
                };

                fluxo.Controls.Add(btn);
                fluxo.Controls.Add(resposta);
            }

            certasPorAba.Add(acertou);
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

            int certas = 0;
            int total = 0;
            foreach (bool[] aba in certasPorAba)
            {
                total += aba.Length;
                foreach (bool a in aba) { if (a) { certas++; } }
            }

            rodape.Text = "Perguntas certas: " + certas + " de " + total
                        + "     |     abas liberadas: " + liberadas + " de " + resolvido.Count;
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
    }
}
