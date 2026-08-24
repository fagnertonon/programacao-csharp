# Login — a tela que a turma preenche

**Windows Forms.** A tela de login vem montada e **vazia por dentro**: 21
lacunas numeradas, em ordem, que a turma escreve junto com você. **Termina
falando com o MySQL.**

| Pasta | O que é |
|:--|:--|
| [`login-inicial/`](login-inicial/) | O que a turma recebe. **Compila e abre** — e nenhum botão faz nada |
| [`login-final/`](login-final/) | O gabarito, no estado das 22h — só seu |
| [`banco/`](banco/CriarBanco.sql) | O script da tabela de contas. Roda no Bloco 0 |

> **Este README é o do professor.** O aluno recebe a pasta `login-inicial/`,
> que tem o [`LEIA-ME.md`](login-inicial/LEIA-ME.md) dele dentro.

## O pacto

**O que vem pronto:** o Designer das três telas, a classe `Usuario`, o
`Conexao.cs` inteiro e o script do banco. **O que vem vazio:** toda a
lógica, em `// TODO` numerados.

Encanamento não é conteúdo — e é onde treze máquinas travam ao mesmo tempo.
O aluno só toca no `Conexao.cs` para trocar a senha do `root`.

> **Os quatro métodos do `UsuarioDAO` são os da Atividade 1** —
> `LoginExiste`, `CriarConta`, `Autenticar`, `BuscarPorId`, com os mesmos
> parâmetros e os mesmos retornos. **O arquivo que sair daqui às 22h é o
> arquivo que ele vai entregar.** Hoje eles nascem com `List` (a Parte 7) e
> às 20h45 só o corpo troca (a Parte 11). Vale dizer isso em voz alta no
> Bloco 0: a noite conta nota duas vezes.

---

## As 21 lacunas

| Arquivo | TODOs | O que é |
|:--|:--:|:--|
| `frmLogin.cs` | **10** | O arquivo da noite |
| `UsuarioDAO.cs` | **5** | A classe que guarda as contas |
| `frmCadastro.cs` | **5** | O extra, para quem terminar antes |
| `frmPrincipal.cs` | **1** | A tela de depois de entrar |
| `Sessao.cs` | — | **Não existe.** O aluno cria do zero no Bloco 5 |

**Já resolvidos no `frmLogin.cs`, de exemplo:** o `Avisar(string)`, com o
comentário registrando que ele nasceu de três `MessageBox.Show` de quatro
argumentos — o aluno vê a extração feita aqui e faz a dele no
`frmCadastro`. Mais o `btnCriarConta_Click` (uma linha) e o
`frmLogin_Shown`, que é rede de segurança de laboratório.

---

## O que aparece na tela, TODO a TODO

**Rode F5 depois de cada um.** É o que segura a turma até as 22h.

| TODO | Bloco | O que passa a funcionar |
|:--:|:--:|:--|
| 1 | 1 | O botão **Limpar** limpa |
| 2 | 1 | Entrar com campo vazio **reclama**, em vez de não fazer nada |
| DAO 1–5 | 2 | Criar conta numa tela e **entrar com ela pela outra** |
| 3 a 5 | 3 | O login funciona de verdade |
| 6 | 3 | Errar três vezes **trava o botão Entrar** |
| 8 a 10 | 4 | `#`, `##`, `###` **enquanto se digita a senha** |
| 7 e frmPrincipal 1 | 5 | A tela de dentro diz **o nome de quem entrou** |
| — | 6 | Entra com `ana`/`1234`, **que ninguém cadastrou nesta execução** |
| — | 7 | Cria conta, **fecha o programa, abre e entra** |

> **O erro que vale ouro, no Bloco 3:** trocar `case 2` por `case 3` no
> `switch` das tentativas. O compilador não reclama, o programa roda, e a
> tela diz "resta 1 tentativa" logo na primeira. **Deixe acontecer.**

---

## Onde cada construção mora, e por quê

