using System;
using System.Windows.Forms;

namespace Conecta
{
    /// <summary>
    /// Conecta - tela de criar conta - O EXTRA DA NOITE.
    ///
    /// Faca esta tela SO DEPOIS que o login estiver de pe. Ela nao entra
    /// no roteiro do professor: e para quem terminar antes.
    ///
    /// A tela ja esta montada no Designer e o projeto ja compila. Sao 5
    /// lacunas, em ordem.
    ///
    /// CONTROLES (ja montados no Designer):
    ///   txtNome, txtUsuario, txtSenha ..... TextBox
    ///   btnSalvar, btnVoltar .............. Button
    /// </summary>
    public partial class frmCadastro : Form
    {
        public frmCadastro()
        {
            InitializeComponent();
        }

        // JA PRONTO.
        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            // TODO 1 - as tres recusas, todas no mesmo molde:
            //
            //     if (alguma coisa errada)
            //     {
            //         Avisar("a mensagem");
            //         return;
            //     }
            //
            //   a) algum dos tres campos vazio
            //   b) o usuario tem espaco no meio:
            //      txtUsuario.Text.Contains(" ")
            //   c) a senha tem menos de 4 caracteres
            //
            //   O return e o que impede o resto do metodo de rodar. Sem
            //   ele, o programa avisa que esta errado e grava assim mesmo.

            // TODO 2 - pergunte se o login ja esta em uso:
            //
            //     if (UsuarioDAO.LoginExiste(txtUsuario.Text) == true)
            //     {
            //         Avisar("Ja existe uma conta com esse usuario.");
            //         return;
            //     }
            //
            //   Repare de novo: quem sabe responder isso e o DAO, nao a
            //   tela. A tela pergunta.

            // TODO 3 - monte a conta e mande gravar:
            //
            //     Usuario novo = new Usuario();
            //     novo.Nome  = txtNome.Text;
            //     novo.Login = txtUsuario.Text;
            //     novo.Senha = txtSenha.Text;
            //
            //     UsuarioDAO.CriarConta(novo);
            //
            //     Avisar("Conta criada. Agora entre com ela.");
            //     this.Close();
        }

        private void Avisar(string mensagem)
        {
            // TODO 4 - escreva o corpo, igual ao Avisar do frmLogin:
            //
            //     MessageBox.Show(mensagem, "Conecta",
            //         MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //
            //   Repare que voce NAO precisou escrever esse MessageBox de
            //   quatro argumentos quatro vezes ali em cima, porque este
            //   metodo ja estava aqui esperando.
            //
            //   No frmLogin ele nasceu do jeito contrario: primeiro tres
            //   MessageBox iguais, depois a extracao. Compare os dois
            //   Avisar - faz sentido cada tela ter o seu?
        }

        // TODO 5 - nao ha nada a fazer aqui, e essa e a licao.
        //
        //   Quando o UsuarioDAO passar a falar com o MySQL, no Bloco 6,
        //   volte neste arquivo e confira: NENHUMA linha dele muda.
        //
        //   E a prova de que a tela deixou de saber de onde vem o dado.
        //   Ela chama CriarConta e LoginExiste pelo NOME; se o corpo passou
        //   a ser um INSERT ou continua sendo uma List, nao e problema
        //   dela.
    }
}
