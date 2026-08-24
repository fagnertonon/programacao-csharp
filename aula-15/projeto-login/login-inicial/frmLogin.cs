using System;
using System.Windows.Forms;

namespace Conecta
{
    /// <summary>
    /// Conecta - tela de login - PONTO DE PARTIDA.
    ///
    /// A tela ja esta pronta (frmLogin.Designer.cs) e o projeto JA COMPILA
    /// E RODA: aperte F5 antes de escrever qualquer coisa. Os campos
    /// aceitam digitacao, os botoes existem, e nada acontece - esse e o
    /// seu trabalho de hoje.
    ///
    /// Procure por TODO neste arquivo. Sao 10 lacunas, em ordem.
    ///
    /// CONTROLES (ja montados no Designer):
    ///   txtUsuario, txtSenha ......................... TextBox
    ///   lblCabecalho, lblUsuarioCap, lblSenhaCap ..... Label
    ///   lblForca ..... Label Consolas - a barra de forca, nasce vazio
    ///   lblStatus .... Label - o aviso que fica na tela, nasce vazio
    ///   btnEntrar, btnCriarConta, btnLimpar .......... Button
    /// </summary>
    public partial class frmLogin : Form
    {
        // Quantas vezes o usuario errou seguido. E o valor que o switch do
        // TODO 6 vai olhar.
        private int tentativas = 0;

        private const int MAXIMO_DE_TENTATIVAS = 3;

        public frmLogin()
        {
            InitializeComponent();
        }

        // =================================================================
        // JA PRONTO - o aviso da tela
        //
        // Este metodo nasceu de um incomodo: o MessageBox.Show de QUATRO
        // argumentos estava escrito tres vezes neste arquivo, e a ordem dos
        // argumentos e o que mais se erra. Extraido para ca, a ordem mora
        // num lugar so, e as tres chamadas viraram uma linha cada.
        //
        // Ele fica pronto de exemplo. No frmCadastro.cs voce faz a mesma
        // extracao com as suas maos.
        // =================================================================

