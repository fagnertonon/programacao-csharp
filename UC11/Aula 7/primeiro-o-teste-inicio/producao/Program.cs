using System;

namespace PrimeiroOTeste
{
    /// <summary>
    /// O programa da noite. Ele COMPILA e ele RODA - e nao faz nada,
    /// porque nao ha nada implementado ainda.
    ///
    /// Rode com F5 e veja onde ele para. A mensagem que aparece na
    /// tela e o assunto da aula de hoje.
    /// </summary>
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("=======================================");
            Console.WriteLine("  UC11 - Aula 7 - primeiro o teste");
            Console.WriteLine("=======================================");
            Console.WriteLine();
            Console.WriteLine("Vou tentar somar 2 + 3.");
            Console.WriteLine();

            double resultado = Calculadora.Somar(2, 3);

            Console.WriteLine("Deu " + resultado + ".");
            Console.WriteLine("Se voce esta lendo esta linha, alguem ja implementou.");
        }
    }
}
