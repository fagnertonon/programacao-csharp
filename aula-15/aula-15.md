# Aula 15 — segunda-feira, 24/08/2026

## A tela de login, preenchida pela turma — e no fim ela fala com o banco

| | |
|:--|:--|
| **Data** | 24/08/2026 · 18h00 às 22h00 · 4h |
| **Categoria (SIG)** | Laboratório de práticas |
| **Formato** | **Windows Forms** com Designer, e **MySQL** no último terço |
| **Material** | **Nesta pasta**: [`projeto-login/`](projeto-login/README.md) — o `login-inicial/` que a turma recebe, o `login-final/` que é só seu, e o [`banco/CriarBanco.sql`](projeto-login/banco/CriarBanco.sql) |
| **Conhecimentos** | **2** (estrutura de dados) · **4** (conexão e manipulação do banco) · **7** (POO) |
| **Habilidades** | 1 · 3 · 7 |

> ✅ **A numeração está fechada:** **Aula 14 = a prova, sexta 21/08** · **Aula 15
> = esta noite, segunda 24/08** · **Aula 16 = terça 25/08**, a retomada num
> arquivo só, que mora na pasta [`aula-14/`](../aula-14/aula-14.md) por causa do
> nome do projeto lá dentro. **Vale o número, não o nome da pasta.**

---

## A ideia da noite

**A turma recebe a tela de login montada e vazia por dentro.** Vinte e uma
lacunas numeradas, em ordem, que eles escrevem junto com você — é o mesmo pacto
da calculadora e do jogo da forca, agora na tela que eles vão entregar.

O que vem pronto é o que não é conteúdo: o Designer das três telas, a classe
`Usuario`, o `Conexao.cs` inteiro e o script do banco. O que vem vazio é toda a
lógica. **Cada construção entra porque o passo seguinte não sai sem ela** —
método porque o `MessageBox` de quatro argumentos se repete, classe porque a
conta criada numa tela precisa ser vista por outra, `foreach` porque procurar
numa lista é olhar cada item, `switch` porque três respostas para o mesmo número
não são três `if`.

**A novidade é o banco**, e ela entra pelo motivo que o aluno já sentiu na pele:
ele cria uma conta, aperta F5, e a conta sumiu. Não é *"agora vamos ver banco de
dados"* — é *"o programa está esquecendo, e nós vamos consertar"*. O conserto é
curto: trocar o corpo de dois métodos. **Nenhuma tela muda um pixel**, porque nos
TODOs 3 e 4 a tela deixou de saber de onde vêm os dados.

> **O argumento para dizer em voz alta às 18h:** os quatro métodos do
> `UsuarioDAO` são **exatamente** os da Atividade 1 — mesmos nomes, mesmos
> parâmetros, mesmos retornos. Hoje eles nascem com `List` (a **Parte 7**) e às
> 20h45 só o corpo troca (a **Parte 11**). **O arquivo que sair daqui às 22h é o
> arquivo que ele vai entregar.** A noite conta nota duas vezes.

O critério honesto do fim é um só: **criar uma conta, fechar o programa, abrir de
novo e entrar com ela.**

### O que muda em relação à Aula 12

| | |
|:--|:--|
| **É a tela deles** | Não é app de exercício com mini-tela. É o login do Conecta |
| **Vazio, não resolvido** | O projeto abre e nenhum botão faz nada. É o combinado |
| **Um arquivo por assunto** | Voltam o Designer e os vários `.cs` |
| **A `List` é degrau** | Nasce no Bloco 2 e é substituída no Bloco 6, na mesma noite |
| **O banco entra** | Primeira noite de MySQL desde a demonstração da Aula 6 |

---

## As 21 lacunas, e onde cada construção mora

| Arquivo | TODOs | Construções |
|:--|:--:|:--|
| `frmLogin.cs` | **10** | método · `if` · **`switch`** · `foreach` · **`for`** |
| `UsuarioDAO.cs` | **5** | classe · `static` · `foreach` · depois, o SQL |
| `frmCadastro.cs` | **5** | o extra: validação, e a extração do `Avisar` |
| `frmPrincipal.cs` | **1** | a `Sessao` atravessando telas |
| `Sessao.cs` | — | **não existe** — o aluno cria do zero no Bloco 5 |

| Construção | Onde | Por que **ali** |
|:--|:--|:--|
| **`if`** | `btnEntrar_Click` | Cada pergunta é sobre uma **coisa diferente**, resposta sim/não |
| **`switch`** | `MostrarErroDeTentativa` | Um valor só — `tentativas` — contra constantes. Fica **dez linhas abaixo do `if`**, no mesmo fluxo, e trava o botão Entrar na terceira |
| **`foreach`** | `ForcaDaSenha` e os três métodos de busca do DAO | Percorre o que **existe**; quem manda nas voltas é a coleção |
| **`for`** | `BarraDeForca` | **Não há coleção.** Há um número calculado de `#` |
| **classe** | `Sessao.cs`, do zero | `static` pelo motivo **oposto** ao do DAO: lá, a lista tem de ser uma só; aqui, o dado precisa **atravessar telas** |

