using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Conecta
{
    // O aluno completa os metodos em Desafios.cs e roda com F5.
    // O aplicativo chama o que ele escreveu de dois jeitos:
    //   - o Corretor, que compara com o esperado e destrava a proxima aba;
    //   - a Bancada, a mini-tela que roda o metodo com o que ele digitou.
    //
    // O lado esquerdo (o ensino) vem de ../comum/PainelEnsino.cs.

    public class frmProva : Form
    {
        // Calculadas a partir do tamanho da tela, em MedirTela().
        private int largEnsino = 620;
        private int largDesafio = 560;

        private Conteudo conteudo;
        private TabControl abas;
        private Label rodape;
        private Label sessao;

        // Campo, e nao variavel local do construtor: o EnviarRespostas precisa
        // desabilitar o botao enquanto os testes rodam na thread da tela.
        private Button btnEnviar;

        // O nome vem do desafio 0, e nao de um campo da tela. Como o aluno
        // pode resolve-lo a qualquer momento, isto e lido toda vez em vez de
        // ser guardado uma vez so.
        private static string Aluno()
        {
            try { return (Desafios.MeuNome() ?? "").Trim(); }
            catch (Exception) { return ""; }
        }

        private static bool NomeValido()
        {
            string n = Aluno();
            return n.Length >= 4 && n.IndexOf(' ') > 0;
        }

        private readonly List<Label> selos = new List<Label>();
        private readonly List<Panel> painelResultado = new List<Panel>();
        private readonly List<Panel> bancadas = new List<Panel>();
        private readonly List<bool> resolvido = new List<bool>();
        private readonly List<bool> livre = new List<bool>();
        private readonly List<SplitContainer> divisores = new List<SplitContainer>();

        public frmProva()
        {

            // MontarAbas ficava FORA deste try. Um topico incompleto no JSON
            // matava o aplicativo na abertura, na maquina do aluno, com a
            // caixa de erro do Windows e nenhuma pista do que fazer.
            try
            {
                conteudo = Conteudo.Carregar();
                MontarJanela();
                MontarAbas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Nao consegui montar o aplicativo a partir do conteudo-prova.json."
                    + "\r\n\r\n" + ex.Message,
                    "Conecta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Load += delegate { Close(); };
                return;
            }

            AbasPintadas.Ligar(abas, EstadoDaAba);
            AtualizarAbas();
            Pintura.Suavizar(this);
        }

        private void MontarJanela()
        {
            Text = conteudo.Aula + " - " + conteudo.Titulo;
            StartPosition = FormStartPosition.CenterScreen;

            // Tela cheia. O tamanho normal serve so para quando alguem
            // restaurar a janela.
            // O tamanho restaurado e quase o da tela de proposito: as larguras
            // das duas colunas sao decididas UMA vez, na montagem, e um tamanho
            // restaurado bem menor cortaria o texto que ja foi montado largo.
            Rectangle area = Screen.PrimaryScreen.WorkingArea;
            ClientSize = new Size(area.Width - 20, area.Height - 48);
            MinimumSize = new Size(1024, 600);
            WindowState = FormWindowState.Maximized;
            MedirTela(ClientSize.Width);
            BackColor = Color.White;

            Panel cabecalho = new Panel();
            cabecalho.Dock = DockStyle.Top;
            cabecalho.Height = 62;
            cabecalho.BackColor = Paleta.Dark;

            Label titulo = new Label();
            titulo.Text = conteudo.Titulo ?? "Conecta";
            titulo.Font = UI.H1;
            titulo.ForeColor = Color.White;
            titulo.AutoSize = true;
            titulo.Location = new Point(18, 8);
            cabecalho.Controls.Add(titulo);

            Label sub = new Label();
            sub.Text = conteudo.Aula + " - " + conteudo.Data + "   |   "
                     + (conteudo.Subtitulo ?? "complete o metodo em Desafios.cs, salve e rode com F5");
            sub.Font = UI.Mini;
            sub.ForeColor = Paleta.PurpleLt;
            sub.AutoSize = true;
            sub.Location = new Point(20, 36);
            cabecalho.Controls.Add(sub);

            // Quem esta logado na memoria - muda quando a bancada da aba 3 roda.
            sessao = new Label();
            sessao.Text = "";
            sessao.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            sessao.ForeColor = Color.White;
            sessao.AutoSize = true;
            sessao.TextAlign = ContentAlignment.MiddleRight;
            sessao.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            sessao.Visible = false;
            cabecalho.Controls.Add(sessao);

            Button btnFoco = new Button();
            btnFoco.Text = "So o desafio";
            btnFoco.Font = UI.Mini;
            btnFoco.FlatStyle = FlatStyle.Flat;
            btnFoco.ForeColor = Color.White;
            btnFoco.BackColor = Paleta.Purple;
            btnFoco.FlatAppearance.BorderColor = Paleta.PurpleLt;
            btnFoco.Size = new Size(110, 28);
            btnFoco.Location = new Point(0, 10);   // x definido em PosicionarCabecalho
            btnFoco.Click += delegate
            {
                foreach (SplitContainer d in divisores)
                {
                    d.Panel1Collapsed = !d.Panel1Collapsed;
                }
            };
            // Em modo de prova a coluna de ensino nao existe: o botao so
            // teria como deixar meia tela em branco no meio do cronometro.
            btnFoco.Visible = !conteudo.ModoProva;
            cabecalho.Controls.Add(btnFoco);

            btnEnviar = new Button();
            btnEnviar.Text = "Enviar as minhas respostas";
            btnEnviar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEnviar.FlatStyle = FlatStyle.Flat;
            btnEnviar.ForeColor = Color.White;
            btnEnviar.BackColor = Paleta.Mint;
            btnEnviar.FlatAppearance.BorderSize = 0;
            btnEnviar.Size = new Size(210, 30);
            btnEnviar.Location = new Point(0, 9);   // x definido em PosicionarCabecalho
            btnEnviar.Click += delegate { EnviarRespostas(); };
            cabecalho.Controls.Add(btnEnviar);

            Button btnNaoEntra = new Button();
            btnNaoEntra.Text = "O que NAO entra hoje";
            btnNaoEntra.Font = UI.Mini;
            btnNaoEntra.FlatStyle = FlatStyle.Flat;
            btnNaoEntra.ForeColor = Color.White;
            btnNaoEntra.BackColor = Paleta.Purple;
            btnNaoEntra.FlatAppearance.BorderColor = Paleta.PurpleLt;
            btnNaoEntra.Size = new Size(170, 28);
            btnNaoEntra.Location = new Point(0, 10);   // x definido em PosicionarCabecalho
            btnNaoEntra.Click += delegate { MostrarNaoEntra(); };
            cabecalho.Controls.Add(btnNaoEntra);

            // Os botoes sao colocados a partir da largura REAL do cabecalho,
            // e nao de ClientSize no construtor: ali ClientSize ainda vinha de
            // Screen.WorkingArea, que num computador de dois monitores mede a
            // area combinada. Os botoes nasciam a 4670 px, fora da tela.
            EventHandler porA = delegate
            {
                int dir = cabecalho.ClientSize.Width;
                if (dir <= 0) { return; }

                btnEnviar.Left   = dir - btnEnviar.Width - 20;
                btnNaoEntra.Left = btnEnviar.Left - btnNaoEntra.Width - 10;
                btnFoco.Left     = btnNaoEntra.Left - btnFoco.Width - 10;
            };
            cabecalho.Resize += porA;
            Shown += porA;
            porA(this, EventArgs.Empty);

            // A HORA DA COMPILACAO. O Visual Studio pergunta "rodar a ultima
            // compilacao bem-sucedida?" quando o build falha; quem clica em Sim
            // passa dez minutos achando que o proprio codigo nao funciona.
            // Este relogio denuncia o executavel velho.
            sub.Text += "   |   build de " + HoraDoBuild();

            rodape = new Label();
            rodape.Dock = DockStyle.Bottom;
            rodape.Height = 30;
            rodape.TextAlign = ContentAlignment.MiddleLeft;
            rodape.Padding = new Padding(16, 0, 0, 0);
            rodape.Font = UI.Mini;
            rodape.BackColor = Paleta.Tint;
            rodape.ForeColor = Paleta.Body;

            abas = new TabControl();
            abas.Dock = DockStyle.Fill;
            abas.Font = new Font("Segoe UI", 9F);
            abas.Padding = new Point(10, 6);
            abas.Selecting += Abas_Selecting;
            abas.Selected += delegate { CorrigirVisiveis(); };

            Controls.Add(abas);
            Controls.Add(rodape);
            Controls.Add(cabecalho);
        }

        // As duas colunas crescem com a tela, ate um limite: linha de texto
        // larga demais fica ruim de ler, e o desafio nao precisa de mais que
        // isso para caber a mini-tela.
        private void MedirTela(int largura)
        {
            largDesafio = Limitar((int)(largura * 0.30), 440, 780);
            largEnsino = Limitar((int)(largura * 0.34), 440, 900);

            // As duas mais a moldura tem que caber na janela, senao o divisor
            // encolhe uma delas e o texto ja montado fica cortado.
            int folga = 2 * 40 + 2 * SystemInformation.VerticalScrollBarWidth + 12;
            int sobra = largura - folga - largEnsino - largDesafio;

            if (sobra < 0)
            {
                largEnsino += (int)(sobra * 0.55);
                largDesafio += sobra - (int)(sobra * 0.55);
                if (largEnsino < 320) { largEnsino = 320; }
                if (largDesafio < 340) { largDesafio = 340; }
            }
        }

        private static int Limitar(int valor, int minimo, int maximo)
        {
            if (valor < minimo) { return minimo; }
            if (valor > maximo) { return maximo; }
            return valor;
        }

        // --------------------------------------------------------------
        //  O ENVIO
        // --------------------------------------------------------------

        private void EnviarRespostas()
        {
            // Corrige tudo antes de enviar: o que vai para o professor tem que
            // ser o resultado do codigo que esta compilado agora, e nao o que
            // sobrou de uma correcao preguicosa de dez minutos atras.
            List<RespostaAluno> lista = new List<RespostaAluno>();

            for (int i = 0; i < conteudo.Topicos.Count; i++)
            {
                Topico t = conteudo.Topicos[i];
                ResultadoTopico r;

                try { r = Corretor.Corrigir(t); }
                catch (Exception) { r = new ResultadoTopico(); }

                RespostaAluno a = new RespostaAluno();
                a.Questao = t.Id;
                a.Numero = t.Numero;
                a.Metodo = (t.Desafio == null ? t.Titulo : t.Desafio.Metodo);
                a.Passaram = r.Passaram;
                a.Total = r.Total;
                a.Resolvido = r.Resolvido;
                lista.Add(a);

                resolvido[i] = r.Resolvido;
            }
            AtualizarAbas();

            if (!NomeValido())
            {
                MessageBox.Show(
                    "Antes de enviar, resolva o desafio 0.\r\n\r\n"
                    + "Escreva o seu nome completo no metodo MeuNome, em Desafios.cs, "
                    + "salve, feche o programa e rode de novo com F5.\r\n\r\n"
                    + "Sem o nome, o professor nao tem como saber de quem sao as "
                    + "respostas - e o envio nao serviria para nada.",
                    "Falta o seu nome", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                abas.SelectedIndex = 0;
                return;
            }

            Cursor = Cursors.WaitCursor;
            btnEnviar.Enabled = false;
            btnEnviar.Text = "Enviando...";
            ResultadoEnvio env = null;
            try { env = Envio.Enviar(Aluno(), lista); }
            catch (Exception ex)
            {
                // Endereco de servidor digitado errado faz o proprio construtor
                // do MySqlConnection lancar. Sem este catch, o clique no Enviar
                // mata o aplicativo no meio da prova.
                MessageBox.Show(
                    "Nao consegui enviar.\r\n\r\n" + Conexao.Traduzir(ex)
                    + "\r\n\r\nChame o professor antes de fechar o programa.",
                    "Envio das respostas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            finally
            {
                Cursor = Cursors.Default;
                btnEnviar.Enabled = true;
                btnEnviar.Text = "Enviar as minhas respostas";
            }

            MostrarResultadoDoEnvio(env, lista);
        }

        private void MostrarResultadoDoEnvio(ResultadoEnvio env, List<RespostaAluno> lista)
        {
            int resolvidas = 0;
            foreach (RespostaAluno a in lista) { if (a.Resolvido) { resolvidas++; } }

            string texto = "Enviado por: " + Aluno() + "\r\n";
            texto += "Questoes resolvidas: " + resolvidas + " de " + lista.Count + "\r\n\r\n";

            if (env.ArquivoOk)
            {
                texto += "GRAVADO NESTA MAQUINA\r\n" + env.CaminhoArquivo + "\r\n\r\n";

                if (Conexao.INSTRUCAO_ENTREGA != "")
                {
                    texto += "ENTREGA\r\n"
                           + Conexao.INSTRUCAO_ENTREGA + "\r\n\r\n";
                }
            }
            else
            {
                texto += "NAO CONSEGUI GRAVAR O ARQUIVO NESTA MAQUINA.\r\n"
                       + "Avise o professor antes de fechar o programa.\r\n\r\n";
            }

            MessageBoxIcon icone;

            if (!env.BancoTentado)
            {
                texto += "PRONTO - a sua resposta esta salva nesta maquina.";
                icone = MessageBoxIcon.Information;
            }
            else if (env.BancoOk)
            {
                texto += "ENVIADO PARA O SERVIDOR com sucesso.";
                icone = MessageBoxIcon.Information;
            }
            else
            {
                texto += "NAO FOI PARA O SERVIDOR.\r\n" + env.ErroBanco + "\r\n\r\n"
                       + "A sua resposta esta salva nesta maquina, no arquivo acima. "
                       + "Avise o professor - ninguem perde nada por causa disto.";
                icone = MessageBoxIcon.Warning;
            }

            MessageBox.Show(texto, "Envio das respostas", MessageBoxButtons.OK, icone);

            AbrirAPasta(env);
        }

        // O arquivo nasce em bin\Debug\net8.0-windows\, tres pastas abaixo do
        // projeto e no diretorio que a turma aprendeu a tratar como lixo.
        // Sem isto, ninguem acha o proprio arquivo as 21h50.
        //
        // Vem DEPOIS da caixa de mensagem, e nao antes: assim a janela do
        // Explorer nao rouba o foco da caixa que o aluno precisa ler.
        private static void AbrirAPasta(ResultadoEnvio env)
        {
            if (!env.ArquivoOk) { return; }

            try
            {
                System.Diagnostics.Process.Start(
                    "explorer.exe", "/select,\"" + env.CaminhoArquivo + "\"");
            }
            catch (Exception)
            {
                // Se o Explorer nao abrir, o caminho ja esta escrito na caixa
                // acima. Nao vale derrubar o envio por causa disto.
            }
        }

        private static string HoraDoBuild()
        {
            try
            {
                string exe = System.Reflection.Assembly.GetExecutingAssembly().Location;
                if (!string.IsNullOrEmpty(exe) && File.Exists(exe))
                {
                    return File.GetLastWriteTime(exe).ToString("HH:mm");
                }
            }
            catch (Exception) { }
            return "?";
        }

        private void MostrarNaoEntra()
        {
            string texto = "Estes assuntos NAO entram hoje:\r\n\r\n";

            if (conteudo.ForaDaRevisao != null)
            {
                foreach (string item in conteudo.ForaDaRevisao)
                {
                    texto += "   -  " + item + "\r\n";
                }
            }

            texto += "\r\nSe voce viu alguma dessas coisas pesquisando por conta, otimo -\r\n";
            texto += "guarde para depois. Hoje o assunto sao os " + conteudo.Topicos.Count
                   + " desafios das abas.";

            MessageBox.Show(texto, "Fora desta aula", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        private void MontarAbas()
        {
            for (int i = 0; i < conteudo.Topicos.Count; i++)
            {
                Topico t = conteudo.Topicos[i];

                TabPage pagina = new TabPage();
                pagina.Text = " " + t.Numero + ". " + t.Titulo + " ";
                pagina.BackColor = Color.White;

                SplitContainer div = new SplitContainer();
                div.Dock = DockStyle.Fill;
                div.SplitterWidth = 6;
                div.BackColor = Paleta.Line;
                div.Panel1.BackColor = Color.White;
                div.Panel2.BackColor = Paleta.Tint;
                div.Panel1.AutoScroll = true;
                div.Panel2.AutoScroll = true;
                // Sem mexer em Panel1MinSize/Panel2MinSize: atribuir esses
                // valores antes de o controle ter largura de verdade lanca
                // InvalidOperationException. Quem limita a largura util e o
                // AjustarDivisores, depois que a janela existe.
                divisores.Add(div);

                // Em modo de prova a coluna de ensino nem e montada: o aluno ve
                // o enunciado e os testes, e mais nada.
                if (!conteudo.ModoProva)
                {
                    PainelEnsino.Montar(div.Panel1, t, largEnsino, conteudo.AvisoNovo);
                }
                div.Panel1Collapsed = conteudo.ModoProva;
                MontarDesafio(div.Panel2, t);

                pagina.Controls.Add(div);
                abas.TabPages.Add(pagina);

                resolvido.Add(false);
                livre.Add(t.Livre);
            }
        }

        private void MontarDesafio(Panel destino, Topico t)
        {
            FlowLayoutPanel fluxo = UI.NovoFluxo(destino, largDesafio);

            fluxo.Controls.Add(UI.Titulo("Desafio " + t.Numero,
                "complete em Desafios.cs, salve e rode com F5", largDesafio));

            Label selo = new Label();
            selo.AutoSize = false;
            selo.Width = largDesafio;
            selo.Height = 34;
            selo.TextAlign = ContentAlignment.MiddleCenter;
            selo.Font = UI.H2;
            selo.Margin = new Padding(0, 0, 0, 10);
            fluxo.Controls.Add(selo);
            selos.Add(selo);

            if (t.Desafio != null)
            {
                fluxo.Controls.Add(UI.Paragrafo(t.Desafio.Enunciado, largDesafio));
                fluxo.Controls.Add(UI.Subtitulo("O metodo que voce completa"));
                fluxo.Controls.Add(UI.Codigo(t.Desafio.Assinatura, largDesafio));

                Button btnDica = UI.Botao("Ver a dica", 120, false);
                btnDica.Visible = !conteudo.ModoProva;
                string dica = t.Desafio.Dica ?? "(sem dica)";
                int numero = t.Numero;
                btnDica.Click += delegate
                {
                    MessageBox.Show(dica, "Dica do desafio " + numero,
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                };
                fluxo.Controls.Add(btnDica);

                // O nome mudou de proposito. Desafios.cs e COMPILADO: dentro de
                // uma execucao este botao recomputa exatamente o mesmo resultado.
                // Quem editava o arquivo, clicava aqui e via o selo continuar
                // vermelho aprendia a coisa errada sobre o aplicativo.
                Button btnTestar = UI.Botao("Conferir de novo  (so muda depois do F5)",
                                            largDesafio, true);
                btnTestar.Click += delegate { CorrigirVisiveis(); };
                fluxo.Controls.Add(btnTestar);
            }

            // ---- a bancada entra ANTES dos resultados ----
            // Assim ela nao se mexe quando a lista de testes cresce ou encolhe.
            Panel bancada = PainelBancada.Montar(t.Bancada, largDesafio,
                delegate (string idBotao, Dictionary<string, string> valores)
                {
                    AoClicarNaBancada(t, idBotao, valores);
                });

            if (bancada != null)
            {
                fluxo.Controls.Add(UI.Subtitulo("A tela: rode o seu codigo de verdade"));
                fluxo.Controls.Add(bancada);
            }
            bancadas.Add(bancada);

            Panel resultados = new Panel();
            resultados.Width = largDesafio;
            resultados.AutoSize = true;
            resultados.Margin = new Padding(0, 0, 0, 16);
            fluxo.Controls.Add(resultados);
            painelResultado.Add(resultados);
        }

        // --------------------------------------------------------------
        //  CORRECAO
        // --------------------------------------------------------------

        // Preguicoso de proposito. Antes isto rodava TODOS os topicos a cada
        // clique - e agora tambem roda a cada clique na bancada. Corrigir so o
        // que o aluno pode estar vendo reduz a exposicao a um metodo que trava
        // ou estoura para o metodo em que ele esta trabalhando agora.
        private void CorrigirVisiveis()
        {
            for (int i = 0; i < conteudo.Topicos.Count; i++)
            {
                bool naSequencia = (i == 0) || resolvido[i - 1] || livre[i];
                bool interessa = naSequencia || i == abas.SelectedIndex;

                if (interessa) { Corrigir(i); }
                else { resolvido[i] = false; }
            }

            AtualizarAbas();
            UI.DesligarMnemonicos(this);
        }

        private void Corrigir(int i)
        {
            ResultadoTopico r = Corretor.Corrigir(conteudo.Topicos[i]);
            resolvido[i] = r.Resolvido;
            PintarSelo(selos[i], r);
            PintarResultados(painelResultado[i], r);
            PainelBancada.MarcarEstado(bancadas[i], r.Resolvido);
        }

        private void PintarSelo(Label selo, ResultadoTopico r)
        {
            if (r.Total == 0)
            {
                selo.Text = "(este desafio nao tem testes)";
                selo.BackColor = Paleta.Tint;
                selo.ForeColor = Paleta.Body;
                return;
            }

            if (r.Resolvido)
            {
                selo.Text = "RESOLVIDO - todos os testes passaram";
                selo.BackColor = Paleta.MintBg;
                selo.ForeColor = Paleta.Mint;
            }
            else
            {
                // Cuidado ao ler este numero: um metodo ainda vazio devolve o
                // valor padrao do tipo (0, false, "") e ja acerta algum teste
                // por acidente. So vale 100%.
                selo.Text = r.Passaram + " de " + r.Total + " testes passando";
                selo.BackColor = Paleta.AmberBg;
                selo.ForeColor = Paleta.Amber;
            }
        }

        private void PintarResultados(Panel destino, ResultadoTopico r)
        {
            destino.Controls.Clear();
            int y = 0;

            Label cab = new Label();
            cab.Text = "O que o corretor testou";
            cab.Font = UI.H2;
            cab.ForeColor = Paleta.Purple;
            cab.AutoSize = true;
            cab.Location = new Point(0, y);
            destino.Controls.Add(cab);
            y += 26;

            foreach (ResultadoTeste teste in r.Testes)
            {
                Panel linha = new Panel();
                linha.Width = largDesafio - 10;
                linha.BackColor = teste.Passou ? Paleta.MintBg : Paleta.RedBg;
                linha.Location = new Point(0, y);

                Label marca = new Label();
                marca.Text = teste.Passou ? "OK" : "X";
                marca.Font = UI.MonoB;
                marca.ForeColor = teste.Passou ? Paleta.Mint : Paleta.Red;
                marca.AutoSize = true;
                marca.Location = new Point(8, 8);
                linha.Controls.Add(marca);

                Label desc = new Label();
                desc.Text = teste.Descricao;
                desc.Font = UI.Mini;
                desc.ForeColor = Paleta.Ink;
                desc.AutoSize = true;
                desc.MaximumSize = new Size(largDesafio - 70, 0);
                desc.Location = new Point(42, 8);
                linha.Controls.Add(desc);

                int alt = Math.Max(30, desc.PreferredHeight + 14);

                if (!teste.Passou)
                {
                    Label det = new Label();
                    det.Text = "esperado: [" + teste.Esperado + "]";
                    det.Font = UI.Mono;
                    det.ForeColor = Paleta.Red;
                    det.AutoSize = true;
                    det.MaximumSize = new Size(largDesafio - 70, 0);
                    det.Location = new Point(42, alt);
                    linha.Controls.Add(det);
                    alt += det.PreferredHeight + 2;

                    Label obt = new Label();
                    obt.Text = "obtido:   [" + (teste.Obtido ?? "(null)") + "]";
                    obt.Font = UI.Mono;
                    obt.ForeColor = Paleta.Red;
                    obt.AutoSize = true;
                    obt.MaximumSize = new Size(largDesafio - 70, 0);
                    obt.Location = new Point(42, alt);
                    linha.Controls.Add(obt);
                    alt += obt.PreferredHeight + 4;

                    if (!string.IsNullOrEmpty(teste.Erro))
                    {
                        Label err = new Label();
                        err.Text = teste.Erro;
                        err.Font = UI.Mini;
                        err.ForeColor = Paleta.Amber;
                        err.AutoSize = true;
                        err.MaximumSize = new Size(largDesafio - 70, 0);
                        err.Location = new Point(42, alt);
                        linha.Controls.Add(err);
                        alt += err.PreferredHeight + 4;
                    }
                }

                linha.Height = alt + 6;
                destino.Controls.Add(linha);
                y += linha.Height + 6;
            }

            destino.Height = y + 8;
        }

        // --------------------------------------------------------------
        //  A BANCADA
        // --------------------------------------------------------------

        // Este aplicativo NAO tem mini-tela: nenhum topico do JSON traz o bloco
        // "bancada", entao PainelBancada.Montar devolve null e este metodo nunca
        // e chamado. Ele existe so para o formulario continuar sendo o mesmo das
        // Aulas 11 e 12, sem uma segunda versao para manter.
        private void AoClicarNaBancada(Topico t, string idBotao,
                                       Dictionary<string, string> valores)
        {
        }

        private void AtualizarSessao()
        {
            if (Memoria.Logado == null)
            {
                sessao.Visible = false;
            }
            else
            {
                sessao.Text = "Conectado como " + Memoria.Logado.Nome;
                sessao.Location = new Point(
                    sessao.Parent.ClientSize.Width - sessao.PreferredWidth - 20, 40);
                sessao.Visible = true;
            }
        }

        // --------------------------------------------------------------
        //  ABAS
        // --------------------------------------------------------------

        private EstadoAba EstadoDaAba(int indice)
        {
            if (resolvido[indice]) { return EstadoAba.Resolvida; }

            int liberadas = Destravadas.Contar(resolvido, livre, abas.TabPages.Count);
            return indice < liberadas ? EstadoAba.Liberada : EstadoAba.Travada;
        }

        private void AtualizarAbas()
        {
            int liberadas = Destravadas.Contar(resolvido, livre, abas.TabPages.Count);

            for (int i = 0; i < abas.TabPages.Count; i++)
            {
                Topico t = conteudo.Topicos[i];
                abas.TabPages[i].Text = AbasPintadas.Marcar(EstadoDaAba(i), t.Numero, t.Titulo);
            }
            abas.Invalidate();

            int feitos = 0;
            foreach (bool b in resolvido) { if (b) { feitos++; } }

            // Sem cadeado e sem lista em memoria: aqui o rodape so conta questoes.
            rodape.Text = "Resolvidas: " + feitos + " de " + resolvido.Count
                        + "     |     " + (NomeValido() ? Aluno()
                              : "SEM NOME - resolva o desafio 0")
                        + "     |     escreva em Desafios.cs, salve, feche o programa e rode com F5";
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            AjustarLarguras();

            // A primeira correcao sai do construtor: a janela pinta primeiro, e
            // um metodo do aluno que trave nao impede mais o aplicativo de abrir.
            BeginInvoke((MethodInvoker)delegate { CorrigirVisiveis(); });
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AjustarLarguras();
        }

        private void AjustarLarguras()
        {
            // UI.NovoFluxo cria o fluxo com largura+40, e o AutoScroll vertical
            // ainda rouba a largura da propria barra. Sem contar as duas, sobra
            // uma barra de rolagem horizontal parasita na coluna do desafio.
            int folga = 40 + SystemInformation.VerticalScrollBarWidth;
            double fracao = largEnsino / (double)(largEnsino + largDesafio);

            Destravadas.AjustarDivisores(divisores, fracao,
                                         360, largDesafio + folga);

            foreach (SplitContainer div in divisores)
            {
                UI.Centralizar(div.Panel1);
                UI.Centralizar(div.Panel2);
            }

            if (abas != null && abas.TabPages.Count > 0)
            {
                // Dez abas numa linha so, em qualquer largura de tela.
                int larguraAba = Limitar(
                    (abas.ClientSize.Width - 8) / abas.TabPages.Count, 92, 160);
                abas.ItemSize = new Size(larguraAba, 30);
            }
        }

        private void Abas_Selecting(object sender, TabControlCancelEventArgs e)
        {
            Destravadas.BloquearSeTravada(e, resolvido, livre, conteudo);
        }
    }
}
