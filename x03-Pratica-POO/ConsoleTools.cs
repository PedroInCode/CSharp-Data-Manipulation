using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace x03_Pratica_POO;

internal class ConsoleTools
{
    public void ExibirMusicas(IEnumerable<Musica> musicas)
    {
        var cont = 1;
        Console.WriteLine("Exibindo músicas:");
        foreach (var musica in musicas)
        {
            Console.WriteLine($"\t - {musica.Titulo} - {musica.Artista} ({musica.Duracao} segundos)"); 
            cont++;

            if (cont > 10)
            {
                Console.WriteLine("\t - ...");
                break;
            }
        }
    }
}