O roteiro operacional — o que aparece na tela a cada TODO, os erros que valem a
pena deixar acontecer e o que fazer se o tempo apertar — está no
[README do projeto](projeto-login/README.md).

---

## Roteiro em blocos

| Bloco | Hora | Min | O quê |
|:--|:--:|:--:|:--|
| **0** | 18:00 | 15 | Copiar · abrir · F5 · **rodar o `CriarBanco.sql`** · trocar o `pwd` |
| **1** | 18:15 | 20 | **Método** — TODOs 1 e 2 |
| **2** | 18:35 | 35 | **Classe e `foreach`** — o `UsuarioDAO`. ⭐ **não pode cair** |
| **3** | 19:10 | 30 | **`if` e `switch`** — TODOs 3 a 6 |
| — | 19:40 | 15 | **Intervalo** |
| **4** | 19:55 | 25 | **`foreach` e `for`** — TODOs 8 a 10 |
| **5** | 20:20 | 25 | **Classe** — criar `Sessao.cs` do zero |
| **6** | 20:45 | 45 | **O banco entra** — `Autenticar` e `LoginExiste` viram `SELECT` |
| **7** | 21:30 | 20 | **`INSERT`** — criar conta, fechar, abrir e entrar |
| **8** | 21:50 | 10 | Recados · o `frmCadastro` para quem terminou |

Soma: 15+20+35+30+15+25+25+45+20+10 = **240**. ✓

**Ordem do que cai:** Bloco 4 → Bloco 5 → Bloco 7 (vira tarefa de casa) →
**nunca os Blocos 2 e 6**.

> **O Bloco 4 cai primeiro** — é o único que não é pré-requisito de nada. Mas o
> **`switch` não cai com ele**: mora no Bloco 3.

> **O script do banco saiu do Bloco 6 e foi para o Bloco 0.** Ele agora é
> idempotente — `CREATE IF NOT EXISTS` e `INSERT IGNORE` —, então rodar às 18h05
> não custa nada e **move as falhas de MySQL de 20h50, quando não há folga, para
> 18h05, quando há.**

### O que escrever no quadro às 18h

1. Hoje vocês escrevem a tela de login. Ela vem montada e **vazia por dentro**.
2. São **21 lacunas numeradas**. Procurem por `TODO`.
3. **F5 depois de cada uma.** Se não mudou nada na tela, algo ficou para trás.
4. O `UsuarioDAO` de hoje **é o da Atividade 1 de vocês**. Não é exercício.
5. Depois do intervalo: a conta que vocês criarem continua existindo amanhã.

---

## Os três momentos de projetor

| | Quando | O quê |
|:--:|:--|:--|
| **1** | Bloco 0 | O `CriarBanco.sql` no Workbench: o raio amarelo do *Execute All* e as três linhas do `SELECT` do fim |
| **2** | Bloco 2 | Criar conta numa tela e **entrar com ela pela outra** — a primeira vez na noite que duas telas conversam |
| **3** | Bloco 6 | **Apagar o `foreach` e escrever o `SELECT` no lugar**, com o arquivo do Bloco 2 aberto ao lado |

**O erro que vale ouro** *(Bloco 3)*: trocar `case 2` por `case 3` no `switch` das
tentativas. Compila, roda, e a tela diz "resta 1 tentativa" logo na primeira.
**Deixe acontecer** — é o argumento mais barato a favor de dar nome a constante,
e por isso o gabarito usa `MAXIMO_DE_TENTATIVAS` em vez de `3`.

**Noventa segundos de brinde** *(Bloco 6)*: abra o `TraduzirErro` do `Conexao.cs`,
que veio pronto. É um `switch` com `case 0: case 1042:` dividindo o mesmo corpo —
o mesmo `switch` que eles escreveram às 19h20, num lugar onde ele é obrigatório, e
escrevendo o que `else if` não escreve tão limpo.

**A pergunta que vai aparecer**: *"por que a senha está em texto puro na tabela?"*
A resposta certa **não** é uma aula de hash às 21h20. É: *"hoje o assunto é o
banco guardar; guardar senha direito é assunto de outra noite, e a pergunta está
certíssima."* Anote quem perguntou.

---

## Antes das 18h

| # | |
|:--:|:--|
| 1 | **Copiar o `login-inicial/` e a pasta `banco/` para as máquinas.** Sem GitHub, é pendrive — 10 a 20 minutos reais para 13 máquinas |
| 2 | **Não copiar `bin/`, `obj/` nem `.vs/`.** O `.gitignore` cuida do git; do pendrive, não |
| 3 | **Guardar uma cópia limpa.** Sem GitHub não existe desfazer |
| 4 | **Rodar o `CriarBanco.sql` numa máquina do laboratório** e conferir as três linhas |
| 5 | **Conferir o serviço MySQL80** em pelo menos três máquinas |
| 6 | **Escrever a senha do `root` no quadro** antes do Bloco 0 |

