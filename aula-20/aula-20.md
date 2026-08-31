# Aula 20 — segunda-feira, 31/08/2026

## Um método chama outro método: a calculadora de console

| | |
|:--|:--|
| **Data** | 31/08/2026 · Laboratório de Informática Geral · 18h–22h |
| **Categoria (SIG)** | **Laboratório de práticas** |
| **Projeto** | [`projeto-calculadora-console/`](projeto-calculadora-console/) — console, sem banco, sem NuGet |
| **Material do aluno** | A pasta `calculadora-inicial/` + a [folha de conferência](folha-de-conferencia.md) impressa |
| **Conhecimentos** | **7** (POO) · **2** (estrutura de dados — o acumulador e o laço) |
| **Habilidades** | **1** (resolver problemas lógicos) · **3** (interpretar textos técnicos) · **7** (analisar as etapas do processo) |

> **A noite tem duas partes que não se misturam.** Os primeiros 35 minutos são a
> **roda de conversa** da folha de pesquisa de sexta, já prometida na
> [`aula-19.md`](../aula-19/aula-19.md). A calculadora começa às 18h35.

---

## A ideia da noite

A Aula 17 entregou o algoritmo pronto em Portugol e pediu a tradução para C#. O
aluno decidiu tipo de retorno, parâmetro e nome — mas sempre com o algoritmo do
lado, dizendo o que fazer.

Hoje o apoio sai aos poucos, e entra uma coisa que ainda não apareceu: **um
método que o aluno escreveu chamando outro método que o aluno escreveu.**

O veículo é uma calculadora de console. Não tem tela para montar, não tem banco
para conectar, não tem `libs/` para copiar. **Não há encanamento nenhum** — e é
por isso que dá para escrever 24 métodos numa noite.

### O que muda em relação à Aula 17

| Na Aula 17 | Hoje |
|:--|:--|
| O algoritmo vinha pronto | Do TODO 11 em diante, só o problema em português |
| A assinatura estava quase escrita | Do TODO 11 em diante, a assinatura é dele |
| Cada método vivia sozinho | O `Media` chama o `Somar` e o `Dividir` |
| Um arquivo | Quatro, e nenhum resolve sozinho |

---

## A regra que vai para o quadro às 18h35

```
       O Calculo.cs nao pode ter a palavra Console.
```

Escreva, sublinhe, e deixe lá a noite inteira.

Ela existe porque o erro nº 1 de quem está aprendendo método é escrever
`Console.WriteLine(a + b)` dentro do `Somar` e achar que terminou. Com a regra no
quadro, esse erro deixa de ser uma correção sua e vira uma regra do projeto — o
aluno se pega sozinho.

E ela tem um efeito de estrutura: **se o `Calculo` não mostra e o `Tela` não
calcula, o `Main` é obrigado a chamar os dois.** A separação em quatro arquivos
não é organização; é o que força a chamada.

**Como conferir às 21h50:** `Ctrl+F` por `Console` dentro do `Calculo.cs` de cada
um. Tem que dar **0**.

---

## Os quatro arquivos

| Arquivo | O que mora nele | Métodos |
|:--|:--|:--:|
| `Calculo.cs` | Só conta. Recebe e devolve, nunca mostra | **13** |
| `Tela.cs` | Só escreve | **6** |
| `Entrada.cs` | Só lê do teclado e valida | **4** |
| `Program.cs` | O `Main`: o menu, o `while` e o `switch` | **1** |

---

## Os 24 métodos, em três graus

| Grau | TODO | O que o aluno recebe |
|:--:|:--|:--|
| **1** | 1–10 | A assinatura pronta. Escreve só o corpo, de uma ou duas linhas |
| **2** | 11–19 | Só o comentário do que o método faz. **Escreve a assinatura e o corpo** |
| **3** | 20–24 | Só o problema, em português. Escreve tudo |

### Os quatro momentos que decidem a noite

| TODO | Método | O que fazer no projetor |
|:--:|:--|:--|
| **4 e 5** | `PodeDividir` e `Dividir` | Escreva as **duas assinaturas lado a lado no quadro** e pergunte: *por que uma devolve `bool` e a outra devolve `double`?* A resposta é que são duas perguntas diferentes — *"dá para dividir?"* e *"quanto dá?"*. É a primeira vez que o tipo de retorno é uma **decisão**, e não uma cópia |
| **11** | `Media` | **O momento da aula.** Diga em voz alta: *"o `Media` não sabe somar. Ele pede para o `Somar` somar."* Escreva no projetor, e deixe a turma ver que o corpo tem duas linhas e nenhuma conta |
| **20** | `Calcular` | **A prova.** Um `switch` de seis linhas que não calcula nada — só escolhe quem chamar. Quem entendeu resolve em cinco minutos. Quem vai tentar imprimir dentro dele esbarra na regra do quadro, e aí a regra ensina sozinha |
| **21** | `LerOpcao` | **A armadilha.** A turma vai querer copiar o `LerInteiro` inteiro para dentro. O gabarito **chama**. A frase: *"você já escreveu esse código uma vez — por que escrever de novo?"* |

---

## Roteiro em blocos

