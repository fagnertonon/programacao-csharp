# Simulado da prova — Aula 13

Onze abas: o **desafio 0** e o seu nome, e os dez seguintes sao a prova de amanha ensaiada.
**As abas estao todas abertas**: empacou numa, pule e volte.

## Como trabalhar

1. Abra o **`simulado/SimuladoA.csproj`** com um duplo clique.
2. Rode com **F5**.
   O aplicativo abre na aba **0. Quem e voce** - escreva o seu nome completo ali, no
   codigo, e nao numa caixa de texto. Voce faz isso **uma vez**: o que esta no codigo
   sobrevive a todo F5.
3. Leia a aba, escreva o metodo em **`simulado/Desafios.cs`**, **salve** (Ctrl+S).
4. **Feche o programa** e rode de novo com **F5**.

> **O passo 4 nao e opcional.** O seu codigo e compilado quando voce roda. Com o programa
> aberto, editar o arquivo nao muda nada na tela.

## O botao Enviar

Quando terminar - ou quando o professor pedir - clique em **Enviar as minhas respostas**,
no alto a direita.

Ele faz duas coisas: grava um arquivo nesta maquina, **sempre**, e - se o professor tiver
configurado o servidor - manda tambem para la. Se a rede falhar, ou se o servidor nao
estiver configurado, o arquivo continua aqui e ninguem perde nada. A tela avisa o que
aconteceu, e onde o arquivo ficou.

**Pode clicar quantas vezes quiser.** Cada envio e uma linha nova - reenviar nao apaga o
anterior.

## Se travar

- O quadro vermelho mostra **o que era esperado e o que o seu codigo devolveu**.
- O contador "3 de 6 testes passando" so vale quando chega a 6: um metodo vazio devolve o
  valor padrao do tipo e ja acerta algum teste por acidente.
- **Ver a dica** existe em todo desafio. Amanha, na prova, ele nao vai existir.

## O que voce NAO precisa abrir

`comum/`, `libs/`, `Corretor.cs`, `frmSimulado.cs` - sao o motor. Voce trabalha em **um
arquivo so**: `simulado/Desafios.cs`.
