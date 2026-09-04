using System;
using System.Collections.Generic;

namespace MundoDeCubos
{
    /// <summary>
    /// ================================================================
    ///  MUNDO DE CUBOS - os 8 desafios
    /// ================================================================
    ///
    /// ESTE E O UNICO ARQUIVO QUE VOCE ESCREVE.
    ///
    /// COMO FUNCIONA:
    ///   1. Escreva o metodo.
    ///   2. Salve, PARE o programa (o quadrado vermelho) e rode de novo
    ///      com F5.
    ///   3. Se os testes passarem, a tecla daquele desafio comeca a
    ///      funcionar no mundo 3D.
    ///
    /// O MUNDO E UMA CAIXA DE CUBINHOS:
    ///
    ///        Y (para cima, ate 19)
    ///        |
    ///        |    Z (para o fundo, ate 23)
    ///        |   /
    ///        |  /
    ///        | /
    ///        +--------- X (para os lados, ate 23)
    ///
    ///   Y = 0 e o bedrock, o fundo do mundo, e ele nao quebra.
    ///   O jogador ocupa DUAS casas: os pes em Y e a cabeca em Y + 1.
    ///
    /// COMO CONVERSAR COM O MUNDO (voce nunca mexe numa matriz):
    ///
    ///   mundo.Dentro(x, y, z)      essa casa existe na caixa?
    ///   mundo.Vazio(x, y, z)       da para passar por ela?
    ///   mundo.Bloco(x, y, z)       o nome do bloco, ou "" se for ar
    ///   mundo.Dureza("pedra")      quanto de picareta esse tipo pede
    ///   mundo.Por(x, y, z, tipo)   poe um bloco
    ///   mundo.Tirar(x, y, z)       tira o bloco
    ///
    ///   Pos.Nova(x, y, z)               faz um lugar novo
    ///   Bloco.Novo(nome, cor, dureza)   faz um tipo de bloco novo
    ///   Inimigo.Novo(desenho, x, z)     faz um monstro novo
    ///
    /// TRES REGRAS DESTE ARQUIVO:
    ///   1. Nenhum metodo daqui sorteia nada. O que era para ser acaso
    ///      chega por parametro. Sorteio nao se testa.
    ///   2. Nenhum metodo daqui desenha nada. Quem desenha e o navegador.
    ///      Todo metodo RECEBE valores e DEVOLVE resposta.
    ///   3. Nenhum metodo chama outro deste arquivo. Se o Cavar chamasse
    ///      o Minerar, um erro no Minerar pintaria o Cavar de vermelho -
    ///      e o diagnostico na tela estaria mentindo para voce.
    /// </summary>
    public static class Desafios
    {
        // =============================================================
        //  DESAFIO 1 - Mover                                    [ if ]
        //  DESTRAVA: o boneco anda com W A S D
        // =============================================================

        /// <summary>
        /// Para onde o jogador vai quando anda dx no eixo X e dz no eixo Z.
        ///
        /// Quatro perguntas, nesta ordem:
        ///
        ///   1. A casa de destino existe na caixa do mundo?
        ///      Se nao, devolva a posicao que voce recebeu, sem mudar.
        ///
        ///   2. Da para passar reto? Precisa de ar nas DUAS casas:
        ///      a dos pes (Y) e a da cabeca (Y + 1).
        ///
        ///   3. Tem bloco de um cubo na frente, mas cabe subir o degrau?
        ///      Precisa de ar em Y + 1 e Y + 2. Se cabe, ele sobe: a nova
        ///      posicao tem Y + 1.
        ///
        ///   4. Nada disso? E parede: devolva a posicao que recebeu.
        ///
        /// Para devolver um lugar novo, use Pos.Nova(x, y, z).
        /// </summary>
        public static Pos Mover(Mundo mundo, Pos onde, int dx, int dz)
        {
            return onde;   // <<< TROQUE ESTA LINHA pela sua
        }

        // =============================================================
        //  DESAFIO 2 - Pular                                 [ while ]
        //  DESTRAVA: a barra de espaco pula
        // =============================================================

        /// <summary>
        /// Ate que altura Y o jogador sobe quando pula.
        ///
        /// Ele sobe UM cubo por vez, enquanto duas coisas forem verdade
        /// ao mesmo tempo (use &&):
        ///   - ainda nao subiu 'forca' cubos;
        ///   - a casa ACIMA DA CABECA esta vazia.
        ///
        /// ATENCAO: a casa acima da cabeca e Y + 2, e nao Y + 1. Y sao os
        /// pes, Y + 1 e a cabeca. Quem usa Y + 1 aqui faz o boneco
        /// atravessar o teto - e o defeito classico deste desafio.
        ///
        /// Forca zero devolve o proprio Y, e voce nao precisa de if para
        /// isso: o while testa ANTES de rodar e simplesmente nao entra.
        ///
        /// Devolva o Y final. Quem faz o boneco cair depois e o motor.
        ///
        /// CUIDADO: se voce esquecer de somar 1 no contador dentro do
        /// laco, a condicao nunca fica falsa. O servidor espera 2
        /// segundos e avisa - mas o certo e nao chegar la.
        /// </summary>
        public static int Pular(Mundo mundo, Pos onde, int forca)
        {
            return onde.Y;   // <<< TROQUE ESTA LINHA pela sua
        }

