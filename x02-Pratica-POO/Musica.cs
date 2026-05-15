using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColocandoNaPratica;

internal class Musica
{
    public string Titulo { get; set; } = string.Empty;
    public string Artista { get; set; }
    public int Duracao { get; set; } // Duração em segundos

    public Musica(string titulo, string artista, int duracao)
    {
        Titulo = titulo;
        Artista = artista;
        Duracao = duracao;
    }
}
