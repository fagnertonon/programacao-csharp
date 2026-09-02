# Portaria — ponto de partida

**Abra o `Portaria.sln` e aperte F5 antes de escrever qualquer coisa.**

A tela de login aparece. Os botões estão lá, os campos estão lá — e nada funciona
ainda. É assim mesmo.

O sistema já está montado; o que falta é o miolo, e o miolo é seu: **14 métodos**.

---

## O que é a Portaria

Um sistema de acesso, com três telas:

| Tela | O que faz |
|:--|:--|
| **Login** | Digitar usuário e senha para entrar |
| **Cadastro** | Criar uma conta nova |
| **Principal** | A lista de todos os usuários cadastrados |

**Uma tabela só, no MySQL**, no banco `portariadb`.

> O `conectadb` das aulas anteriores continua na sua máquina, intacto. São dois
> bancos diferentes.

---

## Antes do TODO 1: ligue o banco

| # | O quê |
|:--:|:--|
| 1 | Confira que o serviço **MySQL80** está em execução (Windows + R, `services.msc`) |
| 2 | Abra o **Workbench** e rode o `banco/CriarBanco.sql` — **uma vez só** |
| 3 | Abra o `Dados/Conexao.cs` e troque `SUA_SENHA_AQUI` pela sua senha do MySQL |
| 4 | F5. A faixa de baixo do login tem que ficar **verde** |

> ⛔ O `CriarBanco.sql` começa com `DROP DATABASE`. Rodar de novo depois de ter
> cadastrado usuários apaga os usuários. Rode **uma vez**, no começo.

---

## A regra que vai para o quadro

> ### O `Regras.cs` não abre banco e não desenha tela.
>
> Procure lá dentro pelo nome da biblioteca do MySQL e pelo do Windows Forms:
> **tem que dar zero nos dois.**

Todo método de lá **recebe** valores e **devolve** resposta. Quem fala com o banco
é o `UsuarioDAO`; quem desenha é o `frm...`.

---

## Os arquivos, e onde você mexe

| Arquivo | Você mexe? | O que é |
|:--|:--:|:--|
| `Modelo/Usuario.cs` | **ESCREVE** | TODO 2 |
| `Regras/Regras.cs` | **ESCREVE** | TODO 3 e 11 · **o defeito mora aqui** |
| `Dados/UsuarioDAO.cs` | **ESCREVE** | TODO 4, 5, 7, 8, 10, 13 |
| `Telas/frmLogin.cs` | **ESCREVE** | TODO 1 e 9 |
| `Telas/frmCadastro.cs` | **ESCREVE** | TODO 6 |
| `Telas/frmPrincipal.cs` | **ESCREVE** | TODO 12 e 14 |
| `Dados/Conexao.cs` | **uma linha** | A senha do seu MySQL, e só |
| `Modelo/Sessao.cs` | não | Quem está logado agora |
| `*.Designer.cs` | não | O layout das telas |
| `Program.cs` | não | Encanamento |

---

## PRIMEIRA NOITE — TODO 1 a 6

| # | Onde | Indicador | O que muda quando você escreve |
|:--:|:--|:--:|:--|
| **1** | `frmLogin.btnTestarConexao_Click` | I1 · I4 | **A faixa acende verde.** É o primeiro — sem ele nada mais funciona |
| **2** | `Usuario.Nome` — o `set` | I2 | O nome para de entrar torto |
| **3** | `Regras.ValidarLogin` | I2 · I3 | A tela ganha como perguntar se o login serve |
| **4** | `UsuarioDAO.LoginExiste` | I5 | O sistema recusa login repetido |
| **5** | `UsuarioDAO.CriarConta` | I5 | **O primeiro dado que sai do programa e fica no banco** |
| **6** | `frmCadastro.btnCadastrar_Click` | I3 · I2 · I6 | **O cadastro funciona.** Fim da primeira noite |

> ### O teste que fecha a noite 1
> Crie uma conta. **Feche o programa.** Abra de novo e entre com ela.
>
> **A conta continua lá?** Então o dado saiu do programa e ficou no MySQL de
> verdade. Se sumiu, ele nunca chegou ao banco.
>
> *(Para entrar você precisa do TODO 8 e 9 — na noite 1, confira pelo Workbench:
> `SELECT * FROM Usuario`.)*

---

## SEGUNDA NOITE — TODO 7 a 14

| # | Onde | Indicador | O que muda quando você escreve |
|:--:|:--|:--:|:--|
| **7** | `UsuarioDAO.MontarUsuario` | I5 · I2 | Nada sozinho — mas o 8 e o 10 dependem dele |
| **8** | `UsuarioDAO.Autenticar` | I5 · I3 | O banco passa a saber dizer quem é quem |
| **9** | `frmLogin.btnEntrar_Click` | I3 · I4 · I6 | **Você entra no sistema** |
| **10** | `UsuarioDAO.ListarTodos` | I5 · I2 | A lista sai do banco, nas duas ordens |
| **11** | `Regras.UltimoCadastrado` | I2 · I3 · I4 | O rodapé diz quem foi o último |
| **12** | `frmPrincipal.CarregarLista` | I3 · I5 | **A lista aparece na tela** |
| **13** | `UsuarioDAO.ExcluirConta` | I5 | Dá para tirar alguém do sistema |
| **14** | `frmPrincipal.btnExcluir_Click` | I3 · I4 · I6 | O botão Excluir funciona |

