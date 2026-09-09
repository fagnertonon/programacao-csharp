using System;
using System.Drawing;
using System.Windows.Forms;

namespace PrimeiroOTeste
{
    /// <summary>
    /// A JANELA da Aula 7 - Windows Forms, o mesmo tipo de projeto que
    /// voces fizeram a UC12 inteira.
    ///
    /// ELA NAO TEM REGRA NENHUMA DENTRO. Cada botao daqui so chama um
    /// metodo da Calculadora ou da Conta e mostra o que voltou. Se o
    /// metodo ainda nao tem corpo, o que volta e o estouro - e a janela
    /// escreve AINDA NAO IMPLEMENTADO em vez de fechar na sua cara.
    ///
    /// E POR ISSO QUE ELA EXISTE: conforme voce escreve o corpo de cada
    /// metodo, o painel da direita vai acendendo. Ele e o placar da sua
    /// noite.
    ///
    /// Repare que a janela NAO E TESTADA hoje, e nao e por falta de
    /// tempo: ela le de caixa de texto e escreve em rotulo, e um teste
    /// nao confere sozinho o que ela faz. Quem esta sob teste e a
    /// Calculadora e a Conta - as duas nao leem nem escrevem nada.
    ///
    /// UC11 - Aula 7, 09/09/2026.
    /// </summary>
    public class TelaPrimeiroOTeste : Form
    {
        private TextBox txtA;
        private TextBox txtB;
        private Label lblResultado;

        private TextBox txtValor;
        private Label lblSaldoA;
        private Label lblSaldoB;
        private Label lblConta;

        private Label[] placar;
        private Conta contaA = new Conta();
        private Conta contaB = new Conta();

        private static readonly string[] NOMES =
        {
            "Somar", "Subtrair", "Multiplicar", "Dividir", "Porcentagem",
            "Depositar", "Sacar", "TemSaldo", "Transferir",
        };

        public TelaPrimeiroOTeste()
        {
            Text = "Primeiro o teste - UC11 Aula 7";
            ClientSize = new Size(880, 590);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.White;
            Font = new Font("Segoe UI", 9F);

            Label titulo = new Label();
            titulo.Text = "PRIMEIRO O TESTE";
            titulo.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            titulo.SetBounds(20, 14, 560, 32);
            Controls.Add(titulo);

            Label aviso = new Label();
            aviso.Text = "Cada botao chama um metodo. Enquanto o metodo nao tiver corpo, "
                       + "a resposta vai ser AINDA NAO IMPLEMENTADO.";
            aviso.ForeColor = Color.FromArgb(90, 90, 90);
            aviso.SetBounds(20, 46, 560, 34);
            Controls.Add(aviso);

            MontarCalculadora();
            MontarConta();
            MontarPlacar();

            Atualizar();
        }

        // ---------------------------------------------------------------
        // A CALCULADORA
        // ---------------------------------------------------------------
        private void MontarCalculadora()
        {
            GroupBox caixa = new GroupBox();
            caixa.Text = " Calculadora ";
            caixa.SetBounds(20, 88, 560, 190);
            Controls.Add(caixa);

            Label lblA = new Label();
            lblA.Text = "Primeiro numero:";
            lblA.SetBounds(18, 32, 120, 22);
            caixa.Controls.Add(lblA);

            txtA = new TextBox();
            txtA.Text = "10";
            txtA.SetBounds(145, 29, 90, 25);
            caixa.Controls.Add(txtA);

            Label lblB = new Label();
            lblB.Text = "Segundo numero:";
            lblB.SetBounds(255, 32, 120, 22);
            caixa.Controls.Add(lblB);

            txtB = new TextBox();
            txtB.Text = "2";
            txtB.SetBounds(382, 29, 90, 25);
            caixa.Controls.Add(txtB);

            string[] rotulos = { "Somar", "Subtrair", "Multiplicar", "Dividir", "Porcentagem" };
            for (int i = 0; i < rotulos.Length; i++)
            {
                Button b = new Button();
                b.Text = rotulos[i];
                b.SetBounds(18 + i * 108, 70, 100, 30);
                b.Tag = rotulos[i];
                b.Click += CalcularClique;
                caixa.Controls.Add(b);
            }

            lblResultado = new Label();
            lblResultado.Text = "Clique em um botao.";
            lblResultado.Font = new Font("Consolas", 11F, FontStyle.Bold);
            lblResultado.BorderStyle = BorderStyle.FixedSingle;
            lblResultado.TextAlign = ContentAlignment.MiddleLeft;
            lblResultado.BackColor = Color.FromArgb(248, 248, 250);
            lblResultado.SetBounds(18, 112, 522, 60);
            caixa.Controls.Add(lblResultado);
        }

