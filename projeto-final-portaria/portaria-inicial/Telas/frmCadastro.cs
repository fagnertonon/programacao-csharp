using System;
using System.Windows.Forms;

namespace Portaria
{
    /// <summary>
    /// Tela de criacao de conta. Abre por cima do login, com ShowDialog.
    ///
    /// Nesta tela voce escreve UM metodo: o TODO 6.
    /// </summary>
    public partial class frmCadastro : Form
    {
        public frmCadastro()
        {
            InitializeComponent();
        }

        /// <summary>
        /// TODO 6 - Cria a conta.            [Indicadores I3, I2 e I6]
        ///
        /// E o metodo mais longo da noite 1, e o mais repetitivo: sao
        /// CINCO recusas no mesmo molde da Aula 15, e depois a gravacao.
        ///
        /// O MOLDE DE CADA RECUSA:
        ///     if (alguma coisa esta errada)
        ///     {
        ///         MessageBox.Show("o aviso", "Atencao",
        ///             MessageBoxButtons.OK, MessageBoxIcon.Warning);
        ///         return;
        ///     }
        /// O return e o que impede o resto do metodo de rodar.
        ///
        /// AS CINCO RECUSAS, NESTA ORDEM:
        ///
        ///   M1 - algum campo em branco
        ///        txtNome.Text == "" || txtUsuario.Text == ""
        ///                           || txtSenha.Text == ""
        ///
        ///   M2 - login invalido
        ///        Regras.ValidarLogin(txtUsuario.Text) == false
        ///
        ///   M3 - senha curta
        ///        txtSenha.Text.Length < Regras.TAMANHO_MINIMO_SENHA
        ///
        ///   M4 - senhas diferentes
        ///        txtSenha.Text != txtConfirmar.Text
        ///
        ///   M5 - login ja em uso
        ///        UsuarioDAO.LoginExiste(txtUsuario.Text) == true
        ///
        /// A ORDEM IMPORTA: o M5 vai ao banco, e os quatro primeiros nao.
        /// Deixe ele por ultimo para nao consultar o banco a toa.
        ///
        /// PASSANDO POR TUDO, o que foi digitado vira OBJETO:
        ///
        ///     Usuario novo = new Usuario();
        ///     novo.Nome  = txtNome.Text;
        ///     novo.Login = txtUsuario.Text;
        ///     novo.Senha = txtSenha.Text;
        ///     UsuarioDAO.CriarConta(novo);
        ///
        /// e depois um MessageBox de sucesso e  this.Close();
        ///
        /// AS CINCO MENSAGENS VAO PARA O MANUAL, na seccao "Mensagens do
        /// sistema". Escreva textos que um usuario comum entenda - e nao
        /// mude a redacao depois, porque o manual vai copiar daqui.
        /// </summary>
        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("TODO 6 ainda nao foi escrito.");   // <<< APAGUE esta linha
        }

        /// <summary>JA VEM PRONTO.</summary>
        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
