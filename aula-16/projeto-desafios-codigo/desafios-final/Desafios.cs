using System;
using System.Collections.Generic;

namespace Conecta
{
    // =====================================================================
    // GABARITO dos dez desafios de codigo. SO SEU.
    //
    // Cada metodo esta escrito do jeito que o Portugol do desafio manda -
    // e ao lado de cada um esta o erro que a turma comete nele.
    //
    // Para conferir que os testes estao certos, rode este projeto: os
    // trinta e poucos testes tem de passar todos, e o
    // DesafiosCodigoGabarito.exe --autoteste devolve 0.
    // =====================================================================
    public static class Desafios
    {
        // ---------------------------------------------------------------- 1
        // O ERRO: escrever r.Autor = "Ana" com o nome fixo. Passa no
        // primeiro teste e reprova no segundo, que cria um Bruno.
        public static Recado CriarRecado(string autor, string texto)
        {
            Recado r = new Recado();
            r.Autor = autor;
            r.Texto = texto;
            return r;
        }

        // ---------------------------------------------------------------- 2
        // O ERRO: esquecer o espaco depois dos dois-pontos. O corretor
        // compara letra por letra, entao "Ana:oi" reprova.
        public static string Descrever(Recado r)
        {
            return r.Autor + ": " + r.Texto;
        }

        // ---------------------------------------------------------------- 3
        // O ERRO: usar if/else if. Funciona, mas a noite pede switch aqui -
        // um valor so contra constantes. E esquecer o default deixa o
        // metodo sem devolver nada, o que nem compila (CS0161).
        public static string Saudacao(int quantos)
        {
            switch (quantos)
            {
                case 0:
                    return "Mural vazio";

                case 1:
                    return "1 recado";

                default:
                    return quantos + " recados";
            }
        }

        // ---------------------------------------------------------------- 4
        // O ERRO: contar antes do Trim. Tres espacos passam por um nome.
        public static bool NomeValido(string autor)
        {
            return autor.Trim().Length >= 3;
        }

        // ---------------------------------------------------------------- 5
        // O ERRO: acrescentar antes de numerar. Depois do Add a quantidade
        // ja mudou, e o Id sai um a mais.
        public static int Gravar(List<Recado> mural, Recado r)
        {
            r.Id = mural.Count + 1;
            mural.Add(r);
            return r.Id;
        }

        // ---------------------------------------------------------------- 6
        // OS ERROS: comecar em Count (estoura), parar em i > 0 (perde o
        // primeiro recado) e usar i++ (laco sem fim).
        public static List<Recado> Listar(List<Recado> mural)
        {
            List<Recado> saida = new List<Recado>();

            for (int i = mural.Count - 1; i >= 0; i--)
            {
                saida.Add(mural[i]);
            }

            return saida;
        }

        // ---------------------------------------------------------------- 7
        // OS ERROS: dar return dentro do laco (para no primeiro achado) e
        // comparar sem ToLower dos DOIS lados.
        public static int Procurar(List<Recado> mural, string termo)
        {
            int quantos = 0;

            foreach (Recado r in mural)
            {
                if (r.Texto.ToLower().Contains(termo.ToLower()))
                {
                    quantos = quantos + 1;
                }
            }

            return quantos;
        }

        // ---------------------------------------------------------------- 8
        // O ERRO CLASSICO, o mesmo que a prova cobrou: por o return null
        // DENTRO do foreach. Ai ele desiste no primeiro recado que nao e do
        // autor, e so acha quem esta na primeira posicao.
        public static Recado PrimeiroDoAutor(List<Recado> mural, string autor)
        {
            foreach (Recado r in mural)
            {
                if (r.Autor == autor)
                {
                    return r;
                }
            }

            return null;
        }

        // ---------------------------------------------------------------- 9
        // O ERRO: cortar sem conferir antes. Substring com limite maior que
        // o texto estoura em tempo de execucao.
        public static string Resumir(string texto, int limite)
        {
            if (texto.Length <= limite)
            {
                return texto;
            }

            return texto.Substring(0, limite) + "...";
        }

        // --------------------------------------------------------------- 10
        // O ERRO: inverter a ordem do ||. Conferir a data antes do null
        // estoura na primeira volta, porque melhor ainda e nulo.
        public static Recado MaisRecente(List<Recado> mural)
        {
            Recado melhor = null;

            foreach (Recado r in mural)
            {
                if (melhor == null || r.DataHora > melhor.DataHora)
                {
                    melhor = r;
                }
            }

            return melhor;
        }
    }
}
