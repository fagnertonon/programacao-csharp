# 20 desafios de acesso — UC11, Aulas 8 e 9

**Dê dois cliques em `acesso-inicial/Acesso.sln`.** É o único arquivo que você abre.

| O que tem aqui | |
|:--|:--|
| `acesso-inicial/Acesso.sln` | os três projetos, já ligados um no outro |
| `acesso-inicial/regras/Login.cs` | desafios **1 a 10** — dez métodos, nenhum com corpo |
| `acesso-inicial/regras/Cadastro.cs` | desafios **11 a 20** — mais dez, idem |
| `acesso-inicial/regras-testes/AcessoTests.cs` | **onde você escreve os testes** |
| `acesso-inicial/desafios/` | a janela: uma aba por desafio |
| `comum/` | o motor que confere o seu trabalho — **não precisa mexer** |

Apertou **F5** e a janela abre. O título dela diz **0 de 20**. No fim das duas
noites vai dizer **20 de 20**.

---

## A diferença desta noite

Nas aulas passadas o teste vinha pronto e você escrevia o `Assert`, ou o método vinha sem
corpo e você escrevia o teste. Agora são **as duas coisas**:

> **Cada aba só fecha quando o MÉTODO está certo E o TESTE está escrito.**

Não adianta fazer o método funcionar e pular o teste. Não adianta escrever um teste bonito
e deixar o método vazio. A aba pergunta quatro coisas, e quer **sim** nas quatro:

| | A pergunta | O que ela quer |
|:--:|:--|:--|
| **1** | O método tem corpo? | você apagou o `throw new NotImplementedException` |
| **2** | O método está certo? | ele passa nos casos que a aba mostra |
| **3** | O teste existe e passa? | existe um método de teste **com o nome que a aba pede** |
| **4** | O teste cobre as fronteiras? | os seus `[DataRow]` incluem os valores que a aba exige |

### A pergunta 4 é a que pega

Um teste que só experimenta o caso fácil não prova nada. Se a regra é "no mínimo 8
caracteres", testar com 3 e com 20 não descobre se o programador escreveu `> 8` no lugar
de `>= 8`. **Quem descobre é o 8.**

Por isso a aba lista, do lado esquerdo, exatamente quais valores precisam estar na sua
tabela — e enquanto faltar um, ela não fecha, mesmo com o método perfeito.

---

## A ordem da noite, e ela não muda

1. **Leia o enunciado** na aba do desafio, e a lista de fronteiras do lado esquerdo
2. **Escreva o teste** no `AcessoTests.cs`, com o nome que a aba pede
3. **Escreva o corpo** do método no `Login.cs` ou no `Cadastro.cs`
4. **F5** e confira as quatro perguntas

> **Por que o teste primeiro?** Porque escrever o teste é o que obriga você a decidir o que
> a regra faz **antes** de sair digitando. É a ordem da aula passada, e ela continua valendo.

O botão **"Conferir de novo"** relê o resultado, mas ele **não recompila**.

> ⚠️ **Mexeu no código: FECHE a janela dos desafios primeiro** — no X, no canto — e só
> então aperte **F5**. Com a janela ainda aberta o F5 não faz nada: o Visual Studio já
> está rodando, e você fica consertando um código que ele nem leu. **Não há aviso
> nenhum** — a tela simplesmente não muda, e é o jeito mais fácil de perder dez minutos
> hoje.

---

## O molde

O desafio 1 já vem com o teste escrito. Ele é o modelo dos outros dezenove — copie o
formato:

```csharp
[DataTestMethod]
[DataRow("ana", false, "3 caracteres, um a menos")]
[DataRow("joao", true, "LIMITE: exatamente 4")]
[DataRow("abcdefghijabcdefghij", true, "LIMITE: exatamente 20")]
[DataRow("abcdefghijabcdefghijk", false, "21, um a mais")]
[DataRow("", false, "vazio")]
public void UsuarioValido_PorTabela(string usuario, bool esperado, string caso)
{
    Assert.AreEqual(esperado, Login.UsuarioValido(usuario), caso);
}
```

Quatro coisas para reparar:

- **`[DataTestMethod]`**, e não `[TestMethod]` — este recebe uma tabela
- **um `[DataRow]` por caso**, na ordem dos parâmetros
- **o último valor é o recado**: aparece na barra quando o caso fica vermelho.
  Chame o parâmetro de `caso`, como no molde — é assim que o corretor sabe que aquele
  valor é texto seu, e não uma entrada do teste
- **uma linha de `Assert`** só, que serve para todos os casos

O desafio 1 está com a pergunta 4 verde porque os quatro comprimentos que ele exige — 3, 4,
20 e 21 — estão todos ali. Falta só escrever o corpo do método.

---

## As abas destravam em cadeia

A aba 2 só abre depois que a 1 fecha, e assim por diante. Não é implicância: **os métodos
se chamam uns aos outros**.

> **São dois blocos, e cada um abre sozinho.** O desafio **1** abre o login e o **11**
> abre o cadastro — as duas abas nascem destravadas. Quem faltar na noite do login começa
> a do cadastro direto na aba 11, sem precisar fechar as dez de trás. O `ForcaDaSenha` do desafio 5 pergunta para os desafios 2, 3 e
4; o `PodeCadastrar` do 19 pergunta para quase todos os anteriores.

Consequência prática, e ela vai acontecer com você:

> **Um erro lá atrás pinta de vermelho um desafio da frente.** Quando o 19 ficar
> vermelho, desconfie do 12, não do 19.

---

## Quando travar

| O que aparece | O que fazer |
|:--|:--|
| *"O método ainda não tem corpo"* | apague o `throw new NotImplementedException` e escreva a regra |
| *"Não achei o método de teste X"* | o nome do seu teste está diferente do que a aba pede — copie o nome exato |
| *"O teste existe, mas não tem nenhum `[DataRow]`"* | faltou a tabela de casos |
| *"falta o `[DataTestMethod]` em cima"* | você escreveu `[TestMethod]`. Com ele as linhas da tabela não rodam |
| *"mas ele está STATIC"* | apague a palavra `static` da assinatura do teste |
| *"pede N valores, e este `[DataRow]` tem M"* | conte os parâmetros do método e os valores da linha — têm de bater |
| *"Falta no seu teste: uma entrada de N caracteres"* | acrescente um `[DataRow]` com esse valor |
| *"O seu teste ficou VERMELHO"* | ou o `esperado` que você escreveu está errado, ou o método está |
| *"O seu teste passou de 2 segundos"* | laço infinito — confira a condição de parada |
| a janela abriu **exatamente igual**, como se você não tivesse mexido em nada | **não compilou.** O Visual Studio perguntou *"...run the last successful build?"* e você respondeu **Sim** — ele abriu a janela ANTIGA. Responda **Não**, abra a **Lista de Erros** e conserte. Um `;` ou um `}` faltando no seu `AcessoTests.cs` derruba a janela junto, porque é ela que lê o seu teste |
| a janela abre preta, tipo console | o Visual Studio escolheu o projeto errado: botão direito em **`Desafios`** → **Definir como Projeto de Inicialização** |

E a última: **um teste que nasce verde não provou nada.** Se você escreveu o teste depois
do método e ele passou de primeira, rode-o uma vez contra o método errado de propósito, só
para ver o vermelho. É a única forma de saber que o teste está mesmo olhando.
