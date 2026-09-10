using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Acesso
{
    public class ResultadoCaso
    {
        public string Descricao;
        public string Esperado;
        public string Obtido;
        public bool Passou;
        public string Erro;
    }

    public class ResultadoDesafio
    {
        public int Numero;

        // pergunta 1
        public bool MetodoTemCorpo;

        // pergunta 2
        public List<ResultadoCaso> Casos = new List<ResultadoCaso>();
        public int CasosPassaram;

        // pergunta 3
        public bool TesteExiste;
        public int LinhasDoTeste;
        public bool TestePassa;
        public string ErroDoTeste;

        // pergunta 4
        public List<string> FronteirasFaltando = new List<string>();

        public bool MetodoOk
        {
            get { return MetodoTemCorpo && Casos.Count > 0 && CasosPassaram == Casos.Count; }
        }

        public bool TesteOk
        {
            get { return TesteExiste && TestePassa && FronteirasFaltando.Count == 0; }
        }

        public bool Resolvido
        {
            get { return MetodoOk && TesteOk; }
        }
    }

    /// <summary>
    /// O CORRETOR DA NOITE - quatro perguntas por desafio.
    ///
    ///   1. o metodo tem corpo?           (nao estourou NotImplementedException)
    ///   2. o metodo esta certo?          (os casos que a aba mostra)
    ///   3. o teste existe e passa?       (reflexao sobre o projeto de teste)
    ///   4. o teste cobre as fronteiras?  (reflexao sobre os [DataRow])
    ///
    /// A pergunta 4 e a que da dente ao exercicio: nao basta o metodo
    /// funcionar, o teste precisa provar que o aluno pensou no limite.
    ///
    /// NAO hospedamos o MSTest aqui dentro. O corretor le os atributos e
    /// invoca o metodo de teste por reflexao, capturando a excecao de
    /// assercao. So o pacote MSTest.TestFramework, que ja vem com o projeto
    /// de teste.
    /// </summary>
    public static class Corretor
    {
        public static ResultadoDesafio Conferir(Desafio d)
        {
            ResultadoDesafio r = new ResultadoDesafio();
            r.Numero = d.Numero;

            ConferirMetodo(d, r);
            ConferirTeste(d, r);

            return r;
        }

        // -------------------------------------------------- perguntas 1 e 2
        private static void ConferirMetodo(Desafio d, ResultadoDesafio r)
        {
            r.MetodoTemCorpo = true;
            bool naoTerminou = false;

            foreach (Caso c in d.Casos)
            {
                ResultadoCaso rc = new ResultadoCaso();
                rc.Descricao = c.Descricao + "   ->   " + Argumentos(c.Args);
                rc.Esperado = Mostrar(c.Esperado);

                // Depois do primeiro caso que nao termina, os outros nao rodam. Sao 2
                // segundos cada um: insistir nos cinco casos de um metodo travado
                // custava 10 segundos de janela parada, e o aluno ja tinha a resposta
                // no primeiro.
                if (naoTerminou)
                {
                    rc.Obtido = "(nao rodou)";
                    rc.Passou = false;
                    r.Casos.Add(rc);
                    continue;
                }

                Execucao e = Sandbox.Rodar(() => d.Chamar(c.Args));

                if (e.NaoImplementado)
                {
                    r.MetodoTemCorpo = false;
                    rc.Obtido = "(ainda sem corpo)";
                    rc.Passou = false;
                }
                else if (e.EstourouOTempo)
                {
                    naoTerminou = true;
                    rc.Obtido = "(nao terminou em 2 segundos)";
                    rc.Erro = "o seu metodo nao termina - procure um laco sem fim.";
                    rc.Passou = false;
                }
                else if (e.Erro != null)
                {
                    rc.Obtido = "(estourou)";
                    rc.Erro = Traduzir(e.Erro);
                    rc.Passou = false;
                }
                else
                {
                    rc.Obtido = Mostrar(e.Valor);
                    rc.Passou = rc.Obtido == rc.Esperado;
                }

                if (rc.Passou) { r.CasosPassaram = r.CasosPassaram + 1; }
                r.Casos.Add(rc);
            }
        }

        // -------------------------------------------------- perguntas 3 e 4
        private static void ConferirTeste(Desafio d, ResultadoDesafio r)
        {
            Type tipo = typeof(AcessoTestes.AcessoTests);
            MethodInfo metodo = null;

            // Procura em TUDO - publico, privado, de instancia e static - e nao so no
            // que serve. Achar o metodo errado e poder dizer POR QUE ele nao serve vale
            // mais que dizer "nao achei" para um metodo que esta na tela do aluno.
            foreach (MethodInfo m in tipo.GetMethods(BindingFlags.Public | BindingFlags.NonPublic
                                                     | BindingFlags.Instance | BindingFlags.Static))
            {
                if (m.Name == d.NomeDoTeste) { metodo = m; break; }
            }

            if (metodo == null)
            {
                r.TesteExiste = false;
                r.ErroDoTeste = "Nao achei o metodo de teste " + d.NomeDoTeste + ".";
                foreach (Fronteira f in d.Fronteiras) { r.FronteirasFaltando.Add(f.Descricao); }
                return;
            }

            if (metodo.IsStatic)
            {
                r.TesteExiste = false;
                r.ErroDoTeste = "Achei o " + d.NomeDoTeste + ", mas ele esta STATIC. "
                              + "Metodo de teste nao pode ser static - apague a palavra.";
                foreach (Fronteira f in d.Fronteiras) { r.FronteirasFaltando.Add(f.Descricao); }
                return;
            }

            if (!metodo.IsPublic)
            {
                r.TesteExiste = false;
                r.ErroDoTeste = "Achei o " + d.NomeDoTeste + ", mas ele nao esta PUBLIC. "
                              + "Escreva public void " + d.NomeDoTeste + "(...).";
                foreach (Fronteira f in d.Fronteiras) { r.FronteirasFaltando.Add(f.Descricao); }
                return;
            }

            r.TesteExiste = true;

            List<object[]> linhas = new List<object[]>();
            foreach (object o in metodo.GetCustomAttributes(typeof(DataRowAttribute), false))
            {
                linhas.Add(((DataRowAttribute)o).Data);
            }
            r.LinhasDoTeste = linhas.Count;

            if (linhas.Count == 0)
            {
                r.TestePassa = false;
                r.ErroDoTeste = "O metodo de teste existe, mas nao tem nenhum [DataRow].";
                foreach (Fronteira f in d.Fronteiras) { r.FronteirasFaltando.Add(f.Descricao); }
                return;
            }

            // [DataRow] sem [DataTestMethod] em cima: o corretor conseguiria rodar assim,
            // porque ele mesmo invoca as linhas - mas o MSTest NAO. No Test Explorer o
            // metodo apareceria vermelho, ou nem apareceria. Fechar a aba aqui seria
            // ensinar uma coisa que a ferramenta de verdade recusa, e a Aula 9 poe as
            // duas lado a lado justamente para mostrar que medem a mesma coisa.
            if (metodo.GetCustomAttributes(typeof(DataTestMethodAttribute), false).Length == 0)
            {
                r.TestePassa = false;
                r.ErroDoTeste = "O seu teste tem [DataRow], mas falta o [DataTestMethod] em "
                              + "cima. Com [TestMethod] as linhas da tabela nao rodam.";
                return;
            }

            // pergunta 4 - cada fronteira precisa de ao menos uma linha que a satisfaca
            List<object[]> valores = SemADescricao(metodo, linhas);

            bool fronteiraTravou = false;

            foreach (Fronteira f in d.Fronteiras)
            {
                bool achou = false;

                // Se uma fronteira ja nao terminou, as outras deste desafio tambem nao
                // vao - e a mesma chamada ao mesmo metodo travado. Sem este corte eram
                // 2 segundos por fronteira por linha: 18 segundos de janela parada num
                // desafio so.
                if (fronteiraTravou)
                {
                    r.FronteirasFaltando.Add(f.Descricao);
                    continue;
                }

                foreach (object[] linha in valores)
                {
                    // PELO SANDBOX, e nao direto. Algumas fronteiras chamam o codigo do
                    // aluno para decidir - as do desafio 6 perguntam a forca da senha
                    // para o ForcaDaSenha dele. Avaliadas aqui na thread da janela, um
                    // laco sem fim no metodo do aluno congelava a janela PARA SEMPRE,
                    // sem mensagem e sem pintar nada, porque o Conferir() roda no Shown.
                    // So o Gerenciador de Tarefas resolvia, e nada na tela dizia qual
                    // desafio tinha matado o programa.
                    Execucao e = Sandbox.Rodar(() => (object)f.Satisfaz(linha));

                    if (e.EstourouOTempo)
                    {
                        fronteiraTravou = true;
                        break;
                    }

                    if (e.Erro == null && e.Valor is bool && (bool)e.Valor)
                    {
                        achou = true;
                        break;
                    }
                    // erro: esta linha nao satisfaz, e segue para a proxima
                }

                if (!achou) { r.FronteirasFaltando.Add(f.Descricao); }
            }

            if (fronteiraTravou)
            {
                r.TestePassa = false;
                r.ErroDoTeste = "Para saber se o seu teste cobre as fronteiras eu preciso "
                              + "rodar o metodo do desafio anterior, e ele nao termina. "
                              + "Procure um laco sem fim la atras.";
                return;
            }

            // pergunta 3 - o teste roda e passa?
            r.TestePassa = RodarTeste(tipo, metodo, linhas, r);
        }

        /// <summary>
        /// Tira da linha o parametro chamado "caso" - a frase que o aluno
        /// escreve para aparecer quando o caso fica vermelho.
        ///
        /// Sem isto a pergunta 4 aceita coisa que nao devia: a descricao e
        /// texto livre, e "bem longa" tem exatamente 9 caracteres, entao ela
        /// sozinha fechava a fronteira "uma entrada de 9 caracteres" que o
        /// teste nao cobria. Fronteira tem de ser satisfeita por ENTRADA ou
        /// por ESPERADO, nunca pelo recado.
        ///
        /// Se o aluno chamar o parametro de outra coisa, a linha inteira
        /// volta a ser olhada - e o comportamento antigo, e o molde do
        /// desafio 1 ensina o nome certo.
        /// </summary>
        private static List<object[]> SemADescricao(MethodInfo metodo, List<object[]> linhas)
        {
            ParameterInfo[] ps = metodo.GetParameters();
            int fora = -1;

            for (int i = 0; i < ps.Length; i++)
            {
                if (string.Equals(ps[i].Name, "caso", StringComparison.OrdinalIgnoreCase))
                {
                    fora = i;
                    break;
                }
            }

            if (fora < 0) { return linhas; }

            List<object[]> limpas = new List<object[]>();

            foreach (object[] linha in linhas)
            {
                if (fora >= linha.Length) { limpas.Add(linha); continue; }

                List<object> sobrou = new List<object>();

                for (int i = 0; i < linha.Length; i++)
                {
                    if (i != fora) { sobrou.Add(linha[i]); }
                }

                limpas.Add(sobrou.ToArray());
            }

            return limpas;
        }

        private static bool RodarTeste(Type tipo, MethodInfo metodo, List<object[]> linhas,
                                       ResultadoDesafio r)
        {
            object instancia;

            try
            {
                instancia = Activator.CreateInstance(tipo);
            }
            catch (Exception ex)
            {
                r.ErroDoTeste = "Nao consegui criar a classe de teste: " + ex.Message;
                return false;
            }

            int pedidos = metodo.GetParameters().Length;

            foreach (object[] linha in linhas)
            {
                // Sem esta conferencia, o Invoke lanca TargetParameterCountException e o
                // aluno le "Parameter count mismatch" em ingles, sem nenhum numero. E o
                // erro mais comum de quem copia o molde do desafio 1 para um desafio de
                // mais parametros, e a conta que resolve cabe na propria mensagem.
                if (linha.Length != pedidos)
                {
                    r.ErroDoTeste = "O seu " + metodo.Name + " pede " + pedidos
                        + " valores, e este [DataRow] tem " + linha.Length + ". "
                        + "Conte os parametros do metodo e os valores da linha.";
                    return false;
                }

                Execucao e = Sandbox.Rodar(() =>
                {
                    metodo.Invoke(instancia, linha);
                    return null;
                });

                if (e.EstourouOTempo)
                {
                    r.ErroDoTeste = "O seu teste passou de 2 segundos e foi interrompido.";
                    return false;
                }

                if (e.Erro != null)
                {
                    Exception real = e.Erro is TargetInvocationException
                        ? ((TargetInvocationException)e.Erro).InnerException
                        : e.Erro;

                    if (real is AssertFailedException)
                    {
                        r.ErroDoTeste = "O seu teste ficou VERMELHO: " + real.Message;
                    }
                    else if (real is NotImplementedException)
                    {
                        r.ErroDoTeste = "O seu teste chama um metodo que ainda nao tem corpo.";
                    }
                    else
                    {
                        r.ErroDoTeste = "O seu teste estourou: " + Traduzir(real);
                    }

                    return false;
                }
            }

            return true;
        }

        // -------------------------------------------------- apresentacao
        private static string Mostrar(object valor)
        {
            if (valor == null) { return "(null)"; }
            if (valor is bool) { return ((bool)valor) ? "true" : "false"; }
            if (valor is string && (string)valor == "") { return "(texto vazio)"; }
            return valor.ToString();
        }

        private static string Argumentos(object[] args)
        {
            string fora = "";

            for (int i = 0; i < args.Length; i++)
            {
                if (i > 0) { fora = fora + ", "; }

                if (args[i] is string)
                {
                    string s = (string)args[i];
                    fora = fora + (s == "" ? "\"\"" : "\"" + s + "\"");
                }
                else
                {
                    fora = fora + Mostrar(args[i]);
                }
            }

            return fora;
        }

        private static string Traduzir(Exception ex)
        {
            if (ex is NullReferenceException)
            {
                return "voce usou um objeto que nao existe (NullReferenceException).";
            }
            if (ex is IndexOutOfRangeException || ex is ArgumentOutOfRangeException)
            {
                return "voce passou de um limite de posicao (indice fora do intervalo).";
            }
            if (ex is DivideByZeroException)
            {
                return "divisao por zero.";
            }
            if (ex is FormatException)
            {
                return "conversao de texto para numero falhou (FormatException).";
            }

            return ex.GetType().Name + ": " + ex.Message;
        }
    }
}
