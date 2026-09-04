using System;
using System.Collections.Generic;

namespace MundoDeCubos
{
    // ==================================================================
    //  OS DESENHOS DE MONSTRO. Voce NAO precisa mexer aqui.
    //
    //  Quatro monstros vem de fabrica, e o quinto e o SEU - aquele que
    //  voce desenhou no MeuMonstro.cs.
    //
    //  Este arquivo tambem e a peneira do seu desenho: se voce escrever
    //  uma linha maior que a outra, esquecer uma cor ou passar do
    //  tamanho, ele arruma e o jogo continua. Um desenho torto nunca
    //  derruba o programa - so fica torto na tela, que e metade da graca.
    // ==================================================================
    public static class Desenhos
    {
        public const int LARGURA_MAX = 8;
        public const int FUNDO_MAX = 8;
        public const int ANDARES_MAX = 10;

        /// <summary>Os nomes que o DESAFIO 7 aceita.</summary>
        public static List<string> Nomes()
        {
            List<string> n = new List<string>();
            n.Add("gosma");
            n.Add("fantasma");
            n.Add("aranha");
            n.Add("robo");
            n.Add("meumonstro");
            return n;
        }

        public static bool Existe(string nome)
        {
            foreach (string n in Nomes())
            {
                if (n == nome) { return true; }
            }
            return false;
        }

        // ------------------------------------------------------------------
        //  O DESENHO DO ALUNO, peneirado
        // ------------------------------------------------------------------

        /// <summary>
        /// Le o MeuMonstro.cs e devolve os andares ja arrumados: todas as
        /// linhas do mesmo tamanho, nada maior que o limite, e so as
        /// letras que existem.
        /// </summary>
        public static List<List<string>> AndaresDoAluno()
        {
            List<List<string>> andares = new List<List<string>>();
            List<string> atual = new List<string>();

            string[] cru = MeuMonstro.Desenho;
            if (cru == null) { return Fallback(); }

            foreach (string linhaCrua in cru)
            {
                string linha = (linhaCrua ?? "").Trim();

                if (linha == "---")
                {
                    if (atual.Count > 0) { andares.Add(atual); }
                    atual = new List<string>();
                    continue;
                }

                if (linha == "") { continue; }
                if (atual.Count >= FUNDO_MAX) { continue; }

                atual.Add(Limpar(linha));
            }

            if (atual.Count > 0) { andares.Add(atual); }
            if (andares.Count == 0) { return Fallback(); }

            while (andares.Count > ANDARES_MAX) { andares.RemoveAt(andares.Count - 1); }

            Emparelhar(andares);
            return andares;
        }

        /// <summary>As quatro cores do aluno, ja conferidas.</summary>
        public static Dictionary<string, string> CoresDoAluno()
        {
            Dictionary<string, string> c = new Dictionary<string, string>();
            c["a"] = Cor(MeuMonstro.CorA, "#B02A3C");
            c["b"] = Cor(MeuMonstro.CorB, "#241633");
            c["c"] = Cor(MeuMonstro.CorC, "#FFD84D");
            c["d"] = Cor(MeuMonstro.CorD, "#8B5FBF");
            return c;
        }

        public static string NomeDoAluno()
        {
            string n = (MeuMonstro.Nome ?? "").Trim();
            if (n == "") { return "meu monstro"; }
            return n.Length > 20 ? n.Substring(0, 20) : n;
        }

        // ------------------------------------------------------------------

        // So as letras a, b, c, d e o ponto sobrevivem. Qualquer outra
        // coisa - um X, um espaco no meio, um acento - vira vazio.
        private static string Limpar(string linha)
        {
            string saida = "";
            int quantos = linha.Length > LARGURA_MAX ? LARGURA_MAX : linha.Length;

            for (int i = 0; i < quantos; i++)
            {
                char ch = char.ToLowerInvariant(linha[i]);
                if (ch == 'a' || ch == 'b' || ch == 'c' || ch == 'd') { saida += ch; }
                else { saida += '.'; }
            }
            return saida;
        }

        // Todas as linhas de todos os andares ficam com a MESMA largura, e
        // todos os andares com o mesmo numero de linhas. Sem isso o monstro
        // sairia rasgado na tela quando o aluno digitasse uma linha a menos.
        private static void Emparelhar(List<List<string>> andares)
        {
            int largura = 1;
            int fundo = 1;

            foreach (List<string> andar in andares)
            {
                if (andar.Count > fundo) { fundo = andar.Count; }
                foreach (string linha in andar)
                {
                    if (linha.Length > largura) { largura = linha.Length; }
                }
            }

            foreach (List<string> andar in andares)
            {
                for (int i = 0; i < andar.Count; i++)
                {
                    andar[i] = andar[i].PadRight(largura, '.');
                }
                while (andar.Count < fundo) { andar.Add(new string('.', largura)); }
            }
        }

        // Se o aluno apagar o desenho inteiro, ele ganha um cubinho simples
        // em vez de um monstro invisivel que parece defeito.
        private static List<List<string>> Fallback()
        {
            List<List<string>> a = new List<List<string>>();
            List<string> andar = new List<string>();
            andar.Add("aaaa");
            andar.Add("aaaa");
            andar.Add("aaaa");
            andar.Add("aaaa");
            a.Add(andar);
            return a;
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
    }
}
