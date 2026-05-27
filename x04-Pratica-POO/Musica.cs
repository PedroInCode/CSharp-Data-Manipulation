using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace x04_Pratica_POO;

internal class Musica
{
    public string Titulo { get; set; }
    public string Artista { get; set; }
    public int Duracao { get; set; } // Duração em segundos
    public IEnumerable<string> Generos { get; set; }
}
