using System;

namespace SistemaLogin
{
    /// <summary>
    /// So le do teclado. Quatro metodos.
    ///
    /// Nenhum metodo daqui decide se o acesso e permitido. Eles devolvem
    /// um valor confiavel para quem chamou, e quem decide e o Cadastro.
    ///
    /// TAMBEM NAO SERA TESTADA HOJE: ela le do teclado, e teste unitario
    /// nao sabe digitar.
    /// </summary>
    public class Entrada
    {
        // Le uma linha de texto. Se o usuario so apertar ENTER, devolve
        // texto vazio - e isso e proposital: o vazio tem de chegar ate a
        // regra, para a regra poder recusar.
        public static string LerTexto(string mensagem)
        {
            Console.Write(mensagem);
            string texto = Console.ReadLine();

            if (texto == null)
            {
                return "";
            }

            return texto;
        }

        // while que insiste ate o texto virar numero inteiro.
        //
        // TryParse faz duas coisas na mesma linha: TENTA converter e diz
        // se conseguiu. Por isso ele cabe dentro do while.
        public static int LerInteiro(string mensagem)
        {
            int valor = 0;
            bool deuCerto = false;

            while (deuCerto == false)
            {
                Console.Write(mensagem);
                string texto = Console.ReadLine();

                deuCerto = int.TryParse(texto, out valor);

                if (deuCerto == false)
                {
                    Tela.MostrarErro("Isso nao e um numero inteiro. Tente de novo.");
                }
            }

            return valor;
        }

        // le a opcao do menu e so aceita de 0 a 6.
        //
        // Repare que ele nao repete o LerInteiro: ele CHAMA o LerInteiro
        // e so acrescenta a regra do intervalo.
        public static int LerOpcao()
        {
            int opcao = -1;

            while (opcao < 0 || opcao > 3)
            {
                opcao = LerInteiro("");

                if (opcao < 0 || opcao > 3)
                {
                    Tela.MostrarErro("Escolha um numero de 0 a 3.");
                    Console.Write("Escolha: ");
                }
            }

            return opcao;
        }

        // devolve true ou false a partir do que foi digitado.
        public static bool Confirmar(string pergunta)
        {
            Console.Write(pergunta + " (S/N): ");
            string resposta = Console.ReadLine();

            return resposta == "S" || resposta == "s";
        }
    }
}
