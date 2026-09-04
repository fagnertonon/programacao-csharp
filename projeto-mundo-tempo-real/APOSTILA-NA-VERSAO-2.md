# A apostila serve para esta versão?

**Serve.** Os quatro arquivos que o aluno abre são **byte a byte idênticos** nas
duas versões:

| Arquivo | v1 e v2 |
|:--|:--|
| `Desafios.cs` (o esqueleto) | **idêntico** |
| `Desafios.cs` (o gabarito) | **idêntico** |
| `MinhaConfig.cs` | **idêntico** |
| `MeuMonstro.cs` | **idêntico** |

Ou seja: os oito capítulos de método da apostila — o código, o linha a linha, o
erro mais comum, o "como saber que funcionou" — valem sem uma vírgula de
mudança. Não regere o PDF.

O que segue são **três correções pequenas**, para você falar em voz alta na
largada da aula. Nenhuma delas muda o que o aluno escreve.

---

## 1 · A porta é 5200, não 5199

A apostila cita `5199` em dois lugares: em *"Como o jogo abre"* e na tabela
*"Se der problema"*. Nesta versão o endereço é:

`http://localhost:5200`

Na prática o aluno nem digita isso — o F5 abre o navegador sozinho. Só importa
se a linha *"a porta já está em uso"* aparecer: aí o número na mensagem será
5200.

> **As duas versões rodam ao mesmo tempo**, justamente porque as portas são
> diferentes. Se você quiser mostrar a diferença de ritmo lado a lado, dá.

---

## 2 · O mundo anda sozinho

A apostila não erra em nada aqui, mas ela foi escrita para um jogo onde **nada
acontece enquanto você não aperta uma tecla**. Nesta versão um relógio no
servidor chama os monstros a cada 550 ms.

Uma consequência prática para o aluno, e ela é boa:

**O desafio 8 pode ser testado sem sair do lugar.** Na v1 era preciso andar para
ver o monstro andar. Aqui basta ficar parado e olhar — se o `PerseguirJogador`
estiver certo, eles vêm.

E uma consequência para você: **um `while` sem passo no desafio 2 é mais
visível aqui**, porque o relógio continua batendo enquanto o método está preso.
O motor aguenta — o guarda de reentrância impede que os tiques se empilhem, e
depois de duas travadas o desafio para de ser chamado até o próximo F5.

---

## 3 · Existe vida, e os monstros machucam

Isto é **novo**, e não está na apostila.

| | |
|:--|:--|
| Vida cheia | 10 corações, no alto da tela |
| Encostar num monstro | tira 1 |
| Entre um golpe e outro | 900 ms de espera |
| Vida no zero | volta para a largada, **vida cheia de novo** |

**Perder não é morrer.** Não há tela de fim, não há placar, não se perde o que
foi construído: o mundo continua exatamente como estava, e os monstros ficam
onde estavam. É a última aula, não um jogo de sobrevivência.

Vale dizer isso à turma **antes** de alguém tomar o primeiro dano, senão a
primeira reação é achar que quebrou.

> A dica *"se tranque numa caixa de dois cubos"*, no fim da apostila, fica muito
> melhor aqui — na v1 era uma curiosidade, nesta versão é a única forma de
> descansar.

---

## O que **não** muda

Vale repetir, porque é o ponto todo desta variante:

- os oito métodos, com as mesmas assinaturas e os mesmos testes — **44 de 44**;
- a regra de ouro: o jogo só chama o método do aluno **depois que os testes passam**;
- o destravamento progressivo, um desafio de cada vez;
- o `MeuMonstro.cs` e o `MinhaConfig.cs`, com as mesmas cores e o mesmo desenho;
- as duas armadilhas explicadas na abertura: os testes que passam sem código
  escrito, e o `new Bloco(...)` que não compila.

Um aluno que resolveu os oito na v1 abre a v2, aperta F5 e joga — o
`Desafios.cs` dele funciona sem tocar em nada. **É literalmente o mesmo
arquivo.**
