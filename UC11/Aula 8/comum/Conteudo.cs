using System;
using System.Collections.Generic;

namespace Acesso
{
    /// <summary>
    /// Um caso do corretor: os argumentos, o que se espera, e por que.
    /// </summary>
    public class Caso
    {
        public object[] Args;
        public object Esperado;
        public string Descricao;

        public Caso(object esperado, string descricao, params object[] args)
        {
            Esperado = esperado;
            Descricao = descricao;
            Args = args;
        }
    }

    /// <summary>
    /// Uma fronteira que o teste do aluno e OBRIGADO a cobrir.
    ///
    /// O corretor le os [DataRow] do metodo de teste por reflexao e pergunta,
    /// para cada fronteira, se ALGUMA das linhas satisfaz o predicado. Se
    /// faltar uma, a aba nao fecha - mesmo com o metodo certo.
    /// </summary>
    public class Fronteira
    {
        public string Descricao;
        public Func<object[], bool> Satisfaz;

        public Fronteira(string descricao, Func<object[], bool> satisfaz)
        {
            Descricao = descricao;
            Satisfaz = satisfaz;
        }
    }

    public class Desafio
    {
        public int Numero;
        public string Titulo;
        public string Metodo;
        public string Assinatura;
        public string Enunciado;
        public string NomeDoTeste;
        public string Depende;
        public Func<object[], object> Chamar;
        public List<Caso> Casos = new List<Caso>();
        public List<Fronteira> Fronteiras = new List<Fronteira>();

        /// <summary>
        /// Comeca um bloco: esta aba nasce destravada, sem depender de nada.
        ///
        /// Sao dois - o desafio 1, que abre o login, e o 11, que abre o
        /// cadastro. Existe por causa de turma noturna: quem faltar na noite
        /// do login chega na do cadastro e comeca a trabalhar na hora, em vez
        /// de precisar fechar dez abas antes de alcancar a turma.
        /// </summary>
        public bool AbreBloco;
    }

    /// <summary>
    /// Os 20 desafios, em C# e nao em JSON de proposito: sem parser, sem
    /// despachante por texto, e o compilador confere as chamadas.
    ///
    /// Os valores esperados aqui sao os mesmos da lista mestra que o
    /// professor tem em maos. Mudou la, muda aqui.
    /// </summary>
    public static class Conteudo
    {
        private static int Tam(object o)
        {
            string s = o as string;
            return s == null ? -1 : s.Length;
        }