Esta é a coluna que sustenta a noite. Se perguntarem *"por que `for` aqui e
`foreach` ali?"*, a resposta é a direita — não uma definição.

| Construção | Onde | Por que **ali** |
|:--|:--|:--|
| **método** | `LimparCampos`, `Entrar`, `MostrarErroDeTentativa`, `ForcaDaSenha`, `BarraDeForca` | Cinco corpos vazios com a assinatura pronta. O `Avisar` vem resolvido, de molde |
| **classe** | `Sessao.cs`, **do zero** | É o único arquivo que não vem no pacote, e o motivo é sentido na pele: o `Usuario` que o login achou é variável local dentro de um evento — morre no fim do método |
| **`static`** | `UsuarioDAO` e `Sessao` | **O contraste é o conteúdo:** o DAO é `static` porque a lista tem de ser **uma só**; a `Sessao` é `static` porque o dado precisa **atravessar telas** |
| **`if`** | `btnEntrar_Click` — TODOs 2 e 4 | Cada pergunta é sobre uma **coisa diferente**, e a resposta é sim/não. Isso não vira `switch` nunca |
| **`switch`** | `MostrarErroDeTentativa` — TODO 6 | Um valor só (`tentativas`) contra constantes. E fica **dez linhas abaixo do `if`**, no mesmo fluxo |
| **`foreach`** | `ForcaDaSenha` (TODO 9) e os três métodos de busca do DAO | Percorre o que **existe**. Quem manda nas voltas é a coleção |
| **`for`** | `BarraDeForca` — TODO 10 | **Não há coleção nenhuma.** Há um número calculado de `#`. Quem manda nas voltas é a conta |
| **banco** | O corpo dos quatro métodos do DAO, nos Blocos 6 e 7 | Entra pelo motivo que o aluno já sentiu: criou a conta, apertou F5, sumiu |

**Três `switch` na noite, de propósito.** O do TODO 6 **não tem `default`**
— ali só existem 1, 2 e 3. O do TODO 10 **tem**, porque existe "qualquer
outro caso". E o terceiro o aluno só lê: o `TraduzirErro` do `Conexao.cs`,
com `case 0: case 1042:` dividindo o mesmo corpo — 90 segundos de projetor
no Bloco 6, mostrando o que `else if` não escreve tão limpo.

> **O momento da noite é o `foreach` desaparecendo.** O laço escrito às
> 18h50 vira `WHERE Login = @login`. Quem percorre as linhas agora é o
> MySQL, e o `WHERE` é literalmente o `if` que estava dentro do laço.

---

## Roteiro em blocos

| Bloco | Hora | Min | O quê |
|:--|:--:|:--:|:--|
| **0** | 18:00 | 15 | Copiar · abrir o `Login.sln` · F5 · **rodar o `CriarBanco.sql`** · trocar o `pwd` |
| **1** | 18:15 | 20 | **Método** — TODOs 1 e 2 |
| **2** | 18:35 | 35 | **Classe e `foreach`** — os 5 TODOs do `UsuarioDAO`. ⭐ |
| **3** | 19:10 | 30 | **`if` e `switch`** — TODOs 3 a 6 |
| — | 19:40 | 15 | **Intervalo** |
| **4** | 19:55 | 25 | **`foreach` e `for`** — TODOs 8 a 10 |
| **5** | 20:20 | 25 | **Classe** — criar `Sessao.cs` + TODO 7 + frmPrincipal |
| **6** | 20:45 | 45 | **O banco entra** — `Autenticar` e `LoginExiste` viram `SELECT` |
| **7** | 21:30 | 20 | **`INSERT`** — criar conta, fechar, abrir, entrar |
| **8** | 21:50 | 10 | Recados · o `frmCadastro` para quem terminou |

Soma: 15+20+35+30+15+25+25+45+20+10 = **240**. ✓

**A ordem do que não pode cair:** Bloco 2 > Bloco 6 > Bloco 3 > Bloco 5 >
Bloco 4.

