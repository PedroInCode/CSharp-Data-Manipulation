using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace x03_Pratica_POO;

internal class LeitorArquivo
{
    public IEnumerable<Musica> ObterMusicas(StreamReader stream)
    {
        var linha = stream.ReadLine();

        while (linha is not null)
        {
            var partes = linha.Split(";");

            var musica = new Musica
            {
                Titulo = partes[0],
                Artista = partes[1],
                Duracao = int.Parse(partes[2])
            };

            yield return musica;
            linha = stream.ReadLine();
        }
    }
}
