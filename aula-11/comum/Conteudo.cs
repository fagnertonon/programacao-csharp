using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Revisao
{
    // O MOLDE do conteudo. Estas classes so descrevem o formato do arquivo
    // conteudo-revisao.json - a fonte unica que alimenta as tres variantes
    // do aplicativo e tambem a apostila impressa.
    //
    // Voce NAO precisa mexer aqui. O que voce completa hoje esta em Desafios.cs.

    public class Conteudo
    {
        [JsonPropertyName("aula")] public string Aula { get; set; }
        [JsonPropertyName("data")] public string Data { get; set; }
        [JsonPropertyName("titulo")] public string Titulo { get; set; }
        [JsonPropertyName("foraDaRevisao")] public List<string> ForaDaRevisao { get; set; }
        [JsonPropertyName("topicos")] public List<Topico> Topicos { get; set; }

        public static Conteudo Carregar()
        {
            string caminho = Path.Combine(AppContext.BaseDirectory, "conteudo-revisao.json");

            if (!File.Exists(caminho))
            {
                throw new FileNotFoundException(
                    "Nao encontrei o conteudo-revisao.json ao lado do executavel.\r\n" +
                    "Procurei em: " + caminho + "\r\n\r\n" +
                    "Recompile o projeto (F5) - o arquivo e copiado automaticamente.");
            }

            string json = File.ReadAllText(caminho);

            JsonSerializerOptions opcoes = new JsonSerializerOptions();
            opcoes.PropertyNameCaseInsensitive = true;

            return JsonSerializer.Deserialize<Conteudo>(json, opcoes);
        }
    }

    public class Topico
    {
        [JsonPropertyName("id")] public string Id { get; set; }
        [JsonPropertyName("numero")] public int Numero { get; set; }
        [JsonPropertyName("titulo")] public string Titulo { get; set; }
        [JsonPropertyName("subtitulo")] public string Subtitulo { get; set; }
        // true = assunto novo desta aula, nao e revisao
        [JsonPropertyName("novo")] public bool Novo { get; set; }
        [JsonPropertyName("explicacao")] public List<string> Explicacao { get; set; }
        [JsonPropertyName("diagrama")] public string Diagrama { get; set; }
        [JsonPropertyName("exemplo")] public Exemplo Exemplo { get; set; }
        [JsonPropertyName("armadilha")] public Armadilha Armadilha { get; set; }
        [JsonPropertyName("desafio")] public Desafio Desafio { get; set; }

        // Um topico pode ter mais de uma pergunta: os dois mais cobrados na
        // revisao (operadores e switch) tem duas. Sao 10 no total.
        [JsonPropertyName("quiz")] public List<Quiz> Quiz { get; set; }
        [JsonPropertyName("folhaRevisao")] public string FolhaRevisao { get; set; }
    }

    public class Exemplo
    {
        [JsonPropertyName("codigo")] public string Codigo { get; set; }
        [JsonPropertyName("legenda")] public string Legenda { get; set; }
    }

    public class Armadilha
    {
        [JsonPropertyName("titulo")] public string Titulo { get; set; }
        [JsonPropertyName("texto")] public string Texto { get; set; }
        [JsonPropertyName("erroCompilador")] public string ErroCompilador { get; set; }
        [JsonPropertyName("regraDoCurso")] public string RegraDoCurso { get; set; }
    }

    public class Desafio
    {
        [JsonPropertyName("metodo")] public string Metodo { get; set; }
        [JsonPropertyName("assinatura")] public string Assinatura { get; set; }
        [JsonPropertyName("enunciado")] public string Enunciado { get; set; }
        // A resposta NAO fica aqui. Este arquivo vai para a maquina do aluno;
        // o gabarito mora so no GABARITO.md, que fica com o professor.
        // O que fica e a lista do que o codigo precisa conter para ser aceito
        // na variante B - e isso nao diz mais do que o enunciado ja diz.
        [JsonPropertyName("exigeTokens")] public List<string> ExigeTokens { get; set; }
        [JsonPropertyName("dica")] public string Dica { get; set; }
        [JsonPropertyName("testes")] public List<Teste> Testes { get; set; }
    }

    public class Teste
    {
        [JsonPropertyName("entrada")] public string Entrada { get; set; }
        [JsonPropertyName("esperado")] public string Esperado { get; set; }
        [JsonPropertyName("descricao")] public string Descricao { get; set; }
    }

    public class Quiz
    {
        [JsonPropertyName("pergunta")] public string Pergunta { get; set; }
        [JsonPropertyName("alternativas")] public List<string> Alternativas { get; set; }
        [JsonPropertyName("certa")] public int Certa { get; set; }
    }
}
