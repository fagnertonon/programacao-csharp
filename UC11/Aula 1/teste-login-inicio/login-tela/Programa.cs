using System;
using System.Windows.Forms;

namespace SistemaLogin
{
    /// <summary>
    /// O ponto de partida do programa. Tres linhas, e nenhuma regra.
    /// </summary>
    public class Programa
    {
        // [STAThread] e obrigatorio em Windows Forms. Sem ele a janela
        // abre, mas caixas de dialogo e area de transferencia falham.
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TelaDeCadastro());
        }
    }
}
