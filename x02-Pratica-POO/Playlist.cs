using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ColocandoNaPratica;

internal class Playlist : ICollection<Musica>
{
    private List<Musica> playlist = new List<Musica>();
    public string Nome { get; set; }

    // -=-=-=-=-=- Construtor para inicializar a playlist com um nome -=-=-=-=-=-
    public Playlist(string nome)
    {
        Nome = nome;
    }

    // -=-=-=-=-=- Metodos da playlist -=-=-=-=-=-

    public void ExibirPlaylist()
    {
        Console.WriteLine($"Tocando as músicas de {this.Nome}");
        foreach (var musica in this.playlist)
        {
            Console.WriteLine($"\t - {musica.Titulo}");
        }
    }

    public Musica? ObterPeloTitulo(string titulo)
    {
        foreach (var musica in playlist)
        {
            if (musica.Titulo == titulo)
            {
                return musica; // Retorna a música encontrada
            }
        }
        return null; // Retorna null se a música não for encontrada
    }

    public Musica? TocarMusicaAleatoria()
    {
        if (playlist.Count == 0)
        {
            return null; // Retorna null se a playlist estiver vazia
        }

        Random random = new Random();
        var indiceAleatorio = random.Next(0, playlist.Count); // Gera um índice aleatório dentro do intervalo da lista
        return playlist[indiceAleatorio]; // Retorna a música correspondente ao índice aleatório
    }

    // -=-=-=-=-=- Métodos do ICollection<Musica> -=-=-=-=-=-

    public int Count => playlist.Count;

    public bool IsReadOnly => false; // false porque queremos permitir modificações na playlist

    public void Add(Musica musica)
    {
        playlist.Add(musica);
    }

    public void Clear()
    {
        playlist.Clear();
    }

    public bool Contains(Musica musica)
    {
        return playlist.Contains(musica);
    }

    public void CopyTo(Musica[] array, int arrayIndex)
    {
        playlist.CopyTo(array, arrayIndex); // Utiliza o método CopyTo da List<Musica> para copiar os elementos para o array
    }
    public bool Remove(Musica musica)
    {
        return playlist.Remove(musica);
    }

    // -=-=-=-=-=- Implementação do método GetEnumerator para permitir a iteração sobre a playlist -=-=-=-=-=-
    public IEnumerator<Musica> GetEnumerator()
    {
        return playlist.GetEnumerator(); // Retorna o enumerador da lista de músicas
    }


    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
