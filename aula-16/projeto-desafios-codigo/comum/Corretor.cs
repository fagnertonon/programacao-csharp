using System;
using System.Collections.Generic;

namespace Conecta
{
    // =====================================================================
    // O CORRETOR. JA ESTA PRONTO - voce nao mexe aqui.
    //
    // Ele CHAMA o metodo que voce escreveu no Desafios.cs, com valores que
    // ele mesmo monta, e compara o que voltou com o que era esperado.
    //
    // Nao ha adivinhacao e nao ha "parece certo": ou o seu metodo devolveu
    // o valor esperado, ou nao devolveu.
    //
    // Se o seu metodo estourar - dividir por zero, passar da ultima
    // posicao, mexer em algo nulo -, o corretor nao morre junto: ele mostra
    // o erro na linha do teste.
    // =====================================================================

    public class Resultado
    {
        public string Descricao;
        public string Esperado;
        public string Obtido;
        public bool Passou;

        public Resultado(string descricao, string esperado, string obtido)
        {
            Descricao = descricao;
            Esperado = esperado;
            Obtido = obtido;
            Passou = esperado == obtido;
        }
    }

    public static class Corretor
    {
        public static List<Resultado> Conferir(int numero)
        {
            try
            {
                return Rodar(numero);
            }
            catch (Exception ex)
            {
                // O metodo do aluno estourou. Isso e resultado, nao acidente.
                List<Resultado> saida = new List<Resultado>();
                saida.Add(new Resultado(
                    "o seu metodo estourou durante o teste",
                    "nenhum erro",
                    ex.GetType().Name + ": " + ex.Message));
                return saida;
            }
        }

        /// <summary>Quantos testes passaram, deste desafio.</summary>
        public static int Passaram(int numero)
        {
            int quantos = 0;
            foreach (Resultado r in Conferir(numero))
            {
                if (r.Passou) quantos++;
            }
            return quantos;
        }

        public static bool Fechou(int numero)
        {
            List<Resultado> r = Conferir(numero);
            return r.Count > 0 && Passaram(numero) == r.Count;
        }

        // -----------------------------------------------------------------
        // Os dados de teste. Datas fixas, nunca DateTime.Now: um teste que
        // depende do relogio da maquina e um teste que um dia falha sozinho.
        // -----------------------------------------------------------------
        private static readonly DateTime D1 = new DateTime(2026, 8, 25, 18, 10, 0);
        private static readonly DateTime D2 = new DateTime(2026, 8, 25, 19, 30, 0);
        private static readonly DateTime D3 = new DateTime(2026, 8, 25, 20, 45, 0);

        private static Recado Novo(int id, string autor, string texto, DateTime quando)
        {
            Recado r = new Recado();
            r.Id = id;
            r.Autor = autor;
            r.Texto = texto;
            r.DataHora = quando;
            return r;
        }

        private static List<Recado> MuralDeTeste()
        {
            List<Recado> m = new List<Recado>();
            m.Add(Novo(1, "Ana", "alguem tem a Apostila da atividade 2?", D1));
            m.Add(Novo(2, "Bruno", "a entrega e sexta, dia 28", D2));
            m.Add(Novo(3, "Ana", "achei a apostila, obrigada", D3));
            return m;
        }

        private static string Texto(object o)
        {
            return o == null ? "null" : o.ToString();
        }

        private static string Ficha(Recado r)
        {
            return r == null ? "null" : (r.Id + " / " + r.Autor + " / " + r.Texto);
        }

        private static string Nomes(List<Recado> lista)
        {
            if (lista == null) return "null";
            if (lista.Count == 0) return "(lista vazia)";

            string saida = "";
            foreach (Recado r in lista)
            {
                if (saida.Length > 0) saida += ", ";
                saida += r.Texto;
            }
            return saida;
        }