> **O Bloco 4 cai primeiro, e sem dó** — é o único que não é pré-requisito
> de nada. Mas repare: **o `switch` não cai com ele**, porque mora no
> Bloco 3. O pedido explícito da noite está protegido.

> **O script saiu do Bloco 6 e foi para o Bloco 0.** Ele agora é
> idempotente, então rodar às 18h05 não custa nada — e move as falhas de
> MySQL de 20h50, quando não há folga, para 18h05, quando há. Às 20h45 o
> Bloco 6 vira só o que deveria ser: trocar o corpo de dois métodos.

---

## Se o tempo apertar

| Situação | O que fazer |
|:--|:--|
| 20h45 e o Bloco 5 não fechou | **Pule o 5 inteiro.** O banco não depende da `Sessao` |
| 21h20 e o `SELECT` não está de pé em metade da turma | Feche o 6 com quem está e passe o 7 no quadro, para casa |
| Máquina sem MySQL | Ela para no Bloco 6. Junte com quem tem — **dois numa máquina é melhor que um parado** |
| **A turma inteira sem MySQL** | A noite roda inteira com a `List` do Bloco 2, e o Bloco 6 vira projetor. **E não é noite mutilada:** o DAO em memória e o DAO no MySQL têm exatamente as mesmas quatro assinaturas, então o que ficou pronto **é a Parte 7 completa** da Atividade 1 |

---

## Para quem terminar antes

O `frmCadastro.cs`, com os 5 TODOs dele. Está disponível de 19h10 em
diante — com 13 alunos, dois ou três vão precisar.

O TODO 5 dele **não é código**: manda o aluno voltar ao arquivo depois do
Bloco 6 e conferir que **nenhuma linha mudou** quando o DAO foi para o
MySQL. É a prova de que a tela deixou de saber de onde vem o dado.

| # | Desafio, se ainda sobrar |
|:--:|:--|
| 1 | O **Limpar** também apaga a barra de força. Ele já faz? Por quê? |
| 2 | Recusar login com menos de 3 caracteres **sem** criar `if` novo na tela |
| 3 | A senha está em texto puro na tabela. Qual seria o primeiro passo? *(discussão)* |
| 4 | Trocar `AddWithValue` por texto grudado com `+` e digitar `' OR '1'='1` no campo Usuario. **Depois desfazer** |

---

## Antes das 18h

| # | |
|:--:|:--|
| 1 | **Copiar o `login-inicial/` para as máquinas.** Sem GitHub, é pendrive ou pasta de rede — 10 a 20 minutos reais para 13 máquinas. Comece antes de a turma chegar |
| 2 | **Levar a pasta `banco/` junto**, ou o Bloco 0 não acontece |
| 3 | **Não copie `bin/`, `obj/` nem `.vs/`.** O `.gitignore` cuida do git; do pendrive, não |
| 4 | **Guardar uma cópia limpa.** Sem GitHub não existe desfazer |
| 5 | **Rodar o `CriarBanco.sql` numa máquina do laboratório** e conferir as três linhas |
| 6 | **Conferir o serviço MySQL80** em pelo menos três máquinas |
| 7 | **Escrever a senha do `root` no quadro** — ela entra no `Conexao.cs` de 13 máquinas |

> ✅ **Verificado nesta máquina, de verdade.**
> `login-inicial` compila **com todos os 21 TODOs vazios**: 0 erro, 0 aviso.
> `login-final`: 0 erro, 0 aviso. **Os dois executáveis abrem** — testados
> rodando, não só compilando. O `MySql.Data.dll` de `libs/` foi carregado em
> tempo de execução e devolveu erro traduzido do MySQL, provando que a
> referência sem NuGet funciona.
>
> ⚠️ **O que NÃO foi testado:** o fluxo completo contra um `conectadb` de
> verdade, e o laboratório. Os itens 5 e 6 acima não são opcionais.
