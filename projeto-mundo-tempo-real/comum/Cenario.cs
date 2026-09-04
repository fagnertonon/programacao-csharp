using System;

namespace MundoDeCubos
{
    // ==================================================================
    //  OS MUNDINHOS DE TESTE. Voce NAO precisa mexer aqui.
    //
    //  Cada teste do JSON diz o nome de um cenario, e este arquivo monta
    //  aquele mundinho do zero. Nada aqui depende do mundo do jogo: e por
    //  isso que o que voce faz jogando nunca destrava nem trava um
    //  desafio.
    //
    //  Em todos os cenarios o CHAO e solido ate o Y = 8, entao o jogador
    //  fica de pe no Y = 9 com a cabeca no Y = 10.
    // ==================================================================
    public static class Cenario
    {
        public const int CHAO = 8;    // ultimo Y solido
        public const int PE = 9;    // onde o jogador pisa

        public static Mundo Montar(string nome)
        {
            Mundo m = new Mundo();

            foreach (Bloco b in Gerador.BlocosDeFabrica())
            {
                m.RegistrarTipo(b);
            }

            Piso(m);

            if (nome == "degrau")
            {
                // Um degrau de UM cubo em x = 6. Da para subir andando.
                Coluna(m, 6, PE, PE, "grama");
            }
            else if (nome == "parede")
            {
                // Parede de DOIS cubos em x = 6. Nao da para subir.
                Coluna(m, 6, PE, PE + 1, "pedra");
            }
            else if (nome == "teto")
            {
                // Teto logo acima da cabeca: nao da para pular.
                Laje(m, PE + 2, "pedra");
            }
            else if (nome == "tetoalto")
            {
                // Teto a quatro cubos: da para pular 2, nao 5.
                Laje(m, PE + 4, "pedra");
            }
            else if (nome == "torre")
            {
                // Uma torre de tipos variados em x = 5, z = 5, para minerar.
                m.Por(5, PE, 5, "grama");
                m.Por(5, PE + 1, 5, "pedra");
                m.Por(5, PE + 2, 5, "folha");
                m.Por(5, PE + 3, 5, "tronco");
                m.Por(5, PE + 4, 5, "areia");
            }
            else if (nome == "poco")
            {
                // Uma coluna solida do bedrock ate o chao, em x = 5, z = 5,
                // para o Cavar furar.
                //   Y 0        bedrock
                //   Y 1 a 5    pedra
                //   Y 6 a 8    terra
                for (int y = 1; y <= 5; y++) { m.Por(5, y, 5, "pedra"); }
                for (int y = 6; y <= CHAO; y++) { m.Por(5, y, 5, "terra"); }
            }
            else if (nome == "buraco")
            {
                // Igual ao poco, mas com um vao vazio no meio (Y 6 e 7),
                // para provar que o Cavar conta so o que realmente tirou.
                for (int y = 1; y <= 5; y++) { m.Por(5, y, 5, "pedra"); }
                m.Tirar(5, 6, 5);
                m.Tirar(5, 7, 5);
                m.Por(5, CHAO, 5, "terra");
            }

            return m;
        }

        // Chao macico do Y = 0 ao Y = 8, com bedrock embaixo.
        private static void Piso(Mundo m)
        {
            for (int x = 0; x < Mundo.LARGURA; x++)
            {
                for (int z = 0; z < Mundo.FUNDO; z++)
                {
                    m.Por(x, 0, z, "bedrock");
                    for (int y = 1; y < CHAO; y++) { m.Por(x, y, z, "terra"); }
                    m.Por(x, CHAO, z, "grama");
                }
            }
        }

        private static void Coluna(Mundo m, int x, int de, int ate, string tipo)
        {
            for (int z = 0; z < Mundo.FUNDO; z++)
            {
                for (int y = de; y <= ate; y++) { m.Por(x, y, z, tipo); }
            }
        }

        private static void Laje(Mundo m, int y, string tipo)
        {
            for (int x = 0; x < Mundo.LARGURA; x++)
            {
                for (int z = 0; z < Mundo.FUNDO; z++) { m.Por(x, y, z, tipo); }
            }
        }
    }
}
