using var arquivo = new FileStream("musicas.csv", FileMode.Open, FileAccess.Read);
using var stream = new StreamReader(arquivo);


OperacaoDeObtencaoDeElementos(stream);

void OperacaoDeObtencaoDeElementos(StreamReader stream)
{
    var musicas = ObterMusicas(stream);

    var primeiraMusica = musicas.First(); // Obtém a primeira música da coleção usando o método First do LINQ
    Console.WriteLine($"A primeira música da coleção é '{primeiraMusica.Titulo}' do artista {primeiraMusica.Artista}.");

    var maiorDuracao = musicas.MaxBy(m => m.Duracao); // Obtém a duração máxima das músicas usando o método Max do LINQ
    Console.WriteLine($"A música com mair duração é {maiorDuracao.Titulo}");
}
void OperacoesDeAgrupamento(StreamReader stream)
{
    var artistas = ObterMusicas(stream)
    .GroupBy(m => m.Artista);

    Console.WriteLine("Artistas e suas músicas:");
    foreach (var artista in artistas.Take(5))
    {
        Console.WriteLine($"\nArtista: {artista.Key} com {artista.Count()} músicas no total");
        foreach (var musica in artista)
        {
            Console.WriteLine($"\t - {musica.Titulo} ({musica.Duracao} segundos)");
        }
    }
}
void EstatisticasDeMusicas(StreamReader stream)
{
    var musicas = ObterMusicas(stream).ToList(); // Converte o IEnumerable<Musica> para List<Musica> para evitar múltiplas iterações sobre o arquivo

    Console.WriteLine($"\nExistem {musicas.Count()} músicas na coleção.");
    Console.WriteLine($"\nExistem {musicas.Count(m => m.Duracao > 600)} músicas com mais do que 10 minutos na coleção.");
    Console.WriteLine($"\nA música com menor duração da coleção leva {musicas.Min(m => m.Duracao)} segundos.");
    Console.WriteLine($"\nA música com maior duração da coleção leva {musicas.Max(m => m.Duracao)} segundos.");
    Console.WriteLine($"\nA duração média das músicas da coleção é {musicas.Average(m => m.Duracao):F2} segundos.");
    Console.WriteLine($"\nVocê vai levar {musicas.Sum(m => m.Duracao)/(3600*24)} dias para ouvir toda a coleção!"); 
}
void OperacoesDeProjecao2(StreamReader stream)
{ 
    var generos = ObterMusicas(stream)
    .SelectMany(musica => musica.Generos)      // 1. Seleciona os gêneros de todas as músicas usando SelectMany do LINQ
    .Distinct()                               // 2. Remove os Generos duplicados usando Distinct do LINQ
    .OrderBy(genero => genero);              // 3. Ordena os Generos em ordem alfabética usando OrderBy do LINQ

    foreach (var genero in generos)
    {
        Console.WriteLine(genero);
    }
}
void OperacoesDeProjecao(StreamReader stream)
{
    var artistas = ObterMusicas(stream)
    .Select(musica => musica.Artista)      // 1. Seleciona o nome do artista usando Select do LINQ
    .Distinct()                           // 2. Remove os artistas duplicados usando Distinct do LINQ
    .OrderBy(artista => artista);        // 3. Ordena os artistas em ordem alfabética usando OrderBy do LINQ

    foreach (var artista in artistas)
    {
        Console.WriteLine(artista);
    }
}
void OperacoesDeFiltroEOrdenacao(StreamReader stream)
{
    var musicasDoColdplay =
        ObterMusicas(stream)                                        // 1. Obtém as músicas do arquivo CSV
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
    var linha = stream.ReadLine();                               // Lê a primeira linha (cabeçalho)
    while (linha is not null)                                   // Continua lendo até o final do arquivo
    {
        var partes = linha.Split(';');                        // Divide a linha em partes usando o ponto e vírgula como separador
        var musica = new Musica
        {
            Titulo = partes[0],
            Artista = partes[1],
            Duracao = Convert.ToInt32(partes[2]),
            Generos = partes[3].Split(",").Select(g => g.Trim())   // Divide os gêneros em partes usando a vírgula como separador e remove os espaços em branco usando Trim
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

}