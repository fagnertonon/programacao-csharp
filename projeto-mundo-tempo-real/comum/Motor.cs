using System;
using System.Collections.Generic;
using System.Threading;

namespace MundoDeCubos
{
    // ==================================================================
    //  O MOTOR. Voce NAO precisa mexer aqui.
    //
    //  Ele guarda o mundo e o jogador, aplica a gravidade, e chama os
    //  SEUS metodos.
    //
    //  ------------------------------------------------------------
    //   A REGRA DE OURO, herdada da bancada da Aula 12:
    //   O JOGO SO CHAMA UM METODO SEU DEPOIS QUE OS TESTES DAQUELE
    //   METODO PASSARAM.
    //
    //   Enquanto nao passaram, a tecla simplesmente nao faz nada e a
    //   tela avisa qual desafio destrava aquilo. Se o jogo chamasse um
    //   metodo ainda vazio, ele quebraria e voce acharia que o erro
    //   era do jogo.
    //  ------------------------------------------------------------
    //
    //  Quem cai e o motor, nao voce: a gravidade e daqui. O seu Mover
    //  decide se da para andar; o seu Pular decide ate onde sobe. Cair
    //  depois disso e trabalho da casa.
    // ==================================================================
    public static class Motor
    {
        private static readonly object trava = new object();

        public static Mundo Mundo;
        public static Pos Jogador;
        public static List<Inimigo> Inimigos = new List<Inimigo>();
        public static int Pegou;          // quantas vezes um monstro te bateu
        public static string QuemPegou = "";

        // ---- O QUE MUDA NESTA VERSAO ----
        // Na versao por turno, os monstros so andavam quando voce andava.
        // Aqui eles tem RELOGIO PROPRIO: um Timer chama o turno deles a
        // cada RITMO_MS, quer voce se mexa ou nao. E dai vem a vida: um
        // bicho que anda sozinho e que nao machuca nao assusta ninguem.
        public const int RITMO_MS = 550;
        public const int VIDA_CHEIA = 10;
        public const int ESPERA_DANO_MS = 900;

        public static int Vida = VIDA_CHEIA;
        public static int Golpes;              // quantos monstros voce derrubou

        private static Timer relogio;
        private static DateTime ultimoDano = DateTime.MinValue;
        public static int Semente = 7;
        public static string Recado = "";

        // O que mudou desde a ultima resposta - so isso viaja para o
        // navegador a cada acao, e nao os 11520 cubos do mundo inteiro.
        public static List<object> Mudancas = new List<object>();

        private static Conteudo conteudo;
        private static readonly Dictionary<string, bool> resolvido =
            new Dictionary<string, bool>();

        // ------------------------------------------------------------------

        public static void Comecar(int semente)
        {
            lock (trava)
            {
                Semente = semente;
                Mundo = Gerador.Criar(semente);
                Recado = "";

                RegistrarBlocosDoAluno();
                Jogador = Nascedouro();
                Pegou = 0;
                Golpes = 0;
                Vida = VIDA_CHEIA;
                QuemPegou = "";
                SoltarInimigos();
            }

            LigarRelogio();
            return;
        }

        // O CORACAO DA VERSAO EM TEMPO REAL.
        //
        // Um Timer do .NET chama o turno dos monstros a cada RITMO_MS. Ele
        // roda numa thread do pool, e por isso TUDO que ele toca esta
        // dentro do mesmo lock do resto do motor - senao dois turnos
        // poderiam mexer no mundo ao mesmo tempo.
        // 0 = livre, 1 = tem um tique rodando agora.
        private static int tiqueEmCurso;

        private static void LigarRelogio()
        {
            if (relogio != null) { return; }

            relogio = new Timer(delegate (object estado)
            {
                // NAO DEIXA UM TIQUE ATROPELAR O OUTRO.
                //
                // O Timer dispara a cada RITMO_MS mesmo que o anterior nao
                // tenha terminado. Um metodo do aluno que passe nos testes
                // e trave em jogo segura o tique por 2 segundos - o prazo
                // do Sandbox -, e sem esta trava quatro tiques ficariam
                // empilhados esperando o lock. Medido: o servidor
                // aguentava, mas a fila nao serve para nada. Aqui o tique
                // atrasado simplesmente desiste da vez.
                if (Interlocked.Exchange(ref tiqueEmCurso, 1) == 1) { return; }

                try { MoverInimigos(); }
                catch (Exception) { /* um tique ruim nao derruba o jogo */ }
                finally { Interlocked.Exchange(ref tiqueEmCurso, 0); }
            }, null, RITMO_MS, RITMO_MS);
        }

