using System;
using System.Collections.Generic;

namespace Portaria
{
    /// <summary>
    /// As regras da Portaria.
    ///
    ///     A REGRA DO QUADRO:
    ///     O Regras.cs nao abre banco e nao desenha tela.
    ///
    /// Procure aqui dentro pelo nome da biblioteca do MySQL e pelo do
    /// Windows Forms: tem que dar ZERO nos dois. Todo metodo daqui
    /// RECEBE valores e DEVOLVE resposta - nada mais.
    ///
    /// E isso que torna estas regras conferiveis com o MySQL desligado.
    /// </summary>
    public class Regras
    {
        /// <summary>Tamanho minimo do login.</summary>
        public const int TAMANHO_MINIMO_LOGIN = 3;

        /// <summary>Tamanho minimo da senha.</summary>
        public const int TAMANHO_MINIMO_SENHA = 4;

        /// <summary>
        /// TODO 3 - O login serve?                 [Indicadores I2 e I3]
        ///
        /// Devolve true SO quando as tres condicoes valem:
        ///   1. tem texto de verdade (nao e nulo, vazio nem so espacos)
        ///   2. nao tem espaco no meio
        ///   3. tem TAMANHO_MINIMO_LOGIN caracteres ou mais
        ///
        /// DICAS:
        ///   string.IsNullOrWhiteSpace(login)  responde a condicao 1 -
        ///     mas ao CONTRARIO do que voce quer (ele diz se esta vazio)
        ///   login.Contains(" ")               responde a condicao 2
        ///   login.Length                      responde a condicao 3
        ///
        /// Use a constante la de cima, nao o numero 3 solto.
        /// </summary>
        public static bool ValidarLogin(string login)
        {
            return true;   // <<< TROQUE ESTA LINHA pela sua
        }

        /// <summary>
        /// TODO 11 - Quem foi o ultimo a se cadastrar.
        ///                                    [Indicadores I2, I3 e I4]
        ///
        /// Percorra a lista com FOREACH guardando o de maior Id - o
        /// mesmo padrao "maior item" da Aula 13.
        ///
        /// Comece supondo que o ultimo e o primeiro da lista, e va
        /// trocando sempre que achar alguem com Id maior.
        ///
        /// Repare que a lista pode vir ordenada por NOME - por isso nao
        /// adianta pegar o primeiro nem o ultimo item e pronto.
        ///
        /// ATENCAO a lista vazia: nao da para pegar lista[0] de uma
        /// lista sem ninguem. Devolva null - a tela ja sabe lidar.
        /// </summary>
        public static Usuario UltimoCadastrado(List<Usuario> lista)
        {
            return null;   // <<< TROQUE ESTA LINHA pela sua
        }

        /// <summary>
        /// So o primeiro nome, para a saudacao do alto da tela principal
        /// (o lblConectado, nao o rodape).
        ///
        /// ESTE METODO JA VEM ESCRITO - E ELE ESTA COM DEFEITO.
        /// E a caca ao defeito da segunda noite. Nao mexa nele antes da
        /// hora: primeiro voce vai VER o erro acontecer na tela, depois
        /// vai achar o motivo com o breakpoint.
        /// </summary>
        public static string PrimeiroNome(string nomeCompleto)
        {
            if (string.IsNullOrWhiteSpace(nomeCompleto))
            {
                return "";
            }

            int espaco = nomeCompleto.IndexOf(' ');

            if (espaco < 0)
            {
                return nomeCompleto;      // nome de uma palavra so
            }

            return nomeCompleto.Substring(espaco);
        }
    }
}
