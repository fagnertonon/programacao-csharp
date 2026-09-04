using System;
using System.Collections.Generic;

namespace MundoDeCubos
{
    // ==================================================================
    //  O GERADOR DE MUNDO. Voce NAO escreve nada aqui.
    //
    //  Ele monta a ilha: relevo, terra, pedra, agua e arvores. E
    //  DETERMINISTICO de proposito - a mesma semente devolve sempre o
    //  mesmo mundo, entao a turma inteira ve a mesma ilha no projetor.
    //
    //  A semente e o primeiro lugar em que voce pode personalizar: troque
    //  o numero em MinhaConfig.cs e voce ganha outra ilha.
    // ==================================================================
    public static class Gerador
    {
        public const int NIVEL_DA_AGUA = 7;

        // Os cinco blocos de fabrica. Os SEUS entram depois, no desafio 6.
        public static List<Bloco> BlocosDeFabrica()
        {
            List<Bloco> lista = new List<Bloco>();
            lista.Add(Bloco.Novo("bedrock", "#3A3340", 9));
            lista.Add(Bloco.Novo("pedra", "#8A8494", 3));
            lista.Add(Bloco.Novo("terra", "#7A5A3C", 1));
            lista.Add(Bloco.Novo("grama", "#5AA65A", 1));
            lista.Add(Bloco.Novo("areia", "#DCCB92", 1));
            lista.Add(Bloco.Novo("agua", "#3F7FBF", 1));
            lista.Add(Bloco.Novo("tronco", "#6B4A2B", 2));
            lista.Add(Bloco.Novo("folha", "#3E8E4F", 1));
            lista.Add(Bloco.Novo("cascalho", "#9A9099", 1));
            return lista;
        }

        public static Mundo Criar(int semente)
        {
            Mundo m = new Mundo();

            foreach (Bloco b in BlocosDeFabrica())
            {
                m.RegistrarTipo(b);
            }

            // ---- o relevo ----
            // Soma de senos em vez de ruido de Perlin: cabe em cinco
            // linhas, nao precisa de biblioteca, e ja da morro e vale.
            for (int x = 0; x < Mundo.LARGURA; x++)
            {
                for (int z = 0; z < Mundo.FUNDO; z++)
                {
                    int altura = AlturaEm(x, z, semente);

                    for (int y = 0; y <= altura; y++)
                    {
                        m.Por(x, y, z, TerrenoDe(y, altura));
                    }

                    // ---- a agua ----
                    for (int y = altura + 1; y <= NIVEL_DA_AGUA; y++)
                    {
                        m.Por(x, y, z, "agua");
                    }
                }
            }

            PlantarArvores(m, semente);
            return m;
        }

        private static int AlturaEm(int x, int z, int semente)
        {
            double s = semente * 0.017;

            double h = 9.0
                     + 2.6 * Math.Sin(x * 0.31 + s)
                     + 2.2 * Math.Cos(z * 0.27 - s)
                     + 1.4 * Math.Sin((x + z) * 0.19 + s * 2.0)
                     + 0.9 * Math.Cos((x - z) * 0.41);

            int altura = (int)Math.Round(h);

            if (altura < 2) { altura = 2; }
            if (altura > Mundo.ALTURA - 6) { altura = Mundo.ALTURA - 6; }
            return altura;
        }

        private static string TerrenoDe(int y, int altura)
        {
            if (y == 0) { return "bedrock"; }
            if (y < altura - 3) { return "pedra"; }
            if (y < altura) { return "terra"; }

            // A camada de cima: areia na beira da agua, grama no resto.
            if (altura <= NIVEL_DA_AGUA) { return "areia"; }
            return "grama";
        }

        // Arvores em posicoes fixas, calculadas da semente. Sem sorteio:
        // o mundo tem que ser o mesmo em todas as maquinas da sala.
        private static void PlantarArvores(Mundo m, int semente)
        {
            int passo = 5;
            int desvio = semente % 3;

            for (int x = 3 + desvio; x < Mundo.LARGURA - 3; x += passo)
            {
                for (int z = 3; z < Mundo.FUNDO - 3; z += passo)
                {
                    int chao = ChaoEm(m, x, z);

                    if (chao <= NIVEL_DA_AGUA) { continue; }
                    if (m.Bloco(x, chao, z) != "grama") { continue; }
                    if (chao + 6 >= Mundo.ALTURA) { continue; }

                    int tronco = 3 + ((x + z + semente) % 2);

                    for (int i = 1; i <= tronco; i++)
                    {
                        m.Por(x, chao + i, z, "tronco");
                    }

                    Copa(m, x, chao + tronco, z);
                }
            }
        }

        private static void Copa(Mundo m, int cx, int cy, int cz)
        {
            for (int dx = -2; dx <= 2; dx++)
            {
                for (int dz = -2; dz <= 2; dz++)
                {
                    for (int dy = 0; dy <= 2; dy++)
                    {
                        int d = Math.Abs(dx) + Math.Abs(dz) + dy;
                        if (d > 3) { continue; }
                        if (dx == 0 && dz == 0 && dy == 0) { continue; }

                        if (m.Vazio(cx + dx, cy + dy, cz + dz))
                        {
                            m.Por(cx + dx, cy + dy, cz + dz, "folha");
                        }
                    }
                }
            }
        }

        /// <summary>O Y do bloco solido mais alto desta coluna.</summary>
        public static int ChaoEm(Mundo m, int x, int z)
        {
            for (int y = Mundo.ALTURA - 1; y >= 0; y--)
            {
                string t = m.Bloco(x, y, z);
                if (t != "" && t != "agua") { return y; }
            }
            return 0;
        }

        /// <summary>
        /// O Y em que um bicho de DOIS cubos cabe de pe nesta coluna, ou
        /// -1 se nao couber.
        ///
        /// Existe porque nascer no lugar errado e o defeito mais chato de
        /// depurar: um monstro criado em cima de uma arvore nasce dentro
        /// do tronco, sobe para a copa atras do jogador e fica preso nas
        /// folhas para sempre. Visto de fora parece que a perseguicao nao
        /// funciona - e funciona, o bicho e que esta enroscado.
        /// </summary>
        public static int ChaoLivreEm(Mundo m, int x, int z)
        {
            int chao = ChaoPisavelEm(m, x, z);
            if (chao <= 0) { return -1; }

            if (!m.Vazio(x, chao + 1, z)) { return -1; }
            if (!m.Vazio(x, chao + 2, z)) { return -1; }

            return chao + 1;
        }

        /// <summary>
        /// O Y do CHAO DE VERDADE - grama, areia, terra ou pedra.
        ///
        /// Diferente do ChaoEm, que devolve o bloco solido mais alto e por
        /// isso aponta para a copa da arvore. Nascer em cima de uma folha,
        /// a 13 cubos do chao, e uma estreia esquisita: o jogador da um
        /// passo e despenca.
        /// </summary>
        public static int ChaoPisavelEm(Mundo m, int x, int z)
        {
            for (int y = Mundo.ALTURA - 1; y >= 0; y--)
            {
                string t = m.Bloco(x, y, z);
                if (t == "grama" || t == "areia" || t == "terra" || t == "pedra")
                {
                    return y;
                }
            }
            return 0;
        }
    }
}
