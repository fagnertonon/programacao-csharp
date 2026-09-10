using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acesso;

namespace AcessoTestes
{
    /// <summary>
    /// OS SEUS TESTES - UC11, Aulas 8 e 9.
    ///
    /// Cada desafio pede DUAS coisas: o corpo do metodo, no Login.cs ou no
    /// Cadastro.cs, e o teste aqui. A aba so fecha com as duas.
    ///
    /// O CORRETOR CONFERE QUATRO PERGUNTAS:
    ///
    ///   1. o metodo tem corpo?
    ///   2. o metodo esta certo?
    ///   3. existe um teste com o NOME que a aba pede, e ele passa?
    ///   4. o seu [DataRow] cobre as FRONTEIRAS que a aba exige?
    ///
    /// A pergunta 4 e a que pega. Nao basta o metodo funcionar: o seu teste
    /// precisa provar que voce pensou no limite. A aba lista, do lado
    /// esquerdo, exatamente quais valores tem de estar na sua tabela.
    ///
    /// O NOME DO TESTE NAO E ESCOLHA SUA. Cada aba diz o nome que ela
    /// procura, e e por ele que o corretor acha o seu teste.
    /// </summary>
    [TestClass]
    public class AcessoTests
    {
        // ============================================================
        // DESAFIO 1 - ESTE SAI PRONTO, e e o molde dos outros dezenove.
        //
        //   1. [DataTestMethod] em cima, e nao [TestMethod], porque ele
        //      recebe uma TABELA de casos.
        //   2. um [DataRow] por caso, na ordem dos parametros abaixo.
        //   3. o ultimo valor e um texto que aparece na barra quando o caso
        //      fica vermelho. Escreva o que voce esta testando.
        //   4. UMA linha de Assert, que serve para todos os casos.
        //
        // Repare que os quatro comprimentos que a aba 1 exige - 3, 4, 20 e
        // 21 - estao todos aqui. E por isso que a pergunta 4 dela ja esta
        // verde. Falta so escrever o corpo do UsuarioValido.
        // ============================================================
        [DataTestMethod]
        [DataRow("ana", false, "3 caracteres, um a menos")]
        [DataRow("joao", true, "LIMITE: exatamente 4")]
        [DataRow("abcdefghijabcdefghij", true, "LIMITE: exatamente 20")]
        [DataRow("abcdefghijabcdefghijk", false, "21, um a mais")]
        [DataRow("", false, "vazio")]
        public void UsuarioValido_PorTabela(string usuario, bool esperado, string caso)
        {
            Assert.AreEqual(esperado, Login.UsuarioValido(usuario), caso);
        }

        // ============================================================
        // DESAFIO 2 - SenhaTemTamanhoMinimo
        //
        // O nome do seu teste tem de ser:  SenhaTemTamanhoMinimo_PorTabela
        //
        // Molde:
        //
        //   [DataTestMethod]
        //   [DataRow("1234567", false, "7, um a menos")]
        //   public void SenhaTemTamanhoMinimo_PorTabela(string senha, bool esperado, string caso)
        //   {
        //       Assert.AreEqual(esperado, Login.SenhaTemTamanhoMinimo(senha), caso);
        //   }
        //
        // A aba exige comprimentos 7, 8 e 9. Faltando um, ela nao fecha.
        // ============================================================

        // ============================================================
        // DESAFIO 3 - SenhaTemNumero
        // Nome do teste:  SenhaTemNumero_PorTabela
        // A aba exige: uma senha COM digito, uma SEM, e a vazia.
        // ============================================================

        // ============================================================
        // DESAFIO 4 - SenhaTemMaiuscula
        // Nome do teste:  SenhaTemMaiuscula_PorTabela
        // A aba exige: uma senha COM maiuscula, uma SEM, e a vazia.
        // ============================================================

        // ============================================================
        // DESAFIO 5 - ForcaDaSenha
        // Nome do teste:  ForcaDaSenha_PorTabela
        //
        // Aqui o esperado e um NUMERO, e nao true/false:
        //   public void ForcaDaSenha_PorTabela(string senha, int esperado, string caso)
        //
        // A aba exige um caso que espera 0 e um que espera 3 - o piso e o teto.
        // ============================================================

        // ============================================================
        // DESAFIO 6 - SenhaAceita
        // Nome do teste:  SenhaAceita_PorTabela
        // A aba exige senhas de forca 1, 2 e 3. A de forca 2 e o limite:
        // e a primeira que ja e aceita.
        // ============================================================

