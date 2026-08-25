# -*- coding: utf-8 -*-
"""
Monta a FONTE UNICA da Aula 16: conteudo/passos-mural.json

    python montar-conteudo.py

O Portugol dos passos 1 a 8 vem de portugol_dos_passos.py, ao lado deste
arquivo - o unico lugar onde ele esta escrito. Ninguem redigita, entao a
tela e o papel nao podem divergir. O que este arquivo acrescenta e a coluna da direita: o C# com
lacunas numeradas, as opcoes de cada lacuna e a dica de quando erra.

O caminho e um so, e vai numa direcao so:

    portugol_dos_passos.py  +  a coluna da direita daqui
             |
             v
    conteudo/passos-mural.json   <- a fonte unica, gerada
             |
             +-->  folhas/gerar_folhas.py .... a folha B impressa
             +-->  projeto-desafio/ .......... o aplicativo do modo facil

RODE ESTE ARQUIVO PRIMEIRO. O gerador das folhas le o JSON que sai daqui.

O passo 9 nao tem Portugol de proposito: o VisuAlg nao tem banco, e
inventar comando seria ensinar uma lingua falsa. Ali a coluna da esquerda
e o C# em memoria - o mesmo lado a lado da folha C.
"""

import importlib.util
import io
import json
import os

AQUI = os.path.dirname(os.path.abspath(__file__))
RAIZ = os.path.dirname(AQUI)


def desafios_de_codigo():
    caminho = os.path.join(AQUI, "desafios_de_codigo.py")
    spec = importlib.util.spec_from_file_location("desafios", caminho)
    m = importlib.util.module_from_spec(spec)
    spec.loader.exec_module(m)
    return m.DESAFIOS


def portugol_dos_passos():
    """Le o Portugol do modulo de dados, ao lado deste arquivo."""
    caminho = os.path.join(AQUI, "portugol_dos_passos.py")
    spec = importlib.util.spec_from_file_location("portugol", caminho)
    m = importlib.util.module_from_spec(spec)
    spec.loader.exec_module(m)
    return {p["n"]: p for p in m.PASSOS}


