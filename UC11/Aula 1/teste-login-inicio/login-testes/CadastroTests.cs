using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaLogin;

namespace SistemaLoginTestes
{
    /// <summary>
    /// O ROTEIRO DE TESTES da Aula 1 - UC11, 08/09/2026.
    ///
    /// Sao 18 casos, e cada um ja tem NOME e REGRA. Falta o corpo: uma
    /// linha de Assert em cada, e e voce quem escreve.
    ///
    /// O nome do teste diz tudo: Metodo_Cenario_OQueDeveDar. Quando um
    /// deles ficar vermelho, e esse nome que aparece na lista - por isso
    /// ele conta a regra, e nao "teste1", "teste2".
    ///
    /// NAO HA UM UNICO double NESTA SUITE, e portanto nenhuma FOLGA para
    /// lembrar. Sao tres tipos de Assert, so:
    ///
    ///     Assert.IsTrue(algo)          quando o metodo devolve bool e
    ///                                  voce espera VERDADEIRO
    ///     Assert.IsFalse(algo)         bool, e voce espera FALSO
    ///     Assert.AreEqual(esp, obtido) quando o metodo devolve int ou
    ///                                  string - o ESPERADO vem primeiro
    ///
    /// NAO E PARA SUPOR QUANTOS VAO FICAR VERMELHOS. Escreva os 18, rode,
    /// e deixe a barra responder.
    /// </summary>
    [TestClass]
    public class CadastroTests
    {
        // As frases que o MensagemDoErro devolve, ja prontas para o
        // TODO 17. Use a CONSTANTE, nunca o texto digitado a mao.
        //
        // ATENCAO: elas NAO tem o ">> " que aparece na tela. Aquele
        // prefixo e de quem imprime, e nao faz parte da frase.
        private const string SENHA_OBVIA = "Escolha uma senha menos obvia.";
        private const string SENHAS_DIFERENTES = "As duas senhas nao sao iguais.";
        private const string USUARIO_EM_USO = "Este usuario ja esta em uso.";

        // ============================================================
        // O PRIMEIRO SAI PRONTO - e o molde dos outros dezessete.
        //
        //   1. o [TestMethod] em cima. Sem ele, o teste nao aparece na
        //      janela Test Explorer.
        //   2. public void, sem parametro nenhum.
        //   3. o nome contando a regra.
        //   4. UMA linha de Assert la dentro.
        // ============================================================

        [TestMethod]
        public void UsuarioPreenchido_ComTexto_DevolveVerdadeiro()
        {
            Assert.IsTrue(Cadastro.UsuarioPreenchido("joao"));
        }

        // TODO 1
        [TestMethod]
        public void UsuarioPreenchido_ComTextoVazio_DevolveFalso()
        {
        }

        // TODO 2
        [TestMethod]
        public void SenhaPreenchida_ComTextoVazio_DevolveFalso()
        {
        }

        // TODO 3
        [TestMethod]
        public void UsuarioDisponivel_ComUsuarioNovo_DevolveVerdadeiro()
        {
        }

        // TODO 4
        [TestMethod]
        public void UsuarioDisponivel_ComUsuarioJaCadastrado_DevolveFalso()
        {
        }

        // TODO 5
        [TestMethod]
        public void SenhasConferem_ComAsDuasIguais_DevolveVerdadeiro()
        {
        }

        // TODO 6
        //
        // As duas senhas tem seis caracteres cada, e sao completamente
        // diferentes. E este o caso que denuncia.
        [TestMethod]
        public void SenhasConferem_ComSenhasDiferentesDoMesmoTamanho_DevolveFalso()
        {
        }

        // TODO 7
        [TestMethod]
        public void SenhaProibida_ComAdmin_DevolveVerdadeiro()
        {
        }

        // TODO 8
        [TestMethod]
        public void SenhaProibida_ComSenac_DevolveVerdadeiro()
        {
        }

        // TODO 9
        [TestMethod]
        public void SenhaProibida_ComSenhaComum_DevolveFalso()
        {
        }

        // TODO 10
        [TestMethod]
        public void CaracteresIguaisSeguidos_SemNenhumaRepeticao_DevolveUm()
        {
        }

        // TODO 11
        [TestMethod]
        public void CaracteresIguaisSeguidos_ComDoisNoComeco_DevolveDois()
        {
        }

        // TODO 12
        //
        // Repare que a sequencia dos tres "1" TERMINA no fim do texto. E
        // so nesse caso que o defeito aparece.
        [TestMethod]
        public void CaracteresIguaisSeguidos_ComTresNoFim_DevolveTres()
        {
        }

        // TODO 13
        [TestMethod]
        public void SemRepeticaoDemais_ComSenhaVariada_DevolveVerdadeiro()
        {
        }

        // TODO 14
        [TestMethod]
        public void SenhaDiferenteDoUsuario_ComSenhaIgualAoUsuario_DevolveFalso()
        {
        }

        // TODO 15
        [TestMethod]
        public void PodeCriarConta_ComTudoCerto_DevolveVerdadeiro()
        {
        }

        // TODO 16
        //
        // Com os tres campos em branco NENHUMA regra passa, e mesmo assim
        // a versao defeituosa deixa criar a conta.
        [TestMethod]
        public void PodeCriarConta_ComTudoEmBranco_DevolveFalso()
        {
        }

        // TODO 17
        //
        // "ana" viola DUAS regras ao mesmo tempo: o usuario ja existe E as
        // duas senhas sao diferentes. A regra diz que o usuario em uso vem
        // primeiro.
        //
        // Repare nos TAMANHOS: "abc12" tem cinco e "xyz789" tem seis. E de
        // proposito - assim os dois metodos consultados respondem igual nas
        // duas versoes, e o unico culpado deste vermelho e a ORDEM.
        [TestMethod]
        public void MensagemDoErro_ComUsuarioEmUsoESenhasDiferentes_ReclamaDoUsuario()
        {
        }
    }
}
