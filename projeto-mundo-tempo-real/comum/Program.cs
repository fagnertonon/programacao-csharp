using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MundoDeCubos
{
    // ==================================================================
    //  O SERVIDOR. Voce NAO precisa mexer aqui.
    //
    //  Aperte F5 e o Visual Studio faz duas coisas: sobe este servidor e
    //  abre o navegador em http://localhost:5200.
    //
    //  Quem desenha o mundo em 3D e o navegador. Quem MANDA no mundo e
    //  este programa em C# - e dentro dele, os seis metodos que voce
    //  escreveu no Desafios.cs.
    //
    //  Repare que este projeto nao usa NENHUM pacote do NuGet. O ASP.NET
    //  Core ja vem dentro do .NET 8, entao ele compila e roda mesmo com a
    //  internet do laboratorio fora do ar.
    // ==================================================================
    public class Program
    {
        public static void Main(string[] args)
        {
            // A raiz e a pasta do EXECUTAVEL, e nao a pasta do projeto.
            // Motivo: o wwwroot e o desafios-mundo.json vivem uma pasta
            // acima, compartilhados pelos dois projetos, e sao COPIADOS
            // para a saida na compilacao. Sem esta linha o servidor
            // procuraria a tela do jogo em mundo-inicial\wwwroot, que nao
            // existe, e responderia 404 na cara do aluno.
            string raiz = AppContext.BaseDirectory;

            WebApplicationOptions opcoes = new WebApplicationOptions
            {
                Args = args,
                ContentRootPath = raiz,
                WebRootPath = Path.Combine(raiz, "wwwroot")
            };

            WebApplicationBuilder construtor = WebApplication.CreateBuilder(opcoes);
            construtor.Logging.ClearProviders();
            construtor.WebHost.UseUrls("http://localhost:5200");

            WebApplication app = construtor.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            Motor.Corrigir();
            Motor.Comecar(MinhaConfig.Semente);

            // ---- o mundo inteiro, uma vez so, na abertura ----
            app.MapGet("/api/mundo", () => Results.Json(Instantaneo()));

            // ---- os desafios e o resultado dos testes ----
            app.MapGet("/api/desafios", () =>
            {
                List<ResultadoDesafio> r = Motor.Corrigir();
                return Results.Json(new
                {
                    titulo = Motor.Conteudo.Titulo,
                    subtitulo = Motor.Conteudo.Subtitulo,
                    desafios = Motor.Conteudo.Desafios,
                    resultados = r
                });
            });

            // O NAVEGADOR PERGUNTA ISTO DEZ VEZES POR SEGUNDO.
            //
            // Na versao por turno nao existia: o mundo so mudava quando o
            // jogador apertava alguma coisa, entao a resposta da acao ja
            // trazia tudo. Aqui os monstros andam sozinhos, e a tela
            // precisa perguntar "o que mudou?" o tempo todo.
            //
            // A resposta e de proposito pequena - jogador, monstros, vida
            // e o que mudou no mundo -, nunca os 11520 cubos.
            app.MapGet("/api/estado", () => Results.Json(Resposta(null)));

            // ---- as acoes ----
            app.MapPost("/api/andar", (Comando c) =>
            {
                Motor.Andar(c.dx, c.dz);
                return Results.Json(Resposta(null));
            });

            app.MapPost("/api/pular", (Comando c) =>
            {
                Motor.Saltar(c.forca <= 0 ? 2 : c.forca);
                return Results.Json(Resposta(null));
            });

            app.MapPost("/api/minerar", (Comando c) =>
            {
                string caiu = Motor.Quebrar(c.x, c.y, c.z, c.picareta <= 0 ? 3 : c.picareta);
                return Results.Json(Resposta(caiu));
            });

            app.MapPost("/api/cavar", (Comando c) =>
            {
                int quantos = Motor.Poco(c.x, c.y, c.z, c.profundidade <= 0 ? 4 : c.profundidade);
                return Results.Json(Resposta("cavou " + quantos));
            });

            app.MapPost("/api/colocar", (Comando c) =>
            {
                bool ok = Motor.Por(c.x, c.y, c.z, c.tipo ?? "");
                return Results.Json(Resposta(ok ? "colocou" : ""));
            });

            app.MapPost("/api/reiniciar", (Comando c) =>
            {
                Sandbox.Perdoar();
                Motor.Corrigir();
                Motor.Comecar(c.semente <= 0 ? MinhaConfig.Semente : c.semente);
                return Results.Json(Instantaneo());
            });

            Console.WriteLine();
            Console.WriteLine("  MUNDO DE CUBOS - TEMPO REAL  em  http://localhost:5200");
            Console.WriteLine("  Os monstros andam sozinhos. Cuidado.");
            Console.WriteLine("  Feche esta janela preta para parar o servidor.");
            Console.WriteLine();

            app.Run();
        }

        // O mundo vira um vetor achatado de indices de tipo: 11520 numeros
        // pequenos em vez de 11520 textos. Sai em uns 25 KB de JSON, e so e
        // enviado na abertura e no reiniciar.
        private static object Instantaneo()
        {
            List<string> nomes = new List<string>();
            List<object> tipos = new List<object>();

            nomes.Add("");   // o indice 0 e sempre o ar
            tipos.Add(new { nome = "ar", cor = "#000000", dureza = 0 });

            foreach (Bloco b in Motor.Mundo.Tipos)
            {
                nomes.Add(b.Nome);
                tipos.Add(new { nome = b.Nome, cor = b.Cor, dureza = b.Dureza });
            }

            int[] celulas = new int[Mundo.LARGURA * Mundo.ALTURA * Mundo.FUNDO];
            int i = 0;

            for (int y = 0; y < Mundo.ALTURA; y++)
            {
                for (int z = 0; z < Mundo.FUNDO; z++)
                {
                    for (int x = 0; x < Mundo.LARGURA; x++)
                    {
                        celulas[i] = nomes.IndexOf(Motor.Mundo.Bloco(x, y, z));
                        if (celulas[i] < 0) { celulas[i] = 0; }
                        i++;
                    }
                }
            }

            return new
            {
                largura = Mundo.LARGURA,
                altura = Mundo.ALTURA,
                fundo = Mundo.FUNDO,
                semente = Motor.Semente,
                tipos = tipos,
                celulas = celulas,
                jogador = new { x = Motor.Jogador.X, y = Motor.Jogador.Y, z = Motor.Jogador.Z },
                inimigos = Inimigos(),
                monstro = Monstro(),
                vida = Motor.Vida,
                vidaCheia = Motor.VIDA_CHEIA,
                golpes = Motor.Golpes,
                tempoReal = true,
                destravado = Destravado(),
                config = Config()
            };
        }

        // O que o aluno escreveu no MinhaConfig.cs. Toda cor passa por uma
        // peneira: se ele digitar torto, o jogo usa a de fabrica em vez de
        // ficar preto e parecer defeito.
        private static object Config()
        {
            return new
            {
                jogador = Texto(MinhaConfig.Jogador, ""),
                semente = MinhaConfig.Semente,
                roupa = Cor(MinhaConfig.CorDaRoupa, "#4C6BD9"),
                pele = Cor(MinhaConfig.CorDaPele, "#EBC29A"),
                cabelo = Cor(MinhaConfig.CorDoCabelo, "#5C3A26"),
                calca = Cor(MinhaConfig.CorDaCalca, "#2B3A6B"),
                ceu = Cor(MinhaConfig.CorDoCeu, "#6BA3D8")
            };
        }

        private static string Texto(string v, string padrao)
        {
            if (v == null) { return padrao; }
            v = v.Trim();
            if (v == "" || v == "coloque o seu nome aqui") { return padrao; }
            return v.Length > 24 ? v.Substring(0, 24) : v;
        }

        private static string Cor(string v, string padrao)
        {
            if (v == null || v.Length != 7 || v[0] != '#') { return padrao; }

            for (int i = 1; i < 7; i++)
            {
                char c = char.ToLowerInvariant(v[i]);
                bool ok = (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f');
                if (!ok) { return padrao; }
            }
            return v;
        }

        private static object Resposta(string mensagem)
        {
            return new
            {
                jogador = new { x = Motor.Jogador.X, y = Motor.Jogador.Y, z = Motor.Jogador.Z },
                inimigos = Inimigos(),
                vida = Motor.Vida,
                vidaCheia = Motor.VIDA_CHEIA,
                golpes = Motor.Golpes,
                pegou = Motor.Pegou,
                quemPegou = Motor.QuemPegou,
                mudou = Motor.Mudancas,
                recado = Motor.Recado,
                mensagem = mensagem ?? "",
                destravado = Destravado()
            };
        }

        private static object Inimigos()
        {
            List<object> lista = new List<object>();

            foreach (Inimigo i in Motor.Inimigos)
            {
                lista.Add(new
                {
                    desenho = i.Desenho,
                    x = i.Onde.X,
                    y = i.Onde.Y,
                    z = i.Onde.Z
                });
            }
            return lista;
        }

        // O desenho que o aluno fez no MeuMonstro.cs, ja peneirado, indo
        // para o navegador virar cubinhos.
        private static object Monstro()
        {
            return new
            {
                nome = Desenhos.NomeDoAluno(),
                cores = Desenhos.CoresDoAluno(),
                andares = Desenhos.AndaresDoAluno()
            };
        }

        private static object Destravado()
        {
            return new
            {
                mover = Motor.Resolvido("mover"),
                pular = Motor.Resolvido("pular"),
                minerar = Motor.Resolvido("minerar"),
                cavar = Motor.Resolvido("cavar"),
                colocar = Motor.Resolvido("colocar"),
                blocos = Motor.Resolvido("blocos"),
                inimigos = Motor.Resolvido("inimigos"),
                perseguir = Motor.Resolvido("perseguir")
            };
        }
    }

    // Um comando so, com todos os campos possiveis. E feio, e e de
    // proposito: com uma classe por endpoint seriam seis arquivos a mais
    // para o aluno tropecar.
    public class Comando
    {
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }
        public int dx { get; set; }
        public int dz { get; set; }
        public int forca { get; set; }
        public int picareta { get; set; }
        public int profundidade { get; set; }
        public int semente { get; set; }
        public string tipo { get; set; }
    }
}
