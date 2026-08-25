using System;
using System.Collections.Generic;

namespace Conecta
{
    // =====================================================================
    //
    //   >>>>>>>>>>  E AQUI QUE VOCE ESCREVE. So aqui.  <<<<<<<<<<
    //
    // Sao dez metodos. Cada um esta vazio, com um TODO.
    //
    // COMO FUNCIONA
    //
    //   1. Rode com F5. A janela abre com os dez desafios.
    //   2. A ESQUERDA da janela esta o ALGORITMO EM PORTUGOL do desafio -
    //      e ele que diz o que o metodo tem de fazer. Nao adivinhe.
    //   3. Escreva o metodo aqui neste arquivo.
    //   4. FECHE O PROGRAMA, rode de novo com F5, e clique em Conferir.
    //      O corretor EXECUTA o seu metodo e mostra, teste a teste, o que
    //      era esperado e o que voce devolveu.
    //
    // NAO MUDE o nome dos metodos, nem o que eles recebem, nem o tipo que
    // eles devolvem: o corretor procura por eles exatamente como estao.
    //
    // Os metodos ja vem com um "return" provisorio so para o projeto
    // compilar antes de voce escrever qualquer coisa. Apague junto com o
    // TODO.
    //
    // =====================================================================
    public static class Desafios
    {
        // ---------------------------------------------------------------- 1
        // CRIAR O RECADO
        //
        // FICHA
        //   RECEBE ... um autor e um texto
        //   FAZ ...... monta um Recado novo
        //   DEVOLVE .. o Recado
        //
        // Cuidado: use os PARAMETROS. Escrever um nome fixo entre aspas faz
        // todo recado sair com o mesmo autor - e o corretor pega isso.
        public static Recado CriarRecado(string autor, string texto)
        {
            // TODO 1: crie o Recado com new, preencha Autor e Texto, devolva.
            return null;
        }

        // ---------------------------------------------------------------- 2
        // DESCREVER
        //
        // FICHA
        //   RECEBE ... um recado
        //   FAZ ...... monta o texto de uma linha do mural
        //   DEVOLVE .. um texto
        //
        // Tem de sair exatamente assim:   Ana Souza: alguem tem a apostila?
        public static string Descrever(Recado r)
        {
            // TODO 2
            return "";
        }

        // ---------------------------------------------------------------- 3
        // A SAUDACAO
        //
        // FICHA
        //   RECEBE ... um numero
        //   FAZ ...... escolhe a frase certa para essa quantidade
        //   DEVOLVE .. um texto
        //
        // Um valor so, comparado com constantes: e switch, nao if.
        public static string Saudacao(int quantos)
        {
            // TODO 3
            return "";
        }

        // ---------------------------------------------------------------- 4
        // O NOME VALE?
        //
        // FICHA
        //   RECEBE ... um texto
        //   FAZ ...... diz se o nome serve (3 letras ou mais)
        //   DEVOLVE .. sim ou nao
        //
        // Tire os espacos das pontas ANTES de contar.
        public static bool NomeValido(string autor)
        {
            // TODO 4
            return false;
        }

        // ---------------------------------------------------------------- 5
        // GRAVAR NO MURAL
        //
        // FICHA
        //   RECEBE ... o mural e um recado
        //   FAZ ...... numera o recado e guarda no mural
        //   DEVOLVE .. o Id que ele recebeu
        //
        // Numere ANTES de acrescentar.
        public static int Gravar(List<Recado> mural, Recado r)
        {
            // TODO 5
            return 0;
        }

        // ---------------------------------------------------------------- 6
        // LISTAR DO MAIS NOVO
        //
        // FICHA
        //   RECEBE ... o mural
        //   FAZ ...... monta a lista ao contrario, do fim para o comeco
        //   DEVOLVE .. uma lista de recados
        //
        // E o unico laco destes dez que precisa da POSICAO: for, nao foreach.
        public static List<Recado> Listar(List<Recado> mural)
        {
            // TODO 6
            return new List<Recado>();
        }

        // ---------------------------------------------------------------- 7
        // PROCURAR UMA PALAVRA
        //
        // FICHA
        //   RECEBE ... o mural e um texto
        //   FAZ ...... conta quantos recados contem o termo
        //   DEVOLVE .. um numero
        //
        // Contar nao e localizar: nao existe return dentro do laco.
        public static int Procurar(List<Recado> mural, string termo)
        {
            // TODO 7
            return 0;
        }

        // ---------------------------------------------------------------- 8
        // O PRIMEIRO DE UM AUTOR
        //
        // FICHA
        //   RECEBE ... o mural e um autor
        //   FAZ ...... acha o primeiro recado daquele autor
        //   DEVOLVE .. o Recado, ou null se nao achar
        //
        // O return de ACHOU fica DENTRO do laco. O de nao achou, DEPOIS dele.
        public static Recado PrimeiroDoAutor(List<Recado> mural, string autor)
        {
            // TODO 8
            return null;
        }

        // ---------------------------------------------------------------- 9
        // RESUMIR O TEXTO
        //
        // FICHA
        //   RECEBE ... um texto e um limite
        //   FAZ ...... corta o texto e poe ... no fim, se passar do limite
        //   DEVOLVE .. um texto
        //
        // Confira ANTES de cortar. Em C#, o pedaco e texto.Substring(0, limite).
        public static string Resumir(string texto, int limite)
        {
            // TODO 9
            return "";
        }

        // --------------------------------------------------------------- 10
        // O MAIS RECENTE
        //
        // FICHA
        //   RECEBE ... o mural
        //   FAZ ...... acha o recado de data mais nova
        //   DEVOLVE .. o Recado, ou null se o mural estiver vazio
        //
        // Guarde o melhor ate agora numa variavel que comeca nula.
        public static Recado MaisRecente(List<Recado> mural)
        {
            // TODO 10
            return null;
        }
    }
}
