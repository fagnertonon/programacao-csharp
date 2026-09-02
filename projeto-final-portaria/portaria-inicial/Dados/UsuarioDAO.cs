using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Portaria
{
    /// <summary>
    /// Acesso aos dados de Usuario (DAO - Data Access Object).
    ///
    /// TRES REGRAS que valem para todos os metodos, sem excecao:
    ///
    ///   1. todo valor entra por @parametro, NUNCA colado com +
    ///   2. UPDATE e DELETE sempre tem WHERE
    ///   3. a conexao fecha no finally
    ///
    /// A regra 1 e seguranca: SQL montado com + aceita que o texto
    /// digitado pelo usuario vire comando. A regra 2 e a diferenca
    /// entre apagar uma linha e esvaziar a tabela.
    ///
    /// -----------------------------------------------------------------
    ///  O ESQUELETO DE TODO METODO DAQUI E SEMPRE O MESMO:
    ///
    ///      1. MySqlConnection con = Conexao.Obter();
    ///      2. try  {  con.Open();
    ///      3.         string sql = @"...";
    ///      4.         MySqlCommand cmd = new MySqlCommand(sql, con);
    ///      5.         cmd.Parameters.AddWithValue("@x", valor);
    ///      6.         executa e le  }
    ///      7. finally { con.Close(); }
    ///
    ///  Muda o SQL e mudam os parametros. Mais nada.
    ///
    ///  QUEM EXECUTA O QUE:
    ///      ExecuteScalar     - devolve UM valor    (COUNT, LAST_INSERT_ID)
    ///      ExecuteReader     - devolve LINHAS      (SELECT)
    ///      ExecuteNonQuery   - devolve a CONTAGEM  (INSERT, UPDATE, DELETE)
    /// -----------------------------------------------------------------
    /// </summary>
    public class UsuarioDAO
    {
        // -----------------------------------------------------------------
        //  LOGIN EXISTE                                        << TODO 4
        // -----------------------------------------------------------------

        /// <summary>
        /// TODO 4 - True se o login ja estiver em uso.  [Indicador I5]
        ///
        /// O SQL e este (pode copiar):
        ///
        ///     SELECT COUNT(*)
        ///       FROM Usuario
        ///      WHERE Login = @login
        ///
        /// O COUNT devolve UM numero, entao quem executa e o
        /// ExecuteScalar. A resposta dele vem como object - precisa de
        /// Convert.ToInt32 em volta.
        ///
        /// Devolva true quando esse numero for maior que zero.
        ///
        /// O COUNT aqui e a mensagem bonita. Quem garante mesmo que nao
        /// haja dois logins iguais e o UNIQUE (Login) da tabela.
        /// </summary>
        public static bool LoginExiste(string login)
        {
            return false;   // <<< TROQUE ESTA LINHA pela sua
        }

        // -----------------------------------------------------------------
        //  CRIAR CONTA                                         << TODO 5
        // -----------------------------------------------------------------

        /// <summary>
        /// TODO 5 - Grava a conta e devolve o Id que o banco gerou.
        ///                                              [Indicador I5]
        ///
        ///     INSERT INTO Usuario (Nome, Login, Senha)
        ///     VALUES (@nome, @login, @senha);
        ///     SELECT LAST_INSERT_ID();
        ///
        /// Sao TRES parametros, nao quatro: a DataCadastro entra sozinha,
        /// pelo DEFAULT CURRENT_TIMESTAMP da tabela. Voce nao precisa
        /// mandar a data.
        ///
        /// O SELECT LAST_INSERT_ID() no fim devolve o Id novo. Como o
        /// comando devolve um valor so, quem executa e o ExecuteScalar.
        ///
        /// Guarde esse numero em u.Id ANTES de devolver.
        /// </summary>
        public static int CriarConta(Usuario u)
        {
            return 0;   // <<< TROQUE ESTA LINHA pela sua
        }

        // -----------------------------------------------------------------
        //  MONTAR USUARIO                                      << TODO 7
        // -----------------------------------------------------------------

        /// <summary>
        /// TODO 7 - Transforma a linha atual do leitor num objeto.
        ///                                        [Indicadores I5 e I2]
        ///
        /// Crie um new Usuario() e preencha as CINCO propriedades a
        /// partir do leitor:
        ///
        ///     u.Id           = Convert.ToInt32(r["Id"]);
        ///     u.Nome         = Convert.ToString(r["Nome"]);
        ///     u.Login        = ...
        ///     u.Senha        = ...
        ///     u.DataCadastro = Convert.ToDateTime(r["DataCadastro"]);
        ///
        /// Depois devolva o objeto.
        ///
        /// Este metodo existe para estas cinco linhas nao se repetirem
        /// no Autenticar e no ListarTodos. Mudou uma coluna? Muda aqui,
        /// num lugar so. Escreva ele PRIMEIRO: os dois metodos seguintes
        /// dependem dele.
        /// </summary>
        private static Usuario MontarUsuario(MySqlDataReader r)
        {
            return new Usuario();   // <<< TROQUE ESTA LINHA pela sua
        }

        // -----------------------------------------------------------------
        //  AUTENTICAR                                          << TODO 8
        // -----------------------------------------------------------------

        /// <summary>
        /// TODO 8 - Confere login e senha e devolve a conta achada.
        ///                                        [Indicadores I5 e I3]
        ///
        ///     SELECT Id, Nome, Login, Senha, DataCadastro
        ///       FROM Usuario
        ///      WHERE Login = @login
        ///        AND Senha = @senha
        ///
        /// Sao DOIS parametros e os dois precisam bater ao mesmo tempo -
        /// e o AND que faz isso.
        ///
        /// Quem executa e o ExecuteReader. Use  if (r.Read())  e NAO
        /// while: aqui volta uma linha so, nao ha o que percorrer.
        ///
        /// Quando nao achar ninguem, devolva NULL. E esse null que a
        /// tela de login usa para dizer "usuario ou senha incorretos" -
        /// se voce devolver um Usuario vazio, qualquer senha entra.
        /// </summary>
        public static Usuario Autenticar(string login, string senha)
        {
            return null;   // <<< TROQUE ESTA LINHA pela sua
        }

        // -----------------------------------------------------------------
        //  LISTAR TODOS                                        << TODO 10
        // -----------------------------------------------------------------

        /// <summary>
        /// TODO 10 - Todas as contas, na ordem pedida.
        ///                                        [Indicadores I5 e I2]
        ///
        /// Use um SWITCH sobre o parametro ordem para escolher o SQL:
        ///
        ///   case 1:  ORDER BY DataCadastro DESC, Id DESC
        ///   case 0:
        ///   default: ORDER BY Nome
        ///
        /// As colunas sao sempre as mesmas:
        ///     SELECT Id, Nome, Login, Senha, DataCadastro FROM Usuario
        ///
        /// O DEFAULT E OBRIGATORIO e tem que devolver o MESMO SQL do
        /// case 0. Sem ele o projeto NEM COMPILA: o compilador nao
        /// consegue garantir que o sql recebeu valor, e acusa
        ///     CS0165 - uso de variavel local nao atribuida 'sql'
        /// Com o default, qualquer numero inesperado cai na ordem por
        /// nome.
        ///
        /// Depois de montar o sql, e o mesmo esqueleto de sempre, com
        /// ExecuteReader e  while (r.Read())  chamando MontarUsuario.
        ///
        /// QUEM ORDENA E O BANCO. Nao existe laco de ordenacao no C# em
        /// lugar nenhum deste sistema.
        /// </summary>
        public static List<Usuario> ListarTodos(int ordem)
        {
            return new List<Usuario>();   // <<< TROQUE ESTA LINHA pela sua
        }

        // -----------------------------------------------------------------
        //  EXCLUIR CONTA                                       << TODO 13
        // -----------------------------------------------------------------

        /// <summary>
        /// TODO 13 - Apaga UMA conta, pelo Id.          [Indicador I5]
        ///
        ///     DELETE FROM Usuario
        ///      WHERE Id = @id
        ///
        /// O WHERE aqui NAO E OPCIONAL: um DELETE sem WHERE esvazia a
        /// tabela inteira, e nao ha como desfazer.
        ///
        /// Quem executa e o ExecuteNonQuery, que devolve quantas linhas
        /// foram afetadas. Devolva true quando esse numero for maior
        /// que zero.
        /// </summary>
        public static bool ExcluirConta(int id)
        {
            return false;   // <<< TROQUE ESTA LINHA pela sua
        }
    }
}
