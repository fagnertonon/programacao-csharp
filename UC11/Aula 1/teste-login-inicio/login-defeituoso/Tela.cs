using System;

namespace SistemaLogin
{
    /// <summary>
    /// So escreve na tela. Seis metodos.
    ///
    /// Nenhum metodo daqui decide nada e nenhum le do teclado. Eles recebem
    /// o que ja foi decidido e mostram.
    ///
    /// E POR ISSO QUE ESTA CLASSE NAO SERA TESTADA HOJE: um teste nao
    /// consegue conferir sozinho o que apareceu na tela.
    /// </summary>
    public class Tela
    {
        // void. Este metodo nao devolve nada: ele faz.
        public static void MostrarTitulo(string texto)
        {
            Console.WriteLine();
            Console.WriteLine("=== " + texto + " ===");
        }

        public static void MostrarLinha(string texto)
        {
            Console.WriteLine(texto);
        }

        // A mensagem de recusa, com o marcador de erro na frente.
        //
        // ATENCAO: o ">> " e daqui. Ele NAO faz parte do texto que o
        // Cadastro.MensagemDoErro devolve.
        public static void MostrarErro(string mensagem)
        {
            Console.WriteLine(">> " + mensagem);
        }

        // A mensagem de sucesso. Mesmo molde do MostrarErro, outro marcador.
        public static void MostrarOk(string mensagem)
        {
            Console.WriteLine("** " + mensagem);
        }

        // Uma resposta de sim ou nao, com o rotulo na frente.
        public static void MostrarResposta(string rotulo, bool valor)
        {
            if (valor == true)
            {
                Console.WriteLine(rotulo + "SIM");
            }
            else
            {
                Console.WriteLine(rotulo + "NAO");
            }
        }

        // void e SEM parametro nenhum. Os parenteses ficam
        // vazios, e continua sendo um metodo.
        public static void Pausar()
        {
            Console.WriteLine();
            Console.WriteLine("Tecle ENTER para voltar ao menu...");
            Console.ReadLine();
        }

        public static void MostrarMenu()
        {
            Console.WriteLine();
            Console.WriteLine("=====================================");
            Console.WriteLine("   CRIAR CONTA - Portal do Aluno");
            Console.WriteLine("=====================================");
            Console.WriteLine(" 1 - Criar uma conta");
            Console.WriteLine(" 2 - Ver as contas que ja existem");
            Console.WriteLine(" 3 - Contar caracteres iguais seguidos");
            Console.WriteLine(" 0 - Sair");
            Console.WriteLine("=====================================");
            Console.Write("Escolha: ");
        }
    }
}
