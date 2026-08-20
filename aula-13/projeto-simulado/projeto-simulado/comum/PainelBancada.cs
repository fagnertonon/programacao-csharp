using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Conecta
{
    // A BANCADA: a mini-tela viva de cada aba.
    //
    // Ela nao sabe nada sobre desafio nenhum. Monta os campos e os botoes que
    // o JSON pedir e, a cada clique, entrega o id do botao e o que foi digitado
    // para quem a criou. Quem decide o que fazer com isso e a Oficina.
    //
    // Voce NAO precisa mexer aqui.

    // As pecas que precisam ser reencontradas depois da montagem.
    public class PecasBancada
    {
        public Label Faixa;
        public Label Banner;
        public Label Detalhe;
        public ListBox Lista;
        public Label Rodape;
        public string TextoVazio;

        public Dictionary<string, TextBox> Caixas = new Dictionary<string, TextBox>();

        // O banner cresce para baixo conforme o tamanho da mensagem. Estes
        // tres campos existem para empurrar o que vem embaixo dele em vez de
        // deixar a mensagem cobrir a lista.
        public int TopoBanner;
        public int AlturaBase;
        public List<Control> Abaixo = new List<Control>();
        public List<int> AbaixoY = new List<int>();
    }

    public static class PainelBancada
    {
        private const string AVISO_TRAVADA =
            "OS TESTES AINDA NAO PASSARAM.  O botao abaixo roda o corretor primeiro.";
        private const string AVISO_LIBERADA =
            "CODIGO APROVADO.  Esta tela esta rodando o SEU metodo.";

        public static Panel Montar(Bancada b, int largura,
                                   Action<string, Dictionary<string, string>> aoClicar)
        {
            // JSON incompleto nao pode matar o aplicativo: a aba simplesmente
            // fica sem bancada.
            if (b == null || b.Campos == null || b.Botoes == null) { return null; }

            PecasBancada pecas = new PecasBancada();

            Panel p = new Panel();
            p.Width = largura;
            p.BackColor = Color.White;
            p.BorderStyle = BorderStyle.FixedSingle;
            p.Margin = new Padding(0, 4, 0, 14);
            p.Tag = pecas;

            int y = 0;

            // ---- barra escura, imitando o titulo de uma janela ----
            Label barra = new Label();
            barra.Text = "   " + (b.Titulo ?? "Bancada");
            barra.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            barra.ForeColor = Color.White;
            barra.BackColor = Paleta.Dark;
            barra.TextAlign = ContentAlignment.MiddleLeft;
            barra.Size = new Size(largura, 26);
            barra.Location = new Point(0, y);
            p.Controls.Add(barra);
            y += 26;

            // ---- faixa de estado: repintada pelo formulario ----
            Label faixa = new Label();
            faixa.Text = AVISO_TRAVADA;
            faixa.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            faixa.ForeColor = Paleta.Red;
            faixa.BackColor = Paleta.RedBg;
            faixa.TextAlign = ContentAlignment.MiddleLeft;
            faixa.Padding = new Padding(10, 0, 0, 0);
            faixa.Size = new Size(largura, 24);
            faixa.Location = new Point(0, y);
            p.Controls.Add(faixa);
            pecas.Faixa = faixa;
            y += 24 + 8;

            // ---- instrucao ----
            if (!string.IsNullOrEmpty(b.Instrucao))
            {
                Label ins = new Label();
                ins.Text = b.Instrucao;
                ins.Font = UI.Mini;
                ins.ForeColor = Paleta.Body;
                ins.AutoSize = true;
                ins.MaximumSize = new Size(largura - 24, 0);
                ins.Location = new Point(12, y);
                p.Controls.Add(ins);
                y += ins.PreferredHeight + 8;
            }

            // ---- os campos: rotulo a esquerda, caixa a direita ----
            foreach (CampoBancada c in b.Campos)
            {
                Label r = new Label();
                r.Text = c.Rotulo;
                r.Font = UI.Texto;
                r.ForeColor = Paleta.Ink;
                r.TextAlign = ContentAlignment.MiddleRight;
                r.Size = new Size(88, 24);
                r.Location = new Point(12, y);
                p.Controls.Add(r);

                TextBox t = new TextBox();
                t.Font = UI.Texto;
                t.Text = c.Valor ?? "";
                t.Width = (c.Largura > 0 ? c.Largura : 200);
                if (t.Width > largura - 124) { t.Width = largura - 124; }
                t.Location = new Point(108, y);
                if (c.Tipo == "senha") { t.PasswordChar = '*'; }
                p.Controls.Add(t);

                pecas.Caixas[c.Id] = t;
                y += 30;
            }

            y += 4;

            // ---- os botoes, quebrando linha quando nao couber ----
            int x = 12;
            foreach (BotaoBancada bb in b.Botoes)
            {
                Button bt = new Button();
                bt.Text = bb.Texto;
                bt.FlatStyle = FlatStyle.Flat;

                if (bb.Destaque)
                {
                    bt.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                    bt.BackColor = Paleta.Purple;
                    bt.ForeColor = Color.White;
                    bt.FlatAppearance.BorderSize = 0;
                }
                else
                {
                    bt.Font = UI.Mini;
                    bt.BackColor = Color.White;
                    bt.ForeColor = Paleta.Body;
                    bt.FlatAppearance.BorderColor = Paleta.Line;
                }

                int w = TextRenderer.MeasureText(bt.Text, bt.Font).Width + 26;
                bt.Size = new Size(w, 30);

                if (x + w > largura - 12 && x > 12) { x = 12; y += 36; }
                bt.Location = new Point(x, y);
                x += w + 8;

                string idBotao = bb.Id;
                bt.Click += delegate
                {
                    // Um clique de cada vez: se o metodo do aluno demorar, a
                    // fila de cliques nao vira uma pilha de execucoes.
                    bt.Enabled = false;
                    try { aoClicar(idBotao, LerCampos(pecas)); }
                    finally { bt.Enabled = true; }
                };

                p.Controls.Add(bt);
            }
            y += 30 + 10;

            // ---- banner do resultado: nasce com altura zero ----
            pecas.TopoBanner = y;

            Label banner = new Label();
            banner.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            banner.AutoSize = false;
            banner.Size = new Size(largura - 24, 0);
            banner.Location = new Point(12, y);
            banner.Padding = new Padding(8, 6, 8, 2);
            banner.Visible = false;
            p.Controls.Add(banner);
            pecas.Banner = banner;

            Label detalhe = new Label();
            detalhe.Font = UI.Mini;
            detalhe.AutoSize = false;
            detalhe.Size = new Size(largura - 24, 0);
            detalhe.Location = new Point(12, y);
            detalhe.Padding = new Padding(8, 0, 8, 6);
            detalhe.Visible = false;
            p.Controls.Add(detalhe);
            pecas.Detalhe = detalhe;

            // ---- a saida: a lista ----
            if (b.Saida != null)
            {
                Label cap = new Label();
                cap.Text = (b.Saida.Titulo ?? "Resultado").ToUpperInvariant();
                cap.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
                cap.ForeColor = Paleta.PurpleLt;
                cap.AutoSize = true;
                cap.Location = new Point(12, y);
                p.Controls.Add(cap);
                Registrar(pecas, cap, y);
                y += 20;

                ListBox lb = new ListBox();
                lb.Font = new Font("Consolas", 9.5F);
                lb.BorderStyle = BorderStyle.FixedSingle;
                lb.SelectionMode = SelectionMode.None;
                lb.IntegralHeight = false;
                lb.BackColor = Paleta.CodeBg;
                lb.ForeColor = Paleta.Ink;
                lb.Size = new Size(largura - 24, 116);
                lb.Location = new Point(12, y);
                p.Controls.Add(lb);
                Registrar(pecas, lb, y);
                pecas.Lista = lb;
                pecas.TextoVazio = b.Saida.Vazio ?? "(vazio)";
                y += 116 + 6;
            }

            // ---- rodape ----
            Label rod = new Label();
            rod.Text = Memoria.Resumo();
            rod.Font = UI.Mini;
            rod.ForeColor = Paleta.PurpleLt;
            rod.AutoSize = false;
            rod.Size = new Size(largura - 24, 18);
            rod.Location = new Point(12, y);
            p.Controls.Add(rod);
            Registrar(pecas, rod, y);
            pecas.Rodape = rod;
            y += 18 + 10;

            pecas.AlturaBase = y;
            p.Height = y;

            if (pecas.Lista != null) { Preencher(p, Memoria.Contas); }
            return p;
        }

        private static void Registrar(PecasBancada pecas, Control c, int y)
        {
            pecas.Abaixo.Add(c);
            pecas.AbaixoY.Add(y);
        }

        private static Dictionary<string, string> LerCampos(PecasBancada pecas)
        {
            Dictionary<string, string> v = new Dictionary<string, string>();
            foreach (KeyValuePair<string, TextBox> par in pecas.Caixas)
            {
                v[par.Key] = (par.Value.Text ?? "");
            }
            return v;
        }

        private static PecasBancada De(Panel p)
        {
            if (p == null) { return null; }
            return p.Tag as PecasBancada;
        }

        // A faixa de cima: o codigo ja foi aprovado pelo corretor?
        public static void MarcarEstado(Panel p, bool aprovado)
        {
            PecasBancada k = De(p);
            if (k == null || k.Faixa == null) { return; }

            k.Faixa.Text = aprovado ? AVISO_LIBERADA : AVISO_TRAVADA;
            k.Faixa.ForeColor = aprovado ? Paleta.Mint : Paleta.Red;
            k.Faixa.BackColor = aprovado ? Paleta.MintBg : Paleta.RedBg;
        }

        // O banner do resultado do ultimo clique. Empurra a lista para baixo
        // em vez de cobri-la.
        public static void Pintar(Panel p, bool ok, string mensagem, string detalhe)
        {
            PecasBancada k = De(p);
            if (k == null || k.Banner == null) { return; }

            k.Banner.Text = mensagem ?? "";
            k.Banner.ForeColor = ok ? Paleta.Mint : Paleta.Red;
            k.Banner.BackColor = ok ? Paleta.MintBg : Paleta.RedBg;
            k.Banner.Height = AlturaDe(k.Banner) + 10;
            k.Banner.Location = new Point(12, k.TopoBanner);
            k.Banner.Visible = true;

            bool temDetalhe = !string.IsNullOrEmpty(detalhe);
            k.Detalhe.Text = detalhe ?? "";
            k.Detalhe.ForeColor = Paleta.Body;
            k.Detalhe.BackColor = ok ? Paleta.MintBg : Paleta.RedBg;
            k.Detalhe.Height = temDetalhe ? AlturaDe(k.Detalhe) + 8 : 0;
            k.Detalhe.Location = new Point(12, k.TopoBanner + k.Banner.Height);
            k.Detalhe.Visible = temDetalhe;

            int empurrao = k.Banner.Height + k.Detalhe.Height + 10;

            for (int i = 0; i < k.Abaixo.Count; i++)
            {
                k.Abaixo[i].Top = k.AbaixoY[i] + empurrao;
            }
            p.Height = k.AlturaBase + empurrao;
        }

        private static int AlturaDe(Label l)
        {
            Size medida = TextRenderer.MeasureText(l.Text, l.Font,
                              new Size(l.Width - 20, 0), TextFormatFlags.WordBreak);
            return Math.Max(18, medida.Height);
        }

        // A lista de contas, em largura fixa para ler de longe.
        public static void Preencher(Panel p, List<Usuario> contas)
        {
            PecasBancada k = De(p);
            if (k == null || k.Lista == null) { return; }

            k.Lista.Items.Clear();

            if (contas == null || contas.Count == 0)
            {
                k.Lista.Items.Add(k.TextoVazio);
            }
            else
            {
                foreach (Usuario u in contas)
                {
                    k.Lista.Items.Add("#" + u.Id + "  "
                        + Coluna(u.Nome, 18) + "  "
                        + Coluna(u.Login, 12) + "  " + u.Senha);
                }
            }

            if (k.Rodape != null) { k.Rodape.Text = Memoria.Resumo(); }
        }

        private static string Coluna(string texto, int largura)
        {
            texto = texto ?? "";
            if (texto.Length > largura) { return texto.Substring(0, largura - 1) + "."; }
            return texto.PadRight(largura);
        }
    }
}
