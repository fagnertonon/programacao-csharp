using System;

namespace Portaria
{
    /// <summary>
    /// Uma conta cadastrada na Portaria.
    ///
    /// Repare no Nome: ele NAO e uma propriedade automatica como as
    /// outras. Ele tem um campo privado por tras e um set que arruma o
    /// texto antes de guardar. E o encapsulamento da Aula 5: quem
    /// decide o que entra no objeto e o proprio objeto.
    /// </summary>
    public class Usuario
    {
        private string nome = "";

        /// <summary>Numero que identifica a conta. Quem gera e o banco.</summary>
        public int Id { get; set; }

        /// <summary>
        /// TODO 2 - Nome completo, sempre sem espacos nas pontas e nunca
        /// nulo.                                        [Indicador I2]
        ///
        /// O set NAO recusa nada e NAO dispara erro - quem recusa dado
        /// ruim e a tela, com MessageBox. Aqui o trabalho e so ARRUMAR:
        ///
        ///   se o value for null   ->  guarde "" no campo nome
        ///   senao                 ->  guarde value.Trim()
        ///
        /// O Trim() tira os espacos das PONTAS. Sem ele, um nome digitado
        /// com espaco sobrando entra torto na lista da tela principal.
        /// </summary>
        public string Nome
        {
            get { return nome; }
            set
            {
                nome = value;   // <<< TROQUE ESTA LINHA pela sua
            }
        }

        /// <summary>O que se digita na tela de entrada.</summary>
        public string Login { get; set; } = "";

        /// <summary>Guardada em texto puro, como nos projetos anteriores.</summary>
        public string Senha { get; set; } = "";

        /// <summary>Carimbada pelo banco no momento do cadastro.</summary>
        public DateTime DataCadastro { get; set; }
    }
}