# ---------------------------------------------------------------------
# A coluna da direita: o C# com lacunas.
#
# {1}, {2}... sao as lacunas. Cada uma tem UMA resposta certa, quatro
# opcoes e uma dica que so aparece quando o aluno erra.
#
# As lacunas foram escolhidas pelo assunto da noite: o que o metodo
# DEVOLVE, o que ele RECEBE, e as palavras que decidem isso.
# ---------------------------------------------------------------------
DIREITA = {
    1: dict(
        ficha=None,
        csharp=[
            "public class Recado",
            "{",
            "    public {1} Id = 0;",
            "    public {2} Autor = \"\";",
            "    public {3} Texto = \"\";",
            "    public {4} DataHora = DateTime.Now;",
            "}",
        ],
        lacunas=[
            (1, "int", ["int", "string", "bool", "double"],
             "O Id e um numero inteiro - quem numera e o banco, com AUTO_INCREMENT."),
            (2, "string", ["string", "int", "char", "texto"],
             "Texto em C# e string, com s minusculo. 'caractere' e a palavra do Portugol."),
            (3, "string", ["string", "int", "bool", "double"],
             "O recado tambem e texto."),
            (4, "DateTime", ["DateTime", "string", "int", "Data"],
             "Data e hora tem tipo proprio: DateTime. Guardar data como texto impede ordenar depois."),
        ]),

    2: dict(
        ficha=("um recado (Recado)", "monta o texto de uma linha do mural", "um texto (string)"),
        csharp=[
            "private {1} Descrever({2})",
            "{",
            "    {3} \"[\" + r.DataHora.ToString(\"dd/MM HH:mm\") + \"] \"",
            "         + r.Autor + \": \" + r.Texto;",
            "}",
        ],
        lacunas=[
            (1, "string", ["string", "void", "int", "Recado"],
             "A ficha diz DEVOLVE um texto. O que ele devolve vem ANTES do nome."),
            (2, "Recado r", ["Recado r", "string r", "r", "Recado"],
             "A ficha diz RECEBE um recado. Dentro dos parenteses vai o TIPO e o NOME."),
            (3, "return", ["return", "retorne", "escreva", "void"],
             "'retorne' e Portugol. Em C# a palavra e return - e ela sai da folha A."),
        ]),

    3: dict(
        ficha=("nada", "busca os recados e poe cada um na tela", "nada"),
        csharp=[
            "private {1} CarregarMural()",
            "{",
            "    List<Recado> recados = MuralDAO.{2};",
            "",
            "    lstMural.Items.{3};",
            "",
            "    {4} (Recado r in recados)",
            "    {",
            "        lstMural.Items.Add({5});",
            "    }",
            "",
            "    lblSaudacao.Text = Saudacao(recados.{6});",
            "}",
        ],
        lacunas=[
            (1, "void", ["void", "string", "int", "List<Recado>"],
             "A ficha diz DEVOLVE nada. Quem nao devolve nada leva void - o 'procedimento' do Portugol."),
            (2, "Listar()", ["Listar()", "Gravar()", "Descrever()", "Listar"],
             "Quem sabe onde os recados moram e o DAO. E chamada de metodo leva parenteses."),
            (3, "Clear()", ["Clear()", "Add()", "Count", "Remove()"],
             "Antes de encher a lista da tela, esvazie - senao os recados aparecem duas vezes."),
            (4, "foreach", ["foreach", "for", "while", "para cada"],
             "Percorre o que EXISTE e ninguem precisa da posicao: foreach."),
            (5, "Descrever(r)", ["Descrever(r)", "r", "r.Texto", "Descrever(recados)"],
             "Quem monta o texto da linha e o Descrever - e ele recebe UM recado por vez."),
            (6, "Count", ["Count", "Length", "tamanho", "Size"],
             "Quantos itens tem uma List: .Count. O .Length e de texto e de vetor fixo."),
        ]),

    4: dict(
        ficha=("um recado (Recado)", "guarda o recado na lista", "o Id que ele recebeu (int)"),
        csharp=[
            "private {1} List<Recado> _recados = new List<Recado>();",
            "private {2} int _proximoId = 1;",
            "",
            "public static {3} Gravar({4})",
            "{",
            "    r.Id = _proximoId;",
            "    _proximoId = _proximoId + 1;",
            "",
            "    _recados.{5};",
            "",
            "    {6} r.Id;",
            "}",
        ],
        lacunas=[
            (1, "static", ["static", "public", "void", "new"],
             "A lista tem de ser UMA SO para o programa inteiro. Sem static, cada uso cria uma lista vazia."),
            (2, "static", ["static", "public", "int", "const"],
             "Mesmo motivo do campo de cima: um contador so."),
            (3, "int", ["int", "void", "string", "Recado"],
             "A ficha diz DEVOLVE o Id, e Id e numero inteiro."),
            (4, "Recado r", ["Recado r", "int r", "Recado", "r"],
             "A ficha diz RECEBE um recado: tipo e nome, dentro dos parenteses."),
            (5, "Add(r)", ["Add(r)", "Add()", "Insert(r)", "Add(_recados)"],
             "Acrescentar na List e .Add(), e o que entra e o recado que chegou por parametro."),
            (6, "return", ["return", "retorne", "devolve", "void"],
             "A palavra do C# e return."),
        ]),

    5: dict(
        ficha=("nada", "monta a lista do mural, do mais novo para o mais velho",
               "uma lista de recados (List<Recado>)"),
        csharp=[
            "public static {1} Listar()",
            "{",
            "    List<Recado> mural = new List<Recado>();",
            "",
            "    for (int i = _recados.Count {2} 1; i {3} 0; i{4})",
            "    {",
            "        mural.Add(_recados[{5}]);",
            "    }",
            "",
            "    {6} mural;",
            "}",
        ],
        lacunas=[
            (1, "List<Recado>", ["List<Recado>", "Recado", "void", "int"],
             "A ficha diz DEVOLVE uma lista de recados. E a primeira vez que um metodo seu devolve uma colecao."),
            (2, "-", ["-", "+", "*", "="],
             "A primeira posicao e ZERO, entao a ultima e Count - 1. Comecar em Count estoura o programa."),
            (3, ">=", [">=", ">", "<=", "=="],
             "O laco tem de chegar ate a posicao 0, entao a condicao inclui o zero."),
            (4, "--", ["--", "++", "-1", "+1"],
             "De tras para a frente: a cada volta o i DIMINUI um."),
            (5, "i", ["i", "0", "Count", "r"],
             "Pega o recado que esta na posicao da vez."),
            (6, "return", ["return", "retorne", "void", "break"],
             "A palavra do C# e return - e ela devolve a lista montada."),
        ]),

    6: dict(
        ficha=("o evento (sender, e) - nao e voce quem decide",
               "confere, monta o recado, manda gravar e recarrega", "nada"),
        csharp=[
            "private {1} btnPublicar_Click(object sender, EventArgs e)",
            "{",
            "    if (txtAutor.Text.Trim() {2} \"\")",
            "    {",
            "        Avisar(\"Escreva o seu nome antes de publicar.\");",
            "        {3};",
            "    }",
            "",
            "    // ... as outras duas perguntas, no mesmo molde ...",
            "",
            "    Recado r = {4} Recado();",
            "    r.Autor = txtAutor.Text.Trim();",
            "    r.Texto = txtTexto.Text.Trim();",
            "    r.DataHora = {5};",
            "",
            "    MuralDAO.{6};",
            "",
            "    Limpar();",
            "    CarregarMural();",
            "}",
        ],
        lacunas=[
            (1, "void", ["void", "string", "int", "bool"],
             "Evento nao devolve nada para ninguem: quem chama e o Windows, e ele nao espera resposta."),
            (2, "==", ["==", "=", "<>", "!="],
             "UM igual guarda um valor. DOIS iguais perguntam. Aqui a gente pergunta."),
            (3, "return", ["return", "break", "continue", "exit"],
             "Sai do metodo agora, para nao continuar com dado ruim. Em metodo void, return vai sozinho."),
            (4, "new", ["new", "novo", "Recado", "create"],
             "'novo' e Portugol. A palavra do C# e new - ela cria o objeto a partir do molde."),
            (5, "DateTime.Now", ["DateTime.Now", "agora()", "Date.Now", "DateTime.Hoje"],
             "O agora do C# e DateTime.Now."),
            (6, "Gravar(r)", ["Gravar(r)", "Listar()", "Gravar()", "Add(r)"],
             "Manda o DAO guardar o recado que voce acabou de montar."),
        ]),

    7: dict(
        ficha=("um numero (int)", "escolhe a frase certa para essa quantidade",
               "um texto (string)"),
        csharp=[
            "private {1} Saudacao({2} quantos)",
            "{",
            "    {3} (quantos)",
            "    {",
            "        {4} 0:",
            "            return \"Mural vazio - seja o primeiro a publicar.\";",
            "",
            "        case 1:",
            "            return \"1 recado no mural.\";",
            "",
            "        {5}:",
            "            return quantos + \" recados no mural.\";",
            "    }",
            "}",
        ],
        lacunas=[
            (1, "string", ["string", "void", "int", "bool"],
             "A ficha diz DEVOLVE um texto - e esse texto vai para o lblSaudacao.Text."),
            (2, "int", ["int", "string", "double", "quantos"],
             "Dentro dos parenteses vai o TIPO antes do nome. Quantos recados e numero inteiro."),
            (3, "switch", ["switch", "escolha", "if", "case"],
             "UM valor so comparado com constantes: switch. 'escolha' e a palavra do Portugol."),
            (4, "case", ["case", "caso", "if", "when"],
             "Cada valor previsto abre um case, com dois-pontos no fim."),
            (5, "default", ["default", "outrocaso", "else", "senao"],
             "O caso de qualquer outro numero. 'outrocaso' e Portugol; em C# e default."),
        ]),

    8: dict(
        ficha=("um texto (string)", "percorre o mural, mostra so os que combinam e conta",
               "um numero (int)"),
        csharp=[
            "private {1} Procurar({2} termo)",
            "{",
            "    List<Recado> recados = MuralDAO.Listar();",
            "",
            "    int quantos = {3};",
            "",
            "    lstMural.Items.Clear();",
            "",
            "    foreach (Recado r in recados)",
            "    {",
            "        if (r.Texto.ToLower().{4})",
            "        {",
            "            lstMural.Items.Add(Descrever(r));",
            "            quantos = {5};",
            "        }",
            "    }",
            "",
            "    {6} quantos;",
            "}",
        ],
        lacunas=[
            (1, "int", ["int", "void", "string", "List<Recado>"],
             "A ficha diz DEVOLVE um numero: quantos recados combinaram."),
            (2, "string", ["string", "int", "termo", "char"],
             "O que ele recebe e o texto procurado."),
            (3, "0", ["0", "1", "quantos", "recados.Count"],
             "O acumulador nasce ZERO, antes do laco. Comecar em 1 conta um recado que nao existe."),
            (4, "Contains(termo.ToLower())",
             ["Contains(termo.ToLower())", "Contains(termo)", "Equals(termo)", "contem(termo)"],
             "Os DOIS lados em minuscula, senao 'Prova' nao acha 'prova'."),
            (5, "quantos + 1", ["quantos + 1", "1", "quantos", "quantos - 1"],
             "Cresce SOBRE O PROPRIO VALOR. Escrever quantos = 1 zera a conta a cada volta."),
            (6, "return", ["return", "retorne", "break", "void"],
             "Devolve DEPOIS que o laco terminou. Contar nao e localizar: nao ha return dentro do laco."),
        ]),

    9: dict(
        ficha=("nada", "busca todos os recados, do mais novo para o mais velho",
               "uma lista de recados (List<Recado>)"),
        csharp=[
            "public static List<Recado> Listar()",
            "{",
            "    List<Recado> mural = new List<Recado>();",
            "    MySqlConnection con = Conexao.Obter();",
            "",
            "    try",
            "    {",
            "        con.{1};",
            "",
            "        string sql = \"SELECT Id, Autor, Texto, DataHora\"",
            "                   + \"  FROM Recado\"",
            "                   + \" ORDER BY Id {2}\";",
            "",
            "        MySqlCommand cmd = new MySqlCommand(sql, con);",
            "        MySqlDataReader leitor = cmd.{3}();",
            "",
            "        {4} (leitor.Read())",
            "        {",
            "            Recado r = new Recado();",
            "            r.Id    = Convert.ToInt32(leitor[\"Id\"]);",
            "            r.Autor = Convert.ToString(leitor[\"Autor\"]);",
            "            r.Texto = Convert.ToString(leitor[\"Texto\"]);",
            "            r.DataHora = Convert.ToDateTime(leitor[\"DataHora\"]);",
            "            mural.Add(r);",
            "        }",
            "",
            "        return mural;",
            "    }",
            "    {5} { con.Close(); }",
            "}",
        ],
        lacunas=[
            (1, "Open()", ["Open()", "Close()", "Obter()", "Start()"],
             "O Obter devolveu a conexao FECHADA. Quem abre e quem chama - e o Open fica DENTRO do try."),
            (2, "DESC", ["DESC", "ASC", "DOWN", "INVERSO"],
             "Do maior Id para o menor: o mais novo em cima. E este DESC que substituiu o seu laco invertido."),
            (3, "ExecuteReader", ["ExecuteReader", "ExecuteScalar", "ExecuteNonQuery", "Execute"],
             "A resposta sao VARIAS LINHAS com varias colunas: Reader. Scalar e para um valor so."),
            (4, "while", ["while", "enquanto", "for", "if"],
             "Enquanto ainda houver linha. E o 'enquanto' do Portugol, com o nome em ingles."),
            (5, "finally", ["finally", "catch", "else", "fim"],
             "Fecha a conexao tenha dado certo ou nao. Sem ele, a conexao vaza."),
        ]),
}

