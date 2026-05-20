using var arquivo = new FileStream("musicas.csv", FileMode.Open, FileAccess.Read);
using var strean = new StreamReader(arquivo);


var musicasDoArtista = 
    ObterMusicas(strean)                  // 1. Obtém as músicas do arquivo CSV
    .FiltrarPor("Coldplay");              // 2. Filtragem por artista usando o método de extensão
ExibirMusicas(musicasDoArtista);

// ----------------------------------------------------------------------------------------------------------------- //

// Método que exibe as músicas no console
void ExibirMusicas(IEnumerable<Musica> musicas)
{
    var contador = 1;
    Console.WriteLine("\nExbindo as Músicas:");
    foreach (var musica in musicas)
    {
        Console.WriteLine($"\t - {musica.Titulo} ({musica.Artista}) - {musica.Duracao} segundos");
        contador ++;
        if (contador > 10) break; // Limita a exibição a 10 músicas

    }
}

// Método que lê as músicas de um arquivo CSV e retorna um IEnumerable<Musica>
IEnumerable<Musica> ObterMusicas(StreamReader stream)
{
    var linha = stream.ReadLine(); // Lê a primeira linha (cabeçalho)
    while (linha is not null) // Continua lendo até o final do arquivo
    {
        var partes = linha.Split(';'); // Divide a linha em partes usando o ponto e vírgula como separador
        var musica = new Musica
        {
            Titulo = partes[0],
            Artista = partes[1],
            Duracao = Convert.ToInt32(partes[2])
        };
        yield return musica; // Retorna a música atual e pausa a execução
        linha = stream.ReadLine(); // Lê a próxima linha
    }
}


// Classes
static class Extensoes
{
    public static IEnumerable<Musica> FiltrarPor(this IEnumerable<Musica> musicas, string artista)
    {
        foreach (var musica in musicas)
        {
            if (musica.Artista.Equals(artista, StringComparison.OrdinalIgnoreCase))
            {
                yield return musica; // Retorna a música que atende ao critério
            }
        }
    }
}
class Musica
{
    public string Titulo { get; set; }
    public string Artista { get; set; }
    public int Duracao { get; set; } // Duração em segundos
    
}