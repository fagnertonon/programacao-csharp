using System;

namespace SistemaLogin
{
    /// <summary>
    /// CRIAR CONTA - programa de console.
    ///
    /// E a RESERVA da noite: a tela de verdade e o projeto login-tela, em
    /// Windows Forms. Este aqui existe para o caso de a janela nao abrir
    /// em alguma maquina - as regras testadas sao exatamente as mesmas,
    /// porque os dois usam o mesmo Cadastro.cs.
    ///
    /// Este arquivo so pergunta, e manda a Tela mostrar o que o Cadastro
    /// decidiu. Nenhuma REGRA mora aqui.
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            bool sair = false;

            Tela.MostrarTitulo("Criar conta - Portal do Aluno");
            Tela.MostrarLinha("Ja existem tres contas no sistema. Tente criar a sua.");

            while (sair == false)
            {
                Tela.MostrarMenu();
                int opcao = Entrada.LerOpcao();

                switch (opcao)
                {
                    case 1:
                        Tela.MostrarTitulo("Criar uma conta");

                        string usuario = Entrada.LerTexto("Usuario: ");
                        string senha = Entrada.LerTexto("Senha: ");
                        string repetir = Entrada.LerTexto("Repita a senha: ");

                        Tela.MostrarLinha("");
                        Tela.MostrarResposta("Usuario preenchido ......... ", Cadastro.UsuarioPreenchido(usuario));
                        Tela.MostrarResposta("Usuario disponivel ......... ", Cadastro.UsuarioDisponivel(usuario));
                        Tela.MostrarResposta("Senha preenchida ........... ", Cadastro.SenhaPreenchida(senha));
                        Tela.MostrarResposta("As senhas conferem ......... ", Cadastro.SenhasConferem(senha, repetir));
                        Tela.MostrarResposta("Senha fora da lista ........ ", Cadastro.SenhaProibida(senha) == false);
                        Tela.MostrarResposta("Sem repeticao demais ....... ", Cadastro.SemRepeticaoDemais(senha));
                        Tela.MostrarResposta("Senha != usuario ........... ", Cadastro.SenhaDiferenteDoUsuario(usuario, senha));

                        Tela.MostrarLinha("");

                        if (Cadastro.PodeCriarConta(usuario, senha, repetir) == true)
                        {
                            Tela.MostrarOk("Conta criada com sucesso.");
                        }
                        else
                        {
                            Tela.MostrarErro(Cadastro.MensagemDoErro(usuario, senha, repetir));
                        }

                        Tela.Pausar();
                        break;

                    case 2:
                        Tela.MostrarTitulo("As contas que ja existem");
                        Tela.MostrarLinha("   ana        bruno        carla");
                        Tela.MostrarLinha("");
                        Tela.MostrarLinha("E as senhas proibidas por serem obvias demais:");
                        Tela.MostrarLinha("   senha      admin        qwerty        senac");
                        Tela.Pausar();
                        break;

                    case 3:
                        Tela.MostrarTitulo("Contar caracteres iguais seguidos");
                        Tela.MostrarLinha("Tres iguais seguidos ja e repeticao demais.");

                        string texto = Entrada.LerTexto("Digite um texto: ");

                        Tela.MostrarLinha("");
                        Tela.MostrarLinha("Maior sequencia de iguais: " + Cadastro.CaracteresIguaisSeguidos(texto));
                        Tela.MostrarResposta("Passa na regra ............. ", Cadastro.SemRepeticaoDemais(texto));
                        Tela.Pausar();
                        break;

                    case 0:
                        sair = Entrada.Confirmar("Deseja mesmo sair?");
                        break;
                }
            }

            Tela.MostrarLinha("Ate a proxima.");
        }
    }
}
