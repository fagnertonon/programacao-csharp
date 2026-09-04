using System;
using System.Collections.Generic;

namespace MundoDeCubos
{
    // ==================================================================
    //  O CORRETOR. Voce NAO precisa mexer aqui.
    //
    //  Ele monta um mundinho de mentira para cada teste, chama o metodo
    //  que voce escreveu, e compara o resultado com o esperado - como
    //  texto.
    //
    //  NOTA DE MANUTENCAO (para quem cuida do material)
    //
    //  INVARIANTE 1: cada teste monta o PROPRIO mundo, do zero, por um
    //  nome de cenario. Nada do que o jogador fizer na tela do jogo pode
    //  travar ou destravar um desafio - o resultado dos testes e funcao
    //  SO do Desafios.cs compilado.
    //
    //  INVARIANTE 2: nenhum teste chama DOIS metodos do aluno. Se o teste
    //  do Cavar chamasse o Minerar, o Cavar ficaria vermelho por causa de
    //  um erro no Minerar - e o diagnostico na tela estaria mentindo.
    //
    //  INVARIANTE 3: nenhum valor de teste contem o caractere "|", que e
    //  o separador dos argumentos.
    // ==================================================================

    public class ResultadoTeste
    {
        public string Descricao { get; set; }
        public string Esperado { get; set; }
        public string Obtido { get; set; }
        public bool Passou { get; set; }
        public string Erro { get; set; }
    }

    public class ResultadoDesafio
    {
        public string Id { get; set; }
        public int Numero { get; set; }
        public bool Resolvido { get; set; }
        public int Passaram { get; set; }
        public int Total { get; set; }
        public List<ResultadoTeste> Testes { get; set; } = new List<ResultadoTeste>();
    }

    public static class Corretor
    {
        public static ResultadoDesafio Corrigir(Desafio d)
        {
            ResultadoDesafio saida = new ResultadoDesafio();
            saida.Id = d.Id;
            saida.Numero = d.Numero;

            if (d.Testes == null || d.Testes.Count == 0)
            {
                return saida;
            }

            int passaram = 0;
            bool travou = false;

            foreach (Teste t in d.Testes)
            {
                ResultadoTeste r = new ResultadoTeste();
                r.Descricao = t.Descricao;
                r.Esperado = t.Esperado;

                if (travou)
                {
                    r.Obtido = "(nao testei)";
                    r.Erro = "Pulei este teste porque o anterior nao terminou.";
                    r.Passou = false;
                    saida.Testes.Add(r);
                    continue;
                }

                string id = d.Id;
                Teste teste = t;
                Execucao e = Sandbox.Rodar(id, delegate { return Executar(id, teste); });

                if (e.EstourouOTempo)
                {
                    travou = true;
                    r.Obtido = "(nao terminou)";
                    r.Erro = Sandbox.Recado(e);
                    r.Passou = false;
                }
                else if (e.Falha != null)
                {
                    r.Obtido = "(o programa parou)";
                    r.Erro = Sandbox.Traduzir(e.Falha);
                    r.Passou = false;
                }
                else
                {
                    r.Obtido = e.Valor == null ? "(null)" : e.Valor.ToString();
                    r.Passou = (r.Obtido == r.Esperado);
                }

                if (r.Passou) { passaram++; }
                saida.Testes.Add(r);
            }

            saida.Passaram = passaram;
            saida.Total = saida.Testes.Count;
            saida.Resolvido = (passaram == saida.Total && saida.Total > 0);
            return saida;
        }

        // ------------------------------------------------------------------
        //  O DESPACHANTE: transforma o texto do JSON numa chamada de verdade.
        // ------------------------------------------------------------------
        private static object Executar(string id, Teste t)
        {
            string[] p = (t.Entrada ?? "").Split('|');
            Mundo m = Cenario.Montar(t.Cenario);

            if (id == "mover")
            {
                Pos onde = Pos.Nova(N(p, 0), N(p, 1), N(p, 2));
                Pos fim = Desafios.Mover(m, onde, N(p, 3), N(p, 4));
                if (fim == null) { return "(null)"; }
                return fim.ToString();
            }

            if (id == "pular")
            {
                Pos onde = Pos.Nova(N(p, 0), N(p, 1), N(p, 2));
                return Desafios.Pular(m, onde, N(p, 3));
            }

            if (id == "minerar")
            {
                string caiu = Desafios.Minerar(m, N(p, 0), N(p, 1), N(p, 2), N(p, 3));

                // O que ficou NO LUGAR importa tanto quanto o que caiu: e
                // assim que se pega quem devolve o nome certo sem tirar o
                // bloco, ou quem tira um bloco que nao devia.
                string ficou = m.Bloco(N(p, 0), N(p, 1), N(p, 2));

                // "(nada)" e "ar" em vez de texto vazio: um esperado que
                // comeca com espaco e impossivel de conferir a olho na tela.
                if (caiu == "") { caiu = "(nada)"; }
                if (ficou == "") { ficou = "ar"; }

                return caiu + " / " + ficou;
            }

            if (id == "cavar")
            {
                int quantos = Desafios.Cavar(m, N(p, 0), N(p, 1), N(p, 2), N(p, 3));
                return quantos + " / " + ColunaDe(m, N(p, 0), N(p, 2));
            }

