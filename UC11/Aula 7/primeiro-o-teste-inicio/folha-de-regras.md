# Folha de regras — UC11, Aula 7 · 09/09/2026

**Nome:** ______________________________________________

> **Esta folha se preenche ANTES de abrir o Visual Studio.** Ela é o contrato da
> noite: o que estiver escrito aqui é o que o programa vai ter de fazer, e o que
> não estiver escrito aqui não vai existir.

---

## Como se escreve uma regra

Três linhas, sempre nesta ordem:

```
REGRA 3:  Nao da para sacar mais do que tem na conta.
CASO:     conta com 100, sacar 150  ->  recusa, e o saldo continua 100
TESTE:    Sacar_ComValorMaiorQueSaldo_RecusaEMantemSaldo
```

| A linha | O que ela é | De onde vem |
|:--|:--|:--|
| **REGRA** | O que o sistema tem de fazer, em português | Da sua cabeça, e do quadro |
| **CASO** | Um exemplo com número, e o resultado esperado | Você escolhe os valores |
| **TESTE** | O nome do método de teste | `Metodo_Cenario_OQueDeveDar` |

> ⚠️ **Uma regra sem CASO não vira teste.** "A senha tem de ser forte" não é regra —
> é opinião. "Senha com menos de 8 caracteres é recusada, e `abc1234` tem 7" é regra.

---

## Parte 1 — A Calculadora

Cinco métodos: `Somar` · `Subtrair` · `Multiplicar` · `Dividir` · `Porcentagem`

```
REGRA 1:  ______________________________________________________________
CASO:     ______________________________________________________________
TESTE:    ______________________________________________________________

REGRA 2:  ______________________________________________________________
CASO:     ______________________________________________________________
TESTE:    ______________________________________________________________

REGRA 3:  ______________________________________________________________
CASO:     ______________________________________________________________
TESTE:    ______________________________________________________________

REGRA 4:  ______________________________________________________________
CASO:     ______________________________________________________________
TESTE:    ______________________________________________________________

REGRA 5:  ______________________________________________________________
CASO:     ______________________________________________________________
TESTE:    ______________________________________________________________

REGRA 6:  ______________________________________________________________
CASO:     ______________________________________________________________
TESTE:    ______________________________________________________________
```

### A pergunta que a turma tem de responder junto

```
       Dividir(10, 0) tem de _______________________________________
```

> **Não existe resposta certa impressa em lugar nenhum.** A turma decide, a decisão
> vai para o quadro, e a partir daí ela **é** a regra. Quem implementar diferente do
> que a turma decidiu vai ficar vermelho — e vai estar errado.

---

## Parte 2 — A Conta bancária

Quatro métodos e um saldo: `Depositar` · `Sacar` · `TemSaldo` · `Transferir`

```
REGRA 1:  ______________________________________________________________
CASO:     ______________________________________________________________
TESTE:    ______________________________________________________________

REGRA 2:  ______________________________________________________________
CASO:     ______________________________________________________________
TESTE:    ______________________________________________________________

REGRA 3:  ______________________________________________________________
CASO:     ______________________________________________________________
TESTE:    ______________________________________________________________

REGRA 4:  ______________________________________________________________
CASO:     ______________________________________________________________
TESTE:    ______________________________________________________________

REGRA 5:  ______________________________________________________________
CASO:     ______________________________________________________________
TESTE:    ______________________________________________________________

REGRA 6:  ______________________________________________________________
CASO:     ______________________________________________________________
TESTE:    ______________________________________________________________
```

---

## Antes de sair da sala

Depois que a sua suíte estiver verde, volte aqui e responda:

| | |
|:--|:--|
| Quantas regras você escreveu antes de abrir a IDE? | ______ |
| Quantas você **acrescentou** depois, ao escrever os testes? | ______ |
| Quantas apareceram só quando o professor rodou a sabotagem? | ______ |

> **A terceira coluna é a nota da noite**, e ela não vale nota nenhuma. Ela é a
> medida de quanto do sistema você não tinha visto — e todo mundo tem um número aí,
> inclusive quem programa há vinte anos.

```
       O teste que voce nao escreveu
       e o defeito que voce vai entregar.
```
