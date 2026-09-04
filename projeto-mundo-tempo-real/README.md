# Mundo de Cubos — versão em TEMPO REAL

**A segunda versão. Mesmo jogo, mesmos 8 métodos — mas aqui os monstros não
esperam a sua vez.**

> Esta pasta é uma **variante** do [`projeto-mundo-cubos`](../projeto-mundo-cubos/README.md).
> Os oito desafios são idênticos, a apostila serve para as duas, e o aluno não
> precisa aprender nada a mais. O que muda é o **motor**.
>
> **A apostila do aluno é a mesma** — o `Desafios.cs` das duas versões é byte a
> byte o mesmo arquivo. As três diferenças que valem ser ditas em voz alta estão
> em [`APOSTILA-NA-VERSAO-2.md`](APOSTILA-NA-VERSAO-2.md).

---

## O que muda, e por quê

| | Versão 1 (por turno) | Versão 2 (tempo real) |
|:--|:--|:--|
| Quando os monstros andam | quando **você** anda | **sozinhos**, a cada 550 ms |
| Encostar num monstro | conta e avisa | **tira vida** |
| Se a vida acaba | não existe vida | volta para a largada, vida cheia |
| Como a tela se atualiza | na resposta de cada ação | perguntando `/api/estado` 5x por segundo |
| Porta | 5199 | **5200** |

As duas podem rodar ao mesmo tempo, em portas diferentes.

---

## As três peças novas

**1. O relógio.** Um `System.Threading.Timer` no `Motor` chama o turno dos
monstros a cada `RITMO_MS`, quer o jogador se mexa ou não. Ele roda numa thread
do pool, e por isso **tudo que ele toca está dentro do mesmo `lock`** do resto
do motor — senão dois turnos poderiam mexer no mundo ao mesmo tempo.

**2. A vida.** Um bicho que anda sozinho e não machuca não assusta ninguém.
Encostar tira um coração, com uma **espera de 900 ms** entre um golpe e outro:
sem ela, quatro monstros colados esvaziariam a vida em dois segundos e o jogo
seria injusto.

Perder a vida **não é morrer**: você volta para a largada com a vida cheia, e os
monstros continuam de onde estavam. É a última aula, não um jogo de
sobrevivência.

**3. O laço do navegador.** Na versão 1 a tela só mudava quando o jogador
apertava alguma coisa, então a resposta da ação já trazia tudo. Aqui a tela
pergunta `/api/estado` a cada **180 ms** — três vezes por batida do relógio, o
suficiente para nenhum passo passar despercebido sem inundar o servidor.

A resposta é pequena de propósito: jogador, monstros, vida e o que mudou no
mundo. Nunca os 11.520 cubos.

---

## O que o aluno escreve

**Exatamente os mesmos oito métodos.** Nenhuma linha do `Desafios.cs` muda entre
as duas versões, e é isso que faz esta variante valer a pena: o mesmo código do
aluno, com um motor diferente por baixo, vira um jogo com outro ritmo.

O `PerseguirJogador` do desafio 8 fica bem mais interessante aqui — ele é
chamado pelo relógio, não pela sua tecla.

---

## Estado de verificação

| | |
|:--|:--|
| Os dois projetos compilam | **0 erro** |
| Gabarito contra os testes | **44 de 44** |
| Monstros andam sem o jogador tocar em nada | **medido**: 8 leituras em 10 s |
| Encostar tira vida | **medido**: 10 → 9 → 8 → 6 → 5 |
| Voltar para a largada ao zerar | visto na tela |
| Console do navegador | **sem erros** |

> **Testado com menos profundidade que a versão 1**, por causa do prazo. O que
> está na tabela acima foi medido; o resto herda a versão 1, que passou por
> bateria completa. Antes de levar para a sala, rode uma partida inteira.

---

## Rodar

Abra `mundo-inicial/MundoDeCubos.csproj` e **F5**. O navegador abre em
`http://localhost:5200`.
