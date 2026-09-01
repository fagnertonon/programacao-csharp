using System;

namespace Masmorra
{
    /// <summary>
    /// O CEREBRO DO JOGO. ESTE E O SEU ARQUIVO - o unico que voce abre hoje.
    ///
    /// A REGRA DA NOITE: procure por C-o-n-s-o-l-e e por
    /// S-y-s-t-e-m-.-W-i-n-d-o-w-s-.-F-o-r-m-s aqui dentro. Voce nao vai
    /// achar, e e de proposito. Nenhum metodo daqui desenha nada e nenhum
    /// le teclado. Todos RECEBEM valores e DEVOLVEM resposta.
    ///
    /// Quem desenha e quem le o teclado e o frmJogo.cs, que ja esta pronto
    /// e nao se toca. Ele PERGUNTA para os metodos que voce vai escrever.
    ///
    /// Sao 10 TODO, em ordem. Procure por TODO:
    ///   Exibir > Lista de Tarefas, ou Ctrl+F.
    ///
    /// APERTE F5 A CADA DOIS OU TRES. O jogo tem que continuar abrindo o
    /// tempo inteiro.
    /// </summary>
    public class Jogo
    {
        // O tamanho do mapa. Ja esta pronto - o PodeAndar usa estes dois.
        public const int COLUNAS = 12;
        public const int LINHAS = 8;

        // =================================================================
        //  TODO 1 - PodeAndar          *** O PRIMEIRO F5 QUE MUDA A TELA ***
        //
        //  Devolva verdadeiro quando a casa (coluna, linha) EXISTE no mapa.
        //
        //  O mapa tem COLUNAS colunas e LINHAS linhas, e a contagem comeca
        //  no ZERO. Entao a primeira coluna e a 0 e a ultima e a COLUNAS-1.
        //
        //  Sao quatro comparacoes ligadas por &&: nao pode ser negativa, e
        //  nao pode passar do fim - nas duas direcoes.
        //
        //  Enquanto isto devolver false, o heroi nao sai do canto. Escreva,
        //  aperte F5 e ande com as setas: e a primeira coisa que funciona.
        // =================================================================
        public static bool PodeAndar(int coluna, int linha)
        {
            return false;   // <<< APAGUE esta linha e escreva a sua
        }

        // =================================================================
        //  TODO 2 - CalcularDano       *** A GUARDA QUE FAZ A LUTA ACABAR ***
        //
        //  Quanto um golpe tira: a forca de quem bate menos a defesa de
        //  quem apanha.
        //
        //  MAS NUNCA MENOS QUE 1. Um monstro de defesa alta poderia zerar
        //  o dano - e um golpe que tira zero deixa a luta empatada PARA
        //  SEMPRE. Guarde esta frase para o Lutar: e esta linha que faz
        //  o while daqui a pouco terminar.
        // =================================================================
        public static int CalcularDano(int forca, int defesa)
        {
            return 0;   // <<< APAGUE esta linha
        }

        // =================================================================
        //  TODO 3 - Bater
        //
        //  A vida que sobra depois de levar o golpe: vida menos dano.
        //  Nunca abaixo de ZERO - ninguem tem vida negativa.
        // =================================================================
        public static int Bater(int vida, int dano)
        {
            return vida;   // <<< APAGUE esta linha
        }

        // =================================================================
        //  TODO 4 - EstaVivo
        //
        //  Uma comparacao so. Devolve bool, e nao int: a pergunta "esta
        //  vivo?" tem resposta sim ou nao.
        // =================================================================
        public static bool EstaVivo(int vida)
        {
            return true;   // <<< APAGUE esta linha
        }

        // =================================================================
        //  TODO 5 - Lutar                    *** O WHILE DA NOITE ***
        //
        //  A luta inteira acontece aqui dentro, de uma vez, e o metodo
        //  devolve o RELATO do que aconteceu - o texto que aparece no log.
        //
        //  ENQUANTO os dois estiverem vivos:
        //     1. voce bate no monstro   (CalcularDano com a SUA forca e a
        //        defesa DELE, depois Bater na vida dele)
        //     2. se o monstro caiu, a luta acabou: devolva o relato
        //     3. o monstro bate em voce (a forca DELE, a sua defesa)
        //
        //  Monte o relato somando texto:
        //     relato = relato + "Voce bate: -" + dano + ". ";
        //
        //  POR QUE while E NAO for: ninguem sabe quantas voltas a luta vai
        //  dar. Depende da forca dos dois. E exatamente para isso que o
        //  while existe - o for serve quando voce SABE o numero de voltas.
        //
        //  ⚠ SE VOCE ERRAR A CONDICAO, o jogo espera 2 segundos, desiste e
        //  te avisa no log. Ele nao trava - mas a luta nao acontece.
        //  Confira se a vida de alguem realmente DIMINUI dentro do laco.
        // =================================================================
        public static string Lutar(Personagem heroi, Personagem monstro)
        {
            return "";   // <<< APAGUE esta linha
        }

