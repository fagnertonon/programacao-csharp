using System.Collections.Generic;

namespace Revisao
{
    // ===================================================================
    //  E AQUI QUE VOCE TRABALHA HOJE.
    //
    //  Sao oito metodos. Cada um esta vazio, com um // TODO no lugar do
    //  codigo que falta. Escreva o codigo, salve, e rode com F5.
    //
    //  O aplicativo testa sozinho o que voce escreveu:
    //    - acertou  -> a aba fica verde e a proxima destrava
    //    - errou    -> ele mostra o que esperava e o que recebeu
    //
    //  Nao mexa nos nomes dos metodos nem no que esta entre parenteses.
    //  E por ali que o corretor encontra o seu codigo.
    //
    //  ATENCAO: todo metodo aqui DEVOLVE um valor com return. Escrever na
    //  tela com Console.WriteLine nao conta - o corretor nao le a tela,
    //  ele le o que o metodo devolveu.
    // ===================================================================

    public static class Desafios
    {
        // -------------------------------------------------------------
        // 1 - CLASSE E CAMPO
        //
        // Crie um objeto Lutador com new, coloque o nome recebido no
        // campo Nome e devolva o objeto.
        // Vida e Forca ja nascem com o valor certo do molde.
        // -------------------------------------------------------------
        public static Lutador CriarLutador(string nome)
        {
            // TODO: crie o objeto com new, coloque o Nome e devolva.
            return null;
        }

        // -------------------------------------------------------------
        // 2 - METODO (parametro e retorno)
        //
        // Some os pontos a vida e devolva o resultado.
        // A vida nunca pode passar de 20 - se passar, devolva 20.
        // -------------------------------------------------------------
        public static int Descansar(int vida, int pontos)
        {
            // TODO: some, trave o teto em 20, devolva.
            return 0;
        }

        // -------------------------------------------------------------
        // 3 - OPERADORES (== && ||)
        //
        // Devolva verdadeiro somente quando a vida for exatamente 20
        // E os treinos forem 3 ou mais. Nos outros casos, falso.
        //
        // CUIDADO: = guarda um valor. == pergunta se e igual.
        // -------------------------------------------------------------
        public static bool EstaPronto(int vida, int treinos)
        {
            // TODO: uma linha basta.
            return false;
        }

        // -------------------------------------------------------------
        // 4 - IF / ELSE IF
        //
        // Devolva a mencao pela nota:
        //   9 ou mais -> "A"     7 ou mais -> "B"
        //   5 ou mais -> "C"     abaixo    -> "D"
        //
        // A ORDEM DOS TESTES IMPORTA. Comece pela nota mais alta.
        // -------------------------------------------------------------
        public static string Mencao(double nota)
        {
            // TODO: a cadeia if / else if / else.
            return "";
        }

        // -------------------------------------------------------------
        // 5 - SWITCH
        //
        // DEVOLVA (com return, nao com Console.WriteLine):
        //   "1" -> "Treinar"      "2" -> "Descansar"
        //   "3" -> "Ficha"        "0" -> "Sair"
        //   qualquer outra coisa  -> "Opcao invalida"
        //
        // Use switch, nao if. E nao esqueca o default.
        // -------------------------------------------------------------
        public static string DescreverOpcao(string opcao)
        {
            // TODO: o switch com os quatro case e o default.
            return "";
        }

        // -------------------------------------------------------------
        // 6 - FOR
        //
        // Monte e devolva um texto com um # para cada ponto de vida.
        //   vida 5 -> "#####"      vida 0 -> "" (texto vazio)
        //
        // Nao precisa de if para o caso do zero: o proprio for resolve.
        // -------------------------------------------------------------
        public static string Barra(int vida)
        {
            // TODO: comece com texto vazio e va grudando um # por volta.
            return "";
        }

        // -------------------------------------------------------------
        // 7 - WHILE   (assunto NOVO desta aula)
        //
        // Usando WHILE (nao use for), devolva os numeros de 1 ate n
        // separados por espaco, com espaco tambem no fim.
        //   n = 3 -> "1 2 3 "      n = 0 -> "" (texto vazio)
        //
        // Nao esqueca o passo DENTRO do corpo, ou o programa trava.
        // -------------------------------------------------------------
        public static string ContarAte(int n)
        {
            // TODO: declare o contador antes, teste no while, avance dentro.
            return "";
        }

        // -------------------------------------------------------------
        // 8 - FOREACH E LIST   (assunto NOVO desta aula)
        //
        // Usando FOREACH (nao use for), devolva os nomes de todos os
        // lutadores da lista separados por virgula e espaco, com virgula
        // e espaco tambem no fim.
        //   [Ana, Bruno] -> "Ana, Bruno, "
        //   lista vazia  -> "" (texto vazio)
        // -------------------------------------------------------------
        public static string Elenco(List<Lutador> lutadores)
        {
            // TODO: foreach (Lutador l in lutadores) { ... }
            return "";
        }
    }
}