        private void CalcularClique(object remetente, EventArgs e)
        {
            string qual = (string)((Button)remetente).Tag;
            double a, b;

            if (double.TryParse(txtA.Text, out a) == false ||
                double.TryParse(txtB.Text, out b) == false)
            {
                Mostrar(lblResultado, "Digite dois numeros.", Color.FromArgb(150, 90, 0));
                return;
            }

            try
            {
                double r;
                if (qual == "Somar") { r = Calculadora.Somar(a, b); }
                else if (qual == "Subtrair") { r = Calculadora.Subtrair(a, b); }
                else if (qual == "Multiplicar") { r = Calculadora.Multiplicar(a, b); }
                else if (qual == "Dividir") { r = Calculadora.Dividir(a, b); }
                else { r = Calculadora.Porcentagem(a, b); }

                Mostrar(lblResultado, qual + "(" + a + ", " + b + ")  =  " + r,
                        Color.FromArgb(0, 110, 70));
            }
            catch (NotImplementedException)
            {
                Mostrar(lblResultado, qual + " AINDA NAO IMPLEMENTADO.\r\n"
                      + "Escreva o teste, veja o vermelho, e so entao o corpo.",
                        Color.FromArgb(150, 40, 60));
            }
            catch (Exception erro)
            {
                Mostrar(lblResultado, qual + " estourou:\r\n" + erro.GetType().Name
                      + " - " + erro.Message, Color.FromArgb(150, 40, 60));
            }

            Atualizar();
        }

        // ---------------------------------------------------------------
        // A CONTA
        // ---------------------------------------------------------------
        private void MontarConta()
        {
            GroupBox caixa = new GroupBox();
            caixa.Text = " Conta bancaria ";
            caixa.SetBounds(20, 288, 560, 230);
            Controls.Add(caixa);

            lblSaldoA = new Label();
            lblSaldoA.Font = new Font("Consolas", 11F, FontStyle.Bold);
            lblSaldoA.SetBounds(18, 28, 250, 24);
            caixa.Controls.Add(lblSaldoA);

            lblSaldoB = new Label();
            lblSaldoB.Font = new Font("Consolas", 11F, FontStyle.Bold);
            lblSaldoB.SetBounds(285, 28, 250, 24);
            caixa.Controls.Add(lblSaldoB);

            Label lblV = new Label();
            lblV.Text = "Valor:";
            lblV.SetBounds(18, 62, 50, 22);
            caixa.Controls.Add(lblV);

            txtValor = new TextBox();
            txtValor.Text = "100";
            txtValor.SetBounds(70, 59, 90, 25);
            caixa.Controls.Add(txtValor);

            Button zerar = new Button();
            zerar.Text = "Zerar as duas contas";
            zerar.SetBounds(390, 58, 150, 27);
            zerar.Click += ZerarClique;
            caixa.Controls.Add(zerar);

            string[] rotulos = { "Depositar", "Sacar", "TemSaldo", "Transferir" };
            for (int i = 0; i < rotulos.Length; i++)
            {
                Button b = new Button();
                b.Text = rotulos[i] == "Transferir" ? "Transferir A->B" : rotulos[i];
                b.SetBounds(18 + i * 135, 98, 127, 30);
                b.Tag = rotulos[i];
                b.Click += ContaClique;
                caixa.Controls.Add(b);
            }

            lblConta = new Label();
            lblConta.Text = "Clique em um botao.";
            lblConta.Font = new Font("Consolas", 11F, FontStyle.Bold);
            lblConta.BorderStyle = BorderStyle.FixedSingle;
            lblConta.TextAlign = ContentAlignment.MiddleLeft;
            lblConta.BackColor = Color.FromArgb(248, 248, 250);
            lblConta.SetBounds(18, 140, 522, 72);
            caixa.Controls.Add(lblConta);
        }

        private void ZerarClique(object remetente, EventArgs e)
        {
            contaA = new Conta();
            contaB = new Conta();
            Mostrar(lblConta, "Duas contas novas, as duas com saldo zero.",
                    Color.FromArgb(60, 60, 60));
            Atualizar();
        }

