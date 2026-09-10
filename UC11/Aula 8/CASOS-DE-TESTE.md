# Casos de teste — os 20 desafios de acesso

> ⚠️ **Documento do professor. Não distribuir.** É a lista mestra: dela saem os casos do
> corretor, a suíte do `acesso-final/` e o que vai para o quadro quando a turma discutir os
> casos antes de escrever o teste.

**UC11 · Aulas 8 e 9 · 10 e 11/09/2026**

| | |
|:--|:--|
| **Desafios 1 a 10** | Sistema de login — noite de 10/09 |
| **Desafios 11 a 20** | Sistema de cadastro — noite de 11/09 |
| **Por desafio** | o aluno escreve **o método** e **o teste unitário** |

---

## Como ler este documento

Cada desafio traz três coisas:

| | |
|:--|:--|
| **A regra** | o que o método tem de fazer, em uma frase |
| **Os casos** | o que o corretor confere no método do aluno |
| **A fronteira obrigatória** | os valores que o `[DataRow]` do aluno **tem de** conter, senão a aba não fecha |

> **A fronteira obrigatória é a novidade da noite.** Não basta o método funcionar: o teste
> precisa provar que o aluno pensou no limite. O corretor lê os `[DataRow]` por reflexão e
> confere se aqueles valores estão lá.

---

# LOGIN — desafios 1 a 10

## 1 · `UsuarioValido(string usuario)`

**A regra:** o usuário tem de 4 a 20 caracteres, incluindo os dois extremos.

| Entrada | Esperado | Por quê |
|:--|:--:|:--|
| `"ana"` | `false` | 3 caracteres, um a menos |
| `"joao"` | `true` | **limite de baixo: exatamente 4** |
| `"abcdefghijabcdefghij"` | `true` | **limite de cima: exatamente 20** |
| `"abcdefghijabcdefghijk"` | `false` | 21, um a mais |
| `""` | `false` | vazio |

**Fronteira obrigatória no teste:** comprimentos **3, 4, 20 e 21**.

---

## 2 · `SenhaTemTamanhoMinimo(string senha)`

**A regra:** no mínimo 8 caracteres.

| Entrada | Esperado | Por quê |
|:--|:--:|:--|
| `"1234567"` | `false` | 7, um a menos |
| `"12345678"` | `true` | **limite: exatamente 8** |
| `"123456789"` | `true` | 9, folgado |
| `""` | `false` | vazio |

**Fronteira obrigatória:** comprimentos **7, 8 e 9**.

> É o `no minimo` contra o `mais que` — o mesmo defeito da Aula 6, agora do lado de quem
> escreve.

---

## 3 · `SenhaTemNumero(string senha)`

**A regra:** tem de haver pelo menos um dígito.

| Entrada | Esperado |
|:--|:--:|
| `"abc123"` | `true` |
| `"abcdef"` | `false` |
| `"1"` | `true` |
| `""` | `false` |

**Fronteira obrigatória:** um caso **com** dígito, um **sem**, e o **vazio**.

---

## 4 · `SenhaTemMaiuscula(string senha)`

**A regra:** tem de haver pelo menos uma letra maiúscula.

| Entrada | Esperado |
|:--|:--:|
| `"abcDef"` | `true` |
| `"abcdef"` | `false` |
| `"A"` | `true` |
| `""` | `false` |

**Fronteira obrigatória:** um caso **com** maiúscula, um **sem**, e o **vazio**.

---

## 5 · `ForcaDaSenha(string senha)`

**A regra:** vale um ponto para cada um dos desafios 2, 3 e 4 que a senha cumprir. O
resultado vai de 0 a 3.

| Entrada | Esperado | Por quê |
|:--|:--:|:--|
| `"abc"` | `0` | **piso: não cumpre nenhum** |
| `"abcdefgh"` | `1` | só o tamanho |
| `"abcdefg1"` | `2` | tamanho e número |
| `"Abcdefg1"` | `3` | **teto: os três** |
| `"Abc1"` | `2` | número e maiúscula, mas curta |

**Fronteira obrigatória:** os extremos **0 e 3**.

> **Primeiro método que chama outros.** Se o desafio 2 estiver errado, este quebra junto —
> e é aí que a propagação começa a aparecer.

---

## 6 · `SenhaAceita(string senha)`

**A regra:** a senha é aceita quando a força é 2 ou mais. Chama o desafio 5.

