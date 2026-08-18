using System;
using System.Text;

namespace Revisao
{
    // O conferidor da variante B.
    //
    // IMPORTANTE, E DIGA ISSO AO ALUNO: isto NAO e um compilador. Os projetos
    // sao net8.0, onde nao existe compilacao em tempo de execucao sem baixar
    // pacote - e o laboratorio nao tem internet.
    //
    // O que ele faz: normaliza o texto digitado (espacos, quebras de linha) e
    // confere se as pecas que o desafio exige estao la - a lista exigeTokens do
    // conteudo-revisao.json. Essa lista NAO e a resposta: sao as pecas que o
    // proprio enunciado ja pede, tipo "usa switch", "tem default", "tem return".
    //
    // Por que nao comparar com o gabarito: o conteudo-revisao.json vai junto com
    // o programa para a maquina do aluno. Qualquer um abre o arquivo num editor
    // de texto. Guardar a resposta ali seria entregar a resposta.
    //
    // Consequencia: um codigo que tenha as pecas certas e esteja errado no
    // miolo passa por aqui. E o preco de nao ter compilador - e por isso a
    // variante A, que testa de verdade, continua sendo a recomendada.

    public static class Validador
    {
        public static bool Confere(Topico t, string digitado, out string motivo)
        {
            string alvo = Normalizar(digitado);

            if (alvo.Length == 0)
            {
                motivo = "Voce ainda nao escreveu nada.";
                return false;
            }

            // Erros comuns valem uma dica especifica, em vez de um "nao" seco.
            if (ContemAtribuicaoEmIf(alvo) || ContemAtribuicaoEmReturn(alvo))
            {
                motivo = "Parece que voce usou = onde queria perguntar se e igual. "
                       + "Lembre: = guarda, == pergunta.";
                return false;
            }

            // As pecas que o enunciado pede.
            if (t.Desafio.ExigeTokens != null)
            {
                foreach (string token in t.Desafio.ExigeTokens)
                {
                    if (alvo.IndexOf(Normalizar(token), StringComparison.Ordinal) < 0)
                    {
                        motivo = "Faltou uma peca: nao encontrei \"" + token + "\" no seu codigo. "
                               + "Releia o enunciado e o exemplo ao lado.";
                        return false;
                    }
                }
            }

            if (!alvo.EndsWith(";") && !alvo.EndsWith("}"))
            {
                motivo = "O seu codigo nao termina em ponto e virgula nem em chave. "
                       + "Faltou fechar alguma coisa?";
                return false;
            }

            motivo = "";
            return true;
        }

        // Tira tudo o que nao muda o sentido do codigo: espacos repetidos,
        // quebras de linha, e espaco em volta de pontuacao. Assim o aluno pode
        // indentar como quiser.
        public static string Normalizar(string texto)
        {
            if (texto == null) { return ""; }

            StringBuilder sb = new StringBuilder();
            bool espacoPendente = false;

            foreach (char c in texto)
            {
                if (c == '\r' || c == '\n' || c == '\t' || c == ' ')
                {
                    espacoPendente = sb.Length > 0;
                    continue;
                }

                if (espacoPendente)
                {
                    // espaco so importa entre dois caracteres de palavra
                    char anterior = sb[sb.Length - 1];
                    if (EhPalavra(anterior) && EhPalavra(c)) { sb.Append(' '); }
                    espacoPendente = false;
                }

                sb.Append(c);
            }

            return sb.ToString();
        }

        private static bool EhPalavra(char c)
        {
            return char.IsLetterOrDigit(c) || c == '_' || c == '"';
        }

        private static bool ContemAtribuicaoEmIf(string normalizado)
        {
            int i = normalizado.IndexOf("if(", StringComparison.Ordinal);

            while (i >= 0)
            {
                int fim = normalizado.IndexOf(')', i);
                if (fim < 0) { break; }

                if (TemIgualSolto(normalizado.Substring(i + 3, fim - i - 3))) { return true; }

                i = normalizado.IndexOf("if(", fim, StringComparison.Ordinal);
            }

            return false;
        }

        // "return vida = 20 && ..." - o mesmo erro do if, so que num return.
        // Aqui um = solto nunca e proposital: nao existe desafio que peca
        // atribuicao dentro da expressao devolvida.
        private static bool ContemAtribuicaoEmReturn(string normalizado)
        {
            int i = normalizado.IndexOf("return ", StringComparison.Ordinal);

            while (i >= 0)
            {
                int fim = normalizado.IndexOf(';', i);
                if (fim < 0) { fim = normalizado.Length; }

                if (TemIgualSolto(normalizado.Substring(i + 7, fim - i - 7))) { return true; }

                i = normalizado.IndexOf("return ", fim, StringComparison.Ordinal);
            }

            return false;
        }

        private static bool TemIgualSolto(string trecho)
        {
            for (int k = 0; k < trecho.Length; k++)
            {
                if (trecho[k] != '=') { continue; }

                bool duplo = (k > 0 && trecho[k - 1] == '=')
                          || (k + 1 < trecho.Length && trecho[k + 1] == '=');
                bool comparador = k > 0 && (trecho[k - 1] == '!'
                                         || trecho[k - 1] == '<'
                                         || trecho[k - 1] == '>');

                if (!duplo && !comparador) { return true; }
            }

            return false;
        }
    }
}
