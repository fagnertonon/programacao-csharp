# O modo fácil — Portugol à esquerda, C# à direita

**O segundo caminho pela mesma noite.** À esquerda, o algoritmo em Portugol —
o mesmo que está na folha B, na mão do aluno. À direita, o C# com **lacunas
numeradas**: ele escolhe a peça que falta.

| | |
|:--|:--|
| **Projeto** | `Desafio.csproj` → `DesafioMural.exe` |
| **Conteúdo** | [`../conteudo/passos-mural.json`](../conteudo/passos-mural.json) — **a mesma fonte da folha B** |
| **Rodada 1** | **9 passos** — o C# com lacunas, 47 no total |
| **Rodada 2** | **10 perguntas** — sobre método, parâmetro e retorno |
| **E depois** | Os **10 desafios de código** ficam em [`../projeto-desafios-codigo/`](../projeto-desafios-codigo/README.md), em arquivo, com corretor que executa |

**A esquerda é sempre o Portugol.** É ele que orienta as duas rodadas — e é o
mesmo que está na folha, na mão do aluno.

---

## As duas rodadas

| | O que o aluno faz | Como é conferido |
|:--|:--|:--|
| **1 · Os 9 passos** | Escolhe a peça que falta em cada lacuna | Resposta exata por lacuna |
| **2 · As 10 perguntas** | Escolhe entre quatro, olhando o Portugol | Resposta exata |

**O código se preenche na frente do aluno.** Enquanto ele escolhe, o `[1]` some
e a peça entra no lugar — e a caixa da direita vai virando C# de verdade. Depois
do Conferir, cada peça fica verde ou vermelha, no meio do próprio código.

> ### As duas colunas não podem ser copiadas
>
> Nenhuma das duas é caixa de texto: são painéis que **desenham** o código. Não
> há seleção, não há `Ctrl+C`, não há menu de contexto.
>
> Aqui o motivo é ainda mais direto que no projeto dos desafios: **depois de
> acertar as lacunas, a coluna da direita fica com o método inteiro**, pronto
> para colar no `mural-inicial`. O aluno acertaria as peças e nunca teria
> escrito o método.
>
> **O que ainda dá para ver, e é o preço de conferir sem compilador:** este app
> precisa das respostas para saber se a escolha está certa, então o
> `passos-mural.json` que fica ao lado do executável **tem as respostas dentro**.
> Um aluno que abrir aquele arquivo no Bloco de Notas as encontra. É a mesma
> troca que o [README da Aula 11](../../aula-11/projeto-revisao/README.md) já
> documenta — e a essa altura ele teria aprendido mais resolvendo.
>
> **No projeto dos desafios de código isso não acontece**: lá o arquivo que
> acompanha o executável não tem resposta nenhuma.

### E quem quiser escrever, e não escolher?

Vai para o [`projeto-desafios-codigo/`](../projeto-desafios-codigo/README.md):
**10 métodos escritos no `Desafios.cs`**, com um corretor que **executa** o que
o aluno escreveu. Este app aqui é o degrau de baixo — o de escolher a peça
certa.

---

## Para quem é

**Não é para a turma inteira.** É o caminho de quem trava na digitação e
perde a noite antes de chegar ao assunto.

| Aluno | Caminho |
|:--|:--|
| Está acompanhando | O `mural-inicial` no Visual Studio, com a folha B do lado. **Escreve a assinatura inteira** |
| Trava na digitação, ou chegou atrasado | **Rodadas 1 e 2 deste app.** Escolhe as peças, entende o método, e depois digita no projeto |
| Fechou as duas rodadas | Os [desafios em arquivo](../projeto-desafios-codigo/README.md), onde ele escreve o método — é a ponte de volta para o teclado |
| Terminou antes de todo mundo | Volta ao `mural-inicial` e adianta a Atividade 2 |

O app **não substitui** o projeto — ele destrava. Quando o aluno fecha um
passo aqui, a própria tela diz onde escrever aquilo de verdade: *"o TODO 4
está em MuralDAO.cs"*.

---

## Por que ele é mais fácil, exatamente

Três degraus a menos que o papel, e nenhum deles é o assunto da noite:

| No papel | No app |
|:--|:--|
| A ficha vem **em branco** — o aluno preenche | A ficha vem **preenchida**, à vista |
| Ele escreve a assinatura inteira, à mão | Ele **escolhe** o tipo de retorno e o parâmetro |
| Ele escreve o corpo inteiro | O corpo já está lá, com **lacunas** nos pontos que decidem |

