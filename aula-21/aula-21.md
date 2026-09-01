# Aula 21 — terça-feira, 01/09/2026

## Um `while` que não sabe quantas voltas vai dar

| | |
|:--|:--|
| **Data** | 01/09/2026 · Laboratório de Informática Geral · 18h–22h |
| **Categoria (SIG)** | **Laboratório de práticas** |
| **Projeto** | [`projeto-masmorra/`](projeto-masmorra/README.md) — WinForms, sem banco, sem NuGet |
| **Material do aluno** | A pasta `masmorra-inicial/` |
| **Conhecimentos** | **2** (estrutura de dados) · **7** (POO) |
| **Habilidades** | **1** · **3** · **7** |

---

## A ideia da noite

Ontem foram 24 métodos em quatro arquivos de console. Hoje é o oposto: **um
arquivo só**, com um jogo inteiro construído em volta dele.

`while` não é assunto novo — entrou na Aula 11, o aluno escreveu o primeiro na
Aula 12 (o `SugerirLogin`), foi cobrado na prova de 21/08 e voltou ontem, na
calculadora. **O que falta é usá-lo onde ele é a única ferramenta que serve.**

Uma luta é isso. Ela dura o quanto durar: depende da força dos dois, e ninguém
sabe de antemão quantos golpes vão ser trocados. `for` não escreve isso.

### O que muda em relação à Aula 20

| Ontem | Hoje |
|:--|:--|
| Quatro arquivos, 24 métodos | **Um arquivo, 10 métodos** |
| Console | Windows Forms, com o Designer todo pronto |
| Cada método isolado | Os métodos se chamam: o `Lutar` usa quatro dos outros |
| `while` para insistir na leitura | **`while` que não sabe quantas voltas dá** |

---

## O jogo

**Masmorra** — RPG visto de cima, num mapa de 12 por 8. O herói anda com as setas;
três monstros nascem no começo e outro aparece a cada um derrotado. Andar para
cima de um monstro resolve a luta na hora, e o relato sai no log. Vence quem
derrubar 10; perde quem chegar a 0 de vida.

**Herói e monstros são `Label` com emoji**, movidos pelo `.Location` — o mesmo
truque do *Botão Fujão*. Sem imagem, sem `Paint`, sem `Timer` movendo coisa
sozinha: **nada se mexe sem o aluno apertar uma tecla.**

---

## A regra que vai para o quadro às 18h05

```
       O Jogo.cs nao desenha e nao le teclado.
```

`Ctrl+F` por `Console` e por `Windows.Forms` dentro do `Jogo.cs` tem que dar
**zero** nos dois. É a mesma regra de ontem, num lugar novo — e é ela que faz os
10 métodos serem conferíveis sem abrir o jogo.

---

## Os 10 TODO, e os três `while`

| # | Método | Estreia |
|:--:|:--|:--|
| 1 | `PodeAndar` | **O primeiro F5 que muda a tela** — o herói anda |
| 2 | `CalcularDano` | O mínimo de 1, que é o que faz o laço do 5 terminar |
| 3 | `Bater` | Nunca abaixo de zero |
| 4 | `EstaVivo` | |
| **5** | **`Lutar`** | **★ O `while` da noite** — voltas de quantidade desconhecida |
| **6** | **`CalcularNivel`** | **★** Acumulador consumido dentro do corpo |
| 7 | `VidaMaximaDoNivel` | |
| 8 | `ForcaDoNivel` | |
| **9** | **`BarraDeVida`** | **★** Contagem até dez — o único que também sairia com `for` |
| 10 | `Situacao` | Cadeia de `if` / `else if` |

---

## Roteiro em blocos

| Horário | Bloco |
|:--|:--|
| 18h00–18h05 | F5 antes de escrever nada: o mapa aparece, o herói não anda |
| 18h05–18h15 | A regra do quadro · por que o `Jogo.cs` é o único arquivo |
| 18h15–19h15 | **TODO 1 a 4.** O 1 no projetor — e F5 na hora. Pare no 2 para a pergunta |
| 19h15–20h00 | Prática: os quatro escritos, e o log já reclamando que o `Lutar` não devolve nada |
| 20h00–20h15 | **Intervalo** |
| 20h15–21h10 | **TODO 5 e 6 — o `while`.** O 5 no projetor, junto; o 6 é prática |
| 21h10–21h55 | **TODO 7 a 10.** Sem conceito novo. O jogo fica jogável e a barra aparece |
| 21h55–22h00 | Fechamento · `Ctrl+F` por `Console` · a pasta sobe |

