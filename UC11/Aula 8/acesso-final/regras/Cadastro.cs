using System;

namespace Acesso
{
    /// <summary>
    /// SISTEMA DE CADASTRO - desafios 11 a 20. VERSAO IMPLEMENTADA (gabarito).
    ///
    /// Nao distribua.
    ///
    /// UC11 - Aulas 8 e 9, 10 e 11/09/2026.
    /// </summary>
    public class Cadastro
    {
        // As tres contas que ja existem no sistema.
        private static readonly string[] JA_CADASTRADOS =
        {
            "ana@senac.br", "bruno@senac.br", "carla@senac.br",
        };

        // ---------------------------------------------------------------
        // DESAFIO 11 - tem arroba, e um ponto DEPOIS da arroba.
        // ---------------------------------------------------------------
        public static bool EmailValido(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            int arroba = email.IndexOf('@');

            if (arroba < 0)
            {
                return false;
            }

            int ponto = email.IndexOf('.', arroba);

            return ponto > arroba;
        }

        // ---------------------------------------------------------------
        // DESAFIO 12 - 10 ou 11 digitos, e SO digitos.
        // ---------------------------------------------------------------
        public static bool TelefoneValido(string telefone)
        {
            if (SoDigitos(telefone) == false)
            {
                return false;
            }

            return telefone.Length == 10 || telefone.Length == 11;
        }

        // ---------------------------------------------------------------
        // DESAFIO 13 - exatamente 8 digitos.
        // ---------------------------------------------------------------
        public static bool CepValido(string cep)
        {
            if (SoDigitos(cep) == false)
            {
                return false;
            }

            return cep.Length == 8;
        }

        // ---------------------------------------------------------------
        // DESAFIO 14 - de 18 a 120, INCLUINDO os dois.
        // ---------------------------------------------------------------
        public static bool IdadeValida(int idade)
        {
            return idade >= 18 && idade <= 120;
        }

        // ---------------------------------------------------------------
        // DESAFIO 15 - pelo menos duas palavras. Espaco sobrando nao
        // inventa nem tira palavra.
        // ---------------------------------------------------------------
        public static bool NomeCompleto(string nome)
        {
            if (nome == null)
            {
                return false;
            }

            string[] partes = nome.Split(' ');
            int palavras = 0;

            for (int i = 0; i < partes.Length; i++)
            {
                if (partes[i] != "")
                {
                    palavras = palavras + 1;
                }
            }

            return palavras >= 2;
        }

        // ---------------------------------------------------------------
        // DESAFIO 16 - 11 digitos. NAO valida o digito verificador: aqui e
        // formato, nao matematica.
        // ---------------------------------------------------------------
        public static bool CpfTemFormato(string cpf)
        {
            if (SoDigitos(cpf) == false)
            {
                return false;
            }

            return cpf.Length == 11;
        }

        // ---------------------------------------------------------------
        // DESAFIO 17 - nenhum dos tres pode estar vazio nem so com espaco.
        // ---------------------------------------------------------------
        public static bool CamposPreenchidos(string nome, string email, string telefone)
        {
            return Preenchido(nome) && Preenchido(email) && Preenchido(telefone);
        }

        // ---------------------------------------------------------------
        // DESAFIO 18 - tres contas ja existem, e a comparacao NAO
        // diferencia maiuscula de minuscula.
        // ---------------------------------------------------------------
        public static bool EmailJaCadastrado(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            for (int i = 0; i < JA_CADASTRADOS.Length; i++)
            {
                if (JA_CADASTRADOS[i] == email.ToLower())
                {
                    return true;
                }
            }

            return false;
        }

        // ---------------------------------------------------------------
        // DESAFIO 19 - o cadastro so passa quando TODAS as condicoes valem
        // ao mesmo tempo. Este metodo nao decide nada sozinho: ele
        // pergunta para os outros.
        // ---------------------------------------------------------------
        public static bool PodeCadastrar(string nome, string email, string telefone,
                                         string cep, int idade, string senha)
        {
            return CamposPreenchidos(nome, email, telefone)
                && NomeCompleto(nome)
                && EmailValido(email)
                && EmailJaCadastrado(email) == false
                && TelefoneValido(telefone)
                && CepValido(cep)
                && IdadeValida(idade)
                && Login.SenhaAceita(senha);
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
        // ---------------------------------------------------------------
        public static string MensagemDoCadastro(string nome, string email, string telefone,
                                                string cep, int idade, string senha)
        {
            if (CamposPreenchidos(nome, email, telefone) == false)
            {
                return "Preencha todos os campos.";
            }

            if (NomeCompleto(nome) == false)
            {
                return "Informe o nome completo.";
            }

            if (EmailValido(email) == false)
            {
                return "E-mail invalido.";
            }

            if (EmailJaCadastrado(email))
            {
                return "Este e-mail ja esta cadastrado.";
            }

            if (TelefoneValido(telefone) == false)
            {
                return "Telefone invalido.";
            }

            if (CepValido(cep) == false)
            {
                return "CEP invalido.";
            }

            if (IdadeValida(idade) == false)
            {
                return "Idade fora do permitido.";
            }

            if (Login.SenhaAceita(senha) == false)
            {
                return "Senha muito fraca.";
            }

            return "";
        }

        // ---------------------------------------------------------------
        // Ajudantes internos. Nao sao desafio: ja vem prontos.
        // ---------------------------------------------------------------
        private static bool SoDigitos(string texto)
        {
            if (string.IsNullOrEmpty(texto))
            {
                return false;
            }

            for (int i = 0; i < texto.Length; i++)
            {
                if (texto[i] < '0' || texto[i] > '9')
                {
                    return false;
                }
            }

            return true;
        }

        private static bool Preenchido(string texto)
        {
            return texto != null && texto.Trim() != "";
        }
    }
}
