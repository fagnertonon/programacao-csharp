using System.Collections.Generic;

namespace Conecta
{
    // ===================================================================
    //  PROVA - 21/08/2026
    //
    //  Sao sete metodos, contando o 0 (o seu nome). Cada um esta vazio,
    //  com um // TODO no lugar do codigo que falta. Escreva o codigo,
    //  salve, FECHE o programa, e rode de novo com F5.
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
        // 1 - CRIAR A CONTA
        //
        // Crie um objeto Usuario com new, guarde nele o id, o nome, o
        // login e a senha que chegaram, e devolva o objeto.
        //
        // Os quatro valores ja vem prontos entre parenteses: nao invente
        // nenhum texto entre aspas. Os campos do Usuario sao Id, Nome,
        // Login e Senha.
        // -------------------------------------------------------------
        public static Usuario CriarUsuario(int id, string nome,
                                           string login, string senha)
        {
            // TODO: um new, uma atribuicao por campo, e o return do objeto.
            return null;
        }

        // -------------------------------------------------------------
        // 2 - SENHAS FORTES
        //
        // Percorra as contas e devolva quantas tem senha FORTE. Senha
        // forte e a que tem 4 ou MAIS caracteres. Se a lista estiver
        // vazia, devolva 0.
        // -------------------------------------------------------------
        public static int ContarSenhasFortes(List<Usuario> contas)
        {
            // TODO: Aqui nao existe achou, existe contar.
            return 0;
        }

        // -------------------------------------------------------------
        // 3 - O MENU
        //
        // O menu do Conecta recebe o numero que o usuario escolheu e
        // devolve o nome da tela que deve abrir. Use switch: 1 devolve
        // Novo cadastro, 2 devolve Fazer login, 3 devolve Ver o mural e
        // 4 devolve Encerrar. Qualquer outra coisa devolve a frase
        // Opcao inexistente. com o ponto final incluido.
        // -------------------------------------------------------------
        public static string MenuPrincipal(string opcao)
        {
            // TODO: Sao quatro case e um default.
            return "";
        }

        // -------------------------------------------------------------
        // 4 - PARTICIPANTES
        //
        // Monte a linha de participantes do mural com os NOMES das
        // contas, mas do ULTIMO para o PRIMEIRO da lista, separados por
        // espaco hifen espaco. Tres contas na ordem Ana, Bruno e Carla
        // viram o texto Carla - Bruno - Ana. O separador so aparece
        // ENTRE dois nomes: nao sobra no fim. Lista vazia devolve texto
        // vazio.
        // -------------------------------------------------------------
        public static string LinhaDeParticipantes(List<Usuario> contas)
        {
            // TODO: Comece com um texto vazio e va grudando um nome por volta.
            return "";
        }

        // -------------------------------------------------------------
        // 5 - QUANTOS NA FRENTE
        //
        // Descubra QUANTAS contas estao a frente do login procurado na
        // fila. Se ele for o primeiro da lista, nao ha ninguem na
        // frente: devolva 0. Se for o segundo, devolva 1, e assim por
        // diante. Se nenhuma conta tiver esse login, devolva -1.
        // -------------------------------------------------------------
        public static int QuantosNaFrente(List<Usuario> contas,
                                          string login)
        {
            // TODO: O foreach nao serve aqui: ele nao sabe dizer em que
            // posicao voce esta.
            return 0;
        }

        // -------------------------------------------------------------
        // 6 - NOME MENOR
        //
        // Devolva o Nome mais CURTO da lista, ou seja, o que tem menos
        // caracteres. Se dois nomes empatarem no tamanho, fica o que
        // aparecer primeiro na lista. Se a lista estiver vazia, devolva
        // texto vazio.
        // -------------------------------------------------------------
        public static string MenorNome(List<Usuario> contas)
        {
            // TODO: Antes do laco, guarde o campeao numa variavel.
            return "";
        }
    }
}
