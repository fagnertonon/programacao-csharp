using System;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaLogin
{
    /// <summary>
    /// A TELA DE CRIAR CONTA.
    ///
    /// Esta classe SO monta a janela e mostra o que o Cadastro decidiu. Ela
    /// nao tem uma regra sequer dentro: toda resposta que aparece na tela e
    /// dada por um metodo do Cadastro.cs.
    ///
    /// E POR ISSO QUE ELA NAO SERA TESTADA HOJE. Um teste unitario nao sabe
    /// clicar num botao nem ler um Label. O que da para testar e o Cadastro
    /// - e e nele que estao as regras.
    /// </summary>
    public class TelaDeCadastro : Form
    {
        private TextBox txtUsuario;
        private TextBox txtSenha;
        private TextBox txtRepetir;
        private Button btnCriar;
        private Button btnLimpar;

        private Label lblUsuarioOk;
        private Label lblDisponivel;
        private Label lblSenhaOk;
        private Label lblConferem;
        private Label lblProibida;
        private Label lblRepeticao;
        private Label lblDiferente;
        private Label lblMensagem;

        public TelaDeCadastro()
        {
            Text = "Criar conta - Portal do Aluno - UC11";
            ClientSize = new Size(620, 545);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.White;
            Font = new Font("Segoe UI", 9.75f);

            MontarCabecalho();
            MontarFormulario();
            MontarRespostas();
            MontarListas();
        }

        private void MontarCabecalho()
        {
            Label titulo = new Label();
            titulo.Text = "CRIAR CONTA";
            titulo.Font = new Font("Segoe UI", 16f, FontStyle.Bold);
            titulo.ForeColor = Color.FromArgb(45, 27, 69);
            titulo.SetBounds(20, 14, 580, 32);
            Controls.Add(titulo);

            Label aviso = new Label();
            aviso.Text = "Preencha os tres campos e clique em Criar conta. "
                       + "As respostas de cada regra aparecem abaixo.";
            aviso.ForeColor = Color.Gray;
            aviso.Font = new Font("Segoe UI", 8.25f);
            aviso.SetBounds(20, 46, 580, 24);
            Controls.Add(aviso);
        }

        private void MontarFormulario()
        {
            GroupBox caixaFormulario = new GroupBox();
            caixaFormulario.Text = " Seus dados ";
            caixaFormulario.SetBounds(20, 76, 580, 150);
            Controls.Add(caixaFormulario);

            Label lblU = new Label();
            lblU.Text = "Usuario:";
            lblU.SetBounds(18, 32, 110, 22);
            caixaFormulario.Controls.Add(lblU);

            txtUsuario = new TextBox();
            txtUsuario.SetBounds(135, 29, 200, 25);
            caixaFormulario.Controls.Add(txtUsuario);

            Label lblS = new Label();
            lblS.Text = "Senha:";
            lblS.SetBounds(18, 64, 110, 22);
            caixaFormulario.Controls.Add(lblS);

            txtSenha = new TextBox();
            txtSenha.SetBounds(135, 61, 200, 25);
            caixaFormulario.Controls.Add(txtSenha);

            Label lblR = new Label();
            lblR.Text = "Repita a senha:";
            lblR.SetBounds(18, 96, 110, 22);
            caixaFormulario.Controls.Add(lblR);

            txtRepetir = new TextBox();
            txtRepetir.SetBounds(135, 93, 200, 25);
            caixaFormulario.Controls.Add(txtRepetir);

            btnCriar = new Button();
            btnCriar.Text = "Criar conta";
            btnCriar.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            btnCriar.SetBounds(370, 45, 180, 42);
            btnCriar.Click += new EventHandler(BotaoCriarClicado);
            caixaFormulario.Controls.Add(btnCriar);

            btnLimpar = new Button();
            btnLimpar.Text = "Limpar";
            btnLimpar.SetBounds(370, 96, 180, 28);
            btnLimpar.Click += new EventHandler(BotaoLimparClicado);
            caixaFormulario.Controls.Add(btnLimpar);

            AcceptButton = btnCriar;
        }

        // ----------------------------------------------------------------
        // As sete regras, uma por linha, em duas colunas.
        //
        // TODAS aparecem sempre, mesmo quando a conta e recusada - e isso
        // que deixa a contradicao visivel numa tela so.
        // ----------------------------------------------------------------
        private void MontarRespostas()
        {
            GroupBox caixaRespostas = new GroupBox();
            caixaRespostas.Text = " O que cada regra respondeu ";
            caixaRespostas.SetBounds(20, 236, 580, 200);
            Controls.Add(caixaRespostas);

            lblUsuarioOk = NovaResposta(caixaRespostas, 18, 30);
            lblDisponivel = NovaResposta(caixaRespostas, 18, 54);
            lblSenhaOk = NovaResposta(caixaRespostas, 18, 78);
            lblConferem = NovaResposta(caixaRespostas, 18, 102);

            lblProibida = NovaResposta(caixaRespostas, 300, 30);
            lblRepeticao = NovaResposta(caixaRespostas, 300, 54);
            lblDiferente = NovaResposta(caixaRespostas, 300, 78);

            lblMensagem = new Label();
            lblMensagem.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
            lblMensagem.SetBounds(18, 138, 545, 45);
            caixaRespostas.Controls.Add(lblMensagem);
        }

        private Label NovaResposta(GroupBox dono, int x, int y)
        {
            Label l = new Label();
            l.Font = new Font("Consolas", 9.5f);
            l.SetBounds(x, y, 265, 22);
            dono.Controls.Add(l);
            return l;
        }

        // ----------------------------------------------------------------
        // As duas listas que as regras usam. Elas nao mudam nunca: estao
        // aqui para voce nao precisar adivinhar os valores.
        // ----------------------------------------------------------------
        private void MontarListas()
        {
            GroupBox caixaListas = new GroupBox();
            caixaListas.Text = " O que o sistema ja tem ";
            caixaListas.SetBounds(20, 446, 580, 80);
            Controls.Add(caixaListas);

            Label contas = new Label();
            contas.Text = "Contas que ja existem:   ana     bruno     carla";
            contas.Font = new Font("Consolas", 9.5f);
            contas.SetBounds(18, 26, 545, 22);
            caixaListas.Controls.Add(contas);

            Label proibidas = new Label();
            proibidas.Text = "Senhas proibidas:        senha   admin     qwerty   senac";
            proibidas.Font = new Font("Consolas", 9.5f);
            proibidas.SetBounds(18, 50, 545, 22);
            caixaListas.Controls.Add(proibidas);
        }

        // ----------------------------------------------------------------
        // O BOTAO CRIAR CONTA.
        //
        // Pergunta as sete regras, mostra as sete respostas, e so entao
        // decide. Nenhuma decisao e tomada aqui dentro.
        // ----------------------------------------------------------------
        private void BotaoCriarClicado(object remetente, EventArgs argumentos)
        {
            string usuario = txtUsuario.Text;
            string senha = txtSenha.Text;
            string repetir = txtRepetir.Text;

            Responder(lblUsuarioOk, "Usuario preenchido ..... ", Cadastro.UsuarioPreenchido(usuario));
            Responder(lblDisponivel, "Usuario disponivel ..... ", Cadastro.UsuarioDisponivel(usuario));
            Responder(lblSenhaOk, "Senha preenchida ....... ", Cadastro.SenhaPreenchida(senha));
            Responder(lblConferem, "As senhas conferem ..... ", Cadastro.SenhasConferem(senha, repetir));
            Responder(lblProibida, "Senha fora da lista .... ", Cadastro.SenhaProibida(senha) == false);
            Responder(lblRepeticao, "Sem repeticao demais ... ", Cadastro.SemRepeticaoDemais(senha));
            Responder(lblDiferente, "Senha != usuario ....... ", Cadastro.SenhaDiferenteDoUsuario(usuario, senha));

            if (Cadastro.PodeCriarConta(usuario, senha, repetir) == true)
            {
                lblMensagem.Text = "Conta criada com sucesso.";
                lblMensagem.ForeColor = Color.FromArgb(14, 122, 85);
            }
            else
            {
                lblMensagem.Text = Cadastro.MensagemDoErro(usuario, senha, repetir);
                lblMensagem.ForeColor = Color.FromArgb(176, 42, 60);
            }
        }

        private void BotaoLimparClicado(object remetente, EventArgs argumentos)
        {
            txtUsuario.Text = "";
            txtSenha.Text = "";
            txtRepetir.Text = "";

            lblUsuarioOk.Text = "";
            lblDisponivel.Text = "";
            lblSenhaOk.Text = "";
            lblConferem.Text = "";
            lblProibida.Text = "";
            lblRepeticao.Text = "";
            lblDiferente.Text = "";
            lblMensagem.Text = "";

            txtUsuario.Focus();
        }

        private void Responder(Label alvo, string rotulo, bool valor)
        {
            if (valor == true)
            {
                alvo.Text = rotulo + "SIM";
                alvo.ForeColor = Color.FromArgb(14, 122, 85);
            }
            else
            {
                alvo.Text = rotulo + "NAO";
                alvo.ForeColor = Color.FromArgb(176, 42, 60);
            }
        }
    }
}