| Horário | Bloco | O que acontece |
|:--|:--|:--|
| 18h00–18h35 | **Roda de conversa** | A folha de pesquisa de sexta. Cada aluno conta o que achou; a curiosidade vai para o quadro **com o nome de quem achou**. Anote as áreas circuladas e as respostas da pergunta 5 |
| 18h35–18h45 | **Abertura** | F5 antes de escrever nada — a janela preta abre e escreve quatro linhas. A **regra do quadro**. Os quatro arquivos, e por que são quatro |
| 18h45–19h15 | **Grau 1** | TODO 1 a 10. Rode a cada três. Pare no 4 e 5 para a pergunta do `bool` × `double` |
| 19h15–20h00 | **Grau 2, parte 1** | TODO 11 a 16 — o bloco do `Calculo`. O **11 é no projetor**, os outros cinco são prática |
| 20h00–20h15 | **Intervalo** | Inegociável |
| 20h15–21h10 | **Grau 2 parte 2 + o 20** | TODO 17, 18 e 19 (`Entrada` e `Tela`), e o **TODO 20** ainda com fôlego. É o último conceito novo da noite |
| 21h10–21h55 | **Grau 3** | TODO 21 a 24. Sem conceito novo: `while`, `switch` e chamada. A calculadora fica de pé e a **folha de conferência** é preenchida |
| 21h55–22h00 | **Fechamento** | A pasta sobe para o ponto de sincronização. `Ctrl+F` por `Console` no `Calculo.cs` |

> **Nada de conceito novo depois das 21h10.** O que é novo hoje — assinatura
> decidida pelo aluno, e método chamando método — acontece antes disso. Do TODO
> 21 ao 24 é só aplicar o que já foi visto.

---

## Os dois caminhos pela mesma noite

**O piso.** Quem chegar no TODO 16 tem o `Calculo.cs` **inteiro** — os 12
primeiros métodos de conta, escritos e conferidos. É esse arquivo que vale para a
próxima unidade curricular, e é ele que evidencia o Conhecimento 7. Um aluno que
parou no 16 **não parou pela metade**: parou com a parte que importa.

**O teto.** O `Program.cs` completo, os 24 TODO e a folha de conferência
preenchida.

> **Diga isso em voz alta às 18h40.** A turma noturna desiste quando acha que
> "não vai dar tempo de terminar". Se o piso está dito desde o começo, ninguém
> para de tentar às 21h.

---

## ⚠️ O risco da noite, e o que fazer a respeito

**O TODO 24 é grande.** São dez `case` que repetem quase a mesma sequência, e ele
cai justamente no bloco mais cansado.

**O que fazer:** escreva **os `case` 1 e 4 juntos, no projetor**, às 21h15 — o 1
porque é o molde, e o 4 porque é o único diferente (tem a guarda do
`PodeDividir`). Os outros oito o aluno faz por analogia, e quem não terminar
todos ainda tem a calculadora rodando com os que fez.

**Se a turma atrasar**, corte nesta ordem, e nunca fora dela:

| Corte | O quê | Por quê |
|:--:|:--|:--|
| 1º | Os `case` 7, 8 e 9 do TODO 24 | A calculadora roda com as cinco primeiras opções e o menu continua de pé |
| 2º | O TODO 22 (`Confirmar`) | O `case 0` sai direto, sem perguntar |
| **Nunca** | **Os TODO 11 e 20** | São os dois que fazem a noite existir. Se um deles cair, a aula virou uma lista de exercícios |

---

## Antes das 18h

| # | |
|:--:|:--|
| 1 | Abrir o `calculadora-inicial/Calculadora.sln` e **apertar F5**. Tem que abrir a janela preta e escrever as quatro linhas. **Se não abrir na sua máquina, não vai abrir em treze** |
| 2 | Copiar a pasta `calculadora-inicial/` para o ponto de sincronização, **sem `bin/` e sem `obj/`** |
| 3 | Imprimir a [folha de conferência](folha-de-conferencia.md) — **13 cópias, uma folha, só frente** |
| 4 | Levar a folha de pesquisa de quem entregar, para a roda de conversa |
| 5 | Deixar o `calculadora-final/` **fechado**. A tentação de projetá-lo às 21h30 é real |

---

## Avaliação da noite

**Não vale nota.** É aula de laboratório, e o que ela produz é evidência para a
ficha de indicadores:

| O que observar | Indicador |
|:--|:--:|
| O projeto compila e o aluno lê o erro do compilador quando não compila | **I4** |
| Os métodos do Grau 2 e 3 com assinatura correta — tipo de retorno, parâmetros e `return` no lugar | **I3** |
| `Calculo.cs` com **zero** ocorrências de `Console`, e o `LerOpcao` **chamando** o `LerInteiro` em vez de copiá-lo | **I2** |

> **O `Ctrl+F` por `Console` é o instrumento mais rápido da noite.** Em dez
> segundos por máquina você sabe quem entendeu que método devolve valor.

---

## Depois da aula — o que ficou para a próxima

> **Preencher antes de sair, enquanto está fresco.**

**Quantos chegaram ao TODO 16 (o piso), e quantos ao 24 (o teto):**

_______________________________________________________________________

**Quantos `Calculo.cs` deram zero no `Ctrl+F` por `Console`:**

_______________________________________________________________________

**Quantos copiaram o `LerInteiro` dentro do `LerOpcao` em vez de chamá-lo:**

_______________________________________________________________________

**A linha da folha de conferência que mais gente errou:**

_______________________________________________________________________

**Da roda de conversa: as áreas mais circuladas e os programas que eles disseram
que criariam:**

_______________________________________________________________________

**Ocorrências da noite** (para o campo Observações do SIG):

_______________________________________________________________________

---

## O que vem depois

O `Calculo.cs` sai desta noite como **13 métodos que só recebem e devolvem**.
Nenhum deles precisa do programa rodando para ser conferido — e a folha de
conferência que o aluno preencheu à mão hoje é, linha por linha, a tabela que a
unidade de teste e implantação vai automatizar.

**E o projeto não vai mudar uma linha quando isso acontecer.** É essa a frase de
abertura de lá — e ela só é verdade por causa da regra que foi para o quadro às
18h35 de hoje.