        // Nascer dentro do lago e uma estreia ruim. Procura, em circulos
        // a partir do centro, a primeira coluna cujo chao esta ACIMA do
        // nivel da agua. Se a ilha inteira estiver submersa - nao acontece
        // com as sementes que a turma usa, mas pode -, cai no centro
        // mesmo, e o jogador fica com os pes molhados em vez de o jogo
        // nao abrir.
        private static Pos Nascedouro()
        {
            int cx = Mundo.LARGURA / 2;
            int cz = Mundo.FUNDO / 2;

            for (int raio = 0; raio < Mundo.LARGURA / 2; raio++)
            {
                for (int dx = -raio; dx <= raio; dx++)
                {
                    for (int dz = -raio; dz <= raio; dz++)
                    {
                        int x = cx + dx;
                        int z = cz + dz;
                        if (!Mundo.Dentro(x, 1, z)) { continue; }

                        int chao = Gerador.ChaoPisavelEm(Mundo, x, z);
                        if (chao <= Gerador.NIVEL_DA_AGUA) { continue; }
                        if (!Mundo.Vazio(x, chao + 1, z)) { continue; }
                        if (!Mundo.Vazio(x, chao + 2, z)) { continue; }

                        return Pos.Nova(x, chao + 1, z);
                    }
                }
            }

            return Pos.Nova(cx, Gerador.ChaoPisavelEm(Mundo, cx, cz) + 1, cz);
        }

        public static Conteudo Conteudo
        {
            get
            {
                if (conteudo == null) { conteudo = MundoDeCubos.Conteudo.Carregar(); }
                return conteudo;
            }
        }

        /// <summary>
        /// Roda os testes de todos os desafios e guarda quem passou. E daqui
        /// que sai o destravamento: o painel pinta, e o jogo libera a tecla.
        /// </summary>
        public static List<ResultadoDesafio> Corrigir()
        {
            List<ResultadoDesafio> saida = new List<ResultadoDesafio>();

            lock (trava)
            {
                foreach (Desafio d in Conteudo.Desafios)
                {
                    ResultadoDesafio r = Corretor.Corrigir(d);
                    resolvido[d.Id] = r.Resolvido;
                    saida.Add(r);
                }

                RegistrarBlocosDoAluno();
                SoltarInimigos();
            }
            return saida;
        }

        // Os blocos que o aluno inventou entram na paleta do mundo que JA
        // esta em pe - e nao so quando o mundo e criado de novo. Sem isto,
        // resolver o desafio 6 obrigaria a reiniciar, e a construcao que o
        // aluno levou meia hora fazendo iria junto.
        private static void RegistrarBlocosDoAluno()
        {
            if (Mundo == null) { return; }
            if (!Resolvido("blocos")) { return; }

            Execucao e = Sandbox.Rodar("blocos", delegate { return Desafios.CriarBlocos(); });
            List<Bloco> meus = e.Valor as List<Bloco>;
            if (meus == null) { return; }

            foreach (Bloco b in meus) { Mundo.RegistrarTipo(b); }
        }

        public static bool Resolvido(string id)
        {
            bool v;
            return resolvido.TryGetValue(id, out v) && v;
        }

        // ------------------------------------------------------------------
        //  OS MONSTROS
        // ------------------------------------------------------------------