> ✅ **Verificado nesta máquina, de verdade.** O `login-inicial` compila **com
> todos os 21 TODOs vazios**: 0 erro, 0 aviso. O `login-final`: 0 erro, 0 aviso.
> **Os dois executáveis abrem** — testados rodando, não só compilando. E o
> `MySql.Data.dll` de `libs/` carregou em tempo de execução, então ninguém
> instala pacote NuGet às 20h45.
>
> ⚠️ **O que NÃO foi testado:** o fluxo completo contra um `conectadb` de
> verdade, e o laboratório. Os itens 4 e 5 acima não são opcionais.

---

## Avaliação da noite

| Conhecimento / Habilidade | Evidência |
|:--|:--|
| **C2** — estrutura de dados | A `static List<Usuario>` do Bloco 2 e os três `foreach` que a percorrem |
| **C4** — conexão e manipulação do banco | O `CriarBanco.sql` executado e os quatro métodos consultando o MySQL com `@parametro` |
| **C7** — POO | A `Sessao` criada do zero, o corpo do `UsuarioDAO`, e os cinco métodos escritos no `frmLogin` |
| **H1** — resolver problemas lógicos | Decidir entre `for` e `foreach` no Bloco 4, e entre `if` e `switch` no Bloco 3 — `achado == null` é `if`, `tentativas` é `switch` |
| **H3** — interpretar textos técnicos | Ler a mensagem do MySQL e decidir o que ela quer: serviço parado, senha ou banco inexistente |
| **H7** — analisar as etapas do processo | A ordem `Obter` → `Open` → `@parametro` → executar → `finally`, e saber por que o `Open` fica **dentro** do `try` |

> **Todo comando SQL com `@parametro`, nunca texto grudado com `+`.** É o
> critério que a ficha usa para o indicador de banco, e o exemplo do
> `' OR '1'='1` está comentado no rodapé do `UsuarioDAO.cs` do aluno.

---

## Depois da aula — o que ficou para a próxima

> **Preencha esta seção na mesma noite**, ainda com a memória fresca. É daqui
> que sai o planejamento da aula seguinte.

**Até onde a turma chegou:**

| Bloco | Fechou? | Observação |
|:--|:--:|:--|
| 1 — TODOs 1 e 2 | ☐ | |
| 2 — o `UsuarioDAO` | ☐ | **o inegociável** |
| 3 — `if` e `switch` | ☐ | |
| 4 — `for` e `foreach` | ☐ | o primeiro a cair |
| 5 — a `Sessao` criada do zero | ☐ | |
| 6 — o `SELECT` | ☐ | **a novidade da noite** |
| 7 — o `INSERT` e o teste de fechar e abrir | ☐ | |

**Quantos saíram com a conta sobrevivendo ao fechar e abrir:**

_______________________________________________________________________

**Quantas máquinas travaram no MySQL, e por quê (serviço, senha, banco):**

_______________________________________________________________________

**A diferença entre `for` e `foreach` ficou de pé? Quem soube explicar:**

_______________________________________________________________________

**Quantos chegaram no `frmCadastro`, e em qual TODO pararam:**

_______________________________________________________________________

**Em qual TODO a turma mais travou:**

_______________________________________________________________________

**A turma trabalhou sem você ao lado, hoje?**

_______________________________________________________________________

**Conteúdo que NÃO foi passado e precisa entrar na próxima:**

_______________________________________________________________________

**Ocorrências da noite** (para o campo Observações do app do Senac):

_______________________________________________________________________

---

## O que vem depois

**Amanhã é a Aula 16**, na pasta [`aula-14/`](../aula-14/aula-14.md) — a retomada
num arquivo só, sem conteúdo novo e sem banco. Se esta noite terminar com a turma
dividida entre quem persistiu e quem não, ela é o lugar de equilibrar.

**A Atividade 2 vence 28/08** e pede o CRUD completo do mural, com `INNER JOIN`,
`LIKE`, regra de dono e `finally` em todos os métodos. Quem sair hoje com o
`UsuarioDAO` no MySQL tem a fundação pronta — e o `PostagemDAO` é o mesmo molde,
nove vezes.

Duas pendências que **não** são desta noite e continuam abertas no registro:

- **Ordenação de dados** (C2). O plano de curso pede listas, ordenação e pesquisa;
  as noites anteriores entregaram listas e pesquisa. A ordenação tem casa marcada
  — é o `ORDER BY DataHora DESC` do feed, na Atividade 2.
- **Política de recuperação de dados** (C6), que segue marcada só na Aula 1, sem
  lastro nenhum.
