# Calculadora de console — ponto de partida

**Abra o `Calculadora.sln` e aperte F5 antes de escrever qualquer coisa.**

Uma janela preta abre, escreve quatro linhas e espera você teclar ENTER. É assim
mesmo. O programa já compila e já roda — o que falta são os **24 métodos** que
você vai escrever hoje.

---

## A regra da noite

> ### O `Calculo.cs` não pode ter a palavra do teclado e da tela.
>
> Procure por `Console` dentro dele quando terminar: tem que dar **zero**.

Todo método do `Calculo.cs` **recebe** valores por parâmetro e **devolve** o
resultado com `return`. Nenhum imprime nada.

Se você sentiu vontade de escrever `Console` lá dentro, o método está certo — o
**arquivo** é que está errado. Ele é do `Entrada.cs` ou do `Tela.cs`.

---

## Os quatro arquivos

| Arquivo | O que mora nele | Métodos |
|:--|:--|:--:|
| `Calculo.cs` | **Só conta.** Nada de tela, nada de teclado | 13 |
| `Entrada.cs` | Só ler do teclado e validar | 4 |
| `Tela.cs` | Só escrever na tela | 6 |
| `Program.cs` | O `Main`: o menu e o `switch` que chama os outros três | 1 |

Nenhum deles faz o trabalho sozinho. É de propósito: é isso que obriga um método
a chamar o outro.

---

## Os três graus

Procure por `TODO` no Visual Studio — **Exibir → Lista de Tarefas**, ou `Ctrl+F`.
São 24, numerados, e ficam mais difíceis de propósito:

| Grau | TODO | O que já vem pronto | O que é seu |
|:--:|:--|:--|:--|
| **1** | 1 a 10 | A assinatura inteira | O corpo (uma ou duas linhas) |
| **2** | 11 a 19 | Só a explicação do que o método faz | **A assinatura e o corpo** |
| **3** | 20 a 24 | Só o problema, em português | **Tudo** |

**No Grau 2 e no Grau 3 você escreve a assinatura.** Antes de digitar, responda
as três perguntas da Aula 17:

```
1. O que ele DEVOLVE?  ->  o tipo, ANTES do nome
2. O que ele RECEBE?   ->  os parâmetros, dentro dos ( )
3. O que ele FAZ?      ->  o corpo, entre as { }
```

---

## Uma armadilha, avisada

Os métodos do Grau 1 nascem com uma linha provisória:

```csharp
return 0;   // <<< APAGUE esta linha e escreva a sua
```

Ela existe só para o projeto compilar antes de você começar. **Se ela ficar, o
método devolve zero para sempre** — e você vai passar meia hora procurando o erro
no lugar errado.

---

## Aperte F5 a cada três TODO

Não escreva os 24 de uma vez. O programa tem que continuar compilando o tempo
inteiro — é muito mais barato achar o erro agora do que três métodos depois.

Do TODO 1 ao 23 o menu ainda não aparece: é o TODO 24 que liga tudo. Para testar
antes disso, chame o método que você acabou de escrever direto no `Main`, veja o
resultado, e apague a chamada.

---

## A vírgula é o separador decimal

`3,5` funciona. `3.5` é recusado. No Windows em português do Brasil o ponto é
separador de milhar, não decimal.

---

## Roteiro de teste — quando o TODO 24 estiver pronto

| # | O que fazer | Deve acontecer |
|:--:|:--|:--|
| 1 | Opção `1`, com `10` e `3` | `10 + 3 = 13,00` |
| 2 | Opção `4`, com `10` e `0` | *Nao da para dividir por zero* |
| 3 | Opção `6`, com `10` e `3` | `Media = 6,50` |
| 4 | Opção `7`, com `2` e expoente `0` | `Resultado = 1,00` |
| 5 | Opção `8`, com `0` | `Fatorial de 0 = 1` |
| 6 | Opção `9`, com `5` e `5` | Maior `5`, Menor `5`, **ÍMPAR** |
| 7 | Digitar `banana` onde ele pede número | Avisa e pergunta de novo |
| 8 | Digitar `12` no menu | *Escolha um numero de 0 a 9* |
| 9 | Opção `0` e responder `N` | **Volta para o menu** |
| 10 | Opção `0` e responder `S` | Sai |

Os testes **4, 5, 6 e 9** são os que separam. Expoente zero, fatorial de zero,
dois números iguais e o "não quero sair" são exatamente os casos que a gente
esquece de conferir.
