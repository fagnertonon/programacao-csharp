using System.Collections.Generic;

namespace Conecta
{
    // ===================================================================
    //  E AQUI QUE VOCE TRABALHA HOJE.
    //
    //  Sao dez metodos. Cada um esta vazio, com um // TODO no lugar do
    //  codigo que falta. Escreva o codigo, salve, e rode com F5.
    //
    //  Cada um deles e um pedaco do Conecta - o mesmo sistema que voce
    //  esta construindo na apostila. Nao e exercicio de faz de conta.
    //
    //  O aplicativo faz DUAS coisas com o que voce escrever:
    //    1. testa sozinho  -> acertou, a proxima aba destrava
    //    2. roda de verdade -> na mini-tela ao lado, com os dados que
    //                          VOCE digitar na hora
    //
    //  Nao mexa nos nomes dos metodos nem no que esta entre parenteses.
    //  E por ali que o corretor encontra o seu codigo.
    //
    //  ATENCAO: todo metodo aqui DEVOLVE um valor com return. Escrever na
    //  tela com MessageBox nao conta - o corretor nao le a tela, ele le o
    //  que o metodo devolveu.
    // ===================================================================

    public static class Desafios
    {
        // -------------------------------------------------------------
        // 1 - A CONTA          (frmCadastro - Parte 6 da apostila)
        //
        // Crie um objeto Usuario com new, guarde nele o nome, o login e a
        // senha que chegaram, e devolva o objeto.
        //
        // O Id nao e problema seu: quem da o numero e o desafio 2.
        // -------------------------------------------------------------
        public static Usuario CriarUsuario(string nome, string login, string senha)
        {
            // TODO: crie o objeto com new, preencha os tres campos e devolva.
            return null;
        }

        // -------------------------------------------------------------
        // 2 - A LISTA          (UsuarioDAO.CriarConta - Parte 7)
        //
        // De ao usuario um Id igual a quantidade de contas MAIS UM,
        // acrescente ele na lista, e devolva o Id.
        //
        // A primeira conta tem que sair com Id 1, nao 0.
        // -------------------------------------------------------------
        public static int CriarConta(List<Usuario> contas, Usuario u)
        {
            // TODO: Count + 1 no Id, Add na lista, return do Id.
            return 0;
        }

        // -------------------------------------------------------------
        // 3 - ENTRAR           (UsuarioDAO.Autenticar - Parte 7)
        //
        // Percorra a lista com foreach e devolva a conta em que o login E
        // a senha batem com os que chegaram.
        // Se o foreach terminar sem achar nenhuma, devolva null.
        //
        // SAO DOIS return: um DENTRO do laco, com a conta achada, e um
        // DEPOIS do laco, com null.
        // -------------------------------------------------------------
        public static Usuario Autenticar(List<Usuario> contas, string login, string senha)
        {
            // TODO: foreach, if com == e &&, return da conta; null no fim.
            return null;
        }

        // -------------------------------------------------------------
        // 4 - JA EXISTE        (UsuarioDAO.LoginExiste - Parte 7)
        //
        // Percorra as contas e devolva verdadeiro se o login ja estiver em
        // uso. Se o foreach terminar sem achar, devolva falso.
        // -------------------------------------------------------------
        public static bool LoginExiste(List<Usuario> contas, string login)
        {
            // TODO: o true sai de dentro do laco; o false, so depois dele.
            return false;
        }

        // -------------------------------------------------------------
        // 5 - O PORTEIRO       (as validacoes do cadastro - Parte 5)
        //
        // Devolva a mensagem de erro do PRIMEIRO problema que encontrar,
        // nesta ordem. Se estiver tudo certo, devolva texto vazio "".
        //
        //   nome vazio            -> "Informe o seu nome."
        //   login vazio           -> "Informe o usuario."
        //   senha fora de 4 a 12  -> "A senha precisa ter de 4 a 12 caracteres."
        //   senha != confirmar    -> "As senhas nao conferem."
        //
        // A ORDEM IMPORTA: cada if devolve na hora e o metodo acaba ali.
        // -------------------------------------------------------------
        public static string ValidarCadastro(string nome, string login,
                                             string senha, string confirmar)
        {
            // TODO: quatro if, cada um com o seu return. "" no fim.
            return "";
        }