            if (id == "colocar")
            {
                Pos jog = Pos.Nova(N(p, 4), N(p, 5), N(p, 6));
                bool ok = Desafios.Colocar(m, N(p, 0), N(p, 1), N(p, 2), T(p, 3), jog);

                string ficou = m.Bloco(N(p, 0), N(p, 1), N(p, 2));
                if (ficou == "") { ficou = "ar"; }

                return (ok ? "true" : "false") + " / " + ficou;
            }

            if (id == "blocos")
            {
                return ConferirBlocos();
            }

            if (id == "inimigos")
            {
                return ConferirInimigos();
            }

            if (id == "perseguir")
            {
                Pos ini = Pos.Nova(N(p, 0), N(p, 1), N(p, 2));
                Pos jog = Pos.Nova(N(p, 3), N(p, 4), N(p, 5));
                Pos fim = Desafios.PerseguirJogador(m, ini, jog);
                if (fim == null) { return "(null)"; }
                return fim.ToString();
            }

            return "(desafio desconhecido: " + id + ")";
        }

        // A coluna vira texto de baixo para cima, so com a inicial de cada
        // bloco. E o jeito de ver o buraco que o Cavar abriu.
        private static string ColunaDe(Mundo m, int x, int z)
        {
            string s = "";
            for (int y = 0; y < 12; y++)
            {
                string t = m.Bloco(x, y, z);
                s += (t == "") ? "." : t.Substring(0, 1);
            }
            return s;
        }

        // ------------------------------------------------------------------
        //  O DESAFIO 6 nao da para comparar com uma lista fixa: os blocos
        //  sao os do ALUNO. Entao o teste confere o FORMATO e devolve "ok"
        //  ou o motivo da recusa - o mesmo desenho do desafio do nome, na
        //  Aula 13.
        // ------------------------------------------------------------------
        private static string ConferirBlocos()
        {
            List<Bloco> meus = Desafios.CriarBlocos();

            if (meus == null) { return "(devolveu null)"; }
            if (meus.Count < 1) { return "(lista vazia - crie pelo menos 1 bloco)"; }
            if (meus.Count > 10) { return "(sao " + meus.Count + " blocos - o maximo e 10)"; }

            List<string> vistos = new List<string>();

            foreach (Bloco b in meus)
            {
                if (b == null) { return "(um dos blocos e null)"; }

                string nome = (b.Nome ?? "").Trim();
                if (nome == "") { return "(um bloco esta sem nome)"; }
                if (nome.IndexOf(' ') >= 0) { return "(nome com espaco: " + nome + ")"; }

                foreach (string v in vistos)
                {
                    if (v == nome) { return "(nome repetido: " + nome + ")"; }
                }
                foreach (Bloco f in Gerador.BlocosDeFabrica())
                {
                    if (f.Nome == nome) { return "(esse nome ja e de fabrica: " + nome + ")"; }
                }
                vistos.Add(nome);

                if (!CorValida(b.Cor)) { return "(cor invalida em " + nome + ": " + b.Cor + ")"; }
                if (b.Dureza < 1 || b.Dureza > 5)
                {
                    return "(dureza fora de 1 a 5 em " + nome + ": " + b.Dureza + ")";
                }
            }

            return "ok";
        }

        // ------------------------------------------------------------------
        //  O DESAFIO 7, pelo mesmo motivo do 6: os monstros sao do ALUNO,
        //  entao o teste confere o FORMATO e diz qual esta torto.
        // ------------------------------------------------------------------
        private static string ConferirInimigos()
        {
            List<Inimigo> meus = Desafios.CriarInimigos();

            if (meus == null) { return "(devolveu null)"; }
            if (meus.Count < 1) { return "(lista vazia - crie pelo menos 1 monstro)"; }
            if (meus.Count > 6) { return "(sao " + meus.Count + " monstros - o maximo e 6)"; }

            foreach (Inimigo i in meus)
            {
                if (i == null) { return "(um dos monstros e null)"; }
                if (i.Onde == null) { return "(um monstro esta sem lugar)"; }

                string d = (i.Desenho ?? "").Trim();
                if (d == "") { return "(um monstro esta sem desenho)"; }

                if (!Desenhos.Existe(d))
                {
                    return "(desenho desconhecido: " + d + ")";
                }

                if (i.Onde.X < 0 || i.Onde.X >= Mundo.LARGURA)
                {
                    return "(o X de " + d + " esta fora do mundo: " + i.Onde.X + ")";
                }
                if (i.Onde.Z < 0 || i.Onde.Z >= Mundo.FUNDO)
                {
                    return "(o Z de " + d + " esta fora do mundo: " + i.Onde.Z + ")";
                }
            }

            return "ok";
        }

        private static bool CorValida(string cor)
        {
            if (cor == null || cor.Length != 7) { return false; }
            if (cor[0] != '#') { return false; }

            for (int i = 1; i < 7; i++)
            {
                char c = char.ToLowerInvariant(cor[i]);
                bool digito = (c >= '0' && c <= '9');
                bool letra = (c >= 'a' && c <= 'f');
                if (!digito && !letra) { return false; }
            }
            return true;
        }

        private static string T(string[] p, int i)
        {
            return (i < p.Length) ? (p[i] ?? "") : "";
        }

        private static int N(string[] p, int i)
        {
            int n = 0;
            int.TryParse(T(p, i), out n);
            return n;
        }
    }
}
