using System;
using System.Drawing;
using System.Windows.Forms;

namespace Revisao
{
    // As pecas visuais e o painel de ensino, compartilhados pelas TRES variantes.
    // O que muda de uma variante para a outra e so o lado do desafio.
    //
    // Voce NAO precisa mexer aqui.

    public static class UI
    {
        public static readonly Font H1 = new Font("Segoe UI", 15F, FontStyle.Bold);
        public static readonly Font H2 = new Font("Segoe UI", 10.5F, FontStyle.Bold);
        public static readonly Font Texto = new Font("Segoe UI", 10F);
        public static readonly Font Mini = new Font("Segoe UI", 8.5F);
        public static readonly Font Mono = new Font("Consolas", 10F);
        public static readonly Font MonoB = new Font("Consolas", 10F, FontStyle.Bold);

        public static FlowLayoutPanel NovoFluxo(Panel destino, int largura)
        {
            FlowLayoutPanel f = new FlowLayoutPanel();
            f.FlowDirection = FlowDirection.TopDown;
            f.WrapContents = false;
            f.AutoSize = true;
            f.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            f.Padding = new Padding(18, 14, 18, 20);
            f.Width = largura + 40;
            destino.Controls.Add(f);
            return f;
        }

        public static Control Titulo(string texto, string sub, int largura)
        {
            Panel p = new Panel();
            p.Width = largura;
            p.Height = 54;
            p.Margin = new Padding(0, 0, 0, 8);

            Label a = new Label();
            a.Text = texto;
            a.Font = H1;
            a.ForeColor = Paleta.Purple;
            a.AutoSize = true;
            a.MaximumSize = new Size(largura, 0);
            a.Location = new Point(0, 0);
            p.Controls.Add(a);

            Label b = new Label();
            b.Text = sub;
            b.Font = Mini;
            b.ForeColor = Paleta.PurpleLt;
            b.AutoSize = true;
            b.Location = new Point(2, 30);
            p.Controls.Add(b);

            return p;
        }

        public static Control Subtitulo(string texto)
        {
            Label l = new Label();
            l.Text = (texto ?? "").ToUpperInvariant();
            l.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            l.ForeColor = Paleta.PurpleLt;
            l.AutoSize = true;
            l.Margin = new Padding(0, 12, 0, 2);
            return l;
        }

        public static Control Paragrafo(string texto, int largura)
        {
            Label l = new Label();
            l.Text = texto;
            l.Font = Texto;
            l.ForeColor = Paleta.Body;
            l.AutoSize = true;
            l.MaximumSize = new Size(largura, 0);
            l.Margin = new Padding(0, 0, 0, 10);
            return l;
        }

        public static Control Legenda(string texto, int largura)
        {
            Label l = new Label();
            l.Text = texto;
            l.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
            l.ForeColor = Paleta.PurpleLt;
            l.AutoSize = true;
            l.MaximumSize = new Size(largura, 0);
            l.Margin = new Padding(2, 2, 0, 12);
            return l;
        }

        // Caixa de codigo somente leitura. Nao e editavel: o aluno escreve no
        // Visual Studio (variante A) ou na caixa propria (variante B).
        //
        // A barra de rolagem horizontal do TextBox multiline aparece SEMPRE que
        // WordWrap esta desligado - mesmo quando o texto cabe - e come uns 17px
        // da altura. Numa caixa de uma linha so isso engolia a linha inteira, e
        // a assinatura do metodo aparecia em branco. Por isso a barra so e
        // ligada quando o texto realmente nao cabe, e quando e ligada a altura
        // dela entra na conta.
        public static Control Codigo(string texto, int largura)
        {
            TextBox t = new TextBox();
            t.Multiline = true;
            t.ReadOnly = true;
            t.WordWrap = false;
            t.Font = Mono;
            t.BackColor = Paleta.CodeBg;
            t.ForeColor = Paleta.Ink;
            t.BorderStyle = BorderStyle.FixedSingle;
            t.Text = (texto ?? "").Replace("\n", "\r\n");
            t.Width = largura;
            t.Margin = new Padding(0, 2, 0, 6);

            int maisLarga = 0;
            foreach (string linha in t.Lines)
            {
                int w = TextRenderer.MeasureText(linha, Mono).Width;
                if (w > maisLarga) { maisLarga = w; }
            }

            bool precisaRolar = maisLarga > largura - 10;
            t.ScrollBars = precisaRolar ? ScrollBars.Horizontal : ScrollBars.None;

            int altura = Math.Max(1, t.Lines.Length) * 17 + 14;
            if (precisaRolar) { altura += SystemInformation.HorizontalScrollBarHeight; }
            t.Height = Math.Max(30, altura);

            t.GotFocus += delegate { t.Select(0, 0); };
            return t;
        }

