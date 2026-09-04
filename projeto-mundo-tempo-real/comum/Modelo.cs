using System;
using System.Collections.Generic;

namespace MundoDeCubos
{
    // ==================================================================
    //  AS TRES PECINHAS DO JOGO: Bloco, Pos e Mundo.
    //
    //  Voce NAO escreve nada aqui. Este arquivo existe para os seus
    //  metodos terem com o que conversar.
    // ==================================================================

    /// <summary>
    /// Um TIPO de bloco: o nome dele, a cor com que aparece na tela, e a
    /// dureza (quanto de picareta precisa para quebrar).
    ///
    /// Repare que voce nunca escreve "new Bloco(...)" com valores dentro
    /// dos parenteses - isso e construtor, e a turma nao viu construtor.
    /// Voce usa Bloco.Novo(...), que e um metodo estatico como todos os
    /// outros que voce ja escreveu.
    /// </summary>
    public class Bloco
    {
        public string Nome { get; set; }
        public string Cor { get; set; }
        public int Dureza { get; set; }

        /// <summary>
        /// A fabrica de blocos. E ela que voce usa no DESAFIO 6.
        ///
        ///     Bloco.Novo("obsidiana", "#241633", 5)
        ///
        /// nome   - como o bloco se chama, em minusculas e sem espaco
        /// cor    - em hexadecimal, do jeito que a web escreve: "#RRGGBB"
        /// dureza - de 1 a 5. Quanto maior, mais dificil de minerar.
        /// </summary>
        public static Bloco Novo(string nome, string cor, int dureza)
        {
            Bloco b = new Bloco();
            b.Nome = nome;
            b.Cor = cor;
            b.Dureza = dureza;
            return b;
        }
    }

    /// <summary>
    /// Um lugar no mundo: X para os lados, Y para cima, Z para o fundo.
    ///
    /// Y = 0 e o fundo do mundo. Y cresce para CIMA, como em toda escada.
    /// </summary>
    public class Pos
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        /// <summary>A fabrica de posicoes. Use Pos.Nova(x, y, z).</summary>
        public static Pos Nova(int x, int y, int z)
        {
            Pos p = new Pos();
            p.X = x;
            p.Y = y;
            p.Z = z;
            return p;
        }

