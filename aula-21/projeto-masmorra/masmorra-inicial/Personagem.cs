using System;

namespace Masmorra
{
    // ---------------------------------------------------------------------
    // O MOLDE de todo mundo que anda na masmorra: voce e os monstros.
    //
    // ESTE ARQUIVO JA ESTA PRONTO. Voce nao mexe aqui.
    //
    // Repare que e a mesma ideia do Lutador do projeto Batalha: campos
    // publicos, com o valor inicial na propria declaracao. O que mudou e
    // que agora o personagem tambem sabe ONDE ESTA - a Coluna e a Linha.
    // ---------------------------------------------------------------------
    public class Personagem
    {
        public string Nome = "";
        public string Simbolo = "";

        public int Vida = 20;
        public int VidaMaxima = 20;
        public int Forca = 5;
        public int Defesa = 0;

        public int Coluna = 0;
        public int Linha = 0;

        // Uma copia deste personagem. O jogo usa isso para deixar a sua
        // luta acontecer sem risco: se o seu while nao terminar, quem fica
        // estragado e a copia, e nao o heroi de verdade.
        public Personagem Copiar()
        {
            Personagem c = new Personagem();

            c.Nome = Nome;
            c.Simbolo = Simbolo;
            c.Vida = Vida;
            c.VidaMaxima = VidaMaxima;
            c.Forca = Forca;
            c.Defesa = Defesa;
            c.Coluna = Coluna;
            c.Linha = Linha;

            return c;
        }
    }
}
