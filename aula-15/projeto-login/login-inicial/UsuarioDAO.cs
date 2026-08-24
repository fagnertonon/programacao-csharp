using System;
using System.Collections.Generic;

namespace Conecta
{
    /// <summary>
    /// Acesso aos dados de Usuario (DAO - Data Access Object).
    ///
    /// Os QUATRO METODOS aqui dentro sao exatamente os da sua Atividade 1:
    /// mesmos nomes, mesmos parametros, mesmo tipo de retorno. Hoje eles
    /// nascem guardando numa List na memoria - e a Parte 7 da apostila.
    ///
    /// No Bloco 6 so o CORPO deles muda, para consultar o MySQL - e a
    /// Parte 11. Nome nenhum muda, e por isso NENHUMA TELA vai precisar
    /// ser alterada.
    ///
    /// Ou seja: o arquivo que sair daqui as 22h e o arquivo da sua
    /// Atividade 1. Nao e exercicio de faz de conta.
    ///
    /// Sao 5 lacunas, em ordem.
    /// </summary>
    public class UsuarioDAO
    {
        // TODO 1 - crie a estrutura que guarda as contas:
        //
        //     private static List<Usuario> _usuarios = new List<Usuario>();
        //     private static int _proximoId = 1;
        //
        //   "static" porque a lista de contas do sistema tem de ser UMA
        //   so. Se cada tela tivesse a sua, cadastrar numa e entrar pela
        //   outra nunca funcionaria - e e exatamente isso que voce vai
        //   testar no fim do Bloco 2.

        public static int CriarConta(Usuario u)
        {
            // TODO 2 - grave a conta e devolva o Id gerado:
            //
            //     u.Id = _proximoId;
            //     _proximoId++;
            //     _usuarios.Add(u);
            //     return u.Id;
            //
            //   Devolve o Id porque a Atividade 2 vai precisar dele para
            //   gravar o UsuarioId de cada postagem.
            //
            //   APAGUE a linha-tampao abaixo.
            return 0;
        }

        public static bool LoginExiste(string login)
        {
            // TODO 3 - o primeiro foreach do arquivo:
            //
            //     foreach (Usuario u in _usuarios)
            //     {
            //         if (u.Login == login) return true;
            //     }
            //     return false;
            //
            //   Ha N contas e e preciso olhar CADA UMA. Quem manda no
            //   numero de voltas e a colecao, nao voce - e a definicao do
            //   foreach.
            //
            //   Repare onde ficam os dois return: o true sai de DENTRO do
            //   laco, na hora que achou. O false so pode sair DEPOIS do
            //   laco - antes disso ainda ha contas por olhar.
            //
            //   APAGUE a linha-tampao abaixo.
            return false;
        }

        public static Usuario Autenticar(string login, string senha)
        {
            // TODO 4 - o metodo que a tela de login chama:
            //
            //     foreach (Usuario u in _usuarios)
            //     {
            //         if (u.Login == login && u.Senha == senha) return u;
            //     }
            //     return null;
            //
            //   POR QUE ELE DEVOLVE Usuario E NAO bool: depois de entrar, o
            //   sistema precisa saber o Id e o Nome de quem entrou, para
            //   escrever "Bem-vindo, Ana" na tela seguinte. Um "true" nao
            //   carrega isso.
            //
            //   E devolver null e o jeito de dizer "nao achei ninguem".
            //
            //   APAGUE a linha-tampao abaixo.
            return null;
        }

        public static Usuario BuscarPorId(int id)
        {
            // TODO 5 - o mesmo foreach, procurando pelo Id.
            //
            //   Nenhuma tela chama este metodo hoje. Escreva assim mesmo:
            //   os QUATRO metodos sao os da sua Atividade 1, e a Atividade
            //   2 usa este aqui para achar o dono de uma postagem.
            //
            //   APAGUE a linha-tampao abaixo.
            return null;
        }

        // =====================================================================
        //  BLOCO 6 E 7 - o mesmo arquivo, falando com o MySQL
        //
        //  Nao escreva nada daqui de baixo antes das 20h45. Esta tudo
        //  comentado de proposito.
        //
        //  O MOLDE, que se repete nos quatro metodos:
        //
        //      MySqlConnection con = Conexao.Obter();   // 1. pega a conexao
        //      try
        //      {
        //          con.Open();                          // 2. abre
        //
        //          string sql = "... @parametro ...";   // 3. monta o SQL
        //
        //          MySqlCommand cmd = new MySqlCommand(sql, con);
        //          cmd.Parameters.AddWithValue("@parametro", valor);  // 4.
        //
        //          // 5. executa
        //      }
        //      finally
        //      {
        //          con.Close();                         // 6. fecha, SEMPRE
        //      }
        //
        //  Obter() devolve a conexao FECHADA. Quem chama e que da o Open, e
        //  o Open fica DENTRO do try - assim o finally cobre tudo. O
        //  finally roda mesmo quando da erro no meio: conexao que nao fecha
        //  e conexao perdida.
        //
        //  OS TRES JEITOS DE EXECUTAR, escolhidos por "o que eu quero de
        //  volta?":
        //
        //      ExecuteReader()     varias linhas    -> Autenticar, BuscarPorId
        //      ExecuteScalar()     um valor so      -> LoginExiste (COUNT)
        //      ExecuteNonQuery()   nada, so age     -> um INSERT sem retorno
        //
        //  E o metodo que le uma linha e monta o objeto - descomente ele no
        //  Bloco 6, junto com o "using MySql.Data.MySqlClient;" la em cima:
        //
        //      private static Usuario MontarUsuario(MySqlDataReader r)
        //      {
        //          Usuario u = new Usuario();
        //
        //          u.Id    = Convert.ToInt32(r["Id"]);
        //          u.Nome  = r["Nome"].ToString();
        //          u.Login = r["Login"].ToString();
        //          u.Senha = r["Senha"].ToString();
        //
        //          return u;
        //      }
        //
        //  Ele existe para nao repetir estas quatro linhas em cada consulta.
        //
        //  ---------------------------------------------------------------
        //  POR QUE @login E NAO "... WHERE Login = '" + login + "'"
        //
        //  Com o texto grudado, quem digitar    ' OR '1'='1
        //  no campo Usuario faz o WHERE virar sempre verdadeiro, e o SELECT
        //  devolve a primeira conta da tabela - entrou sem saber a senha.
        //
        //  Com @login, o que o sujeito digitou e tratado como VALOR, nunca
        //  como comando. Vale para todo SQL do curso, sem excecao.
        // =====================================================================
    }
}
