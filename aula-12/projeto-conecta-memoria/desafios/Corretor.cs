using System;
using System.Collections.Generic;

namespace Conecta
{
    // O corretor: pega cada teste descrito no conteudo-conecta.json, chama o
    // metodo que voce escreveu em Desafios.cs e compara o resultado com o
    // esperado.
    //
    // Voce NAO precisa mexer aqui.
    //
    // ------------------------------------------------------------------
    //  NOTA DE MANUTENCAO (para quem cuida do material, nao para o aluno)
    //
    //  INVARIANTE: Executar() NUNCA le a Memoria. Ele monta a propria
    //  entrada, sempre, a partir do texto do JSON. E isso que garante que
    //  o resultado dos testes seja funcao SO do Desafios.cs compilado -
    //  nada que o aluno digite na mini-tela pode travar ou destravar uma
    //  aba. Quebrar esta regra quebra a credibilidade do aplicativo.
    //
    //  REGRA 2: nenhum ramo chama DOIS metodos do aluno. Se o ramo da aba 2
    //  chamasse o metodo da aba 1, a aba 2 ficaria vermelha por causa de um
    //  erro da aba 1 - e o diagnostico na tela estaria mentindo.
    //
    //  OS TRES SEPARADORES, encaixados nesta ordem:
    //     |  separa os ARGUMENTOS do metodo
    //     ;  separa os ITENS de uma lista
    //     ,  separa os CAMPOS de um item
    //  Nenhum valor de teste pode conter esses tres caracteres.
    // ------------------------------------------------------------------

    public class ResultadoTeste
    {
        public string Descricao;
        public string Esperado;
        public string Obtido;
        public bool Passou;
        public string Erro;      // preenchido quando o codigo do aluno estourou
    }

    public class ResultadoTopico
    {
        public List<ResultadoTeste> Testes = new List<ResultadoTeste>();
        public bool Resolvido;
        public int Passaram;

        // Nao existe estado "ainda nao comecou": um metodo vazio devolve o valor
        // padrao do tipo (0, false, "") e isso faz alguns testes passarem por
        // acidente. Contar quantos passaram e a unica leitura honesta.
        public int Total { get { return Testes.Count; } }
    }

    public static class Corretor
    {
        public static ResultadoTopico Corrigir(Topico topico)
        {
            ResultadoTopico saida = new ResultadoTopico();

            if (topico == null || topico.Desafio == null || topico.Desafio.Testes == null)
            {
                saida.Resolvido = false;
                return saida;
            }

            int passaram = 0;
            bool travou = false;

            foreach (Teste teste in topico.Desafio.Testes)
            {
                ResultadoTeste r = new ResultadoTeste();
                r.Descricao = teste.Descricao;
                r.Esperado = teste.Esperado;

                if (travou)
                {
                    // Um laco infinito ja apareceu neste desafio: nao adianta
                    // rodar os outros testes e deixar mais threads girando.
                    r.Obtido = "(nao testei)";
                    r.Erro = "Pulei este teste porque o anterior nao terminou.";
                    r.Passou = false;
                    saida.Testes.Add(r);
                    continue;
                }

                string id = topico.Id;
                string entrada = teste.Entrada;
                Execucao e = Sandbox.Rodar(id, delegate { return Executar(id, entrada); });

                if (e.EstourouOTempo)
                {
                    travou = true;
                    r.Obtido = "(nao terminou)";
                    r.Erro = Sandbox.Recado(e);
                    r.Passou = false;
                }
                else if (e.Falha != null)
                {
                    // Erro em tempo de execucao no codigo do aluno. O mais comum
                    // e o NullReferenceException, quando o "return null" ficou.
                    r.Obtido = "(o programa parou)";
                    r.Erro = Traduzir(e.Falha);
                    r.Passou = false;
                }
                else
                {
                    r.Obtido = e.Valor;
                    r.Passou = (r.Obtido == r.Esperado);
                }

                if (r.Passou) { passaram++; }
                saida.Testes.Add(r);
            }

            saida.Passaram = passaram;
            saida.Resolvido = (passaram == saida.Testes.Count && saida.Testes.Count > 0);

            return saida;
        }

