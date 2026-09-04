using System;
using System.Collections.Generic;
using System.Threading;

namespace MundoDeCubos
{
    // Roda o codigo do aluno com PRAZO.
    //
    // Sem isto, um while cuja condicao nunca fica falsa - o erro classico
    // do desafio 2 - deixa o servidor pendurado, o navegador esperando
    // para sempre, e a unica saida e fechar tudo. Com isto, o servidor
    // espera 2 segundos, desiste, e responde dizendo o que aconteceu.
    //
    // A thread que ficou girando NAO tem como ser morta com seguranca no
    // .NET moderno (Thread.Abort nao existe mais). Ela e marcada como
    // IsBackground, entao nao segura o processo na hora de fechar, e o
    // servidor para de insistir naquele desafio depois de duas
    // tentativas - senao cada clique deixaria mais uma girando.
    //
    // Voce NAO precisa mexer aqui.
    //
    // ------------------------------------------------------------------
    //  NOTA DE MANUTENCAO
    //  A contagem e POR DESAFIO. Com um contador unico, um laco infinito
    //  no desafio 2 fazia os desafios ja resolvidos virarem "nao
    //  terminou" e travarem de volta. Um erro num desafio nao pode
    //  derrubar os outros.
    // ------------------------------------------------------------------

    public class Execucao
    {
        public object Valor;
        public Exception Falha;
        public bool EstourouOTempo;
        public bool Desistiu;
    }

    public static class Sandbox
    {
        public const int LIMITE_MS = 2000;
        private const int TENTATIVAS = 2;

        private static readonly Dictionary<string, int> presas =
            new Dictionary<string, int>();
        private static readonly object trava = new object();

        private static int PresasDe(string chave)
        {
            int n;
            return presas.TryGetValue(chave ?? "", out n) ? n : 0;
        }

        public static void Perdoar()
        {
            lock (trava) { presas.Clear(); }
        }

        public static Execucao Rodar(string chave, Func<object> f)
        {
            Execucao e = new Execucao();

            lock (trava)
            {
                if (PresasDe(chave) >= TENTATIVAS)
                {
                    e.EstourouOTempo = true;
                    e.Desistiu = true;
                    return e;
                }
            }

            object valor = null;
            Exception falha = null;

            Thread t = new Thread(delegate ()
            {
                try { valor = f(); }
                catch (Exception ex) { falha = ex; }
            });

            t.IsBackground = true;
            t.Start();

            if (!t.Join(LIMITE_MS))
            {
                lock (trava) { presas[chave ?? ""] = PresasDe(chave) + 1; }
                e.EstourouOTempo = true;
                return e;
            }

            e.Valor = valor;
            e.Falha = falha;
            return e;
        }

        public static string Recado(Execucao e)
        {
            if (e != null && e.Desistiu)
            {
                return "Este desafio ja ficou preso duas vezes. Pare o programa, "
                     + "conserte o laco em Desafios.cs e rode de novo com F5. "
                     + "Os outros desafios continuam funcionando.";
            }
            return "O seu codigo passou de 2 segundos sem terminar. Quase sempre e "
                 + "um laco cuja condicao nunca fica falsa: confira se alguma coisa "
                 + "muda DENTRO do corpo do while.";
        }

        // O nome cru da excecao nao diz nada para quem esta aprendendo.
        public static string Traduzir(Exception ex)
        {
            if (ex is NullReferenceException)
            {
                return "NullReferenceException: voce usou um objeto que nao existe. "
                     + "Quase sempre e um 'return null;' que ficou no lugar, ou um "
                     + "Pos.Nova que faltou.";
            }
            if (ex is DivideByZeroException)
            {
                return "Voce dividiu por zero. Confira o divisor antes de dividir.";
            }
            if (ex is IndexOutOfRangeException || ex is ArgumentOutOfRangeException)
            {
                return "Voce pediu uma posicao que nao existe. Pergunte "
                     + "mundo.Dentro(x, y, z) ANTES de mexer no bloco.";
            }
            if (ex is StackOverflowException)
            {
                return "O metodo chamou a si mesmo sem parar.";
            }
            return ex.GetType().Name + ": " + ex.Message;
        }
    }
}
