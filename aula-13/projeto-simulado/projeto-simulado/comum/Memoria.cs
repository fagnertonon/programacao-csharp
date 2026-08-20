using System.Collections.Generic;

namespace Conecta
{
    // A MEMORIA DO CONECTA.
    //
    // Esta lista e A MESMA nas oito abas: a conta que voce criar na mini-tela
    // da aba 1 e a conta com que voce entra na mini-tela da aba 3.
    //
    // Ela vive enquanto o programa estiver aberto. Rodou de novo com F5?
    // Lista nova, vazia. Isso NAO e defeito - e a razao de existir banco de
    // dados, e voce vai escrever exatamente estas linhas hoje a noite, dentro
    // do UsuarioDAO.cs do seu projeto.
    //
    // Voce NAO mexe aqui. Mas LEIA as duas primeiras linhas de dentro da classe.
    public static class Memoria
    {
        public static List<Usuario> Contas = new List<Usuario>();

        // O papel que a classe Sessao faz no seu projeto.
        public static Usuario Logado = null;

        public static void Limpar()
        {
            Contas = new List<Usuario>();
            Logado = null;
        }

        // Duas contas prontas, para quem caiu aqui sem passar pela aba 1.
        public static void Semear()
        {
            Limpar();

            Usuario a = new Usuario();
            a.Id = 1; a.Nome = "Ana Souza"; a.Login = "ana"; a.Senha = "1234";

            Usuario b = new Usuario();
            b.Id = 2; b.Nome = "Bruno Lima"; b.Login = "bruno"; b.Senha = "abcd";

            Contas.Add(a);
            Contas.Add(b);
        }

        // Uma copia da lista, para entregar ao metodo do aluno sem arriscar
        // a lista viva se o codigo dele parar no meio.
        public static List<Usuario> Copia()
        {
            return new List<Usuario>(Contas);
        }

        public static string Resumo()
        {
            string quem = (Logado == null) ? "ninguem logado" : "logado: " + Logado.Nome;
            return Contas.Count + " conta(s)   |   " + quem
                 + "   |   esta lista vive enquanto o programa estiver aberto";
        }

        // "[ana], [bruno]" - o que a bancada mostra quando o login nao bate,
        // para o aluno ver que digitou diferente do que cadastrou.
        public static string Logins()
        {
            string s = "";
            foreach (Usuario u in Contas)
            {
                if (s != "") { s += ", "; }
                s += "[" + u.Login + "]";
            }
            return s == "" ? "(nenhuma)" : s;
        }
    }
}