        private void ContaClique(object remetente, EventArgs e)
        {
            string qual = (string)((Button)remetente).Tag;
            double v;

            if (double.TryParse(txtValor.Text, out v) == false)
            {
                Mostrar(lblConta, "Digite um valor.", Color.FromArgb(150, 90, 0));
                return;
            }

            try
            {
                if (qual == "Depositar")
                {
                    contaA.Depositar(v);
                    Mostrar(lblConta, "A.Depositar(" + v + ")   ->   saldo de A agora e "
                          + contaA.Saldo, Color.FromArgb(0, 110, 70));
                }
                else if (qual == "Sacar")
                {
                    bool ok = contaA.Sacar(v);
                    Mostrar(lblConta, "A.Sacar(" + v + ")   ->   devolveu " + ok
                          + "\r\nsaldo de A agora e " + contaA.Saldo,
                            Color.FromArgb(0, 110, 70));
                }
                else if (qual == "TemSaldo")
                {
                    bool ok = contaA.TemSaldo(v);
                    Mostrar(lblConta, "A.TemSaldo(" + v + ")   ->   devolveu " + ok
                          + "\r\nsaldo de A continua " + contaA.Saldo,
                            Color.FromArgb(0, 110, 70));
                }
                else
                {
                    bool ok = contaA.Transferir(contaB, v);
                    Mostrar(lblConta, "A.Transferir(B, " + v + ")   ->   devolveu " + ok
                          + "\r\nA fica com " + contaA.Saldo + "   e B fica com " + contaB.Saldo,
                            Color.FromArgb(0, 110, 70));
                }
            }
            catch (NotImplementedException)
            {
                Mostrar(lblConta, qual + " AINDA NAO IMPLEMENTADO.\r\n"
                      + "Escreva o teste, veja o vermelho, e so entao o corpo.",
                        Color.FromArgb(150, 40, 60));
            }
            catch (Exception erro)
            {
                Mostrar(lblConta, qual + " estourou:\r\n" + erro.GetType().Name
                      + " - " + erro.Message, Color.FromArgb(150, 40, 60));
            }

            Atualizar();
        }

        // ---------------------------------------------------------------
        // O PLACAR - o painel que acende conforme voce implementa
        // ---------------------------------------------------------------
        private void MontarPlacar()
        {
            GroupBox caixa = new GroupBox();
            caixa.Text = " O que ja existe ";
            caixa.SetBounds(600, 88, 258, 430);
            Controls.Add(caixa);

            Label ajuda = new Label();
            ajuda.Text = "Este painel le os nove metodos e diz quais ja tem corpo. "
                       + "Ele e o placar da sua noite.";
            ajuda.ForeColor = Color.FromArgb(90, 90, 90);
            ajuda.SetBounds(16, 26, 226, 46);
            caixa.Controls.Add(ajuda);

            placar = new Label[NOMES.Length];
            for (int i = 0; i < NOMES.Length; i++)
            {
                Label l = new Label();
                l.Font = new Font("Consolas", 10.5F, FontStyle.Bold);
                l.SetBounds(16, 82 + i * 30, 226, 24);
                caixa.Controls.Add(l);
                placar[i] = l;
            }
        }

        /// <summary>
        /// Chama cada metodo so para ver se ele tem corpo. Um
        /// NotImplementedException quer dizer que nao tem; qualquer outro
        /// estouro quer dizer que tem, e que ele reclamou de outra coisa.
        ///
        /// A conta usada aqui e descartavel, para o placar nao mexer no
        /// saldo que esta na tela.
        /// </summary>
        private static bool TemCorpo(Action acao)
        {
            try
            {
                acao();
                return true;
            }
            catch (NotImplementedException)
            {
                return false;
            }
            catch
            {
                return true;
            }
        }

        private void Atualizar()
        {
            lblSaldoA.Text = "Conta A:  " + contaA.Saldo;
            lblSaldoB.Text = "Conta B:  " + contaB.Saldo;

            Conta c = new Conta();
            Conta d = new Conta();

            bool[] feito =
            {
                TemCorpo(delegate { Calculadora.Somar(1, 1); }),
                TemCorpo(delegate { Calculadora.Subtrair(1, 1); }),
                TemCorpo(delegate { Calculadora.Multiplicar(1, 1); }),
                TemCorpo(delegate { Calculadora.Dividir(1, 1); }),
                TemCorpo(delegate { Calculadora.Porcentagem(1, 1); }),
                TemCorpo(delegate { c.Depositar(1); }),
                TemCorpo(delegate { c.Sacar(1); }),
                TemCorpo(delegate { c.TemSaldo(1); }),
                TemCorpo(delegate { c.Transferir(d, 1); }),
            };

            int prontos = 0;
            for (int i = 0; i < NOMES.Length; i++)
            {
                bool ok = feito[i];
                if (ok) { prontos = prontos + 1; }
                placar[i].Text = (ok ? "[X] " : "[ ] ") + NOMES[i];
                placar[i].ForeColor = ok
                    ? Color.FromArgb(0, 110, 70)
                    : Color.FromArgb(150, 40, 60);
            }

            Text = "Primeiro o teste - UC11 Aula 7   -   " + prontos + " de 9 implementados";
        }

        private static void Mostrar(Label onde, string texto, Color cor)
        {
            onde.Text = texto;
            onde.ForeColor = cor;
        }
    }
}
