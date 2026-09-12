using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acesso;

namespace AcessoTestes
{
    /// <summary>
    /// A SUITE COMPLETA dos 20 desafios - gabarito do professor.
    /// Nao distribua.
    ///
    /// Os nomes dos metodos sao os MESMOS que o corretor exige do aluno.
    /// Mudar um nome aqui sem mudar no Conteudo.cs quebra a pergunta 3 do
    /// corretor para aquele desafio.
    /// </summary>
    [TestClass]
    public class AcessoTests
    {
        // ============================== LOGIN ==============================

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

        [DataTestMethod]
        [DataRow("1234567", false, "7, um a menos")]
        [DataRow("12345678", true, "LIMITE: exatamente 8")]
        [DataRow("123456789", true, "9, folgado")]
        [DataRow("", false, "vazio")]
        public void SenhaTemTamanhoMinimo_PorTabela(string senha, bool esperado, string caso)
        {
            Assert.AreEqual(esperado, Login.SenhaTemTamanhoMinimo(senha), caso);
        }

        [DataTestMethod]
        [DataRow("abc123", true, "tem digito")]
        [DataRow("abcdef", false, "nenhum digito")]
        [DataRow("1", true, "um digito so")]
        [DataRow("", false, "vazio")]
        public void SenhaTemNumero_PorTabela(string senha, bool esperado, string caso)
        {
            Assert.AreEqual(esperado, Login.SenhaTemNumero(senha), caso);
        }

        [DataTestMethod]
        [DataRow("abcDef", true, "tem maiuscula")]
        [DataRow("abcdef", false, "nenhuma maiuscula")]
        [DataRow("A", true, "uma maiuscula so")]
        [DataRow("", false, "vazio")]
        public void SenhaTemMaiuscula_PorTabela(string senha, bool esperado, string caso)
        {
            Assert.AreEqual(esperado, Login.SenhaTemMaiuscula(senha), caso);
        }

        [DataTestMethod]
        [DataRow("abc", 0, "PISO: nao cumpre nenhum")]
        [DataRow("abcdefgh", 1, "so o tamanho")]
        [DataRow("abcdefg1", 2, "tamanho e numero")]
        [DataRow("Abcdefg1", 3, "TETO: os tres")]
        [DataRow("Abc1", 2, "numero e maiuscula, mas curta")]
        public void ForcaDaSenha_PorTabela(string senha, int esperado, string caso)
        {
            Assert.AreEqual(esperado, Login.ForcaDaSenha(senha), caso);
        }

        [DataTestMethod]
        [DataRow("abc", false, "forca 0")]
        [DataRow("abcdefgh", false, "forca 1, um a menos")]
        [DataRow("abcdefg1", true, "LIMITE: forca 2 ja aceita")]
        [DataRow("Abcdefg1", true, "forca 3")]
        public void SenhaAceita_PorTabela(string senha, bool esperado, string caso)
        {
            Assert.AreEqual(esperado, Login.SenhaAceita(senha), caso);
        }

        [DataTestMethod]
        [DataRow("ana", "Senha1", "ana", "Senha1", true, "tudo igual")]
        [DataRow("ANA", "Senha1", "ana", "Senha1", true, "usuario ignora a caixa")]
        [DataRow("ana", "senha1", "ana", "Senha1", false, "senha DIFERENCIA a caixa")]
        [DataRow("bia", "Senha1", "ana", "Senha1", false, "outro usuario")]
        [DataRow("", "", "ana", "Senha1", false, "vazio")]
        public void Autenticar_PorTabela(string usuario, string senha, string usuarioGravado,
                                         string senhaGravada, bool esperado, string caso)
        {
            Assert.AreEqual(esperado, Login.Autenticar(usuario, senha, usuarioGravado, senhaGravada), caso);
        }

