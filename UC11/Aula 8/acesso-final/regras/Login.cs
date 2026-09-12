using System;

namespace Acesso
{
    /// <summary>
    /// SISTEMA DE LOGIN - desafios 1 a 10. VERSAO IMPLEMENTADA (gabarito).
    ///
    /// Nao distribua. O aluno recebe a mesma classe com os corpos vazios.
    ///
    /// Repare que nenhum metodo aqui le do teclado nem escreve na tela, e
    /// nenhum guarda estado: todos recebem por parametro e devolvem com
    /// return. E por isso que da para testar os vinte sem abrir a janela.
    ///
    /// UC11 - Aulas 8 e 9, 10 e 11/09/2026.
    /// </summary>
    public class Login
    {
        // ---------------------------------------------------------------
        // DESAFIO 1 - o usuario tem de 4 a 20 caracteres, incluindo os dois.
        // ---------------------------------------------------------------
        public static bool UsuarioValido(string usuario)
        {
            if (usuario == null)
            {
                return false;
            }

            return usuario.Length >= 4 && usuario.Length <= 20;
        }

        // ---------------------------------------------------------------
        // DESAFIO 2 - a senha tem no minimo 8 caracteres.
        // ---------------------------------------------------------------
        public static bool SenhaTemTamanhoMinimo(string senha)
        {
            if (senha == null)
            {
                return false;
            }

            return senha.Length >= 8;
        }

        // ---------------------------------------------------------------
        // DESAFIO 3 - a senha tem pelo menos um digito.
        // ---------------------------------------------------------------
        public static bool SenhaTemNumero(string senha)
        {
            if (senha == null)
            {
                return false;
            }

            for (int i = 0; i < senha.Length; i++)
            {
                if (senha[i] >= '0' && senha[i] <= '9')
                {
                    return true;
                }
            }

            return false;
        }

        // ---------------------------------------------------------------
        // DESAFIO 4 - a senha tem pelo menos uma letra maiuscula.
        // ---------------------------------------------------------------
        public static bool SenhaTemMaiuscula(string senha)
        {
            if (senha == null)
            {
                return false;
            }

            for (int i = 0; i < senha.Length; i++)
            {
                if (senha[i] >= 'A' && senha[i] <= 'Z')
                {
                    return true;
                }
            }

            return false;
        }

        // ---------------------------------------------------------------
        // DESAFIO 5 - um ponto para cada desafio 2, 3 e 4 cumprido.
        // Vai de 0 a 3.
        // ---------------------------------------------------------------
        public static int ForcaDaSenha(string senha)
        {
            int forca = 0;

            if (SenhaTemTamanhoMinimo(senha))
            {
                forca = forca + 1;
            }

            if (SenhaTemNumero(senha))
            {
                forca = forca + 1;
            }

            if (SenhaTemMaiuscula(senha))
            {
                forca = forca + 1;
            }

            return forca;
        }

        // ---------------------------------------------------------------
        // DESAFIO 6 - a senha e aceita a partir da forca 2.
        // ---------------------------------------------------------------
        public static bool SenhaAceita(string senha)
        {
            return ForcaDaSenha(senha) >= 2;
        }

        // ---------------------------------------------------------------
        // DESAFIO 7 - o usuario NAO diferencia maiuscula de minuscula.
        // A senha, essa sim, diferencia.
        // ---------------------------------------------------------------
        public static bool Autenticar(string usuario, string senha,
                                      string usuarioGravado, string senhaGravada)
        {
            if (usuario == null || senha == null)
            {
                return false;
            }

            bool mesmoUsuario = usuario.ToLower() == (usuarioGravado ?? "").ToLower();
            bool mesmaSenha = senha == senhaGravada;

            return mesmoUsuario && mesmaSenha;
        }

        // ---------------------------------------------------------------
        // DESAFIO 8 - sao 3 tentativas. O resultado nunca e negativo.
        // ---------------------------------------------------------------
        public static int TentativasRestantes(int tentativas)
        {
            int restam = 3 - tentativas;

            if (restam < 0)
            {
                return 0;
            }

            return restam;
        }

        // ---------------------------------------------------------------
        // DESAFIO 9 - bloqueia quando nao resta nenhuma tentativa.
        // ---------------------------------------------------------------
        public static bool ContaBloqueada(int tentativas)
        {
            return TentativasRestantes(tentativas) == 0;
        }

        // ---------------------------------------------------------------
        // DESAFIO 10 - diz POR QUE o login foi recusado. O mais forte
        // manda, e a ordem e esta:
        //
        //   1o  conta bloqueada  ->  "Conta bloqueada por excesso de tentativas."
        //   2o  campo em branco  ->  "Preencha o usuario e a senha."
        //   3o  nao autenticou   ->  "Usuario ou senha incorretos."
        //       deu tudo certo   ->  ""
        // ---------------------------------------------------------------
        public static string MensagemDoLogin(string usuario, string senha,
                                             string usuarioGravado, string senhaGravada,
                                             int tentativas)
        {
            if (ContaBloqueada(tentativas))
            {
                return "Conta bloqueada por excesso de tentativas.";
            }

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(senha))
            {
                return "Preencha o usuario e a senha.";
            }

            if (Autenticar(usuario, senha, usuarioGravado, senhaGravada) == false)
            {
                return "Usuario ou senha incorretos.";
            }

            return "";
        }
    }
}
