using System.Collections.Generic;

namespace Conecta
{
    // Metodos que o aplicativo ja traz prontos, para voce USAR nos desafios.
    //
    // Voce NAO mexe aqui - mas pode ler. Repare que LoginEmUso e exatamente
    // o mesmo codigo do desafio 4 da Aula 12: um foreach, um if, e dois
    // return em lugares diferentes.
    public static class Ajuda
    {
        /// <summary>True se o login ja estiver em uso na lista.</summary>
        public static bool LoginEmUso(List<Usuario> contas, string login)
        {
            if (contas == null) { return false; }

            foreach (Usuario u in contas)
            {
                if (u.Login == login) { return true; }
            }
            return false;
        }
    }
}