# =====================================================================
#  RODADA 2 - as 10 perguntas
#
#  Mesmo formato das lacunas: escolher entre quatro. O que muda e que
#  aqui a pergunta e sobre o ASSUNTO, nao sobre uma linha de codigo.
#
#  Toda pergunta tem um trecho de Portugol a esquerda, para o aluno
#  responder olhando o algoritmo - nunca de cabeca.
# =====================================================================
PERGUNTAS = [
    dict(titulo="procedimento vira o que?",
         portugol=["procedimento Limpar()",
                   "inicio",
                   "   txtTexto <- \"\"",
                   "fimprocedimento"],
         enunciado="Este procedimento nao devolve nada. Qual palavra do C# ocupa o "
                   "lugar do tipo de retorno?",
         opcoes=["void", "string", "return", "private"],
         resposta="void",
         dica="void e a palavra que diz 'este metodo nao responde nada'. E o "
              "procedimento do Portugol, escrito de outro jeito."),

    dict(titulo="onde entra o que o metodo devolve",
         portugol=["funcao Descrever(r: Recado): caractere",
                   "inicio",
                   "   retorne ...",
                   "fimfuncao"],
         enunciado="No Portugol o tipo devolvido vem DEPOIS dos parenteses, com "
                   "dois-pontos. Em C#, onde ele vai?",
         opcoes=["Antes do nome do metodo",
                 "Depois dos parenteses, igual ao Portugol",
                 "Dentro dos parenteses",
                 "Na primeira linha do corpo"],
         resposta="Antes do nome do metodo",
         dica="private string Descrever(Recado r) - o string vem antes do nome. "
              "E a inversao que mais confunde quem vem do Portugol."),

    dict(titulo="metodo que nao recebe nada",
         portugol=["procedimento CarregarMural()",
                   "inicio",
                   "   ...",
                   "fimprocedimento"],
         enunciado="Este metodo nao recebe parametro nenhum. Como ficam os "
                   "parenteses em C#?",
         opcoes=["Ficam vazios, mas continuam existindo",
                 "Somem",
                 "Levam a palavra void dentro",
                 "Levam o nome do metodo dentro"],
         resposta="Ficam vazios, mas continuam existindo",
         dica="Os parenteses fazem parte do metodo, sempre. Sem eles, o "
              "compilador nao ve um metodo - ve um campo."),

    dict(titulo="parametro ou argumento?",
         portugol=["funcao Saudacao(quantos: inteiro): caractere",
                   "",
                   "// e, mais adiante, no programa:",
                   "lblSaudacao <- Saudacao(7)"],
         enunciado="Na primeira linha esta 'quantos'. Na ultima esta o 7. "
                   "Qual dos dois e o PARAMETRO?",
         opcoes=["quantos, porque esta na declaracao",
                 "O 7, porque e o valor de verdade",
                 "Os dois sao parametros",
                 "Nenhum: parametro e o tipo inteiro"],
         resposta="quantos, porque esta na declaracao",
         dica="Parametro e o nome na DECLARACAO. Argumento e o valor na "
              "CHAMADA. O metodo e escrito uma vez e serve para todos os valores."),

    dict(titulo="o retorno que ninguem pega",
         portugol=["funcao Saudacao(quantos: inteiro): caractere",
                   "",
                   "// o aluno escreveu, no meio do codigo:",
                   "Saudacao(3)"],
         enunciado="O metodo devolve um texto, e essa linha nao guarda o texto em "
                   "lugar nenhum. O que acontece?",
         opcoes=["Compila, roda e nao aparece nada",
                 "Da erro de compilacao",
                 "O texto aparece sozinho na tela",
                 "O programa trava"],
         resposta="Compila, roda e nao aparece nada",
         dica="O retorno se perde. Metodo que devolve e ninguem pega e trabalho "
              "jogado fora - o valor tem de ir para algum lugar."),

    dict(titulo="quem chama o evento",
         portugol=["procedimento btnPublicar_Click()",
                   "inicio",
                   "   ...",
                   "fimprocedimento"],
         enunciado="Voce nao escreve 'btnPublicar_Click()' em lugar nenhum do seu "
                   "codigo. Entao quem chama esse metodo?",
         opcoes=["O Windows, quando o usuario clica",
                 "O Program.cs, quando o programa abre",
                 "O CarregarMural",
                 "Ninguem: ele roda sozinho o tempo todo"],
         resposta="O Windows, quando o usuario clica",
         dica="A ligacao esta no frmMural.Designer.cs: btnPublicar.Click += ... "
              "E por isso a assinatura de um evento e a unica que voce nao escolhe."),

    dict(titulo="o erro CS0161",
         portugol=["funcao Situacao(nota: real): caractere",
                   "inicio",
                   "   se nota >= 6 entao",
                   "      retorne \"aprovado\"",
                   "   fimse",
                   "fimfuncao"],
         enunciado="Traduzido assim para C#, o compilador acusa CS0161. O que ele "
                   "esta reclamando?",
         opcoes=["Que nem todos os caminhos devolvem valor",
                 "Que faltou ponto e virgula",
                 "Que o tipo real nao existe",
                 "Que o metodo nao foi chamado"],
         resposta="Que nem todos os caminhos devolvem valor",
         dica="Se a nota for menor que 6, o metodo chega ao fim sem devolver "
              "nada - e ele prometeu devolver um texto. Toda saida precisa de return."),

    dict(titulo="retorne vira o que",
         portugol=["funcao Procurar(termo: caractere): inteiro",
                   "inicio",
                   "   ...",
                   "   retorne quantos",
                   "fimfuncao"],
         enunciado="Qual e a palavra do C# que ocupa o lugar de 'retorne'?",
         opcoes=["return", "retorne", "devolve", "break"],
         resposta="return",
         dica="return, sem o e final. Esta na folha A, na secao dos subprogramas."),

    dict(titulo="por que static",
         portugol=["var recados: vetor de Recado",
                   "    proximoId: inteiro <- 1",
                   "",
                   "// no alto da classe MuralDAO"],
         enunciado="No C# esses dois campos levam a palavra static. Por que?",
         opcoes=["Para a lista ser UMA SO no programa inteiro",
                 "Para o metodo poder devolver valor",
                 "Para a lista poder crescer",
                 "Porque toda classe precisa de static"],
         resposta="Para a lista ser UMA SO no programa inteiro",
         dica="Sem static, cada vez que alguem usasse o DAO apareceria uma lista "
              "nova e vazia - e o recado publicado numa tela sumiria na outra."),

    dict(titulo="tamanho vira o que",
         portugol=["lblSaudacao <- Saudacao(tamanho(recados))"],
         enunciado="Como se pergunta a uma List quantos itens ela tem, em C#?",
         opcoes=["recados.Count", "recados.Length", "tamanho(recados)", "recados.Size"],
         resposta="recados.Count",
         dica=".Count e de colecao. O .Length e de texto e de vetor de tamanho "
              "fixo - trocar os dois e erro de compilacao."),
]


