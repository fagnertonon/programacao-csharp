# Os 10 desafios de código — em arquivo, com o Portugol orientando

**O aluno escreve o método no `Desafios.cs`, roda com F5, e o corretor
executa o que ele escreveu.** É o mesmo formato das Aulas 11, 12 e 13 — o que
muda é que agora **o algoritmo em Portugol fica na tela**, à esquerda, dizendo
o que o método tem de fazer.

| Pasta | O que é |
|:--|:--|
| [`desafios-inicial/`](desafios-inicial/) | O que a turma recebe. `Desafios.cs` com os **10 TODO** |
| [`desafios-final/`](desafios-final/) | O gabarito — só seu. Cada método com o **erro típico** anotado ao lado |
| [`comum/`](comum/) | O corretor, a janela e a classe `Recado`. Os dois projetos compilam os mesmos arquivos |

---

## A tela

```
┌───────────────────────────────┬──────────────────────────────────────┐
│  O ALGORITMO EM PORTUGOL      │  public static int Procurar(...)     │
│  — e ele que diz o que fazer  │  RECEBE / FAZ / DEVOLVE              │
│                               ├──────────────────────────────────────┤
│  funcao Procurar(mural, termo)│  ok │ Procurar "apostila"  │ 2 │ 2   │
│  inicio                       │  ok │ Procurar "APOSTILA"  │ 2 │ 2   │
│     quantos <- 0              │  x  │ Procurar "prova"     │ 0 │ 3   │
│     para cada r em mural faca │                                      │
│        ...                    │  Dica: contar não é localizar...     │
└───────────────────────────────┴──────────────────────────────────────┘
```

**A esquerda é o enunciado.** Não há texto explicando o que fazer em português
corrido: há o algoritmo, escrito na língua em que a turma aprendeu a pensar. O
trabalho do aluno é traduzir — que é o assunto da noite inteira.

> ### O Portugol não pode ser copiado, e isso é de propósito
>
> A coluna da esquerda **não é uma caixa de texto**: é um painel que *desenha*
> o algoritmo. Não há o que selecionar, não há `Ctrl+C`, não há menu de
> contexto.
>
> **Sem isso o exercício não acontecia.** O Portugol é tão parecido com o C#
> que colar e ajustar sairia mais rápido do que traduzir — e traduzir é a noite
> inteira. O caminho do algoritmo até o programa passa pelos dedos do aluno.
>
> A mesma trava vale para o arquivo de conteúdo: o que vai para o lado do
> executável é o **`desafios-portugol.json`**, que tem só o algoritmo, a
> assinatura, a ficha e a dica. O `passos-mural.json` completo — que tem as
> respostas das lacunas e das perguntas do outro app — **não vai junto**, e o
> conferidor reprova se ele aparecer lá.

**A direita é a verdade.** O corretor chama o método com valores que ele mesmo
monta e compara o que voltou com o que era esperado, teste a teste. Não há
"parece certo".

---

## Os 10 desafios

Todos saem do Mural, e todos são o conteúdo da noite:

| # | Método | O que exercita |
|:--:|:--|:--|
| 1 | `CriarRecado(autor, texto)` | `new`, campo, `return` — **e usar o parâmetro** |
| 2 | `Descrever(r)` | concatenação, e o retorno como resposta |
| 3 | `Saudacao(quantos)` | `switch` com `default` |
| 4 | `NomeValido(autor)` | `bool` de retorno, e `Trim` antes de contar |
| 5 | `Gravar(mural, r)` | ordem das operações: numerar **antes** de acrescentar |
| 6 | `Listar(mural)` | **`for` invertido** — o único que precisa da posição |
| 7 | `Procurar(mural, termo)` | `foreach` + acumulador — contar não é localizar |
| 8 | `PrimeiroDoAutor(mural, autor)` | ⭐ **`return` dentro × fora do laço**, e o `null` |
| 9 | `Resumir(texto, limite)` | guarda antes de cortar |
| 10 | `MaisRecente(mural)` | melhor parcial, `null` inicial, e a ordem do `\|\|` |

