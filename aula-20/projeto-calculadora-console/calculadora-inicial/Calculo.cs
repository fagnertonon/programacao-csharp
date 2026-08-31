using System;

namespace Calculadora
{
    /// <summary>
    /// SO CONTA. Treze metodos moram aqui.
    ///
    /// A REGRA DESTE ARQUIVO, e ela vale ate as 22h:
    ///
    ///     procure por C-o-n-s-o-l-e neste arquivo. Voce nao pode achar.
    ///
    /// Nenhum metodo daqui le do teclado e nenhum escreve na tela. Todos
    /// RECEBEM valores por parametro e DEVOLVEM o resultado com return.
    ///
    /// Precisou escrever a palavra do teclado e da tela aqui dentro? Entao
    /// o metodo esta no arquivo errado - ele e do Entrada ou do Tela.
    /// </summary>
    public class Calculo
    {
        // =================================================================
        //  GRAU 1 - a assinatura ja esta escrita. Voce escreve so o corpo.
        // =================================================================

        // -----------------------------------------------------------------
        // TODO 1 - devolva a soma de a com b. Uma linha.
        // -----------------------------------------------------------------
        public static double Somar(double a, double b)
        {
            return 0;   // <<< APAGUE esta linha e escreva a sua
        }

        // -----------------------------------------------------------------
        // TODO 2 - devolva a de menos b.
        // -----------------------------------------------------------------
        public static double Subtrair(double a, double b)
        {
            return 0;   // <<< APAGUE esta linha
        }

        // -----------------------------------------------------------------
        // TODO 3 - devolva a vezes b.
        // -----------------------------------------------------------------
        public static double Multiplicar(double a, double b)
        {
            return 0;   // <<< APAGUE esta linha
        }

        // -----------------------------------------------------------------
        // TODO 4 - devolva verdadeiro quando DA para dividir por b.
        //
        // Repare no tipo devolvido: bool, e nao double. A pergunta "da para
        // dividir?" tem resposta sim ou nao. Guarde esta pergunta para o
        // quadro: por que este devolve bool e o de baixo devolve double?
        // -----------------------------------------------------------------
        public static bool PodeDividir(double b)
        {
            return false;   // <<< APAGUE esta linha
        }

        // -----------------------------------------------------------------
        // TODO 5 - devolva a dividido por b.
        //
        // ATENCAO: este e o primeiro metodo que CHAMA outro metodo seu.
        // Antes de dividir, pergunte ao PodeDividir se pode. Se nao puder,
        // devolva 0 e pronto - sem quebrar o programa.
        // -----------------------------------------------------------------
        public static double Dividir(double a, double b)
        {
            return 0;   // <<< APAGUE esta linha
        }

        // -----------------------------------------------------------------
        // TODO 6 - devolva verdadeiro quando n for par.
        //
        // Dica: o operador % devolve o RESTO da divisao.
        // -----------------------------------------------------------------
        public static bool EhPar(int n)
        {
            return false;   // <<< APAGUE esta linha
        }

        // =================================================================
        //  GRAU 2 - agora nao vem assinatura nenhuma. Voce escreve o metodo
        //           INTEIRO, do "public static" ao ultimo fecha-chaves.
        //
        //  Antes de digitar cada um, responda as tres perguntas da Aula 17:
        //     1. O que ele DEVOLVE?  ->  o tipo, ANTES do nome
        //     2. O que ele RECEBE?   ->  os parametros, dentro dos ( )
        //     3. O que ele FAZ?      ->  o corpo, entre as { }
        // =================================================================

        // -----------------------------------------------------------------
        // TODO 11 - Media          *** O MOMENTO DA AULA ***
        //
        // Um metodo chamado Media, que receba dois numeros com virgula e
        // devolva a media dos dois.
        //
        // Ele NAO soma e NAO divide. Ele pede para o seu Somar somar, e
        // pede para o seu Dividir dividir. Dois metodos seus, chamados por
        // um terceiro metodo seu.
        // -----------------------------------------------------------------


        // -----------------------------------------------------------------
        // TODO 12 - Porcentagem
        //
        // Recebe um valor e um percentual, e devolve quanto e esse
        // percentual do valor. 15 por cento de 200 tem que dar 30.
        // -----------------------------------------------------------------


        // -----------------------------------------------------------------
        // TODO 13 - Maior
        //
        // Recebe dois numeros e devolve o maior dos dois.
        //
        // Pense no caso dos dois iguais ANTES de escrever: 5 e 5 devolve o
        // que? Um if com return nos dois ramos resolve.
        // -----------------------------------------------------------------


        // -----------------------------------------------------------------
        // TODO 14 - Menor
        //
        // O irmao do Maior, ao contrario.
        // -----------------------------------------------------------------


        // -----------------------------------------------------------------
        // TODO 15 - Potencia
        //
        // Recebe um numero e um expoente INTEIRO, e devolve o numero
        // elevado a esse expoente. 2 elevado a 5 da 32.
        //
        // Molde da Aula 10: acumulador declarado ANTES do for, e devolvido
        // DEPOIS dele. Comece o acumulador em 1, e nao em 0 - pense no
        // porque.
        //
        // Expoente 0 tem que dar 1. Confira se o seu da.
        // -----------------------------------------------------------------


        // -----------------------------------------------------------------
        // TODO 16 - Fatorial
        //
        // Recebe um inteiro n e devolve o fatorial dele.
        // Fatorial de 5 = 5 x 4 x 3 x 2 x 1 = 120.
        //
        // Mesmo molde do Potencia. E fatorial de 0 da 1 - se o seu for
        // comecar no 2, isso se resolve sozinho.
        // -----------------------------------------------------------------


        // =================================================================
        //  GRAU 3 - so o problema. Nem assinatura, nem dica de estrutura.
        // =================================================================

        // -----------------------------------------------------------------
        // TODO 20                  *** A PROVA DA NOITE ***
        //
        // Cinco opcoes do menu - somar, subtrair, multiplicar, dividir e
        // porcentagem - fazem a mesma coisa: pegam dois numeros e um sinal.
        //
        // Escreva UM metodo, chamado Calcular, que receba os dois numeros e
        // o sinal ("+", "-", "*", "/" ou "%") e devolva o resultado certo.
        //
        // Ele nao faz conta nenhuma. Ele so ESCOLHE qual dos seus metodos
        // chamar. A palavra que faz essa escolha voce ja usou na Aula 9.
        //
        // Se voce sentir vontade de escrever a palavra do teclado e da tela
        // aqui dentro, pare: o metodo esta certo, o lugar e que esta errado.
        // -----------------------------------------------------------------

    }
}
