using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Revisao
{
    // A mecanica de destravar, compartilhada pelas tres variantes:
    // a aba N so abre quando a N-1 estiver resolvida.
    //
    // Voce NAO precisa mexer aqui.
    public static class Destravadas
    {
        // Quantas abas estao liberadas: a primeira sempre, mais uma a cada
        // desafio resolvido em sequencia.
        public static int Contar(List<bool> resolvido, int total)
        {
            int liberadas = 1;

            for (int i = 0; i < resolvido.Count; i++)
            {
                if (resolvido[i]) { liberadas = i + 2; }
                else { break; }
            }

            return liberadas > total ? total : liberadas;
        }

        public static void BloquearSeTravada(TabControlCancelEventArgs e,
                                             List<bool> resolvido, Conteudo conteudo)
        {
            int destino = e.TabPageIndex;
            if (destino <= 0) { return; }

            if (!resolvido[destino - 1])
            {
                e.Cancel = true;

                Topico anterior = conteudo.Topicos[destino - 1];

                MessageBox.Show(
                    "Esta aba ainda esta travada.\r\n\r\n" +
                    "Resolva o desafio " + anterior.Numero + " (" + anterior.Titulo + ") "
                    + "para destravar.",
                    "Aba travada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // O lado do ensino fica com o que sobrar depois de reservar espaco para
        // o desafio. SplitterDistance so vale depois que o controle tem largura
        // de verdade - por isso isto roda no OnShown e no OnResize, nunca na
        // montagem.
        public static void AjustarDivisores(List<SplitContainer> divisores, int reservaDireita)
        {
            foreach (SplitContainer div in divisores)
            {
                if (div.Width < 400) { continue; }

                int desejado = div.Width - reservaDireita;
                if (desejado < 380) { desejado = 380; }

                int maximo = div.Width - div.Panel2MinSize - div.SplitterWidth - 1;
                if (desejado > maximo) { desejado = maximo; }
                if (desejado < div.Panel1MinSize) { continue; }

                try { div.SplitterDistance = desejado; }
                catch (InvalidOperationException) { /* janela pequena demais */ }
            }
        }
    }
}
