# Aula 16 — terça-feira, 25/08/2026

## Método, parâmetro e retorno — o algoritmo vem em Portugol, o C# quem escreve é a turma

| | |
|:--|:--|
| **Data** | 25/08/2026 · 18h00 às 22h00 · 4h |
| **Categoria (SIG)** | Laboratório de práticas |
| **Formato** | **Windows Forms**, uma tela só, com **MySQL** no último terço |
| **Material** | **Nesta pasta**: [`folhas/`](folhas/) — três folhas A4 impressas — o [`projeto-mural/`](projeto-mural/README.md), com o `mural-inicial/` que a turma recebe e o `mural-final/` que é só seu, o [`projeto-desafio/`](projeto-desafio/README.md) — **o modo fácil** — e o [`projeto-desafios-codigo/`](projeto-desafios-codigo/README.md), os **10 desafios em arquivo** |
| **Conhecimentos** | **7** (POO) · **4** (conexão e manipulação do banco) · **2** (estrutura de dados) |
| **Habilidades** | 1 · 3 · 7 |

> ✅ **Esta é a Aula 16.** A numeração ficou fechada em 24/08: **Aula 14 = a
> prova, sexta 21/08** · **Aula 15 = segunda 24/08**, a noite do login e do
> banco · **Aula 16 = esta noite**.
>
> **Mudança de plano em 25/08:** esta noite estava planejada como a retomada num
> arquivo só, que mora em [`aula-14/`](../aula-14/aula-14.md). Aquele projeto
> continua pronto e conferido, e passou a ser **reserva sem data**.

---

## A ideia da noite

**A turma estudou algoritmo em Portugol e trava na sintaxe do C#, que é em
inglês.** Isso não é suposição: está escrito, com estas palavras, no
[LEIA-ME da apostila de classes e sintaxe](../../extras/apostila-classes-e-sintaxe/LEIA-ME.md).

Então o algoritmo chega **pronto, no papel, em VisuAlg** — a lógica deixa de ser
o obstáculo. O que sobra é o obstáculo real: escrever aquilo em C#.

**E o assunto da noite é método: criar, receber por parâmetro, devolver.** Sobre
cada passo o aluno responde três perguntas antes de escrever qualquer chave —
*o que este método recebe, o que ele faz, o que ele devolve* — e a assinatura
cai dessas três respostas.

O Portugol ajuda aqui como não ajuda em nenhum outro assunto: **o VisuAlg já
separa `procedimento` de `funcao`**. A turma já tem, na própria língua, a
distinção entre o método que faz e o método que responde. Falta descobrir que em
C# os dois se escrevem quase igual — o que muda é o que vem antes do nome.

### O que muda em relação a ontem

| | |
|:--|:--|
| **A assinatura é do aluno** | Até ontem ela vinha pronta e ele preenchia o corpo. Hoje ele escreve a linha inteira — e por isso a maioria dos métodos **nem existe** no projeto que ele recebe |
| **O papel entra** | Três folhas A4. A ficha preenchida à mão é a **evidência individual** que o registro da Aula 12 pediu |
| **O banco não é estreia** | Ontem a autenticação ficou de pé contra o MySQL. Hoje é o **mesmo molde outra vez**, numa tabela que não é a de usuário |
| **É o ensaio de sexta** | O mural de hoje é a Atividade 2, que vence **28/08**, com o relacionamento de fora |

---

## A ficha — o instrumento da noite

```
┌─ RECEBE ────────────┬─ FAZ ───────────────┬─ DEVOLVE ───────────┐
│ um recado           │ monta o texto de    │ um texto            │
│ (Recado)            │ uma linha do mural  │ (string)            │
└─────────────────────┴─────────────────────┴─────────────────────┘

  private  ________  Descrever ( ________________ )
           DEVOLVE                RECEBE
```

**A assinatura deixa de ser mistério e vira preenchimento.** O que a ficha diz
que o método DEVOLVE vira a palavra antes do nome; o que ela diz que ele RECEBE
vira o que fica dentro dos parênteses. Não devolve nada? `void` — que é o
`procedimento` do Portugol, com outro nome.

