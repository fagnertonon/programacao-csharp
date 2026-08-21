using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Conecta
{
    // A mecanica de destravar: a aba N so abre quando a N-1 estiver resolvida.
    //
    // Um topico marcado "livre" no JSON nasce aberto e nao trava quem vem
    // depois dele. Serve para liberar o material de quem quer ir na frente.
    //
    // ATENCAO de manutencao: sao DOIS guardioes. Contar() decide o que a aba
    // mostra, e BloquearSeTravada() decide se o clique passa. Mexer so num
    // deixa a aba pintada como liberada e o clique cancelado - o pior estado
    // possivel. Por isso os dois usam a MESMA regra, e o segundo chama o
    // primeiro em vez de olhar o vizinho por conta propria.
    //
    // Voce NAO precisa mexer aqui.
    public static class Destravadas
    {
        public static int Contar(List<bool> resolvido, List<bool> livre, int total)
        {
            int liberadas = 1;

            for (int i = 0; i < resolvido.Count; i++)
            {
                bool passa = resolvido[i] || (livre != null && i < livre.Count && livre[i]);
                if (passa) { liberadas = i + 2; }
                else { break; }
            }

            return liberadas > total ? total : liberadas;
        }

        public static void BloquearSeTravada(TabControlCancelEventArgs e,
                                             List<bool> resolvido, List<bool> livre,
                                             Conteudo conteudo)
        {
            int destino = e.TabPageIndex;
            if (destino <= 0) { return; }

            // A MESMA regra da pintura.
            if (destino < Contar(resolvido, livre, conteudo.Topicos.Count)) { return; }

            e.Cancel = true;

            Topico anterior = conteudo.Topicos[destino - 1];

            MessageBox.Show(
                "Esta aba ainda esta travada.\r\n\r\n" +
                "Resolva o desafio " + anterior.Numero + " (" + anterior.Titulo + ") "
                + "para destravar.",
                "Aba travada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // O lado do ensino fica com o que sobrar depois de reservar espaco para
        // o desafio. SplitterDistance so vale depois que o controle tem largura
        // de verdade - por isso isto roda no OnShown e no OnResize, nunca na
        // montagem.
        // A divisao e PROPORCIONAL, e nao por reserva fixa. Com a janela em tela
        // cheia, uma reserva fixa deixava um dos lados enorme e o outro do
        // tamanho de sempre; a fracao faz os dois crescerem juntos, e o que
        // sobrar em cada painel vira margem centralizada, que ninguem enxerga
        // porque tem a cor do proprio painel.
        public static void AjustarDivisores(List<SplitContainer> divisores,
                                            double fracaoEsquerda,
                                            int minEsquerda, int minDireita)
        {
            foreach (SplitContainer div in divisores)
            {
                if (div.Width < 400) { continue; }

                int desejado = (int)((div.Width - div.SplitterWidth) * fracaoEsquerda);
                if (desejado < minEsquerda) { desejado = minEsquerda; }

                int maximo = div.Width - minDireita - div.SplitterWidth - 1;
                if (desejado > maximo) { desejado = maximo; }
                if (desejado < div.Panel1MinSize) { continue; }

                try { div.SplitterDistance = desejado; }
                catch (InvalidOperationException) { /* janela pequena demais */ }
            }
        }
    }
}
