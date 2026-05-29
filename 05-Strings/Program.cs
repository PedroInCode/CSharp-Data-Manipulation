using var arquivo = new FileStream("musicas.csv", FileMode.Open, FileAccess.Read);
using var stream = new StreamReader(arquivo);

var musicas = ObterMusicas(stream) // Obtém as músicas do arquivo CSV
    .Where(m => m.Titulo.StartsWith("The")) // Filtra músicas cujo título começa com "The"
    .Take(50); // Limita a exibição às primeiras 50 músicas
ExibirMusicas(musicas); // Exibe as músicas no console

void ExibirMusicas(IEnumerable<Musica> musicas)
{
    var titulo = "Músicas Disponíveis";

    Console.WriteLine(titulo);
    foreach (var musica in musicas)
    {
        Console.WriteLine($"\t - {musica.Titulo} ({musica.Artista}) - {musica.Duracao} segundos - Lançamento: {musica.DataLancamento.ToShortDateString()}");
    }
}

// Método que lê as músicas de um arquivo CSV e retorna um IEnumerable<Musica>

IEnumerable<Musica> ObterMusicas(StreamReader stream)
{
    var linha = stream.ReadLine();                               // Lê a primeira linha (cabeçalho)
    while (linha is not null)                                   // Continua lendo até o final do arquivo
    {
        var partes = linha.Split(';');                        // Divide a linha em partes usando o ponto e vírgula como separador
        var musica = new Musica
        {
        Titulo = partes[0],
        Artista = partes[1],
        Duracao = Convert.ToInt32(partes[2]),
        Generos = partes[3].Split(",").Select(g => g.Trim()),   
        DataLancamento = Convert.ToDateTime(partes[4])
        };
            yield return musica;                          // Retorna a música atual e pausa a execução
            linha = stream.ReadLine();                   // Lê a próxima linha
    }
}

// Classes
class Musica
{
    public string Titulo { get; set; }
    public string Artista { get; set; }
    public int Duracao { get; set; } // Duração em segundos
    public IEnumerable<string> Generos { get; set; }
    public DateTime DataLancamento { get; set; }

}