# =====================================================================
#  RODADA 3 - os 10 desafios de codigo
#
#  Aqui o aluno DIGITA. Nao ha escolha, nao ha lacuna: ele le o Portugol
#  a esquerda e escreve a linha de C# correspondente.
#
#  A conferencia e por PADRAO, nao por texto literal. O padrao aceita
#  qualquer nome de parametro, espaco a mais, private ou public - o que
#  ele cobra e a FORMA: o que devolve, o que recebe, e a palavra certa.
#
#  'aceita' e 'recusa' nao vao para a tela: sao os casos de teste que o
#  conferir-material.py roda contra o padrao, e que o proprio aplicativo
#  roda quando chamado com --autoteste.
# =====================================================================
ESQUERDA_9 = dict(
    tipo="csharp",
    titulo="ATE AS 20h45 - com List (o que voce escreveu no passo 5)",
    linhas=[
        "public static List<Recado> Listar()",
        "{",
        "    List<Recado> mural = new List<Recado>();",
        "",
        "    for (int i = _recados.Count - 1; i >= 0; i--)",
        "    {",
        "        mural.Add(_recados[i]);",
        "    }",
        "",
        "    return mural;",
        "}",
        "",
        "// A ordenacao e ESTE laco: de tras para a",
        "// frente, na mao, posicao por posicao.",
        "//",
        "// Repare na primeira linha dos dois lados.",
        "// Ela e IDENTICA. So o corpo muda.",
    ],
)


