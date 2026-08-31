using System;

namespace Calculadora
{
    /// <summary>
    /// O Main, e so ele.
    ///
    /// Quando voce terminar, este arquivo nao vai ter conta nenhuma e nem
    /// um WriteLine solto. Ele so decide QUEM chamar - todo o resto mora
    /// no Calculo, no Entrada e no Tela.
    /// </summary>
    internal class Program
    {
        // -----------------------------------------------------------------
        // TODO 24 - O ULTIMO. So depois dos 23 anteriores.
        //
        // Apague o corpo abaixo e escreva o programa:
        //
        //   - repete enquanto a pessoa nao escolher 0
        //   - mostra o menu               (Tela.MostrarMenu)
        //   - le a opcao                  (Entrada.LerOpcao)
        //   - e ai um switch com um case para cada opcao de 0 a 9
        //
        // Cada case faz sempre a mesma sequencia: mostra o titulo, le os
        // numeros, chama o Calculo, mostra o resultado e pausa.
        //
        // Duas coisas para nao esquecer:
        //   - na divisao, PERGUNTE ao PodeDividir antes de chamar o
        //     Calcular. E a guarda.
        //   - no 0, use o Confirmar antes de sair de verdade.
        // -----------------------------------------------------------------
        static void Main()
        {
            Console.WriteLine("Calculadora - o projeto de hoje comeca aqui.");
            Console.WriteLine();
            Console.WriteLine("Nada funciona ainda: sao 24 metodos para escrever.");
            Console.WriteLine("Procure por TODO - Exibir > Lista de Tarefas, ou Ctrl+F.");
            Console.ReadLine();
        }
    }
}
