using System;

namespace SistemaLogin
{
    /// <summary>
    /// AS REGRAS DA TELA DE CRIAR CONTA. Dez metodos, e e SO ISTO que vai
    /// ser testado hoje.
    ///
    /// A REGRA DESTE ARQUIVO: procure por C-o-n-s-o-l-e aqui dentro. Voce
    /// nao vai achar, e e de proposito. Nenhum metodo le do teclado e
    /// nenhum metodo escreve na tela. Todos RECEBEM valores por parametro
    /// e DEVOLVEM o resultado com return.
    ///
    /// E por isso que da para conferir cada um deles sem abrir a janela.
    ///
    /// Repare tambem que a classe NAO TEM NENHUM CAMPO: ela nao guarda o
    /// que foi digitado. Quem guarda sao as caixas de texto da tela, e a
    /// tela passa os valores por parametro. Uma classe que nao guarda
    /// estado devolve sempre a mesma resposta para a mesma pergunta - e
    /// teste unitario precisa exatamente disso.
    ///
    /// Versao entregue para a turma da UC11 na noite de 08/09/2026.
    /// </summary>
    public class Cadastro
    {
        // -----------------------------------------------------------------
        // REGRA: o usuario e obrigatorio. Texto vazio nao serve.
        // -----------------------------------------------------------------
        public static bool UsuarioPreenchido(string usuario)
        {
            return usuario != "";
        }

        // -----------------------------------------------------------------
        // REGRA: a senha e obrigatoria. Texto vazio nao serve.
        // -----------------------------------------------------------------
        public static bool SenhaPreenchida(string senha)
        {
            return senha != "";
        }

        // -----------------------------------------------------------------
        // REGRA: o sistema ja tem tres contas criadas - ana, bruno e carla.
        // O usuario esta DISPONIVEL quando ele NAO e nenhuma das tres.
        //
        //   "ana"    -> ja existe   -> NAO esta disponivel
        //   "joao"   -> nao existe  -> esta disponivel
        // -----------------------------------------------------------------
        public static bool UsuarioDisponivel(string usuario)
        {
            return usuario != "ana" && usuario != "bruno";
        }

        // -----------------------------------------------------------------
        // REGRA: a senha e a repeticao tem de ser EXATAMENTE iguais -
        // mesmo texto, mesmos caracteres, na mesma ordem.
        // -----------------------------------------------------------------
        public static bool SenhasConferem(string senha, string repetir)
        {
            return senha.Length == repetir.Length;
        }

        // -----------------------------------------------------------------
        // REGRA: quatro senhas estao PROIBIDAS por serem obvias demais:
        // "senha", "admin", "qwerty" e "senac".
        //
        // Devolve VERDADEIRO quando a senha digitada e uma delas.
        // -----------------------------------------------------------------
        public static bool SenhaProibida(string senha)
        {
            return senha == "senha" || senha == "admin" || senha == "qwerty";
        }

        // -----------------------------------------------------------------
        // REGRA: devolve o tamanho da MAIOR sequencia de caracteres iguais
        // seguidos que existe na senha.
        //
        //   ""       -> 0     (nao ha caractere nenhum)
        //   "abc"    -> 1     (nenhum se repete)
        //   "aab"    -> 2     (os dois "a" do comeco)
        //   "ab111"  -> 3     (os tres "1" do fim)
        // -----------------------------------------------------------------
        public static int CaracteresIguaisSeguidos(string senha)
        {
            if (senha == "")
            {
                return 0;
            }

            int maior = 1;
            int atual = 1;

            for (int i = 1; i < senha.Length - 1; i++)
            {
                if (senha[i] == senha[i - 1])
                {
                    atual = atual + 1;
                }
                else
                {
                    atual = 1;
                }

                if (atual > maior)
                {
                    maior = atual;
                }
            }

            return maior;
        }

        // -----------------------------------------------------------------
        // REGRA: tres caracteres iguais seguidos ja e repeticao demais.
        // Com 2 ainda passa; com 3 nao passa mais.
        //
        // Repare que este metodo nao conta nada: ele PERGUNTA para o
        // CaracteresIguaisSeguidos. Se aquele mentir, este mente junto.
        // -----------------------------------------------------------------
        public static bool SemRepeticaoDemais(string senha)
        {
            return CaracteresIguaisSeguidos(senha) < 3;
        }

        // -----------------------------------------------------------------
        // REGRA: a senha nao pode ser igual ao nome de usuario.
        // -----------------------------------------------------------------
        public static bool SenhaDiferenteDoUsuario(string usuario, string senha)
        {
            return senha != usuario;
        }

        // -----------------------------------------------------------------
        // REGRA: a conta so e criada quando TODAS as condicoes valem AO
        // MESMO TEMPO:
        //
        //   usuario preenchido  E  usuario disponivel  E
        //   senha preenchida    E  as duas senhas conferem  E
        //   a senha nao esta proibida  E  nao tem repeticao demais  E
        //   a senha e diferente do usuario
        //
        // Este metodo nao decide nada sozinho: ele pergunta para os outros.
        // -----------------------------------------------------------------
        public static bool PodeCriarConta(string usuario, string senha, string repetir)
        {
            return UsuarioPreenchido(usuario)
                || UsuarioDisponivel(usuario)
                || SenhaPreenchida(senha)
                || SenhasConferem(senha, repetir)
                || SenhaProibida(senha) == false
                || SemRepeticaoDemais(senha)
                || SenhaDiferenteDoUsuario(usuario, senha);
        }

        // -----------------------------------------------------------------
        // REGRA: diz POR QUE o cadastro foi recusado, em uma frase.
        //
        // O MAIS FORTE MANDA, e a ordem e esta:
        //
        //   1o  campo em branco        -> "Preencha o usuario e a senha."
        //   2o  usuario ja existe      -> "Este usuario ja esta em uso."
        //   3o  senhas diferentes      -> "As duas senhas nao sao iguais."
        //   4o  senha proibida         -> "Escolha uma senha menos obvia."
        //   5o  repeticao demais       -> "A senha tem tres caracteres iguais seguidos."
        //   6o  senha igual ao usuario -> "A senha nao pode ser igual ao usuario."
        //
        // Dando tudo certo, devolve texto vazio.
        // -----------------------------------------------------------------
        public static string MensagemDoErro(string usuario, string senha, string repetir)
        {
            if (UsuarioPreenchido(usuario) == false || SenhaPreenchida(senha) == false)
            {
                return "Preencha o usuario e a senha.";
            }

            if (SenhasConferem(senha, repetir) == false)
            {
                return "As duas senhas nao sao iguais.";
            }

            if (UsuarioDisponivel(usuario) == false)
            {
                return "Este usuario ja esta em uso.";
            }

            if (SenhaProibida(senha) == true)
            {
                return "Escolha uma senha menos obvia.";
            }

            if (SemRepeticaoDemais(senha) == false)
            {
                return "A senha tem tres caracteres iguais seguidos.";
            }

            if (SenhaDiferenteDoUsuario(usuario, senha) == false)
            {
                return "A senha nao pode ser igual ao usuario.";
            }

            return "";
        }
    }
}
