using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Conecta
{
    // =====================================================================
    // A CAIXA DE CODIGO. JA ESTA PRONTA - voce nao mexe aqui.
    //
    // POR QUE ELA EXISTE, e nao uma caixa de texto comum:
    //
    //   Numa caixa de texto o codigo pode ser selecionado e copiado com
    //   Ctrl+C. Aqui isso seria pior que no Portugol: depois de acertar as
    //   lacunas, a caixa da direita fica com o METODO INTEIRO, pronto para
    //   colar no mural-inicial. O aluno acertaria as pecas e nunca teria
    //   escrito o metodo.
    //
    //   Esta caixa nao guarda texto: ela DESENHA o codigo. Nao ha o que
    //   selecionar, nao ha o que copiar, nao ha menu de contexto. O
    //   caminho do algoritmo para o seu programa passa pelos seus dedos.
    //
    // Ela continua rolando quando o codigo e mais alto que a janela, e
    // continua pintando comentario em cinza.
    //
    // GEMEA: este arquivo tem uma copia em
    // projeto-desafios-codigo/comum/CaixaDeCodigo.cs. Sao dois projetos
    // separados, que vao para a maquina do aluno em pastas diferentes -
    // por isso a copia. Mexeu num, mexa no outro.
    // =====================================================================
    public class CaixaDeCodigo : Panel
    {
        /// <summary>Um pedaco de linha com a sua cor - o que da para pintar
        /// uma peca de verde no meio do codigo.</summary>
        public class Pedaco
        {
            public string Texto;
            public Color Cor;
            public bool Negrito;

            public Pedaco(string texto, Color cor, bool negrito)
            {
                Texto = texto;
                Cor = cor;
                Negrito = negrito;
            }
        }

        private List<List<Pedaco>> linhas = new List<List<Pedaco>>();

        private readonly Font fonte = new Font("Consolas", 9.75F);
        private readonly Font fonteNegrito = new Font("Consolas", 9.75F, FontStyle.Bold);

        private const int MargemX = 8;
        private const int MargemY = 6;

        private static readonly TextFormatFlags SemFolga =
            TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix;

        public CaixaDeCodigo()
        {
            DoubleBuffered = true;
            BorderStyle = BorderStyle.FixedSingle;
            BackColor = ColorTranslator.FromHtml("#FAF8FC");
            AutoScroll = true;
            TabStop = false;
        }

        public int AlturaDaLinha
        {
            get { return fonte.Height + 2; }
        }

        /// <summary>Codigo simples, de uma cor so.</summary>
        public void Definir(List<string> texto, Color cor, Color corDeComentario)
        {
            List<List<Pedaco>> novas = new List<List<Pedaco>>();

            foreach (string linha in texto)
            {
                List<Pedaco> pedacos = new List<Pedaco>();
                bool comentario = linha.TrimStart().StartsWith("//");
                pedacos.Add(new Pedaco(linha, comentario ? corDeComentario : cor, false));
                novas.Add(pedacos);
            }

            Definir(novas);
        }

        /// <summary>Codigo com pedacos de cores diferentes na mesma linha.</summary>
        public void Definir(List<List<Pedaco>> novas)
        {
            linhas = novas ?? new List<List<Pedaco>>();
            AjustarRolagem();
            Invalidate();
        }

        private void AjustarRolagem()
        {
            int largura = 0;

            using (Graphics g = CreateGraphics())
            {
                foreach (List<Pedaco> linha in linhas)
                {
                    int x = 0;
                    foreach (Pedaco p in linha)
                    {
                        x += Medir(g, p);
                    }
                    if (x > largura) largura = x;
                }
            }

            AutoScrollMinSize = new Size(largura + MargemX * 2,
                                         linhas.Count * AlturaDaLinha + MargemY * 2);
        }

        private int Medir(Graphics g, Pedaco p)
        {
            return TextRenderer.MeasureText(g, p.Texto,
                p.Negrito ? fonteNegrito : fonte, Size.Empty, SemFolga).Width;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            int y = MargemY + AutoScrollPosition.Y;

            foreach (List<Pedaco> linha in linhas)
            {
                if (y + AlturaDaLinha >= 0 && y <= Height)
                {
                    int x = MargemX + AutoScrollPosition.X;

                    foreach (Pedaco p in linha)
                    {
                        if (p.Texto.Length > 0)
                        {
                            Font f = p.Negrito ? fonteNegrito : fonte;
                            TextRenderer.DrawText(e.Graphics, p.Texto, f,
                                new Point(x, y), p.Cor, SemFolga);
                            x += Medir(e.Graphics, p);
                        }
                    }
                }

                y += AlturaDaLinha;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }
    }
}
