using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Conecta
{
    // =====================================================================
    // O MOLDE do conteudo.
    //
    // Estas classes so descrevem o formato do passos-mural.json - a FONTE
    // UNICA da Aula 16, que alimenta ao mesmo tempo:
    //
    //   - a folha B impressa (folhas/gerar_folhas.py le o mesmo arquivo)
    //   - este aplicativo
    //
    // Nao ha copia. O Portugol que aparece na tela e, letra por letra, o
    // que esta no papel na mao do aluno.
    //
    // ESTE ARQUIVO NAO E EXERCICIO. Voce nao mexe aqui.
    // =====================================================================

    public class Conteudo
    {
        [JsonPropertyName("aula")] public string Aula { get; set; }
        [JsonPropertyName("data")] public string Data { get; set; }
        [JsonPropertyName("titulo")] public string Titulo { get; set; }
        [JsonPropertyName("passos")] public List<Passo> Passos { get; set; }
        [JsonPropertyName("perguntas")] public List<Pergunta> Perguntas { get; set; }

        public static Conteudo Carregar()
        {
            string caminho = Path.Combine(AppContext.BaseDirectory, "passos-mural.json");

            if (!File.Exists(caminho))
            {
                throw new FileNotFoundException(
                    "Nao encontrei o passos-mural.json ao lado do executavel." +
                    Environment.NewLine +
                    "Procurei em: " + caminho + Environment.NewLine + Environment.NewLine +
                    "Recompile o projeto (F5) - o arquivo e copiado automaticamente.");
            }

            JsonSerializerOptions opcoes = new JsonSerializerOptions();
            opcoes.PropertyNameCaseInsensitive = true;

            return JsonSerializer.Deserialize<Conteudo>(File.ReadAllText(caminho), opcoes);
        }
    }

    public class Passo
    {
        [JsonPropertyName("n")] public int N { get; set; }
        [JsonPropertyName("nome")] public string Nome { get; set; }
        [JsonPropertyName("onde")] public string Onde { get; set; }
        [JsonPropertyName("estreia")] public string Estreia { get; set; }
        [JsonPropertyName("naFolha")] public bool NaFolha { get; set; }
        [JsonPropertyName("esquerda")] public Coluna Esquerda { get; set; }
        [JsonPropertyName("ficha")] public Ficha Ficha { get; set; }
        [JsonPropertyName("csharp")] public List<string> CSharp { get; set; }
        [JsonPropertyName("lacunas")] public List<Lacuna> Lacunas { get; set; }
    }

    /// <summary>
    /// Um pedaco de uma linha de codigo: ou texto comum (Lacuna = 0), ou
    /// uma lacuna numerada.
    /// </summary>
    public class Trecho
    {
        public string Texto;
        public int Lacuna;

        public Trecho(string texto, int lacuna)
        {
            Texto = texto;
            Lacuna = lacuna;
        }
    }

    public static class Codigo
    {
        /// <summary>
        /// Reparte uma linha do C# em pedacos, separando as lacunas {1} {2}
        /// do resto.
        ///
        /// CUIDADO COM AS CHAVES DO PROPRIO C#: a linha
        ///
        ///     {5} { con.Close(); }
        ///
        /// tem uma lacuna e um bloco. So conta como lacuna o que tem um
        /// NUMERO dentro das chaves - o resto e codigo, e vai como texto.
        /// </summary>
        public static List<Trecho> Fatiar(string linha)
        {
            List<Trecho> trechos = new List<Trecho>();
            string resto = linha;

            while (true)
            {
                int abre = resto.IndexOf('{');
                int fecha = abre >= 0 ? resto.IndexOf('}', abre) : -1;

                if (abre < 0 || fecha < 0)
                {
                    if (resto.Length > 0) trechos.Add(new Trecho(resto, 0));
                    return trechos;
                }

                string miolo = resto.Substring(abre + 1, fecha - abre - 1);
                int numero;

                if (int.TryParse(miolo, out numero))
                {
                    if (abre > 0) trechos.Add(new Trecho(resto.Substring(0, abre), 0));
                    trechos.Add(new Trecho("", numero));
                }
                else
                {
                    trechos.Add(new Trecho(resto.Substring(0, fecha + 1), 0));
                }

                resto = resto.Substring(fecha + 1);
            }
        }

        /// <summary>
        /// A linha inteira com as lacunas preenchidas. Serve ao autoteste:
        /// com todas as respostas certas, o resultado tem de ser o C# do
        /// gabarito, sem sobrar nenhum [n].
        /// </summary>
        public static string Preencher(string linha, Dictionary<int, string> pecas)
        {
            string saida = "";

            foreach (Trecho t in Fatiar(linha))
            {
                if (t.Lacuna == 0)
                {
                    saida += t.Texto;
                }
                else
                {
                    string peca;
                    saida += pecas.TryGetValue(t.Lacuna, out peca) && peca.Length > 0
                        ? peca : ("[" + t.Lacuna + "]");
                }
            }

            return saida;
        }
    }

    public class Coluna
    {
        [JsonPropertyName("tipo")] public string Tipo { get; set; }
        [JsonPropertyName("titulo")] public string Titulo { get; set; }
        [JsonPropertyName("linhas")] public List<string> Linhas { get; set; }
    }

    public class Ficha
    {
        [JsonPropertyName("recebe")] public string Recebe { get; set; }
        [JsonPropertyName("faz")] public string Faz { get; set; }
        [JsonPropertyName("devolve")] public string Devolve { get; set; }
    }

    // -----------------------------------------------------------------
    // RODADA 2 - as dez perguntas. Escolher entre quatro, com um trecho
    // de Portugol na tela para responder OLHANDO o algoritmo.
    // -----------------------------------------------------------------
    public class Pergunta
    {
        [JsonPropertyName("n")] public int N { get; set; }
        [JsonPropertyName("titulo")] public string Titulo { get; set; }
        [JsonPropertyName("portugol")] public List<string> Portugol { get; set; }
        [JsonPropertyName("enunciado")] public string Enunciado { get; set; }
        [JsonPropertyName("opcoes")] public List<string> Opcoes { get; set; }
        [JsonPropertyName("resposta")] public string Resposta { get; set; }
        [JsonPropertyName("dica")] public string Dica { get; set; }
    }

    public class Lacuna
    {
        [JsonPropertyName("n")] public int N { get; set; }
        [JsonPropertyName("resposta")] public string Resposta { get; set; }
        [JsonPropertyName("opcoes")] public List<string> Opcoes { get; set; }
        [JsonPropertyName("dica")] public string Dica { get; set; }

        /// <summary>
        /// As opcoes na ordem em que aparecem na tela.
        ///
        /// A rotacao depende do passo e do numero da lacuna, entao a ordem
        /// e sempre a MESMA em todas as maquinas - mas nao e a ordem em que
        /// a resposta certa foi escrita no arquivo. Nada de Random aqui:
        /// duas maquinas mostrando ordens diferentes viram discussao na
        /// sala em vez de aula.
        /// </summary>
        public List<string> OpcoesNaTela(int numeroDoPasso)
        {
            List<string> saida = new List<string>();
            int total = Opcoes.Count;
            int giro = (numeroDoPasso * 3 + N * 2) % total;

            for (int i = 0; i < total; i++)
            {
                saida.Add(Opcoes[(i + giro) % total]);
            }

            return saida;
        }
    }
}
