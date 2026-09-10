using System;
using System.Windows.Forms;

namespace Acesso
{
    public class Programa
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmDesafios());
        }
    }
}
