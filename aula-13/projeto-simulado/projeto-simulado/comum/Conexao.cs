using System;
using MySql.Data.MySqlClient;

namespace Conecta
{
    // A CONEXAO COM O BANCO DA PROVA.
    //
    // O aplicativo grava aqui as respostas de cada aluno. O banco fica num
    // servidor na internet, e nao nesta maquina.
    //
    // ------------------------------------------------------------------
    //  PARA O PROFESSOR - preencher antes de distribuir
    //
    //  Os quatro valores abaixo estao em branco de proposito: senha de banco
    //  nao entra em repositorio. Preencha, compile uma vez, e distribua.
    //
    //  O usuario deste banco tem que ter SOMENTE INSERT numa tabela. A senha
    //  vai dentro do executavel de cada aluno - nao ha como esconder - entao a
    //  defesa e o banco recusar tudo que nao seja inserir. O script com os
    //  GRANT esta em ../banco/criar-tabela.sql.
    //
    //  Deixando em branco, o aplicativo funciona igual: ele grava as respostas
    //  num arquivo ao lado do executavel e avisa isso na tela.
    // ------------------------------------------------------------------

    public static class Conexao
    {
        public const string SERVIDOR = "";      // ex.: "191.0.0.1" ou "meuservidor.com"
        public const int    PORTA    = 3306;
        public const string BANCO    = "";      // ex.: "provauc12"
        public const string USUARIO  = "";      // o usuario que so tem INSERT
        public const string SENHA    = "";

        /// <summary>True quando o professor preencheu os dados do servidor.</summary>
        public static bool Configurada()
        {
            return SERVIDOR != "" && BANCO != "" && USUARIO != "";
        }

        public static string Texto()
        {
            // ConnectionTimeout curto de proposito: numa prova, esperar quinze
            // segundos por um servidor que nao responde e pior do que cair no
            // arquivo local em cinco.
            return "Server=" + SERVIDOR
                 + ";Port=" + PORTA
                 + ";Database=" + BANCO
                 + ";Uid=" + USUARIO
                 + ";Pwd=" + SENHA
                 + ";CharSet=utf8mb4"
                 + ";ConnectionTimeout=5"
                 + ";DefaultCommandTimeout=10";
        }

        public static MySqlConnection Obter()
        {
            return new MySqlConnection(Texto());
        }

        /// <summary>
        /// Traduz o erro de conexao para uma frase que ajuda quem esta na sala.
        /// A mensagem crua do MySQL nao diz nada para um aluno.
        /// </summary>
        public static string Traduzir(Exception ex)
        {
            string m = (ex == null ? "" : ex.Message);

            if (m.IndexOf("Unable to connect", StringComparison.OrdinalIgnoreCase) >= 0
             || m.IndexOf("timeout", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return "Nao consegui falar com o servidor. Pode ser a rede da sala, "
                     + "ou o servidor fora do ar.";
            }
            if (m.IndexOf("Access denied", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return "O servidor recusou o usuario ou a senha. Avise o professor.";
            }
            if (m.IndexOf("Unknown database", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return "O servidor respondeu, mas nao achou o banco. Avise o professor.";
            }
            if (m.IndexOf("command denied", StringComparison.OrdinalIgnoreCase) >= 0
             || m.IndexOf("INSERT command denied", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return "O servidor respondeu, mas recusou a gravacao. Avise o professor.";
            }
            return ex == null ? "Erro desconhecido." : ex.GetType().Name + ": " + m;
        }
    }
}
