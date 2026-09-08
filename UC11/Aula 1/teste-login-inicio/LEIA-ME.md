# Criar conta — o projeto da Aula 1

**UC11 · terça-feira, 08/09/2026 · 18h–22h**

## Comece por aqui: dê dois cliques no `Aula01Login.sln`

**É o único arquivo que você abre hoje.** Ele traz os três projetos da noite
juntos, já ligados um no outro — você não precisa criar projeto nenhum, nem
configurar referência nenhuma. Abriu, está pronto.

```
       Aula01Login.sln          <-- abra ESTE
          |
          +-- LoginTela         a janela que voce vai olhar
          +-- Login             as regras que voce vai testar
          +-- LoginTestes       os testes que voce vai escrever
```

| O quê | Para quê |
|:--|:--|
| `login-tela/` | **A TELA.** Windows Forms, do mesmo tipo que você fez a UC12 inteira. Usuário, senha, repetir senha |
| `login-defeituoso/` | **As regras que você vai testar** — o `Cadastro.cs`. A tela usa este mesmo arquivo |
| `login-testes/` | **O roteiro de testes.** 18 casos, um pronto e 17 para você escrever |
| A apostila | **A referência da UC inteira.** As primeiras partes explicam o que é teste unitário, para que serve, quando usar e como se escreve. Guarde: você volta a elas nas outras noites |

> **Apertou F5 e abriu uma janela preta de console?** O Visual Studio escolheu o
> projeto errado para iniciar. Clique com o botão direito em **`LoginTela`** →
> **Definir como Projeto de Inicialização**, e aperte F5 de novo.

---

## O programa compila. O programa roda. O programa mente.

Não procure erro de compilação: **não tem.** Abriu o `.sln`, apertou F5, a tela
de criar conta aparece normalmente.

O que está errado é outra coisa: em algum lugar deste código, **a regra escrita
no comentário e a linha escrita embaixo dela não dizem a mesma coisa.** Um
exemplo do formato:

```
    A REGRA diz:   a garantia vale ate o 90o dia, inclusive
    O CODIGO diz:  if (dias < 90) return true;
    A DIFERENCA:   NaGarantia(90) devolve falso
```

> Este exemplo é de **outro sistema**, de propósito — na tela desta noite, quem
> acha é você.

Não vamos dizer quantos são nem onde estão. **É isso que a noite descobre.**

---

## O que você faz hoje, em ordem

| # | O quê | Onde |
|:--:|:--|:--|
| 1 | Entender **o que é teste unitário**: para que serve, quando usar, quando não usar, e como se escreve um | Apostila |
| 2 | **Abrir o `Aula01Login.sln`** e apertar F5. Anotar o que estiver errado na tela, sem abrir o código ainda | A janela, e papel |
| 3 | Ler o `Cadastro.cs` **contra os comentários de REGRA**, e anotar o que achar suspeito | Papel |
| 4 | Rodar o teste que já vem pronto e ver a primeira **barra verde** | Test Explorer |
| 5 | Escrever os `Assert` que faltam e rodar os 18 | Test Explorer |
| 6 | Comparar: o que você **achou olhando** × o que a **barra provou** | Papel |
| 7 | **Corrigir** dois ou três, junto com a turma, e ver o vermelho virar verde | Visual Studio |

> **Você não monta o projeto de teste hoje** — ele já vem montado no `.sln`. O
> professor monta um do zero no projetor, para você ver de onde aquilo veio, e
> você faz isso com as próprias mãos na Aula 2.

**O passo 6 é a aula.** Quase ninguém acerta a lista inteira olhando — e
descobrir *quais* você deixou passar, e *quem* você acusou errado, vale mais que
o placar.

> **Se o passo 5 não couber na noite**, ele volta nos primeiros minutos de
> quarta-feira. O projeto de amanhã é outro e não depende deste — você não fica
> devendo nada.

---

## A regra desta noite

```
       So o Cadastro.cs esta sob teste.
```

`Program.cs`, `Tela.cs` e `Entrada.cs` **não são testados hoje**, e não é por
falta de tempo: eles escrevem na tela e leem do teclado, e por isso um teste não
consegue conferir sozinho o que eles fazem. O `Cadastro.cs` não faz nem uma
coisa nem outra — **é essa regra que torna o teste possível.**

Repare também que o `Cadastro` **não guarda nada**: ele não sabe o que você
digitou. Quem guarda são as caixas de texto da tela, e a tela passa os valores
por parâmetro. Uma classe que não guarda estado devolve sempre a mesma resposta
para a mesma pergunta — e teste unitário precisa exatamente disso.

---

## A tela, para você se achar

A janela tem três partes, de cima para baixo:

| Parte | O que tem |
|:--|:--|
| **Seus dados** | usuário, senha, repetir a senha, e os botões *Criar conta* e *Limpar* |
| **O que cada regra respondeu** | sete linhas de SIM/NÃO, e a mensagem colorida embaixo |
| **O que o sistema já tem** | as contas que já existem e as senhas proibidas |

**As duas listas estão impressas no rodapé da janela de propósito.** Você não
precisa adivinhar valor nenhum — precisa TESTAR os que estão ali.

> **As sete respostas aparecem sempre**, mesmo quando a conta é recusada. É por
> elas que você descobre QUAL regra respondeu errado, e não só que algo deu
> errado.

> Se a janela não abrir na sua máquina, avise o professor: existe uma versão em
> console na mesma pasta, com exatamente as mesmas regras.

---

## Antes de tudo dar errado

**O projeto de teste precisa baixar três pacotes do NuGet na primeira
compilação.** Se a máquina não tiver internet naquele momento, o Visual Studio
reclama de pacote não encontrado e nada roda. Avise o professor: isso se resolve
em minutos, e é a única coisa da noite que depende de rede.