def montar():
    fonte = portugol_dos_passos()
    passos = []

    for n in sorted(DIREITA):
        d = DIREITA[n]

        if n in fonte:
            b = fonte[n]
            esquerda = dict(tipo="portugol", titulo="PORTUGOL (VisuAlg)",
                            linhas=b["portugol"])
            cabecalho = dict(nome=b["nome"], onde=b["onde"], estreia=b["estreia"],
                             metodo=b["metodo"], linhasEscrita=b["linhasEscrita"],
                             naFolha=True)
        else:
            esquerda = ESQUERDA_9
            cabecalho = dict(nome="O banco - o SELECT e a ordenacao",
                             onde="MuralDAO.cs - TODO 9",
                             estreia="o corpo muda, a assinatura nao",
                             metodo=True, linhasEscrita=8, naFolha=False)

        ficha = None
        if d["ficha"]:
            ficha = dict(recebe=d["ficha"][0], faz=d["ficha"][1], devolve=d["ficha"][2])

        passo = dict(n=n, **cabecalho)
        passo["esquerda"] = esquerda
        passo["ficha"] = ficha
        passo["csharp"] = d["csharp"]
        passo["lacunas"] = [
            dict(n=ln, resposta=r, opcoes=list(ops), dica=dica)
            for (ln, r, ops, dica) in d["lacunas"]
        ]
        passos.append(passo)

    perguntas = []
    for i, p in enumerate(PERGUNTAS, 1):
        perguntas.append(dict(
            n=i, titulo=p["titulo"], portugol=p["portugol"],
            enunciado=p["enunciado"], opcoes=p["opcoes"],
            resposta=p["resposta"], dica=p["dica"]))

    desafios = []
    for d in desafios_de_codigo():
        desafios.append(dict(
            n=d["n"], titulo=d["titulo"], metodo=d["metodo"],
            portugol=d["portugol"], dica=d["dica"],
            ficha=dict(recebe=d["ficha"][0], faz=d["ficha"][1],
                       devolve=d["ficha"][2])))

    return dict(
        aula="16",
        data="25/08/2026",
        titulo="O Mural do Conecta - do Portugol para o C#",
        passos=passos,
        perguntas=perguntas,
        desafios=desafios,
    )


