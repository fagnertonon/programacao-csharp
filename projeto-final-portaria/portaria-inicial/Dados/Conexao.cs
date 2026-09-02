using System;
using MySql.Data.MySqlClient;

namespace Portaria
{
    /// <summary>
    /// O endereco do banco, num lugar so.
    ///
    /// Esta classe vem PRONTA. A UNICA coisa que voce mexe aqui e a
    /// senha do seu MySQL, na linha do pwd.
    ///
    /// Se a senha mudar, muda AQUI - e o sistema inteiro passa a usar a
    /// nova. E a razao de nenhum DAO escrever a string de conexao.
    /// </summary>
    public class Conexao
    {
        private const string STRING_CONEXAO =
            "server=localhost;" +
            "port=3306;" +
            "database=portariadb;" +
            "uid=root;" +
            "pwd=SUA_SENHA_AQUI;" +
            "CharSet=utf8mb4;";

        /// <summary>
        /// Devolve uma conexao FECHADA. Quem chama abre e fecha.
        /// </summary>
        public static MySqlConnection Obter()
        {
            return new MySqlConnection(STRING_CONEXAO);
        }

        /// <summary>
        /// Tenta abrir a conexao e devolve true se conseguiu.
        /// Quando nao consegue, devolve pelo out uma mensagem em
        /// portugues dizendo o que fazer.
        ///
        /// O out aqui e o mesmo out do double.TryParse da Aula 9: o
        /// metodo devolve true/false pelo return e o detalhe pelo
        /// parametro.
        /// </summary>
        public static bool TestarConexao(out string mensagemErro)
        {
            mensagemErro = "";
            MySqlConnection con = Obter();

            try
            {
                con.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                mensagemErro = TraduzirErro(ex);
                return false;
            }
            catch (Exception ex)
            {
                mensagemErro = "Erro inesperado: " + ex.Message;
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// Transforma o numero do erro do MySQL numa frase que diz o que
        /// fazer. Sao os quatro erros que aparecem de verdade no
        /// laboratorio.
        /// </summary>
        private static string TraduzirErro(MySqlException ex)
        {
            switch (ex.Number)
            {
                case 0:
                case 1042:
                    return "Nao achei o servidor.\n"
                         + "O servico MySQL80 esta rodando?\n"
                         + "Windows + R, digite services.msc e procure MySQL80.";

                case 1045:
                    return "Usuario ou senha do MySQL incorretos.\n"
                         + "Confira o pwd no Conexao.cs.";

                case 1049:
                    return "O banco portariadb nao existe.\n"
                         + "Rode o CriarBanco.sql no Workbench.";

                case 1146:
                    return "A tabela Usuario nao existe.\n"
                         + "O CriarBanco.sql rodou pela metade. Rode de novo.";

                default:
                    return "Erro " + ex.Number + ":\n" + ex.Message;
            }
        }
    }
}