| Entrada | Força | Esperado |
|:--|:--:|:--:|
| `"abc"` | 0 | `false` |
| `"abcdefgh"` | 1 | `false` — **limite de baixo** |
| `"abcdefg1"` | 2 | `true` — **limite: exatamente 2 já aceita** |
| `"Abcdefg1"` | 3 | `true` |

**Fronteira obrigatória:** senhas de força **1, 2 e 3**.

---

## 7 · `Autenticar(string usuario, string senha, string usuarioGravado, string senhaGravada)`

**A regra:** o usuário **não** diferencia maiúscula de minúscula. A senha **diferencia**.

| usuario | senha | gravado | senha gravada | Esperado |
|:--|:--|:--|:--|:--:|
| `"ana"` | `"Senha1"` | `"ana"` | `"Senha1"` | `true` |
| `"ANA"` | `"Senha1"` | `"ana"` | `"Senha1"` | `true` — **usuário ignora a caixa** |
| `"ana"` | `"senha1"` | `"ana"` | `"Senha1"` | `false` — **senha diferencia** |
| `"bia"` | `"Senha1"` | `"ana"` | `"Senha1"` | `false` |
| `""` | `""` | `"ana"` | `"Senha1"` | `false` |

**Fronteira obrigatória:** o par **`ANA` contra `ana`** e o par **senha com a caixa trocada**.

> **As duas metades da regra num método só.** Um teste que só usa `"ana"`/`"Senha1"` fica
> verde com as duas comparações erradas.

---

## 8 · `TentativasRestantes(int tentativas)`

**A regra:** são 3 tentativas no total. O resultado nunca é negativo.

| Entrada | Esperado | Por quê |
|:--|:--:|:--|
| `0` | `3` | ninguém errou ainda |
| `1` | `2` | |
| `3` | `0` | **limite: acabou** |
| `4` | `0` | **nunca negativo** |

**Fronteira obrigatória:** **0, 3 e 4**.

---

## 9 · `ContaBloqueada(int tentativas)`

**A regra:** a conta está bloqueada quando não resta nenhuma tentativa. Chama o desafio 8.

| Entrada | Restantes | Esperado |
|:--|:--:|:--:|
| `0` | 3 | `false` |
| `2` | 1 | `false` — **última chance** |
| `3` | 0 | `true` — **bloqueou** |
| `5` | 0 | `true` |

**Fronteira obrigatória:** **2 e 3**.

---

## 10 · `MensagemDoLogin(string usuario, string senha, string usuarioGravado, string senhaGravada, int tentativas)`

**A regra:** diz **por que** o login foi recusado, em uma frase. **O mais forte manda**, e a
ordem é esta:

```
1o  conta bloqueada      -> "Conta bloqueada por excesso de tentativas."
2o  campo em branco      -> "Preencha o usuario e a senha."
3o  nao autenticou       -> "Usuario ou senha incorretos."
    deu tudo certo       -> ""
```

| Situação | Esperado |
|:--|:--|
| 3 tentativas, **senha certa** | `"Conta bloqueada por excesso de tentativas."` |
| 0 tentativas, usuário vazio | `"Preencha o usuario e a senha."` |
| 0 tentativas, senha errada | `"Usuario ou senha incorretos."` |
| 0 tentativas, tudo certo | `""` |

**Fronteira obrigatória:** o caso em que a conta está **bloqueada e a senha está certa** —
é ele que prova que o bloqueio vem antes de tudo.

> **O desafio que fecha a noite.** Chama o 7 e o 9, que chamam o 8 e o 2, 3, 4. Se qualquer
> um estiver errado, este fica vermelho — e o aluno descobre sozinho onde está o defeito.

---

# CADASTRO — desafios 11 a 20

## 11 · `EmailValido(string email)`

**A regra:** tem de existir uma arroba, e um ponto **depois** dela.

| Entrada | Esperado | Por quê |
|:--|:--:|:--|
| `"ana@senac.br"` | `true` | |
| `"ana.senac.br"` | `false` | não tem arroba |
| `"ana.b@senacbr"` | `false` | **o ponto está antes da arroba** |
| `"ana@senacbr"` | `false` | tem arroba, não tem ponto |
| `""` | `false` | |

**Fronteira obrigatória:** **sem arroba**, **ponto antes da arroba**, **ponto depois**.