if __name__ == "__main__":
    dados = montar()

    # conferencias que impedem um JSON quebrado de sair daqui
    for p in dados["passos"]:
        marcas = set()
        for linha in p["csharp"]:
            for i in range(1, 10):
                if "{%d}" % i in linha:
                    marcas.add(i)
        declaradas = {l["n"] for l in p["lacunas"]}
        if marcas != declaradas:
            raise SystemExit(
                "passo %d: o codigo tem as lacunas %s mas a lista declara %s"
                % (p["n"], sorted(marcas), sorted(declaradas)))
        for l in p["lacunas"]:
            if l["resposta"] not in l["opcoes"]:
                raise SystemExit(
                    "passo %d, lacuna %d: a resposta '%s' nao esta entre as opcoes"
                    % (p["n"], l["n"], l["resposta"]))
            if len(set(l["opcoes"])) != len(l["opcoes"]):
                raise SystemExit("passo %d, lacuna %d: opcao repetida"
                                 % (p["n"], l["n"]))

    # as perguntas: a resposta tem de estar entre as opcoes
    for p in dados["perguntas"]:
        if p["resposta"] not in p["opcoes"]:
            raise SystemExit("pergunta %d: a resposta nao esta entre as opcoes"
                             % p["n"])
        if len(set(p["opcoes"])) != len(p["opcoes"]):
            raise SystemExit("pergunta %d: opcao repetida" % p["n"])

    # os desafios: assinatura, Portugol e dica em todos
    vistos = set()
    for d in dados["desafios"]:
        if not d["metodo"].startswith("public static "):
            raise SystemExit("desafio %d: a assinatura tem de comecar com "
                             "'public static' - o corretor chama sem instanciar"
                             % d["n"])
        if not d["portugol"] or not d["dica"]:
            raise SystemExit("desafio %d: falta Portugol ou dica" % d["n"])
        nome = d["metodo"].split("(")[0].split()[-1]
        if nome in vistos:
            raise SystemExit("desafio %d: o metodo %s aparece duas vezes"
                             % (d["n"], nome))
        vistos.add(nome)

    destino = os.path.join(AQUI, "passos-mural.json")
    io.open(destino, "w", encoding="utf-8").write(
        json.dumps(dados, ensure_ascii=False, indent=2))

    # ------------------------------------------------------------------
    # A VERSAO ENXUTA, que vai para o lado do executavel dos desafios.
    #
    # O passos-mural.json inteiro tem as RESPOSTAS das lacunas e das
    # perguntas. Ele nao pode ser copiado para a maquina do aluno junto
    # com o projeto dos desafios: qualquer um abriria no Bloco de Notas.
    #
    # Esta versao so tem o que o desafio precisa mostrar: o algoritmo em
    # Portugol, a assinatura, a ficha e a dica. Nenhuma resposta.
    # ------------------------------------------------------------------
    enxuta = dict(aula=dados["aula"], data=dados["data"],
                  titulo=dados["titulo"], desafios=dados["desafios"])

    magro = os.path.join(AQUI, "desafios-portugol.json")
    io.open(magro, "w", encoding="utf-8").write(
        json.dumps(enxuta, ensure_ascii=False, indent=2))

    if "resposta" in json.dumps(enxuta) or "lacunas" in json.dumps(enxuta):
        raise SystemExit("o desafios-portugol.json saiu com resposta dentro")

    total = sum(len(p["lacunas"]) for p in dados["passos"])
    print("gerado: %s" % os.path.basename(destino))
    print("gerado: %s  (sem resposta nenhuma)" % os.path.basename(magro))
    print("%d passos, %d lacunas" % (len(dados["passos"]), total))
    print("%d perguntas" % len(dados["perguntas"]))
    print("%d desafios de codigo, escritos em arquivo" % len(dados["desafios"]))