        // =================================================================
        //  TODO 6 - CalcularNivel            *** O SEGUNDO WHILE ***
        //
        //  Devolve o nivel do heroi a partir do XP que ele tem.
        //
        //  O nivel 1 e de graca. O nivel 2 custa 10 de XP; o 3 custa mais
        //  20; o 4 custa mais 30 - cada nivel custa 10 a mais que o anterior.
        //
        //  ENQUANTO o XP der para pagar o proximo nivel:
        //     tire o custo do XP, suba um nivel, e aumente o custo em 10.
        //
        //  Repare que aqui e o CONTRARIO do Lutar: o laco nao para porque
        //  alguem morreu, para porque o XP acabou. E o mesmo while.
        //
        //  Confira: 0 de XP da nivel 1. 10 de XP da nivel 2. 30 da nivel 3.
        // =================================================================
        public static int CalcularNivel(int xp)
        {
            return 1;   // <<< APAGUE esta linha
        }

        // =================================================================
        //  TODO 7 - VidaMaximaDoNivel
        //
        //  20, mais 5 para cada nivel ALEM do primeiro.
//  Nivel 1 da 20, nivel 2 da 25, nivel 3 da 30. Uma linha.
//
//  Cuidado com o nivel 1: se ele devolver mais que 20, o heroi 'sobe
//  de nivel' assim que o jogo abre, sem ter matado ninguem.
        // =================================================================
        public static int VidaMaximaDoNivel(int nivel)
        {
            return 0;   // <<< APAGUE esta linha
        }

        // =================================================================
        //  TODO 8 - ForcaDoNivel
        //
        //  5, mais 2 para cada nivel ALEM do primeiro.
//  Nivel 1 da 5, nivel 2 da 7, nivel 3 da 9. Uma linha.
        // =================================================================
        public static int ForcaDoNivel(int nivel)
        {
            return 0;   // <<< APAGUE esta linha
        }

        // =================================================================
        //  TODO 9 - BarraDeVida              *** O TERCEIRO WHILE ***
        //
        //  Devolve a barra que aparece no painel, com DEZ tracinhos:
        //
        //     vida cheia   -> ##########
        //     metade       -> #####.....
        //     quase morto  -> #.........
        //     morto        -> ..........
        //
        //  Primeiro descubra quantos cheios: vida * 10 / vidaMaxima.
        //  Depois monte o texto num laco de dez voltas, somando "#"
        //  enquanto couber e "." depois disso.
        //
        //  ⚠ GUARDA ANTES DE DIVIDIR: se vidaMaxima for zero, a divisao
        //  quebra o programa. Devolva os dez pontos e saia, antes de dividir.
        //
        //  Este e o unico dos tres while da noite que TAMBEM sairia com um
        //  for - porque aqui voce sabe o numero de voltas: dez. Vale a
        //  pergunta: por que o do Lutar nao sairia?
        // =================================================================
        public static string BarraDeVida(int vida, int vidaMaxima)
        {
            return "";   // <<< APAGUE esta linha
        }

        // =================================================================
        //  TODO 10 - Situacao
        //
        //  Uma palavra sobre como o heroi esta, a partir da porcentagem de
        //  vida que sobrou:
        //
        //     mais de 60%          -> "Situacao: bem"
        //     de 30% ate 60%       -> "Situacao: ferido"
        //     acima de 0 ate 30%   -> "Situacao: quase morrendo"
        //     zero                 -> "Situacao: caiu"
        //
        //  Uma cadeia de if / else if, de cima para baixo. A ordem importa.
        //
        //  ⚠ A mesma guarda do BarraDeVida: vidaMaxima zero nao pode chegar na
        //  divisao.
        // =================================================================
        public static string Situacao(int vida, int vidaMaxima)
        {
            return "";   // <<< APAGUE esta linha
        }
    }
}
