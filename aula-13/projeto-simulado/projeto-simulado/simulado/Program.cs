using System;
using System.Windows.Forms;

namespace Conecta
{
    internal class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // O nome do aluno nao e mais perguntado numa caixa: ele e o
            // desafio 0, escrito no proprio Desafios.cs. Assim o aluno digita
            // uma vez so e o nome sobrevive a todo F5.
            Application.Run(new frmSimulado());
        }
    }
}
