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
// [x] Tocar uma musica aleatoria da playlist
// [x] Reordenar musicas segundo alguma logica especifica (ex. duracao)
// [x] Uma playlist nao pode ter musicas repetidas
// [x] Exibir as 10 musicas mais tocadas em todas as playlists (ranking)
// [ ] Player de musica com:
// [ ] - Fila de reproducao (para musicas avulsas e/ou playlists)
// [ ] - Historico de reproducao */

using System.Collections;
using System.Reflection.Metadata.Ecma335;

var musica1 = new Musica("Que Pais É Esse", "Legião Urbana", 350 );
var musica2 = new Musica("Tempo Perdido", "Legião Urbana", 455 );
var musica3 = new Musica("Pro dia nascer feliz", "Barão Vermelho", 345 );
var musica4 = new Musica("Eduardo e Mônica", "Legião Urbana", 530 );
var musica5 = new Musica("Geração Coca-Cola", "Legião Urbana", 350 );
var rockNacional = new Playlist { Nome = "Rock Nacional" };
rockNacional.Add(musica1);
rockNacional.Add(musica2);
rockNacional.Add(musica3);
rockNacional.Add(musica4);
rockNacional.Add(musica5);

var legiaoUrbana = new Playlist() { Nome = "Mais Populares da Legião Urbana" };
legiaoUrbana.Add(musica1);
legiaoUrbana.Add(musica2);
legiaoUrbana.Add(musica4);
legiaoUrbana.Add(musica5);

// Console:

var player = new PlayerDeMusica();
player.AdicionarNaFila(musica1);
player.AdicionarNaFila(rockNacional);
ExibirFila(player);

// _______________________________________________________________________________________________________________________________________
void ExibirFila(PlayerDeMusica player)
{
    Console.WriteLine("\nFila de reprodução:");
    foreach (var musica in player.Fila())
    {
        Console.WriteLine($"\t - {musica.Titulo} ({musica.Artista}) - {musica.Duracao} segundos");
    }
}
void ExibirMaisTocadas(Playlist playlist1, Playlist playlist2)
{
    // Musica (Chave/key) - Contagem (Valor/value)
    Dictionary<Musica, int> ranking = [];
    foreach (var musica in playlist1)
    {
        ranking.Add(musica, 1); // Inicializa a contagem de cada música da primeira playlist com 1
    }

    foreach (var musica in playlist2)
    {
        if (ranking.TryGetValue(musica, out int contagem)) // Verifica se a música da segunda playlist já está presente no ranking
        {
            contagem++; // Incrementa a contagem se a música já estiver presente no ranking
            ranking[musica] = contagem;
        }
        else
        {
            ranking[musica] = 1; // Adiciona a música ao ranking com contagem 1 se ela não estiver presente
        }
    }

    List<KeyValuePair<Musica, int>> top = new(ranking); // Converte o dicionário em uma lista de pares chave-valor para ordenação
    top.Sort(new PorContagem()); // Ordena a lista de pares chave-valor com base na contagem (valor) em ordem decrescente

    Console.WriteLine($"\nTop 5 Músicas mais incluidas nas playlists");
    int contador = 1;
    foreach (var par in top)
    {
        Console.WriteLine($"\t - {par.Key.Titulo}");
        contador++;
        if (contador > 5) break; // Exibe apenas as 5 músicas mais tocadas
    }
}

void ExibirPlaylist(Playlist playlist)
{
    Console.WriteLine($"\n Tocando as músicas da playlist: {playlist.Nome}");
    foreach (var musica in playlist)
    {
        Console.WriteLine($"\t - {musica.Titulo} ({musica.Artista}) - {musica.Duracao} segundos");
    }
}

void RemoverMusicaPeloTitulo(Playlist playlist, string titulo)
{
    var musicaEncontrada = playlist.ObterPeloTitulo(titulo);
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
}

void ExibirMusicaAleatoria(Playlist playlist)
{
    var musicaAleatoria = rockNacional.ObterMusicaAleatoria();
    if (musicaAleatoria is not null)
    {
        Console.WriteLine($"\nTocando música aleatória: {musicaAleatoria.Titulo}");
    }
    else
    {
        Console.WriteLine("Playlist vazia...");
    }
}

class CompararPorArtista : IComparer<Musica>
{
    public int Compare(Musica? x, Musica? y)
    {
        if (x is null && y is null) return 0;
        if (x is null) return 1;
        if (y is null) return -1;
        return x.Artista.CompareTo(y.Artista);
    }
}

