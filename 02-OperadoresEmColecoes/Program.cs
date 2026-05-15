/*
livremente a qualquer momento. Além disso, o aplicativo precisa oferecer a funcionalidade de
reprodução aleatória para uma playlist específica, proporcionando uma experiência de audição
dinâmica e variada, sem, contudo, alterar a ordem original que o usuário definiu. O desafio
é criar uma estrutura robusta que suporte a adição e remoção eficiente de músicas, a
reordenação flexível dentro das playlists e a seleção de faixas tanto em modo sequencial
quanto aleatório.


Funcoes que vamos implementar:
// [x] Criar as classes para musicas e playlist
// [x] Listar musicas da playlist
// [x] Adicionar musica à playlist
// [x] Obter uma musica especifica da playlist
// [x] Remover musica da playlist
// [ ] Tocar uma musica aleatoria da playlist
// [ ] Reordenar musicas segundo alguma logica especifica (ex. duracao)
// [ ] Uma playlist nao pode ter musicas repetidas
// [ ] Exibir as 10 musicas mais tocadas em todas as playlists (ranking)
// [ ] Player de musica com:
// [ ] - Fila de reproducao (para musicas avulsas e/ou playlists)
// [ ] - Historico de reproducao */

using System.Collections;
using System.Reflection.Metadata.Ecma335;

var musica1 = new Musica { Titulo = "Qua Pais É Esse", Artista = "Legião Urbana", Duracao = 350 };
var musica2 = new Musica { Titulo = "Tempo Perdido", Artista = "Legião Urbana", Duracao = 455 };
var musica3 = new Musica { Titulo = "Pro dia nascer feliz", Artista = "Barão Vermelho", Duracao = 345 };
var musica4 = new Musica { Titulo = "Eduardo e Mônica", Artista = "Legião Urbana", Duracao = 530 };
var musica5 = new Musica { Titulo = "Geração Coca-Cola", Artista = "Legião Urbana", Duracao = 350 };

var rockNacional = new Playlist { Nome = "Rock Nacional" };
rockNacional.Add(musica1);
rockNacional.Add(musica2);
rockNacional.Add(musica3);
rockNacional.Add(musica4);
rockNacional.Add(musica5);

ExibirPlaylist(rockNacional);

var musicaEncontrada = rockNacional.ObterPeloTitulo("Eduardo e Mônica");
if (musicaEncontrada is not null)
{
    Console.WriteLine("\nRemovendo a música...");
    rockNacional.Remove(musicaEncontrada);
}
else
{
    Console.WriteLine("Música não encontrada.");
}

ExibirPlaylist(rockNacional);

var musicaAleatoria = rockNacional.ObterMusicaAleatoria();
if (musicaAleatoria is not null)
{
    Console.WriteLine($"\nTocando música aleatória: {musicaAleatoria.Titulo}");
}
else
{
    Console.WriteLine("Playlist vazia...");
}

    // _______________________________________________________________________________________________________________________________________

    void ExibirPlaylist(Playlist playlist)
    {
        Console.WriteLine($"\n Tocando as músicas da playlist: {playlist.Nome}");
        foreach (var musica in playlist)
        {
            Console.WriteLine($"\t - {musica.Titulo}");
        }
    }

class Musica
{
    public string Titulo { get; set; }
    public string Artista { get; set; }
    public int Duracao { get; set; }
}
class Playlist : ICollection<Musica> // Implementando IEnumerable para permitir iteração sobre as músicas da playlist
{
    private List<Musica> lista = new List<Musica>(); // lista interna para armazenar as músicas da playlist x
    public string Nome { get; set; }

    public int Count => lista.Count;

    public bool IsReadOnly => false;

    public void Add(Musica musica)
    {
        lista.Add(musica);
    }

    public void Clear()
    {
        lista.Clear();
    }

    public bool Contains(Musica musicaX)
    {
        return lista.Contains(musicaX);
    }

    public Musica? ObterPeloTitulo(string titulo)
    {
        foreach (var musica in lista)
        {
            if (musica.Titulo == titulo)
                return musica;
        }
        return null;
    }

    public Musica? ObterMusicaAleatoria()
    {
        if (lista.Count == 0)
            return null;
        
        var random = new Random();
        int indiceAleatorio = random.Next(0, lista.Count -1);
        return lista[indiceAleatorio];
    }

    public void CopyTo(Musica[] array, int arrayIndex)
    {
        lista.CopyTo(array, arrayIndex);
    }
    public bool Remove(Musica musica)
    {
        return lista.Remove(musica);
    }

    public IEnumerator<Musica> GetEnumerator() // Implementação do método GetEnumerator para permitir a iteração sobre as músicas da playlist
    {
        return lista.GetEnumerator();
    }


    IEnumerator IEnumerable.GetEnumerator() // Metodo antigo para compatibilidade com IEnumerable, delegando a chamada para o método genérico GetEnumerator
    {
        return GetEnumerator();
    }
}
