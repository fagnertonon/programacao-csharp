namespace Conecta
{
    /// <summary>
    /// Um aluno cadastrado no Conecta.
    ///
    /// Esta e a versao FINAL da classe, do jeito que ela fica no fim da
    /// Atividade 1. Na Parte 6 ela comecou menor, com campos publicos e
    /// os dois membros estaticos Cadastrado e TemCadastro; quando o
    /// UsuarioDAO apareceu, na Parte 7, esses dois sairam - quem guarda
    /// a lista de contas agora e o DAO.
    ///
    /// NAO herda de Publicacao: um usuario nao e uma publicacao.
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Numero que identifica a conta. Na Atividade 1 quem gera e o
        /// proprio UsuarioDAO; na Atividade 2 quem gera e o banco.
        /// </summary>
        public int Id { get; set; }

        public string Nome { get; set; } = "";
        public string Login { get; set; } = "";
        public string Senha { get; set; } = "";
    }
}