Ela está em branco nos oito passos da folha B, e **é recolhível às 22h**.

---

## Os 9 passos, e o que cada um estreia

**O número do passo é o número do `TODO`, e é a ordem da noite.** Papel, projeto
e roteiro andam juntos — de propósito, porque esta turma se perde quando não
andam.

| # | Método | Recebe | Devolve | O que estreia | Bloco |
|:--:|:--|:--|:--|:--|:--:|
| 0 | `Limpar()` | nada | nada | **vem pronto**, de molde | — |
| 1 | `class Recado` | — | — | classe pode ser só molde de dado: **sem método nenhum** | 2 |
| 2 | `Descrever(Recado r)` | 1 objeto | `string` | função com parâmetro — devolve e **não mexe na tela** | 2 |
| 3 | `CarregarMural()` | nada | nada | procedimento que **consome o retorno** de outros dois | 2 |
| 4 | `Gravar(Recado r)` | 1 objeto | `int` | `static`, e um retorno que o chamador **pode ignorar** | 3 ⭐ |
| 5 | `Listar()` | nada | `List<Recado>` | **devolve uma coleção** · o `for` invertido | 3 ⭐ |
| 6 | `btnPublicar_Click` | o evento | nada | a assinatura que **não é você quem escolhe** · `if` | 4 |
| 7 | `Saudacao(int)` | 1 número | `string` | o retorno **vira texto na tela** · `switch` | 4 |
| 8 | `Procurar(string)` | 1 texto | `int` | o **acumulador vira o retorno** | 5 |
| 9 | os dois do DAO | **iguais** | **iguais** | **só o corpo muda** | 6 ⭐ |

**O par que ensina sozinho são o 2 e o 3**, um do lado do outro: `Descrever`
recebe um recado e devolve um texto sem tocar na tela; `CarregarMural` não
recebe nada, não devolve nada, e é quem põe na tela o que os outros devolveram.
Mesmo assunto, naturezas opostas.

---

## Roteiro em blocos

| Bloco | Hora | Min | O quê |
|:--|:--:|:--:|:--|
| **0** | 18:00 | 15 | Copiar · abrir · F5 · **rodar o `CriarMural.sql`** · trocar o `pwd` · **entregar as folhas** |
| **1** | 18:15 | 25 | **A ficha do método** — a folha A no projetor. Duas fichas preenchidas juntos, **no papel, antes do teclado** |
| **2** | 18:40 | 30 | **Passos 1, 2 e 3** — a classe, `Descrever` e `CarregarMural` |
| **3** | 19:10 | 30 | **Passos 4 e 5** — o `MuralDAO` em memória. ⭐ **não pode cair** |
| — | 19:40 | 15 | **Intervalo** |
| **4** | 19:55 | 25 | **Passos 6 e 7** — e o **primeiro F5 que muda a tela** |
| **5** | 20:20 | 25 | **Passo 8** — `Procurar`, com acumulador |
| **6** | 20:45 | 45 | **O banco** — a folha C: `INSERT` e `SELECT ... ORDER BY`. ⭐ **não pode cair** |
| **7** | 21:30 | 20 | Publicar, **fechar o programa, abrir e o recado continuar lá** |
| **8** | 21:50 | 10 | Recados · a folha A vai para casa, e serve na Atividade 2 |

Soma: 15+25+30+30+15+25+25+45+20+10 = **240**. ✓

**Ordem do que cai:** Bloco 5 → Bloco 7 (vira tarefa) → Bloco 1 (encurta para
15) → **nunca os Blocos 3 e 6**.

> **O DAO ficou ANTES do intervalo, de propósito.** Ele é o inegociável da
> primeira metade, e o que vem depois das 19h55 é sempre o que mais sofre com a
> turma voltando devagar.

### O que escrever no quadro às 18h

