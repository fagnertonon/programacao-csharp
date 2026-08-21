using System.Collections.Generic;

namespace Conecta
{
    // ===================================================================
    //  PROVA - 21/08/2026
    //
    //  Sao seis metodos, contando o 0 (o seu nome). Cada um esta vazio,
    //  com um // TODO no lugar do
    //  codigo que falta. Escreva o codigo, salve, FECHE o programa, e
    //  rode de novo com F5.
    //
    //  O aplicativo testa sozinho o que voce escreveu e mostra, teste a
    //  teste, o que era esperado e o que o seu metodo devolveu.
    //
    //  Nao mexa nos nomes dos metodos nem no que esta entre parenteses.
    //  E por ali que o corretor encontra o seu codigo.
    // ===================================================================

    public static class Desafios
    {
        // -------------------------------------------------------------
        // 0 - QUEM E VOCE
        //
        // Devolva o seu nome completo, com nome e sobrenome. E so
        // escrever entre as aspas.
        //
        // Sem este resolvido o botao Enviar nao funciona: o professor
        // nao teria como saber de quem sao as respostas.
        // -------------------------------------------------------------
        public static string MeuNome()
        {
            // TODO: escreva o seu nome completo entre as aspas.
            return "";
        }

        // -------------------------------------------------------------
        // 1 - SENHAS CURTAS
        //
        // Percorra as contas e devolva quantas tem senha curta. Senha
        // curta e a que tem MENOS de 4 caracteres, e senha em branco
        // tambem conta como curta. Se a lista estiver vazia, devolva 0.
        // -------------------------------------------------------------
        public static int ContarSenhasCurtas(List<Usuario> contas)
        {
            // TODO: Aqui nao existe achou, existe contar.
            return 0;
        }

        // -------------------------------------------------------------
        // 2 - O MENU
        //
        // O menu do Conecta recebe o numero que o usuario escolheu e
        // devolve o nome da tela que deve abrir. Use switch: 1 devolve
        // Cadastrar conta, 2 devolve Entrar, 3 devolve Mural e 4 devolve
        // Sair. Qualquer outra coisa devolve a frase Opcao invalida. com
        // o ponto final incluido.
        // -------------------------------------------------------------
        public static string MenuPrincipal(string opcao)
        {
            // TODO: Sao quatro case e um default.
            return "";
        }

        // -------------------------------------------------------------
        // 3 - PARTICIPANTES
        //
        // Monte a linha de participantes do mural: os NOMES das contas,
        // na ordem da lista, separados por espaco hifen espaco. Tres
        // contas chamadas Ana Bruno e Carla viram o texto Ana - Bruno -
        // Carla. O separador so aparece ENTRE dois nomes: o texto nao
        // comeca com ele e nao termina com ele. Lista vazia devolve
        // texto vazio.
        // -------------------------------------------------------------
        public static string LinhaDeParticipantes(List<Usuario> contas)
        {
            // TODO: Comece com um texto vazio e va grudando um nome por volta.
            return "";
        }

        // -------------------------------------------------------------
        // 4 - O LUGAR NA FILA
        //
        // Descubra em que linha da lista esta o login procurado. A
        // primeira linha da lista e a numero 1, a segunda e a numero 2,
        // e assim por diante. Se nenhuma conta tiver esse login, devolva
        // 0.
        // -------------------------------------------------------------
        public static int LugarNaFila(List<Usuario> contas,
                                      string login)
        {
            // TODO: O foreach nao serve aqui: ele nao sabe dizer em que
            // posicao voce esta.
            return 0;
        }

        // -------------------------------------------------------------
        // 5 - NOME MAIOR
        //
        // Devolva o Nome mais comprido da lista, ou seja, o que tem mais
        // caracteres. Se dois nomes empatarem no tamanho, fica o que
        // aparecer primeiro na lista. Se a lista estiver vazia, devolva
        // texto vazio.
        // -------------------------------------------------------------
        public static string MaiorNome(List<Usuario> contas)
        {
            // TODO: Antes do laco, guarde o campeao numa variavel.
            return "";
        }
    }
}