        [DataTestMethod]
        [DataRow(0, 3, "ninguem errou ainda")]
        [DataRow(1, 2, "errou uma")]
        [DataRow(3, 0, "LIMITE: acabou")]
        [DataRow(4, 0, "nunca negativo")]
        public void TentativasRestantes_PorTabela(int tentativas, int esperado, string caso)
        {
            Assert.AreEqual(esperado, Login.TentativasRestantes(tentativas), caso);
        }

        [DataTestMethod]
        [DataRow(0, false, "tres restantes")]
        [DataRow(2, false, "LIMITE: ultima chance")]
        [DataRow(3, true, "LIMITE: bloqueou")]
        [DataRow(5, true, "passou do limite")]
        public void ContaBloqueada_PorTabela(int tentativas, bool esperado, string caso)
        {
            Assert.AreEqual(esperado, Login.ContaBloqueada(tentativas), caso);
        }

        [DataTestMethod]
        [DataRow("ana", "Senha1", 3, "Conta bloqueada por excesso de tentativas.", "ALVO: bloqueio vem antes de tudo")]
        [DataRow("", "Senha1", 0, "Preencha o usuario e a senha.", "usuario vazio")]
        [DataRow("ana", "errada", 0, "Usuario ou senha incorretos.", "senha errada")]
        [DataRow("ana", "Senha1", 0, "", "tudo certo")]
        public void MensagemDoLogin_PorTabela(string usuario, string senha, int tentativas,
                                              string esperado, string caso)
        {
            Assert.AreEqual(esperado, Login.MensagemDoLogin(usuario, senha, "ana", "Senha1", tentativas), caso);
        }

        // ============================= CADASTRO =============================

        [DataTestMethod]
        [DataRow("ana@senac.br", true, "arroba e ponto depois")]
        [DataRow("ana.senac.br", false, "sem arroba")]
        [DataRow("ana.b@senacbr", false, "ALVO: o ponto esta ANTES da arroba")]
        [DataRow("ana@senacbr", false, "arroba sem ponto")]
        [DataRow("", false, "vazio")]
        public void EmailValido_PorTabela(string email, bool esperado, string caso)
        {
            Assert.AreEqual(esperado, Cadastro.EmailValido(email), caso);
        }

        [DataTestMethod]
        [DataRow("279999999", false, "9 digitos")]
        [DataRow("2733334444", true, "LIMITE: 10, fixo")]
        [DataRow("27999998888", true, "LIMITE: 11, celular")]
        [DataRow("279999988887", false, "12 digitos")]
        [DataRow("2799999888a", false, "11, mas tem letra")]
        public void TelefoneValido_PorTabela(string telefone, bool esperado, string caso)
        {
            Assert.AreEqual(esperado, Cadastro.TelefoneValido(telefone), caso);
        }

        [DataTestMethod]
        [DataRow("2901000", false, "7 digitos")]
        [DataRow("29010000", true, "LIMITE: exatamente 8")]
        [DataRow("290100000", false, "9 digitos")]
        [DataRow("2901000a", false, "8, mas tem letra")]
        public void CepValido_PorTabela(string cep, bool esperado, string caso)
        {
            Assert.AreEqual(esperado, Cadastro.CepValido(cep), caso);
        }

        [DataTestMethod]
        [DataRow(17, false, "um a menos")]
        [DataRow(18, true, "LIMITE de baixo")]
        [DataRow(120, true, "LIMITE de cima")]
        [DataRow(121, false, "um a mais")]
        [DataRow(0, false, "zero")]
        public void IdadeValida_PorTabela(int idade, bool esperado, string caso)
        {
            Assert.AreEqual(esperado, Cadastro.IdadeValida(idade), caso);
        }

        [DataTestMethod]
        [DataRow("Ana", false, "uma palavra")]
        [DataRow("Ana Souza", true, "duas palavras")]
        [DataRow("  Ana   Souza  ", true, "ALVO: espaco sobrando nao muda a conta")]
        [DataRow("", false, "vazio")]
        [DataRow("   ", false, "so espaco")]
        public void NomeCompleto_PorTabela(string nome, bool esperado, string caso)
        {
            Assert.AreEqual(esperado, Cadastro.NomeCompleto(nome), caso);
        }

