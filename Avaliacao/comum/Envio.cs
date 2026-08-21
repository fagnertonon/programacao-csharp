using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MySql.Data.MySqlClient;

namespace Conecta
{
    // O ENVIO DAS RESPOSTAS.
    //
    // Grava SEMPRE um arquivo ao lado do executavel, e depois tenta gravar no
    // banco. A ordem importa: se a rede falhar, a resposta do aluno ja esta
    // salva em disco. Problema de rede nao pode custar a prova de ninguem.
    //
    // Voce NAO precisa mexer aqui.

    public class ResultadoEnvio
    {
        public bool ArquivoOk;
        public string CaminhoArquivo;

        public bool BancoOk;
        public bool BancoTentado;
        public string ErroBanco;

        public int Questoes;
    }

    public class RespostaAluno
    {
        public string Questao;      // o id do topico
        public int Numero;
        public string Metodo;
        public int Passaram;
        public int Total;
        public bool Resolvido;
    }

    public static class Envio
    {
        /// <summary>
        /// O codigo-fonte que o aluno escreveu. Procura o Desafios.cs em alguns
        /// lugares previsiveis: ao lado do executavel (quando o csproj copia) e
        /// subindo tres pastas a partir de bin/Debug/net8.0-windows, que e onde
        /// o Visual Studio deixa o executavel ao rodar com F5.
        /// </summary>
        public static string LerCodigo()
        {
            string b = AppContext.BaseDirectory;

            string[] tentativas = {
                Path.Combine(b, "Desafios.cs"),
                Path.Combine(b, "..", "..", "..", "Desafios.cs"),
                Path.Combine(b, "..", "..", "..", "..", "Desafios.cs"),
            };

            foreach (string t in tentativas)
            {
                try
                {
                    if (File.Exists(t)) { return File.ReadAllText(t); }
                }
                catch (Exception) { /* caminho invalido: tenta o proximo */ }
            }
            return "(nao encontrei o Desafios.cs para enviar)";
        }

        public static ResultadoEnvio Enviar(string aluno, List<RespostaAluno> respostas)
        {
            ResultadoEnvio r = new ResultadoEnvio();
            r.Questoes = respostas == null ? 0 : respostas.Count;

            string codigo = LerCodigo();
            string maquina = Ambiente();

            // ---- 1. o arquivo, SEMPRE, e antes de qualquer rede ----
            try
            {
                r.CaminhoArquivo = GravarArquivo(aluno, maquina, respostas, codigo);
                r.ArquivoOk = true;
            }
            catch (Exception)
            {
                r.ArquivoOk = false;
            }

            // ---- 2. o banco, se o professor tiver configurado ----
            if (!Conexao.Configurada()) { return r; }

            r.BancoTentado = true;

            // O Obter() tem que ficar DENTRO do try: com um endereco mal
            // digitado quem lanca e o construtor do MySqlConnection, antes
            // de qualquer Open().
            MySqlConnection con = null;

            try
            {
                con = Conexao.Obter();
                con.Open();

                foreach (RespostaAluno a in respostas)
                {
                    string sql =
                        "INSERT INTO resposta "
                      + "(aluno, maquina, questao, numero, metodo, passaram, total, resolvido, codigo) "
                      + "VALUES (@aluno, @maquina, @questao, @numero, @metodo, "
                      + "@passaram, @total, @resolvido, @codigo)";

                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@aluno", aluno);
                    cmd.Parameters.AddWithValue("@maquina", maquina);
                    cmd.Parameters.AddWithValue("@questao", a.Questao);
                    cmd.Parameters.AddWithValue("@numero", a.Numero);
                    cmd.Parameters.AddWithValue("@metodo", a.Metodo);
                    cmd.Parameters.AddWithValue("@passaram", a.Passaram);
                    cmd.Parameters.AddWithValue("@total", a.Total);
                    cmd.Parameters.AddWithValue("@resolvido", a.Resolvido ? 1 : 0);
                    cmd.Parameters.AddWithValue("@codigo", codigo);

                    cmd.ExecuteNonQuery();
                }

                r.BancoOk = true;
            }
            catch (Exception ex)
            {
                r.BancoOk = false;
                r.ErroBanco = Conexao.Traduzir(ex);
            }
            finally
            {
                try { if (con != null) { con.Close(); } } catch (Exception) { }
            }

            return r;
        }

        private static string Ambiente()
        {
            try { return Environment.MachineName + " / " + Environment.UserName; }
            catch (Exception) { return "(desconhecida)"; }
        }

        private static string GravarArquivo(string aluno, string maquina,
                                            List<RespostaAluno> respostas, string codigo)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("ALUNO   : " + aluno);
            sb.AppendLine("MAQUINA : " + maquina);
            sb.AppendLine("QUANDO  : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            sb.AppendLine();
            sb.AppendLine("RESULTADO POR QUESTAO");
            sb.AppendLine("---------------------");

            foreach (RespostaAluno a in respostas)
            {
                sb.AppendLine(a.Numero + ". " + a.Metodo + "  ->  "
                            + a.Passaram + " de " + a.Total + " testes"
                            + (a.Resolvido ? "   RESOLVIDO" : ""));
            }

            sb.AppendLine();
            sb.AppendLine("CODIGO ENVIADO");
            sb.AppendLine("--------------");
            sb.AppendLine(codigo);

            string nome = "respostas-" + Limpar(aluno) + "-"
                        + DateTime.Now.ToString("HHmmss") + ".txt";
            string caminho = Path.Combine(AppContext.BaseDirectory, nome);

            File.WriteAllText(caminho, sb.ToString(), Encoding.UTF8);
            return caminho;
        }

        // Nome de arquivo nao aceita tudo que um nome de pessoa aceita.
        private static string Limpar(string texto)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in (texto ?? "aluno"))
            {
                if (char.IsLetterOrDigit(c)) { sb.Append(c); }
                else if (c == ' ' || c == '-') { sb.Append('-'); }
            }
            string s = sb.ToString();
            return s == "" ? "aluno" : s;
        }
    }
}
