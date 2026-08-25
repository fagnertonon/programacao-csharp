using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Conecta
{
    internal class Program
    {
        [STAThread]
        static int Main(string[] args)
        {
            // ---------------------------------------------------------
            // DesafioMural.exe --autoteste
            //
            // Confere o conteudo do aplicativo e grava o resultado em
            // autoteste.txt, ao lado do executavel. Devolve 0 se tudo
            // passou e 1 se algo falhou.
            //
            // O que ele prova: que o codigo da direita VIRA C# quando as
            // pecas certas entram - nenhum [n] sobra, e as chaves do
            // proprio C# nao foram comidas como se fossem lacuna. Foi
            // exatamente isso que quebrou uma vez.
            // ---------------------------------------------------------
            if (args != null && Array.IndexOf(args, "--autoteste") >= 0)
            {
                return Autoteste();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmDesafio());
            return 0;
        }

        private static int Autoteste()
        {
            StringBuilder saida = new StringBuilder();
            int falhas = 0;
            int casos = 0;

            try
            {
                Conteudo c = Conteudo.Carregar();

                foreach (Pergunta p in c.Perguntas)
                {
                    casos++;
                    if (!p.Opcoes.Contains(p.Resposta))
                    {
                        falhas++;
                        saida.AppendLine("FALHOU  pergunta " + p.N +
                            ": a resposta nao esta entre as opcoes");
                    }
                }

                // O codigo da direita tem de VIRAR C# quando as pecas certas
                // entram: nenhum [n] pode sobrar, e as chaves do proprio C#
                // nao podem ter sido comidas como se fossem lacuna.
                foreach (Passo passo in c.Passos)
                {
                    Dictionary<int, string> certas = new Dictionary<int, string>();
                    foreach (Lacuna l in passo.Lacunas) certas[l.N] = l.Resposta;

                    foreach (string linha in passo.CSharp)
                    {
                        casos++;
                        string cheia = Codigo.Preencher(linha, certas);

                        if (cheia.Contains("[") && cheia.Contains("]") &&
                            Regex.IsMatch(cheia, @"\[\d+\]"))
                        {
                            falhas++;
                            saida.AppendLine("FALHOU  passo " + passo.N +
                                ": sobrou lacuna depois de preencher -> " + cheia);
                        }

                        if (cheia.Contains("{") &&
                            Regex.IsMatch(cheia, @"\{\d+\}"))
                        {
                            falhas++;
                            saida.AppendLine("FALHOU  passo " + passo.N +
                                ": marcador nao foi substituido -> " + cheia);
                        }
                    }
                }

                saida.AppendLine();
                saida.AppendLine(falhas == 0
                    ? ("TUDO PASSOU - " + casos + " casos")
                    : (falhas + " FALHA(S) em " + casos + " casos"));
            }
            catch (Exception ex)
            {
                falhas++;
                saida.AppendLine("EXPLODIU: " + ex.Message);
            }

            File.WriteAllText(
                Path.Combine(AppContext.BaseDirectory, "autoteste.txt"),
                saida.ToString());

            return falhas == 0 ? 0 : 1;
        }
    }
}
