using System;
using System.Collections.Generic;

namespace Conecta
{
    // A OFICINA: o que acontece quando voce clica um botao da mini-tela.
    //
    // E a irma do Corretor. O Corretor prova que o seu codigo esta certo; a
    // Oficina roda o seu codigo de verdade, com os dados que voce digitou.
    //
    // Voce NAO precisa mexer aqui.
    //
    // ------------------------------------------------------------------
    //  NOTA DE MANUTENCAO
    //  Tres regras valem para TODOS os ramos:
    //    1. o metodo do aluno recebe uma COPIA da lista; a Memoria so e
    //       atualizada depois que o metodo voltou inteiro;
    //    2. todo objeto devolvido e testado contra null ANTES de virar .Nome;
    //    3. quem grava na Memoria e ESTE arquivo. O aluno nunca grava direto.
    // ------------------------------------------------------------------

    public class RespostaBancada
    {
        public bool Ok;
        public string Mensagem;   // a faixa colorida
        public string Detalhe;    // a linha menor: o que fazer agora
    }

    public static class Oficina
    {
        // Botoes que existem em toda bancada e nao dependem do codigo do aluno.
        public static bool Generico(string idBotao)
        {
            return idBotao == "limpar" || idBotao == "semear"
                || idBotao == "esvaziar" || idBotao == "sair";
        }