        // ============================================================
        // DESAFIO 7 - Autenticar
        // Nome do teste:  Autenticar_PorTabela
        //
        // Sao quatro parametros de entrada:
        //   public void Autenticar_PorTabela(string usuario, string senha,
        //          string usuarioGravado, string senhaGravada, bool esperado, string caso)
        //
        // A aba exige DUAS linhas: uma em que so o usuario troca de caixa,
        // e outra em que so a senha troca. Sao as duas metades da regra.
        //
        //   [DataRow("ANA",  "Senha1", "ana", "Senha1", true,  "usuario ignora a caixa")]
        //   [DataRow("ana",  "senha1", "ana", "Senha1", false, "senha DIFERENCIA a caixa")]
        //
        // Os nomes nao precisam ser estes: o que a aba cobre e a TROCA DE
        // CAIXA. "CARLOS" contra "carlos" serve igual.
        // ============================================================

        // ============================================================
        // DESAFIO 8 - TentativasRestantes
        // Nome do teste:  TentativasRestantes_PorTabela
        // Entrada e saida sao numeros. A aba exige as entradas 0, 3 e 4.
        // ============================================================

        // ============================================================
        // DESAFIO 9 - ContaBloqueada
        // Nome do teste:  ContaBloqueada_PorTabela
        // A aba exige as entradas 2 e 3 - a ultima chance e o bloqueio.
        // ============================================================

        // ============================================================
        // DESAFIO 10 - MensagemDoLogin
        // Nome do teste:  MensagemDoLogin_PorTabela
        //
        // O esperado aqui e um TEXTO, e nao um true/false.
        //
        // O MensagemDoLogin recebe cinco valores. Voce escolhe quantos passam
        // pela tabela: da para fixar a conta gravada dentro da chamada e
        // deixar so o que muda no [DataRow]. As duas formas fecham a aba.
        //
        // A aba exige o caso da conta bloqueada COM a senha certa - e ele
        // que prova que o bloqueio vem antes de tudo.
        // ============================================================

        // ============================================================
        // DESAFIO 11 - EmailValido        ->  EmailValido_PorTabela
        // Exige: sem arroba, ponto ANTES da arroba, e ponto depois.
        // ============================================================

        // ============================================================
        // DESAFIO 12 - TelefoneValido     ->  TelefoneValido_PorTabela
        // Exige: comprimentos 9, 10, 11, 12 e uma entrada com letra.
        // ============================================================

        // ============================================================
        // DESAFIO 13 - CepValido          ->  CepValido_PorTabela
        // Exige: comprimentos 7, 8 e 9.
        // ============================================================

        // ============================================================
        // DESAFIO 14 - IdadeValida        ->  IdadeValida_PorTabela
        // Exige: as entradas 17, 18, 120 e 121.
        // ============================================================

        // ============================================================
        // DESAFIO 15 - NomeCompleto       ->  NomeCompleto_PorTabela
        // Exige: uma palavra, duas palavras, e um com espaco sobrando.
        // ============================================================

        // ============================================================
        // DESAFIO 16 - CpfTemFormato      ->  CpfTemFormato_PorTabela
        // Exige: comprimentos 10, 11 e 12.
        // ============================================================

        // ============================================================
        // DESAFIO 17 - CamposPreenchidos  ->  CamposPreenchidos_PorTabela
        // Tres entradas de texto. Exige: um campo vazio e um so com espaco.
        // ============================================================

        // ============================================================
        // DESAFIO 18 - EmailJaCadastrado  ->  EmailJaCadastrado_PorTabela
        // Exige: um e-mail que existe, o MESMO em maiusculas, e um novo.
        // ============================================================

        // ============================================================
        // DESAFIO 19 - PodeCadastrar      ->  PodeCadastrar_PorTabela
        // Seis entradas. Exige um caso true e casos false.
        // Regra de ouro: UM erro por vez. Dois erros juntos e voce nao sabe
        // qual condicao pegou.
        // ============================================================

        // ============================================================
        // DESAFIO 20 - MensagemDoCadastro ->  MensagemDoCadastro_PorTabela
        // Exige: um e-mail INVALIDO e um e-mail valido porem JA CADASTRADO.
        // Sao duas recusas diferentes, e a ordem entre elas e o que este
        // desafio cobra.
        // ============================================================
    }
}
