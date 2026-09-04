using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MundoDeCubos
{
    // O MOLDE do conteudo: estas classes so descrevem o formato do
    // arquivo conteudo/desafios-mundo.json, que e a fonte unica dos
    // enunciados, das dicas e dos testes.
    //
    // Voce NAO precisa mexer aqui. O que voce completa esta em Desafios.cs.

    public class Conteudo
    {
        [JsonPropertyName("titulo")] public string Titulo { get; set; }
        [JsonPropertyName("subtitulo")] public string Subtitulo { get; set; }
        [JsonPropertyName("desafios")] public List<Desafio> Desafios { get; set; }

        public static Conteudo Carregar()
        {
            string caminho = Path.Combine(AppContext.BaseDirectory, "desafios-mundo.json");

            if (!File.Exists(caminho))
            {
                throw new FileNotFoundException(
                    "Nao encontrei o desafios-mundo.json ao lado do executavel.\r\n" +
                    "Procurei em: " + caminho + "\r\n\r\n" +
                    "Recompile o projeto - o arquivo e copiado automaticamente.");
            }

            JsonSerializerOptions o = new JsonSerializerOptions();
            o.PropertyNameCaseInsensitive = true;

            Conteudo c = JsonSerializer.Deserialize<Conteudo>(File.ReadAllText(caminho), o);

            if (c == null || c.Desafios == null || c.Desafios.Count == 0)
            {
                throw new Exception("O arquivo foi lido, mas nao tem a lista 'desafios'.");
            }
            return c;
        }
    }

    public class Desafio
    {
        [JsonPropertyName("id")] public string Id { get; set; }
        [JsonPropertyName("numero")] public int Numero { get; set; }
        [JsonPropertyName("titulo")] public string Titulo { get; set; }
        [JsonPropertyName("revisa")] public string Revisa { get; set; }
        [JsonPropertyName("tecla")] public string Tecla { get; set; }
        [JsonPropertyName("destrava")] public string Destrava { get; set; }
        [JsonPropertyName("metodo")] public string Metodo { get; set; }
        [JsonPropertyName("assinatura")] public string Assinatura { get; set; }
        [JsonPropertyName("enunciado")] public string Enunciado { get; set; }
        [JsonPropertyName("explicacao")] public List<string> Explicacao { get; set; }
        [JsonPropertyName("dica")] public string Dica { get; set; }
        [JsonPropertyName("testes")] public List<Teste> Testes { get; set; }
    }

    public class Teste
    {
        // O cenario e o nome do mundinho que o Cenario.cs monta para este
        // teste. A resposta NAO fica aqui: este arquivo vai para a maquina
        // do aluno, e gabarito nele seria gabarito na mao dele.
        [JsonPropertyName("cenario")] public string Cenario { get; set; }

        // Os argumentos do metodo, separados por "|".
        [JsonPropertyName("entrada")] public string Entrada { get; set; }
        [JsonPropertyName("esperado")] public string Esperado { get; set; }
        [JsonPropertyName("descricao")] public string Descricao { get; set; }
    }
}
