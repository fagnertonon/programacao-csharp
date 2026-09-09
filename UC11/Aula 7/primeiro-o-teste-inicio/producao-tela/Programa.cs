using System;
using System.Windows.Forms;

namespace PrimeiroOTeste
{
    /// <summary>
    /// O ponto de partida da JANELA. UC11, Aula 7.
    /// </summary>
    public class Programa
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TelaPrimeiroOTeste());
        }
    }
}