        // -----------------------------------------------------------------
        private static List<Resultado> Rodar(int numero)
        {
            List<Resultado> t = new List<Resultado>();

            if (numero == 1)
            {
                Recado a = Desafios.CriarRecado("Ana", "oi");
                t.Add(new Resultado("CriarRecado(\"Ana\", \"oi\") devolve um recado",
                    "nao e null", a == null ? "null" : "nao e null"));
                t.Add(new Resultado("... com o Autor que chegou por parametro",
                    "Ana", a == null ? "null" : Texto(a.Autor)));
                t.Add(new Resultado("... e com o Texto que chegou por parametro",
                    "oi", a == null ? "null" : Texto(a.Texto)));

                Recado b = Desafios.CriarRecado("Bruno", "tchau");
                t.Add(new Resultado("CriarRecado(\"Bruno\", ...) NAO pode sair como Ana",
                    "Bruno", b == null ? "null" : Texto(b.Autor)));
            }
            else if (numero == 2)
            {
                t.Add(new Resultado("Descrever de Ana / oi",
                    "Ana: oi", Texto(Desafios.Descrever(Novo(1, "Ana", "oi", D1)))));
                t.Add(new Resultado("Descrever de Bruno / a entrega e sexta",
                    "Bruno: a entrega e sexta",
                    Texto(Desafios.Descrever(Novo(2, "Bruno", "a entrega e sexta", D2)))));
            }
            else if (numero == 3)
            {
                t.Add(new Resultado("Saudacao(0)", "Mural vazio", Texto(Desafios.Saudacao(0))));
                t.Add(new Resultado("Saudacao(1)", "1 recado", Texto(Desafios.Saudacao(1))));
                t.Add(new Resultado("Saudacao(7)", "7 recados", Texto(Desafios.Saudacao(7))));
                t.Add(new Resultado("Saudacao(2)", "2 recados", Texto(Desafios.Saudacao(2))));
            }
            else if (numero == 4)
            {
                t.Add(new Resultado("NomeValido(\"Ana\")", "True", Texto(Desafios.NomeValido("Ana"))));
                t.Add(new Resultado("NomeValido(\"Al\")", "False", Texto(Desafios.NomeValido("Al"))));
                t.Add(new Resultado("NomeValido(\"   \") - so espacos", "False",
                    Texto(Desafios.NomeValido("   "))));
                t.Add(new Resultado("NomeValido(\"  Bia  \") - com espacos nas pontas",
                    "True", Texto(Desafios.NomeValido("  Bia  "))));
            }
            else if (numero == 5)
            {
                List<Recado> m = new List<Recado>();
                int id1 = Desafios.Gravar(m, Novo(0, "Ana", "primeiro", D1));
                t.Add(new Resultado("o primeiro recado recebe o Id 1", "1", Texto(id1)));
                t.Add(new Resultado("... e o mural fica com 1 recado", "1", Texto(m.Count)));

                int id2 = Desafios.Gravar(m, Novo(0, "Bruno", "segundo", D2));
                t.Add(new Resultado("o segundo recebe o Id 2", "2", Texto(id2)));
                t.Add(new Resultado("... e o mural fica com 2", "2", Texto(m.Count)));
            }
            else if (numero == 6)
            {
                List<Recado> saida = Desafios.Listar(MuralDeTeste());
                t.Add(new Resultado("Listar devolve os 3, do mais novo para o mais velho",
                    "achei a apostila, obrigada, a entrega e sexta, dia 28, "
                    + "alguem tem a Apostila da atividade 2?", Nomes(saida)));

                List<Recado> vazio = Desafios.Listar(new List<Recado>());
                t.Add(new Resultado("Listar de um mural vazio devolve lista vazia",
                    "(lista vazia)", Nomes(vazio)));
            }
            else if (numero == 7)
            {
                t.Add(new Resultado("Procurar \"apostila\" - dois recados falam nela",
                    "2", Texto(Desafios.Procurar(MuralDeTeste(), "apostila"))));
                t.Add(new Resultado("Procurar \"APOSTILA\" - maiuscula nao pode mudar nada",
                    "2", Texto(Desafios.Procurar(MuralDeTeste(), "APOSTILA"))));
                t.Add(new Resultado("Procurar \"prova\" - ninguem falou nisso",
                    "0", Texto(Desafios.Procurar(MuralDeTeste(), "prova"))));
                t.Add(new Resultado("Procurar num mural vazio",
                    "0", Texto(Desafios.Procurar(new List<Recado>(), "apostila"))));
            }
            else if (numero == 8)
            {
                t.Add(new Resultado("PrimeiroDoAutor \"Ana\" - ela tem dois, vale o primeiro",
                    "1 / Ana / alguem tem a Apostila da atividade 2?",
                    Ficha(Desafios.PrimeiroDoAutor(MuralDeTeste(), "Ana"))));
                t.Add(new Resultado("PrimeiroDoAutor \"Bruno\" - ele nao e o primeiro da lista",
                    "2 / Bruno / a entrega e sexta, dia 28",
                    Ficha(Desafios.PrimeiroDoAutor(MuralDeTeste(), "Bruno"))));
                t.Add(new Resultado("PrimeiroDoAutor \"Carlos\" - nao existe",
                    "null", Ficha(Desafios.PrimeiroDoAutor(MuralDeTeste(), "Carlos"))));
                t.Add(new Resultado("PrimeiroDoAutor num mural vazio",
                    "null", Ficha(Desafios.PrimeiroDoAutor(new List<Recado>(), "Ana"))));
            }
            else if (numero == 9)
            {
                t.Add(new Resultado("Resumir(\"abcdefgh\", 5)", "abcde...",
                    Texto(Desafios.Resumir("abcdefgh", 5))));
                t.Add(new Resultado("Resumir(\"abc\", 5) - menor que o limite, nao corta",
                    "abc", Texto(Desafios.Resumir("abc", 5))));
                t.Add(new Resultado("Resumir(\"abcde\", 5) - do tamanho exato, nao corta",
                    "abcde", Texto(Desafios.Resumir("abcde", 5))));
            }
            else if (numero == 10)
            {
                t.Add(new Resultado("MaisRecente - o das 20h45",
                    "3 / Ana / achei a apostila, obrigada",
                    Ficha(Desafios.MaisRecente(MuralDeTeste()))));
                t.Add(new Resultado("MaisRecente de um mural vazio",
                    "null", Ficha(Desafios.MaisRecente(new List<Recado>()))));
            }

            return t;
        }
    }
}
