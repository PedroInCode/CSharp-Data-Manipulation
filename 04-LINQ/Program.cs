using var arquivo = new FileStream("musicas.csv", FileMode.Open, FileAccess.Read);
using var strean = new StreamReader(arquivo);

var artistas = ObterMusicas(strean)
    .Select(musica => musica.Artista)      // 1. Seleciona o nome do artista usando Select do LINQ
    .Distinct()                           // 2. Remove os artistas duplicados usando Distinct do LINQ
    .OrderBy(artista => artista);        // 3. Ordena os artistas em ordem alfabética usando OrderBy do LINQ

foreach (var artista in artistas)
{
       Console.WriteLine(artista);
}


// Método que exibe as músicas no console

void OperacoesDeFiltroEOrdenacao(StreamReader stream)
{
    var musicasDoColdplay =
        ObterMusicas(strean)                                        // 1. Obtém as músicas do arquivo CSV
        .Where(musica => musica.Artista == "Coldplay")             // 2. Filtra as músicas do artista "Coldplay" usando Where do LINQ
        .OrderBy(musica => musica.Titulo)                         // 3. Ordena as músicas por título usando OrderBy do LINQ
        //.ThenBy(musica => musica.Duracao)                      // 4. Ordena as músicas por duração usando ThenBy do LINQ (opcional)
        .Skip(5 * 2)                                            // 5. Pula as primeras 10 músicas usando o método Skip do LINQ 
        .Take(5);                                              // 6. Pega as próximas 5 músicas usando o método Take do LINQ

    ExibirMusicas(musicasDoColdplay);
}

void ExibirMusicas(IEnumerable<Musica> musicas)
{
    var contador = 1;
    Console.WriteLine("\nExbindo as Músicas:");
    foreach (var musica in musicas)
    {
        Console.WriteLine($"\t - {musica.Titulo} ({musica.Artista}) - {musica.Duracao} segundos");
        contador++;
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

class Musica
{
    public string Titulo { get; set; }
    public string Artista { get; set; }
    public int Duracao { get; set; } // Duração em segundos

}