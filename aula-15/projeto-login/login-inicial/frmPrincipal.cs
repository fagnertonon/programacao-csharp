using System;
using System.Windows.Forms;

namespace Conecta
{
    /// <summary>
    /// Conecta - a tela que abre DEPOIS de entrar.
    ///
    /// Uma lacuna so, no Bloco 5.
    ///
    /// CONTROLES (ja montados no Designer):
    ///   lblBemVindo, lblAviso ..... Label
    ///   btnSair ................... Button
    /// </summary>
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();

            // TODO 1 - esta tela nao recebe nada por parametro, e mesmo
            //          assim precisa saber quem entrou. Pergunte para a
            //          Sessao - a classe que voce cria no Bloco 5:
            //
            //     if (Sessao.TemUsuarioLogado())
            //     {
            //         lblBemVindo.Text = "Bem-vindo, "
            //                          + Sessao.UsuarioLogado.Nome + "!";
            //
            //         lblAviso.Text = "Voce entrou como "
            //                       + Sessao.UsuarioLogado.Login
            //                       + " (conta numero "
            //                       + Sessao.UsuarioLogado.Id + ").";
            //     }
            //     else
            //     {
            //         // Nao deveria acontecer: so se chega aqui depois de
            //         // entrar. Mas tela que confia no "nao deveria" quebra.
            //         lblBemVindo.Text = "Bem-vindo";
            //         lblAviso.Text = "Ninguem esta logado.";
            //     }
            //
            //   Pare um segundo aqui: o Usuario que o login achou era uma
            //   variavel LOCAL, dentro de um evento - ela morre no fim do
            //   metodo. Esta tela abre depois, e formulario nenhum alcanca
            //   variavel de outro formulario. E por isso que a Sessao
            //   precisa existir, e por isso que ela e static.
            //
            //   APAGUE as duas linhas-tampao abaixo.
            lblBemVindo.Text = "Bem-vindo";
            lblAviso.Text = "O TODO 1 desta tela ainda nao foi feito.";
        }

        // JA PRONTO.
        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
