namespace Masmorra
{
    // Este arquivo e escrito pelo DESIGNER, sozinho, quando voce arrasta
    // controle na tela. A tela de hoje ja vem montada - voce nao precisa
    // abrir este arquivo.
    partial class frmJogo
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
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pnlMapa = new System.Windows.Forms.Panel();
            this.lblHeroi = new System.Windows.Forms.Label();
            this.lblNome = new System.Windows.Forms.Label();
            this.lblNivel = new System.Windows.Forms.Label();
            this.lblBarra = new System.Windows.Forms.Label();
            this.lblVida = new System.Windows.Forms.Label();
            this.lblForca = new System.Windows.Forms.Label();
            this.lblXp = new System.Windows.Forms.Label();
            this.lblSituacao = new System.Windows.Forms.Label();
            this.lblPlacar = new System.Windows.Forms.Label();
            this.lblAjuda = new System.Windows.Forms.Label();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.pnlMapa.SuspendLayout();
            this.SuspendLayout();
            //
            // lblTitulo
            //
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(16, 10);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(500, 30);
            this.lblTitulo.Text = "Masmorra";
            //
            // pnlMapa
            //
            // 12 colunas x 8 linhas de 48 pixels = 576 x 384.
            // Os monstros nascem DENTRO deste painel, criados pelo codigo
            // que ja esta pronto no frmJogo.cs.
            //
            this.pnlMapa.BackColor = System.Drawing.Color.FromArgb(30, 18, 48);
            this.pnlMapa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMapa.Controls.Add(this.lblHeroi);
            this.pnlMapa.Location = new System.Drawing.Point(16, 46);
            this.pnlMapa.Name = "pnlMapa";
            this.pnlMapa.Size = new System.Drawing.Size(576, 384);
            //
            // lblHeroi
            //
            // Quem faz este Label ANDAR e o TODO 1: enquanto o PodeAndar
            // devolver false, o heroi fica parado no canto.
            //
            this.lblHeroi.BackColor = System.Drawing.Color.Transparent;
            this.lblHeroi.Font = new System.Drawing.Font("Segoe UI Emoji", 20F);
            this.lblHeroi.ForeColor = System.Drawing.Color.White;
            this.lblHeroi.Location = new System.Drawing.Point(0, 0);
            this.lblHeroi.Name = "lblHeroi";
            this.lblHeroi.Size = new System.Drawing.Size(48, 48);
            this.lblHeroi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblNome
            //
            this.lblNome.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblNome.Location = new System.Drawing.Point(612, 46);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(300, 28);
            this.lblNome.Text = "HEROI";
            //
            // lblNivel
            //
            this.lblNivel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblNivel.Location = new System.Drawing.Point(612, 78);
            this.lblNivel.Name = "lblNivel";
            this.lblNivel.Size = new System.Drawing.Size(300, 24);
            this.lblNivel.Text = "Nivel 1";
            //
            // lblBarra
            //
            // Nasce vazio. Quem escreve nele e o TODO 9.
            //
            this.lblBarra.Font = new System.Drawing.Font("Consolas", 16F);
            this.lblBarra.ForeColor = System.Drawing.Color.FromArgb(14, 122, 85);
            this.lblBarra.Location = new System.Drawing.Point(612, 106);
            this.lblBarra.Name = "lblBarra";
            this.lblBarra.Size = new System.Drawing.Size(300, 30);
            this.lblBarra.Text = "";
            //
            // lblVida
            //
            this.lblVida.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblVida.Location = new System.Drawing.Point(612, 140);
            this.lblVida.Name = "lblVida";
            this.lblVida.Size = new System.Drawing.Size(300, 24);
            this.lblVida.Text = "Vida 20 / 20";
            //
            // lblForca
            //
            this.lblForca.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblForca.Location = new System.Drawing.Point(612, 168);
            this.lblForca.Name = "lblForca";
            this.lblForca.Size = new System.Drawing.Size(300, 24);
            this.lblForca.Text = "Forca 5";
            //
            // lblXp
            //
            this.lblXp.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblXp.Location = new System.Drawing.Point(612, 196);
            this.lblXp.Name = "lblXp";
            this.lblXp.Size = new System.Drawing.Size(300, 24);
            this.lblXp.Text = "XP 0";
            //
            // lblSituacao
            //
            // Nasce vazio. Quem escreve nele e o TODO 10.
            //
            this.lblSituacao.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblSituacao.Location = new System.Drawing.Point(612, 224);
            this.lblSituacao.Name = "lblSituacao";
            this.lblSituacao.Size = new System.Drawing.Size(300, 24);
            this.lblSituacao.Text = "";
            //
            // lblPlacar
            //
            this.lblPlacar.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblPlacar.ForeColor = System.Drawing.Color.FromArgb(92, 45, 145);
            this.lblPlacar.Location = new System.Drawing.Point(612, 262);
            this.lblPlacar.Name = "lblPlacar";
            this.lblPlacar.Size = new System.Drawing.Size(300, 24);
            this.lblPlacar.Text = "Monstros derrotados: 0 de 10";
            //
            // lblAjuda
            //
            this.lblAjuda.Location = new System.Drawing.Point(16, 436);
            this.lblAjuda.Name = "lblAjuda";
            this.lblAjuda.Size = new System.Drawing.Size(900, 24);
            this.lblAjuda.Text = "Setas para andar. Ande para cima de um monstro para lutar.";
            //
            // lstLog
            //
            this.lstLog.Font = new System.Drawing.Font("Consolas", 10F);
            this.lstLog.FormattingEnabled = true;
            this.lstLog.ItemHeight = 15;
            this.lstLog.Location = new System.Drawing.Point(16, 462);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(900, 139);
            this.lstLog.TabStop = false;
            //
            // frmJogo
            //
            this.ClientSize = new System.Drawing.Size(934, 616);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.pnlMapa);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.lblNivel);
            this.Controls.Add(this.lblBarra);
            this.Controls.Add(this.lblVida);
            this.Controls.Add(this.lblForca);
            this.Controls.Add(this.lblXp);
            this.Controls.Add(this.lblSituacao);
            this.Controls.Add(this.lblPlacar);
            this.Controls.Add(this.lblAjuda);
            this.Controls.Add(this.lstLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmJogo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Masmorra - Aula 21";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmJogo_KeyDown);
            this.Shown += new System.EventHandler(this.frmJogo_Shown);
            this.pnlMapa.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel pnlMapa;
        private System.Windows.Forms.Label lblHeroi;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.Label lblNivel;
        private System.Windows.Forms.Label lblBarra;
        private System.Windows.Forms.Label lblVida;
        private System.Windows.Forms.Label lblForca;
        private System.Windows.Forms.Label lblXp;
        private System.Windows.Forms.Label lblSituacao;
        private System.Windows.Forms.Label lblPlacar;
        private System.Windows.Forms.Label lblAjuda;
        private System.Windows.Forms.ListBox lstLog;
    }
}
