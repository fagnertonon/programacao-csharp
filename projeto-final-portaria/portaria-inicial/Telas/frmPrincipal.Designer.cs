namespace Portaria
{
    partial class frmPrincipal
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

        #region Codigo gerado pelo Windows Form Designer

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblConectado = new System.Windows.Forms.Label();
            this.lblOrdem = new System.Windows.Forms.Label();
            this.cboOrdem = new System.Windows.Forms.ComboBox();
            this.lblCabecalho = new System.Windows.Forms.Label();
            this.lstUsuarios = new System.Windows.Forms.ListBox();
            this.lblRodape = new System.Windows.Forms.Label();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(20, 16);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(230, 25);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Usuarios cadastrados";
            //
            // lblConectado
            //
            this.lblConectado.AutoSize = true;
            this.lblConectado.ForeColor = System.Drawing.Color.DimGray;
            this.lblConectado.Location = new System.Drawing.Point(22, 46);
            this.lblConectado.Name = "lblConectado";
            this.lblConectado.Size = new System.Drawing.Size(120, 15);
            this.lblConectado.TabIndex = 1;
            this.lblConectado.Text = "Conectado como:";
            //
            // lblOrdem
            //
            this.lblOrdem.AutoSize = true;
            this.lblOrdem.Location = new System.Drawing.Point(20, 82);
            this.lblOrdem.Name = "lblOrdem";
            this.lblOrdem.Size = new System.Drawing.Size(48, 15);
            this.lblOrdem.TabIndex = 2;
            this.lblOrdem.Text = "Ordem:";
            //
            // cboOrdem
            //
            this.cboOrdem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOrdem.FormattingEnabled = true;
            this.cboOrdem.Items.AddRange(new object[] {
            "Por nome (A-Z)",
            "Mais recentes primeiro"});
            this.cboOrdem.Location = new System.Drawing.Point(74, 79);
            this.cboOrdem.Name = "cboOrdem";
            this.cboOrdem.Size = new System.Drawing.Size(220, 23);
            this.cboOrdem.TabIndex = 3;
            this.cboOrdem.SelectedIndexChanged += new System.EventHandler(this.cboOrdem_SelectedIndexChanged);
            //
            // lblCabecalho
            //
            this.lblCabecalho.AutoSize = true;
            this.lblCabecalho.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblCabecalho.Location = new System.Drawing.Point(22, 116);
            this.lblCabecalho.Name = "lblCabecalho";
            this.lblCabecalho.Size = new System.Drawing.Size(460, 15);
            this.lblCabecalho.TabIndex = 4;
            this.lblCabecalho.Text = " #  NOME                    (USUARIO)      CADASTRO";
            //
            // lstUsuarios
            //
            this.lstUsuarios.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.lstUsuarios.FormattingEnabled = true;
            this.lstUsuarios.ItemHeight = 15;
            this.lstUsuarios.Location = new System.Drawing.Point(20, 136);
            this.lstUsuarios.Name = "lstUsuarios";
            this.lstUsuarios.Size = new System.Drawing.Size(556, 184);
            this.lstUsuarios.TabIndex = 5;
            //
            // lblRodape
            //
            this.lblRodape.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRodape.Location = new System.Drawing.Point(20, 330);
            this.lblRodape.Name = "lblRodape";
            this.lblRodape.Size = new System.Drawing.Size(556, 30);
            this.lblRodape.TabIndex = 6;
            this.lblRodape.Text = "Carregando...";
            this.lblRodape.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // btnExcluir
            //
            this.btnExcluir.Location = new System.Drawing.Point(20, 372);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(180, 32);
            this.btnExcluir.TabIndex = 7;
            this.btnExcluir.Text = "Excluir o selecionado";
            this.btnExcluir.UseVisualStyleBackColor = true;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            //
            // btnSair
            //
            this.btnSair.Location = new System.Drawing.Point(482, 372);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(94, 32);
            this.btnSair.TabIndex = 8;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            //
            // frmPrincipal
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 419);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnExcluir);
            this.Controls.Add(this.lblRodape);
            this.Controls.Add(this.lstUsuarios);
            this.Controls.Add(this.lblCabecalho);
            this.Controls.Add(this.cboOrdem);
            this.Controls.Add(this.lblOrdem);
            this.Controls.Add(this.lblConectado);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Portaria - Principal";
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblConectado;
        private System.Windows.Forms.Label lblOrdem;
        private System.Windows.Forms.ComboBox cboOrdem;
        private System.Windows.Forms.Label lblCabecalho;
        private System.Windows.Forms.ListBox lstUsuarios;
        private System.Windows.Forms.Label lblRodape;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Button btnSair;
    }
}