⭐ **O 8 é o intocável.** É o erro que a prova cobrou e que o registro aponta
como o que a turma mais erra: `return null` dentro do `foreach`.

---

## O que os testes pegam

Não são testes de fachada. Cada um foi escrito contra um erro real:

| Desafio | O teste que pega |
|:--|:--|
| 1 | Cria um **Bruno** depois do Ana — pega o nome fixo entre aspas |
| 2 | Compara letra por letra — pega o espaço esquecido depois dos dois-pontos |
| 4 | `"   "` só com espaços → **false**; `"  Bia  "` → **true** |
| 5 | Grava **dois** recados — o segundo pega quem acrescentou antes de numerar |
| 6 | Mural vazio → lista vazia, e a ordem completa dos três |
| 7 | `"APOSTILA"` maiúsculo, e um termo que ninguém falou |
| 8 | Procura o **Bruno**, que não é o primeiro da lista — pega o `return null` no lugar errado |
| 9 | Texto **do tamanho exato** do limite, que não pode ser cortado |
| 10 | Mural vazio → `null` |

**Se o método do aluno estourar** — passar da última posição, mexer em algo
nulo —, o corretor não morre junto: ele mostra o erro na linha do teste.

---

## A prova de que os testes valem

Duas execuções, e as duas rodam no [`conferir-material.py`](../conferir-material.py):

```bash
DesafiosCodigoGabarito.exe --autoteste     # tem de devolver 0
DesafiosCodigo.exe --autoteste             # tem de devolver 1
```

| | O que prova |
|:--|:--|
| **O gabarito passa em tudo** — 33 de 33 | Os valores esperados estão certos |
| **O inicial reprova** — 0 de 10 desafios fechados | **Nenhum desafio passa sem o aluno escrever nada** |

> **O segundo já pegou erro de verdade.** Um desafio foi resolvido no arquivo
> da turma durante um teste e não voltou ao `TODO` — o `Descrever` chegaria
> pronto na mão deles. O conferidor não aceita mais só "reprovou": ele exige
> **zero desafios fechados**, e diz o que aconteceu.

O segundo é o que mais importa. Oito testes soltos passam com os `TODO` vazios,
porque o `return` provisório coincide com o esperado em casos como *"não achou →
null"*. Mas **nenhum desafio fecha inteiro** — e é isso que o corretor cobra.

---

## Como o aluno usa

| # | |
|:--:|:--|
| 1 | Abre o `Desafios.csproj` de `desafios-inicial/` e roda com **F5** |
| 2 | Escolhe um desafio na trilha e **lê o Portugol da esquerda** |
| 3 | Escreve o método no `Desafios.cs` |
| 4 | **Fecha o programa**, roda de novo com F5, clica em **Conferir** |

> **Fechar antes de rodar de novo não é capricho.** Editar o arquivo com o
> programa aberto não muda nada: o executável que está rodando já foi
> compilado. É o mesmo aviso que a Aula 16 leva no quadro.

**Todos liberados desde o início** — empacou num, pula e volta. É a decisão da
Aula 13, e está no registro.

---

## Onde ele entra na noite

Este projeto **não é o caminho principal**. A noite é o Mural, com a folha B na
mão e o `mural-inicial` no Visual Studio.

| Quando | Para quem |
|:--|:--|
| **Depois do Bloco 5**, se sobrar tempo | Quem fechou o `Procurar` e quer mais |
| **A noite inteira** | Quem já sabe o assunto e acha o Mural lento |
| **Em casa** | Todo mundo — roda offline, sem banco, sem GitHub |
| **Na aula seguinte** | Como retomada, se a noite do Mural render menos que o previsto |

> ✅ **Verificado nesta máquina.** Os dois projetos compilam com 0 erro e 0
> aviso, os dois executáveis abrem, o gabarito fecha **33 de 33 testes**, e o
> inicial fecha **0 de 10 desafios**. As dez assinaturas do JSON conferem com
> as dos dois `Desafios.cs`.
>
> ⚠️ **O que NÃO foi testado:** a aparência da janela, que é montada em código.
> **Abra uma vez antes da aula.**
