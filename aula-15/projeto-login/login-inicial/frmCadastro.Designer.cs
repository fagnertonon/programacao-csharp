namespace Conecta
{
    partial class frmCadastro
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
            this.lblNomeCap = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblUsuarioCap = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.lblSenhaCap = new System.Windows.Forms.Label();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblCabecalho
            //
            this.lblCabecalho.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.lblCabecalho.Location = new System.Drawing.Point(20, 15);
            this.lblCabecalho.Name = "lblCabecalho";
            this.lblCabecalho.Size = new System.Drawing.Size(340, 32);
            this.lblCabecalho.Text = "Criar conta";
            //
            // lblNomeCap
            //
            this.lblNomeCap.Location = new System.Drawing.Point(20, 65);
            this.lblNomeCap.Name = "lblNomeCap";
            this.lblNomeCap.Size = new System.Drawing.Size(85, 23);
            this.lblNomeCap.Text = "Nome:";
            //
            // txtNome
            //
            this.txtNome.Location = new System.Drawing.Point(112, 65);
            this.txtNome.MaxLength = 50;
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(240, 23);
            //
            // lblUsuarioCap
            //
            this.lblUsuarioCap.Location = new System.Drawing.Point(20, 100);
            this.lblUsuarioCap.Name = "lblUsuarioCap";
            this.lblUsuarioCap.Size = new System.Drawing.Size(85, 23);
            this.lblUsuarioCap.Text = "Usuario:";
            //
            // txtUsuario
            //
            this.txtUsuario.Location = new System.Drawing.Point(112, 100);
            this.txtUsuario.MaxLength = 50;
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(240, 23);
            //
            // lblSenhaCap
            //
            this.lblSenhaCap.Location = new System.Drawing.Point(20, 135);
            this.lblSenhaCap.Name = "lblSenhaCap";
            this.lblSenhaCap.Size = new System.Drawing.Size(85, 23);
            this.lblSenhaCap.Text = "Senha:";
            //
            // txtSenha
            //
            this.txtSenha.Location = new System.Drawing.Point(112, 135);
            this.txtSenha.MaxLength = 50;
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.PasswordChar = '*';
            this.txtSenha.Size = new System.Drawing.Size(240, 23);
            //
            // btnSalvar
            //
            this.btnSalvar.Location = new System.Drawing.Point(112, 180);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(115, 34);
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            //
            // btnVoltar
            //
            this.btnVoltar.Location = new System.Drawing.Point(237, 180);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(115, 34);
            this.btnVoltar.Text = "Voltar";
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            //
            // frmCadastro
            //
            this.AcceptButton = this.btnSalvar;
            this.ClientSize = new System.Drawing.Size(380, 240);
            this.Controls.Add(this.lblCabecalho);
            this.Controls.Add(this.lblNomeCap);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.lblUsuarioCap);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.lblSenhaCap);
            this.Controls.Add(this.txtSenha);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnVoltar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmCadastro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Conecta - Criar conta";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblCabecalho;
        private System.Windows.Forms.Label lblNomeCap;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblUsuarioCap;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblSenhaCap;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnVoltar;
    }
}