class CompararPorTitulo : IComparer<Musica>
{
    public int Compare(Musica? x, Musica? y)
    {
        if (x is null && y is null) return 0;
        if (x is null) return 1;
        if (y is null) return -1;
        return x.Titulo.CompareTo(y.Titulo);
    }
}

class PorContagem : IComparer<KeyValuePair<Musica, int>>
{
    public int Compare(KeyValuePair<Musica, int> x, KeyValuePair<Musica, int> y)
    {
        return y.Value.CompareTo(x.Value); // Multiplica por -1 para ordenar em ordem decrescente com base na contagem (valor)
    }
}

class Musica : IComparable
{
    public string Titulo { get; } = string.Empty;
    public string Artista { get; } = string.Empty;
    public int Duracao { get; }

    public Musica(string titulo, string artista, int duracao)
    {
        Titulo = titulo;
        Artista = artista;
        Duracao = duracao;
    }

    public int CompareTo(object? other) // iguais : 0, menor : -1, maior : 1
    {
        if (other is null) return -1;
        if (other is Musica outraMusica) return this.Duracao.CompareTo(outraMusica.Duracao);
        return -1;
    }

    public override bool Equals(object? obj) // Sobrescreve o método Equals para comparar músicas com base no título e artista, garantindo que músicas com o mesmo título e artista sejam consideradas iguais, mesmo que sejam instâncias diferentes
    {
        if (obj is null) return false;
        if (obj is Musica outraMusica) return this.Titulo.Equals(outraMusica.Titulo) && this.Artista.Equals(outraMusica.Artista);
        return false;
    }

    public override int GetHashCode()
    {
        return this.Titulo.GetHashCode() ^ this.Artista.GetHashCode(); // Sobrescreve o método GetHashCode para garantir que o hash code de uma música seja consistente com a implementação de Equals, usando o título e artista para calcular o hash code
    }
}
class Playlist : ICollection<Musica> // Implementando IEnumerable para permitir iteração sobre as músicas da playlist
{
    private HashSet<Musica> set = []; // Conjunto para rastrear os títulos das músicas e evitar duplicatas
    private List<Musica> lista = new List<Musica>(); // lista interna para armazenar as músicas da playlist x
    public string Nome { get; set; } = string.Empty;

    public int Count => lista.Count;

    public bool IsReadOnly => false;

    public void Add(Musica musica)
    {
        if (set.Add(musica)) // Tenta adicionar a música ao conjunto. Se for bem-sucedido, significa que a música não é uma duplicata e pode ser adicionada à lista.
        {
            lista.Add(musica);
        }
        else
        {
            Console.WriteLine($"A música '{musica.Titulo}' já está presente na playlist '{Nome}' e não será adicionada novamente.");
        }
    }

    public void Clear()
    {
        set.Clear();
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
        int indiceAleatorio = random.Next(0, lista.Count );
        return lista[indiceAleatorio];
    }

    public void OrdenarPorDuracao()
    {
        lista.Sort(); // Ordena usando a implementação de CompareTo da classe Musica, que compara pela duração
    }

    public void OrdenarPorArtista()
    {
        lista.Sort(new CompararPorArtista()); // Ordena usando a classe CompararPorArtista, que implementa IComparer<Musica>
    }

    public void OrdenarPorTitulo()
    {
        lista.Sort(new CompararPorTitulo()); // Ordena usando a classe CompararPorTitulo, que implementa IComparer<Musica>
    }

    public void CopyTo(Musica[] array, int arrayIndex)
    {
        lista.CopyTo(array, arrayIndex);
    }
    public bool Remove(Musica musica)
    {
        set.Remove(musica); // Remove a música do conjunto para garantir que ela possa ser adicionada novamente no futuro, se desejado
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
class PlayerDeMusica
{
    private List<Musica> fila = new List<Musica>(); // Lista para armazenar a fila de reprodução
    public void AdicionarNaFila(Musica musica)
    {
        fila.Add(musica); // Adiciona a música à fila de reprodução
    }

    public void AdicionarNaFila(Playlist playlist)
    {
        foreach (var musica in playlist)
            AdicionarNaFila(musica); // Adiciona todas as músicas da playlist à fila de reprodução
    }

    public IEnumerable<Musica> Fila()
    {
        foreach (var musica in fila)
            yield return musica; // Retorna as músicas da fila de reprodução uma a uma
    }
}
    