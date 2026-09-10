using System;

namespace Acesso
{
    /// <summary>
    /// SISTEMA DE LOGIN - desafios 1 a 10.
    ///
    /// Os dez metodos ja tem NOME, PARAMETROS e TIPO DE RETORNO. O que falta
    /// e o corpo, e e voce quem escreve.
    ///
    /// A ORDEM DA NOITE, e ela nao muda:
    ///
    ///   1. leia o enunciado na aba do desafio
    ///   2. escreva o TESTE no AcessoTests.cs, cobrindo as fronteiras que a
    ///      aba exige
    ///   3. escreva o CORPO do metodo aqui
    ///   4. F5 e confira: a aba fecha quando as QUATRO perguntas dao verde
    ///
    /// A aba seguinte so destrava quando a anterior fecha.
    ///
    /// UC11 - Aulas 8 e 9.
    /// </summary>
    public class Login
    {
        // ---------------------------------------------------------------
        // DESAFIO 1 - o usuario tem de 4 a 20 caracteres, incluindo os dois.
        // ---------------------------------------------------------------
        public static bool UsuarioValido(string usuario)
        {
            throw new NotImplementedException("Desafio 1: escreva o corpo.");
        }

        // ---------------------------------------------------------------
        // DESAFIO 2 - a senha tem no minimo 8 caracteres.
        //
        // Cuidado com a diferenca entre "no minimo 8" e "mais que 8".
        // ---------------------------------------------------------------
        public static bool SenhaTemTamanhoMinimo(string senha)
        {
            throw new NotImplementedException("Desafio 2: escreva o corpo.");
        }

        // ---------------------------------------------------------------
        // DESAFIO 3 - a senha tem pelo menos um digito.
        // ---------------------------------------------------------------
        public static bool SenhaTemNumero(string senha)
        {
            throw new NotImplementedException("Desafio 3: escreva o corpo.");
        }

        // ---------------------------------------------------------------
        // DESAFIO 4 - a senha tem pelo menos uma letra maiuscula.
        // ---------------------------------------------------------------
        public static bool SenhaTemMaiuscula(string senha)
        {
            throw new NotImplementedException("Desafio 4: escreva o corpo.");
        }

        // ---------------------------------------------------------------
        // DESAFIO 5 - um ponto para cada desafio 2, 3 e 4 cumprido.
        // Vai de 0 a 3.
        //
        // Este metodo nao conta nada sozinho: ele PERGUNTA para os tres de
        // cima. Se um deles estiver errado, este erra junto.
        // ---------------------------------------------------------------
        public static int ForcaDaSenha(string senha)
        {
            throw new NotImplementedException("Desafio 5: escreva o corpo.");
        }

        // ---------------------------------------------------------------
        // DESAFIO 6 - a senha e aceita a partir da forca 2.
        // ---------------------------------------------------------------
        public static bool SenhaAceita(string senha)
        {
            throw new NotImplementedException("Desafio 6: escreva o corpo.");
        }

        // ---------------------------------------------------------------
        // DESAFIO 7 - o usuario NAO diferencia maiuscula de minuscula.
        // A senha, essa sim, diferencia.
        //
        // Sao duas regras diferentes no mesmo metodo. Um teste que so usa
        // "ana" e "Senha1" fica verde com as duas erradas.
        // ---------------------------------------------------------------
        public static bool Autenticar(string usuario, string senha,
                                      string usuarioGravado, string senhaGravada)
        {
            throw new NotImplementedException("Desafio 7: escreva o corpo.");
        }

        // ---------------------------------------------------------------
        // DESAFIO 8 - sao 3 tentativas. O resultado nunca e negativo.
        // ---------------------------------------------------------------
        public static int TentativasRestantes(int tentativas)
        {
            throw new NotImplementedException("Desafio 8: escreva o corpo.");
        }

        // ---------------------------------------------------------------
        // DESAFIO 9 - bloqueia quando nao resta nenhuma tentativa.
        // ---------------------------------------------------------------
        public static bool ContaBloqueada(int tentativas)
        {
            throw new NotImplementedException("Desafio 9: escreva o corpo.");
        }

        // ---------------------------------------------------------------
        // DESAFIO 10 - diz POR QUE o login foi recusado. O mais forte
        // manda, e a ordem e esta:
        //
        //   1o  conta bloqueada  ->  "Conta bloqueada por excesso de tentativas."
        //   2o  campo em branco  ->  "Preencha o usuario e a senha."
        //   3o  nao autenticou   ->  "Usuario ou senha incorretos."
        //       deu tudo certo   ->  ""
        //
        // Escreva as frases EXATAMENTE como estao ai em cima.
        // ---------------------------------------------------------------
        public static string MensagemDoLogin(string usuario, string senha,
                                             string usuarioGravado, string senhaGravada,
                                             int tentativas)
        {
            throw new NotImplementedException("Desafio 10: escreva o corpo.");
        }
    }
}
