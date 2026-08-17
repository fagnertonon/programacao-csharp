# Programação C# — Senac

Repositório de **material de aula** do curso de Programação em C#.

Aqui fica **apenas o que é usado na aula do dia**: a apostila do aluno e o
projeto inicial, para você baixar no começo da aula e trabalhar a partir dele.

> **Não é o repositório completo do curso.** É um ponto de partida limpo,
> publicado aula a aula.

---

## Como baixar o material

### Opção 1 — sem Git (mais simples)

1. Clique no botão verde **`Code`** aqui no GitHub
2. **`Download ZIP`**
3. Extraia em uma pasta sua (ex.: `Documentos\csharp`)

### Opção 2 — com Git

```bash
git clone https://github.com/<usuario>/programacao-csharp.git
```

Para pegar as aulas novas depois:

```bash
git pull
```

---

## Como abrir o projeto da aula

1. Entre na pasta da aula (ex.: `aula-10/duelo-inicial/`)
2. Dê **duplo clique** no arquivo `.csproj` — o Visual Studio abre
3. Aperte **F5**

Se o projeto rodar e aparecer uma frase na tela, está tudo certo para começar.

Pelo terminal, se preferir:

```bash
dotnet run
```

---

## Aulas publicadas

| Aula | Data | Assunto | Pasta |
|:--:|:--|:--|:--|
| 10 | 17/08/2026 | **Duelo** — o primeiro laço de repetição (`for`) | [`aula-10/`](aula-10/) |

---

## Regras da casa

- **Trabalhe numa cópia sua.** Antes de mexer, copie a pasta da aula para
  outro lugar. Assim, se algo quebrar, o original continua limpo.
- **Você digita o código.** O projeto vem propositalmente vazio: o objetivo
  da aula é escrever o programa do zero, não preencher lacunas.
- **Não existe gabarito aqui.** A resposta pronta não é publicada — ela
  atrapalha o aprendizado. O que vale é o programa que *você* fez rodar.

---

## Requisitos

- **Visual Studio 2022** (ou VS Code + SDK do .NET)
- **.NET 8.0**

Os projetos são de **console**, sem banco de dados, sem pacotes NuGet e sem
internet — rodam em qualquer máquina do laboratório.
