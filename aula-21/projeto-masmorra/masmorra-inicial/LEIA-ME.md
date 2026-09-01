# Masmorra — ponto de partida

**Abra o `Masmorra.sln` e aperte F5 antes de escrever qualquer coisa.**

O mapa aparece, o painel do herói aparece, três monstros nascem — e as setas do
teclado não fazem nada. É assim mesmo. O jogo já está inteiro; o que falta é o
cérebro dele, e o cérebro é seu.

---

## Você abre UM arquivo: o `Jogo.cs`

| Arquivo | Você mexe? | O que é |
|:--|:--:|:--|
| **`Jogo.cs`** | **ESCREVE** | **Os 10 TODO da noite. É o seu arquivo** |
| `frmJogo.cs` | **não** | O teclado, os monstros, o desenho na tela |
| `frmJogo.Designer.cs` | **não** | O layout, montado no Designer |
| `Personagem.cs` | lê | O molde do herói e dos monstros |
| `Sandbox.cs`, `Program.cs` | **não** | Encanamento |

**Se você precisou abrir um segundo arquivo, pare e me chame.** Quase sempre é
sinal de que a resposta estava no `Jogo.cs` e passou batido.

---

## A regra da noite

> ### O `Jogo.cs` não desenha nada e não lê teclado.
>
> Procure por `Console` e por `Windows.Forms` dentro dele: **tem que dar zero**.

Todo método de lá **recebe** valores e **devolve** resposta. Quem mostra na tela é
o `frmJogo.cs`, que já está pronto — ele *pergunta* para os seus métodos.

---

## Os 10 TODO, em ordem

| # | Método | O que muda quando você escreve |
|:--:|:--|:--|
| 1 | `PodeAndar` | **O herói começa a andar.** É o primeiro F5 que muda a tela |
| 2 | `CalcularDano` | Quanto um golpe tira — **nunca menos que 1** |
| 3 | `Bater` | A vida que sobra, nunca negativa |
| 4 | `EstaVivo` | Uma comparação |
| 5 | **`Lutar`** | **★ O `while` da noite.** A luta acontece |
| 6 | **`CalcularNivel`** | **★ O segundo `while`.** Você sobe de nível |
| 7 | `VidaMaximaDoNivel` | Subir de nível passa a dar vida |
| 8 | `ForcaDoNivel` | …e força |
| 9 | **`BarraDeVida`** | **★ O terceiro `while`.** A barra aparece no painel |
| 10 | `Situacao` | A palavra embaixo da barra |

**O chão da noite é o TODO 5.** Com ele o jogo anda e luta. Do 7 ao 10 é
acabamento — bonito, mas o jogo já roda sem.

---

## Os três `while` não são o mesmo `while`

É por isso que são três:

| TODO | Quantas voltas ele dá? |
|:--:|:--|
| **5** `Lutar` | **Ninguém sabe.** Depende da força dos dois. Só o `while` serve |
| **6** `CalcularNivel` | Ninguém sabe. Para quando o XP acaba |
| **9** `BarraDeVida` | **Dez. Sempre dez** — e por isso este também sairia com `for` |

Guarde a pergunta: **por que o 9 poderia ser `for` e o 5 não poderia?**

---

## A armadilha do `while` — e a rede que o jogo tem

Um `while` cuja condição nunca fica falsa **trava o programa**. Você já viu isso
na Aula 12.

Aqui o jogo tem rede: ele espera **2 segundos** pela sua luta, desiste, e escreve
no log o que está errado. **A janela não congela** — mas a luta não acontece.

Se aparecer *"O seu codigo passou de 2 segundos sem terminar"*, o problema é quase
sempre um destes dois:

1. **O `CalcularDano` devolve 0.** Se o golpe não tira nada, os dois ficam vivos
   para sempre. É por isso que o TODO 2 exige o mínimo de 1.
2. **A vida não muda dentro do laço.** Você calculou o dano mas não guardou o
   resultado do `Bater` de volta na vida.

---

## Uma armadilha, avisada

Cada método nasce com uma linha provisória:

```csharp
return false;   // <<< APAGUE esta linha e escreva a sua
```

Ela existe só para o projeto compilar antes de você começar. **Se ela ficar, o
método responde sempre a mesma coisa** — e você vai procurar o erro no lugar
errado.

---

## Se o emoji virar quadradinho

Abra o `frmJogo.cs`, primeiras linhas, e troque os dois símbolos por `"@"` e
`"M"`. É a **única** coisa que você pode mudar fora do `Jogo.cs`, e só se
precisar.

---

## Roteiro de teste — quando terminar

| # | O que fazer | Deve acontecer |
|:--:|:--|:--|
| 1 | Andar com as setas até a borda | O herói para na borda e não some |
| 2 | Andar para cima de um monstro | Sai o relato da luta no log |
| 3 | Derrotar um monstro | +XP, ele some do mapa e nasce outro |
| 4 | Chegar a 10 de XP | Sobe para o nível 2, e a vida máxima aumenta |
| 5 | Apanhar até a vida cair | A barra encurta e a situação muda de palavra |
| 6 | Derrotar 10 monstros | `VOCE LIMPOU A MASMORRA` |
| 7 | Deixar a vida chegar a zero | `VOCE CAIU`, e as setas param de responder |
