using System;
using MySql.Data.MySqlClient;

namespace Conecta
{
    // ---------------------------------------------------------------------
    // ESTE ARQUIVO JA ESTA PRONTO.
    //
    // A UNICA coisa que voce mexe nele hoje e a senha do root, na linha do
    // pwd, logo abaixo. Nada mais.
    //
    // Duas coisas para reparar quando chegar o Bloco 6:
    //
    //   1. Obter() devolve a conexao FECHADA. Quem chama e que da o Open, e
    //      o Open fica DENTRO do try - assim o finally cobre tudo, inclusive
    //      a abertura.
    //
    //   2. TraduzirErro, la embaixo, e um switch. O MESMO switch que voce
    //      escreveu no MostrarErroDeTentativa, num lugar onde ele e
    //      obrigatorio: repare no "case 0: case 1042:" dividindo o mesmo
    //      corpo. Escada de else if nao escreve isso tao limpo.
    // ---------------------------------------------------------------------
    /// <summary>
    /// O endereco do banco, num lugar so.
    ///
    /// Se a senha mudar, muda AQUI - e o sistema inteiro passa a usar a
    /// nova. E a razao de nenhum DAO escrever a string de conexao.
    /// </summary>
    public class Conexao
    {
        private const string STRING_CONEXAO =
            "server=localhost;" +
            "port=3306;" +
            "database=conectadb;" +
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
        /// fazer. Sao os tres erros que aparecem de verdade no laboratorio.
        /// </summary>
        private static string TraduzirErro(MySqlException ex)
        {
            switch (ex.Number)
            {
                case 0:
                case 1042:
                    return "Nao achei o servidor.\n"
                         + "O servico MySQL80 esta rodando?";

                case 1045:
                    return "Usuario ou senha do MySQL incorretos.\n"
                         + "Confira o pwd no Conexao.cs.";

                case 1049:
                    return "O banco conectadb nao existe.\n"
                         + "Rode o CriarBanco.sql.";

                default:
                    return "Erro " + ex.Number + ":\n" + ex.Message;
            }
        }
    }
}
