using var arquivo = new FileStream("musicas.csv", FileMode.Open, FileAccess.Read);
using var strean = new StreamReader(arquivo);


var musicasDoArtista =
    ObterMusicas(strean)                             // 1. Obtém as músicas do arquivo CSV
    .FiltrarPor( musica => musica.Artista == "Metallica")            // 2. Filtragem por artista usando o método de extensão
    .FiltrarPor(musica => musica.Duracao >= 400);              // 3. Filtragem por duracao usando o metodo de extensão
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

Func<Musica, bool> condicao; // Delegate = tipos que representam metodos com a mesma assinatura.

// Classes
static class Extensoes // classe precisa ser estática para conter métodos de extensão
{
    // Método precisa ser estático e o primeiro parâmetro deve usar a palavra-chave "this" para indicar que é um método de extensão
  
    public static IEnumerable<Musica> FiltrarPor(this IEnumerable<Musica> musicas, Func<Musica, bool> condicao)
    {
        foreach (var musica in musicas)
        {
            if (condicao(musica))
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