        private static bool AlgumTem(object[] a, Func<object, bool> f)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (f(a[i])) { return true; }
            }
            return false;
        }

        private static Fronteira Comprimento(int n)
        {
            return new Fronteira("uma entrada de " + n + " caracteres",
                a => AlgumTem(a, o => Tam(o) == n));
        }

        private static Fronteira ValorInteiro(int n)
        {
            return new Fronteira("a entrada " + n,
                a => AlgumTem(a, o => (o is int) && (int)o == n));
        }

        /// <summary>
        /// A linha tem duas strings iguais a menos da caixa - "ANA" e "ana", ou
        /// "senha1" e "Senha1". E como se prova, sem depender da POSICAO de cada
        /// coluna, que o aluno testou a troca de maiuscula por minuscula.
        /// </summary>
        private static bool ParDiferenteSoNaCaixa(object[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                string x = a[i] as string;
                if (string.IsNullOrEmpty(x)) { continue; }

                for (int j = i + 1; j < a.Length; j++)
                {
                    string y = a[j] as string;
                    if (string.IsNullOrEmpty(y)) { continue; }

                    if (x != y && x.ToLower() == y.ToLower()) { return true; }
                }
            }

            return false;
        }

        private static Fronteira Texto(string t, string descricao)
        {
            return new Fronteira(descricao,
                a => AlgumTem(a, o => (o as string) == t));
        }

        public static List<Desafio> Todos()
        {
            List<Desafio> d = new List<Desafio>();

            // ---------------------------------------------------- 1
            d.Add(new Desafio
            {
                Numero = 1,
                AbreBloco = true,
                Titulo = "Usuario valido",
                Metodo = "Login.UsuarioValido",
                Assinatura = "public static bool UsuarioValido(string usuario)",
                Enunciado = "O usuario tem de 4 a 20 caracteres, incluindo os dois extremos.",
                NomeDoTeste = "UsuarioValido_PorTabela",
                Chamar = a => Login.UsuarioValido((string)a[0]),
                Casos =
                {
                    new Caso(false, "3 caracteres, um a menos", "ana"),
                    new Caso(true, "LIMITE: exatamente 4", "joao"),
                    new Caso(true, "LIMITE: exatamente 20", "abcdefghijabcdefghij"),
                    new Caso(false, "21, um a mais", "abcdefghijabcdefghijk"),
                    new Caso(false, "vazio", ""),
                },
                Fronteiras = { Comprimento(3), Comprimento(4), Comprimento(20), Comprimento(21) },
            });

            // ---------------------------------------------------- 2
            d.Add(new Desafio
            {
                Numero = 2,
                Titulo = "Tamanho da senha",
                Metodo = "Login.SenhaTemTamanhoMinimo",
                Assinatura = "public static bool SenhaTemTamanhoMinimo(string senha)",
                Enunciado = "A senha tem no minimo 8 caracteres.",
                NomeDoTeste = "SenhaTemTamanhoMinimo_PorTabela",
                Chamar = a => Login.SenhaTemTamanhoMinimo((string)a[0]),
                Casos =
                {
                    new Caso(false, "7, um a menos", "1234567"),
                    new Caso(true, "LIMITE: exatamente 8", "12345678"),
                    new Caso(true, "9, folgado", "123456789"),
                    new Caso(false, "vazio", ""),
                },
                Fronteiras = { Comprimento(7), Comprimento(8), Comprimento(9) },
            });

            // ---------------------------------------------------- 3
            d.Add(new Desafio
            {
                Numero = 3,
                Titulo = "Senha com numero",
                Metodo = "Login.SenhaTemNumero",
                Assinatura = "public static bool SenhaTemNumero(string senha)",
                Enunciado = "A senha tem pelo menos um digito.",
                NomeDoTeste = "SenhaTemNumero_PorTabela",
                Chamar = a => Login.SenhaTemNumero((string)a[0]),
                Casos =
                {
                    new Caso(true, "tem digito", "abc123"),
                    new Caso(false, "nenhum digito", "abcdef"),
                    new Caso(true, "um digito so", "1"),
                    new Caso(false, "vazio", ""),
                },
                Fronteiras =
                {
                    new Fronteira("uma senha COM digito",
                        a => AlgumTem(a, o => { string s = o as string; if (s == null) return false;
                                                foreach (char c in s) if (c >= '0' && c <= '9') return true;
                                                return false; })),
                    new Fronteira("uma senha SEM digito e nao vazia",
                        a => AlgumTem(a, o => { string s = o as string; if (string.IsNullOrEmpty(s)) return false;
                                                foreach (char c in s) if (c >= '0' && c <= '9') return false;
                                                return true; })),
                    Texto("", "a senha vazia"),
                },
            });

            // ---------------------------------------------------- 4
            d.Add(new Desafio
            {
                Numero = 4,
                Titulo = "Senha com maiuscula",
                Metodo = "Login.SenhaTemMaiuscula",
                Assinatura = "public static bool SenhaTemMaiuscula(string senha)",
                Enunciado = "A senha tem pelo menos uma letra maiuscula.",
                NomeDoTeste = "SenhaTemMaiuscula_PorTabela",
                Chamar = a => Login.SenhaTemMaiuscula((string)a[0]),
                Casos =
                {
                    new Caso(true, "tem maiuscula", "abcDef"),
                    new Caso(false, "nenhuma maiuscula", "abcdef"),
                    new Caso(true, "uma maiuscula so", "A"),
                    new Caso(false, "vazio", ""),
                },
                Fronteiras =
                {
                    new Fronteira("uma senha COM maiuscula",
                        a => AlgumTem(a, o => { string s = o as string; if (s == null) return false;
                                                foreach (char c in s) if (c >= 'A' && c <= 'Z') return true;
                                                return false; })),
                    new Fronteira("uma senha SEM maiuscula e nao vazia",
                        a => AlgumTem(a, o => { string s = o as string; if (string.IsNullOrEmpty(s)) return false;
                                                foreach (char c in s) if (c >= 'A' && c <= 'Z') return false;
                                                return true; })),
                    Texto("", "a senha vazia"),
                },
            });

            // ---------------------------------------------------- 5
            d.Add(new Desafio
            {
                Numero = 5,
                Titulo = "Forca da senha",
                Metodo = "Login.ForcaDaSenha",
                Assinatura = "public static int ForcaDaSenha(string senha)",
                Enunciado = "Um ponto para cada desafio 2, 3 e 4 que a senha cumprir. Vai de 0 a 3.",
                NomeDoTeste = "ForcaDaSenha_PorTabela",
                Depende = "2, 3 e 4",
                Chamar = a => Login.ForcaDaSenha((string)a[0]),
                Casos =
                {
                    new Caso(0, "PISO: nao cumpre nenhum", "abc"),
                    new Caso(1, "so o tamanho", "abcdefgh"),
                    new Caso(2, "tamanho e numero", "abcdefg1"),
                    new Caso(3, "TETO: os tres", "Abcdefg1"),
                    new Caso(2, "numero e maiuscula, mas curta", "Abc1"),
                },
                Fronteiras =
                {
                    new Fronteira("um caso que espera 0",
                        a => AlgumTem(a, o => (o is int) && (int)o == 0)),
                    new Fronteira("um caso que espera 3",
                        a => AlgumTem(a, o => (o is int) && (int)o == 3)),
                },
            });

            // ---------------------------------------------------- 6
            d.Add(new Desafio
            {
                Numero = 6,
                Titulo = "Senha aceita",
                Metodo = "Login.SenhaAceita",
                Assinatura = "public static bool SenhaAceita(string senha)",
                Enunciado = "A senha e aceita a partir da forca 2. Pergunte para o desafio 5.",
                NomeDoTeste = "SenhaAceita_PorTabela",
                Depende = "5",
                Chamar = a => Login.SenhaAceita((string)a[0]),
                Casos =
                {
                    new Caso(false, "forca 0", "abc"),
                    new Caso(false, "forca 1, um a menos", "abcdefgh"),
                    new Caso(true, "LIMITE: forca 2 ja aceita", "abcdefg1"),
                    new Caso(true, "forca 3", "Abcdefg1"),
                },
                Fronteiras =
                {
                    new Fronteira("uma senha de forca 1",
                        a => AlgumTem(a, o => Login.ForcaDaSenha(o as string) == 1)),
                    new Fronteira("uma senha de forca 2",
                        a => AlgumTem(a, o => Login.ForcaDaSenha(o as string) == 2)),
                    new Fronteira("uma senha de forca 3",
                        a => AlgumTem(a, o => Login.ForcaDaSenha(o as string) == 3)),
                },
            });

            // ---------------------------------------------------- 7
            d.Add(new Desafio
            {
                Numero = 7,
                Titulo = "Autenticar",
                Metodo = "Login.Autenticar",
                Assinatura = "public static bool Autenticar(string usuario, string senha, "
                           + "string usuarioGravado, string senhaGravada)",
                Enunciado = "O usuario NAO diferencia maiuscula de minuscula. A senha diferencia.",
                NomeDoTeste = "Autenticar_PorTabela",
                Chamar = a => Login.Autenticar((string)a[0], (string)a[1], (string)a[2], (string)a[3]),
                Casos =
                {
                    new Caso(true, "tudo igual", "ana", "Senha1", "ana", "Senha1"),
                    new Caso(true, "usuario ignora a caixa", "ANA", "Senha1", "ana", "Senha1"),
                    new Caso(false, "senha DIFERENCIA a caixa", "ana", "senha1", "ana", "Senha1"),
                    new Caso(false, "outro usuario", "bia", "Senha1", "ana", "Senha1"),
                    new Caso(false, "vazio", "", "", "ana", "Senha1"),
                },
                Fronteiras =
                {
                    // Estas duas aceitam DOIS jeitos, e a razao e que o formato do teste
                    // do aluno nao e fixo: o corretor manda no NOME do metodo de teste,
                    // e nao na lista de parametros. Da para fixar a conta gravada dentro
                    // da chamada e passar so tres entradas na tabela, e isso e uma
                    // escolha legitima do aluno. Entao:
                    //
                    //   - quem escreve as quatro colunas fecha PELA REGRA: duas strings
                    //     da linha iguais a menos da caixa;
                    //   - quem fixa a conta gravada dentro do teste fecha PELO VALOR,
                    //     usando as palavras que a descricao aqui embaixo nomeia.
                    //
                    // So a regra reprovava quem fixa a conta; so o valor reprovava quem
                    // inventa os proprios nomes - e o desafio 7 tranca o 8, o 9 e o 10.
                    new Fronteira("o usuario numa caixa diferente da gravada "
                                  + "(a linha \"ANA\" contra a conta \"ana\", por exemplo)",
                        a => ParDiferenteSoNaCaixa(a) || AlgumTem(a, o => (o as string) == "ANA")),
                    new Fronteira("a senha numa caixa diferente da gravada "
                                  + "(a linha \"senha1\" contra a senha \"Senha1\", por exemplo)",
                        a => ParDiferenteSoNaCaixa(a) || AlgumTem(a, o => (o as string) == "senha1")),
                },
            });

            // ---------------------------------------------------- 8
            d.Add(new Desafio
            {
                Numero = 8,
                Titulo = "Tentativas restantes",
                Metodo = "Login.TentativasRestantes",
                Assinatura = "public static int TentativasRestantes(int tentativas)",
                Enunciado = "Sao 3 tentativas no total. O resultado nunca e negativo.",
                NomeDoTeste = "TentativasRestantes_PorTabela",
                Chamar = a => Login.TentativasRestantes((int)a[0]),
                Casos =
                {
                    new Caso(3, "ninguem errou ainda", 0),
                    new Caso(2, "errou uma", 1),
                    new Caso(0, "LIMITE: acabou", 3),
                    new Caso(0, "nunca negativo", 4),
                },
                Fronteiras = { ValorInteiro(0), ValorInteiro(3), ValorInteiro(4) },
            });

            // ---------------------------------------------------- 9
            d.Add(new Desafio
            {
                Numero = 9,
                Titulo = "Conta bloqueada",
                Metodo = "Login.ContaBloqueada",
                Assinatura = "public static bool ContaBloqueada(int tentativas)",
                Enunciado = "Bloqueia quando nao resta nenhuma tentativa. Pergunte para o desafio 8.",
                NomeDoTeste = "ContaBloqueada_PorTabela",
                Depende = "8",
                Chamar = a => Login.ContaBloqueada((int)a[0]),
                Casos =
                {
                    new Caso(false, "tres restantes", 0),
                    new Caso(false, "LIMITE: ultima chance", 2),
                    new Caso(true, "LIMITE: bloqueou", 3),
                    new Caso(true, "passou do limite", 5),
                },
                Fronteiras = { ValorInteiro(2), ValorInteiro(3) },
            });

            // ---------------------------------------------------- 10
            d.Add(new Desafio
            {
                Numero = 10,
                Titulo = "Mensagem do login",
                Metodo = "Login.MensagemDoLogin",
                Assinatura = "public static string MensagemDoLogin(string usuario, string senha, "
                           + "string usuarioGravado, string senhaGravada, int tentativas)",
                Enunciado = "Diz POR QUE o login foi recusado. O mais forte manda: "
                          + "1o bloqueada, 2o campo em branco, 3o nao autenticou, senao texto vazio.",
                NomeDoTeste = "MensagemDoLogin_PorTabela",
                Depende = "7 e 9",
                Chamar = a => Login.MensagemDoLogin((string)a[0], (string)a[1],
                                                    (string)a[2], (string)a[3], (int)a[4]),
                Casos =
                {
                    new Caso("Conta bloqueada por excesso de tentativas.",
                             "ALVO: bloqueio vem antes de tudo", "ana", "Senha1", "ana", "Senha1", 3),
                    new Caso("Preencha o usuario e a senha.", "usuario vazio", "", "Senha1", "ana", "Senha1", 0),
                    new Caso("Usuario ou senha incorretos.", "senha errada", "ana", "errada", "ana", "Senha1", 0),
                    new Caso("", "tudo certo", "ana", "Senha1", "ana", "Senha1", 0),
                },
                Fronteiras =
                {
                    new Fronteira("o caso bloqueado (3 tentativas) COM a senha certa",
                        a => AlgumTem(a, o => (o is int) && (int)o >= 3)
                          && AlgumTem(a, o => (o as string) == "Conta bloqueada por excesso de tentativas.")),
                },
            });

            Cadastro20(d);
            return d;
        }

        private static void Cadastro20(List<Desafio> d)
        {
            // ---------------------------------------------------- 11
            d.Add(new Desafio
            {
                Numero = 11,
                AbreBloco = true,
                Titulo = "E-mail valido",
                Metodo = "Cadastro.EmailValido",
                Assinatura = "public static bool EmailValido(string email)",
                Enunciado = "Tem de existir uma arroba, e um ponto DEPOIS dela.",
                NomeDoTeste = "EmailValido_PorTabela",
                Chamar = a => Cadastro.EmailValido((string)a[0]),
                Casos =
                {
                    new Caso(true, "arroba e ponto depois", "ana@senac.br"),
                    new Caso(false, "sem arroba", "ana.senac.br"),
                    new Caso(false, "ALVO: o ponto esta ANTES da arroba", "ana.b@senacbr"),
                    new Caso(false, "arroba sem ponto", "ana@senacbr"),
                    new Caso(false, "vazio", ""),
                },
                Fronteiras =
                {
                    new Fronteira("um e-mail sem arroba",
                        a => AlgumTem(a, o => { string s = o as string; return !string.IsNullOrEmpty(s) && s.IndexOf('@') < 0; })),
                    new Fronteira("um e-mail com o ponto ANTES da arroba e nenhum depois",
                        a => AlgumTem(a, o => { string s = o as string;
                                                if (string.IsNullOrEmpty(s)) return false;
                                                int ar = s.IndexOf('@');
                                                return ar > 0 && s.IndexOf('.') < ar && s.IndexOf('.', ar) < 0; })),
                    new Fronteira("um e-mail com o ponto depois da arroba",
                        a => AlgumTem(a, o => { string s = o as string;
                                                if (string.IsNullOrEmpty(s)) return false;
                                                int ar = s.IndexOf('@');
                                                return ar > 0 && s.IndexOf('.', ar) > ar; })),
                },
            });

            // ---------------------------------------------------- 12
            d.Add(new Desafio
            {
                Numero = 12,
                Titulo = "Telefone valido",
                Metodo = "Cadastro.TelefoneValido",
                Assinatura = "public static bool TelefoneValido(string telefone)",
                Enunciado = "10 ou 11 digitos, e SO digitos.",
                NomeDoTeste = "TelefoneValido_PorTabela",
                Chamar = a => Cadastro.TelefoneValido((string)a[0]),
                Casos =
                {
                    new Caso(false, "9 digitos", "279999999"),
                    new Caso(true, "LIMITE: 10, fixo", "2733334444"),
                    new Caso(true, "LIMITE: 11, celular", "27999998888"),
                    new Caso(false, "12 digitos", "279999988887"),
                    new Caso(false, "11, mas tem letra", "2799999888a"),
                },
                Fronteiras =
                {
                    Comprimento(9), Comprimento(10), Comprimento(11), Comprimento(12),
                    new Fronteira("uma entrada com letra",
                        a => AlgumTem(a, o => { string s = o as string;
                                                if (string.IsNullOrEmpty(s)) return false;
                                                foreach (char c in s) if (c < '0' || c > '9') return true;
                                                return false; })),
                },
            });

            // ---------------------------------------------------- 13
            d.Add(new Desafio
            {
                Numero = 13,
                Titulo = "CEP valido",
                Metodo = "Cadastro.CepValido",
                Assinatura = "public static bool CepValido(string cep)",
                Enunciado = "Exatamente 8 digitos.",
                NomeDoTeste = "CepValido_PorTabela",
                Chamar = a => Cadastro.CepValido((string)a[0]),
                Casos =
                {
                    new Caso(false, "7 digitos", "2901000"),
                    new Caso(true, "LIMITE: exatamente 8", "29010000"),
                    new Caso(false, "9 digitos", "290100000"),
                    new Caso(false, "8, mas tem letra", "2901000a"),
                },
                Fronteiras = { Comprimento(7), Comprimento(8), Comprimento(9) },
            });

            // ---------------------------------------------------- 14
            d.Add(new Desafio
            {
                Numero = 14,
                Titulo = "Idade valida",
                Metodo = "Cadastro.IdadeValida",
                Assinatura = "public static bool IdadeValida(int idade)",
                Enunciado = "De 18 a 120, INCLUINDO os dois.",
                NomeDoTeste = "IdadeValida_PorTabela",
                Chamar = a => Cadastro.IdadeValida((int)a[0]),
                Casos =
                {
                    new Caso(false, "um a menos", 17),
                    new Caso(true, "LIMITE de baixo", 18),
                    new Caso(true, "LIMITE de cima", 120),
                    new Caso(false, "um a mais", 121),
                    new Caso(false, "zero", 0),
                },
                Fronteiras = { ValorInteiro(17), ValorInteiro(18), ValorInteiro(120), ValorInteiro(121) },
            });

            // ---------------------------------------------------- 15
            d.Add(new Desafio
            {
                Numero = 15,
                Titulo = "Nome completo",
                Metodo = "Cadastro.NomeCompleto",
                Assinatura = "public static bool NomeCompleto(string nome)",
                Enunciado = "Pelo menos duas palavras. Espaco sobrando nao inventa nem tira palavra.",
                NomeDoTeste = "NomeCompleto_PorTabela",
                Chamar = a => Cadastro.NomeCompleto((string)a[0]),
                Casos =
                {
                    new Caso(false, "uma palavra", "Ana"),
                    new Caso(true, "duas palavras", "Ana Souza"),
                    new Caso(true, "ALVO: espaco sobrando nao muda a conta", "  Ana   Souza  "),
                    new Caso(false, "vazio", ""),
                    new Caso(false, "so espaco", "   "),
                },
                Fronteiras =
                {
                    new Fronteira("um nome de uma palavra so",
                        a => AlgumTem(a, o => { string s = o as string;
                                                return !string.IsNullOrEmpty(s) && s.Trim() != "" && s.Trim().IndexOf(' ') < 0; })),
                    new Fronteira("um nome de duas palavras",
                        a => AlgumTem(a, o => { string s = o as string;
                                                return s != null && s.Trim().IndexOf(' ') > 0 && s.Trim() == s; })),
                    new Fronteira("um nome com espaco sobrando nas pontas ou no meio",
                        a => AlgumTem(a, o => { string s = o as string;
                                                return !string.IsNullOrEmpty(s) && s.Trim() != "" && (s != s.Trim() || s.Contains("  ")); })),
                },
            });

            // ---------------------------------------------------- 16
            d.Add(new Desafio
            {
                Numero = 16,
                Titulo = "Formato do CPF",
                Metodo = "Cadastro.CpfTemFormato",
                Assinatura = "public static bool CpfTemFormato(string cpf)",
                Enunciado = "11 digitos, e so digitos. NAO valida o digito verificador.",
                NomeDoTeste = "CpfTemFormato_PorTabela",
                Chamar = a => Cadastro.CpfTemFormato((string)a[0]),
                Casos =
                {
                    new Caso(false, "10 digitos", "1234567890"),
                    new Caso(true, "LIMITE: exatamente 11", "12345678901"),
                    new Caso(false, "12 digitos", "123456789012"),
                    new Caso(false, "11, mas tem letra", "1234567890a"),
                },
                Fronteiras = { Comprimento(10), Comprimento(11), Comprimento(12) },
            });

            // ---------------------------------------------------- 17
            d.Add(new Desafio
            {
                Numero = 17,
                Titulo = "Campos preenchidos",
                Metodo = "Cadastro.CamposPreenchidos",
                Assinatura = "public static bool CamposPreenchidos(string nome, string email, string telefone)",
                Enunciado = "Nenhum dos tres pode estar vazio nem conter so espaco.",
                NomeDoTeste = "CamposPreenchidos_PorTabela",
                Chamar = a => Cadastro.CamposPreenchidos((string)a[0], (string)a[1], (string)a[2]),
                Casos =
                {
                    new Caso(true, "tudo preenchido", "Ana", "a@b.c", "2733334444"),
                    new Caso(false, "nome vazio", "", "a@b.c", "2733334444"),
                    new Caso(false, "ALVO: nome so com espaco", "   ", "a@b.c", "2733334444"),
                    new Caso(false, "email vazio", "Ana", "", "2733334444"),
                    new Caso(false, "telefone so com espaco", "Ana", "a@b.c", "   "),
                },
                Fronteiras =
                {
                    Texto("", "um campo vazio"),
                    new Fronteira("um campo so com espaco",
                        a => AlgumTem(a, o => { string s = o as string; return s != null && s != "" && s.Trim() == ""; })),
                },
            });

            // ---------------------------------------------------- 18
            d.Add(new Desafio
            {
                Numero = 18,
                Titulo = "E-mail ja cadastrado",
                Metodo = "Cadastro.EmailJaCadastrado",
                Assinatura = "public static bool EmailJaCadastrado(string email)",
                Enunciado = "Ja existem ana@senac.br, bruno@senac.br e carla@senac.br. "
                          + "A comparacao NAO diferencia maiuscula de minuscula.",
                NomeDoTeste = "EmailJaCadastrado_PorTabela",
                Chamar = a => Cadastro.EmailJaCadastrado((string)a[0]),
                Casos =
                {
                    new Caso(true, "ja existe", "ana@senac.br"),
                    new Caso(true, "ALVO: nao diferencia maiuscula", "ANA@SENAC.BR"),
                    new Caso(false, "conta nova", "novo@senac.br"),
                    new Caso(false, "vazio", ""),
                },
                Fronteiras =
                {
                    new Fronteira("um e-mail que ja existe, em minusculas",
                        a => AlgumTem(a, o => (o as string) == "ana@senac.br"
                                           || (o as string) == "bruno@senac.br"
                                           || (o as string) == "carla@senac.br")),
                    new Fronteira("o mesmo e-mail em MAIUSCULAS",
                        a => AlgumTem(a, o => { string s = o as string;
                                                if (string.IsNullOrEmpty(s)) return false;
                                                string b = s.ToLower();
                                                return s != b && (b == "ana@senac.br" || b == "bruno@senac.br" || b == "carla@senac.br"); })),
                    new Fronteira("um e-mail novo",
                        a => AlgumTem(a, o => { string s = o as string;
                                                if (string.IsNullOrEmpty(s)) return false;
                                                string b = s.ToLower();
                                                return b != "ana@senac.br" && b != "bruno@senac.br" && b != "carla@senac.br"; })),
                },
            });

            // ---------------------------------------------------- 19
            d.Add(new Desafio
            {
                Numero = 19,
                Titulo = "Pode cadastrar",
                Metodo = "Cadastro.PodeCadastrar",
                Assinatura = "public static bool PodeCadastrar(string nome, string email, "
                           + "string telefone, string cep, int idade, string senha)",
                Enunciado = "So passa quando TODAS as condicoes valem ao mesmo tempo. "
                          + "Este metodo nao decide nada sozinho: pergunta para os outros.",
                NomeDoTeste = "PodeCadastrar_PorTabela",
                Depende = "11 a 15, 17, 18 e o 6",
                Chamar = a => Cadastro.PodeCadastrar((string)a[0], (string)a[1], (string)a[2],
                                                     (string)a[3], (int)a[4], (string)a[5]),
                Casos =
                {
                    new Caso(true, "tudo certo", "Ana Souza", "nova@senac.br", "2733334444", "29010000", 30, "Abcdefg1"),
                    new Caso(false, "nome com uma palavra", "Ana", "nova@senac.br", "2733334444", "29010000", 30, "Abcdefg1"),
                    new Caso(false, "email invalido", "Ana Souza", "ana@senacbr", "2733334444", "29010000", 30, "Abcdefg1"),
                    new Caso(false, "email ja cadastrado", "Ana Souza", "ana@senac.br", "2733334444", "29010000", 30, "Abcdefg1"),
                    new Caso(false, "idade 17", "Ana Souza", "nova@senac.br", "2733334444", "29010000", 17, "Abcdefg1"),
                    new Caso(false, "senha de forca 1", "Ana Souza", "nova@senac.br", "2733334444", "29010000", 30, "abcdefgh"),
                },
                Fronteiras =
                {
                    new Fronteira("um caso em que tudo esta certo (espera true)",
                        a => AlgumTem(a, o => (o is bool) && (bool)o)),
                    new Fronteira("pelo menos tres casos que esperam false",
                        a => AlgumTem(a, o => (o is bool) && !(bool)o)),
                },
            });

            // ---------------------------------------------------- 20
            d.Add(new Desafio
            {
                Numero = 20,
                Titulo = "Mensagem do cadastro",
                Metodo = "Cadastro.MensagemDoCadastro",
                Assinatura = "public static string MensagemDoCadastro(string nome, string email, "
                           + "string telefone, string cep, int idade, string senha)",
                Enunciado = "Diz POR QUE o cadastro foi recusado. O mais forte manda, nesta ordem: "
                          + "campo em branco, nome incompleto, e-mail invalido, e-mail ja cadastrado, "
                          + "telefone, CEP, idade, senha fraca.",
                NomeDoTeste = "MensagemDoCadastro_PorTabela",
                Depende = "11 a 15, 17, 18 e o 6",
                Chamar = a => Cadastro.MensagemDoCadastro((string)a[0], (string)a[1], (string)a[2],
                                                          (string)a[3], (int)a[4], (string)a[5]),
                Casos =
                {
                    new Caso("", "tudo certo", "Ana Souza", "nova@senac.br", "2733334444", "29010000", 30, "Abcdefg1"),
                    new Caso("Preencha todos os campos.", "nome vazio", "", "nova@senac.br", "2733334444", "29010000", 30, "Abcdefg1"),
                    new Caso("Informe o nome completo.", "nome incompleto", "Ana", "nova@senac.br", "2733334444", "29010000", 30, "Abcdefg1"),
                    new Caso("E-mail invalido.", "ALVO: invalido antes de ja cadastrado", "Ana Souza", "ana@senacbr", "2733334444", "29010000", 30, "Abcdefg1"),
                    new Caso("Este e-mail ja esta cadastrado.", "ALVO: valido porem ja existe", "Ana Souza", "ana@senac.br", "2733334444", "29010000", 30, "Abcdefg1"),
                    new Caso("Idade fora do permitido.", "idade 17", "Ana Souza", "nova@senac.br", "2733334444", "29010000", 17, "Abcdefg1"),
                },
                Fronteiras =
                {
                    Texto("E-mail invalido.", "o caso do e-mail INVALIDO"),
                    Texto("Este e-mail ja esta cadastrado.", "o caso do e-mail valido porem JA CADASTRADO"),
                },
            });
        }
    }
}
