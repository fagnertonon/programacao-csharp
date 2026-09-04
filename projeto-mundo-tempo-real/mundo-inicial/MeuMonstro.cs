namespace MundoDeCubos
{
    /// <summary>
    /// ================================================================
    ///  DESENHE O SEU MONSTRO
    /// ================================================================
    ///
    /// Aqui nao tem nada para aprender - e para brincar. Voce so mexe no
    /// TEXTO ENTRE ASPAS. Nao precisa entender as chaves nem as virgulas.
    ///
    /// COMO FUNCIONA
    ///
    ///   O monstro e feito de cubinhos, e voce desenha ele em FATIAS,
    ///   uma em cima da outra. Cada linha e uma fatia vista de cima.
    ///
    ///   A primeira fatia sao os PES. A ultima e o alto da CABECA.
    ///   Escreva "---" para dizer "acabou este andar, comeca o de cima".
    ///
    ///   Cada letra e um cubinho colorido, e o ponto e vazio:
    ///
    ///       a  cor A        b  cor B
    ///       c  cor C        d  cor D
    ///       .  nada aqui
    ///
    ///   Todas as linhas de um andar precisam ter o MESMO tamanho. Se
    ///   voce errar, o motor arruma e o jogo nao quebra.
    ///
    /// COMECE MEXENDO NAS CORES - e a mudanca mais rapida de todas.
    /// Depois va mudando o desenho e rodando de novo com F5.
    ///
    /// LIMITES: ate 8 cubinhos de largura, 8 de fundo e 10 andares.
    /// </summary>
    public static class MeuMonstro
    {
        /// <summary>O nome do bicho. Aparece quando ele te encosta.</summary>
        public static string Nome = "meu monstro";

        // ------------------------------------------------------------
        //  AS QUATRO CORES, no formato da web: # e seis digitos
        //  de 0-9 ou A-F.
        //
        //     #FF0000 vermelho   #39FF14 verde neon   #FFD84D amarelo
        //     #241633 roxo       #FFFFFF branco       #101018 quase preto
        // ------------------------------------------------------------

        public static string CorA = "#B02A3C";   // o corpo
        public static string CorB = "#241633";   // os olhos
        public static string CorC = "#FFD84D";   // os dentes
        public static string CorD = "#8B5FBF";   // os chifres

        /// <summary>
        /// O desenho. De baixo para cima, com "---" entre os andares.
        ///
        /// Este que veio de fabrica e um bichinho de quatro pernas com
        /// olhos e dentes. Apague tudo e faca o seu.
        /// </summary>
        public static string[] Desenho =
        {
            // andar 1 - as quatro perninhas
            "a....a",
            "......",
            "......",
            "a....a",
            "---",
            // andar 2 - a barriga
            ".aaaa.",
            "aaaaaa",
            "aaaaaa",
            ".aaaa.",
            "---",
            // andar 3 - a boca, virada para a frente
            ".cccc.",
            "aaaaaa",
            "aaaaaa",
            ".aaaa.",
            "---",
            // andar 4 - os olhos
            ".b..b.",
            "aaaaaa",
            "aaaaaa",
            ".aaaa.",
            "---",
            // andar 5 - o alto da cabeca, com dois chifres
            "d....d",
            ".aaaa.",
            ".aaaa.",
            "......"
        };
    }
}
