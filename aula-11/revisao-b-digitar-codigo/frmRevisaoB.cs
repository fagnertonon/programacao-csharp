using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Revisao
{
    // VARIANTE B - o aluno digita o codigo numa caixa dentro do app.
    //
    // NAO existe compilador aqui: veja a explicacao inteira em Validador.cs.
    // O app compara o texto normalizado com as formas aceitas e, quando bate,
    // roda a implementacao de referencia para mostrar a saida.

    public class frmRevisaoB : Form
    {
        // 640 e a largura para a qual os diagramas de ../comum/Diagramas.cs
        // foram desenhados. Com menos que isso, as anotacoes da direita cortam.
        private const int LARG_ENSINO = 640;
        private const int LARG_DESAFIO = 520;

        private Conteudo conteudo;
        private TabControl abas;
        private Label rodape;

        private readonly List<bool> resolvido = new List<bool>();
        private readonly List<SplitContainer> divisores = new List<SplitContainer>();

        public frmRevisaoB()
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
            Text = conteudo.Aula + " - " + conteudo.Titulo + "  [B: digitar o codigo]";
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
                       + "   |   digite o corpo do metodo e clique em conferir";
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
                MontarEditor(div.Panel2, t, i);

                pagina.Controls.Add(div);
                abas.TabPages.Add(pagina);

                resolvido.Add(false);
            }
        }

        private void MontarEditor(Panel destino, Topico t, int indice)
        {
            FlowLayoutPanel fluxo = UI.NovoFluxo(destino, LARG_DESAFIO);

            fluxo.Controls.Add(UI.Titulo("Desafio " + t.Numero,
                "digite o corpo do metodo aqui", LARG_DESAFIO));

            fluxo.Controls.Add(UI.Paragrafo(t.Desafio.Enunciado, LARG_DESAFIO));

            fluxo.Controls.Add(UI.Subtitulo("a assinatura, ja pronta"));
            fluxo.Controls.Add(UI.Codigo(t.Desafio.Assinatura, LARG_DESAFIO));

            fluxo.Controls.Add(UI.Subtitulo("o corpo, que e a sua parte"));

            TextBox editor = new TextBox();
            editor.Multiline = true;
            editor.AcceptsTab = true;
            editor.WordWrap = false;
            editor.ScrollBars = ScrollBars.Both;
            editor.Font = UI.Mono;
            editor.BackColor = Color.White;
            editor.ForeColor = Paleta.Ink;
            editor.BorderStyle = BorderStyle.FixedSingle;
            editor.Width = LARG_DESAFIO;
            editor.Height = 150;
            editor.Margin = new Padding(0, 2, 0, 8);
            fluxo.Controls.Add(editor);

            Label resposta = new Label();
            resposta.AutoSize = false;
            resposta.Width = LARG_DESAFIO;
            resposta.Height = 54;
            resposta.TextAlign = ContentAlignment.MiddleLeft;
            resposta.Padding = new Padding(12, 0, 12, 0);
            resposta.Font = UI.Texto;
            resposta.Margin = new Padding(0, 8, 0, 8);

            TextBox saida = new TextBox();
            saida.Multiline = true;
            saida.ReadOnly = true;
            saida.WordWrap = false;
            saida.ScrollBars = ScrollBars.Vertical;
            saida.Font = UI.Mono;
            saida.BackColor = Paleta.CodeBg;
            saida.ForeColor = Paleta.Body;
            saida.BorderStyle = BorderStyle.FixedSingle;
            saida.Width = LARG_DESAFIO;
            saida.Height = 110;
            saida.Visible = false;
            saida.Margin = new Padding(0, 0, 0, 12);

            Button btn = UI.Botao("Conferir o meu codigo", LARG_DESAFIO, true);
            btn.Click += delegate
            {
                string motivo;
                bool certo = Validador.Confere(t, editor.Text, out motivo);

                if (certo)
                {
                    resolvido[indice] = true;
                    resposta.Text = "Certo!  " + t.FolhaRevisao;
                    resposta.BackColor = Paleta.MintBg;
                    resposta.ForeColor = Paleta.Mint;

                    saida.Text = "O que o seu codigo tem que devolver:\r\n\r\n" + Esperados(t);
                    saida.Visible = true;
                }
                else
                {
                    resposta.Text = motivo;
                    resposta.BackColor = Paleta.RedBg;
                    resposta.ForeColor = Paleta.Red;
                    saida.Visible = false;
                }

                AtualizarAbas();
                UI.DesligarMnemonicos(this);
            };
            fluxo.Controls.Add(btn);

            Button btnDica = UI.Botao("Ver a dica", 140, false);
            btnDica.Click += delegate
            {
                MessageBox.Show(t.Desafio.Dica, "Dica do desafio " + t.Numero,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            fluxo.Controls.Add(btnDica);

            fluxo.Controls.Add(resposta);
            fluxo.Controls.Add(saida);

            fluxo.Controls.Add(UI.Aviso("Isto aqui nao e um compilador",
                "O app compara o que voce escreveu com as formas de escrever que ele "
                + "conhece. Se o seu codigo estiver certo de um jeito diferente, ele "
                + "pode nao reconhecer - nesse caso, chame o professor.",
                Paleta.Amber, Paleta.AmberBg, LARG_DESAFIO));
        }

        private EstadoAba EstadoDaAba(int indice)
        {
            if (resolvido[indice]) { return EstadoAba.Resolvida; }

            int liberadas = Destravadas.Contar(resolvido, abas.TabPages.Count);
            return indice < liberadas ? EstadoAba.Liberada : EstadoAba.Travada;
        }

        // A tabela entrada -> esperado, direto do conteudo-revisao.json. Antes
        // isto vinha de uma implementacao de referencia (o gabarito em C#) que
        // morava nesta pasta - ou seja, ia para a maquina do aluno.
        private static string Esperados(Topico t)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            foreach (Teste teste in t.Desafio.Testes)
            {
                string entrada = string.IsNullOrEmpty(teste.Entrada) ? "(vazio)" : teste.Entrada;
                string esperado = string.IsNullOrEmpty(teste.Esperado) ? "(vazio)" : teste.Esperado;
                sb.AppendLine(entrada + "   ->   " + esperado);
            }

            return sb.ToString().TrimEnd();
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
                        + "     |     abas liberadas: " + liberadas;
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