        public override string ToString()
        {
            return X + "/" + Y + "/" + Z;
        }
    }

    /// <summary>
    /// Um monstro solto no mundo.
    ///
    /// O DESENHO dele e so um nome: "gosma", "fantasma", "aranha", "robo"
    /// ou "meumonstro" - este ultimo e o que VOCE desenha, no arquivo
    /// MeuMonstro.cs.
    ///
    /// Voce nunca escreve o Y: o motor solta o monstro do ceu e deixa ele
    /// cair ate o chao, para ele nunca nascer enterrado na pedra.
    /// </summary>
    public class Inimigo
    {
        public string Desenho { get; set; }
        public Pos Onde { get; set; }

        /// <summary>
        /// A fabrica de monstros. E ela que voce usa no DESAFIO 7.
        ///
        ///     Inimigo.Novo("gosma", 6, 18)
        ///
        /// desenho - "gosma", "fantasma", "aranha", "robo" ou "meumonstro"
        /// x, z    - onde ele comeca, de 0 a 23
        /// </summary>
        public static Inimigo Novo(string desenho, int x, int z)
        {
            Inimigo i = new Inimigo();
            i.Desenho = desenho;
            i.Onde = Pos.Nova(x, 0, z);
            return i;
        }
    }

    /// <summary>
    /// O MUNDO. Uma caixa de 24 x 20 x 24 cubinhos.
    ///
    /// Voce nunca ve como os blocos estao guardados por dentro - e de
    /// proposito. Guardar cubo em tres dimensoes precisa de MATRIZ, e a
    /// turma nao viu matriz. Entao o Mundo guarda, e voce conversa com
    /// ele por metodos: Bloco(), Por(), Tirar(), Vazio(), Dentro().
    ///
    /// Isso tem nome, e voce ja viu: e ENCAPSULAMENTO. O mesmo motivo
    /// pelo qual a senha do Usuario, ontem, nao era um campo publico.
    /// </summary>
    public class Mundo
    {
        public const int LARGURA = 24;   // eixo X
        public const int ALTURA = 20;   // eixo Y
        public const int FUNDO = 24;   // eixo Z

        // A matriz que voce nao precisa enxergar.
        private readonly string[,,] grade = new string[LARGURA, ALTURA, FUNDO];

        // Os tipos de bloco que existem neste mundo. No comeco sao os
        // cinco de fabrica; depois do DESAFIO 6, os seus entram aqui.
        private readonly List<Bloco> tipos = new List<Bloco>();

        // ------------------------------------------------------------------
        //  PERGUNTAR
        // ------------------------------------------------------------------

        /// <summary>
        /// Esta posicao existe dentro da caixa do mundo?
        ///
        /// Pergunte ISTO antes de qualquer outra coisa. Pedir um bloco
        /// fora da caixa e o erro mais comum deste projeto.
        /// </summary>
        public bool Dentro(int x, int y, int z)
        {
            if (x < 0 || x >= LARGURA) { return false; }
            if (y < 0 || y >= ALTURA) { return false; }
            if (z < 0 || z >= FUNDO) { return false; }
            return true;
        }

        /// <summary>
        /// O nome do bloco que esta nesta posicao.
        ///
        /// Devolve "" (texto vazio) quando a posicao esta VAZIA, e tambem
        /// quando esta FORA do mundo. Nunca estoura.
        /// </summary>
        public string Bloco(int x, int y, int z)
        {
            if (!Dentro(x, y, z)) { return ""; }
            string t = grade[x, y, z];
            return t == null ? "" : t;
        }

        /// <summary>
        /// Da para passar por aqui? So se estiver dentro do mundo E sem
        /// bloco nenhum.
        ///
        /// CUIDADO: fora do mundo NAO e vazio. Se fosse, o jogador andaria
        /// para fora da caixa e cairia para sempre.
        /// </summary>
        public bool Vazio(int x, int y, int z)
        {
            if (!Dentro(x, y, z)) { return false; }

            string t = Bloco(x, y, z);

            // A AGUA e a FOLHA deixam passar.
            //
            // A agua, porque se contasse como parede o jogador andaria por
            // cima do lago como se fosse chao - e a primeira coisa que a
            // turma faz e correr para a agua para ver.
            //
            // A folha, por um motivo que so apareceu testando: a copa de
            // uma arvore e uma parede na altura da CABECA. Quem entra
            // embaixo dela fica preso, e com os monstros do desafio 8 isso
            // e pior ainda - o bicho enrosca na copa e a perseguicao
            // parece quebrada. Atravessando, todo mundo passa por baixo da
            // arvore, que e o que qualquer um esperaria.
            return t == "" || t == "agua" || t == "folha";
        }

        /// <summary>A dureza do tipo de bloco. Tipo desconhecido vale 1.</summary>
        public int Dureza(string tipo)
        {
            foreach (Bloco b in tipos)
            {
                if (b.Nome == tipo) { return b.Dureza; }
            }
            return 1;
        }

        public List<Bloco> Tipos { get { return tipos; } }

        // ------------------------------------------------------------------
        //  MEXER
        // ------------------------------------------------------------------

        /// <summary>Poe um bloco. Fora do mundo, nao faz nada.</summary>
        public void Por(int x, int y, int z, string tipo)
        {
            if (!Dentro(x, y, z)) { return; }
            grade[x, y, z] = tipo;
        }

        /// <summary>Tira o bloco, deixando ar. Fora do mundo, nao faz nada.</summary>
        public void Tirar(int x, int y, int z)
        {
            if (!Dentro(x, y, z)) { return; }
            grade[x, y, z] = "";
        }

        public void RegistrarTipo(Bloco b)
        {
            if (b == null || string.IsNullOrEmpty(b.Nome)) { return; }

            for (int i = 0; i < tipos.Count; i++)
            {
                if (tipos[i].Nome == b.Nome) { tipos[i] = b; return; }
            }
            tipos.Add(b);
        }
    }
}