1. Hoje o assunto é **método**: o que ele recebe, o que faz, o que devolve.
2. **A folha B vem em Portugol. O C# quem escreve é você — a assinatura também.**
3. **Preencha as três caixas antes de escrever a assinatura.** Sempre.
4. **F5 depois de cada passo.** Até o passo 6 a tela não muda: é assim mesmo.
5. O mural de hoje é o **ensaio da entrega de sexta**.

---

## Os dois caminhos pela mesma noite

**Nem todo mundo vai pelo papel.** O material tem dois caminhos, e eles levam
ao mesmo lugar — o método com assinatura, parâmetro e retorno.

| | O caminho de sempre | O modo fácil |
|:--|:--|:--|
| **Onde** | Folha B + `mural-inicial` no Visual Studio | [`projeto-desafio/`](projeto-desafio/README.md), um app só |
| **A ficha** | Vem **em branco**: o aluno preenche | Vem **preenchida**, à vista |
| **A assinatura** | Ele escreve a linha inteira, à mão | Ele **escolhe** o retorno e o parâmetro |
| **O corpo** | Escreve inteiro | Já está lá, com lacunas nos pontos que decidem |
| **Para quem** | A turma | Quem trava na digitação, quem chegou atrasado, quem desistiu |

**O app não substitui o projeto — ele destrava.** Ao fechar um passo, a própria
tela diz onde escrever aquilo de verdade: *"o TODO 4 está em MuralDAO.cs"*.

### As duas rodadas do app

| Rodada | O que o aluno faz | Quantos |
|:--|:--|:--:|
| **1 · Os 9 passos** | Escolhe a peça que falta no C# — e **o código se preenche na frente dele** | **47 lacunas** |
| **2 · As 10 perguntas** | Escolhe entre quatro, olhando o Portugol | **10** |

> **O que não ficou mais fácil é o que a noite cobra.** Nas duas rodadas, o que
> se pergunta é sempre *o que o método devolve*, *o que ele recebe*, e a palavra
> que decide isso — `void`, `return`, `static`, `switch`, `foreach`.

**E o app não compila nada.** Está escrito no rodapé da janela dele, e vale
dizer em voz alta: fechar as duas rodadas ali **não é ter escrito o Mural**.

### E o terceiro degrau: escrever de verdade

Quem quiser **escrever** código, e não escolher, tem o
[`projeto-desafios-codigo/`](projeto-desafios-codigo/README.md): **10 métodos
para escrever no `Desafios.cs`**, no mesmo formato das Aulas 11, 12 e 13 — o
aluno escreve, roda com F5, e o corretor **executa o que ele escreveu** e
mostra esperado × obtido, teste a teste.

**A diferença para as outras noites é a coluna da esquerda:** ali fica o
algoritmo em Portugol, dizendo o que o método tem de fazer. Não há enunciado em
português corrido — há o algoritmo, na língua em que eles aprenderam a pensar.

| | |
|:--|:--|
| **São 33 testes**, e cada um pega um erro real | O Bruno depois do Ana pega o nome fixo; o `Procurar` do Bruno pega o `return null` no lugar errado |
| **O gabarito fecha 33 de 33** | Prova que os valores esperados estão certos |
| **Com os `TODO` vazios, 0 de 10 desafios fecham** | Prova que nenhum passa de graça |

---

## ⚠️ O risco da noite, e o que fazer a respeito

**Entre os passos 1 e 5 a tela não muda.** São **80 minutos**, das 18h40 às
20h20, sem retorno visível — porque um app escrito do zero só mostra alguma
coisa quando as seis primeiras peças existem. Não dá para encurtar isso sem
entregar metade pronta, e entregar metade pronta é justamente o que esta noite
não faz.

Com esta turma — quatro noites de dispersão registradas — isso é **o risco
principal**. Três compensações, e as três importam:

| | |
|:--:|:--|
| **1** | **O compilador é o retorno nesse trecho.** F5 tem de continuar compilando a cada passo. Erro de compilação é a resposta que a tela ainda não dá |
| **2** | **Deixe o `mural-final` rodando no projetor**, com dois ou três recados publicados, a noite inteira. A turma tem de ver aonde vai chegar |
| **3** | **Confira a ficha de bancada**, aluno a aluno, antes de ele digitar. É o que substitui o selo verde das noites anteriores |

