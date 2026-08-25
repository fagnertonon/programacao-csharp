using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Conecta
{
    internal class Program
    {
        [STAThread]
        static int Main(string[] args)
        {
            // ---------------------------------------------------------
            // --autoteste
            //
            // Roda os dez desafios sem abrir janela e grava o resultado em
            // autoteste.txt, ao lado do executavel.
            //
            // No GABARITO ele tem de devolver 0: todos os testes passando.
            // No INICIAL, com os TODO vazios, ele tem de devolver 1 - se
            // devolvesse 0, seria sinal de que algum teste passa sem o
            // aluno escrever nada, e um teste desses nao vale nada.
            // ---------------------------------------------------------
            if (args != null && Array.IndexOf(args, "--autoteste") >= 0)
            {
                return Autoteste();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmDesafios());
            return 0;
        }

        private static int Autoteste()
        {
            StringBuilder saida = new StringBuilder();
            int totalTestes = 0;
            int totalPassaram = 0;
            int fechados = 0;

            try
            {
                Conteudo c = Conteudo.Carregar();

                foreach (Desafio d in c.Desafios)
                {
                    List<Resultado> testes = Corretor.Conferir(d.N);
                    int passaram = 0;

                    foreach (Resultado r in testes)
                    {
                        if (r.Passou) passaram++;
                    }

                    totalTestes += testes.Count;
                    totalPassaram += passaram;
                    if (testes.Count > 0 && passaram == testes.Count) fechados++;

                    saida.AppendLine("desafio " + d.N + ": " + passaram + "/" +
                                     testes.Count + "  " + d.Titulo);

                    foreach (Resultado r in testes)
                    {
                        if (!r.Passou)
                        {
                            saida.AppendLine("    x " + r.Descricao +
                                "  | esperado: " + r.Esperado +
                                "  | obtido: " + r.Obtido);
                        }
                    }
                }

                saida.AppendLine();
                saida.AppendLine(totalPassaram + " de " + totalTestes +
                                 " testes passaram, " + fechados + " de " +
                                 c.Desafios.Count + " desafios fechados");
            }
            catch (Exception ex)
            {
                saida.AppendLine("EXPLODIU: " + ex.Message);
                totalPassaram = -1;
            }

            File.WriteAllText(
                Path.Combine(AppContext.BaseDirectory, "autoteste.txt"),
                saida.ToString());

            return (totalTestes > 0 && totalPassaram == totalTestes) ? 0 : 1;
        }
    }
}
