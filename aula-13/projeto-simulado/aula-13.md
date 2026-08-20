# Aula 13 — quinta-feira, 20/08/2026

## Simulado da prova

| | |
|:--|:--|
| **Data** | 20/08/2026 · 18h00 às 22h00 · 4h |
| **Categoria (SIG)** | Laboratório de práticas |
| **Formato** | **Windows Forms**, mesmo aplicativo das Aulas 11 e 12 |
| **Material** | **Nesta pasta**: [`projeto-simulado/`](projeto-simulado/README.md) e o [gabarito](GABARITO-aula-13.md) (só seu). Mais o [Kahoot de fechamento](../../extras/kahoot/kahoot-aula-13-simulado.md) |
| **Conhecimentos** | **2** (estrutura de dados) · **7** (POO) |
| **Habilidades** | 1 · 3 · 7 |

> 🔴 **A prova é amanhã, 21/08:** 5 questões práticas e 5 orais. O material dela está em
> [`../../avaliacao/prova-21-08/`](../../avaliacao/prova-21-08/).

---

## A ideia da noite

**A revisão de conteúdo acabou.** Ontem a turma percorreu as dez abas, incluindo `while`,
trocar senha e remover. `List`, `foreach`, `while` e `null` deixaram de ser dívida.

O que falta não é saber mais — é **conseguir mostrar que sabe, com o relógio andando**. Por
isso a noite inteira é a prova de amanhã, ensaiada: dez métodos novos, no mesmo aplicativo,
em duas rodadas de cinco, com a correção no meio.

**Nenhum dos dez repete os dez de ontem.** Todos usam só o que já foi ensinado, mas cada um
estreia um padrão que os desafios de ontem não tinham — contar, guardar o melhor até agora,
percorrer de trás para frente, tratar o caso vazio antes de dividir.

### O que muda no aplicativo

| | |
|:--|:--|
| **O nome é o desafio 0** | Ele escreve o nome dentro do `Desafios.cs`, e não numa caixa a cada F5. Fica no código, vai junto no envio, e é um método de uma linha — o único sem pegadinha |
| **As abas nascem destravadas** | Empacar na 1 não pode esconder a 5. Numa prova isso seria injusto; num simulado, atrapalha |
| **Tem botão de enviar** | O aluno clica e as respostas vão para o servidor. **É o teste de carga da mecânica de amanhã** |
| **Não tem mini-tela** | O simulado é sobre os testes, não sobre brincar com dados |

---

## Roteiro em blocos

| Bloco | Hora | Min | O quê |
|:--|:--:|:--:|:--|
| **0** | 18:00 | 15 | **O formato da prova no quadro:** 5 práticas + 5 orais, como cada uma é corrigida |
| **1** | 18:15 | 70 | **Rodada 1** — desafios 1 a 5, cronometrada |
| — | 19:25 | 15 | Intervalo |
| **2** | 19:40 | 40 | **Correção no projetor** — um método de cada vez, explicado por um aluno |
| **3** | 20:20 | 45 | **Rodada 2** — desafios 6 a 10, um degrau acima |
| **4** | 21:05 | 25 | **O envio:** todos clicam em Enviar e conferem que chegou |
| **5** | 21:30 | 20 | **Kahoot, 10 perguntas** — as armadilhas que caem amanhã |
| **6** | 21:50 | 10 | O que revisar em casa · recados |

Soma: 15+70+15+40+45+25+20+10 = **240**. ✓

**Ordem do que cai:** Bloco 6 → Bloco 3 (encurta para 30) → Bloco 2 (encurta para 25) →
**nunca o Bloco 4 nem o Bloco 5**.

> **O Bloco 4 é o que eu não deixaria cair.** Descobrir problema de rede hoje custa vinte
> minutos; descobrir amanhã custa a prova.

### O que escrever no quadro às 18h

1. **A prova é amanhã.** 5 questões práticas no computador + 5 perguntas orais, individuais.
2. **As orais são sobre o código que VOCÊ escreveu.** Não é teoria decorada.
3. **Comece pelo desafio 0: o seu nome, dentro do código.** Sem ele o botão Enviar não
   funciona — e você escreve **uma vez só**, porque fica no arquivo.
