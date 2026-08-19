# Conecta em memoria — Aula 12

O aplicativo tem **dez abas**, e abre em tela cheia. Cada uma ensina um assunto, propoe um desafio, e so
**destrava a proxima quando o seu codigo passar em todos os testes**.

Cada desafio e um pedaco de verdade do Conecta — o mesmo sistema da apostila.

## Como trabalhar

1. Abra o **`desafios/ConectaA.csproj`** com um duplo clique. Ele abre no Visual Studio.
2. Rode com **F5**.
3. Leia a aba, escreva o metodo em **`desafios/Desafios.cs`**, **salve** (Ctrl+S).
4. **Feche o programa** e rode de novo com **F5**.
5. Acertou? A aba fica verde e a proxima destrava.

> **O passo 4 nao e opcional.** O seu codigo e compilado quando voce roda. Com o programa
> aberto, editar o arquivo nao muda nada na tela — e o botao "Conferir de novo" tambem nao.
> Se o Visual Studio perguntar *"deseja executar a ultima compilacao bem-sucedida?"*,
> responda **Nao**: quer dizer que ha um erro para consertar.

## As duas coisas que o aplicativo faz com o seu codigo

| | |
|:--|:--|
| **Testa** | compara o que o seu metodo devolveu com o esperado, teste a teste |
| **Roda** | na mini-tela de cada aba, com os dados que **voce** digitar na hora |

A mini-tela so funciona depois que os testes passam. Ela usa uma lista **em memoria**, a
mesma nas dez abas: a conta que voce criar na aba 1 e a conta com que voce entra na aba 3.

> Fechou o programa, a lista esvazia. **Isso nao e defeito** — e a razao de existir banco
> de dados, e e exatamente o que voce vai resolver na Parte 11 da apostila.

## Se travar

- **Botao "Ver a dica"** em todo desafio.
- O quadro vermelho embaixo mostra **o que era esperado e o que o seu codigo devolveu**.
- O contador "3 de 5 testes passando" so vale quando chega a 5: um metodo vazio devolve o
  valor padrao do tipo e ja acerta algum teste por acidente.
- **Os desafios 8 e 10 sao os mais dificeis do dia.** Deixe para depois se estiver
  travado nos outros.
- Se o seu codigo entrar num laco que nao termina, o aplicativo espera 2 segundos,
  desiste e avisa - ele **nao congela**, e os outros desafios continuam funcionando.

## O que voce NAO precisa abrir

`comum/`, `Corretor.cs`, `Oficina.cs`, `frmConecta.cs` — sao o motor do aplicativo.
Voce trabalha em **um arquivo so**: `desafios/Desafios.cs`.