        // =============================================================
        //  DESAFIO 3 - Minerar                              [ switch ]
        //  DESTRAVA: o clique esquerdo quebra bloco
        // =============================================================

        /// <summary>
        /// Quebra o bloco da mira e devolve o NOME do que caiu.
        ///
        /// Quatro recusas primeiro, e a ordem importa - nao da para
        /// perguntar a dureza de um bloco que nem existe:
        ///
        ///   1. a posicao esta fora do mundo        -> devolve ""
        ///   2. nao tem bloco nenhum ali            -> devolve ""
        ///   3. o bloco e "bedrock"                 -> devolve ""
        ///   4. picareta menor que a dureza do tipo -> devolve ""
        ///
        /// Passou pelas quatro? Entao tire o bloco com mundo.Tirar e
        /// decida, num SWITCH, o que caiu:
        ///
        ///   "pedra"  ->  "cascalho"
        ///   "grama"  ->  "terra"
        ///   "folha"  ->  ""          (a folha some, mas o bloco sai)
        ///   default  ->  o proprio tipo
        ///
        /// O default e o que faz os blocos que VOCE inventar no desafio 6
        /// cairem certo, sem voce escrever um case para cada um.
        ///
        /// A dureza voce nao decora: mundo.Dureza(tipo) responde.
        /// </summary>
        public static string Minerar(Mundo mundo, int x, int y, int z, int picareta)
        {
            return "";   // <<< TROQUE ESTA LINHA pela sua
        }

        // =============================================================
        //  DESAFIO 4 - Cavar                                   [ for ]
        //  DESTRAVA: a tecla C abre um poco
        // =============================================================

        /// <summary>
        /// Cava para BAIXO e devolve quantos blocos voce realmente tirou.
        ///
        /// O numero de voltas e conhecido antes de comecar - e a
        /// profundidade. Numero conhecido de voltas e for.
        ///
        /// A casa da vez e y - i: a cada volta voce desce um cubo.
        ///
        /// Duas paradas de emergencia, com return DENTRO do for, e as
        /// duas devolvem o que ja tinha sido cavado (nao zero):
        ///   - a casa saiu do mundo;
        ///   - a casa e "bedrock".
        ///
        /// Casa que ja estava vazia NAO conta. Cavar em cima de uma
        /// caverna pode devolver 3 mesmo com profundidade 5.
        ///
        /// O contador nasce ANTES do for. Se nascer dentro, ele volta
        /// zerado a cada volta - e o compilador nao reclama.
        /// </summary>
        public static int Cavar(Mundo mundo, int x, int y, int z, int profundidade)
        {
            return 0;   // <<< TROQUE ESTA LINHA pela sua
        }

        // =============================================================
        //  DESAFIO 5 - Colocar                                  [ if ]
        //  DESTRAVA: o clique direito coloca bloco
        // =============================================================

        /// <summary>
        /// Coloca o bloco e devolve true. Devolve false sem colocar
        /// quando:
        ///
        ///   1. a posicao esta fora do mundo;
        ///   2. ja tem bloco ali;
        ///   3. o tipo veio vazio ("");
        ///   4. a casa e uma das DUAS que o jogador ocupa - mesmo X,
        ///      mesmo Z, e Y igual a jogador.Y ou a jogador.Y + 1.
        ///
        /// A quarta e a que impede o jogador de se emparedar dentro da
        /// propria pedra.
        ///
        /// Colocar bloco ACIMA da cabeca vale: e assim que se constroi
        /// para cima.
        /// </summary>
        public static bool Colocar(Mundo mundo, int x, int y, int z,
                                   string tipo, Pos jogador)
        {
            return false;   // <<< TROQUE ESTA LINHA pela sua
        }

        // =============================================================
        //  DESAFIO 6 - CriarBlocos              [ List<T> e fabrica ]
        //  DESTRAVA: os SEUS blocos entram na paleta
        // =============================================================

