# -*- coding: utf-8 -*-
"""
OS 10 DESAFIOS DE CODIGO - a fonte de verdade.

Estes nao sao de escolher: o aluno ESCREVE o metodo, no arquivo
Desafios.cs, salva e roda com F5. O corretor executa o que ele escreveu e
compara com os valores esperados.

Cada desafio traz o Portugol que o orienta - o mesmo tipo de algoritmo que
esta na folha. E e o Portugol que responde "o que este metodo tem de
fazer", para o aluno nao ficar adivinhando enunciado.

Os VALORES ESPERADOS de cada teste nao moram aqui: moram no Corretor.cs,
em C#, porque comparar Recado, List<Recado> e null com valor esperado e
trabalho de codigo, nao de JSON. Aqui ficam o algoritmo, a assinatura e a
dica.
"""

DESAFIOS = [
    dict(n=1, titulo="Criar o recado",
         metodo="public static Recado CriarRecado(string autor, string texto)",
         ficha=("um autor e um texto", "monta um Recado novo", "o Recado (Recado)"),
         portugol=[
             "funcao CriarRecado(autor, texto: caractere): Recado",
             "var r: Recado",
             "inicio",
             "   r <- novo Recado",
             "   r.Autor <- autor",
             "   r.Texto <- texto",
             "   retorne r",
             "fimfuncao",
         ],
         dica="Use os PARAMETROS. Escrever um nome fixo entre aspas faz todo "
              "recado sair com o mesmo autor - e o corretor pega isso."),

    dict(n=2, titulo="Descrever",
         metodo="public static string Descrever(Recado r)",
         ficha=("um recado (Recado)", "monta o texto de uma linha do mural",
                "um texto (string)"),
         portugol=[
             "funcao Descrever(r: Recado): caractere",
             "inicio",
             '   retorne r.Autor + ": " + r.Texto',
             "fimfuncao",
             "",
             "// vira:  Ana Souza: alguem tem a apostila?",
         ],
         dica="Concatenar em C# e com +. Repare onde ficam as aspas: os dois "
              "pontos e o espaco fazem parte do texto do meio."),

    dict(n=3, titulo="A saudacao",
         metodo="public static string Saudacao(int quantos)",
         ficha=("um numero (int)", "escolhe a frase certa", "um texto (string)"),
         portugol=[
             "funcao Saudacao(quantos: inteiro): caractere",
             "inicio",
             "   escolha quantos",
             "      caso 0",
             '         retorne "Mural vazio"',
             "      caso 1",
             '         retorne "1 recado"',
             "      outrocaso",
             '         retorne quantos + " recados"',
             "   fimescolha",
             "fimfuncao",
         ],
         dica="Um valor so contra constantes: switch. O default existe porque "
              "'qualquer outro numero' e um caso de verdade."),

    dict(n=4, titulo="O nome vale?",
         metodo="public static bool NomeValido(string autor)",
         ficha=("um texto (string)", "diz se o nome serve", "sim ou nao (bool)"),
         portugol=[
             "funcao NomeValido(autor: caractere): logico",
             "inicio",
             "   se comprimento(limpa(autor)) >= 3 entao",
             "      retorne verdadeiro",
             "   senao",
             "      retorne falso",
             "   fimse",
             "fimfuncao",
         ],
         dica="Tire os espacos das pontas ANTES de contar as letras - senao tres "
              "espacos passam por um nome. Em C#: autor.Trim().Length"),

    dict(n=5, titulo="Gravar no mural",
         metodo="public static int Gravar(List<Recado> mural, Recado r)",
         ficha=("o mural e um recado", "guarda o recado e numera",
                "o Id que ele recebeu (int)"),
         portugol=[
             "funcao Gravar(mural: vetor de Recado, r: Recado): inteiro",
             "inicio",
             "   r.Id <- tamanho(mural) + 1",
             "   acrescente(mural, r)",
             "   retorne r.Id",
             "fimfuncao",
         ],
         dica="Numere ANTES de acrescentar. Depois do Add a quantidade ja mudou, "
              "e o segundo recado sairia com o Id do terceiro."),

    dict(n=6, titulo="Listar do mais novo",
         metodo="public static List<Recado> Listar(List<Recado> mural)",
         ficha=("o mural", "monta a lista ao contrario",
                "uma lista de recados (List<Recado>)"),
         portugol=[
             "funcao Listar(mural: vetor de Recado): vetor de Recado",
             "var saida: vetor de Recado",
             "    i: inteiro",
             "inicio",
             "   saida <- vetor vazio",
             "",
             "   para i de tamanho(mural) - 1 ate 0 passo -1 faca",
             "      acrescente(saida, mural[i])",
             "   fimpara",
             "",
             "   retorne saida",
             "fimfuncao",
         ],
         dica="A ultima posicao e Count - 1, e o laco desce ate 0 INCLUSIVE. "
              "Comecar em Count estoura em tempo de execucao."),

    dict(n=7, titulo="Procurar uma palavra",
         metodo="public static int Procurar(List<Recado> mural, string termo)",
         ficha=("o mural e um texto", "conta quantos contem o termo",
                "um numero (int)"),
         portugol=[
             "funcao Procurar(mural: vetor de Recado,",
             "                termo: caractere): inteiro",
             "var quantos: inteiro",
             "inicio",
             "   quantos <- 0",
             "",
             "   para cada r em mural faca",
             "      se contem(minusculo(r.Texto), minusculo(termo)) entao",
             "         quantos <- quantos + 1",
             "      fimse",
             "   fimpara",
             "",
             "   retorne quantos",
             "fimfuncao",
         ],
         dica="Contar nao e localizar: nao existe return dentro deste laco. E os "
              "DOIS lados em minuscula, senao 'Prova' nao acha 'prova'."),

    dict(n=8, titulo="O primeiro de um autor",
         metodo="public static Recado PrimeiroDoAutor(List<Recado> mural, string autor)",
         ficha=("o mural e um autor", "acha o primeiro recado dele",
                "o Recado, ou null se nao achar"),
         portugol=[
             "funcao PrimeiroDoAutor(mural: vetor de Recado,",
             "                       autor: caractere): Recado",
             "inicio",
             "   para cada r em mural faca",
             "      se r.Autor = autor entao",
             "         retorne r",
             "      fimse",
             "   fimpara",
             "",
             "   retorne nulo",
             "fimfuncao",
         ],
         dica="O return de ACHOU fica DENTRO do laco. O de nao achou fica DEPOIS "
              "dele - la dentro, ele desiste no primeiro que nao serve."),

    dict(n=9, titulo="Resumir o texto",
         metodo="public static string Resumir(string texto, int limite)",
         ficha=("um texto e um limite", "corta o texto se passar do limite",
                "um texto (string)"),
         portugol=[
             "funcao Resumir(texto: caractere, limite: inteiro): caractere",
             "inicio",
             "   se comprimento(texto) <= limite entao",
             "      retorne texto",
             "   fimse",
             "",
             '   retorne pedaco(texto, 0, limite) + "..."',
             "fimfuncao",
         ],
         dica="Confira ANTES de cortar: pedir um pedaco maior que o texto estoura. "
              "Em C#, pedaco e texto.Substring(0, limite)."),

    dict(n=10, titulo="O mais recente",
         metodo="public static Recado MaisRecente(List<Recado> mural)",
         ficha=("o mural", "acha o recado de data mais nova",
                "o Recado, ou null se o mural estiver vazio"),
         portugol=[
             "funcao MaisRecente(mural: vetor de Recado): Recado",
             "var melhor: Recado",
             "inicio",
             "   melhor <- nulo",
             "",
             "   para cada r em mural faca",
             "      se melhor = nulo ou r.DataHora > melhor.DataHora entao",
             "         melhor <- r",
             "      fimse",
             "   fimpara",
             "",
             "   retorne melhor",
             "fimfuncao",
         ],
         dica="Guarde o melhor ate agora numa variavel que comeca nula. A ordem "
              "do OU importa: conferir o nulo primeiro evita estourar."),
]