        private void Avisar(string mensagem)
        {
            MessageBox.Show(mensagem, "Conecta",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // JA PRONTO - uma linha, nao ensina nada.
        private void btnCriarConta_Click(object sender, EventArgs e)
        {
            frmCadastro tela = new frmCadastro();
            tela.ShowDialog();
        }

        // JA PRONTO - a rede de seguranca do laboratorio.
        //
        // Roda assim que a tela aparece e diz, em portugues, se o MySQL
        // esta de pe. Quando o banco entrar, no Bloco 6, esta linha e que
        // vai avisar quem esqueceu de trocar a senha no Conexao.cs.
        private void frmLogin_Shown(object sender, EventArgs e)
        {
            string erro;

            if (Conexao.TestarConexao(out erro) == false)
            {
                lblStatus.Text = erro;
            }

            txtUsuario.Focus();
        }

        // =================================================================
        // A PARTIR DAQUI E COM VOCE
        // =================================================================

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void LimparCampos()
        {
            // TODO 1 - escreva o corpo de LimparCampos.
            //
            //   Apague os dois campos, apague o texto de lblForca e de
            //   lblStatus, zere a variavel tentativas, ligue de volta o
            //   botao com btnEntrar.Enabled = true, e devolva o foco para
            //   txtUsuario.
            //
            //   Repare que este metodo vai ter TRES chamadores ao fim da
            //   noite: o botao Limpar, o Entrar, e a volta da tela
            //   principal. Metodo bom e assim - ele atrai chamador.
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            // TODO 2 - barre os campos vazios, no molde que protege:
            //
            //     if (txtUsuario.Text == "" || txtSenha.Text == "")
            //     {
            //         Avisar("Informe o usuario e a senha.");
            //         return;
            //     }
            //
            //   E "if", nao "switch": a pergunta e sobre UMA COISA - o
            //   campo estar vazio - e a resposta e sim ou nao.

            // TODO 3 - pergunte ao DAO quem e o dono desse login e dessa
            //          senha:
            //
            //     achado = UsuarioDAO.Autenticar(txtUsuario.Text,
            //                                    txtSenha.Text);
            //
            //   Repare no que esta tela NAO faz: ela nao sabe se a conta
            //   veio de uma lista ou do MySQL. Ela chama o NOME do metodo;
            //   quem sabe o CORPO e o UsuarioDAO. E por isso que no Bloco 6
            //   esta linha aqui nao vai mudar.
            //
            //   APAGUE a linha-tampao abaixo.
            Usuario achado = null;

            // TODO 4 - se achado for null, conte mais uma tentativa errada,
            //          chame MostrarErroDeTentativa() e saia com return:
            //
            //     tentativas++;
            //     MostrarErroDeTentativa();
            //     return;
            //
            //   De novo "if": achou ou nao achou e sim/nao.

            // TODO 5 - chegou ate aqui, entrou. Zere as tentativas antes de
            //          chamar o Entrar:
            //
            //     tentativas = 0;
            //
            //   A chamada de Entrar ja esta escrita na linha abaixo: ela
            //   fica para o projeto compilar limpo enquanto os TODOs 3, 4 e
            //   5 estao vazios. Ela nao faz nada por enquanto, porque o
            //   corpo de Entrar tambem esta vazio - e o TODO 7.
            Entrar(achado);
        }

        private void MostrarErroDeTentativa()
        {
            // TODO 6 - O SWITCH DA NOITE.
            //
            //   Sao tres respostas para o MESMO valor - a variavel
            //   tentativas - comparado a constantes: 1, 2 e 3. Isso e a
            //   definicao do switch, e e por isso que aqui ele fica melhor
            //   que uma escada de else if.
            //
            //     switch (tentativas)
            //     {
            //         case 1:  ...  break;
            //         case 2:  ...  break;
            //         case 3:  ...  break;
            //     }
            //
            //   case 1: avise que restam 2 tentativas
            //   case 2: avise que resta 1
            //   case 3: avise que o botao foi bloqueado, e faca
            //           btnEntrar.Enabled = false;
            //
            //   Nos tres, escreva o aviso no lblStatus, limpe o campo da
            //   senha e devolva o foco:
            //
            //     txtSenha.Text = "";
            //     txtSenha.Focus();
            //
            //   NAO esqueca o break de cada case: sem ele o C# nem compila,
            //   e nisso ele e mais rigoroso que o C e que o Java.
            //
            //   APAGUE a linha-tampao abaixo.
            lblStatus.Text = "Usuario ou senha incorretos. (tentativa "
                           + tentativas + " de " + MAXIMO_DE_TENTATIVAS + ")";
        }

        private void Entrar(Usuario quemEntrou)
        {
            // TODO 7 - guarde quem entrou e abra a tela principal:
            //
            //     Sessao.UsuarioLogado = quemEntrou;
            //
            //     frmPrincipal tela = new frmPrincipal();
            //     this.Hide();
            //     tela.ShowDialog();
            //     this.Show();
            //
            //     Sessao.Encerrar();
            //     LimparCampos();
            //
            //   A classe Sessao NAO EXISTE ainda. Voce cria ela no Bloco 5,
            //   com Adicionar > Classe... > Sessao.cs. Enquanto ela nao
            //   existir, deixe este metodo vazio: o projeto tem de
            //   continuar compilando o tempo todo.
        }

        private void txtSenha_TextChanged(object sender, EventArgs e)
        {
            // TODO 8 - ligue os dois metodos de baixo, nesta ordem:
            //
            //     int forca = ForcaDaSenha(txtSenha.Text);
            //     lblForca.Text = BarraDeForca(forca);
            //
            //   Este evento ja esta ligado no Designer. Digitar o metodo na
            //   mao compila, mas ele nunca roda - quem liga metodo a evento
            //   e o icone do raio na janela Propriedades.
        }

        private int ForcaDaSenha(string senha)
        {
            // TODO 9 - AQUI MORA O foreach.
            //
            //   Ele percorre os caracteres que EXISTEM na senha. Quem manda
            //   no numero de voltas e a propria senha - voce nao escolhe
            //   nada - e a unica coisa que interessa e olhar cada
            //   caractere. Se o '4' e o quarto ou o quinto nao muda nada.
            //   Por isso aqui e foreach.
            //
            //     se a senha for "", devolva 0;
            //
            //     bool temNumero = false;
            //     bool temLetra  = false;
            //
            //     foreach (char c in senha)
            //     {
            //         c entre '0' e '9'  -> temNumero = true;
            //         c entre 'a' e 'z'
            //          ou entre 'A' e 'Z' -> temLetra = true;
            //     }
            //
            //     a forca comeca em 1;
            //     +1 se senha.Length >= 6;
            //     +1 se temNumero e temLetra ao mesmo tempo;
            //
            //   APAGUE a linha-tampao abaixo.
            return 0;
        }

        private string BarraDeForca(int forca)
        {
            // TODO 10 - AQUI MORA O for, a quinze linhas do foreach.
            //
            //   A diferenca esta a olho nu: NAO existe colecao nenhuma para
            //   percorrer. O que existe e um NUMERO de tracinhos, calculado
            //   no metodo de cima. Quem manda nas voltas e a conta, nao os
            //   dados - e por isso o foreach nao serve aqui.
            //
            //     string barra = "";
            //
            //     for (int i = 1; i <= forca; i++)
            //     {
            //         barra = barra + "#";
            //     }
            //
            //   E, do lado, um switch pequeno para a palavra:
            //
            //     switch (forca)
            //     {
            //         case 0:  return "";
            //         case 1:  return barra + "  fraca";
            //         case 2:  return barra + "  media";
            //         default: return barra + "  boa";
            //     }
            //
            //   Repare no default: e o "qualquer outro caso". Aqui ele
            //   cobre o 3, e cobriria o 4 e o 5 se um dia existissem. O
            //   switch do TODO 6 nao tem default de proposito - la, so 1, 2
            //   e 3 existem.
            //
            //   APAGUE a linha-tampao abaixo.
            return "";
        }
    }
}