> O caso `"ana.b@senacbr"` é a armadilha: quem só procura `.` e `@` no texto inteiro dá
> verde nele.

---

## 12 · `TelefoneValido(string telefone)`

**A regra:** 10 ou 11 dígitos, e **só** dígitos.

| Entrada | Esperado | Por quê |
|:--|:--:|:--|
| `"279999999"` | `false` | 9 |
| `"2733334444"` | `true` | **10, fixo** |
| `"27999998888"` | `true` | **11, celular** |
| `"279999988887"` | `false` | 12 |
| `"2799999888a"` | `false` | 11, mas tem letra |

**Fronteira obrigatória:** comprimentos **9, 10, 11, 12** e um **com letra**.

---

## 13 · `CepValido(string cep)`

**A regra:** exatamente 8 dígitos.

| Entrada | Esperado |
|:--|:--:|
| `"2901000"` | `false` |
| `"29010000"` | `true` |
| `"290100000"` | `false` |
| `"2901000a"` | `false` |

**Fronteira obrigatória:** comprimentos **7, 8 e 9**.

---

## 14 · `IdadeValida(int idade)`

**A regra:** de 18 a 120, **incluindo os dois**.

| Entrada | Esperado |
|:--|:--:|
| `17` | `false` |
| `18` | `true` |
| `120` | `true` |
| `121` | `false` |
| `0` | `false` |

**Fronteira obrigatória:** **17, 18, 120 e 121**.

---

## 15 · `NomeCompleto(string nome)`

**A regra:** pelo menos duas palavras.

| Entrada | Esperado | Por quê |
|:--|:--:|:--|
| `"Ana"` | `false` | uma palavra |
| `"Ana Souza"` | `true` | duas |
| `"  Ana   Souza  "` | `true` | **espaço sobrando não inventa nem tira palavra** |
| `""` | `false` | |
| `"   "` | `false` | só espaço |

**Fronteira obrigatória:** **uma palavra**, **duas** e **espaço sobrando**.

---

## 16 · `CpfTemFormato(string cpf)`

**A regra:** 11 dígitos, e só dígitos. **Não valida o dígito verificador** — é formato, não
matemática.

| Entrada | Esperado |
|:--|:--:|
| `"1234567890"` | `false` |
| `"12345678901"` | `true` |
| `"123456789012"` | `false` |
| `"1234567890a"` | `false` |

**Fronteira obrigatória:** comprimentos **10, 11 e 12**.

---

## 17 · `CamposPreenchidos(string nome, string email, string telefone)`

**A regra:** nenhum dos três pode estar vazio nem conter só espaço.

| nome | email | telefone | Esperado |
|:--|:--|:--|:--:|
| `"Ana"` | `"a@b.c"` | `"2733334444"` | `true` |
| `""` | `"a@b.c"` | `"2733334444"` | `false` |
| `"   "` | `"a@b.c"` | `"2733334444"` | `false` |
| `"Ana"` | `""` | `"2733334444"` | `false` |
| `"Ana"` | `"a@b.c"` | `"   "` | `false` |

**Fronteira obrigatória:** um campo **vazio** e um campo **só com espaço**.

---

## 18 · `EmailJaCadastrado(string email)`

**A regra:** três contas já existem — `ana@senac.br`, `bruno@senac.br` e `carla@senac.br`.
A comparação **não** diferencia maiúscula de minúscula.

| Entrada | Esperado |
|:--|:--:|
| `"ana@senac.br"` | `true` |
| `"ANA@SENAC.BR"` | `true` |
| `"novo@senac.br"` | `false` |
| `""` | `false` |

**Fronteira obrigatória:** um **existente**, o mesmo **em maiúsculas** e um **novo**.

---

## 19 · `PodeCadastrar(string nome, string email, string telefone, string cep, int idade, string senha)`

**A regra:** o cadastro só passa quando **todas** as condições valem ao mesmo tempo — campos
preenchidos, nome completo, e-mail válido e ainda não cadastrado, telefone, CEP, idade e
senha aceita. Chama os desafios 11 a 18 e o 6.

| O que está errado | Esperado |
|:--|:--:|
| nada | `true` |
| nome com uma palavra só | `false` |
| e-mail sem ponto depois da arroba | `false` |
| e-mail já cadastrado | `false` |
| idade 17 | `false` |
| senha de força 1 | `false` |

