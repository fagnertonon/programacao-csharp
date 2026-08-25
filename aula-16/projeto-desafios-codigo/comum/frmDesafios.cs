using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Conecta
{
    // =====================================================================
    // A janela dos dez desafios de codigo. JA ESTA PRONTA.
    //
    //   A ESQUERDA ... o ALGORITMO EM PORTUGOL. E ele que orienta: diz o
    //                  que o metodo tem de fazer, passo a passo.
    //   A DIREITA .... a assinatura do metodo, a ficha, e o resultado dos
    //                  testes - esperado contra obtido, teste a teste.
    //
    // Voce escreve no Desafios.cs, FECHA o programa, roda de novo com F5 e
    // clica em Conferir. Editar com o programa aberto nao muda nada.
    //
    // A janela e montada em codigo. Nao ha Designer.
    // =====================================================================
    public class frmDesafios : Form
    {
        private readonly Conteudo conteudo;
        private readonly List<Button> trilha = new List<Button>();
        private int atual = 0;

        private Label lblTopoDireita, lblNome, lblAssinatura, lblFicha, lblPlacar, lblDica;
        private CaixaDeCodigo caixaPortugol;
        private ListView lstTestes;
        private Button btnConferir, btnTodos, btnAnterior, btnProximo;
        private Panel pnlTrilha;

        private static readonly Color Dark = ColorTranslator.FromHtml("#2D1B45");
        private static readonly Color Purple = ColorTranslator.FromHtml("#5C2D91");
        private static readonly Color Ink = ColorTranslator.FromHtml("#1F1B24");
        private static readonly Color Corpo = ColorTranslator.FromHtml("#3F3A45");
        private static readonly Color Tint = ColorTranslator.FromHtml("#F4F1F9");
        private static readonly Color Linha = ColorTranslator.FromHtml("#DCD3EA");
        private static readonly Color Mint = ColorTranslator.FromHtml("#0E7A55");
        private static readonly Color MintBg = ColorTranslator.FromHtml("#E6F4EE");
        private static readonly Color Vermelho = ColorTranslator.FromHtml("#B02A3C");
        private static readonly Color VermelhoBg = ColorTranslator.FromHtml("#FBEBEE");
        private static readonly Color Ambar = ColorTranslator.FromHtml("#9C6300");
        private static readonly Color CodeBg = ColorTranslator.FromHtml("#FAF8FC");

        private static readonly Font Mono = new Font("Consolas", 9.75F);

        public frmDesafios()
        {
            conteudo = Conteudo.Carregar();
            MontarJanela();
            Mostrar(0);
        }

        private void MontarJanela()
        {
            Text = "Conecta - os 10 desafios de codigo - Aula 16";
            ClientSize = new Size(1180, 780);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.White;
            MinimumSize = new Size(1080, 700);

            Controls.Add(MontarCorpo());
            Controls.Add(MontarRodape());
            Controls.Add(MontarCabecalho());
            Controls.Add(MontarTrilha());
            Controls.Add(MontarTopo());
        }

        private Control MontarTopo()
        {
            Panel p = new Panel();
            p.Dock = DockStyle.Top;
            p.Height = 58;
            p.BackColor = Dark;

            Label t = new Label();
            t.Text = "Os 10 desafios de codigo";
            t.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            t.ForeColor = Color.White;
            t.Location = new Point(18, 8);
            t.AutoSize = true;
            p.Controls.Add(t);

            Label s = new Label();
            s.Text = "voce escreve o metodo no Desafios.cs - aqui o corretor EXECUTA o que voce escreveu";
            s.Font = new Font("Segoe UI", 8.5F);
            s.ForeColor = ColorTranslator.FromHtml("#CBBFE0");
            s.Location = new Point(20, 34);
            s.AutoSize = true;
            p.Controls.Add(s);

            lblTopoDireita = new Label();
            lblTopoDireita.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTopoDireita.ForeColor = Color.White;
            lblTopoDireita.TextAlign = ContentAlignment.MiddleRight;
            lblTopoDireita.Size = new Size(340, 24);
            lblTopoDireita.Location = new Point(p.Width - 360, 17);
            lblTopoDireita.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            p.Controls.Add(lblTopoDireita);

            return p;
        }

        private Control MontarTrilha()
        {
            pnlTrilha = new Panel();
            pnlTrilha.Dock = DockStyle.Top;
            pnlTrilha.Height = 38;
            pnlTrilha.BackColor = Tint;

            for (int i = 0; i < conteudo.Desafios.Count; i++)
            {
                Button b = new Button();
                b.Text = conteudo.Desafios[i].N.ToString();
                b.Size = new Size(30, 26);
                b.Location = new Point(16 + i * 34, 6);
                b.FlatStyle = FlatStyle.Flat;
                b.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
                b.Tag = i;
                b.Click += (s, e) => Mostrar((int)((Button)s).Tag);
                pnlTrilha.Controls.Add(b);
                trilha.Add(b);
            }

            Label aviso = new Label();
            aviso.Text = "todos liberados - empacou num, pule e volte";
            aviso.Font = new Font("Segoe UI", 8F, FontStyle.Italic);
            aviso.ForeColor = Corpo;
            aviso.Location = new Point(16 + conteudo.Desafios.Count * 34 + 14, 12);
            aviso.AutoSize = true;
            pnlTrilha.Controls.Add(aviso);

            return pnlTrilha;
        }

        private Control MontarCabecalho()
        {
            Panel p = new Panel();
            p.Dock = DockStyle.Top;
            p.Height = 46;

            lblNome = new Label();
            lblNome.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblNome.ForeColor = Purple;
            lblNome.Location = new Point(18, 8);
            lblNome.AutoSize = true;
            p.Controls.Add(lblNome);

            lblPlacar = new Label();
            lblPlacar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPlacar.TextAlign = ContentAlignment.MiddleRight;
            lblPlacar.Size = new Size(420, 22);
            lblPlacar.Location = new Point(p.Width - 440, 10);
            lblPlacar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            p.Controls.Add(lblPlacar);

            return p;
        }

        private Control MontarCorpo()
        {
            TableLayoutPanel tudo = new TableLayoutPanel();
            tudo.Dock = DockStyle.Fill;
            tudo.ColumnCount = 2;
            tudo.RowCount = 1;
            tudo.Padding = new Padding(14, 0, 14, 4);
            tudo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 46F));
            tudo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 54F));

            // ------------------------------------------- a esquerda: Portugol
            Panel esq = new Panel();
            esq.Dock = DockStyle.Fill;
            esq.Margin = new Padding(0, 0, 8, 0);

            // NAO e caixa de texto: e um painel que DESENHA o algoritmo.
            // Assim ninguem seleciona, ninguem copia e ninguem cola o
            // Portugol dentro do Desafios.cs. Traduzir e o exercicio.
            caixaPortugol = new CaixaDeCodigo();
            caixaPortugol.Dock = DockStyle.Fill;
            caixaPortugol.BackColor = CodeBg;
            esq.Controls.Add(caixaPortugol);

            Label tituloEsq = new Label();
            tituloEsq.Dock = DockStyle.Top;
            tituloEsq.Height = 20;
            tituloEsq.Text = "O ALGORITMO EM PORTUGOL  -  para LER e traduzir, nao para copiar";
            tituloEsq.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            tituloEsq.ForeColor = Purple;
            esq.Controls.Add(tituloEsq);

            // -------------------------------------------- a direita: os testes
            TableLayoutPanel dir = new TableLayoutPanel();
            dir.Dock = DockStyle.Fill;
            dir.ColumnCount = 1;
            dir.RowCount = 4;
            dir.Margin = new Padding(8, 0, 0, 0);
            dir.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            dir.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            dir.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            dir.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));

            lblAssinatura = new Label();
            lblAssinatura.Dock = DockStyle.Fill;
            lblAssinatura.Font = new Font("Consolas", 10F, FontStyle.Bold);
            lblAssinatura.ForeColor = Ink;
            dir.Controls.Add(lblAssinatura, 0, 0);

            lblFicha = new Label();
            lblFicha.Dock = DockStyle.Fill;
            lblFicha.Font = new Font("Segoe UI", 8.5F);
            lblFicha.ForeColor = Ink;
            lblFicha.BackColor = MintBg;
            lblFicha.Padding = new Padding(8, 4, 4, 2);
            dir.Controls.Add(lblFicha, 0, 1);

            lstTestes = new ListView();
            lstTestes.Dock = DockStyle.Fill;
            lstTestes.View = View.Details;
            lstTestes.FullRowSelect = true;
            lstTestes.GridLines = true;
            lstTestes.Font = new Font("Segoe UI", 8.5F);
            lstTestes.Columns.Add("", 26);
            lstTestes.Columns.Add("o teste", 250);
            lstTestes.Columns.Add("esperado", 175);
            lstTestes.Columns.Add("o que voltou", 175);
            dir.Controls.Add(lstTestes, 0, 2);

            lblDica = new Label();
            lblDica.Dock = DockStyle.Fill;
            lblDica.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
            lblDica.ForeColor = Ambar;
            dir.Controls.Add(lblDica, 0, 3);

            tudo.Controls.Add(esq, 0, 0);
            tudo.Controls.Add(dir, 1, 0);
            return tudo;
        }

        private Control MontarRodape()
        {
            Panel p = new Panel();
            p.Dock = DockStyle.Bottom;
            p.Height = 86;
            p.BackColor = Tint;

            btnConferir = new Button();
            btnConferir.Text = "Conferir este";
            btnConferir.Size = new Size(150, 34);
            btnConferir.Location = new Point(16, 12);
            btnConferir.BackColor = Purple;
            btnConferir.ForeColor = Color.White;
            btnConferir.FlatStyle = FlatStyle.Flat;
            btnConferir.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnConferir.Click += (s, e) => Mostrar(atual);
            p.Controls.Add(btnConferir);

            btnTodos = new Button();
            btnTodos.Text = "Conferir todos";
            btnTodos.Size = new Size(130, 34);
            btnTodos.Location = new Point(176, 12);
            btnTodos.Click += (s, e) => ConferirTodos();
            p.Controls.Add(btnTodos);

            Label nota = new Label();
            nota.Text = "O algoritmo da esquerda nao pode ser copiado, de proposito: o caminho "
                      + "dele ate o seu programa passa pelos seus dedos.  |  Escreveu no "
                      + "Desafios.cs? FECHE o programa e rode de novo com F5.";
            nota.Location = new Point(18, 54);
            nota.Size = new Size(1000, 18);
            nota.Font = new Font("Segoe UI", 8F, FontStyle.Italic);
            nota.ForeColor = Ambar;
            p.Controls.Add(nota);

            btnProximo = new Button();
            btnProximo.Text = "Proximo  >";
            btnProximo.Size = new Size(120, 30);
            btnProximo.Location = new Point(p.Width - 150, 14);
            btnProximo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnProximo.Click += (s, e) => Mostrar(atual + 1);
            p.Controls.Add(btnProximo);

            btnAnterior = new Button();
            btnAnterior.Text = "<  Anterior";
            btnAnterior.Size = new Size(120, 30);
            btnAnterior.Location = new Point(p.Width - 278, 14);
            btnAnterior.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAnterior.Click += (s, e) => Mostrar(atual - 1);
            p.Controls.Add(btnAnterior);

            return p;
        }

        // =================================================================
        private void Mostrar(int indice)
        {
            if (indice < 0 || indice >= conteudo.Desafios.Count) return;

            atual = indice;
            Desafio d = conteudo.Desafios[indice];

            lblTopoDireita.Text = "DESAFIO " + d.N + " de " + conteudo.Desafios.Count;
            lblNome.Text = d.N + ".  " + d.Titulo;
            lblAssinatura.Text = d.Metodo;
            lblFicha.Text = "RECEBE:  " + d.Ficha.Recebe
                + Environment.NewLine + "FAZ:     " + d.Ficha.Faz
                + Environment.NewLine + "DEVOLVE: " + d.Ficha.Devolve;

            caixaPortugol.Definir(d.Portugol, Ink, ColorTranslator.FromHtml("#7A7285"));

            List<Resultado> testes = Corretor.Conferir(d.N);
            int passaram = 0;

            lstTestes.Items.Clear();
            foreach (Resultado r in testes)
            {
                if (r.Passou) passaram++;

                ListViewItem item = new ListViewItem(r.Passou ? "ok" : "x");
                item.SubItems.Add(r.Descricao);
                item.SubItems.Add(r.Esperado);
                item.SubItems.Add(r.Obtido);
                item.ForeColor = r.Passou ? Mint : Vermelho;
                item.BackColor = r.Passou ? MintBg : VermelhoBg;
                lstTestes.Items.Add(item);
            }

            bool fechou = testes.Count > 0 && passaram == testes.Count;
            lblPlacar.Text = passaram + " de " + testes.Count + " testes passando";
            lblPlacar.ForeColor = fechou ? Mint : Vermelho;
            lblDica.Text = fechou ? "Desafio fechado." : ("Dica:  " + d.Dica);
            lblDica.ForeColor = fechou ? Mint : Ambar;

            btnAnterior.Enabled = indice > 0;
            btnProximo.Enabled = indice < conteudo.Desafios.Count - 1;

            PintarTrilha();
        }

        private void ConferirTodos()
        {
            int fechados = 0;
            foreach (Desafio d in conteudo.Desafios)
            {
                if (Corretor.Fechou(d.N)) fechados++;
            }

            PintarTrilha();

            MessageBox.Show(
                fechados + " de " + conteudo.Desafios.Count + " desafios fechados.",
                "Conecta - desafios de codigo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void PintarTrilha()
        {
            for (int i = 0; i < trilha.Count; i++)
            {
                bool fechou = Corretor.Fechou(conteudo.Desafios[i].N);

                trilha[i].BackColor = fechou ? Mint : Color.White;
                trilha[i].ForeColor = fechou ? Color.White : Corpo;
                trilha[i].FlatAppearance.BorderColor = fechou ? Mint : Linha;
                trilha[i].FlatAppearance.BorderSize = (i == atual) ? 2 : 1;
                if (i == atual) trilha[i].FlatAppearance.BorderColor = Purple;
            }
        }
    }
}
