using System;

namespace Calculadora
{
    /// <summary>
    /// SO LE DO TECLADO E VALIDA. Quatro metodos moram aqui.
    ///
    /// Nenhum metodo daqui faz conta. Eles devolvem um valor CONFIAVEL
    /// para quem chamou: quando sai daqui, ja e numero de verdade.
    ///
    /// Este arquivo comeca VAZIO de proposito. Os quatro metodos sao seus,
    /// do "public static" ao ultimo fecha-chaves.
    /// </summary>
    public class Entrada
    {
        // =================================================================
        //  GRAU 2 - voce escreve o metodo inteiro.
        // =================================================================

        // -----------------------------------------------------------------
        // TODO 17 - LerNumero
        //
        // Recebe a mensagem que vai aparecer na tela ("Primeiro numero: ")
        // e devolve um numero com virgula, digitado pela pessoa.
        //
        // O metodo INSISTE: enquanto o que for digitado nao virar numero,
        // ele avisa o erro e pergunta de novo. So sai do while quando deu
        // certo.
        //
        // double.TryParse faz duas coisas na mesma linha: TENTA converter e
        // DIZ se conseguiu. E por isso que ele cabe dentro do while.
        //
        // Para avisar o erro, chame o MostrarErro que voce escreveu no
        // Tela.cs - nao escreva a mensagem na mao aqui.
        // -----------------------------------------------------------------


        // -----------------------------------------------------------------
        // TODO 18 - LerInteiro
        //
        // O mesmo do LerNumero, trocando numero com virgula por numero
        // inteiro. Duas palavras mudam no corpo inteiro. Descubra quais.
        // -----------------------------------------------------------------


        // =================================================================
        //  GRAU 3 - so o problema.
        // =================================================================

        // -----------------------------------------------------------------
        // TODO 21 - LerOpcao
        //
        // Le a opcao do menu e so aceita de 0 a 9. Digitou 12? Avisa e
        // pergunta de novo.
        //
        // Cuidado com a preguica: NAO copie o LerInteiro para dentro dele. O
        // LerOpcao CHAMA o LerInteiro e so acrescenta a regra do intervalo.
        // Copiar codigo que ja existe e o erro que esta aula existe para
        // tirar de voce.
        // -----------------------------------------------------------------


        // -----------------------------------------------------------------
        // TODO 22 - Confirmar
        //
        // Recebe uma pergunta ("Deseja mesmo sair?"), mostra ela com
        // " (S/N): " no fim, le a resposta e devolve verdadeiro ou falso.
        //
        // Responder "s" minusculo tem que funcionar igual a "S".
        // -----------------------------------------------------------------

    }
}
