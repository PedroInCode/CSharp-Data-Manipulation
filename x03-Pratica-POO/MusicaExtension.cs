using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace x03_Pratica_POO;

static class MusicaExtension
{
    public static IEnumerable<Musica> FiltrarPor(this IEnumerable<Musica> musicas, Func<Musica, bool> condicao)
    {
        foreach (var musica in musicas)
        {
            if (condicao(musica))
            {
                yield return musica;
            }
        }
    }
}