---

## Os três momentos de projetor

| | Quando | O quê |
|:--:|:--|:--|
| **1** | Bloco 0 | O `CriarMural.sql` no Workbench: o raio amarelo do *Execute All* e as três linhas do `SELECT` do fim |
| **2** | Bloco 1 | A assinatura desmontada em pedaços, com a ficha ao lado. **Duas fichas preenchidas com a turma falando** |
| **3** | Bloco 6 | **Apagar o laço invertido e escrever o `ORDER BY` no lugar**, com o arquivo do Bloco 3 aberto ao lado |

### Os dois erros que valem ouro

| Onde | O erro | O que acontece |
|:--|:--|:--|
| **Passo 2** | Usar `txtAutor.Text` dentro do `Descrever`, em vez do parâmetro | Funciona na primeira linha e **repete o mesmo autor em todas as outras**. É o parâmetro se explicando sozinho |
| **Passo 7** | Escrever `Saudacao(3);` numa linha solta | Compila, roda, **não aparece nada**. Método que devolve e ninguém pega o retorno é trabalho jogado fora |

O primeiro é irmão da armadilha do `a.Nome = "Ana"` que já está no projeto de
retomada — mesma lição, domínio novo.

### O momento da noite

No Bloco 6, o `para i de tamanho(recados) - 1 ate 0 passo -1` escrito às 19h30
**vira `ORDER BY Id DESC`**. Quem ordena agora é o MySQL — como o `foreach` de
ontem virou `WHERE`.

**E a tela não muda uma linha**, porque a ordenação sempre morou dentro do
`Listar()`. É aí que se fecha o assunto:

> **O corpo mudou. A assinatura, não — e é por isso que nada mais no programa
> precisou saber.**

---

## Antes das 18h

| # | |
|:--:|:--|
| 1 | **Imprimir as três folhas** — 6 páginas, frente e verso, **13 cópias** |
| 2 | **Copiar o `mural-inicial/` para as máquinas.** Sem GitHub, é pendrive — 10 a 20 minutos reais |
| 2b | **Levar o `projeto-desafio/` e o `projeto-desafios-codigo/` juntos** — o modo fácil e os desafios em arquivo. Você vai precisar dos dois |
| 3 | **Levar a pasta `banco/` junto**, ou o passo 9 não acontece |
| 4 | **Não copiar `bin/`, `obj/` nem `.vs/`** |
| 5 | **Rodar o `CriarMural.sql` numa máquina do laboratório** e conferir as três linhas |
| 6 | **Abrir o `mural-final` no projetor** e publicar um recado, para conferir a tela |
| 7 | **Escrever a senha do `root` no quadro** antes do Bloco 0 |

> ✅ **Verificado nesta máquina, de verdade.** Os **três** projetos compilam com
> 0 erro e 0 aviso — o `mural-inicial` com todos os 9 TODO vazios. **Os três
> executáveis abrem**, testados rodando, e os dois do Mural abrem **mesmo sem
> MySQL**, porque o carregamento inicial está dentro de um `try`. As três
> folhas saem do gerador com o validador de layout passando: nada cortado, nada
> largo demais, nada sobreposto. E o [`conferir-material.py`](conferir-material.py)
> passa nas sete checagens, entre elas as mais fortes: **cada linha das
> lacunas, preenchida, existe no gabarito**; **o gabarito dos desafios fecha
> 33 de 33 testes**; e **com os `TODO` vazios nenhum desafio fecha** — se
> fechasse, seria teste passando de graça.
>
> ⚠️ **O que NÃO foi testado:** o `CriarMural.sql` contra um MySQL de verdade
> — não há servidor nem cliente MySQL nesta máquina — e a **aparência da tela**,
> porque o `frmMural.Designer.cs` foi escrito à mão e não há como renderizar
> aqui. **Os itens 5 e 6 acima não são opcionais.**

