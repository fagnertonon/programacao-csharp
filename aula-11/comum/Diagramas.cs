using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Revisao
{
    // A paleta roxa do Senac, a mesma das apostilas.
    public static class Paleta
    {
        public static readonly Color Dark = ColorTranslator.FromHtml("#2D1B45");
        public static readonly Color Purple = ColorTranslator.FromHtml("#5C2D91");
        public static readonly Color PurpleLt = ColorTranslator.FromHtml("#8B5FBF");
        public static readonly Color Ink = ColorTranslator.FromHtml("#1F1B24");
        public static readonly Color Body = ColorTranslator.FromHtml("#3F3A45");
        public static readonly Color Tint = ColorTranslator.FromHtml("#F4F1F9");
        public static readonly Color Line = ColorTranslator.FromHtml("#DCD3EA");
        public static readonly Color Mint = ColorTranslator.FromHtml("#0E7A55");
        public static readonly Color MintBg = ColorTranslator.FromHtml("#E6F4EE");
        public static readonly Color Amber = ColorTranslator.FromHtml("#9C6300");
        public static readonly Color AmberBg = ColorTranslator.FromHtml("#FDF3E2");
        public static readonly Color Red = ColorTranslator.FromHtml("#B02A3C");
        public static readonly Color RedBg = ColorTranslator.FromHtml("#FBEBEE");
        public static readonly Color CodeBg = ColorTranslator.FromHtml("#FAF8FC");
    }

    // Desenha o diagrama de cada topico. Sem imagem externa: tudo em codigo,
    // para o projeto continuar rodando em maquina sem internet e sem assets.
    public class Diagrama : Panel
    {
        private string tipo = "";

        public Diagrama()
        {
            DoubleBuffered = true;
            BackColor = Color.White;
            Height = 175;
        }

        // Cada diagrama pede a sua altura. O for-vira-while precisa de mais
        // espaco porque as anotacoes ficam embaixo das duas caixas - antes elas
        // ficavam a direita e eram cortadas pela largura do painel.
        public static int AlturaDe(string tipo)
        {
            if (tipo == "for-vira-while") { return 250; }
            return 175;
        }

        public void Definir(string novoTipo)
        {
            tipo = novoTipo ?? "";
            Height = AlturaDe(tipo);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            switch (tipo)
            {
                case "molde-objeto": MoldeObjeto(g); break;
                case "assinatura-metodo": AssinaturaMetodo(g); break;
                case "tabela-verdade": TabelaVerdade(g); break;
                case "cadeia-if": CadeiaIf(g); break;
                case "switch-caminhos": SwitchCaminhos(g); break;
                case "for-tres-partes": ForTresPartes(g); break;
                case "for-vira-while": ForViraWhile(g); break;
                case "lista-foreach": ListaForeach(g); break;
            }
        }

        // ---------- pecas reutilizaveis ----------

        private static readonly Font FonteTitulo = new Font("Segoe UI", 9F, FontStyle.Bold);
        private static readonly Font FonteTexto = new Font("Segoe UI", 8.5F);
        private static readonly Font FonteMono = new Font("Consolas", 9F);
        private static readonly Font FonteMonoB = new Font("Consolas", 9F, FontStyle.Bold);

        private void Caixa(Graphics g, int x, int y, int w, int h, string titulo,
                           string[] linhas, Color borda, Color fundo)
        {
            using (SolidBrush b = new SolidBrush(fundo))
            using (Pen p = new Pen(borda, 1.6f))
            {
                g.FillRectangle(b, x, y, w, h);
                g.DrawRectangle(p, x, y, w, h);
            }

            using (SolidBrush t = new SolidBrush(borda))
            {
                g.DrawString(titulo, FonteTitulo, t, x + 8, y + 6);
            }

            if (linhas != null)
            {
                using (SolidBrush t = new SolidBrush(Paleta.Body))
                {
                    for (int i = 0; i < linhas.Length; i++)
                    {
                        g.DrawString(linhas[i], FonteMono, t, x + 8, y + 26 + i * 16);
                    }
                }
            }
        }

        private void Seta(Graphics g, int x1, int y1, int x2, int y2, string rotulo)
        {
            using (Pen p = new Pen(Paleta.PurpleLt, 2f))
            {
                p.EndCap = LineCap.ArrowAnchor;
                g.DrawLine(p, x1, y1, x2, y2);
            }

            if (!string.IsNullOrEmpty(rotulo))
            {
                using (SolidBrush t = new SolidBrush(Paleta.Purple))
                {
                    g.DrawString(rotulo, FonteTexto, t, (x1 + x2) / 2 - 18, Math.Min(y1, y2) - 18);
                }
            }
        }

        private void Nota(Graphics g, int x, int y, string texto, Color cor)
        {
            using (SolidBrush t = new SolidBrush(cor))
            {
                g.DrawString(texto, FonteTexto, t, x, y);
            }
        }

        // ---------- os oito diagramas ----------

        private void MoldeObjeto(Graphics g)
        {
            Caixa(g, 12, 20, 200, 110, "class Lutador  (o molde)",
                  new string[] { "string Nome", "int Vida = 20", "int Forca = 5" },
                  Paleta.Purple, Paleta.Tint);

            Seta(g, 220, 60, 280, 60, "new");
            Seta(g, 220, 100, 280, 100, "new");

            Caixa(g, 285, 12, 165, 60, "objeto a", new string[] { "Nome = \"Ana\"" },
                  Paleta.Mint, Paleta.MintBg);

            Caixa(g, 285, 82, 165, 60, "objeto b", new string[] { "Nome = \"Bruno\"" },
                  Paleta.Mint, Paleta.MintBg);

            Nota(g, 470, 55, "Um molde, dois objetos.", Paleta.Body);
            Nota(g, 470, 75, "Cada um com a sua propria Vida.", Paleta.Body);
        }

        private void AssinaturaMetodo(Graphics g)
        {
            using (SolidBrush t = new SolidBrush(Paleta.Ink))
            {
                g.DrawString("public", FonteMono, t, 20, 60);
                g.DrawString("void", FonteMonoB, t, 80, 60);
                g.DrawString("Descansar", FonteMonoB, t, 130, 60);
                g.DrawString("(", FonteMono, t, 218, 60);
                g.DrawString("int pontos", FonteMonoB, t, 228, 60);
                g.DrawString(")", FonteMono, t, 315, 60);
            }

            Seta(g, 95, 100, 95, 78, "");
            Nota(g, 40, 102, "tipo de retorno", Paleta.Purple);

            Seta(g, 175, 40, 175, 58, "");
            Nota(g, 148, 20, "nome", Paleta.Purple);

            Seta(g, 270, 100, 270, 78, "");
            Nota(g, 228, 102, "PARAMETRO", Paleta.Purple);

            Nota(g, 400, 40, "Na chamada:", Paleta.Body);
            using (SolidBrush t = new SolidBrush(Paleta.Ink))
            {
                g.DrawString("a.Descansar(5);", FonteMonoB, t, 400, 60);
            }
            Seta(g, 490, 100, 490, 80, "");
            Nato(g, 455, 102, "ARGUMENTO");
        }

        // pequeno atalho para nao repetir a cor em todo rotulo roxo
        private void Nato(Graphics g, int x, int y, string texto)
        {
            Nota(g, x, y, texto, Paleta.Purple);
        }

        private void TabelaVerdade(Graphics g)
        {
            string[] colunas = { "A", "B", "A && B", "A || B" };
            string[,] linhas = {
                { "V", "V", "V", "V" },
                { "V", "F", "F", "V" },
                { "F", "V", "F", "V" },
                { "F", "F", "F", "F" }
            };

            int x0 = 20, y0 = 18, lc = 70, alt = 26;

            using (SolidBrush cab = new SolidBrush(Paleta.Purple))
            using (SolidBrush br = new SolidBrush(Color.White))
            {
                g.FillRectangle(cab, x0, y0, lc * 4, alt);
                for (int c = 0; c < 4; c++)
                {
                    g.DrawString(colunas[c], FonteMonoB, br, x0 + c * lc + 10, y0 + 5);
                }
            }

            for (int r = 0; r < 4; r++)
            {
                int y = y0 + alt + r * alt;
                using (SolidBrush f = new SolidBrush(r % 2 == 0 ? Color.White : Paleta.Tint))
                {
                    g.FillRectangle(f, x0, y, lc * 4, alt);
                }

                for (int c = 0; c < 4; c++)
                {
                    bool verdadeiro = linhas[r, c] == "V";
                    Color cor = verdadeiro ? Paleta.Mint : Paleta.Red;
                    using (SolidBrush t = new SolidBrush(c >= 2 ? cor : Paleta.Body))
                    {
                        g.DrawString(linhas[r, c], FonteMonoB, t, x0 + c * lc + 10, y + 5);
                    }
                }
            }

            using (Pen p = new Pen(Paleta.Line, 1f))
            {
                g.DrawRectangle(p, x0, y0, lc * 4, alt * 5);
            }

            Nota(g, 320, 40, "&&  = OS DOIS", Paleta.Mint);
            Nota(g, 320, 62, "||  = PELO MENOS UM", Paleta.Purple);
            Nota(g, 320, 96, "Voce escreveu isto no Duelo:", Paleta.Body);
            using (SolidBrush t = new SolidBrush(Paleta.Ink))
            {
                g.DrawString("if (a.Vida == 0 || b.Vida == 0)", FonteMonoB, t, 320, 116);
            }
        }

        private void CadeiaIf(Graphics g)
        {
            string[] testes = { "nota >= 9", "nota >= 7", "nota >= 5", "else" };
            string[] result = { "\"A\"", "\"B\"", "\"C\"", "\"D\"" };

            for (int i = 0; i < 4; i++)
            {
                int y = 14 + i * 36;
                Caixa(g, 20, y, 150, 28, "", null, Paleta.Purple, Paleta.Tint);
                using (SolidBrush t = new SolidBrush(Paleta.Ink))
                {
                    g.DrawString(testes[i], FonteMono, t, 30, y + 6);
                }

                Seta(g, 175, y + 14, 215, y + 14, "V");

                Caixa(g, 220, y, 70, 28, "", null, Paleta.Mint, Paleta.MintBg);
                using (SolidBrush t = new SolidBrush(Paleta.Mint))
                {
                    g.DrawString(result[i], FonteMonoB, t, 232, y + 6);
                }

                if (i < 3)
                {
                    Seta(g, 95, y + 30, 95, y + 34, "");
                }
            }

            Nota(g, 320, 40, "Testa de cima para baixo", Paleta.Body);
            Nota(g, 320, 62, "e PARA no primeiro verdadeiro.", Paleta.Body);
            Nota(g, 320, 92, "No maximo UM bloco roda.", Paleta.Purple);
            Nota(g, 320, 118, "Inverta a ordem e todo mundo tira C.", Paleta.Red);
        }

        private void SwitchCaminhos(Graphics g)
        {
            Caixa(g, 20, 60, 110, 30, "", null, Paleta.Purple, Paleta.Tint);
            using (SolidBrush t = new SolidBrush(Paleta.Ink))
            {
                g.DrawString("switch (opcao)", FonteMono, t, 26, 67);
            }

            string[] cases = { "case \"1\"", "case \"2\"", "case \"3\"", "default" };
            for (int i = 0; i < 4; i++)
            {
                int y = 14 + i * 34;
                Seta(g, 135, 75, 195, y + 13, "");
                bool ehDefault = (i == 3);
                Caixa(g, 200, y, 120, 26, "", null,
                      ehDefault ? Paleta.Amber : Paleta.Mint,
                      ehDefault ? Paleta.AmberBg : Paleta.MintBg);
                using (SolidBrush t = new SolidBrush(ehDefault ? Paleta.Amber : Paleta.Mint))
                {
                    g.DrawString(cases[i], FonteMono, t, 208, y + 5);
                }
                using (SolidBrush t = new SolidBrush(Paleta.Red))
                {
                    g.DrawString("break;", FonteMonoB, t, 330, y + 5);
                }
            }

            Nota(g, 410, 30, "Todo case termina em break.", Paleta.Red);
            Nota(g, 410, 52, "Esqueceu? CS0163, nao compila.", Paleta.Red);
            Nota(g, 410, 86, "default = a rede de seguranca.", Paleta.Amber);
            Nota(g, 410, 116, "So compara IGUALDADE.", Paleta.Purple);
        }

        private void ForTresPartes(Graphics g)
        {
            using (SolidBrush t = new SolidBrush(Paleta.Ink))
            {
                g.DrawString("for (", FonteMono, t, 20, 70);
                g.DrawString("int i = 0", FonteMonoB, t, 62, 70);
                g.DrawString(";", FonteMono, t, 148, 70);
                g.DrawString("i < Vida", FonteMonoB, t, 160, 70);
                g.DrawString(";", FonteMono, t, 238, 70);
                g.DrawString("i++", FonteMonoB, t, 250, 70);
                g.DrawString(")", FonteMono, t, 286, 70);
            }

            Seta(g, 100, 108, 100, 88, "");
            Nota(g, 40, 110, "1  uma vez so", Paleta.Purple);

            Seta(g, 196, 46, 196, 66, "");
            Nota(g, 130, 24, "2  antes de CADA volta", Paleta.Purple);

            Seta(g, 264, 108, 264, 88, "");
            Nota(g, 236, 110, "3  no fim de cada volta", Paleta.Purple);

            Nota(g, 400, 44, "Vida = 5  ->  #####", Paleta.Mint);
            Nota(g, 400, 70, "Vida = 0  ->  (nada)", Paleta.Amber);
            Nota(g, 400, 96, "Condicao ja falsa no comeco:", Paleta.Body);
            Nota(g, 400, 116, "ZERO voltas. E esta certo.", Paleta.Red);
        }

        private void ForViraWhile(Graphics g)
        {
            // Conta de 5 a 1, igual ao exemplo do topico no conteudo-revisao.json.
            Caixa(g, 16, 10, 280, 128, "for", new string[] {
                "for (int i = 5; i >= 1; i--)", "{", "    Console.WriteLine(i);", "}"
            }, Paleta.Purple, Paleta.Tint);

            Seta(g, 302, 74, 336, 74, "=");

            Caixa(g, 342, 10, 288, 128, "while", new string[] {
                "int i = 5;", "while (i >= 1)", "{", "    Console.WriteLine(i);", "    i--;", "}"
            }, Paleta.Mint, Paleta.MintBg);

            // As tres pecas, embaixo: aqui ha largura de sobra e nada corta.
            Nota(g, 16, 152, "1  inicializacao ANTES do laco", Paleta.Purple);
            Nota(g, 16, 176, "2  condicao entre parenteses", Paleta.Purple);
            Nota(g, 16, 200, "3  o passo DENTRO do corpo", Paleta.Purple);

            Nota(g, 342, 152, "As mesmas tres pecas do for,", Paleta.Body);
            Nota(g, 342, 176, "em lugares diferentes.", Paleta.Body);
            Nota(g, 342, 200, "Esqueceu o i++? Laco infinito.", Paleta.Red);
        }

        private void ListaForeach(Graphics g)
        {
            Nota(g, 20, 14, "List<Lutador> elenco", Paleta.Purple);

            string[] nomes = { "Ana", "Bruno", "Carla", "Diego" };
            for (int i = 0; i < 4; i++)
            {
                int x = 20 + i * 90;
                Caixa(g, x, 38, 80, 46, "", null, Paleta.Purple, Paleta.Tint);
                using (SolidBrush t = new SolidBrush(Paleta.Ink))
                {
                    g.DrawString(nomes[i], FonteMonoB, t, x + 10, 48);
                }
                using (SolidBrush t = new SolidBrush(Paleta.PurpleLt))
                {
                    g.DrawString("[" + i + "]", FonteMono, t, x + 10, 64);
                }
            }

            Nota(g, 20, 96, "foreach  ->  passa por todos, sem indice", Paleta.Mint);
            Nota(g, 20, 118, "for      ->  quando voce PRECISA da posicao", Paleta.Purple);

            Nota(g, 400, 96, "Comeca no ZERO:", Paleta.Red);
            Nota(g, 400, 118, "elenco[0] e o primeiro.", Paleta.Red);
        }
    }
}
