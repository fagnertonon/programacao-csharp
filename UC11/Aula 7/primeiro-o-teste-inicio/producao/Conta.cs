using System;

namespace PrimeiroOTeste
{
    /// <summary>
    /// A CONTA BANCARIA - quatro metodos e uma propriedade, e de novo
    /// NENHUM metodo tem corpo.
    ///
    /// A diferenca para a Calculadora e que esta classe GUARDA COISA.
    /// A Calculadora recebe dois numeros e devolve um terceiro, sem
    /// lembrar de nada. A Conta tem SALDO, e o saldo muda.
    ///
    /// Isso muda o teste: cada teste precisa comecar de uma conta
    /// NOVA, senao o saldo que sobrou de um teste aparece no outro e
    /// voce passa a noite cacando um defeito que nao existe.
    ///
    /// UC11 - Aula 7, 09/09/2026.
    /// </summary>
    public class Conta
    {
        // O saldo pode ser LIDO de fora, mas so os metodos daqui de
        // dentro escrevem nele. Uma conta nova comeca com zero.
        public double Saldo { get; private set; }

        // -----------------------------------------------------------------
        // REGRA: poe dinheiro na conta e o saldo sobe.
        //
        //   conta nova, Depositar(100)     ->  saldo 100
        //   saldo 100, Depositar(50)       ->  saldo 150
        //   saldo 100, Depositar(-30)      ->  ???
        //
        // Depositar valor negativo e a pergunta que ninguem faz, e e
        // por ela que o dinheiro some.
        // -----------------------------------------------------------------
        public void Depositar(double valor)
        {
            throw new NotImplementedException("Escreva o teste primeiro.");
        }

        // -----------------------------------------------------------------
        // REGRA: tira dinheiro da conta. Devolve VERDADEIRO quando deu
        // certo e FALSO quando foi recusado.
        //
        //   saldo 100, Sacar(30)   ->  true,  e o saldo fica 70
        //   saldo 100, Sacar(150)  ->  false, e o saldo continua 100
        //   saldo 100, Sacar(100)  ->  ???    exatamente o que tem
        //   saldo 100, Sacar(-50)  ->  ???    sacar negativo
        //
        // Quando o saque e recusado, o SALDO NAO PODE MUDAR. Isso e
        // parte da regra, e nao um detalhe - e um teste que so olha o
        // true/false nao pega isso.
        // -----------------------------------------------------------------
        public bool Sacar(double valor)
        {
            throw new NotImplementedException("Escreva o teste primeiro.");
        }

        // -----------------------------------------------------------------
        // REGRA: responde se da para tirar esse valor da conta, SEM
        // tirar nada. So pergunta.
        //
        //   saldo 100, TemSaldo(30)   ->  true
        //   saldo 100, TemSaldo(150)  ->  false
        //   saldo 100, TemSaldo(100)  ->  ???
        //
        // Depois de chamar este metodo o saldo tem de estar do jeito
        // que estava. Perguntar nao gasta.
        // -----------------------------------------------------------------
        public bool TemSaldo(double valor)
        {
            throw new NotImplementedException("Escreva o teste primeiro.");
        }

        // -----------------------------------------------------------------
        // REGRA: tira desta conta e poe na outra. Devolve VERDADEIRO
        // quando deu certo.
        //
        //   A com 100, B com 0, A.Transferir(B, 40)
        //       ->  true, A fica com 60 e B com 40
        //
        //   A com 100, B com 0, A.Transferir(B, 150)
        //       ->  false, e NENHUMA das duas muda
        //
        // Repare que transferir e sacar de uma e depositar na outra -
        // este metodo pode PERGUNTAR para os de cima em vez de mexer
        // no saldo na mao. E transferir para a propria conta?
        // -----------------------------------------------------------------
        public bool Transferir(Conta destino, double valor)
        {
            throw new NotImplementedException("Escreva o teste primeiro.");
        }
    }
}
