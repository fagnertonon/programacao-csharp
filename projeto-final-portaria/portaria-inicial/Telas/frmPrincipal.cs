using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Portaria
{
    /// <summary>
    /// Tela principal: a lista dos usuarios cadastrados.
    ///
    /// O campo listaAtual e a memoria da tela. Ele guarda a MESMA lista
    /// que foi desenhada na ListBox, na MESMA ordem - e e por isso que o
    /// indice da linha selecionada serve para achar o usuario certo na
    /// hora de excluir.
    ///
    /// Nesta tela voce escreve DOIS metodos: o TODO 12 e o TODO 14.
    /// </summary>
    public partial class frmPrincipal : Form
    {
        /// <summary>O que esta na tela agora. Preenchido pelo TODO 12.</summary>
        private List<Usuario> listaAtual = new List<Usuario>();

        public frmPrincipal()
        {
            InitializeComponent();
        }

        /// <summary>
        /// JA VEM PRONTO.
        ///
        /// O cinto de seguranca: se alguem chegar aqui sem ter passado
        /// pelo login, a tela se fecha sozinha.
        ///
        /// A ultima linha define a ordem inicial - e mudar a ordem
        /// dispara o CarregarLista sozinho. Voce nao precisa chama-lo.
        /// </summary>
        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            if (Sessao.TemUsuarioLogado() == false)
            {
                MessageBox.Show("Ninguem esta conectado.", "Atencao",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            lblConectado.Text = "Conectado como: "
                              + Regras.PrimeiroNome(Sessao.UsuarioLogado.Nome);

            cboOrdem.SelectedIndex = 0;   // dispara o CarregarLista
        }

        /// <summary>JA VEM PRONTO. Trocar a ordem redesenha a lista.</summary>
        private void cboOrdem_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarLista();
        }

        /// <summary>
        /// TODO 12 - Le os usuarios do banco e desenha a lista numerada.
        ///                                        [Indicadores I3 e I5]
        ///
        /// Ponha TUDO dentro de try / catch: se o banco cair no meio, o
        /// programa avisa em vez de fechar sozinho.
        ///
        ///  1. LER, na ordem escolhida no ComboBox:
        ///       listaAtual = UsuarioDAO.ListarTodos(cboOrdem.SelectedIndex);
        ///
        ///     Guarde no CAMPO listaAtual, nao numa variavel local - o
        ///     TODO 14 vai precisar dela depois.
        ///
        ///  2. LIMPAR a ListBox:  lstUsuarios.Items.Clear();
        ///
        ///  3. PERCORRER com FOR, porque aqui a POSICAO importa:
        ///
        ///       for (int i = 0; i < listaAtual.Count; i++)
        ///       {
        ///           Usuario u = listaAtual[i];
        ///           string linha = (i + 1).ToString().PadLeft(2) + "  "
        ///                        + u.Nome.PadRight(24)
        ///                        + ("(" + u.Login + ")").PadRight(16)
        ///                        + u.DataCadastro.ToString("dd/MM/yyyy");
        ///           lstUsuarios.Items.Add(linha);
        ///       }
        ///
        ///     POR QUE FOR E NAO FOREACH: o numero que aparece na linha
        ///     e i + 1, e o indice i e exatamente o mesmo que o botao
        ///     Excluir vai usar para achar o usuario em listaAtual. O
        ///     foreach nao te da esse indice.
        ///
        ///  4. RODAPE, usando o TODO 11:
        ///       Usuario ultimo = Regras.UltimoCadastrado(listaAtual);
        ///     se vier null, escreva "Nenhum usuario cadastrado.";
        ///     senao, escreva o total e o nome do ultimo.
        /// </summary>
        private void CarregarLista()
        {
            lblRodape.Text = "TODO 12 ainda nao foi escrito.";   // <<< APAGUE esta linha
        }

        /// <summary>
        /// TODO 14 - Exclui a conta selecionada.
        ///                                    [Indicadores I3, I4 e I6]
        ///
        ///  1. DESCOBRIR o que foi selecionado:
        ///       int i = lstUsuarios.SelectedIndex;
        ///
        ///     Quando nada esta selecionado, o SelectedIndex vale -1.
        ///     Se voce usar esse -1 em listaAtual[i], o programa quebra.
        ///
        ///       if (i &lt; 0)  ->  mensagem M8 e  return;
        ///
        ///  2. ACHAR o usuario, pelo mesmo indice:
        ///       Usuario escolhido = listaAtual[i];
        ///
        ///     E aqui que o for do TODO 12 se paga: a linha numero i da
        ///     tela e o item numero i da lista.
        ///
        ///  3. RECUSAR a propria conta:
        ///       if (escolhido.Id == Sessao.IdUsuarioLogado())
        ///     mensagem M9 e  return;
        ///
        ///     Excluir a si mesmo enquanto esta conectado deixaria a
        ///     sessao apontando para alguem que nao existe mais.
        ///
        ///  4. CONFIRMAR antes de apagar:
        ///       DialogResult resposta = MessageBox.Show(
        ///           "Excluir a conta de " + escolhido.Nome + "?",
        ///           "Confirmar exclusao",
        ///           MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        ///       if (resposta != DialogResult.Yes) { return; }
        ///
        ///  5. APAGAR e redesenhar, dentro de try / catch:
        ///       UsuarioDAO.ExcluirConta(escolhido.Id);
        ///       CarregarLista();
        ///
        /// AS DUAS MENSAGENS VAO PARA O MANUAL:
        ///   M8 - "Escolha um usuario na lista primeiro."
        ///   M9 - "Voce nao pode excluir a sua propria conta..."
        /// </summary>
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            MessageBox.Show("TODO 14 ainda nao foi escrito.");   // <<< APAGUE esta linha
        }

        /// <summary>JA VEM PRONTO.</summary>
        private void btnSair_Click(object sender, EventArgs e)
        {
            Sessao.Encerrar();
            this.Close();
        }
    }
}