        // Solta no mundo os monstros que o aluno criou - mas so se o
        // desafio 7 estiver resolvido. Ate la o mundo e um lugar de paz.
        private static void SoltarInimigos()
        {
            Inimigos = new List<Inimigo>();
            if (Mundo == null || !Resolvido("inimigos")) { return; }

            Execucao e = Sandbox.Rodar("inimigos",
                delegate { return Desafios.CriarInimigos(); });

            List<Inimigo> dele = e.Valor as List<Inimigo>;
            if (dele == null) { return; }

            foreach (Inimigo i in dele)
            {
                if (i == null || i.Onde == null) { continue; }
                if (!Mundo.Dentro(i.Onde.X, 1, i.Onde.Z)) { continue; }

                // O monstro nasce EM PE, num lugar em que ele CABE - e nao
                // onde o aluno mandou no eixo Y, que ele nem escreve.
                //
                // Se o lugar pedido estiver dentro de uma arvore ou de um
                // morro, o motor procura em volta. Sem isto o bicho nasce
                // no tronco, sobe para a copa e fica preso nas folhas: de
                // fora parece que a perseguicao esta quebrada.
                Pos onde = LugarLivrePerto(i.Onde.X, i.Onde.Z);
                if (onde == null) { continue; }
                i.Onde = onde;

                Inimigos.Add(i);
                if (Inimigos.Count >= 6) { break; }
            }
        }

        // Procura em circulos, a partir do lugar pedido, uma coluna em que
        // um bicho de dois cubos caiba de pe.
        private static Pos LugarLivrePerto(int x, int z)
        {
            for (int raio = 0; raio <= 4; raio++)
            {
                for (int dx = -raio; dx <= raio; dx++)
                {
                    for (int dz = -raio; dz <= raio; dz++)
                    {
                        int px = x + dx;
                        int pz = z + dz;
                        if (!Mundo.Dentro(px, 1, pz)) { continue; }

                        int y = Gerador.ChaoLivreEm(Mundo, px, pz);
                        if (y > 0) { return Pos.Nova(px, y, pz); }
                    }
                }
            }
            return null;
        }

        // O TURNO DOS MONSTROS. Nada se move sozinho neste jogo: eles so
        // andam quando VOCE anda, pula, mina, cava ou coloca. E o que
        // transforma cada tecla numa decisao, sem precisar de relogio.
        private static void MoverInimigos()
        {
            lock (trava)
            {
            if (Mundo == null || Jogador == null) { return; }
            if (!Resolvido("perseguir")) { return; }
            if (Inimigos.Count == 0) { return; }

            foreach (Inimigo i in Inimigos)
            {
                Pos antes = i.Onde;
                Execucao e = Sandbox.Rodar("perseguir",
                    delegate { return Desafios.PerseguirJogador(Mundo, Copia(antes), Copia(Jogador)); });

                if (e.EstourouOTempo) { Recado = Sandbox.Recado(e); return; }
                if (e.Falha != null)
                {
                    Recado = "PerseguirJogador: " + Sandbox.Traduzir(e.Falha);
                    return;
                }

                Pos fim = e.Valor as Pos;

                if (fim != null && Mundo.Dentro(fim.X, fim.Y, fim.Z))
                {
                    // O monstro ESBARRA em voce, mas nao entra na sua casa.
                    // Sem isto ele para em cima do boneco e some da tela -
                    // parece que evaporou, e o jogador nao entende o que
                    // encostou nele.
                    if (fim.X == Jogador.X && fim.Z == Jogador.Z)
                    {
                        Bater(i.Desenho);
                    }
                    else
                    {
                        i.Onde = fim;
                    }
                }

                // O monstro tambem cai, senao ficaria pendurado no ar
                // depois que voce cavasse embaixo dele.
                int guarda = 0;
                while (i.Onde.Y > 1 && Mundo.Vazio(i.Onde.X, i.Onde.Y - 1, i.Onde.Z)
                       && guarda < 64)
                {
                    i.Onde = Pos.Nova(i.Onde.X, i.Onde.Y - 1, i.Onde.Z);
                    guarda++;
                }
            }

            Encostou();
            }
        }

        // Encostar nao mata ninguem - e a ultima aula, nao um jogo de
        // sobrevivencia. So conta e avisa. Quem quiser escapar constroi
        // uma parede: o monstro nao sobe degrau.
        private static void Encostou()
        {
            foreach (Inimigo i in Inimigos)
            {
                if (Vizinho(i.Onde, Jogador)) { Bater(i.Desenho); return; }
            }
        }

