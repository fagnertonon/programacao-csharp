# Conecta — a tela de login

**Abra o `Login.sln` e aperte F5 antes de escrever qualquer coisa.**

A tela aparece, os campos aceitam digitação, os botões existem — e nada
acontece. É assim mesmo. O que falta é o que você vai escrever hoje.

---

## Onde você trabalha

| Arquivo | Você mexe? | O quê |
|:--|:--:|:--|
| `frmLogin.cs` | **escreve** | **10 TODOs** — é o arquivo da noite |
| `UsuarioDAO.cs` | **escreve** | **5 TODOs** — a classe que guarda as contas |
| `frmPrincipal.cs` | **escreve** | **1 TODO** — a tela de depois de entrar |
| `frmCadastro.cs` | **extra** | **5 TODOs** — só depois que o login estiver de pé |
| `Sessao.cs` | **cria** | não existe ainda. Você cria no Bloco 5 |
| `Conexao.cs` | **só a senha** | já está pronto. Troque `SUA_SENHA_AQUI` |
| `Usuario.cs` | lê | a classe da conta, já pronta |
| `Program.cs`, `*.Designer.cs`, `libs/` | **não** | é o encanamento |

Procure por `TODO` no Visual Studio: **Exibir → Lista de Tarefas**, ou
`Ctrl+F` por `TODO`. Eles estão numerados e em ordem.

---

## Antes de começar: o banco

1. Abra o **MySQL Workbench** e conecte em *Local instance MySQL80*.
2. **File → Open SQL Script**, escolha o `banco/CriarBanco.sql` (está na
   pasta de cima).
3. Clique no **raio AMARELO** (*Execute All*) — não no raio com o cursor.
4. Tem que aparecer uma tabela com **três linhas**: admin, ana e bruno.
5. Abra o `Conexao.cs` e troque `pwd=SUA_SENHA_AQUI` pela senha do seu
   `root`.

> Esse script pode ser rodado quantas vezes quiser. Ele não apaga nada.

Se o MySQL não estiver de pé, a tela avisa sozinha assim que abre — é o
`lblStatus`, na parte de baixo. Leia o que ele diz: a mensagem já vem
traduzida e diz o que fazer.

---

## O combinado

**Aperte F5 depois de cada TODO.** Se você escreveu e nada mudou na tela,
alguma coisa ficou para trás — e é muito mais barato descobrir isso agora
do que três TODOs depois.

**O projeto tem que compilar o tempo todo.** Por isso alguns TODOs vêm com
uma *linha-tampão* embaixo, marcada com `APAGUE a linha-tampao abaixo`.
Ela só existe para o projeto não quebrar enquanto o corpo está vazio.
Apague quando escrever o seu código.

**Não mude o nome dos métodos nem o que está entre parênteses.** Os quatro
métodos do `UsuarioDAO` são exatamente os da sua Atividade 1 — o arquivo
que sair daqui às 22h é o arquivo que você vai entregar.

---

## O que aparece na tela, TODO a TODO

| TODO | O que passa a funcionar |
|:--:|:--|
| 1 | O botão **Limpar** limpa |
| 2 | Entrar com campo vazio reclama, em vez de não fazer nada |
| 3 a 5 | Entrar com `admin` / `admin` abre a tela de dentro |
| 6 | Errar a senha três vezes **trava o botão Entrar** |
| 7 | A tela de dentro diz **o seu nome** |
| 8 a 10 | `#`, `##`, `###` aparecendo **enquanto você digita a senha** |
