using System;
using System.Threading;

namespace Acesso
{
    public class Execucao
    {
        public object Valor;
        public Exception Erro;
        public bool EstourouOTempo;
        public bool NaoImplementado;
    }

    /// <summary>
    /// Roda o codigo do aluno numa thread com prazo, para um laco infinito
    /// nao congelar a janela. Herdado do projeto-desafio-corrida.
    /// </summary>
    public static class Sandbox
    {
        public const int LIMITE_MS = 2000;

        public static Execucao Rodar(Func<object> acao)
        {
            Execucao e = new Execucao();
            object valor = null;
            Exception erro = null;

            Thread t = new Thread(delegate ()
            {
                try { valor = acao(); }
                catch (Exception ex) { erro = ex; }
            });

            t.IsBackground = true;
            t.Start();

            if (!t.Join(LIMITE_MS))
            {
                e.EstourouOTempo = true;
                e.Erro = new TimeoutException("passou de " + LIMITE_MS + " ms");
                return e;
            }

            if (erro != null)
            {
                Exception real = erro;

                while (real is System.Reflection.TargetInvocationException
                       && real.InnerException != null)
                {
                    real = real.InnerException;
                }

                e.Erro = erro;
                e.NaoImplementado = real is NotImplementedException;
                return e;
            }

            e.Valor = valor;
            return e;
        }
    }
}
