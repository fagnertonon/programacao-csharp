using System;
using System.Windows.Forms;

namespace Portaria
{
    /// <summary>
    /// Tela de entrada da Portaria. E a primeira que aparece.
    ///
    /// Nesta tela voce escreve DOIS metodos: o TODO 1 e o TODO 9.
    /// </summary>
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// JA VEM PRONTO.
        ///
        /// Shown roda DEPOIS de a janela aparecer - diferente do Load.
        /// A checagem do banco fica aqui de proposito: no Load, um erro
        /// de conexao poria a caixa de mensagem na frente de um
        /// retangulo cinza, antes de a tela ter sido desenhada.
        ///
        /// Leia este metodo antes de escrever o TODO 1: ele ja mostra
        /// como se chama o TestarConexao.
        /// </summary>
        private void frmLogin_Shown(object sender, EventArgs e)
        {
            string erro;

            if (Conexao.TestarConexao(out erro))
            {
                lblStatus.Text = "Banco conectado: portariadb";
                lblStatus.ForeColor = System.Drawing.Color.DarkGreen;
            }
            else
            {
                lblStatus.Text = "SEM BANCO - clique em Testar conexao";
                lblStatus.ForeColor = System.Drawing.Color.Firebrick;
            }
        }

        /// <summary>
        /// TODO 1 - Testa a conexao com o banco e mostra o resultado.
        ///                                        [Indicadores I1 e I4]
        ///
        /// Este e o PRIMEIRO metodo da noite. Enquanto ele nao acender
        /// verde, nao adianta escrever mais nada: nenhum outro TODO
        /// funciona com o banco fora do ar.
        ///
        /// Ele existe para voce nao precisar fechar e abrir o programa
        /// toda vez que arrumar a senha no Conexao.cs.
        ///
        /// O que fazer:
        ///   1. declare  string erro;
        ///   2. if (Conexao.TestarConexao(out erro))
        ///        - lblStatus.Text = "Banco conectado: portariadb";
        ///        - lblStatus.ForeColor = System.Drawing.Color.DarkGreen;
        ///        - MessageBox avisando que conectou
        ///   3. else
        ///        - lblStatus.Text = "SEM BANCO";
        ///        - lblStatus.ForeColor = System.Drawing.Color.Firebrick;
        ///        - MessageBox mostrando a variavel erro
        ///
        /// O metodo logo acima ja faz quase isso - copie o desenho.
        ///
        /// DEPOIS DE ESCREVER, provoque os tres erros do laboratorio e
        /// anote a mensagem de cada um: pare o servico MySQL80, erre a
        /// senha no Conexao.cs, e troque o nome do banco. E isso que vai
        /// para o Registro de Depuracao.
        /// </summary>
        private void btnTestarConexao_Click(object sender, EventArgs e)
        {
            MessageBox.Show("TODO 1 ainda nao foi escrito.");   // <<< APAGUE esta linha
        }

        /// <summary>
        /// TODO 9 - Entra no sistema.        [Indicadores I3, I4 e I6]
        ///
        /// Este metodo tem TRES partes:
        ///
        ///  1. RECUSAR campos vazios:
        ///       if (txtUsuario.Text == "" || txtSenha.Text == "")
        ///     mostre a mensagem M6 e  return;
        ///
        ///  2. CHAMAR o Autenticar:
        ///       Usuario achado = UsuarioDAO.Autenticar(txtUsuario.Text,
        ///                                              txtSenha.Text);
        ///
        ///     O Autenticar devolve NULL quando nao acha ninguem.
        ///     Conferir isso ANTES de usar o objeto e o que impede o
        ///     programa de quebrar aqui - e a razao de o TODO 8 ter que
        ///     devolver null de verdade.
        ///
        ///       if (achado == null)  ->  mensagem M7, limpa a senha,
        ///                                txtSenha.Focus() e  return;
        ///
        ///  3. ENTRAR:
        ///       Sessao.UsuarioLogado = achado;
        ///       this.Hide();
        ///       frmPrincipal tela = new frmPrincipal();
        ///       tela.ShowDialog();
        ///       this.Close();
        ///
        ///     O Hide esconde o login enquanto a principal esta aberta,
        ///     e o Close fecha tudo quando ela e fechada.
        ///
        /// AS DUAS MENSAGENS VAO PARA O MANUAL:
        ///   M6 - "Informe o usuario e a senha."
        ///   M7 - "Usuario ou senha incorretos."
        /// </summary>
        private void btnEntrar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("TODO 9 ainda nao foi escrito.");   // <<< APAGUE esta linha
        }

        /// <summary>JA VEM PRONTO.</summary>
        private void btnCriarConta_Click(object sender, EventArgs e)
        {
            frmCadastro tela = new frmCadastro();
            tela.ShowDialog();
        }
    }
}