        public static Control Aviso(string titulo, string corpo, Color cor, Color fundo, int largura)
        {
            Panel p = new Panel();
            p.Width = largura;
            p.BackColor = fundo;
            p.Margin = new Padding(0, 6, 0, 12);

            Label a = new Label();
            a.Text = titulo;
            a.Font = H2;
            a.ForeColor = cor;
            a.AutoSize = true;
            a.MaximumSize = new Size(largura - 30, 0);
            a.Location = new Point(12, 10);
            p.Controls.Add(a);

            Label b = new Label();
            b.Text = corpo;
            b.Font = Texto;
            b.ForeColor = Paleta.Ink;
            b.AutoSize = true;
            b.MaximumSize = new Size(largura - 30, 0);
            b.Location = new Point(12, 12 + a.PreferredHeight);
            p.Controls.Add(b);

            p.Height = 24 + a.PreferredHeight + b.PreferredHeight;
            return p;
        }

        // O WinForms trata & no texto de Label, Button e RadioButton como tecla
        // de atalho: "&&" aparece como "&" e some um caractere. Numa aula que
        // ensina justamente && e ||, isso trocava o conteudo na cara do aluno.
        // Chame isto uma vez, depois de montar a tela.
        public static void DesligarMnemonicos(Control raiz)
        {
            Label rotulo = raiz as Label;
            if (rotulo != null) { rotulo.UseMnemonic = false; }

            ButtonBase botao = raiz as ButtonBase;
            if (botao != null) { botao.UseMnemonic = false; }

            foreach (Control filho in raiz.Controls)
            {
                DesligarMnemonicos(filho);
            }
        }

        public static Button Botao(string texto, int largura, bool destaque)
        {
            Button b = new Button();
            b.Text = texto;
            b.FlatStyle = FlatStyle.Flat;
            b.Width = largura;

            if (destaque)
            {
                b.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                b.Height = 40;
                b.BackColor = Paleta.Purple;
                b.ForeColor = Color.White;
                b.FlatAppearance.BorderSize = 0;
                b.Margin = new Padding(0, 0, 0, 12);
            }
            else
            {
                b.Font = Mini;
                b.Height = 28;
                b.BackColor = Color.White;
                b.FlatAppearance.BorderColor = Paleta.Line;
                b.Margin = new Padding(0, 6, 0, 10);
            }

            return b;
        }
    }

    public static class PainelEnsino
    {
        // Monta o lado esquerdo: explicacao, diagrama, exemplo, armadilha e a
        // frase da folha de revisao. E identico nas tres variantes - o que muda
        // e so o desafio, do outro lado da tela.
        public static void Montar(Panel destino, Topico t, int largura)
        {
            FlowLayoutPanel fluxo = UI.NovoFluxo(destino, largura);

            fluxo.Controls.Add(UI.Titulo(t.Numero + ". " + t.Titulo, t.Subtitulo, largura));

            if (t.Novo)
            {
                fluxo.Controls.Add(UI.Aviso(
                    "ASSUNTO NOVO DESTA AULA",
                    "Os outros sete sao revisao do que voce ja viu. Este e novo - aprenda "
                    + "com calma, sem correr.",
                    Paleta.Amber, Paleta.AmberBg, largura));
            }

            foreach (string paragrafo in t.Explicacao)
            {
                fluxo.Controls.Add(UI.Paragrafo(paragrafo, largura));
            }

            fluxo.Controls.Add(UI.Subtitulo("Como isso se desenha"));
            Diagrama d = new Diagrama();
            d.Width = largura;
            d.Margin = new Padding(0, 4, 0, 10);
            d.Definir(t.Diagrama);   // Definir tambem ajusta a altura
            fluxo.Controls.Add(d);

            fluxo.Controls.Add(UI.Subtitulo("Exemplo"));
            fluxo.Controls.Add(UI.Codigo(t.Exemplo.Codigo, largura));
            fluxo.Controls.Add(UI.Legenda(t.Exemplo.Legenda, largura));

            if (t.Armadilha != null)
            {
                string corpo = t.Armadilha.Texto;

                if (!string.IsNullOrEmpty(t.Armadilha.ErroCompilador))
                {
                    corpo += "\r\n\r\nO compilador diz:\r\n" + t.Armadilha.ErroCompilador;
                }

                if (!string.IsNullOrEmpty(t.Armadilha.RegraDoCurso))
                {
                    corpo += "\r\n\r\n" + t.Armadilha.RegraDoCurso;
                }

                fluxo.Controls.Add(UI.Aviso("ARMADILHA: " + t.Armadilha.Titulo, corpo,
                                            Paleta.Red, Paleta.RedBg, largura));
            }

            fluxo.Controls.Add(UI.Aviso("GUARDE ESTA FRASE", t.FolhaRevisao,
                                        Paleta.Mint, Paleta.MintBg, largura));
        }
    }
}
