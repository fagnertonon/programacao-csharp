using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Conecta
{
    // O MOLDE do conteudo. Estas classes so descrevem o formato do arquivo
    // conteudo-prova.json - a fonte unica que alimenta o aplicativo.
    //
    // Voce NAO precisa mexer aqui. O que voce completa hoje esta em Desafios.cs.

    public class Conteudo
    {
        [JsonPropertyName("aula")] public string Aula { get; set; }
        [JsonPropertyName("data")] public string Data { get; set; }
        [JsonPropertyName("titulo")] public string Titulo { get; set; }
        [JsonPropertyName("subtitulo")] public string Subtitulo { get; set; }

        // A frase da faixa ambar dos assuntos novos. Fica no JSON porque
        // muda de aula para aula ("os outros sete sao revisao" so valia
        // quando eram oito abas com um assunto novo).
        [JsonPropertyName("avisoNovo")] public string AvisoNovo { get; set; }

        // true = prova. Esconde a coluna de ensino e o botao de dica: numa
        // prova, ler a explicacao do padrao ao lado da questao que pede o
        // padrao mede leitura, e nao conhecimento.
        [JsonPropertyName("modoProva")] public bool ModoProva { get; set; }

        [JsonPropertyName("foraDaRevisao")] public List<string> ForaDaRevisao { get; set; }
        [JsonPropertyName("topicos")] public List<Topico> Topicos { get; set; }

        public static Conteudo Carregar()
        {
            string caminho = Path.Combine(AppContext.BaseDirectory, "conteudo-prova.json");

            if (!File.Exists(caminho))
            {
                throw new FileNotFoundException(
                    "Nao encontrei o conteudo-prova.json ao lado do executavel.\r\n" +
                    "Procurei em: " + caminho + "\r\n\r\n" +
                    "Recompile o projeto (F5) - o arquivo e copiado automaticamente.");
            }

            string json = File.ReadAllText(caminho);

            JsonSerializerOptions opcoes = new JsonSerializerOptions();
            opcoes.PropertyNameCaseInsensitive = true;

            Conteudo c = JsonSerializer.Deserialize<Conteudo>(json, opcoes);

            if (c == null || c.Topicos == null || c.Topicos.Count == 0)
            {
                throw new Exception("O arquivo foi lido, mas nao tem a lista 'topicos'.");
            }

            return c;
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

        // true = a aba nasce destravada, mesmo com a anterior por resolver.
        // Serve para abrir o material de quem quer ir na frente.
        [JsonPropertyName("livre")] public bool Livre { get; set; }

        [JsonPropertyName("explicacao")] public List<string> Explicacao { get; set; }
        [JsonPropertyName("diagrama")] public string Diagrama { get; set; }
        [JsonPropertyName("exemplo")] public Exemplo Exemplo { get; set; }
        [JsonPropertyName("noSeuProjeto")] public string NoSeuProjeto { get; set; }
        [JsonPropertyName("armadilha")] public Armadilha Armadilha { get; set; }
        [JsonPropertyName("desafio")] public Desafio Desafio { get; set; }

        // A mini-tela viva da aba. Pode ser null: topico sem bancada
        // funciona igual ao aplicativo da Aula 11.
        [JsonPropertyName("bancada")] public Bancada Bancada { get; set; }

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
        // o gabarito mora so no GABARITO-Desafios.txt, que fica com o professor.
        [JsonPropertyName("dica")] public string Dica { get; set; }
        [JsonPropertyName("testes")] public List<Teste> Testes { get; set; }
    }

    public class Teste
    {
        // Formato cru, so o motor le. Tres separadores encaixados:
        //    |  separa os ARGUMENTOS do metodo
        //    ;  separa os ITENS de uma lista
        //    ,  separa os CAMPOS de um item
        [JsonPropertyName("entrada")] public string Entrada { get; set; }
        [JsonPropertyName("esperado")] public string Esperado { get; set; }

        // O que o aluno le na linha do teste.
        [JsonPropertyName("descricao")] public string Descricao { get; set; }
    }

    // ------------------------------------------------------------------
    //  A BANCADA: a mini-tela viva de cada aba.
    // ------------------------------------------------------------------

    public class Bancada
    {
        // Aparece na barra escura, imitando o nome de uma janela do projeto.
        [JsonPropertyName("titulo")] public string Titulo { get; set; }
        [JsonPropertyName("instrucao")] public string Instrucao { get; set; }
        [JsonPropertyName("campos")] public List<CampoBancada> Campos { get; set; }
        [JsonPropertyName("botoes")] public List<BotaoBancada> Botoes { get; set; }
        [JsonPropertyName("saida")] public SaidaBancada Saida { get; set; }
    }

    public class CampoBancada
    {
        [JsonPropertyName("id")] public string Id { get; set; }
        [JsonPropertyName("rotulo")] public string Rotulo { get; set; }
        [JsonPropertyName("tipo")] public string Tipo { get; set; }      // "texto" | "senha"
        [JsonPropertyName("valor")] public string Valor { get; set; }    // valor inicial de verdade
        [JsonPropertyName("largura")] public int Largura { get; set; }
    }

    public class BotaoBancada
    {
        [JsonPropertyName("id")] public string Id { get; set; }
        [JsonPropertyName("texto")] public string Texto { get; set; }
        [JsonPropertyName("destaque")] public bool Destaque { get; set; }
    }

    public class SaidaBancada
    {
        [JsonPropertyName("titulo")] public string Titulo { get; set; }
        [JsonPropertyName("vazio")] public string Vazio { get; set; }
    }
}