        /// <summary>
        /// Devolva uma lista com DE 1 A 10 blocos inventados por voce.
        ///
        /// Este e o desafio da sua assinatura no jogo. Os blocos de
        /// fabrica sao iguais na tela de todo mundo; estes sao os SEUS.
        ///
        /// Cada bloco tem tres coisas:
        ///
        ///   nome    minusculo, SEM ESPACO, e diferente dos de fabrica
        ///           (bedrock, pedra, terra, grama, areia, agua, tronco,
        ///            folha e cascalho ja existem)
        ///
        ///   cor     hexadecimal da web: um # e seis digitos de 0-9 ou
        ///           A-F. Os dois primeiros sao vermelho, os dois do meio
        ///           verde, os dois ultimos azul.
        ///              #FF0000 vermelho puro     #39FF14 verde neon
        ///              #241633 roxo quase preto  #F2C14E ouro
        ///
        ///   dureza  de 1 a 5. Ela conversa com o desafio 3: um bloco de
        ///           dureza 4 precisa de picareta 4 para quebrar.
        ///
        /// O molde e sempre o mesmo:
        ///
        ///     List&lt;Bloco&gt; meus = new List&lt;Bloco&gt;();
        ///     meus.Add(Bloco.Novo("neon", "#39FF14", 1));
        ///     return meus;
        ///
        /// Repare que voce NAO escreve new Bloco("neon", ...) com valores
        /// dentro dos parenteses. Isso e construtor, e a turma nao viu
        /// construtor. Bloco.Novo e um metodo estatico, igual a todos os
        /// que voce ja escreveu.
        ///
        /// Comece com UM bloco so, rode, veja ele aparecer na paleta - e
        /// so depois va acrescentando os outros.
        /// </summary>
        public static List<Bloco> CriarBlocos()
        {
            List<Bloco> meus = new List<Bloco>();

            // <<< ACRESCENTE OS SEUS BLOCOS AQUI, de 1 a 10

            return meus;
        }

        // =============================================================
        //  DESAFIO 7 - CriarInimigos              [ List<T> e fabrica ]
        //  DESTRAVA: os seus monstros aparecem no mundo
        // =============================================================

        /// <summary>
        /// Devolva uma lista com DE 1 A 6 monstros soltos no mundo.
        ///
        /// E o mesmo molde do desafio 6: cria a lista, vai dando Add,
        /// devolve a lista. So muda a fabrica.
        ///
        ///     Inimigo.Novo(desenho, x, z)
        ///
        /// O DESENHO e um destes cinco nomes:
        ///
        ///     "gosma"       uma bolha verde de olhos pretos
        ///     "fantasma"    branco, com franjas embaixo
        ///     "aranha"      preta, de olhos vermelhos
        ///     "robo"        cinza, com antena
        ///     "meumonstro"  O SEU, aquele que voce desenhou no
        ///                   arquivo MeuMonstro.cs
        ///
        /// O X e o Z vao de 0 a 23, como qualquer casa do mundo. O Y
        /// voce nao escreve: o motor solta o monstro do ceu e deixa cair
        /// ate o chao, para ele nunca nascer enterrado na pedra.
        ///
        /// Um conselho: ponha os monstros LONGE do meio, onde voce nasce.
        /// Os cantos (5, 5) e (19, 19) sao bons.
        ///
        ///     List&lt;Inimigo&gt; meus = new List&lt;Inimigo&gt;();
        ///     meus.Add(Inimigo.Novo("gosma", 5, 5));
        ///     return meus;
        /// </summary>
        public static List<Inimigo> CriarInimigos()
        {
            List<Inimigo> meus = new List<Inimigo>();

            // <<< ACRESCENTE OS SEUS MONSTROS AQUI, de 1 a 6

            return meus;
        }

        // =============================================================
        //  DESAFIO 8 - PerseguirJogador               [ if / else if ]
        //  DESTRAVA: os monstros passam a te cacar
        // =============================================================

        /// <summary>
        /// Para onde o monstro anda para chegar mais perto de voce.
        ///
        /// Ele da UM passo por vez, e nunca na diagonal. A regra e uma
        /// cadeia de if / else if, e a ORDEM dela e a graca:
        ///
        ///   1. voce esta a leste dele  (jogador.X maior)  -> dx = 1
        ///   2. voce esta a oeste dele  (jogador.X menor)  -> dx = -1
        ///   3. ja esta na mesma coluna, e voce ao sul     -> dz = 1
        ///   4. ja esta na mesma coluna, e voce ao norte   -> dz = -1
        ///
        /// Repare no que essa ordem faz: o monstro primeiro se alinha no
        /// eixo X, e SO DEPOIS vem pelo Z. Na tela isso parece
        /// perseguicao de desenho animado - ele contorna e depois avanca.
        /// Se voce trocar a ordem, ele persegue de outro jeito. Nao esta
        /// errado, mas os testes esperam esta.
        ///
        /// Comece com dx e dz valendo ZERO. Se voce ja estiver na mesma
        /// casa que ele, nenhum if pega e ele fica parado.
        ///
        /// Depois de decidir o passo, o resto e IGUALZINHO ao seu Mover,
        /// do desafio 1 - o monstro tem dois cubos de altura e sobe
        /// degrau, exatamente como voce:
        ///
        ///   duas casas livres (pes e cabeca)   -> ele anda
        ///   um cubo na frente com espaco em cima -> ele SOBE o degrau
        ///   nada disso                          -> ele fica onde esta
        ///
        /// Se voce quiser se defender, uma parede de UM cubo nao basta:
        /// ele sobe. Precisa de DOIS - e e assim que o desafio 5 vira a
        /// sua defesa.
        /// </summary>
        public static Pos PerseguirJogador(Mundo mundo, Pos inimigo, Pos jogador)
        {
            return inimigo;   // <<< TROQUE ESTA LINHA pela sua
        }
    }
}