        /// <summary>Casas coladas - de lado, nao na diagonal.</summary>
        private static bool Vizinho(Pos a, Pos b)
        {
            int dy = a.Y - b.Y;
            if (dy < -1 || dy > 1) { return false; }

            int dx = a.X - b.X; if (dx < 0) { dx = -dx; }
            int dz = a.Z - b.Z; if (dz < 0) { dz = -dz; }
            return dx + dz <= 1;
        }

        // O GOLPE. A espera entre um e outro existe porque o relogio bate
        // duas vezes por segundo: sem ela, quatro monstros colados tirariam
        // a vida inteira em dois segundos e o jogo seria injusto.
        private static void Bater(string quem)
        {
            if ((DateTime.UtcNow - ultimoDano).TotalMilliseconds < ESPERA_DANO_MS)
            {
                return;
            }
            ultimoDano = DateTime.UtcNow;

            Pegou = Pegou + 1;
            QuemPegou = quem;
            Vida = Vida - 1;

            if (Vida <= 0)
            {
                // Sem tela de "voce morreu": e a ultima aula, nao um jogo
                // de sobrevivencia. Voce volta ao nascedouro com a vida
                // cheia, e os monstros continuam de onde estavam.
                Vida = VIDA_CHEIA;
                Jogador = Nascedouro();
                Recado = "O " + quem + " te derrubou! Voce voltou para a largada.";
            }
        }

        // ------------------------------------------------------------------
        //  AS ACOES DO JOGADOR
        // ------------------------------------------------------------------

        public static void Andar(int dx, int dz)
        {
            lock (trava)
            {
                Mudancas.Clear();
                Recado = "";

                if (!Resolvido("mover"))
                {
                    Recado = "Andar destrava no desafio 1.";
                    return;
                }

                Pos antes = Jogador;
                Execucao e = Sandbox.Rodar("mover",
                    delegate { return Desafios.Mover(Mundo, Copia(antes), dx, dz); });

                Pos fim = Ler(e, antes, "Mover");
                if (fim != null) { Jogador = fim; }

                Cair();
            }
        }

        public static void Saltar(int forca)
        {
            lock (trava)
            {
                Mudancas.Clear();
                Recado = "";

                if (!Resolvido("pular"))
                {
                    Recado = "Pular destrava no desafio 2.";
                    return;
                }

                Pos antes = Jogador;
                Execucao e = Sandbox.Rodar("pular",
                    delegate { return Desafios.Pular(Mundo, Copia(antes), forca); });

                if (e.EstourouOTempo) { Recado = Sandbox.Recado(e); return; }
                if (e.Falha != null) { Recado = "Pular: " + Sandbox.Traduzir(e.Falha); return; }

                int y = e.Valor == null ? antes.Y : (int)e.Valor;
                if (y < 0) { y = 0; }
                if (y > Mundo.ALTURA - 2) { y = Mundo.ALTURA - 2; }

                Jogador = Pos.Nova(antes.X, y, antes.Z);
                Cair();
            }
        }

        public static string Quebrar(int x, int y, int z, int picareta)
        {
            lock (trava)
            {
                Mudancas.Clear();
                Recado = "";

                if (!Resolvido("minerar"))
                {
                    Recado = "Minerar destrava no desafio 3.";
                    return "";
                }

                Execucao e = Sandbox.Rodar("minerar",
                    delegate { return Desafios.Minerar(Mundo, x, y, z, picareta); });

                if (e.EstourouOTempo) { Recado = Sandbox.Recado(e); return ""; }
                if (e.Falha != null) { Recado = "Minerar: " + Sandbox.Traduzir(e.Falha); return ""; }

                Anotar(x, y, z);
                Cair();
                return e.Valor == null ? "" : e.Valor.ToString();
            }
        }

