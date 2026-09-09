using System;

namespace PrimeiroOTeste
{
    /// <summary>
    /// A CALCULADORA - cinco metodos, e NENHUM deles tem corpo.
    ///
    /// Isto aqui e um CONTRATO, nao um programa. Cada metodo ja tem
    /// o nome, os parametros e o tipo de retorno - o que falta e a
    /// conta la dentro.
    ///
    /// A REGRA DA NOITE: voce NAO escreve o corpo antes de existir um
    /// teste vermelho cobrando ele. Escreveu o teste, rodou, ficou
    /// vermelho? AGORA voce vem aqui.
    ///
    /// Repare que as assinaturas ja estao todas escritas. E de
    /// proposito: sem elas o projeto de teste nem compilaria, e voce
    /// ficaria sem barra nenhuma para ler.
    ///
    /// UC11 - Aula 7, 09/09/2026.
    /// </summary>
    public class Calculadora
    {
        // -----------------------------------------------------------------
        // REGRA: devolve a soma dos dois numeros.
        //
        //   Somar(2, 3)   ->  5
        //   Somar(-1, 1)  ->  ?    quem decide e a turma
        // -----------------------------------------------------------------
        public static double Somar(double a, double b)
        {
            throw new NotImplementedException("Escreva o teste primeiro.");
        }

        // -----------------------------------------------------------------
        // REGRA: devolve o primeiro MENOS o segundo. A ordem importa.
        //
        //   Subtrair(10, 4)  ->  6
        //   Subtrair(4, 10)  ->  ?
        // -----------------------------------------------------------------
        public static double Subtrair(double a, double b)
        {
            throw new NotImplementedException("Escreva o teste primeiro.");
        }

        // -----------------------------------------------------------------
        // REGRA: devolve o produto dos dois numeros.
        //
        //   Multiplicar(3, 4)   ->  12
        //   Multiplicar(-3, 4)  ->  ?    e com sinal, o que acontece?
        //   Multiplicar(5, 0)   ->  ?
        // -----------------------------------------------------------------
        public static double Multiplicar(double a, double b)
        {
            throw new NotImplementedException("Escreva o teste primeiro.");
        }

        // -----------------------------------------------------------------
        // REGRA: divide o primeiro pelo segundo.
        //
        //   Dividir(10, 2)  ->  5
        //   Dividir(10, 0)  ->  ???
        //
        // ESTA E A PERGUNTA DA NOITE, e ela nao tem resposta escrita
        // em lugar nenhum: o que TEM de acontecer quando o segundo
        // numero e zero? A turma decide, e a decisao vira teste.
        //
        // Enquanto ninguem decidir, este metodo nao pode ser escrito.
        // -----------------------------------------------------------------
        public static double Dividir(double a, double b)
        {
            throw new NotImplementedException("Escreva o teste primeiro.");
        }

        // -----------------------------------------------------------------
        // REGRA: devolve quanto e o percentual pedido de um valor.
        //
        //   Porcentagem(200, 10)  ->  20     (10% de 200)
        //   Porcentagem(200, 0)   ->  ?
        //
        // Repare que este metodo nao precisa fazer conta nenhuma
        // sozinho: ele pode PERGUNTAR para o Multiplicar e para o
        // Dividir. Se um dos dois mentir, este mente junto.
        // -----------------------------------------------------------------
        public static double Porcentagem(double valor, double percentual)
        {
            throw new NotImplementedException("Escreva o teste primeiro.");
        }
    }
}
