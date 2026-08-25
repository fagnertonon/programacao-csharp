using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Conecta
{
    // =====================================================================
    // O MODO FACIL da Aula 16, em duas rodadas.
    //
    //   1. OS 9 PASSOS ...... o Portugol a esquerda, o C# com lacunas a
    //                         direita. Voce escolhe a peca que falta.
    //   2. AS 10 PERGUNTAS .. sobre o assunto da noite - metodo, parametro
    //                         e retorno -, sempre com um trecho de
    //                         Portugol na tela para responder olhando.
    // A esquerda e SEMPRE o Portugol. E ele que orienta as duas rodadas -
    // e e o mesmo que esta na folha, na sua mao.
    //
    // OS DESAFIOS DE CODIGO NAO ESTAO AQUI. Eles sao em arquivo, no
    // projeto-desafios-codigo: la o aluno escreve o metodo no Desafios.cs,
    // roda com F5, e o corretor EXECUTA o que ele escreveu. Aqui e o
    // degrau de baixo: escolher a peca certa.
    //
    // A janela toda e montada em codigo, aqui embaixo. Nao ha Designer.
    // =====================================================================
    public class frmDesafio : Form
    {
        private enum Rodada { Passos = 0, Perguntas = 1 }

        private readonly Conteudo conteudo;
        private readonly bool[][] resolvido = new bool[2][];
        private readonly int[] itemAtual = new int[2];

        private Rodada rodada = Rodada.Passos;

        private readonly List<Button> botoesRodada = new List<Button>();
        private readonly List<Button> botoesTrilha = new List<Button>();
        private readonly List<ComboBox> combos = new List<ComboBox>();
        private readonly List<RadioButton> radios = new List<RadioButton>();
        private readonly List<Label> selos = new List<Label>();

        // O passo que esta na tela, o que o aluno escolheu em cada lacuna, e
        // como cada uma ficou depois do Conferir (0 = ainda nao conferida,
        // 1 = certa, 2 = errada). E com isto que o codigo da direita se
        // preenche: escolheu, apareceu.
        private Passo passoAtual;
        private readonly Dictionary<int, string> escolhas = new Dictionary<int, string>();
        private readonly Dictionary<int, int> situacao = new Dictionary<int, int>();

        private Label lblPasso, lblNome, lblOnde, lblEstreia, lblTituloEsq;
        private Label lblRecebe, lblFaz, lblDevolve, lblResultado, lblEnunciado;
        private CaixaDeCodigo caixaEsquerda, caixaCodigo;
        private FlowLayoutPanel pnlInterativo;
        private Panel pnlTrilha, pnlFicha, pnlTopoDireita;
        private TableLayoutPanel colunaDireita;
        private Button btnConferir, btnAnterior, btnProximo;

        // ---------------------------------------------------------- cores
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
        private static readonly Color Comentario = ColorTranslator.FromHtml("#7A7285");

        private static readonly Font Mono = new Font("Consolas", 9.75F);
        private static readonly Font MonoNegrito = new Font("Consolas", 9.75F, FontStyle.Bold);

        public frmDesafio()
        {
            conteudo = Conteudo.Carregar();

            resolvido[(int)Rodada.Passos] = new bool[conteudo.Passos.Count];
            resolvido[(int)Rodada.Perguntas] = new bool[conteudo.Perguntas.Count];

            MontarJanela();
            TrocarRodada(Rodada.Passos);
        }

        private int Quantos(Rodada r)
        {
            return r == Rodada.Passos ? conteudo.Passos.Count
                                      : conteudo.Perguntas.Count;
        }

        // =================================================================
        // A JANELA
        // =================================================================
        private void MontarJanela()
        {
            Text = "Conecta - do Portugol para o C# - modo facil";
            ClientSize = new Size(1180, 800);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.White;
            MinimumSize = new Size(1100, 740);

            Controls.Add(MontarCorpo());
            Controls.Add(MontarRodape());
            Controls.Add(MontarCabecalho());
            Controls.Add(MontarTrilha());
            Controls.Add(MontarBarraDeRodadas());
            Controls.Add(MontarTopo());
        }

        private Control MontarTopo()
        {
            Panel p = new Panel();
            p.Dock = DockStyle.Top;
            p.Height = 56;
            p.BackColor = Dark;

            Label t = new Label();
            t.Text = "Do Portugol para o C#";
            t.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            t.ForeColor = Color.White;
            t.Location = new Point(18, 8);
            t.AutoSize = true;
            p.Controls.Add(t);

            Label s = new Label();
            s.Text = "modo facil - a esquerda o algoritmo, sempre; a direita, o C#";
            s.Font = new Font("Segoe UI", 8.5F);
            s.ForeColor = ColorTranslator.FromHtml("#CBBFE0");
            s.Location = new Point(20, 33);
            s.AutoSize = true;
            p.Controls.Add(s);

            lblPasso = new Label();
            lblPasso.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblPasso.ForeColor = Color.White;
            lblPasso.TextAlign = ContentAlignment.MiddleRight;
            lblPasso.Size = new Size(360, 24);
            lblPasso.Location = new Point(p.Width - 380, 16);
            lblPasso.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            p.Controls.Add(lblPasso);

            return p;
        }

        private Control MontarBarraDeRodadas()
        {
            Panel p = new Panel();
            p.Dock = DockStyle.Top;
            p.Height = 40;
            p.BackColor = ColorTranslator.FromHtml("#3D2A5C");

            string[] nomes = {
                "1  ·  OS 9 PASSOS",
                "2  ·  AS 10 PERGUNTAS",
            };

            for (int i = 0; i < nomes.Length; i++)
            {
                Button b = new Button();
                b.Text = nomes[i];
                b.Size = new Size(190, 30);
                b.Location = new Point(16 + i * 198, 5);
                b.FlatStyle = FlatStyle.Flat;
                b.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
                b.Tag = (Rodada)i;
                b.Click += (s, e) => TrocarRodada((Rodada)((Button)s).Tag);
                p.Controls.Add(b);
                botoesRodada.Add(b);
            }

            return p;
        }

        // A trilha: todos os itens da rodada, TODOS liberados desde o
        // comeco. Empacou num? Pula e volta - foi o que funcionou na
        // Aula 13, e esta no registro.
        private Control MontarTrilha()
        {
            pnlTrilha = new Panel();
            pnlTrilha.Dock = DockStyle.Top;
            pnlTrilha.Height = 38;
            pnlTrilha.BackColor = Tint;
            return pnlTrilha;
        }

        private void MontarBotoesDaTrilha()
        {
            pnlTrilha.Controls.Clear();
            botoesTrilha.Clear();

            int total = Quantos(rodada);

            for (int i = 0; i < total; i++)
            {
                Button b = new Button();
                b.Text = (i + 1).ToString();
                b.Size = new Size(30, 26);
                b.Location = new Point(16 + i * 34, 6);
                b.FlatStyle = FlatStyle.Flat;
                b.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
                b.Tag = i;
                b.Click += (s, e) => MostrarItem((int)((Button)s).Tag);
                pnlTrilha.Controls.Add(b);
                botoesTrilha.Add(b);
            }

            Label aviso = new Label();
            aviso.Text = "todos liberados - empacou num, pule e volte";
            aviso.Font = new Font("Segoe UI", 8F, FontStyle.Italic);
            aviso.ForeColor = Corpo;
            aviso.Location = new Point(16 + total * 34 + 14, 12);
            aviso.AutoSize = true;
            pnlTrilha.Controls.Add(aviso);
        }

        private Control MontarCabecalho()
        {
            Panel p = new Panel();
            p.Dock = DockStyle.Top;
            p.Height = 50;
            p.BackColor = Color.White;

            lblNome = new Label();
            lblNome.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblNome.ForeColor = Purple;
            lblNome.Location = new Point(18, 8);
            lblNome.AutoSize = true;
            p.Controls.Add(lblNome);

            lblOnde = new Label();
            lblOnde.Font = Mono;
            lblOnde.ForeColor = Corpo;
            lblOnde.Location = new Point(20, 30);
            lblOnde.AutoSize = true;
            p.Controls.Add(lblOnde);

            lblEstreia = new Label();
            lblEstreia.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
            lblEstreia.ForeColor = Ambar;
            lblEstreia.TextAlign = ContentAlignment.MiddleRight;
            lblEstreia.Size = new Size(620, 20);
            lblEstreia.Location = new Point(p.Width - 640, 16);
            lblEstreia.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            p.Controls.Add(lblEstreia);

            return p;
        }

        private Control MontarCorpo()
        {
            TableLayoutPanel tudo = new TableLayoutPanel();
            tudo.Dock = DockStyle.Fill;
            tudo.ColumnCount = 2;
            tudo.RowCount = 1;
            tudo.Padding = new Padding(14, 4, 14, 4);
            tudo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44F));
            tudo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 56F));

            // ------------------------------------------- coluna da esquerda
            Panel esq = new Panel();
            esq.Dock = DockStyle.Fill;
            esq.Margin = new Padding(0, 0, 8, 0);

            // NAO e caixa de texto: e um painel que DESENHA o codigo.
            // Depois de acertar as lacunas, a caixa da direita fica com o
            // metodo INTEIRO - e num RichTextBox isso daria um Ctrl+C direto
            // para dentro do mural-inicial. Aqui nao ha o que selecionar.
            caixaEsquerda = new CaixaDeCodigo();
            caixaEsquerda.Dock = DockStyle.Fill;
            esq.Controls.Add(caixaEsquerda);

            lblTituloEsq = new Label();
            lblTituloEsq.Dock = DockStyle.Top;
            lblTituloEsq.Height = 20;
            lblTituloEsq.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblTituloEsq.ForeColor = Purple;
            esq.Controls.Add(lblTituloEsq);

            // ------------------------------------------- coluna da direita
            colunaDireita = new TableLayoutPanel();
            colunaDireita.Dock = DockStyle.Fill;
            colunaDireita.ColumnCount = 1;
            colunaDireita.RowCount = 3;
            colunaDireita.Margin = new Padding(8, 0, 0, 0);
            colunaDireita.RowStyles.Add(new RowStyle(SizeType.Absolute, 62F));
            colunaDireita.RowStyles.Add(new RowStyle(SizeType.Percent, 52F));
            colunaDireita.RowStyles.Add(new RowStyle(SizeType.Percent, 48F));

            colunaDireita.Controls.Add(MontarTopoDaDireita(), 0, 0);

            caixaCodigo = new CaixaDeCodigo();
            caixaCodigo.Dock = DockStyle.Fill;
            colunaDireita.Controls.Add(caixaCodigo, 0, 1);

            pnlInterativo = new FlowLayoutPanel();
            pnlInterativo.Dock = DockStyle.Fill;
            pnlInterativo.FlowDirection = FlowDirection.TopDown;
            pnlInterativo.WrapContents = false;
            pnlInterativo.AutoScroll = true;
            pnlInterativo.BackColor = Color.White;
            pnlInterativo.Padding = new Padding(0, 6, 0, 0);
            colunaDireita.Controls.Add(pnlInterativo, 0, 2);

            tudo.Controls.Add(esq, 0, 0);
            tudo.Controls.Add(colunaDireita, 1, 0);
            return tudo;
        }

        private Control MontarTopoDaDireita()
        {
            pnlTopoDireita = new Panel();
            pnlTopoDireita.Dock = DockStyle.Fill;

            // ------------------------------------------------------ a ficha
            //
            // As tres caixas ficam num TableLayoutPanel de tres colunas
            // IGUAIS, e nao em posicoes fixas. Com posicao fixa, o DEVOLVE
            // saia cortado - "um texto (str" - assim que a janela ficava
            // menor que o previsto.
            pnlFicha = new Panel();
            pnlFicha.Dock = DockStyle.Fill;
            pnlFicha.BackColor = MintBg;

            Label cab = new Label();
            cab.Dock = DockStyle.Top;
            cab.Height = 16;
            cab.Text = "  A FICHA, JA PREENCHIDA  -  no papel quem preenche e voce";
            cab.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            cab.ForeColor = Mint;

            TableLayoutPanel caixas = new TableLayoutPanel();
            caixas.Dock = DockStyle.Fill;
            caixas.ColumnCount = 3;
            caixas.RowCount = 1;
            caixas.Padding = new Padding(6, 0, 6, 2);
            for (int i = 0; i < 3; i++)
            {
                caixas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            }

            lblRecebe = CampoDaFicha(caixas, "RECEBE", 0);
            lblFaz = CampoDaFicha(caixas, "FAZ", 1);
            lblDevolve = CampoDaFicha(caixas, "DEVOLVE", 2);

            pnlFicha.Controls.Add(caixas);
            pnlFicha.Controls.Add(cab);

            // o enunciado, para as perguntas
            lblEnunciado = new Label();
            lblEnunciado.Dock = DockStyle.Fill;
            lblEnunciado.Font = new Font("Segoe UI", 10F);
            lblEnunciado.ForeColor = Ink;
            lblEnunciado.Padding = new Padding(10, 6, 8, 4);
            lblEnunciado.BackColor = Tint;
            lblEnunciado.Visible = false;

            pnlTopoDireita.Controls.Add(lblEnunciado);
            pnlTopoDireita.Controls.Add(pnlFicha);
            return pnlTopoDireita;
        }

        private Label CampoDaFicha(TableLayoutPanel dono, string titulo, int coluna)
        {
            Panel celula = new Panel();
            celula.Dock = DockStyle.Fill;
            celula.Margin = new Padding(2, 0, 2, 0);

            Label v = new Label();
            v.Dock = DockStyle.Fill;
            v.Font = new Font("Segoe UI", 8.5F);
            v.ForeColor = Ink;
            v.AutoEllipsis = true;          // corta com "..." se faltar espaco

            Label t = new Label();
            t.Dock = DockStyle.Top;
            t.Height = 13;
            t.Text = titulo;
            t.Font = new Font("Segoe UI", 7F, FontStyle.Bold);
            t.ForeColor = Mint;

            celula.Controls.Add(v);
            celula.Controls.Add(t);
            dono.Controls.Add(celula, coluna, 0);

            return v;
        }

        private Control MontarRodape()
        {
            Panel p = new Panel();
            p.Dock = DockStyle.Bottom;
            p.Height = 96;
            p.BackColor = Tint;

            btnConferir = new Button();
            btnConferir.Text = "Conferir";
            btnConferir.Size = new Size(140, 34);
            btnConferir.Location = new Point(16, 12);
            btnConferir.BackColor = Purple;
            btnConferir.ForeColor = Color.White;
            btnConferir.FlatStyle = FlatStyle.Flat;
            btnConferir.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnConferir.Click += Conferir;
            p.Controls.Add(btnConferir);

            lblResultado = new Label();
            lblResultado.Location = new Point(168, 8);
            lblResultado.Size = new Size(700, 46);
            lblResultado.Font = new Font("Segoe UI", 9F);
            lblResultado.ForeColor = Corpo;
            p.Controls.Add(lblResultado);

            Label nota = new Label();
            nota.Text = "O codigo das duas colunas nao pode ser copiado, de proposito. "
                      + "Este app confere lacuna por lacuna e NAO compila nada - "
                      + "Para ESCREVER codigo de verdade, o projeto e o desafios-inicial, "
                      + "que roda com F5 e executa o que voce escreveu.";
            nota.Location = new Point(18, 62);
            nota.Size = new Size(1010, 18);
            nota.Font = new Font("Segoe UI", 8F, FontStyle.Italic);
            nota.ForeColor = Ambar;
            p.Controls.Add(nota);

            btnProximo = new Button();
            btnProximo.Text = "Proximo  >";
            btnProximo.Size = new Size(120, 30);
            btnProximo.Location = new Point(p.Width - 150, 14);
            btnProximo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnProximo.Click += (s, e) => MostrarItem(itemAtual[(int)rodada] + 1);
            p.Controls.Add(btnProximo);

            btnAnterior = new Button();
            btnAnterior.Text = "<  Anterior";
            btnAnterior.Size = new Size(120, 30);
            btnAnterior.Location = new Point(p.Width - 278, 14);
            btnAnterior.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAnterior.Click += (s, e) => MostrarItem(itemAtual[(int)rodada] - 1);
            p.Controls.Add(btnAnterior);

            return p;
        }

        // =================================================================
        // TROCAR DE RODADA
        // =================================================================
        private void TrocarRodada(Rodada nova)
        {
            rodada = nova;

            for (int i = 0; i < botoesRodada.Count; i++)
            {
                bool ligado = (int)rodada == i;
                botoesRodada[i].BackColor = ligado ? Color.White : ColorTranslator.FromHtml("#3D2A5C");
                botoesRodada[i].ForeColor = ligado ? Purple : ColorTranslator.FromHtml("#CBBFE0");
                botoesRodada[i].FlatAppearance.BorderColor =
                    ligado ? Color.White : ColorTranslator.FromHtml("#6B5A86");
            }

            MontarBotoesDaTrilha();
            MostrarItem(itemAtual[(int)rodada]);
        }

        // =================================================================
        // MOSTRAR UM ITEM
        // =================================================================
        private void MostrarItem(int indice)
        {
            if (indice < 0 || indice >= Quantos(rodada)) return;

            itemAtual[(int)rodada] = indice;

            pnlInterativo.Controls.Clear();
            combos.Clear();
            radios.Clear();
            selos.Clear();

            if (rodada == Rodada.Passos) MostrarPasso(indice);
            else MostrarPergunta(indice);

            btnAnterior.Enabled = indice > 0;
            btnProximo.Enabled = indice < Quantos(rodada) - 1;

            AtualizarTrilha();
        }

        private void MostrarPasso(int indice)
        {
            Passo passo = conteudo.Passos[indice];

            passoAtual = passo;
            escolhas.Clear();
            situacao.Clear();

            lblPasso.Text = "PASSO " + passo.N + " de " + conteudo.Passos.Count;
            lblNome.Text = passo.N + ".  " + passo.Nome;
            lblOnde.Text = passo.Onde;
            lblEstreia.Text = "estreia: " + passo.Estreia;

            lblTituloEsq.Text = passo.Esquerda.Titulo;
            caixaEsquerda.Definir(passo.Esquerda.Linhas, Ink, Comentario);

            MostrarFicha(passo.Ficha);
            AjustarLinhas(passo.Ficha == null ? 0F : 62F, 52F, true, 48F);

            caixaCodigo.Visible = true;
            PintarCodigoDoPasso();

            foreach (Lacuna lacuna in passo.Lacunas)
            {
                Panel linha = new Panel();
                linha.Size = new Size(600, 30);
                linha.Margin = new Padding(0, 0, 0, 4);

                Label numero = new Label();
                numero.Text = "[" + lacuna.N + "]";
                numero.Font = MonoNegrito;
                numero.ForeColor = Purple;
                numero.Location = new Point(2, 6);
                numero.Size = new Size(34, 20);
                linha.Controls.Add(numero);

                ComboBox c = new ComboBox();
                c.DropDownStyle = ComboBoxStyle.DropDownList;
                c.Font = Mono;
                c.Size = new Size(250, 24);
                c.Location = new Point(40, 2);
                c.Items.Add("- escolha -");

                foreach (string opcao in lacuna.OpcoesNaTela(passo.N))
                {
                    c.Items.Add(opcao);
                }

                c.SelectedIndex = 0;
                c.Tag = lacuna;
                c.SelectedIndexChanged += EscolheuNaLacuna;
                linha.Controls.Add(c);
                combos.Add(c);

                Label selo = new Label();
                selo.Location = new Point(300, 6);
                selo.Size = new Size(290, 20);
                selo.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
                linha.Controls.Add(selo);
                selos.Add(selo);

                pnlInterativo.Controls.Add(linha);
            }

            DizerEstado(indice, "Escolha a peca de cada lacuna e clique em Conferir.");
        }

        private void MostrarPergunta(int indice)
        {
            Pergunta p = conteudo.Perguntas[indice];

            lblPasso.Text = "PERGUNTA " + p.N + " de " + conteudo.Perguntas.Count;
            lblNome.Text = p.N + ".  " + p.Titulo;
            lblOnde.Text = "";
            lblEstreia.Text = "responda OLHANDO o algoritmo, nunca de cabeca";

            lblTituloEsq.Text = "PORTUGOL (VisuAlg)";
            caixaEsquerda.Definir(p.Portugol, Ink, Comentario);

            MostrarEnunciado(p.Enunciado);
            AjustarLinhas(84F, 0F, false, 100F);
            caixaCodigo.Visible = false;

            foreach (string opcao in p.Opcoes)
            {
                RadioButton r = new RadioButton();
                r.Text = opcao;
                r.Font = new Font("Segoe UI", 9.5F);
                r.AutoSize = false;
                r.Size = new Size(620, 26);
                r.Margin = new Padding(4, 0, 0, 6);
                pnlInterativo.Controls.Add(r);
                radios.Add(r);

                Label selo = new Label();
                selo.Size = new Size(0, 0);
                selos.Add(selo);
            }

            DizerEstado(indice, "Escolha uma das quatro e clique em Conferir.");
        }

        private void DizerEstado(int indice, string aviso)
        {
            bool ok = resolvido[(int)rodada][indice];
            lblResultado.ForeColor = ok ? Mint : Corpo;
            lblResultado.Text = ok ? "Resolvido." : aviso;
        }

        private void MostrarFicha(Ficha ficha)
        {
            lblEnunciado.Visible = false;
            pnlFicha.Visible = ficha != null;

            if (ficha != null)
            {
                lblRecebe.Text = ficha.Recebe;
                lblFaz.Text = ficha.Faz;
                lblDevolve.Text = ficha.Devolve;
            }
        }

        private void MostrarEnunciado(string texto)
        {
            pnlFicha.Visible = false;
            lblEnunciado.Visible = true;
            lblEnunciado.Text = texto;
            lblEnunciado.BringToFront();
        }

        /// <summary>
        /// Reparte a altura da coluna da direita entre as tres faixas:
        /// o topo (ficha ou enunciado), a caixa de codigo e a area onde o
        /// aluno responde.
        ///
        /// Nos PASSOS o codigo divide a altura em porcentagem com a area de
        /// resposta, porque os dois sao grandes. Nas PERGUNTAS e nos
        /// DESAFIOS a caixa de codigo tem altura fixa - ou nem aparece.
        /// </summary>
        private void AjustarLinhas(float topo, float codigo, bool codigoEmPorcento,
                                   float interativo)
        {
            colunaDireita.RowStyles[0] = new RowStyle(SizeType.Absolute, topo);

            colunaDireita.RowStyles[1] = codigoEmPorcento
                ? new RowStyle(SizeType.Percent, codigo)
                : new RowStyle(SizeType.Absolute, codigo);

            colunaDireita.RowStyles[2] = new RowStyle(SizeType.Percent, interativo);
        }

        /// <summary>
        /// Monta o C# do passo PREENCHIDO com o que o aluno ja escolheu, e
        /// entrega para a caixa desenhar.
        ///
        /// Uma lacuna sem escolha vira [1], em roxo. Assim que ele escolhe,
        /// o [1] some e a peca entra no lugar - o codigo vai virando C# de
        /// verdade na frente dele. Depois do Conferir, cada peca fica verde
        /// ou vermelha.
        /// </summary>
        private void PintarCodigoDoPasso()
        {
            if (passoAtual == null) return;

            List<List<CaixaDeCodigo.Pedaco>> desenho =
                new List<List<CaixaDeCodigo.Pedaco>>();

            foreach (string linha in passoAtual.CSharp)
            {
                List<CaixaDeCodigo.Pedaco> pedacos = new List<CaixaDeCodigo.Pedaco>();
                Color corDaLinha = CorDaLinha(linha);

                foreach (Trecho t in Codigo.Fatiar(linha))
                {
                    if (t.Lacuna == 0)
                    {
                        pedacos.Add(new CaixaDeCodigo.Pedaco(t.Texto, corDaLinha, false));
                    }
                    else
                    {
                        pedacos.Add(PedacoDaLacuna(t.Lacuna));
                    }
                }

                desenho.Add(pedacos);
            }

            caixaCodigo.Definir(desenho);
        }

        private CaixaDeCodigo.Pedaco PedacoDaLacuna(int numero)
        {
            string escolhido;
            if (!escolhas.TryGetValue(numero, out escolhido)) escolhido = "";

            if (escolhido.Length == 0)
            {
                return new CaixaDeCodigo.Pedaco("[" + numero + "]", Purple, true);
            }

            int como;
            if (!situacao.TryGetValue(numero, out como)) como = 0;

            Color cor = como == 1 ? Mint : (como == 2 ? Vermelho : Purple);
            return new CaixaDeCodigo.Pedaco(escolhido, cor, true);
        }

        private Color CorDaLinha(string linha)
        {
            return linha.TrimStart().StartsWith("//") ? Comentario : Ink;
        }

        private void EscolheuNaLacuna(object sender, EventArgs e)
        {
            ComboBox c = (ComboBox)sender;
            Lacuna lacuna = (Lacuna)c.Tag;

            escolhas[lacuna.N] = c.SelectedIndex <= 0 ? "" : c.SelectedItem.ToString();
            situacao[lacuna.N] = 0;      // mexeu, entao a marcacao antiga nao vale

            c.BackColor = Color.White;
            PintarCodigoDoPasso();
        }

        // =================================================================
        // CONFERIR
        // =================================================================
        private void Conferir(object sender, EventArgs e)
        {
            if (rodada == Rodada.Passos) ConferirPasso();
            else ConferirPergunta();

            AtualizarTrilha();
        }

        private void ConferirPasso()
        {
            int indice = itemAtual[(int)rodada];
            Passo passo = conteudo.Passos[indice];

            int certas = 0;
            string primeiraDica = null;

            for (int i = 0; i < combos.Count; i++)
            {
                Lacuna lacuna = (Lacuna)combos[i].Tag;
                string escolhido = combos[i].SelectedIndex <= 0
                    ? "" : combos[i].SelectedItem.ToString();

                if (escolhido == lacuna.Resposta)
                {
                    certas++;
                    selos[i].Text = "certo";
                    selos[i].ForeColor = Mint;
                    combos[i].BackColor = MintBg;
                    situacao[lacuna.N] = 1;
                }
                else
                {
                    selos[i].Text = escolhido == "" ? "faltou escolher" : "nao e essa";
                    selos[i].ForeColor = Vermelho;
                    combos[i].BackColor = VermelhoBg;
                    situacao[lacuna.N] = 2;

                    if (primeiraDica == null)
                    {
                        primeiraDica = "[" + lacuna.N + "]  " + lacuna.Dica;
                    }
                }
            }

            // o codigo repinta com verde e vermelho no lugar das pecas
            PintarCodigoDoPasso();

            if (certas == combos.Count)
            {
                resolvido[(int)rodada][indice] = true;
                lblResultado.ForeColor = Mint;
                lblResultado.Text = "PASSO " + passo.N + " RESOLVIDO."
                    + (passo.NaFolha
                        ? "  Agora escreva esse metodo no Visual Studio - o TODO "
                          + passo.N + " esta em " + passo.Onde + "."
                        : "  Este e o passo do banco: ele esta na folha C, lado a lado.");
            }
            else
            {
                lblResultado.ForeColor = Vermelho;
                lblResultado.Text = certas + " de " + combos.Count + " certas."
                    + Environment.NewLine + primeiraDica;
            }
        }

        private void ConferirPergunta()
        {
            int indice = itemAtual[(int)rodada];
            Pergunta p = conteudo.Perguntas[indice];

            string escolhido = null;
            foreach (RadioButton r in radios)
            {
                if (r.Checked) escolhido = r.Text;
            }

            if (escolhido == null)
            {
                lblResultado.ForeColor = Vermelho;
                lblResultado.Text = "Escolha uma das quatro antes de conferir.";
                return;
            }

            foreach (RadioButton r in radios)
            {
                bool certa = r.Text == p.Resposta;
                if (r.Checked && certa) r.ForeColor = Mint;
                else if (r.Checked) r.ForeColor = Vermelho;
                else r.ForeColor = Corpo;
            }

            if (escolhido == p.Resposta)
            {
                resolvido[(int)rodada][indice] = true;
                lblResultado.ForeColor = Mint;
                lblResultado.Text = "CERTO.  " + p.Dica;
            }
            else
            {
                lblResultado.ForeColor = Vermelho;
                lblResultado.Text = "Ainda nao." + Environment.NewLine + p.Dica;
            }
        }

        private void AtualizarTrilha()
        {
            int indice = itemAtual[(int)rodada];

            for (int i = 0; i < botoesTrilha.Count; i++)
            {
                Button b = botoesTrilha[i];

                if (resolvido[(int)rodada][i])
                {
                    b.BackColor = Mint;
                    b.ForeColor = Color.White;
                    b.FlatAppearance.BorderColor = Mint;
                }
                else
                {
                    b.BackColor = Color.White;
                    b.ForeColor = Corpo;
                    b.FlatAppearance.BorderColor = Linha;
                }

                b.FlatAppearance.BorderSize = (i == indice) ? 2 : 1;
                if (i == indice) b.FlatAppearance.BorderColor = Purple;
            }
        }
    }
}
