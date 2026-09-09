using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeiroOTeste;

namespace PrimeiroOTesteTestes
{
    /// <summary>
    /// O ROTEIRO DE TESTES DA CONTA - UC11, Aula 7, 09/09/2026.
    ///
    /// A Conta e diferente da Calculadora, e a diferenca tem nome:
    /// ela GUARDA COISA. A Calculadora recebe dois numeros e devolve
    /// um terceiro; a Conta tem um saldo, e o saldo muda.
    ///
    /// Isso cria um problema que nao existia ate agora: se um teste
    /// deposita 100 e o proximo teste usa a MESMA conta, o segundo
    /// comeca com 100 sem saber. Ai a ordem em que os testes rodam
    /// passa a mudar o resultado, e voce vai cacar um defeito que nao
    /// existe.
    ///
    /// A solucao esta logo abaixo, no [TestInitialize]: ele roda ANTES
    /// DE CADA TESTE e cria contas novas. Ele ja vem pronto - so use.
    ///
    /// E TEM UMA SEGUNDA COISA para prestar atencao. Em varios casos
    /// nao basta conferir o que o metodo DEVOLVEU: e preciso conferir
    /// tambem o que ele fez com o SALDO. Um saque recusado que devolve
    /// false mas leva o dinheiro embora passa num teste que so olha o
    /// false.
    /// </summary>
    [TestClass]
    public class ContaTests
    {
        private const double FOLGA = 0.0001;

        // As duas contas da noite. Elas nascem de novo a cada teste.
        private Conta conta;
        private Conta destino;

        // ============================================================
        // ISTO SAI PRONTO, e e a novidade tecnica da noite.
        //
        // O [TestInitialize] roda antes de CADA [TestMethod] desta
        // classe. Nao e voce que chama - o adaptador chama.
        //
        // Por isso todo teste daqui comeca com duas contas zeradas,
        // e nenhum teste enxerga o que o outro fez.
        // ============================================================
        [TestInitialize]
        public void AntesDeCadaTeste()
        {
            conta = new Conta();
            destino = new Conta();
        }

        // ============================================================
        // ESTE SAI PRONTO - o molde mais simples que existe.
        //
        // Rode agora: tem de ficar VERDE, e sem voce implementar nada.
        // Pense por que. (Dica: o Saldo nao e metodo, e propriedade -
        // e propriedade de C# ja nasce funcionando.)
        //
        // E o unico verde de graca da noite. Todo o resto voce ganha.
        // ============================================================
        [TestMethod]
        public void Conta_RecemCriada_ComecaComSaldoZero()
        {
            Assert.AreEqual(0, conta.Saldo, FOLGA);
        }

        // ============================================================
        // TODO 6 - Depositar
        //
        // A REGRA, da sua folha: ..........................
        //
        // Molde, se precisar:
        //
        //   [DataTestMethod]
        //   [DataRow(100.0, 100.0, "deposito comum")]
        //   public void Depositar_PorTabela(double valor, double saldoEsperado, string caso)
        //   {
        //       conta.Depositar(valor);
        //       Assert.AreEqual(saldoEsperado, conta.Saldo, FOLGA, caso);
        //   }
        //
        // E depositar um valor NEGATIVO? Tem regra na sua folha para
        // isso? Se nao tem, esta na hora de decidir - e a decisao vira
        // um [DataRow].
        // ============================================================

        // ============================================================
        // TODO 7 - Sacar
        //
        // A REGRA, da sua folha: ..........................
        //
        // Este e o metodo com mais casos da noite, e por dois motivos:
        // ele DEVOLVE true ou false E ele MEXE no saldo. Todo caso
        // seu precisa conferir as duas coisas.
        //
        //   saldo 100, sacar 30    ->  devolve ....  saldo fica ....
        //   saldo 100, sacar 150   ->  devolve ....  saldo fica ....
        //   saldo 100, sacar 100   ->  devolve ....  saldo fica ....
        //   saldo 100, sacar -50   ->  devolve ....  saldo fica ....
        //
        // A terceira linha e a que separa quem leu a regra de quem
        // achou que leu.
        // ============================================================

        // ============================================================
        // TODO 8 - TemSaldo
        //
        // A REGRA, da sua folha: ..........................
        //
        // Este metodo so PERGUNTA, e perguntar nao gasta: depois de
        // chamar o TemSaldo o saldo tem de estar do jeito que estava.
        // Vale um teste so para isso, alem da tabela.
        // ============================================================

        // ============================================================
        // TODO 9 - Transferir
        //
        // A REGRA, da sua folha: ..........................
        //
        // Aqui entram as duas contas do [TestInitialize]. Um caso de
        // transferencia mexe em DUAS coisas, e o teste confere as
        // duas:
        //
        //   conta com 100, transferir 40 para destino
        //       ->  conta fica com ....   e destino fica com ....
        //
        // E quando NAO da? Se o saque e recusado, o dinheiro nao pode
        // aparecer do outro lado. Escreva esse caso - ele e o que pega
        // dinheiro sendo fabricado do nada.
        //
        // E transferir para a PROPRIA conta?
        // ============================================================
    }
}
