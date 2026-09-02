namespace Portaria
{
    /// <summary>
    /// Guarda quem esta conectado agora.
    ///
    /// E static porque existe UMA sessao para o programa inteiro: nao
    /// faz sentido cada tela ter a sua. Por isso tambem nao se escreve
    /// new Sessao() em lugar nenhum.
    ///
    /// Esta classe vem PRONTA e e a mesma da Aula 15. Voce nao mexe
    /// nela - so usa.
    /// </summary>
    public static class Sessao
    {
        public static Usuario UsuarioLogado = null;

        public static bool TemUsuarioLogado()
        {
            return UsuarioLogado != null;
        }

        public static int IdUsuarioLogado()
        {
            if (UsuarioLogado == null) return 0;
            return UsuarioLogado.Id;
        }

        public static void Encerrar()
        {
            UsuarioLogado = null;
        }
    }
}
