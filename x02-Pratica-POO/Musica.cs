using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColocandoNaPratica;

internal class Musica
{
    public string Nome { get; set; }
    public string Artista { get; set; }
    public int Duracao { get; set; } // Duração em segundos

    public Musica(string nome, string artista, int duracao)
    {
        Nome = nome;
        Artista = artista;
        Duracao = duracao;
    }
}
