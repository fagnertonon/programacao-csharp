namespace Conecta
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

        private void InitializeComponent()
        {
            this.lblBemVindo = new System.Windows.Forms.Label();
            this.lblAviso = new System.Windows.Forms.Label();
            this.btnSair = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblBemVindo
            //
            this.lblBemVindo.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.lblBemVindo.Location = new System.Drawing.Point(20, 25);
            this.lblBemVindo.Name = "lblBemVindo";
            this.lblBemVindo.Size = new System.Drawing.Size(340, 32);
            this.lblBemVindo.Text = "Bem-vindo";
            //
            // lblAviso
            //
            this.lblAviso.Location = new System.Drawing.Point(20, 65);
            this.lblAviso.Name = "lblAviso";
            this.lblAviso.Size = new System.Drawing.Size(340, 40);
            this.lblAviso.Text = "";
            //
            // btnSair
            //
            this.btnSair.Location = new System.Drawing.Point(130, 120);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(120, 34);
            this.btnSair.Text = "Sair";
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            //
            // frmPrincipal
            //
            this.ClientSize = new System.Drawing.Size(380, 180);
            this.Controls.Add(this.lblBemVindo);
            this.Controls.Add(this.lblAviso);
            this.Controls.Add(this.btnSair);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Conecta";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblBemVindo;
        private System.Windows.Forms.Label lblAviso;
        private System.Windows.Forms.Button btnSair;
    }
}