        // -------------------------------------------------------------
        // 6 - BUSCAR A CONTA   (UsuarioDAO.BuscarPorId - Parte 7)
        //
        // Percorra as contas e devolva aquela cujo Id for igual ao pedido.
        // Se nao achar, devolva null.
        //
        // Com este metodo o seu UsuarioDAO fica completo: sao os quatro.
        // -------------------------------------------------------------
        public static Usuario BuscarPorId(List<Usuario> contas, int id)
        {
            // TODO: igual ao 3, mas comparando o Id.
            return null;
        }

        // -------------------------------------------------------------
        // 7 - NUMERAR          (aqui o foreach NAO serve)
        //
        // Monte um texto com as contas numeradas, na ordem da lista:
        //   [Ana Souza, Bruno Lima]  ->  "1. Ana Souza | 2. Bruno Lima | "
        //   lista vazia              ->  "" (texto vazio)
        //
        // Repare no espaco, na barra e no espaco DEPOIS de cada nome,
        // inclusive do ultimo.
        //
        // Voce precisa saber em QUE POSICAO cada conta esta. O foreach nao
        // sabe dizer isso - use for, e lembre que a posicao 0 da lista e a
        // conta numero 1 na tela.
        // -------------------------------------------------------------
        public static string Numerar(List<Usuario> contas)
        {
            // TODO: for com indice, contas[i], e o numero i + 1.
            return "";
        }

        // -------------------------------------------------------------
        // 8 - SUGERIR LOGIN    (o mais dificil do dia)
        //
        // O login desejado esta ocupado? Sugira o mesmo com um numero no
        // fim, comecando em 2, ate achar um que esteja livre:
        //   "ana" ocupado           -> "ana2"
        //   "ana" e "ana2" ocupados -> "ana3"
        //   "carla" livre           -> "carla"
        //
        // Use WHILE: ninguem sabe quantas tentativas serao necessarias, e
        // e exatamente por isso que o for nao serve aqui.
        //
        // Para saber se um login esta ocupado, use o metodo que ja vem
        // pronto:   Ajuda.LoginEmUso(contas, tentativa)
        //
        // CUIDADO: se voce esquecer de mudar a tentativa DENTRO do while,
        // o programa trava e voce vai precisar fecha-lo pelo Gerenciador
        // de Tarefas. Salve o arquivo antes de rodar.
        // -------------------------------------------------------------
        public static string SugerirLogin(List<Usuario> contas, string desejado)
        {
            // TODO: guarde a tentativa e o numero antes; teste no while;
            //       mude a tentativa DENTRO do laco.
            return "";
        }

        // -------------------------------------------------------------
        // 9 - TROCAR A SENHA
        //
        // Ache a conta em que o login E a senha atual batem, troque a
        // senha dela pela nova, e devolva verdadeiro.
        //
        // Se nao achar - login errado OU senha atual errada - devolva
        // falso e nao mude nada.
        //
        // NAO precisa tirar da lista e por de volta. O objeto que o
        // foreach te entrega E o objeto que esta na lista: mudou nele,
        // mudou la dentro.
        // -------------------------------------------------------------
        public static bool TrocarSenha(List<Usuario> contas, string login,
                                       string atual, string nova)
        {
            // TODO: foreach, if com os dois campos, atribua a nova senha,
            //       return true. E o false depois do laco.
            return false;
        }

        // -------------------------------------------------------------
        // 10 - REMOVER A CONTA   (o mais dificil do dia)
        //
        // Ache a conta com aquele Id, tire ela da lista e devolva
        // verdadeiro. Se nao existir nenhuma com esse Id, devolva falso
        // e nao mexa na lista.
        //
        // Para tirar da lista existe:   contas.Remove(u)
        //
        // CUIDADO: mexer na lista enquanto o foreach a percorre estoura o
        // programa - InvalidOperationException, "Collection was modified".
        // Pense em como sair do laco na MESMA hora em que remover.
        // -------------------------------------------------------------
        public static bool Remover(List<Usuario> contas, int id)
        {
            // TODO: ache, remova, e saia do laco imediatamente.
            return false;
        }
    }
}