**Fronteira obrigatória:** o caso **tudo certo** e **um falho por vez** — nunca dois erros
juntos, senão não se sabe qual condição pegou.

---

## 20 · `MensagemDoCadastro(string nome, string email, string telefone, string cep, int idade, string senha)`

**A regra:** diz **por que** o cadastro foi recusado. O mais forte manda:

```
1o  campo em branco        -> "Preencha todos os campos."
2o  nome incompleto        -> "Informe o nome completo."
3o  e-mail invalido        -> "E-mail invalido."
4o  e-mail ja cadastrado   -> "Este e-mail ja esta cadastrado."
5o  telefone invalido      -> "Telefone invalido."
6o  CEP invalido           -> "CEP invalido."
7o  idade fora da faixa    -> "Idade fora do permitido."
8o  senha fraca            -> "Senha muito fraca."
    deu tudo certo         -> ""
```

| Situação | Esperado |
|:--|:--|
| tudo certo | `""` |
| nome vazio | `"Preencha todos os campos."` |
| nome `"Ana"` | `"Informe o nome completo."` |
| e-mail `"ana@senacbr"` | `"E-mail invalido."` |
| e-mail `"ana@senac.br"` | `"Este e-mail ja esta cadastrado."` |
| idade 17 | `"Idade fora do permitido."` |

**Fronteira obrigatória:** um e-mail **inválido** e um e-mail **válido porém já cadastrado**
— são duas recusas diferentes, e a ordem entre elas é o que este desafio cobra.

---

## O grafo de dependências

É ele que faz a propagação acontecer, e é o que substitui a demonstração de regressão do
Caixa: um erro lá atrás pinta de vermelho os desafios da frente.

```
  2 ─┐
  3 ─┼─→ 5 ─→ 6 ──┐
  4 ─┘             │        ┌─→ 19
                   ├────────┤
  11 · 12 · 13 ──┐ │        └─→ 20
  14 · 15 · 17 ──┼─┘
  18 ────────────┘

  8 ─→ 9 ─┐
  7 ──────┴─→ 10

  1     sozinho — nada depende dele
  16    sozinho — o CPF nao entra no PodeCadastrar

  O 19 e o 20 sao IRMAOS, e nao um filho do outro: cada um refaz as
  mesmas checagens por conta propria. Sabotar o 19 nao pinta o 20.
```

**Esta tabela foi medida, não deduzida.** Plantei um defeito em cada um dos 20 métodos, um
de cada vez, e anotei quem ficou vermelho junto:

| Se errar | Ficam vermelhos junto |
|:--:|:--|
| **1** | ninguém |
| **2, 3 ou 4** | 5, 6, 19, 20 |
| **5** | 6, 19, 20 |
| **6** | 19, 20 |
| **7** | 10 |
| **8** | 9, 10 |
| **9** | 10 |
| **10** | ninguém |
| **11, 12, 13, 14, 15, 17 ou 18** | 19, 20 |
| **16** | ninguém |
| **19** | ninguém — o 20 refaz as checagens por conta própria |
| **20** | ninguém |

> **Diga isso em voz alta quando alguém travar no 19.** O vermelho dele quase nunca é dele:
> é de um método lá atrás que passou pelos próprios testes por sorte.

> **E o 1 e o 16 são a saída de emergência.** Ninguém depende deles, então quem empacar ali
> pode pular e voltar depois sem travar a turma. Mas a aba seguinte só destrava com a
> anterior fechada, então avise antes de deixar alguém pular.

### Nem todo defeito propaga — depende do dado

Vale a pena mostrar isto ao vivo, porque contraria a intuição da turma. Se você tirar
**um** ponto do `ForcaDaSenha` — apagar a linha que conta o dígito — só o **5 e o 6** ficam
vermelhos. O 19 e o 20 continuam verdes, porque as senhas do cadastro ainda alcançam força
2 pelos outros dois pontos.

O defeito está lá, os testes do 19 passam, e o sistema está errado. É a diferença entre
*teste que passa* e *código correto*, e é o argumento mais forte da noite para a pergunta 4.

---

## Placar esperado

| | |
|:--|:--|
| Desafios | 20 |
| Casos que o corretor roda | **92** |
| Métodos de teste que o aluno escreve | **19** — o do desafio 1 vem pronto, de molde |
| Fronteiras obrigatórias somadas | **56** |
| Testes do `acesso-final/` | **92 casos, 92 verdes** |
