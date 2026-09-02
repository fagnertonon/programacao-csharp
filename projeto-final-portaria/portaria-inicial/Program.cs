using System;
using System.Windows.Forms;

namespace Portaria
{
    /// <summary>
    /// Porta de entrada do programa. Encanamento - nao se mexe aqui.
    /// A primeira tela do sistema e o login.
    /// </summary>
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin());
        }
    }
}