        // Traduz a entrada de texto do JSON para a chamada de verdade.
        private static string Executar(string idTopico, string entrada)
        {
            string[] p = (entrada ?? "").Split('|');

            if (idTopico == "conta")
            {
                return Achatar(Desafios.CriarUsuario(Arg(p, 0), Arg(p, 1), Arg(p, 2)));
            }

            if (idTopico == "lista")
            {
                List<Usuario> contas = LerContas(Arg(p, 0));

                // O corretor monta o Usuario ELE MESMO - ver a REGRA 2 la em cima.
                string[] c = Arg(p, 1).Split(',');
                Usuario u = new Usuario();
                u.Nome = Arg(c, 0);
                u.Login = Arg(c, 1);
                u.Senha = Arg(c, 2);

                int id = Desafios.CriarConta(contas, u);

                // O Count no esperado e o que pega quem calcula o Id, devolve,
                // e esquece de acrescentar na lista.
                string ultimo = contas.Count > 0 ? contas[contas.Count - 1].Nome : "(vazia)";
                return id + "|" + contas.Count + "|" + ultimo;
            }

            if (idTopico == "entrar")
            {
                List<Usuario> contas = LerContas(Arg(p, 0));
                return Achatar(Desafios.Autenticar(contas, Arg(p, 1), Arg(p, 2)));
            }

            if (idTopico == "repetido")
            {
                List<Usuario> contas = LerContas(Arg(p, 0));
                return Desafios.LoginExiste(contas, Arg(p, 1)).ToString();
            }

            if (idTopico == "porteiro")
            {
                return Desafios.ValidarCadastro(Arg(p, 0), Arg(p, 1), Arg(p, 2), Arg(p, 3));
            }

            if (idTopico == "buscar")
            {
                List<Usuario> contas = LerContas(Arg(p, 0));
                int id = 0;
                int.TryParse(Arg(p, 1), out id);
                return Achatar(Desafios.BuscarPorId(contas, id));
            }

            if (idTopico == "numerar")
            {
                return Desafios.Numerar(LerContas(Arg(p, 0)));
            }

            if (idTopico == "sugerir")
            {
                List<Usuario> contas = LerContas(Arg(p, 0));
                return Desafios.SugerirLogin(contas, Arg(p, 1));
            }

            if (idTopico == "trocar")
            {
                List<Usuario> contas = LerContas(Arg(p, 0));
                bool ok = Desafios.TrocarSenha(contas, Arg(p, 1), Arg(p, 2), Arg(p, 3));

                // A senha DEPOIS da chamada e o que prova que a troca pegou:
                // devolver true sem mexer no objeto nao passa.
                string senha = "(nao achei)";
                foreach (Usuario u in contas)
                {
                    if (u.Login == Arg(p, 1)) { senha = u.Senha; break; }
                }
                return ok + "|" + senha;
            }

            if (idTopico == "remover")
            {
                List<Usuario> contas = LerContas(Arg(p, 0));
                int alvo = 0;
                int.TryParse(Arg(p, 1), out alvo);

                bool ok = Desafios.Remover(contas, alvo);

                // Quem sobrou, e em que ordem: pega quem devolve true sem
                // remover, e quem remove a conta errada.
                string sobraram = "";
                foreach (Usuario u in contas)
                {
                    if (sobraram != "") { sobraram += ", "; }
                    sobraram += u.Nome;
                }
                return ok + "|" + contas.Count + "|" + sobraram;
            }

            return "(topico desconhecido: " + idTopico + ")";
        }

        // Fora do intervalo devolve texto vazio em vez de estourar: um teste
        // que nao precisa do terceiro argumento simplesmente nao o escreve.
        private static string Arg(string[] p, int i)
        {
            return (i < p.Length) ? (p[i] ?? "") : "";
        }

        // A ARMADILHA, centralizada num lugar so: "".Split(';') NAO devolve um
        // array vazio - devolve um array com UM texto vazio dentro. Sem esta
        // guarda, o teste de "lista vazia" viraria uma lista com um usuario em
        // branco, e falharia pelo motivo errado.
        private static List<Usuario> LerContas(string bruto)
        {
            List<Usuario> lista = new List<Usuario>();
            if (string.IsNullOrEmpty(bruto)) { return lista; }

            foreach (string item in bruto.Split(';'))
            {
                string[] c = item.Split(',');
                Usuario u = new Usuario();
                u.Id = lista.Count + 1;          // Id previsivel, de proposito
                u.Nome = Arg(c, 0);
                u.Login = Arg(c, 1);
                u.Senha = Arg(c, 2);
                lista.Add(u);
            }
            return lista;
        }

        private static string Achatar(Usuario u)
        {
            if (u == null) { return "(null)"; }
            return u.Id + "|" + u.Nome + "|" + u.Login + "|" + u.Senha;
        }

        // O nome cru da excecao nao diz nada para quem esta aprendendo.
        public static string Traduzir(Exception ex)
        {
            if (ex is NullReferenceException)
            {
                return "NullReferenceException: voce usou um objeto que nao existe. "
                     + "Quase sempre e um 'return null;' que ficou no lugar, ou um "
                     + "new que faltou.";
            }
            if (ex is InvalidOperationException)
            {
                return "InvalidOperationException: voce mexeu na lista enquanto o "
                     + "foreach estava percorrendo ela. Saia do laco na mesma hora "
                     + "em que remover - o return serve para isso.";
            }
            if (ex is ArgumentOutOfRangeException || ex is IndexOutOfRangeException)
            {
                return "Voce pediu uma posicao que nao existe na lista. Lembre que a "
                     + "primeira posicao e 0 e a ultima e Count - 1.";
            }
            return ex.GetType().Name + ": " + ex.Message;
        }
    }
}