4. **São dez desafios depois desse, e as abas estão todas abertas.** Empacou numa? Pule e volte.
5. **Feche o programa antes de rodar de novo.**

---

## Os dez do simulado

**Antes de tudo**

| # | Método | O que é |
|:--:|:--|:--|
| 0 | `MeuNome` | o nome dele, no código. Uma linha, sem pegadinha — e sem ele o Enviar não funciona |

**Rodada 1 — nível da prova**

| # | Método | O padrão que estreia |
|:--:|:--|:--|
| 1 | `ContarSenhasFracas` | o acumulador: contar sem parar no primeiro |
| 2 | `TelaDoAtalho` | `switch` com `default` |
| 3 | `MediaTamanhoSenha` | a guarda da lista vazia **antes** de dividir |
| 4 | `ListarLogins` | separador **entre** os itens, não depois de cada um |
| 5 | `NomeMaisLongo` | guardar "o melhor até agora", e devolver `null` |

**Rodada 2 — um degrau acima**

| # | Método | O que combina |
|:--:|:--|:--|
| 6 | `ResumoDaTurma` | contar **e** decidir a frase pelo resultado |
| 7 | `Inverter` | `for` de trás para frente, `Count - 1` até `0` |
| 8 | `LoginIgualAoAnterior` | percorrer com índice **e** comparar com o anterior |
| 9 | `PosicaoDoLogin` | achar **e** converter índice em posição |
| 10 | `Importar` | duas listas ao mesmo tempo |

### Os três momentos de projetor

| Desafio | O quê |
|:--:|:--|
| **1** | Trocar `total = total + 1` por `total = 1`. O total para em 1 e fica lá |
| **3** | Apagar a guarda da lista vazia. O programa **estoura** — e o corretor traduz o erro |
| **5** | Começar o campeão com `contas[0]`. Funciona em tudo e quebra só na lista vazia |

---

## Antes das 18h

| # | |
|:--:|:--|
| 1 | **Preencher o `Conexao.cs`** com o servidor, o banco, o usuário e a senha da VPS |
| 2 | **Rodar o `banco/criar-tabela.sql`** na VPS, e conferir que `aluno_uc12` **não** consegue dar `SELECT` |
| 3 | Compilar uma vez com as credenciais e distribuir |
| 4 | Rodar na máquina do laboratório, com o projetor ligado |
| 5 | Deixar o [gabarito](GABARITO-aula-13.md) aberto **só na sua máquina** |
| 6 | Abrir o [Kahoot](../../extras/kahoot/kahoot-aula-13-simulado.md) |

> ✅ **Conferido antes de entregar.** Os 60 testes do simulado e os 32 da prova passam contra
> o gabarito; 12 armadilhas clássicas foram escritas de propósito e o teste certo pegou as 12;
> as duas telas foram conferidas na imagem; e o envio foi testado gravando o arquivo local.
>
> ⚠️ **O que eu NÃO consegui testar:** a gravação no banco da VPS, porque não tenho as
> credenciais. O caminho do erro foi testado (servidor inacessível → mensagem legível e
> arquivo local), mas o caminho de sucesso só na sua mão.

---

## Avaliação da noite

| Conhecimento / Habilidade | Evidência |
|:--|:--|
| **C2** — estrutura de dados | Os dez métodos: percorrer, contar, localizar, montar e inverter uma `List` |
| **C7** — POO | Método com parâmetro e retorno, objeto, `null` como resposta |
| **H1** — resolver problemas lógicos | Escolher entre `for`, `foreach` e `while` pelo que o problema pede |
| **H3** — interpretar textos técnicos | Ler `esperado × obtido` e a tradução do erro em tempo de execução |
| **H7** — analisar as etapas | Explicar, na correção, por que o `return` fica onde fica |

---

## Depois da aula — o que ficou para a próxima

> Preencher hoje mesmo.

**Quantos chegaram ao fim da rodada 1, e quantos ao fim da rodada 2:**

_______________________________________________________________________

**O envio funcionou? Quantos chegaram no banco, e quantos caíram no arquivo local:**

_______________________________________________________________________

**Em qual dos dez a turma mais travou:**

_______________________________________________________________________

**A turma trabalhou sem você ao lado, hoje?**

_______________________________________________________________________

**Ocorrências da noite** (para o SIG):

_______________________________________________________________________
