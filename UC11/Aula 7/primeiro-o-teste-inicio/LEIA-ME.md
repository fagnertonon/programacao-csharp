# Primeiro o teste — o projeto da Aula 7

**UC11 · quarta-feira, 09/09/2026 · 18h–22h**

## Comece por aqui: dê dois cliques no `PrimeiroOTeste.sln`

**É o único arquivo que você abre hoje.** Ele traz os dois projetos da noite já
ligados um no outro — você não precisa criar projeto nenhum nem configurar
referência nenhuma.

```
       PrimeiroOTeste.sln       <-- abra ESTE
          |
          +-- ProducaoTela      A JANELA, que voce vai olhar
          +-- Producao          as regras que ainda NAO existem
          +-- ProducaoTestes    os testes que voce vai escrever
```

| O quê | Para quê |
|:--|:--|
| `producao-tela/` | **A JANELA.** Windows Forms, do mesmo tipo que você fez a UC12 inteira |
| `producao/Calculadora.cs` | Cinco métodos com nome, parâmetros e tipo de retorno — e **nenhum corpo** |
| `producao/Conta.cs` | Quatro métodos e um saldo, na mesma situação |
| `producao/Program.cs` | A versão em console, de reserva. Serve se a janela não abrir na sua máquina |
| `producao-testes/` | **O seu lugar.** Dois moldes prontos e nove TODO |
| A folha de regras | O papel que você preencheu antes de abrir isto aqui |

---

## Ontem × hoje

| Ontem | Hoje |
|:--|:--|
| O código estava pronto | **O código não existe** |
| Ele compilava, rodava e mentia | **Ele compila, roda e para** |
| O roteiro vinha com os 18 nomes | **Os nomes saem da sua folha de regras** |
| Você provava que o código estava errado | **Você diz o que "certo" significa, antes de existir código** |

---

## A janela abre. Ela só não sabe fazer nada ainda.

Aperte F5 antes de qualquer outra coisa. A janela abre inteira: os campos, os nove
botões, os saldos das duas contas. Clique em `Somar`:

```
Somar AINDA NAO IMPLEMENTADO.
Escreva o teste, veja o vermelho, e so entao o corpo.
```

**Não é bug.** É o ponto de partida da noite: um contrato assinado e não cumprido.
As assinaturas estão todas lá — nome, parâmetros, tipo de retorno — e é só por isso
que a janela abre e que o projeto de teste compila. O que falta é a conta lá dentro.

### O painel da direita é o seu placar

Ele lê os nove métodos e diz quais já têm corpo. Agora está assim:

```
       [ ] Somar          [ ] Depositar
       [ ] Subtrair       [ ] Sacar
       [ ] Multiplicar    [ ] TemSaldo
       [ ] Dividir        [ ] Transferir
       [ ] Porcentagem
```

E o título da janela diz **0 de 9 implementados**. Cada método que você escrever
acende um `[X]` e faz esse número subir. **É por isso que a janela existe:** você vê
o efeito do seu código sem precisar ler a barra de testes.

---

## A ordem da noite, e ela não muda

```
       1. a REGRA   ->  esta na sua folha, escrita por voces
       2. o TESTE   ->  voce escreve, RODA, e ele fica VERMELHO
       3. o CODIGO  ->  so agora voce abre a Calculadora.cs
       4. RODA      ->  e ele fica VERDE
```

| # | O quê | Onde |
|:--:|:--|:--|
| 1 | Preencher a folha de regras, **sem a IDE aberta** | Papel |
| 2 | Abrir o `.sln` e rodar o teste que já vem pronto | Test Explorer |
| 3 | Ver o **vermelho de `NotImplementedException`**, e entender por que ele é diferente de um `Assert` que falhou | Test Explorer |
| 4 | Escrever o teste de um método, e vê-lo ficar vermelho | Visual Studio |
| 5 | **Só então** escrever o corpo daquele método, e vê-lo ficar verde | Visual Studio |
| 6 | Repetir 4 e 5 para cada método | Visual Studio |
| 7 | Voltar à folha e anotar quantas regras você **não** tinha visto | Papel |

> **O passo 3 é o que separa esta noite de todas as outras.** Um teste que nasce
> verde não provou nada: você nunca o viu falhar, e ele pode estar testando o nada.
> **Ver o vermelho é como você descobre que o teste está ligado no lugar certo.**

> **O passo 7 é a aula.** Ninguém escreve todas as regras de primeira — e descobrir
> quais faltaram vale mais que a barra verde.

---

## A regra desta noite

```
       Se o teste passa de primeira, ele nao esta testando nada.
```

Há **um** teste que nasce verde de propósito, o `Conta_RecemCriada_ComecaComSaldoZero`,
e ele está lá para você descobrir por quê. Todo o resto você ganha no braço.

---

## Um aviso sobre número com casa decimal

`0.1 + 0.2` não dá `0.3` no computador — dá `0.30000000000000004`. Por isso todo
`Assert.AreEqual` de `double` desta noite leva um terceiro valor, a `FOLGA`:

```
       Assert.AreEqual(esperado, obtido, FOLGA, "o caso")
```

Ela já vem declarada no topo dos dois arquivos de teste. **Não a apague**, e não
tente comparar `double` sem ela — o vermelho que aparece é falso, e você vai perder
meia hora procurando um defeito que não existe.

---

## Antes de tudo dar errado

Na **primeira** compilação, o projeto de teste baixa três pacotes do `nuget.org`.
Se a máquina estiver sem internet nessa hora, nada compila e nada roda. Depois disso
fica tudo no cache e a noite roda offline.

**Se der erro de pacote, avise o professor antes de tentar consertar sozinho.**

> **Apertou F5 e abriu uma janela preta de console?** O Visual Studio escolheu o
> projeto errado. Botão direito em **`ProducaoTela`** → **Definir como Projeto de
> Inicialização**, e F5 de novo.

> Se a janela não abrir de jeito nenhum na sua máquina, avise o professor: o
> `Producao` roda em console com exatamente as mesmas regras.
