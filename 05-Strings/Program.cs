using var arquivo = new FileStream("musicas.csv", FileMode.Open, FileAccess.Read);
using var stream = new StreamReader(arquivo);

var musica = ObterMusicas(stream)          // Obtém as músicas do arquivo CSV
    .Where(m => m.Titulo.StartsWith("T"))  // Filtra músicas cujo título começa com "The"
    .FirstOrDefault();                    // Retorna a primeira música encontrada ou null se nenhuma for encontrada

if (musica is not null)
{
    Console.WriteLine($"Titulo da Música: {musica.Titulo}");
    Console.WriteLine();
    musica.Titulo =musica.Titulo.Replace("The ", ""); // Exemplo de uso do método Replace para substituir parte do título da música
    Console.WriteLine($"Titulo da Música: {musica.Titulo}");
}

void ValidarSenha()
{
    //var titulo = "Músicas Disponíveis";
    //foreach (var letra in titulo) Console.WriteLine(letra);

    var senha = "Santos.1912";

    var totalCaracteres = senha.Length;                             // Conta o número total de caracteres na senha
    var totalLetrasMaiusculas = senha.Count(c => char.IsUpper(c));  // Conta o número de caracteres maiúsculos na senha
    var totalLetrasMinusculas = senha.Count(c => char.IsLower(c));  // Conta o número de caracteres minúsculos na senha
    var totalNumeros = senha.Count(c => char.IsDigit(c));           // Conta o número de caracteres numéricos na senha
    var totalSimbolos = senha.Count(c => !char.IsLetterOrDigit(c)); // Conta o número de caracteres que não são letras ou dígitos (símbolos)

    Console.WriteLine("Verificando a força da senha...");
    if (totalCaracteres >= 8 && totalLetrasMaiusculas > 0 && totalLetrasMinusculas > 0 && totalNumeros > 0 && totalSimbolos > 0)
    {
        Console.WriteLine("Senha forte");
    }
    else
    {
        Console.WriteLine("Senha fraca");
    }

    void ExibirMusicas(IEnumerable<Musica> musicas)
    {
        var titulo = "Músicas Disponíveis";

        Console.WriteLine(titulo);
        foreach (var musica in musicas)
        {
            Console.WriteLine($"\t - {musica.Titulo} ({musica.Artista}) - {musica.Duracao} segundos - Lançamento: {musica.DataLancamento.ToShortDateString()}");
        }
    }
}

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
        Generos = partes[3].Split(",", StringSplitOptions.TrimEntries),   
        DataLancamento = Convert.ToDateTime(partes[4])
        };
            yield return musica;                          // Retorna a música atual e pausa a execução
            linha = stream.ReadLine();                   // Lê a próxima linha
    }
}

class Musica
{
    public string Titulo { get; set; }
    public string Artista { get; set; }
    public int Duracao { get; set; } // Duração em segundos
    public IEnumerable<string> Generos { get; set; }
    public DateTime DataLancamento { get; set; }

}