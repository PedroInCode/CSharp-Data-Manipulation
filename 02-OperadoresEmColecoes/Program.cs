/*
livremente a qualquer momento. Além disso, o aplicativo precisa oferecer a funcionalidade de
reprodução aleatória para uma playlist específica, proporcionando uma experiência de audição
dinâmica e variada, sem, contudo, alterar a ordem original que o usuário definiu. O desafio
é criar uma estrutura robusta que suporte a adição e remoção eficiente de músicas, a
reordenação flexível dentro das playlists e a seleção de faixas tanto em modo sequencial
quanto aleatório.


Funcoes que vamos implementar:
// [x] Criar as classes para musicas e playlist
// [ ] Listar musicas da playlist
// [ ] Adicionar musica à playlist
// [ ] Obter uma musica especifica da playlist
// [ ] Remover musica da playlist
// [ ] Tocar uma musica aleatoria da playlist
// [ ] Reordenar musicas segundo alguma logica especifica (ex. duracao)
// [ ] Uma playlist nao pode ter musicas repetidas
// [ ] Exibir as 10 musicas mais tocadas em todas as playlists (ranking)
// [ ] Player de musica com:
// [ ] - Fila de reproducao (para musicas avulsas e/ou playlists)
// [ ] - Historico de reproducao */

using System.Collections;

var musica1 = new Musica { Titulo = "Qua Pais É Esse", Artista = "Legião Urbana", Duracao = 350 };
var musica2 = new Musica { Titulo = "Tempo Perdido", Artista = "Legião Urbana", Duracao = 455 };
var musica3 = new Musica { Titulo = "Pro dia nascer feliz", Artista = "Barão Vermelho", Duracao = 345 };
var musica4 = new Musica { Titulo = "Eduardo e Mônica", Artista = "Legião Urbana", Duracao = 530 };
var musica5 = new Musica { Titulo = "Geração Coca-Cola", Artista = "Legião Urbana", Duracao = 350 };

var rockNacional = new Playlist { Nome = "Rock Nacional" };
rockNacional.AdicionarMusica(musica1);
rockNacional.AdicionarMusica(musica2);
rockNacional.AdicionarMusica(musica3);
rockNacional.AdicionarMusica(musica4);
rockNacional.AdicionarMusica(musica5);

ExibirPlaylist(rockNacional);

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
class Playlist : IEnumerable<Musica> // Implementando IEnumerable para permitir iteração sobre as músicas da playlist
{
    private List<Musica> lista = new List<Musica>(); // lista interna para armazenar as músicas da playlist x
    public string Nome { get; set; }

    public void AdicionarMusica(Musica musica) // Método para adicionar uma música à playlist
    {
        lista.Add(musica);
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