> **Nada de conceito novo depois das 21h10.** O que é novo — o `while` de
> quantidade desconhecida — acontece às 20h15, com a turma inteira descansada do
> intervalo.

---

## Os quatro momentos de projetor

| TODO | O que fazer |
|:--:|:--|
| **1** | Escreva junto e mande apertar F5 **na hora**. O herói anda, e a turma entende que o jogo estava só esperando por eles |
| **2** | **Pare.** Pergunte: *"e se a defesa do monstro for maior que a minha força?"* A resposta é dano 0 — e dano 0 trava o `while` do TODO 5. **Deixe essa frase no quadro** |
| **5** | O momento da noite. Três passos: bato, confiro se caiu, apanho. E a frase: *"quantas voltas esse laço vai dar? Ninguém sabe — e é por isso que não dá para usar `for`"* |
| **9** | A guarda antes de dividir. `vidaMaxima` zero quebra o programa — é a mesma guarda que metade da turma esqueceu na prova de 21/08 |

---

## ⚠️ O risco da noite, e a rede que já existe

O `while` do `Lutar` roda **na mesma thread que desenha a tela**. Errar a condição
congelaria a janela de verdade — e com 15 máquinas isso mataria a noite.

**O `Sandbox.cs` da Aula 12 foi copiado sem alteração para dentro do projeto.** Ele
dá 2 segundos à luta, desiste, e escreve no log a mensagem que já estava pronta:
*"confira se alguma coisa muda DENTRO do corpo do while"*. Conferido aqui: corta em
exatos 2000 ms.

**Diga isso em voz alta antes do TODO 5.** Saber que o jogo tem rede é o que faz o
aluno tentar em vez de congelar junto com a janela.

---

## Os dois caminhos pela mesma noite

**O chão é o TODO 5.** Quem chegar lá tem o jogo andando e lutando — e tem os dois
`while` que importam. Do 7 ao 10 é acabamento.

**Se a turma atrasar**, corte nesta ordem:

| Corte | O quê |
|:--:|:--|
| 1º | TODO 10 (`Situacao`) — é uma cadeia de `if`, e não acrescenta laço |
| 2º | TODO 7 e 8 — o jogo roda sem subir de nível |
| **Nunca** | **TODO 2 e TODO 5** — o 2 é o que faz o 5 terminar, e o 5 é a aula |

---

## Antes das 18h

| # | |
|:--:|:--|
| 1 | Abrir `masmorra-inicial/Masmorra.sln` e **apertar F5**. Mapa com três monstros, setas sem efeito |
| 2 | **Conferir o emoji.** Quadradinho? Trocar as duas constantes do topo do `frmJogo.cs` por `"@"` e `"M"`, **nas duas pastas** |
| 3 | Copiar `masmorra-inicial/` para o ponto de sincronização, sem `bin/` e sem `obj/` |
| 4 | Deixar o `masmorra-final/` fechado |

---

## Avaliação da noite

**Não vale nota.** O que ela produz é evidência para a ficha:

| O que observar | Indicador |
|:--|:--:|
| O projeto compila, e o aluno lê o erro quando não compila | **I4** |
| Os métodos com assinatura respeitada e `return` no lugar | **I3** |
| `Jogo.cs` com zero `Console` e zero `Windows.Forms`; a guarda antes de dividir no TODO 9 | **I2** |

---

## Depois da aula — o que ficou para a próxima

> **Preencher antes de sair.**

**Quantos chegaram ao TODO 5 (o chão), e quantos ao 10:**

_______________________________________________________________________

**Quantos viram a mensagem dos 2 segundos, e o que estava errado neles:**

_______________________________________________________________________

**Quantos souberam responder por que o TODO 9 poderia ser `for` e o 5 não:**

_______________________________________________________________________

**Ocorrências da noite** (para o campo Observações do SIG):

_______________________________________________________________________
