namespace MundoDeCubos
{
    /// <summary>
    /// ================================================================
    ///  O SEU JOGO, DO SEU JEITO
    /// ================================================================
    ///
    /// MEXA AQUI PRIMEIRO, antes de escrever qualquer metodo.
    ///
    /// Troque um valor, salve, PARE o programa e rode de novo com F5.
    /// A mudanca aparece na hora - e nada aqui pode quebrar o jogo:
    /// se voce escrever uma cor torta, o motor usa a de fabrica e
    /// segue.
    ///
    /// Nao ha nada para aprender neste arquivo. E so voce assinando o
    /// seu mundo.
    /// </summary>
    public static class MinhaConfig
    {
        /// <summary>
        /// O seu nome. Aparece la em cima, na barra do jogo.
        /// </summary>
        public static string Jogador = "coloque o seu nome aqui";

        /// <summary>
        /// A SEMENTE DO MUNDO. Trocar este numero muda a ilha inteira:
        /// os morros, a praia, o lago e onde as arvores nascem.
        ///
        /// Experimente 1, 42, 99, 2026, o dia do seu aniversario. Cada
        /// numero da uma ilha diferente, e a mesma semente sempre da a
        /// mesma ilha - entao da para voce mostrar a sua para o colega.
        /// </summary>
        public static int Semente = 7;

        // ------------------------------------------------------------
        //  AS CORES. Todas no formato da web: # e seis digitos de 0-9
        //  ou A-F. Os dois primeiros sao vermelho, os dois do meio
        //  verde, os dois ultimos azul.
        //
        //     #FF0000 vermelho   #39FF14 verde neon   #FFD84D amarelo
        //     #241633 roxo       #FFFFFF branco       #101018 quase preto
        // ------------------------------------------------------------

        /// <summary>A camisa do seu boneco.</summary>
        public static string CorDaRoupa = "#4C6BD9";

        /// <summary>O rosto do seu boneco.</summary>
        public static string CorDaPele = "#EBC29A";

        /// <summary>O cabelo do seu boneco.</summary>
        public static string CorDoCabelo = "#5C3A26";

        /// <summary>A calca do seu boneco.</summary>
        public static string CorDaCalca = "#2B3A6B";

        /// <summary>
        /// O ceu. Ponha #101018 e o seu mundo vira noite; #E8935A e
        /// entardecer; #9AD6F0 e um dia limpo.
        /// </summary>
        public static string CorDoCeu = "#6BA3D8";
    }
}
