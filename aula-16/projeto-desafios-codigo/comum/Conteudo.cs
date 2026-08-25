using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Conecta
{
    // =====================================================================
    // O MOLDE do conteudo. JA ESTA PRONTO - voce nao mexe aqui.
    //
    // Le o desafios-portugol.json, que sai da FONTE UNICA da Aula 16 - a
    // mesma que gera a folha impressa.
    //
    // Daqui sai o ALGORITMO EM PORTUGOL de cada desafio, que aparece na
    // coluna da esquerda da janela. E ele que diz o que o metodo tem de
    // fazer.
    //
    // E SO ISSO: este arquivo nao tem resposta nenhuma dentro. O
    // passos-mural.json completo, esse sim tem, e por isso ele nao vem
    // junto do executavel.
    // =====================================================================

    public class Conteudo
    {
        [JsonPropertyName("aula")] public string Aula { get; set; }
        [JsonPropertyName("data")] public string Data { get; set; }
        [JsonPropertyName("desafios")] public List<Desafio> Desafios { get; set; }

        public static Conteudo Carregar()
        {
            string caminho = Path.Combine(AppContext.BaseDirectory, "desafios-portugol.json");

            if (!File.Exists(caminho))
            {
                throw new FileNotFoundException(
                    "Nao encontrei o desafios-portugol.json ao lado do executavel." +
                    Environment.NewLine +
                    "Procurei em: " + caminho + Environment.NewLine + Environment.NewLine +
                    "Recompile o projeto (F5) - o arquivo e copiado automaticamente.");
            }

            JsonSerializerOptions opcoes = new JsonSerializerOptions();
            opcoes.PropertyNameCaseInsensitive = true;

            return JsonSerializer.Deserialize<Conteudo>(File.ReadAllText(caminho), opcoes);
        }
    }

    public class Desafio
    {
        [JsonPropertyName("n")] public int N { get; set; }
        [JsonPropertyName("titulo")] public string Titulo { get; set; }
        [JsonPropertyName("metodo")] public string Metodo { get; set; }
        [JsonPropertyName("portugol")] public List<string> Portugol { get; set; }
        [JsonPropertyName("ficha")] public Ficha Ficha { get; set; }
        [JsonPropertyName("dica")] public string Dica { get; set; }
    }

    public class Ficha
    {
        [JsonPropertyName("recebe")] public string Recebe { get; set; }
        [JsonPropertyName("faz")] public string Faz { get; set; }
        [JsonPropertyName("devolve")] public string Devolve { get; set; }
    }
}
