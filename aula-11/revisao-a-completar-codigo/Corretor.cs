using System;
using System.Collections.Generic;
using System.Globalization;

namespace Revisao
{
    // O corretor: pega cada teste descrito no conteudo-revisao.json,
    // chama o metodo que voce escreveu em Desafios.cs e compara o
    // resultado com o esperado.
    //
    // Voce NAO precisa mexer aqui.

    public class ResultadoTeste
    {
        public string Descricao;
        public string Esperado;
        public string Obtido;
        public bool Passou;
        public string Erro;      // preenchido quando o codigo do aluno estourou
    }

    public class ResultadoTopico
    {
        public List<ResultadoTeste> Testes = new List<ResultadoTeste>();
        public bool Resolvido;
        public int Passaram;

        // Nao existe estado "ainda nao comecou": um metodo vazio devolve o valor
        // padrao do tipo (0, false, "") e isso faz alguns testes passarem por
        // acidente. Contar quantos passaram e a unica leitura honesta.
        public int Total { get { return Testes.Count; } }
    }

    public static class Corretor
    {
        public static ResultadoTopico Corrigir(Topico topico)
        {
            ResultadoTopico saida = new ResultadoTopico();

            if (topico.Desafio == null || topico.Desafio.Testes == null)
            {
                saida.Resolvido = false;
                return saida;
            }

            int passaram = 0;

            foreach (Teste teste in topico.Desafio.Testes)
            {
                ResultadoTeste r = new ResultadoTeste();
                r.Descricao = teste.Descricao;
                r.Esperado = teste.Esperado;

                try
                {
                    r.Obtido = Executar(topico.Id, teste.Entrada);
                    r.Passou = (r.Obtido == r.Esperado);
                }
                catch (Exception ex)
                {
                    // Erro em tempo de execucao no codigo do aluno.
                    // O mais comum e o NullReferenceException do metodo 1,
                    // quando o return null continua la.
                    r.Obtido = "(o programa parou)";
                    r.Erro = ex.GetType().Name + ": " + ex.Message;
                    r.Passou = false;
                }

                if (r.Passou) { passaram++; }
                saida.Testes.Add(r);
            }

            saida.Passaram = passaram;
            saida.Resolvido = (passaram == saida.Testes.Count && saida.Testes.Count > 0);

            return saida;
        }

        // Traduz a entrada de texto do JSON para a chamada de verdade.
        private static string Executar(string idTopico, string entrada)
        {
            string[] partes = (entrada ?? "").Split('|');

            if (idTopico == "classe")
            {
                Lutador l = Desafios.CriarLutador(partes[0]);
                if (l == null) { return "(null)"; }
                return l.Nome + "|" + l.Vida + "|" + l.Forca;
            }

            if (idTopico == "metodo")
            {
                int vida = int.Parse(partes[0]);
                int pontos = int.Parse(partes[1]);
                return Desafios.Descansar(vida, pontos).ToString();
            }

            if (idTopico == "operadores")
            {
                int vida = int.Parse(partes[0]);
                int treinos = int.Parse(partes[1]);
                return Desafios.EstaPronto(vida, treinos).ToString();
            }

            if (idTopico == "if")
            {
                double nota = double.Parse(partes[0], CultureInfo.InvariantCulture);
                return Desafios.Mencao(nota);
            }

            if (idTopico == "switch")
            {
                return Desafios.DescreverOpcao(partes[0]);
            }

            if (idTopico == "for")
            {
                return Desafios.Barra(int.Parse(partes[0]));
            }

            if (idTopico == "while")
            {
                return Desafios.ContarAte(int.Parse(partes[0]));
            }

            if (idTopico == "foreach")
            {
                List<Lutador> lista = new List<Lutador>();

                // entrada vazia = lista vazia
                if (!string.IsNullOrEmpty(entrada))
                {
                    foreach (string nome in partes)
                    {
                        Lutador l = new Lutador();
                        l.Nome = nome;
                        lista.Add(l);
                    }
                }

                return Desafios.Elenco(lista);
            }

            return "(topico desconhecido: " + idTopico + ")";
        }
    }
}