**O que NÃO ficou mais fácil é o que a noite cobra:** em todo passo, as
lacunas caem exatamente sobre *o que o método devolve*, *o que ele recebe* e a
palavra que decide isso — `void`, `return`, `static`, `switch`, `foreach`.

---

## O que ele não faz, dito na cara

> **Este app não compila código nenhum.** Ele confere lacuna por lacuna, contra
> a resposta certa.

Isso é diferente — e mais honesto — do que a Variante B da Aula 11 fazia. Lá o
app conferia se as *peças* apareciam no texto digitado, e **um código com as
peças certas e errado no miolo passava**. Aqui cada lacuna tem uma resposta
exata.

Mas escolher não é escrever. **Quem precisa provar que sabe escrever vai para o
[`projeto-desafios-codigo/`](../projeto-desafios-codigo/README.md)**, onde o
corretor executa o método de verdade. E o programa da noite continua sendo o
`mural-inicial`, que abre no Visual Studio e roda com F5.

---

## A fonte única

Não existe cópia do algoritmo. O caminho vai numa direção só:

```
conteudo/portugol_dos_passos.py     o Portugol, escrito uma vez
            +
conteudo/montar-conteudo.py         a coluna da direita: C#, lacunas, dicas
            |
            v
conteudo/passos-mural.json          a fonte unica
            |
            +---> folhas/gerar_folhas.py     a folha B impressa
            +---> projeto-desafio/           este app
```

**Mudou o algoritmo? Mude em `portugol_dos_passos.py`** e rode, nesta ordem:

```bash
cd conteudo && python montar-conteudo.py && cd ../folhas && python gerar_folhas.py
```

O papel e a tela mudam juntos, porque leem a mesma coisa.

---

## Como a tela funciona

| Onde | O quê |
|:--|:--|
| **Barra roxa, no alto** | As duas rodadas. Trocar de rodada não perde o que já foi resolvido |
| **Trilha, abaixo dela** | Os itens da rodada. **Todos liberados** — empacou num, pula e volta. Verde = resolvido |
| **Esquerda** | O Portugol, sempre. No passo 9 é o C# em memória, porque **o VisuAlg não tem banco** |
| **Direita, no alto** | A ficha preenchida (rodada 1) ou o enunciado (rodadas 2 e 3) |
| **Direita, embaixo** | Os combos (rodada 1) ou as quatro alternativas (rodada 2) |
| **Conferir** | Marca o que errou e mostra a **dica** — nunca a resposta |

**A ordem das opções é fixa**, calculada a partir do número do passo e da
lacuna. Nada de sorteio: duas máquinas mostrando ordens diferentes viram
discussão na sala em vez de aula.

> **Todos liberados é decisão da Aula 13**, e está no registro: *"os exercícios
> permaneceram todos liberados desde o início, de modo que a dificuldade em um
> deles não impedisse o acesso aos demais."*

---

## Antes de usar

| # | |
|:--:|:--|
| 1 | **Copiar a pasta `projeto-desafio/` junto com o `mural-inicial/`** — ou pelo menos deixá-la no pendrive |
| 2 | O `passos-mural.json` é copiado para o lado do executável na compilação. **Não apague** |
| 3 | Rodar o [`conferir-material.py`](../conferir-material.py) se você mexeu em qualquer conteúdo |

> ✅ **Verificado nesta máquina.** Compila com 0 erro e 0 aviso, o executável
> abre com o JSON carregado, e o conferidor passa nas **sete** checagens — entre
> elas a mais forte: **cada linha das lacunas, preenchida com a resposta certa,
> existe no gabarito**. Um erro plantado de propósito no passo 7 foi pego.
>
> O autoteste roda sozinho:
>
> ```bash
> DesafioMural.exe --autoteste
> ```
>
> Ele prova a coisa que já quebrou uma vez: que **o código da direita vira C# de
> verdade** quando as peças certas entram — nenhum `[n]` sobra, e as chaves do
> próprio C#, como no `finally { con.Close(); }`, não são comidas como se fossem
> lacuna. São 141 casos. Grava em `autoteste.txt` e devolve 1 se algo falhar.
>
> ⚠️ **O que NÃO foi testado:** a aparência da janela, que é montada em código
> e não pôde ser vista nesta máquina. **Abra uma vez antes da aula.**