        [DataTestMethod]
        [DataRow("1234567890", false, "10 digitos")]
        [DataRow("12345678901", true, "LIMITE: exatamente 11")]
        [DataRow("123456789012", false, "12 digitos")]
        [DataRow("1234567890a", false, "11, mas tem letra")]
        public void CpfTemFormato_PorTabela(string cpf, bool esperado, string caso)
        {
            Assert.AreEqual(esperado, Cadastro.CpfTemFormato(cpf), caso);
        }

        [DataTestMethod]
        [DataRow("Ana", "a@b.c", "2733334444", true, "tudo preenchido")]
        [DataRow("", "a@b.c", "2733334444", false, "nome vazio")]
        [DataRow("   ", "a@b.c", "2733334444", false, "ALVO: nome so com espaco")]
        [DataRow("Ana", "", "2733334444", false, "email vazio")]
        [DataRow("Ana", "a@b.c", "   ", false, "telefone so com espaco")]
        public void CamposPreenchidos_PorTabela(string nome, string email, string telefone,
                                                bool esperado, string caso)
        {
            Assert.AreEqual(esperado, Cadastro.CamposPreenchidos(nome, email, telefone), caso);
        }

        [DataTestMethod]
        [DataRow("ana@senac.br", true, "ja existe")]
        [DataRow("ANA@SENAC.BR", true, "ALVO: nao diferencia maiuscula")]
        [DataRow("novo@senac.br", false, "conta nova")]
        [DataRow("", false, "vazio")]
        public void EmailJaCadastrado_PorTabela(string email, bool esperado, string caso)
        {
            Assert.AreEqual(esperado, Cadastro.EmailJaCadastrado(email), caso);
        }

        [DataTestMethod]
        [DataRow("Ana Souza", "nova@senac.br", "2733334444", "29010000", 30, "Abcdefg1", true, "tudo certo")]
        [DataRow("Ana", "nova@senac.br", "2733334444", "29010000", 30, "Abcdefg1", false, "nome com uma palavra")]
        [DataRow("Ana Souza", "ana@senacbr", "2733334444", "29010000", 30, "Abcdefg1", false, "email invalido")]
        [DataRow("Ana Souza", "ana@senac.br", "2733334444", "29010000", 30, "Abcdefg1", false, "email ja cadastrado")]
        [DataRow("Ana Souza", "nova@senac.br", "2733334444", "29010000", 17, "Abcdefg1", false, "idade 17")]
        [DataRow("Ana Souza", "nova@senac.br", "2733334444", "29010000", 30, "abcdefgh", false, "senha de forca 1")]
        public void PodeCadastrar_PorTabela(string nome, string email, string telefone, string cep,
                                            int idade, string senha, bool esperado, string caso)
        {
            Assert.AreEqual(esperado, Cadastro.PodeCadastrar(nome, email, telefone, cep, idade, senha), caso);
        }

        [DataTestMethod]
        [DataRow("Ana Souza", "nova@senac.br", 30, "", "tudo certo")]
        [DataRow("", "nova@senac.br", 30, "Preencha todos os campos.", "nome vazio")]
        [DataRow("Ana", "nova@senac.br", 30, "Informe o nome completo.", "nome incompleto")]
        [DataRow("Ana Souza", "ana@senacbr", 30, "E-mail invalido.", "ALVO: invalido vem antes de ja cadastrado")]
        [DataRow("Ana Souza", "ana@senac.br", 30, "Este e-mail ja esta cadastrado.", "ALVO: valido porem ja existe")]
        [DataRow("Ana Souza", "nova@senac.br", 17, "Idade fora do permitido.", "idade 17")]
        public void MensagemDoCadastro_PorTabela(string nome, string email, int idade,
                                                 string esperado, string caso)
        {
            Assert.AreEqual(esperado,
                Cadastro.MensagemDoCadastro(nome, email, "2733334444", "29010000", idade, "Abcdefg1"), caso);
        }
    }
}