        public static int Poco(int x, int y, int z, int profundidade)
        {
            lock (trava)
            {
                Mudancas.Clear();
                Recado = "";

                if (!Resolvido("cavar"))
                {
                    Recado = "Cavar destrava no desafio 4.";
                    return 0;
                }

                Execucao e = Sandbox.Rodar("cavar",
                    delegate { return Desafios.Cavar(Mundo, x, y, z, profundidade); });

                if (e.EstourouOTempo) { Recado = Sandbox.Recado(e); return 0; }
                if (e.Falha != null) { Recado = "Cavar: " + Sandbox.Traduzir(e.Falha); return 0; }

                for (int i = 0; i <= profundidade; i++) { Anotar(x, y - i, z); }
                Cair();

                return e.Valor == null ? 0 : (int)e.Valor;
            }
        }

        public static bool Por(int x, int y, int z, string tipo)
        {
            lock (trava)
            {
                Mudancas.Clear();
                Recado = "";

                if (!Resolvido("colocar"))
                {
                    Recado = "Colocar bloco destrava no desafio 5.";
                    return false;
                }

                // O tipo tem de EXISTIR neste mundo.
                //
                // Sem esta guarda, um nome desconhecido entrava na grade,
                // o navegador nao achava a cor dele e desenhava ar - e a
                // casa virava uma PAREDE INVISIVEL: solida no C#, vazia na
                // tela. E o pior tipo de defeito num jogo, porque quem joga
                // nao tem como enxergar a causa.
                if (!TipoConhecido(tipo))
                {
                    Recado = "Nao existe bloco chamado \"" + tipo + "\".";
                    return false;
                }

                Pos jog = Copia(Jogador);
                Execucao e = Sandbox.Rodar("colocar",
                    delegate { return Desafios.Colocar(Mundo, x, y, z, tipo, jog); });

                if (e.EstourouOTempo) { Recado = Sandbox.Recado(e); return false; }
                if (e.Falha != null) { Recado = "Colocar: " + Sandbox.Traduzir(e.Falha); return false; }

                Anotar(x, y, z);
                return e.Valor != null && (bool)e.Valor;
            }
        }

        // ------------------------------------------------------------------

        private static bool TipoConhecido(string tipo)
        {
            if (string.IsNullOrEmpty(tipo)) { return false; }

            foreach (Bloco b in Mundo.Tipos)
            {
                if (b.Nome == tipo) { return true; }
            }
            return false;
        }

        private static Pos Ler(Execucao e, Pos antes, string nome)
        {
            if (e.EstourouOTempo) { Recado = Sandbox.Recado(e); return null; }
            if (e.Falha != null) { Recado = nome + ": " + Sandbox.Traduzir(e.Falha); return null; }

            Pos fim = e.Valor as Pos;
            if (fim == null)
            {
                Recado = nome + " devolveu null. Falta um return com Pos.Nova(...)?";
                return null;
            }

            // Cinto de seguranca: se o metodo do aluno devolver um lugar fora
            // da caixa, o jogador nao vai para la. Sem isto o mundo some da
            // tela e parece defeito do jogo.
            if (!Mundo.Dentro(fim.X, fim.Y, fim.Z))
            {
                Recado = nome + " devolveu um lugar fora do mundo: " + fim.ToString();
                return null;
            }
            return fim;
        }

        // O jogador recebe uma COPIA da propria posicao. Se o metodo do aluno
        // mexer no objeto em vez de devolver um novo, o estado do jogo nao
        // muda pelas costas do motor.
        private static Pos Copia(Pos p)
        {
            return Pos.Nova(p.X, p.Y, p.Z);
        }

        private static void Anotar(int x, int y, int z)
        {
            if (!Mundo.Dentro(x, y, z)) { return; }
            Mudancas.Add(new { x = x, y = y, z = z, t = Mundo.Bloco(x, y, z) });
        }

        // A GRAVIDADE, e ela e do motor. A guarda de 64 voltas existe porque
        // este while roda no servidor: sem ela, um mundo estranho penduraria
        // a resposta HTTP para sempre.
        private static void Cair()
        {
            int guarda = 0;

            while (Jogador.Y > 1
                   && Mundo.Vazio(Jogador.X, Jogador.Y - 1, Jogador.Z)
                   && guarda < 64)
            {
                Jogador = Pos.Nova(Jogador.X, Jogador.Y - 1, Jogador.Z);
                guarda++;
            }
        }
    }
}
