using System;

namespace Acesso
{
    /// <summary>
    /// SISTEMA DE CADASTRO - desafios 11 a 20.
    ///
    /// Mesma regra da noite: primeiro o teste, depois o corpo.
    ///
    /// UC11 - Aulas 8 e 9.
    /// </summary>
    public class Cadastro
    {
        // As tres contas que ja existem no sistema. NAO mexa nesta linha.
        private static readonly string[] JA_CADASTRADOS =
        {
            "ana@senac.br", "bruno@senac.br", "carla@senac.br",
        };

        // ---------------------------------------------------------------
        // DESAFIO 11 - tem arroba, e um ponto DEPOIS da arroba.
        //
        // Cuidado: "ana.b@senacbr" tem ponto e tem arroba, e mesmo assim e
        // invalido - o ponto esta do lado errado.
        // ---------------------------------------------------------------
        public static bool EmailValido(string email)
        {
            throw new NotImplementedException("Desafio 11: escreva o corpo.");
        }

        // ---------------------------------------------------------------
        // DESAFIO 12 - 10 ou 11 digitos, e SO digitos.
        // ---------------------------------------------------------------
        public static bool TelefoneValido(string telefone)
        {
            throw new NotImplementedException("Desafio 12: escreva o corpo.");
        }

        // ---------------------------------------------------------------
        // DESAFIO 13 - exatamente 8 digitos.
        // ---------------------------------------------------------------
        public static bool CepValido(string cep)
        {
            throw new NotImplementedException("Desafio 13: escreva o corpo.");
        }

        // ---------------------------------------------------------------
        // DESAFIO 14 - de 18 a 120, INCLUINDO os dois.
        // ---------------------------------------------------------------
        public static bool IdadeValida(int idade)
        {
            throw new NotImplementedException("Desafio 14: escreva o corpo.");
        }

        // ---------------------------------------------------------------
        // DESAFIO 15 - pelo menos duas palavras. Espaco sobrando nao
        // inventa nem tira palavra.
        // ---------------------------------------------------------------
        public static bool NomeCompleto(string nome)
        {
            throw new NotImplementedException("Desafio 15: escreva o corpo.");
        }

        // ---------------------------------------------------------------
        // DESAFIO 16 - 11 digitos, e so digitos. NAO valida o digito
        // verificador: aqui e formato, nao matematica.
        // ---------------------------------------------------------------
        public static bool CpfTemFormato(string cpf)
        {
            throw new NotImplementedException("Desafio 16: escreva o corpo.");
        }

        // ---------------------------------------------------------------
        // DESAFIO 17 - nenhum dos tres pode estar vazio nem so com espaco.
        // ---------------------------------------------------------------
        public static bool CamposPreenchidos(string nome, string email, string telefone)
        {
            throw new NotImplementedException("Desafio 17: escreva o corpo.");
        }

        // ---------------------------------------------------------------
        // DESAFIO 18 - use o JA_CADASTRADOS ai em cima. A comparacao NAO
        // diferencia maiuscula de minuscula.
        // ---------------------------------------------------------------
        public static bool EmailJaCadastrado(string email)
        {
            throw new NotImplementedException("Desafio 18: escreva o corpo.");
        }

        // ---------------------------------------------------------------
        // DESAFIO 19 - o cadastro so passa quando TODAS as condicoes valem
        // ao mesmo tempo. Este metodo nao decide nada sozinho: ele pergunta
        // para os outros, inclusive para o Login.SenhaAceita do desafio 6.
        // ---------------------------------------------------------------
        public static bool PodeCadastrar(string nome, string email, string telefone,
                                         string cep, int idade, string senha)
        {
            throw new NotImplementedException("Desafio 19: escreva o corpo.");
        }

        // ---------------------------------------------------------------
        // DESAFIO 20 - diz POR QUE o cadastro foi recusado. O mais forte
        // manda, e a ordem e esta:
        //
        //   1o  campo em branco       -> "Preencha todos os campos."
        //   2o  nome incompleto       -> "Informe o nome completo."
        //   3o  e-mail invalido       -> "E-mail invalido."
        //   4o  e-mail ja cadastrado  -> "Este e-mail ja esta cadastrado."
        //   5o  telefone invalido     -> "Telefone invalido."
        //   6o  CEP invalido          -> "CEP invalido."
        //   7o  idade fora da faixa   -> "Idade fora do permitido."
        //   8o  senha fraca           -> "Senha muito fraca."
        //       deu tudo certo        -> ""
        //
        // Escreva as frases EXATAMENTE como estao ai em cima.
        // ---------------------------------------------------------------
        public static string MensagemDoCadastro(string nome, string email, string telefone,
                                                string cep, int idade, string senha)
        {
            throw new NotImplementedException("Desafio 20: escreva o corpo.");
        }
    }
}