---

## Avaliação da noite

| Conhecimento / Habilidade | Evidência |
|:--|:--|
| **C7** — POO | As nove assinaturas escritas do zero: tipo de retorno, parâmetro, `void` × `return`, `static`, e a classe `Recado` |
| **C4** — banco e eventos | O `INSERT` e o `SELECT` com `@parametro`, e o `btnPublicar_Click` como método chamado pelo Windows |
| **C2** — estrutura de dados | A `List<Recado>` devolvida pelo `Listar`, o `foreach` e — **enfim** — a **ordenação**: primeiro no `for` invertido, depois no `ORDER BY` |
| **H1** — problemas lógicos | Decidir se um passo é `procedimento` ou `funcao` pelo que ele precisa responder |
| **H3** — textos técnicos | Ler o `CS0161` (nem todos os caminhos devolvem valor) e a mensagem traduzida do MySQL |
| **H7** — etapas do processo | A ficha RECEBE / FAZ / DEVOLVE preenchida antes de cada passo |

> **A ordenação era o buraco do Conhecimento 2.** O
> [registro](../../registro-de-aula/registros-de-aula.md) diz que o plano de
> curso pede listas, ordenação e pesquisa, e que as noites anteriores entregaram
> listas e pesquisa. **Esta noite é o primeiro lastro real da ordenação** — e
> ela aparece duas vezes, na mão e no banco.

> **Todo comando SQL com `@parametro`, nunca texto grudado com `+`.** É o
> critério que a ficha de avaliação usa para o indicador de banco.

---

## Depois da aula — o que ficou para a próxima

> **Preencha esta seção na mesma noite**, ainda com a memória fresca. É daqui
> que sai o planejamento da aula seguinte.

**Até onde a turma chegou:**

| Bloco | Fechou? | Observação |
|:--|:--:|:--|
| 1 — a ficha do método | ☐ | |
| 2 — passos 1, 2 e 3 | ☐ | |
| 3 — o `MuralDAO` | ☐ | **o inegociável da primeira metade** |
| 4 — passos 6 e 7 | ☐ | o primeiro F5 que muda a tela |
| 5 — o `Procurar` | ☐ | o primeiro a cair |
| 6 — o banco | ☐ | **a segunda vez do molde** |
| 7 — fechar e abrir | ☐ | |

**Quantos escreveram uma assinatura sozinhos, sem perguntar:**

_______________________________________________________________________

**A ficha RECEBE / FAZ / DEVOLVE funcionou? Quantas foram entregues:**

_______________________________________________________________________

**Os 80 minutos sem retorno na tela custaram caro? O que aconteceu:**

_______________________________________________________________________

**Em qual passo a turma mais travou:**

_______________________________________________________________________

**Quantos saíram com o recado sobrevivendo ao fechar e abrir:**

_______________________________________________________________________

**A turma trabalhou sem você ao lado, hoje?**

_______________________________________________________________________

**Conteúdo que NÃO foi passado e precisa entrar na próxima:**

_______________________________________________________________________

**Ocorrências da noite** (para o campo Observações do app do Senac):

_______________________________________________________________________

---

## O que vem depois

**A Atividade 2 vence 28/08** e pede o CRUD completo do mural, com `INNER JOIN`,
`LIKE`, regra de dono e `finally` em todos os métodos. Quem sair hoje com o
`MuralDAO` no MySQL tem o molde na mão: o `PostagemDAO` é este mesmo, com três
métodos a mais e o `UsuarioId` no lugar do `Autor` em texto.

**A folha A vai para casa** e continua servindo — ela é a única peça da noite
que não é sobre o mural.

Duas pendências que continuam abertas no registro:

- **Política de recuperação de dados** (C6), que segue marcada só na Aula 1, sem
  lastro nenhum.
- O projeto da **retomada num arquivo só** ([`aula-14/`](../aula-14/aula-14.md)),
  que está pronto e virou reserva. Ele é a rede para a noite em que a turma
  precisar voltar ao começo.
