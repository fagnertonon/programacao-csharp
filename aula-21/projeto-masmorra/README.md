# Masmorra — o RPG top-down do `while`

**O aluno abre um arquivo só.** O formulário, o teclado, o mapa e os monstros vêm
prontos; o `Jogo.cs` tem os 10 `TODO`, e é o único que ele toca.

| Pasta | O que é |
|:--|:--|
| [`masmorra-inicial/`](masmorra-inicial/) | O que a turma recebe. **Compila e roda** — o mapa aparece e o herói não anda |
| [`masmorra-final/`](masmorra-final/) | O gabarito, no estado das 22h — só seu |

> **Este README é o do professor.** O aluno recebe `masmorra-inicial/`, com o
> [`LEIA-ME.md`](masmorra-inicial/LEIA-ME.md) dele dentro.

---

## O pacto

**Vem pronto:** o `.csproj`, o `.sln`, o Designer inteiro, o `frmJogo.cs` com
teclado e monstros, o `Personagem.cs` e o `Sandbox.cs`.
**Vem vazio:** os 10 métodos do `Jogo.cs`.

É o corte que o roteiro da Corrida Maluca chama de *"o mais importante de todos"*:
**o aluno não escreve a mecânica do jogo.** Encanamento não ensina `while`.

---

## A regra que vai para o quadro às 18h05

```
       O Jogo.cs nao desenha e nao le teclado.
```

Ela faz duas coisas. Primeiro, obriga o aluno a **devolver** em vez de imprimir —
o erro nº 1 de quem aprende método. Segundo, deixa a classe **conferível sem abrir
o jogo**: os 10 métodos são funções puras, e é por isso que eu consegui rodar os
34 casos de teste deles aqui, sem Windows.

**Como conferir às 21h50:** `Ctrl+F` por `Console` e por `Windows.Forms` dentro do
`Jogo.cs`. Tem que dar **0** nos dois.

---

## Os três `while`, e por que são três

| TODO | Método | A forma do laço |
|:--:|:--|:--|
| **5** | `Lutar` | **Quantas voltas? Ninguém sabe.** É o `while` que só o `while` resolve |
| **6** | `CalcularNivel` | Acumulador **consumido** dentro do corpo |
| **9** | `BarraDeVida` | Contagem até dez — **e este também sairia com `for`** |

**A pergunta do quadro, no fim da noite:** *por que o 9 poderia ser `for` e o 5
não poderia?* É a distinção que a Aula 11 desenhou no diagrama `for-vira-while` e
que a prova de 21/08 cobrou como "escolha do laço".

---

## Os quatro momentos

| TODO | O que fazer no projetor |
|:--:|:--|
| **1** `PodeAndar` | Escreva junto e mande apertar F5 **na hora**. O herói anda. É o momento em que a turma entende que o jogo estava esperando por eles |
| **2** `CalcularDano` | **Pare aqui.** Pergunte: *"e se a defesa do monstro for maior que a minha força?"* A resposta — dano 0 — é o que trava o `while` do TODO 5. Deixe a frase no quadro antes de chegar lá |
| **5** `Lutar` | **O momento da noite.** No projetor, junto. Três passos: bato, confiro se caiu, apanho |
| **9** `BarraDeVida` | A guarda antes de dividir. `vidaMaxima` zero quebra o programa — é a mesma guarda que metade da turma esqueceu na prova de 21/08 |

---

## A rede contra o laço infinito — e por que ela existe

O `while` do `Lutar` roda na **mesma thread que desenha a tela**. Um erro do aluno
congelaria a janela de verdade, e só o Gerenciador de Tarefas resolveria — com 15
máquinas, isso mataria a noite.

O `Sandbox.cs` é **o mesmo arquivo da Aula 12**, copiado sem alteração: ele roda o
`Lutar` com prazo de 2 segundos, desiste, e escreve no log a mensagem que já
estava pronta em português. Conferido aqui: corta em exatos 2000 ms.

E o `frmJogo.cs` chama o `Lutar` sobre **cópias** do herói e do monstro, aplicando
o resultado só quando a luta termina no prazo. O aluno não vê nada disso.

---

## Antes das 18h

| # | |
|:--:|:--|
| 1 | Abra `masmorra-inicial/Masmorra.sln` e **aperte F5**. O mapa tem que aparecer com três monstros, e as setas não devem fazer nada |
| 2 | **Confira o emoji.** Se o herói ou os monstros saírem como quadradinho, troque as duas constantes no topo do `frmJogo.cs` por `"@"` e `"M"` — e faça isso **nas duas pastas**, antes de copiar para o ponto de sincronização |
| 3 | Copie `masmorra-inicial/` para o ponto de sincronização, **sem `bin/` e sem `obj/`** |
| 4 | Deixe o `masmorra-final/` fechado |

---

## O que foi verificado, e o que não foi

| Verificado aqui | Como |
|:--|:--|
| Os dois projetos compilam, 0 erros e 0 avisos | Compilação contra as DLLs de referência do WindowsDesktop |
| Os 10 métodos do gabarito | **34 casos**, incluindo as bordas: força igual à defesa, vida zerando exata, XP no limite do nível, e divisão por zero nos TODO 9 e 10 |
| A luta sempre termina | **20 000 lutas aleatórias**, nenhuma passando de 117 golpes, nenhuma terminando com os dois vivos |
| A rede do laço infinito | Com dano 0 de propósito: cortou em 2000 ms com a mensagem certa |

| **NÃO verificado** | Por quê |
|:--|:--|
| **A tela rodando** | Não há Windows nesta máquina. O layout foi conferido à mão contra o `frmLogin.Designer.cs` da Aula 15 |
| **O emoji** | Depende da fonte da máquina. **É o item 2 do "antes das 18h"** |
