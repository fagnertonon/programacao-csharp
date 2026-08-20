using System.Collections.Generic;

namespace Conecta
{
    // ===================================================================
    //  SIMULADO DA PROVA
    //
    //  Sao onze metodos, contando o 0 (o seu nome). Cada um esta vazio,
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
        // 1 - SENHAS FRACAS
        //
        // Percorra as contas e devolva QUANTAS tem senha com menos
        // caracteres do que o minimo que chegou. Nao existe achar aqui:
        // o metodo tem que olhar ate a ultima conta antes de responder.
        // Lista vazia devolve zero.
        // -------------------------------------------------------------
        public static int ContarSenhasFracas(List<Usuario> contas,
                                             int minimo)
        {
            // TODO: Crie a variavel do total ANTES do foreach, valendo
            // zero, e faca ela crescer dentro do laco quando o if for
            // verdadeiro.
            return 0;
        }

        // -------------------------------------------------------------
        // 2 - O ATALHO
        //
        // O Conecta recebe o nome da tecla que o usuario apertou e
        // devolve a tela que deve abrir. Use switch: F2 devolve Nova
        // conta, F3 devolve Procurar, F4 devolve Mural e F8 devolve Sair
        // do Conecta. Qualquer outra tecla devolve a frase Atalho sem
        // uso. com o ponto final incluido.
        // -------------------------------------------------------------
        public static string TelaDoAtalho(string tecla)
        {
            // TODO: Sao quatro case e um default.
            return "";
        }

        // -------------------------------------------------------------
        // 3 - A MEDIA
        //
        // Some o tamanho de todas as senhas e devolva a media, dividindo
        // pela quantidade de contas. Como o retorno e int, o resto da
        // divisao e descartado. Lista vazia devolve zero - e essa
        // conferencia precisa vir ANTES da divisao.
        // -------------------------------------------------------------
        public static int MediaTamanhoSenha(List<Usuario> contas)
        {
            // TODO: Sao duas coisas em ordem: primeiro barre a lista
            // vazia, depois some e divida.
            return 0;
        }

        // -------------------------------------------------------------
        // 4 - EM UMA LINHA
        //
        // Monte uma linha unica com os logins das contas, na ordem da
        // lista, separados por espaco barra espaco: os logins ana e
        // bruno viram o texto ana / bruno. Nao pode sobrar separador
        // depois do ultimo nem antes do primeiro. Lista vazia devolve
        // texto vazio.
        // -------------------------------------------------------------
        public static string ListarLogins(List<Usuario> contas)
        {
            // TODO: Comece com um texto vazio e va grudando um login por
            // volta.
            return "";
        }

        // -------------------------------------------------------------
        // 5 - O MAIOR NOME
        //
        // Percorra as contas e devolva aquela cujo NOME tem mais
        // caracteres. Se duas empatarem no tamanho, devolva a primeira
        // das duas. Lista vazia devolve null.
        // -------------------------------------------------------------
        public static Usuario NomeMaisLongo(List<Usuario> contas)
        {
            // TODO: Antes do laco, crie uma variavel Usuario valendo
            // null para segurar o campeao.
            return null;
        }

        // -------------------------------------------------------------
        // 6 - QUANTAS CONTAS
        //
        // Devolva a frase que a barra de status do Conecta mostra. Lista
        // vazia devolve Nenhuma conta cadastrada. Uma conta devolve 1
        // conta cadastrada. Duas ou mais devolvem o numero seguido de um
        // espaco e da palavra contas cadastradas. As tres frases
        // terminam com ponto final.
        // -------------------------------------------------------------
        public static string ResumoDaTurma(List<Usuario> contas)
        {
            // TODO: Use contas.Count e uma cadeia de if.
            return "";
        }

        // -------------------------------------------------------------
        // 7 - AO CONTRARIO
        //
        // No mural, a conta que chegou por ultimo aparece em cima. Monte
        // o texto com os nomes na ordem inversa da lista, comecando pela
        // ultima conta. Cada nome vem seguido de espaco, sinal de maior
        // e espaco - inclusive o ultimo, igual ao Numerar de ontem.
        // Lista vazia devolve texto vazio.
        // -------------------------------------------------------------
        public static string Inverter(List<Usuario> contas)
        {
            // TODO: E um for com as tres partes trocadas de lado: comece
            // na ultima posicao, ande para tras com i-- e continue
            // enquanto ainda houver posicao valida.
            return "";
        }

        // -------------------------------------------------------------
        // 8 - O VIZINHO
        //
        // A lista chegou de uma importacao e pode ter vindo com duas
        // contas SEGUIDAS usando o mesmo login. Percorra com indice e
        // devolva o primeiro login que for igual ao da conta da posicao
        // anterior. Login repetido longe nao vale: so conta quem esta
        // colado. Se nao houver nenhum, devolva texto vazio.
        // -------------------------------------------------------------
        public static string LoginIgualAoAnterior(List<Usuario> contas)
        {
            // TODO: Aqui o foreach nao serve: voce precisa de duas
            // contas ao mesmo tempo, a de agora e a de tras.
            return "";
        }

        // -------------------------------------------------------------
        // 9 - A POSICAO NA FILA
        //
        // Descubra em que lugar da fila esta a conta daquele login,
        // contando a partir de 1: a primeira da lista e a posicao 1. Se
        // o login nao estiver na lista, devolva 0. Use while, e faca o
        // laco parar por dois motivos: a lista acabou, ou voce ja achou.
        // -------------------------------------------------------------
        public static int PosicaoDoLogin(List<Usuario> contas,
                                         string login)
        {
            // TODO: Antes do laco: um indice comecando em 0 e a resposta
            // comecando em 0.
            return 0;
        }

        // -------------------------------------------------------------
        // 10 - IMPORTAR
        //
        // Chegou uma remessa de contas novas para entrar no Conecta.
        // Percorra a lista chegando e, para cada conta cujo login ainda
        // nao estiver em uso, de a ela o proximo Id (a quantidade de
        // contas mais um), acrescente na lista contas e conte. Devolva
        // quantas entraram de verdade. Para saber se um login esta
        // ocupado use Ajuda.LoginEmUso(contas, login).
        // -------------------------------------------------------------
        public static int Importar(List<Usuario> contas,
                                   List<Usuario> chegando)
        {
            // TODO: O laco percorre chegando, mas quem cresce e contas:
            // sao duas listas com papeis diferentes.
            return 0;
        }
    }
}
