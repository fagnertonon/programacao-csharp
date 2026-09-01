using System;

namespace Calculadora
{
    /// <summary>
    /// SO ESCREVE NA TELA. Seis metodos moram aqui.
    ///
    /// Nenhum metodo daqui faz conta e nenhum le do teclado. Eles recebem
    /// o que ja foi calculado e mostram.
    /// </summary>
    public class Tela
    {
        // =================================================================
        //  GRAU 1 - assinatura pronta, voce escreve o corpo.
        // =================================================================

        // -----------------------------------------------------------------
        // TODO 7 - escreva uma linha em branco e depois o texto entre
        // "=== " e " ===".
        //
        // Repare no tipo: void. Este metodo nao devolve nada - ele FAZ.
        // Metodo void nao leva return.
        // -----------------------------------------------------------------
        public static void MostrarTitulo(string texto)
        {
            Console.WriteLine();
            Console.WriteLine("=== " + texto + " ===");
        }

        // -----------------------------------------------------------------
        // TODO 8 - escreva o texto recebido, e so isso.
        // -----------------------------------------------------------------
        public static void MostrarLinha(string texto)
        {
            Console.WriteLine(texto);

        }

        // -----------------------------------------------------------------
        // TODO 9 - escreva a mensagem com ">> " na frente, para o erro
        // ficar diferente do resto.
        // -----------------------------------------------------------------
        public static void MostrarErro(string mensagem)
        {
            Console.WriteLine(">> " + mensagem);
        }

        // -----------------------------------------------------------------
        // TODO 10 - avise "Tecle ENTER para voltar ao menu..." e espere a
        // pessoa teclar.
        //
        // Repare nos parenteses: eles estao VAZIOS. Este metodo nao recebe
        // nada e nao devolve nada - e continua sendo um metodo.
        // -----------------------------------------------------------------
        public static void Pausar()
        {
            Console.WriteLine();
            Console.WriteLine("Tecle ENTER para voltar ao menu...");
            Console.ReadLine();
        }

        // =================================================================
        //  GRAU 2 - voce escreve o metodo inteiro.
        // =================================================================

        // -----------------------------------------------------------------
        // TODO 19 - MostrarResultado
        //
        // Mostra a conta inteira numa linha so, assim:
        //
        //        10 + 3 = 13,00
        //
        // Para isso ele precisa de QUATRO coisas: o primeiro numero, o
        // sinal, o segundo numero e o resultado. Quatro parametros na mesma
        // assinatura - e a maior que voce vai escrever hoje.
        //
        // Ele NAO calcula o resultado: ele RECEBE o resultado ja pronto.
        // Quem calculou foi o Calculo.
        //
        // Para as duas casas decimais, use .ToString("N2") no resultado.
        // -----------------------------------------------------------------
        public static void MostrarResultado(double a, string sinal, double b, double resultado)
        {
            Console.WriteLine();
            Console.WriteLine("   " + a + " " + sinal + " " + b + " = " + resultado.ToString("N2"));
        }

        // =================================================================
        //  GRAU 3 - so o problema.
        // =================================================================

        // -----------------------------------------------------------------
        // TODO 23 - MostrarMenu
        //
        // Desenhe o menu na tela, exatamente assim:
        //
        //   =====================================
        //      CALCULADORA - Curso Tecnico
        //   =====================================
        //    1 - Somar             6 - Media
        //    2 - Subtrair          7 - Potencia
        //    3 - Multiplicar       8 - Fatorial
        //    4 - Dividir           9 - Comparar
        //    5 - Porcentagem       0 - Sair
        //   =====================================
        //   Escolha:
        //
        // A ultima linha nao pula linha - o cursor tem que ficar do lado do
        // "Escolha: ". Existe um metodo do console que escreve SEM pular
        // linha; e o irmao do WriteLine.
        // -----------------------------------------------------------------
        public static void MostrarMenu()
        {
            Console.WriteLine();
            Console.WriteLine("=====================================");
            Console.WriteLine("   CALCULADORA - Curso Tecnico");
            Console.WriteLine("=====================================");
            Console.WriteLine(" 1 - Somar             6 - Media");
            Console.WriteLine(" 2 - Subtrair          7 - Potencia");
            Console.WriteLine(" 3 - Multiplicar       8 - Fatorial");
            Console.WriteLine(" 4 - Dividir           9 - Comparar");
            Console.WriteLine(" 5 - Porcentagem       0 - Sair");
            Console.WriteLine("=====================================");
            Console.Write("Escolha: ");
        }

    }
}
