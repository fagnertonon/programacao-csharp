# Aula 10 — Duelo

**17/08/2026 · 18h00 às 22h00**

Um projeto inteiro, do início ao fim, em uma noite. Dois lutadores se batem
até um cair. **72 linhas** — e você escreve todas.

---

## O que tem nesta pasta

| Arquivo / Pasta | O que é |
|:--|:--|
| [`apostila-aula-10-duelo.pdf`](apostila-aula-10-duelo.pdf) | **A apostila.** Os 8 passos, com o código de cada um |
| `apostila-aula-10-duelo.pptx` | A mesma apostila, em formato editável |
| [`duelo-inicial/`](duelo-inicial/) | **O projeto de onde partimos.** Compila e roda, mas ainda não faz nada |

---

## Como começar

1. Copie a pasta `duelo-inicial/` para um lugar seu (ex.: `Documentos\csharp\duelo`)
2. Dê duplo clique em `Duelo.csproj`
3. **F5**

Deve aparecer:

```
Duelo - o projeto de hoje comeca aqui.
```

Apareceu? Então o projeto vive e a aula pode começar.

---

## O assunto da noite: o `for`

Esta é a **primeira aula de laço de repetição**. O `for` aparece duas vezes, e
é a mesma coisa nas duas:

| Onde | O que repete |
|:--|:--|
| `Lutador.MostrarBarra` | Um `#` para cada ponto de vida — **o laço desenha** |
| `Program.Main` | Uma volta para cada rodada do duelo |

Fora o `for` (e o `Random`, que sorteia o dano), **tudo o que aparece hoje
você já viu**: classe com campos e métodos, `new`, `if / else if` e `break`.

---

## Os 8 passos

**Rode com F5 depois de cada passo.** É assim que a turma anda junto —
ninguém avança com o programa quebrado.

| # | Passo | O que aparece ao rodar |
|:--:|:--|:--|
| 1 | Abrir o `duelo-inicial` e dar F5 | Uma frase na tela. O projeto vive |
| 2 | Na classe `Lutador`, os três campos: `Nome`, `Vida`, `Forca` | Nada ainda — mas compila |
| 3 | No `Main`, criar `a` e `b`, dar nome, e mandar cada um mostrar a barra | **Erro!** `MostrarBarra` ainda não existe |
| 4 | Escrever `MostrarBarra` **sem o `for`**, só o nome e o número | `Ana       20` |
| 5 | **O `for` que desenha** | `Ana       ####################  20` |
| 6 | Escrever `Atacar` | Um bate no outro, uma vez |
| 7 | **O `for` das rodadas** + o `break` | O duelo inteiro acontece |
| 8 | O `if / else if` do vencedor | O nome do campeão no fim |

> O passo 3 **dá erro de propósito.** É o jeito mais barato de ver que o
> método precisa existir na classe antes de ser chamado. Leia a mensagem de
> erro com calma — ela está dizendo exatamente isso.

---

## Onde o programa vai chegar

```
DUELO: Ana x Bruno

--- RODADA 3 ---
Ana bate em Bruno  (-4)
Bruno bate em Ana  (-2)
Ana       ##############  14
Bruno     ###########  11
```

Sem cor, sem pausa, sem limpar a tela: a saída rola e fica tudo visível — o
que é melhor para conferir junto.

---

## Terminou antes? Tente isto

| # | Desafio |
|:--:|:--|
| 1 | Trocar o `#` por outro caractere, e mostrar a vida perdida com `.` (um segundo `for`, começando em `Vida`) |
| 2 | Deixar a barra **vermelha** quando a vida estiver baixa (`Console.ForegroundColor`) |
| 3 | Uma pausa entre as rodadas, para virar animação (`System.Threading.Thread.Sleep(700)`) |
| 4 | Um terceiro lutador — e descobrir que fica chato repetir tudo |

---

## A pergunta que fica para a próxima aula

> *"E se eu quisesse quatro lutadores em vez de dois?"*

Pense nela até a Aula 11.
