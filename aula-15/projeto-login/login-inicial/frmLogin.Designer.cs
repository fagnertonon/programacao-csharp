namespace Conecta
{
    // Este arquivo e escrito pelo DESIGNER, sozinho, quando voce arrasta
    // controle na tela. A tela de hoje ja vem montada - voce nao precisa
    // abrir este arquivo.
    partial class frmLogin
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblCabecalho = new System.Windows.Forms.Label();
            this.lblUsuarioCap = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.lblSenhaCap = new System.Windows.Forms.Label();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.lblForca = new System.Windows.Forms.Label();
            this.btnEntrar = new System.Windows.Forms.Button();
            this.btnCriarConta = new System.Windows.Forms.Button();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // lblCabecalho
            //
            this.lblCabecalho.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.lblCabecalho.Location = new System.Drawing.Point(20, 15);
            this.lblCabecalho.Name = "lblCabecalho";
            this.lblCabecalho.Size = new System.Drawing.Size(340, 40);
            this.lblCabecalho.Text = "Conecta";
            //
            // lblUsuarioCap
            //
            this.lblUsuarioCap.Location = new System.Drawing.Point(20, 75);
            this.lblUsuarioCap.Name = "lblUsuarioCap";
            this.lblUsuarioCap.Size = new System.Drawing.Size(85, 23);
            this.lblUsuarioCap.Text = "Usuario:";
            //
            // txtUsuario
            //
            this.txtUsuario.Location = new System.Drawing.Point(112, 75);
            this.txtUsuario.MaxLength = 50;
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(240, 23);
            //
            // lblSenhaCap
            //
            this.lblSenhaCap.Location = new System.Drawing.Point(20, 110);
            this.lblSenhaCap.Name = "lblSenhaCap";
            this.lblSenhaCap.Size = new System.Drawing.Size(85, 23);
            this.lblSenhaCap.Text = "Senha:";
            //
            // txtSenha
            //
            this.txtSenha.Location = new System.Drawing.Point(112, 110);
            this.txtSenha.MaxLength = 50;
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.PasswordChar = '*';
            this.txtSenha.Size = new System.Drawing.Size(240, 23);
            this.txtSenha.TextChanged += new System.EventHandler(this.txtSenha_TextChanged);
            //
            // lblForca
            //
            // Nasce vazio. Quem escreve nele e o TODO 8.
            //
            this.lblForca.Font = new System.Drawing.Font("Consolas", 9F);
            this.lblForca.Location = new System.Drawing.Point(112, 136);
            this.lblForca.Name = "lblForca";
            this.lblForca.Size = new System.Drawing.Size(240, 16);
            this.lblForca.Text = "";
            //
            // btnEntrar
            //
            this.btnEntrar.Location = new System.Drawing.Point(112, 158);
            this.btnEntrar.Name = "btnEntrar";
            this.btnEntrar.Size = new System.Drawing.Size(110, 34);
            this.btnEntrar.Text = "Entrar";
            this.btnEntrar.Click += new System.EventHandler(this.btnEntrar_Click);
            //
            // btnCriarConta
            //
            this.btnCriarConta.Location = new System.Drawing.Point(232, 158);
            this.btnCriarConta.Name = "btnCriarConta";
            this.btnCriarConta.Size = new System.Drawing.Size(120, 34);
            this.btnCriarConta.Text = "Criar conta";
            this.btnCriarConta.Click += new System.EventHandler(this.btnCriarConta_Click);
            //
            // btnLimpar
            //
            this.btnLimpar.Location = new System.Drawing.Point(112, 200);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(240, 30);
            this.btnLimpar.Text = "Limpar";
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            //
            // lblStatus
            //
            // O aviso que FICA na tela, sem caixa para o usuario fechar.
            // Nasce vazio. Quem escreve nele e o TODO 6.
            //
            this.lblStatus.ForeColor = System.Drawing.Color.Firebrick;
            this.lblStatus.Location = new System.Drawing.Point(20, 240);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(340, 38);
            this.lblStatus.Text = "";
            //
            // frmLogin
            //
            this.AcceptButton = this.btnEntrar;
            this.ClientSize = new System.Drawing.Size(380, 290);
            this.Controls.Add(this.lblCabecalho);
            this.Controls.Add(this.lblUsuarioCap);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.lblSenhaCap);
            this.Controls.Add(this.txtSenha);
            this.Controls.Add(this.lblForca);
            this.Controls.Add(this.btnEntrar);
            this.Controls.Add(this.btnCriarConta);
            this.Controls.Add(this.btnLimpar);
            this.Controls.Add(this.lblStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Conecta - Entrar";
            this.Shown += new System.EventHandler(this.frmLogin_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblCabecalho;
        private System.Windows.Forms.Label lblUsuarioCap;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblSenhaCap;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.Label lblForca;
        private System.Windows.Forms.Button btnEntrar;
        private System.Windows.Forms.Button btnCriarConta;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.Label lblStatus;
    }
}
