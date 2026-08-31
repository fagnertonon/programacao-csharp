# Folha de conferência — Calculadora de console

```
NOME: ______________________________________________   DATA: ____/____/2026
```

**Como preencher.** Rode o seu programa, faça o que está na coluna *O que fazer*
e escreva na última coluna **o que apareceu na sua tela**. Se bateu com o
esperado, marque ✓. Se não bateu, escreva o que apareceu — **o valor errado vale
mais que o campo em branco**, porque é ele que diz onde procurar.

> Marque com ★ as linhas que você errou de primeira. São essas que você vai
> querer olhar de novo.

---

| # | Método | O que fazer | Esperado | Obtido |
|:--:|:--|:--|:--|:--:|
| 1 | `Somar` | Opção **1** · `10` e `3` | `13,00` | |
| 2 | `Somar` | Opção **1** · `-5` e `5` | `0,00` | |
| 3 | `Subtrair` | Opção **2** · `10` e `3` | `7,00` | |
| 4 | `Multiplicar` | Opção **3** · `10` e `3` | `30,00` | |
| 5 | `Multiplicar` | Opção **3** · `7` e `0` | `0,00` | |
| 6 | `Dividir` | Opção **4** · `10` e `4` | `2,50` | |
| 7 | **`PodeDividir`** | Opção **4** · `10` e **`0`** | *Nao da para dividir por zero* | |
| 8 | `Porcentagem` | Opção **5** · `200` e `15` | `30,00` | |
| 9 | `Porcentagem` | Opção **5** · `80` e `100` | `80,00` | |
| 10 | `Media` | Opção **6** · `10` e `3` | `Media = 6,50` | |
| 11 | **`Media`** | Opção **6** · `5` e `5` | `Media = 5,00` | |
| 12 | `Potencia` | Opção **7** · `2` e expoente `5` | `32,00` | |
| 13 | **`Potencia`** | Opção **7** · `2` e expoente **`0`** | `1,00` | |
| 14 | `Potencia` | Opção **7** · `2` e expoente `1` | `2,00` | |
| 15 | `Fatorial` | Opção **8** · `5` | `120` | |
| 16 | `Fatorial` | Opção **8** · `1` | `1` | |
| 17 | **`Fatorial`** | Opção **8** · **`0`** | `1` | |
| 18 | `Maior` / `Menor` | Opção **9** · `9` e `4` | Maior `9`, Menor `4` | |
| 19 | **`Maior` / `Menor`** | Opção **9** · **`5` e `5`** | Maior `5`, Menor `5` | |
| 20 | `Maior` / `Menor` | Opção **9** · `-2` e `-9` | Maior `-2`, Menor `-9` | |
| 21 | `EhPar` | Opção **9** · `7` e qualquer | `O primeiro e IMPAR` | |
| 22 | **`EhPar`** | Opção **9** · **`0`** e qualquer | `O primeiro e PAR` | |
| 23 | `EhPar` | Opção **9** · `-4` e qualquer | `O primeiro e PAR` | |
| 24 | `LerNumero` | Opção **1** · digite `banana` | Avisa e pergunta de novo | |
| 25 | `LerOpcao` | No menu, digite `12` | *Escolha um numero de 0 a 9* | |
| 26 | **`Confirmar`** | Opção **0** e responda **`N`** | **Volta para o menu** | |
| 27 | `Confirmar` | Opção **0** e responda `S` | Sai | |

---

## As sete linhas em negrito

Elas não estão em negrito por acaso. São os casos que **funcionam por engano** —
o programa parece certo até alguém digitar exatamente aquilo:

| # | Por que ela pega |
|:--:|:--|
| **7** | Sem a guarda, dividir por zero em `double` não dá erro: dá `∞`. O programa não quebra, só mente |
| **11** | Média de dois números iguais tem que dar o próprio número. Se deu outra coisa, o `Dividir(soma, 2)` virou `Dividir(soma, a)` em algum lugar |
| **13** | Todo número elevado a zero dá **1**. Quem começou o acumulador em `0` em vez de `1` acerta todos os outros expoentes e erra só este |
| **17** | Fatorial de zero dá **1**, por definição. Quem começou o `for` no `1` acerta; quem começou no `0` zera tudo |
| **19** | Dois números iguais. `if (a > b)` com `5` e `5` cai no `else` — e tem que devolver `5` do mesmo jeito |
| **22** | Zero **é par**. `0 % 2` dá `0`, e `0 == 0` é verdadeiro. Muita gente escreve o método achando que zero é caso especial |
| **26** | Escolher sair e dizer que não quer. Se o programa sair mesmo assim, o `Confirmar` está sendo ignorado |

---

## Depois de preencher

```
Quantas linhas bateram de primeira: _______ de 27

A que mais me custou: _____________________________________________________

O que estava errado nela: _________________________________________________

__________________________________________________________________________
```

> **Guarde esta folha.** Ela volta na próxima unidade curricular — e lá o
> computador é que vai preencher a coluna *Obtido*, sozinho, sem você rodar o
> programa. Estas 27 linhas são o rascunho disso.
