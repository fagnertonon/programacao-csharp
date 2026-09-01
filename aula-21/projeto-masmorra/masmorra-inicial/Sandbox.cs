using System;
using System.Collections.Generic;
using System.Threading;

namespace Masmorra
{
    // Roda o codigo do aluno com PRAZO.
    //
    // Sem isto, um while sem o passo dentro do corpo congela a janela inteira:
    // o laco roda na mesma thread que desenha a tela, e so o Gerenciador de
    // Tarefas resolve. Com isto, o aplicativo espera 2 segundos, desiste, e
    // avisa que o laco nao termina.
    //
    // A thread que ficou girando NAO tem como ser morta com seguranca no .NET
    // moderno (Thread.Abort nao existe mais). Ela e marcada como IsBackground,
    // entao nao segura o processo na hora de fechar, e o aplicativo para de
    // insistir naquele desafio depois de duas tentativas - senao cada clique
    // deixaria mais uma girando e a maquina do aluno ficaria lenta.
    //
    // Voce NAO precisa mexer aqui.
    //
    // ------------------------------------------------------------------
    //  NOTA DE MANUTENCAO
    //  A contagem e POR DESAFIO, e essa e a parte que importa. Com um
    //  contador unico para o aplicativo inteiro, um laco infinito no
    //  desafio 8 fazia os desafios 1 a 7 - que estavam certos e ja
    //  resolvidos - virarem "(nao terminou)" e travarem as abas de volta.
    //  Um erro num desafio nao pode derrubar os outros.
    // ------------------------------------------------------------------

    public class Execucao
    {
        public string Valor;
        public Exception Falha;
        public bool EstourouOTempo;
        public bool Desistiu;      // ja tentou demais neste mesmo desafio
    }

    public static class Sandbox
    {
        public const int LIMITE_MS = 2000;
        private const int TENTATIVAS = 2;

        private static readonly Dictionary<string, int> presas =
            new Dictionary<string, int>();

        private static int PresasDe(string chave)
        {
            int n;
            return presas.TryGetValue(chave ?? "", out n) ? n : 0;
        }

        public static Execucao Rodar(string chave, Func<string> f)
        {
            Execucao e = new Execucao();

            if (PresasDe(chave) >= TENTATIVAS)
            {
                e.EstourouOTempo = true;
                e.Desistiu = true;
                return e;
            }

            string valor = null;
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
                presas[chave ?? ""] = PresasDe(chave) + 1;
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
                return "Este desafio ja ficou preso duas vezes. Feche o programa, "
                     + "conserte o laco em Desafios.cs e rode de novo com F5. "
                     + "Os outros desafios continuam funcionando normalmente.";
            }
            return "O seu codigo passou de 2 segundos sem terminar. Quase sempre e "
                 + "um laco cuja condicao nunca fica falsa: confira se alguma coisa "
                 + "muda DENTRO do corpo do while.";
        }
    }
}
