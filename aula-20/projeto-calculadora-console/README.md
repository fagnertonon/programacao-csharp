# Calculadora de console — o projeto de método

**Windows Forms não entra aqui.** Sem Designer, sem `libs/`, sem MySQL, sem
NuGet. A noite inteira é **método**: escrever muitos, e chamar uns dos outros.

| Pasta | O que é |
|:--|:--|
| [`calculadora-inicial/`](calculadora-inicial/) | O que a turma recebe. **Compila e roda** antes de qualquer TODO. 24 lacunas numeradas |
| [`calculadora-final/`](calculadora-final/) | O gabarito, no estado das 22h — só seu |

> **Este README é o do professor.** O aluno recebe a pasta `calculadora-inicial/`,
> com o [`LEIA-ME.md`](calculadora-inicial/LEIA-ME.md) dele dentro.

> **Não é a calculadora de `extras/calculadora/`.** Aquela é WinForms, uma classe
> só, 6 TODO, e serve de cobaia para os diagramas UML. Esta é console, quatro
> arquivos, 24 métodos. Quem já fez aquela reconhece as contas — e é bom que
> reconheça: o que muda hoje não é a conta, é onde ela mora.

---

## O pacto

**O que vem pronto:** o `.csproj`, o `.sln`, os quatro arquivos com os
comentários, e as assinaturas dos dez primeiros métodos.
**O que vem vazio:** os 24 corpos — e, do TODO 11 em diante, as assinaturas
também.

**Encanamento não é conteúdo.** Por isso o projeto já compila às 18h45: ninguém
perde a noite criando projeto e configurando referência.

---

## A regra que vai para o quadro às 18h35

```
       O Calculo.cs nao pode ter a palavra Console.
```

É uma frase, e ela sustenta a aula inteira:

1. **Obriga a devolver em vez de imprimir.** É o erro nº 1 de quem está
   aprendendo método — escrever `Console.WriteLine(a + b)` dentro do `Somar` e
   achar que o método está pronto. Com a regra no quadro, o erro tem nome e tem
   endereço.
2. **Obriga a chamar.** Se o `Calculo` não mostra e o `Tela` não calcula, o
   `Main` não tem escolha: ele chama os dois.
3. **Sobrevive à aula.** Um `Calculo.cs` que não conhece tela nem teclado é uma
   classe que dá para conferir **sem abrir o programa** — e é exatamente isso que
   a unidade de teste e implantação vai fazer com ela.

**Como conferir no fim da noite**, na máquina do aluno: `Ctrl+F` por `Console`
dentro do `Calculo.cs`. Tem que dar **0 ocorrências**. No gabarito dá.

---

## Os 24 métodos

| Grau | TODO | O que o aluno recebe | Quantos |
|:--:|:--|:--|:--:|
| **1** | 1–10 | A assinatura pronta, corpo com `return 0;` provisório | 10 |
| **2** | 11–19 | Só o comentário do que o método faz | 9 |
| **3** | 20–24 | Só o problema, em português | 5 |

### Onde cada um mora

| Arquivo | TODO | Métodos |
|:--|:--|:--:|
| `Calculo.cs` | 1–6 · 11–16 · 20 | **13** |
| `Tela.cs` | 7–10 · 19 · 23 | **6** |
| `Entrada.cs` | 17 · 18 · 21 · 22 | **4** |
| `Program.cs` | 24 | **1** |

### Os três que decidem a noite

| TODO | Método | Por que ele importa |
|:--:|:--|:--|
| **5** | `Dividir` | O **primeiro** método que chama outro método. Ele não decide sozinho se pode dividir: pergunta ao `PodeDividir` |
| **11** | `Media` | O **momento da aula**. Não soma e não divide — pede para o `Somar` somar e para o `Dividir` dividir. Dois métodos do aluno, chamados por um terceiro método do aluno |
| **20** | `Calcular` | A **prova da noite**. Um `switch` de seis linhas que não faz conta nenhuma: só escolhe quem chamar. Quem entendeu que método devolve valor resolve rápido; quem não entendeu tenta imprimir dentro dele e esbarra na regra do quadro |

### O TODO 4 existe por causa de uma pergunta

`PodeDividir` devolve `bool` e `Dividir` devolve `double`. **Escreva os dois no
quadro, lado a lado, e pergunte por quê.** É a diferença entre *"dá para
dividir?"* e *"quanto dá?"* — duas perguntas diferentes, dois métodos diferentes,
dois tipos de retorno diferentes.

### O TODO 21 é uma armadilha de propósito

`LerOpcao` precisa ler um inteiro e validar o intervalo. A turma vai querer
**copiar** o `LerInteiro` inteiro para dentro dele. O gabarito **chama**. Vale a
frase em voz alta: *"você já escreveu esse código uma vez — por que escrever de
novo?"*

---

## Antes das 18h

| # | |
|:--:|:--|
| 1 | Abra o `calculadora-inicial/Calculadora.sln` e **aperte F5**. Tem que abrir a janela preta e escrever as quatro linhas. Se não abrir na sua máquina, não vai abrir em treze |
| 2 | Copie a pasta `calculadora-inicial/` para o ponto de sincronização, **sem `bin/` e sem `obj/`** |
| 3 | Imprima a [folha de conferência](../folha-de-conferencia.md) — 13 cópias, uma folha |
| 4 | Deixe o gabarito **fechado**. Ele é seu, e a tentação de projetá-lo às 21h30 é real |

---

## O que fica para a próxima unidade

O `Calculo.cs` sai desta noite como uma classe de **13 métodos que só recebem e
devolvem**. Nenhum deles precisa do programa rodando para ser conferido — e a
folha de conferência que o aluno preenche à mão hoje é, linha por linha, a
tabela que a unidade de teste vai automatizar.

**O projeto não muda uma linha quando isso acontecer.** É essa a frase de
abertura de lá, e ela só é verdade por causa da regra do quadro de hoje.
