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
    private HashSet<Musica> set = []; // Utiliza um HashSet para garantir que não haja músicas duplicadas na playlist
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
            Console.WriteLine($"\t - {musica.Titulo} ({musica.Artista}) - {musica.Duracao} segundos");
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

    public void RemoverMusicaPeloTitulo(string titulo)
    {
        var musica = ObterPeloTitulo(titulo); // Utiliza o método ObterPeloTitulo para encontrar a música
        if (musica is not null)
        {
            playlist.Remove(musica); // Remove a música encontrada da playlist
        }
        else
        {
            Console.WriteLine($"Música com título '{titulo}' não encontrada na playlist."); // Mensagem de erro caso a música não seja encontrada
        }
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

    public void OrdenarPorDuracao()
    {
        playlist.Sort(new OrdenarPorDuracao()); // Utiliza a classe OrdenarPorDuracao para ordenar a playlist por duração
    }

    public void OrdenarPorTitulo()
    {
        playlist.Sort(new OrdenarPorTitulo()); // Utiliza a classe OrdenarPorTitulo para ordenar a playlist por título
    }

    public void OrdenarPorArtista()
    {
        playlist.Sort(new OrdenarPorArtista()); // Utiliza a classe OrdenarPorArtista para ordenar a playlist por artista
    }


    // -=-=-=-=-=- Métodos do ICollection<Musica> -=-=-=-=-=-

    public int Count => playlist.Count;

    public bool IsReadOnly => false; // false porque queremos permitir modificações na playlist

    public void Add(Musica musica)
    {
        if (set.Add(musica)) // Tenta adicionar a música ao HashSet para verificar se é uma música duplicada
        {
            playlist.Add(musica);
        }
        else
        {
            Console.WriteLine($"A música '{musica.Titulo}' já está na playlist. Não será adicionada novamente."); 
            // Mensagem de aviso caso a música seja duplicada
        }
    }

    public void Clear()
    {
        playlist.Clear(); 
        set.Clear(); // Limpa também o HashSet para manter a consistência
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
        set.Remove(musica); // Remove a música do HashSet para manter a consistência
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