        public static RespostaBancada Executar(string idTopico, string idBotao,
                                               Dictionary<string, string> v)
        {
            // ---------- os genericos ----------
            if (idBotao == "limpar")
            {
                return Ok("Campos limpos.", null);
            }
            if (idBotao == "semear")
            {
                Memoria.Semear();
                return Ok("Semeei duas contas: ana / 1234 e bruno / abcd.",
                          "Sao contas de mentira, para voce ter com o que trabalhar.");
            }
            if (idBotao == "esvaziar")
            {
                Memoria.Limpar();
                return Ok("Memoria esvaziada. Zero contas, ninguem logado.", null);
            }
            if (idBotao == "sair")
            {
                Memoria.Logado = null;
                return Ok("Sessao encerrada.", null);
            }

            // ================= 1 - A CONTA =================
            if (idTopico == "conta" && idBotao == "salvar")
            {
                string nome = Txt(v, "nome"), login = Txt(v, "login"), senha = Txt(v, "senha");

                if (nome == "" || login == "" || senha == "")
                {
                    return Falha("Preencha os tres campos antes de clicar em Salvar.",
                                 "Esta conferencia e da mini-tela, nao do seu codigo.");
                }

                Usuario u = Desafios.CriarUsuario(nome, login, senha);

                if (u == null)
                {
                    return Falha("O seu metodo CriarUsuario devolveu null.",
                        "Enquanto o 'return null;' continuar la, nenhuma conta e criada. "
                      + "Escreva o new Usuario() em Desafios.cs, salve, e rode de novo com F5.");
                }

                u.Id = Memoria.Contas.Count + 1;     // nesta aba o Id e do aplicativo
                Memoria.Contas.Add(u);

                return Ok("O SEU CriarUsuario(\"" + nome + "\", \"" + login + "\", \""
                        + senha + "\") devolveu -> Usuario { Nome=\"" + u.Nome
                        + "\", Login=\"" + u.Login + "\" }",
                    "O Add nesta aba quem fez foi o aplicativo. No desafio 2 passa a ser voce.");
            }

            // ================= 2 - A LISTA =================
            if (idTopico == "lista" && idBotao == "salvar")
            {
                string nome = Txt(v, "nome"), login = Txt(v, "login"), senha = Txt(v, "senha");

                if (nome == "" || login == "" || senha == "")
                {
                    return Falha("Preencha os tres campos.", "Conferencia da mini-tela.");
                }

                Usuario u = Desafios.CriarUsuario(nome, login, senha);
                if (u == null)
                {
                    return Falha("O seu CriarUsuario (desafio 1) devolveu null.",
                                 "Sem objeto criado nao ha o que guardar na lista.");
                }

                int antes = Memoria.Contas.Count;
                List<Usuario> copia = Memoria.Copia();
                int id = Desafios.CriarConta(copia, u);

                if (copia.Count == antes)
                {
                    return Falha("O seu CriarConta devolveu o Id " + id
                               + ", mas a lista continua com " + copia.Count + " conta(s).",
                        "Faltou o contas.Add(u). Calcular o numero e guardar de fato sao "
                      + "dois passos diferentes.");
                }

                Memoria.Contas = copia;              // so promove se voltou inteiro

                string detalhe = (id == 0)
                    ? "Id 0? Voce usou contas.Count sem o + 1. A primeira conta nasce com Id 1."
                    : "Este numero saiu do SEU codigo. E o mesmo Id que o UsuarioDAO gera.";

                return Ok("CriarUsuario montou -> CriarConta guardou -> Id " + id
                        + ".  Contas na memoria: " + Memoria.Contas.Count + ".", detalhe);
            }

            // ================= 3 - ENTRAR =================
            if (idTopico == "entrar" && idBotao == "entrar")
            {
                string login = Txt(v, "login"), senha = Txt(v, "senha");

                if (Memoria.Contas.Count == 0)
                {
                    return Falha("Nao ha nenhuma conta na memoria para entrar.",
                        "Clique em Semear, ou volte ao desafio 1 e cadastre uma. "
                      + "A lista e a mesma nas oito abas.");
                }

                Usuario u = Desafios.Autenticar(Memoria.Copia(), login, senha);

                if (u == null)
                {
                    Memoria.Logado = null;
                    return Falha("Usuario ou senha incorretos.",
                        "Procurei por [" + login + "] - na lista tem: " + Memoria.Logins()
                      + ".  Se voce digitou certo, o problema esta no seu Autenticar: "
                      + "confira se os DOIS campos sao comparados com == ligados por && , "
                      + "e se o null so aparece DEPOIS que o foreach terminar.");
                }

                Memoria.Logado = u;
                return Ok("Bem-vindo, " + u.Nome + "!   (sessao aberta com o Id " + u.Id + ")",
                    "Guarde o Id: e ele que vai no UsuarioId de cada postagem, na Atividade 2.");
            }

            // ================= 4 - JA EXISTE =================
            if (idTopico == "repetido" && (idBotao == "salvar" || idBotao == "sem-porteiro"))
            {
                string nome = Txt(v, "nome"), login = Txt(v, "login"), senha = Txt(v, "senha");

                if (nome == "" || login == "" || senha == "")
                {
                    return Falha("Preencha os tres campos.", "Conferencia da mini-tela.");
                }

                if (idBotao == "salvar")
                {
                    if (Desafios.LoginExiste(Memoria.Copia(), login))
                    {
                        return Falha("Este usuario ja esta em uso. Escolha outro.",
                            "Quem barrou foi o SEU LoginExiste. Sem ele, a lista aceitaria "
                          + "duas contas com o mesmo login - e o Autenticar acharia sempre "
                          + "a primeira.");
                    }
                }

                Usuario u = Desafios.CriarUsuario(nome, login, senha);
                if (u == null)
                {
                    return Falha("O seu CriarUsuario (desafio 1) devolveu null.", null);
                }

                u.Id = Memoria.Contas.Count + 1;
                Memoria.Contas.Add(u);

                if (idBotao == "sem-porteiro")
                {
                    return Ok("Conta #" + u.Id + " salva SEM passar pelo porteiro.",
                        "Cadastre o mesmo login duas vezes e olhe a lista. Agora responda: "
                      + "quem entra quando alguem faz login com esse usuario?");
                }

                return Ok("Conta #" + u.Id + " salva para " + u.Nome + ".",
                          "O porteiro deixou passar porque este login estava livre.");
            }

            // ================= 5 - O PORTEIRO =================
            if (idTopico == "porteiro" && idBotao == "validar")
            {
                string nome = Txt(v, "nome"), login = Txt(v, "login");
                string senha = Txt(v, "senha"), confirmar = Txt(v, "confirmar");

                string erro = Desafios.ValidarCadastro(nome, login, senha, confirmar);

                if (!string.IsNullOrEmpty(erro))
                {
                    return Falha(erro,
                        "Foi o SEU ValidarCadastro que devolveu esta mensagem. No projeto de "
                      + "verdade ela aparece num MessageBox e o metodo para com return.");
                }

                Usuario u = new Usuario();
                u.Id = Memoria.Contas.Count + 1;
                u.Nome = nome; u.Login = login; u.Senha = senha;
                Memoria.Contas.Add(u);

                return Ok("Passou pelo porteiro. Conta #" + u.Id + " salva.",
                    "Texto vazio quer dizer 'nao achei problema nenhum'. Repare que o metodo "
                  + "so chega no ultimo return quando os quatro if deixam passar.");
            }

            // ================= 6 - BUSCAR A CONTA =================
            if (idTopico == "buscar" && idBotao == "buscar")
            {
                int id = 0;
                int.TryParse(Txt(v, "id"), out id);

                if (Memoria.Contas.Count == 0)
                {
                    return Falha("Nao ha nenhuma conta na memoria para buscar.",
                                 "Clique em Semear, ou cadastre uma no desafio 1.");
                }

                Usuario u = Desafios.BuscarPorId(Memoria.Copia(), id);

                if (u == null)
                {
                    return Falha("Nao existe conta com o Id " + id + ".",
                        "Devolver null e a resposta certa aqui - e nao um erro. Quem chamou "
                      + "e que precisa conferir antes de usar. Tente um Id que esteja na lista.");
                }

                return Ok("Achei: #" + u.Id + "  " + u.Nome + "  (login " + u.Login + ")",
                    "Com este metodo o seu UsuarioDAO fica completo: sao os quatro.");
            }

            // ================= 7 - NUMERAR =================
            if (idTopico == "numerar" && idBotao == "numerar")
            {
                if (Memoria.Contas.Count == 0)
                {
                    return Falha("Nao ha contas para numerar.",
                                 "Clique em Semear, ou cadastre no desafio 1.");
                }

                string texto = Desafios.Numerar(Memoria.Copia());

                if (string.IsNullOrEmpty(texto))
                {
                    return Falha("O seu Numerar devolveu texto vazio, com "
                               + Memoria.Contas.Count + " conta(s) na lista.",
                        "O laco nao entrou nenhuma vez. Confira a condicao: e "
                      + "i < contas.Count, e o i comeca em 0.");
                }

                return Ok(texto,
                    "Repare que o numero na tela e i + 1: a conta da posicao 0 e a "
                  + "numero 1 para quem le.");
            }

            // ================= 8 - SUGERIR LOGIN =================
            if (idTopico == "sugerir" && idBotao == "sugerir")
            {
                string desejado = Txt(v, "desejado");
                if (desejado == "")
                {
                    return Falha("Digite o login desejado.", "Conferencia da mini-tela.");
                }

                // O unico metodo do dia que pode nao terminar. Roda com prazo,
                // senao a janela congela e so o Gerenciador de Tarefas resolve.
                List<Usuario> copia = Memoria.Copia();
                Execucao e = Sandbox.Rodar("sugerir", delegate
                {
                    return Desafios.SugerirLogin(copia, desejado);
                });

                if (e.EstourouOTempo)
                {
                    return Falha("O seu while nao terminou.", Sandbox.Recado(e));
                }
                if (e.Falha != null)
                {
                    return Falha("O seu metodo parou no meio.", Corretor.Traduzir(e.Falha));
                }

                string sugestao = e.Valor ?? "";
                bool mudou = (sugestao != desejado);

                return Ok("Sugestao: " + (sugestao == "" ? "(texto vazio)" : sugestao),
                    mudou
                      ? "O login [" + desejado + "] estava ocupado, entao o seu while "
                        + "girou ate achar um livre."
                      : "O login [" + desejado + "] estava livre, entao o while nao "
                        + "entrou nenhuma vez. Tente com um que esta na lista.");
            }

            // ================= 9 - TROCAR A SENHA =================
            if (idTopico == "trocar" && idBotao == "trocar")
            {
                string login = Txt(v, "login");
                string atual = Txt(v, "atual");
                string nova = Txt(v, "nova");

                if (login == "" || atual == "" || nova == "")
                {
                    return Falha("Preencha os tres campos.", "Conferencia da mini-tela.");
                }
                if (Memoria.Contas.Count == 0)
                {
                    return Falha("Nao ha contas na memoria.",
                                 "Clique em Semear, ou cadastre no desafio 1.");
                }

                // Aqui a lista NAO e copia: o objeto que o metodo do aluno
                // recebe tem que ser o mesmo que esta na memoria - e esse e o
                // assunto da aba.
                bool ok = Desafios.TrocarSenha(Memoria.Contas, login, atual, nova);

                if (!ok)
                {
                    return Falha("Nao troquei: login ou senha atual nao conferem.",
                        "Procurei por [" + login + "] com a senha atual que voce "
                      + "digitou. Os logins na memoria sao: " + Memoria.Logins() + ".");
                }

                return Ok("Senha de [" + login + "] trocada para [" + nova + "].",
                    "Olhe a lista abaixo: a senha mudou LA DENTRO, e voce nao tirou e "
                  + "pos de volta. Agora va ao desafio 3 e entre com a senha nova.");
            }

            // ================= 10 - REMOVER A CONTA =================
            if (idTopico == "remover" && idBotao == "remover")
            {
                int id = 0;
                int.TryParse(Txt(v, "id"), out id);

                if (Memoria.Contas.Count == 0)
                {
                    return Falha("Nao ha contas para remover.",
                                 "Clique em Semear para ter com o que trabalhar.");
                }

                int antes = Memoria.Contas.Count;
                List<Usuario> copia = Memoria.Copia();

                Execucao e = Sandbox.Rodar("remover", delegate
                {
                    return Desafios.Remover(copia, id).ToString();
                });

                if (e.EstourouOTempo)
                {
                    return Falha("O seu Remover nao terminou.", Sandbox.Recado(e));
                }
                if (e.Falha != null)
                {
                    return Falha("O seu metodo parou no meio.", Corretor.Traduzir(e.Falha));
                }

                bool ok = (e.Valor == "True");

                if (ok && copia.Count == antes)
                {
                    return Falha("O seu Remover devolveu verdadeiro, mas a lista "
                               + "continua com " + antes + " conta(s).",
                        "Achar a conta e tirar da lista sao dois passos. Faltou o "
                      + "contas.Remove(u).");
                }

                Memoria.Contas = copia;

                if (Memoria.Logado != null && !Memoria.Contas.Contains(Memoria.Logado))
                {
                    Memoria.Logado = null;
                }

                if (!ok)
                {
                    return Falha("Nao existe conta com o Id " + id + ".",
                        "Devolver falso e a resposta certa aqui. Tente um Id que "
                      + "esteja na lista abaixo.");
                }

                return Ok("Conta #" + id + " removida. Sobraram " + Memoria.Contas.Count + ".",
                    "Se voce remover e CONTINUAR percorrendo, o programa estoura com "
                  + "InvalidOperationException. Sair do laco na hora resolve.");
            }

            return Falha("Botao sem acao: " + idTopico + " / " + idBotao,
                         "Isto e erro do material, nao seu. Avise o professor.");
        }

        // ---------- ajudantes ----------
        private static string Txt(Dictionary<string, string> v, string id)
        {
            if (v == null || !v.ContainsKey(id)) { return ""; }
            return (v[id] ?? "").Trim();
        }

        private static RespostaBancada Ok(string m, string d)
        {
            RespostaBancada r = new RespostaBancada();
            r.Ok = true; r.Mensagem = m; r.Detalhe = d;
            return r;
        }

        private static RespostaBancada Falha(string m, string d)
        {
            RespostaBancada r = new RespostaBancada();
            r.Ok = false; r.Mensagem = m; r.Detalhe = d;
            return r;
        }
    }
}