**O chão da noite 2 é o TODO 12.** Com ele, o sistema entra e lista. O 13 e o 14
são o acabamento — mas são eles que trazem o `DELETE`.

> Escreva o **7 primeiro**. Ele é curto e os dois seguintes dependem dele.

---

## O defeito plantado — a caça da segunda noite

**Um método deste projeto já vem escrito, e ele está com defeito.** Não é engano:
é o exercício de depuração da UC.

Ele mora em **`Regras.PrimeiroNome`**, e a tela principal usa esse método para
dizer *"Conectado como: ..."* no alto.

**O sintoma:** você entra com a conta de **Ana Souza**, e a tela escreve

```
Conectado como:  Souza
```

quando deveria escrever `Conectado como: Ana`.

| | |
|:--|:--|
| **Ele não quebra o programa** | Nenhuma mensagem de erro, nenhuma tela travada |
| **Ele não é erro de compilação** | O projeto compila com ele vivo — a Lista de Erros não vai ajudar |
| **Ele acerta às vezes** | Com um nome de **uma palavra só**, a resposta sai certa |

O que ajuda aqui é o **breakpoint**: pare a linha, aperte F10, e olhe na janela
**Locais** o valor da variável `espaco` e o que o `Substring` está devolvendo.

> **Não conserte antes da hora.** Ver o defeito acontecer é metade do exercício —
> e é o que o professor vai te pedir para explicar.

---

## A armadilha avisada

Cada método a escrever nasce com uma linha provisória:

```csharp
return false;   // <<< TROQUE ESTA LINHA pela sua
```

Ela existe **só para o projeto compilar antes de você começar**. Se ela ficar, o
método responde sempre a mesma coisa — e você vai procurar o erro no lugar errado.

Nas telas, a provisória é um `MessageBox` dizendo que o TODO não foi escrito —
com **uma exceção**: no TODO 12 (`CarregarLista`) ela é a linha que escreve
`"TODO 12 ainda nao foi escrito."` no rodapé, porque uma caixa de mensagem
saltando toda vez que a tela abre seria insuportável.

---

## As três regras do banco

Valem para os **cinco métodos do `UsuarioDAO` que abrem conexão** — todos menos o
`MontarUsuario`, que só transforma uma linha já lida em objeto:

| # | Regra | Por quê |
|:--:|:--|:--|
| 1 | Todo valor entra por `@parametro`, **nunca colado com `+`** | Texto colado vira comando |
| 2 | `UPDATE` e `DELETE` **sempre** com `WHERE` | Sem ele, o comando pega a tabela inteira |
| 3 | A conexão fecha no `finally` | Fecha mesmo quando dá erro no meio |

> ⛔ **Um `DELETE` sem `WHERE` no código entregue reprova o indicador I5 direto.**
> Não é rigor: é que ele esvazia a tabela toda de uma vez, e não há como desfazer.

---

## As nove mensagens — elas vão para o manual

As mensagens que você escreve nos TODO **6, 9 e 14** são, palavra por palavra, a
seção *"Mensagens do sistema"* do seu manual. Escreva textos que um usuário comum
entenda, e **não mude a redação depois**.

| Código | Onde | Quando aparece |
|:--:|:--|:--|
| M1 | TODO 6 | Algum campo em branco |
| M2 | TODO 6 | Login inválido (espaço, ou curto demais) |
| M3 | TODO 6 | Senha curta |
| M4 | TODO 6 | As duas senhas não são iguais |
| M5 | TODO 6 | Login já em uso |
| M6 | TODO 9 | Usuário ou senha em branco |
| M7 | TODO 9 | Usuário ou senha incorretos |
| M8 | TODO 14 | Nenhuma linha selecionada |
| M9 | TODO 14 | Tentou excluir a própria conta |

---

## Roteiro de teste — quando terminar

| # | O que fazer | Deve acontecer |
|:--:|:--|:--|
| 1 | Clicar em **Testar conexão** | Faixa verde e mensagem de OK |
| 2 | Criar conta, fechar o programa, abrir e entrar | A conta continua lá |
| 3 | Cadastrar com um campo em branco | Recusa (M1) |
| 4 | Cadastrar com usuário `ab`, e depois com `ana bia` | Recusa nos dois (M2) |
| 5 | Cadastrar com senha `123` | Recusa (M3) |
| 6 | Digitar senhas diferentes | Recusa (M4) |
| 7 | Cadastrar com o usuário `ana` | Recusa (M5) |
| 8 | Entrar com a senha errada | Recusa e **não quebra** (M7) |
| 9 | Trocar a ordem no ComboBox | A lista muda de ordem |
| 10 | Clicar em Excluir sem escolher ninguém | Recusa (M8) |
| 11 | Tentar excluir a si mesmo | Recusa (M9) |
| 12 | Excluir outro usuário | Pergunta antes, e some da lista |
| 13 | Cadastrar um nome com acento | Aparece certo na lista |

**Se os 13 passarem, o sistema está pronto para a entrega.**

---

## Se o MySQL não conectar

A faixa vermelha já traz a frase certa. As quatro que aparecem de verdade:

| Mensagem | O que fazer |
|:--|:--|
| Não achei o servidor | O serviço **MySQL80** está parado. `services.msc` e iniciar |
| Usuário ou senha incorretos | A senha do `Conexao.cs` não é a sua |
| O banco `portariadb` não existe | Falta rodar o `CriarBanco.sql` |
| A tabela `Usuario` não existe | O script rodou pela metade. Rode de novo |
