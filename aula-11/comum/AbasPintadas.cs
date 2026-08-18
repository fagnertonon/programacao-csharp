using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Revisao
{
    public enum EstadoAba { Resolvida, Liberada, Travada }

    // As abas sao pintadas a mao em vez de carregarem "[travada]" no texto.
    // Com o prefixo, as oito nao cabiam na largura da janela e o TabControl
    // punha setinhas de rolagem - o aluno nao via o caminho inteiro. Pintando,
    // o texto fica curto e a cor diz o estado.
    //
    // Voce NAO precisa mexer aqui.
    public static class AbasPintadas
    {
        public static void Ligar(TabControl abas, Func<int, EstadoAba> estadoDe)
        {
            abas.DrawMode = TabDrawMode.OwnerDrawFixed;
            abas.SizeMode = TabSizeMode.Normal;
            abas.ItemSize = new Size(0, 30);

            abas.DrawItem += delegate (object s, DrawItemEventArgs e)
            {
                TabPage pagina = abas.TabPages[e.Index];
                EstadoAba estado = estadoDe(e.Index);
                bool atual = (e.Index == abas.SelectedIndex);

                Color fundo, texto;

                if (estado == EstadoAba.Resolvida)
                {
                    fundo = atual ? Paleta.Mint : Paleta.MintBg;
                    texto = atual ? Color.White : Paleta.Mint;
                }
                else if (estado == EstadoAba.Travada)
                {
                    fundo = Paleta.Tint;
                    texto = Color.FromArgb(150, 145, 155);   // cinza: nao da para entrar
                }
                else
                {
                    fundo = atual ? Paleta.Purple : Color.White;
                    texto = atual ? Color.White : Paleta.Ink;
                }

                using (SolidBrush b = new SolidBrush(fundo))
                {
                    e.Graphics.FillRectangle(b, e.Bounds);
                }

                using (Pen p = new Pen(Paleta.Line))
                {
                    e.Graphics.DrawRectangle(p, e.Bounds.X, e.Bounds.Y,
                                             e.Bounds.Width - 1, e.Bounds.Height - 1);
                }

                TextFormatFlags fmt = TextFormatFlags.HorizontalCenter
                                    | TextFormatFlags.VerticalCenter
                                    | TextFormatFlags.EndEllipsis
                                    | TextFormatFlags.NoPrefix;   // & nao vira atalho

                Font fonte = atual ? new Font(abas.Font, FontStyle.Bold) : abas.Font;

                TextRenderer.DrawText(e.Graphics, pagina.Text, fonte, e.Bounds, texto, fmt);

                if (atual) { fonte.Dispose(); }
            };
        }

        // O cadeado no texto da aba, curto o bastante para as oito caberem.
        public static string Marcar(EstadoAba estado, int numero, string titulo)
        {
            string marca = estado == EstadoAba.Resolvida ? "OK  " : "";
            return "  " + marca + numero + ". " + titulo + "  ";
        }
    }

    public static class Pintura
    {
        // Painel, FlowLayoutPanel e ate os paineis internos do SplitContainer
        // tem DoubleBuffered protegido. Sem isso a troca de aba repinta em
        // pedacos e pisca - visivel nas maquinas mais fracas do laboratorio.
        private static readonly PropertyInfo PROP =
            typeof(Control).GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);

        public static void Suavizar(Control raiz)
        {
            if (PROP != null && (raiz is Panel || raiz is TabPage || raiz is TabControl))
            {
                try { PROP.SetValue(raiz, true, null); }
                catch (Exception) { /* algum controle recusou: segue sem suavizar */ }
            }

            foreach (Control filho in raiz.Controls)
            {
                Suavizar(filho);
            }
        }
    }
}
