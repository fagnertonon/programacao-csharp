using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeiroOTeste;

namespace PrimeiroOTesteTestes
{
    /// <summary>
    /// O ROTEIRO DE TESTES DA CALCULADORA - UC11, Aula 7, 09/09/2026.
    ///
    /// Hoje e ao contrario de ontem. Ontem o roteiro vinha com os
    /// dezoito nomes prontos e voce so escrevia o Assert. Hoje NAO HA
    /// nomes prontos, porque os nomes saem da SUA folha de regras.
    ///
    /// A ORDEM E ESTA, e ela nao muda:
    ///
    ///   1. a regra esta na folha, escrita por voces no quadro
    ///   2. voce escreve o teste  ->  RODA  ->  fica VERMELHO
    ///   3. so entao voce abre a Calculadora.cs e escreve o corpo
    ///   4. RODA de novo  ->  fica VERDE
    ///
    /// Se voce escrever o corpo antes do teste, voce perdeu a noite:
    /// um teste que nasce verde nao provou nada, porque voce nunca o
    /// viu falhar. Ele pode estar testando o nada.
    ///
    /// O KIT DE HOJE:
    ///   Assert.AreEqual(esperado, obtido, FOLGA, "o caso")
    ///   Assert.IsTrue(...)   Assert.IsFalse(...)
    ///   Assert.ThrowsException<TipoDoErro>(() => o que deve estourar)
    ///
    /// O ESPERADO VEM PRIMEIRO. E a FOLGA existe porque numero com
    /// casa decimal quase nunca e exatamente igual no computador.
    /// </summary>
    [TestClass]
    public class CalculadoraTests
    {
        // A folga do double. Nao mexa.
        private const double FOLGA = 0.0001;

        // ============================================================
        // ESTE SAI PRONTO - e o molde de todos os outros.
        //
        //   1. [DataTestMethod] em cima, e nao [TestMethod], porque
        //      ele vai receber uma TABELA de casos.
        //   2. um [DataRow] por caso. A ordem dos valores e a mesma
        //      da assinatura logo abaixo.
        //   3. o ultimo valor e um texto: e ele que aparece na barra
        //      quando o caso fica vermelho. Escreva o que voce esta
        //      testando, nao "teste1".
        //   4. UMA linha de Assert la dentro, e ela serve para todos
        //      os casos da tabela.
        //
        // RODE ESTE AGORA, antes de escrever qualquer outro. Ele tem
        // de ficar VERMELHO, e a mensagem tem de dizer
        // NotImplementedException. Esse vermelho e o ponto de partida.
        // ============================================================
        [DataTestMethod]
        [DataRow(2.0, 3.0, 5.0, "o caso obvio")]
        [DataRow(0.0, 0.0, 0.0, "dois zeros")]
        public void Somar_PorTabela(double a, double b, double esperado, string caso)
        {
            Assert.AreEqual(esperado, Calculadora.Somar(a, b), FOLGA, caso);
        }

        // ============================================================
        // TODO 1 - Somar, os casos que faltam
        //
        // O molde acima tem dois casos. A sua folha tem mais regras do
        // que isso. Acrescente os [DataRow] que faltam AQUI EM CIMA,
        // no metodo que ja existe - nao precisa de metodo novo.
        //
        // Pergunte-se: e negativo? e somar zero? e casa decimal?
        // ============================================================

        // ============================================================
        // TODO 2 - Subtrair
        //
        // A REGRA, da sua folha: ..........................
        //
        // Escreva o teste ANTES de implementar. Molde, se precisar:
        //
        //   [DataTestMethod]
        //   [DataRow(10.0, 4.0, 6.0, "o caso obvio")]
        //   public void Subtrair_PorTabela(double a, double b, double esperado, string caso)
        //   {
        //       Assert.AreEqual(esperado, Calculadora.Subtrair(a, b), FOLGA, caso);
        //   }
        //
        // Cuidado com a ORDEM: Subtrair(4, 10) nao e a mesma coisa
        // que Subtrair(10, 4). Tem caso na sua folha para isso?
        // ============================================================

        // ============================================================
        // TODO 3 - Multiplicar
        //
        // A REGRA, da sua folha: ..........................
        //
        // Tres perguntas que valem [DataRow]:
        //   - e quando um dos dois e negativo?
        //   - e quando os DOIS sao negativos?
        //   - e vezes zero?
        // ============================================================

        // ============================================================
        // TODO 4 - Dividir
        //
        // A REGRA, da sua folha: ..........................
        //
        // E O ZERO? A turma decidiu alguma coisa no comeco da noite.
        // Escreva aqui a decisao de voces:
        //
        //   Dividir(10, 0) tem de ....................................
        //
        // Se a decisao foi ESTOURAR, o Assert e outro - nao e AreEqual:
        //
        //   [TestMethod]
        //   public void Dividir_ComZero_Estoura()
        //   {
        //       Assert.ThrowsException<DivideByZeroException>(() => Calculadora.Dividir(10, 0));
        //   }
        //
        // Se a decisao foi devolver um valor, entao e AreEqual mesmo.
        // ============================================================

        // ============================================================
        // TODO 5 - Porcentagem
        //
        // A REGRA, da sua folha: ..........................
        //
        // Este metodo pode ser escrito perguntando para o Multiplicar
        // e para o Dividir, em vez de fazer a conta na mao. Se voce
        // fizer assim, repare no que acontece com a barra quando um
        // daqueles dois estiver errado.
        // ============================================================
    }
}
