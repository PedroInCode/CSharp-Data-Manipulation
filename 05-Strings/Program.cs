using System.Data.Common;
using System.Runtime.CompilerServices;

using var arquivo = new FileStream("musicas.csv", FileMode.Open, FileAccess.Read);
using var stream = new StreamReader(arquivo);

var musicas = ObterMusicas(stream)
    .Take(20);

ExibirMusicasEmTabela(musicas);



void ExecutarLaboratorioStringPool()
{
    #region 1. Conceito de Interning 
    var artista1 = "Coldplay";
    var artista2 = "Coldplay";
    var artista3 = new string("Coldplay"); // Nova instância, pula a piscina

    // Apontam para o mesmo lugar na piscina
    Console.WriteLine($"artista1 e artista2 usam mesma memória? {ReferenceEquals(artista1, artista2)}"); // True
    #endregion

    #region 2. O Caso do ToUpper e string.Intern()
    var artista4 = "COLDPLAY"; // Vai para a piscina por ser literal
    var artista5 = artista1.ToUpper(); // Cria nova string na Heap, NÃO vai para a piscina
    var artista6 = string.Intern(artista1.ToUpper()); // Procura na piscina, acha o de artista4 e reaproveita!

    Console.WriteLine($"artista4 e artista5 usam mesma memória? {ReferenceEquals(artista4, artista5)}"); // False
    Console.WriteLine($"artista4 e artista6 usam mesma memória? {ReferenceEquals(artista4, artista6)}"); // True (Mágica do string.Intern)
    #endregion
}
void ComparandoStrings(StreamReader stream, string artista)
{
    var musicas = ObterMusicas(stream)
        .Where(m => m.Artista.Equals(artista, StringComparison.OrdinalIgnoreCase)); // Filtra as músicas do artista especificado, ignorando diferenças de maiúsculas e minúsculas
    Console.WriteLine($"Músicas do artista: {artista}");
    foreach (var musica in musicas)
    {
        Console.WriteLine($"\t - {musica.Titulo} ({musica.Artista}) - {musica.Duracao} segundos - Lançamento: {musica.DataLancamento.ToShortDateString()}");
    }

    /* métodos que utilizam StringComparison
    "Coldplay".Equals("coldplay", StringComparison.OrdinalIgnoreCase);
    "Coldplay".StartsWith("cold", StringComparison.OrdinalIgnoreCase);
    "Coldplay".EndsWith("coldplay", StringComparison.OrdinalIgnoreCase);
    "Coldplay".IndexOf("coldplay", StringComparison.OrdinalIgnoreCase);
    "Coldplay".Contains("OLD", StringComparison.OrdinalIgnoreCase);
    "Coldplay".Replace("cold", "warm", StringComparison.OrdinalIgnoreCase); */
}
void AlterandoOTitulo(StreamReader stream)
{
    var musica = ObterMusicas(stream)          // Obtém as músicas do arquivo CSV
    .Where(m => m.Titulo.StartsWith("T"))  // Filtra músicas cujo título começa com "The"
    .FirstOrDefault();                    // Retorna a primeira música encontrada ou null se nenhuma for encontrada

    if (musica is not null)
    {
        Console.WriteLine($"Titulo da Música: {musica.Titulo}");
        Console.WriteLine();
        musica.Titulo = musica.Titulo.Replace("The ", ""); // Exemplo de uso do método Replace para substituir parte do título da música
        Console.WriteLine($"Titulo da Música: {musica.Titulo}");
    }
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
void ExibirMusicas(IEnumerable<Musica> musicas)
{
    var titulo = "Músicas do arquivo:"; 

    Console.WriteLine(titulo); 
    foreach (var musica in musicas)
    {
        var linha = $"\t - {musica.Titulo} ({musica.Artista}) - {musica.Duracao}s [{musica.DataLancamento.ToShortDateString()}]";
        Console.WriteLine(linha);
    }
}
void ExibirMusicasEmTabela(IEnumerable<Musica> musicas)
{
    var titulo = "Músicas do arquivo:";
    Console.WriteLine(titulo);

    var colunaTitulo = "Título".PadRight(40);             // Define a largura da coluna para o título (40 caracteres)
    var colunaArtista = "Artista".PadRight(30);          // Define a largura da coluna para o artista (30 caracteres)
    var colunaDuracao = "Duração".PadRight(10);         // Define a largura da coluna para a duração (10 caracteres)
    var colunaLancamento = "Lançamento".PadRight(15);  // Define a largura da coluna para a data de lançamento (15 caracteres)
    Console.WriteLine($"{colunaTitulo}{colunaArtista}{colunaDuracao}{colunaLancamento}"); 

    var borda = "".PadRight(120, '-');
    Console.WriteLine(borda);

    foreach (var musica in musicas)
    {
        var duracao = string.Format("{0, -10:F3}", musica.Duracao/60.0); // Formata a duração em minutos com 3 casas decimais e alinha à esquerda em um campo de 10 caracteres
        var linha = $"{musica.Titulo, -40}{musica.Artista, -30}{duracao}{musica.DataLancamento, -15:dd/MM/yyyy}";
        Console.WriteLine(linha);
    }
}
IEnumerable<Musica> ObterMusicas(StreamReader stream)
{
    var linha = stream.ReadLine();                                 // Lê a primeira linha (cabeçalho)

    while (linha is not null)                                    
    {
        var partes = linha.Split(';');                         // Divide a linha em partes usando o ponto e vírgula como separador
        if (partes.Length == 5)
        {
            var musica = new Musica
            {
            Titulo = string.IsNullOrWhiteSpace(partes[0]) ? "Título Desconecido" : partes[0], // Se o título estiver vazio ou for apenas espaços, atribui "Título Desconecido"
                Artista = string.IsNullOrWhiteSpace(partes[1]) ? "Artista Desconhecido" : partes[1], // Se o artista estiver vazio ou for apenas espaços, atribui "Artista Desconhecido"
            Duracao = int.TryParse(partes[2], out int duracao) ? duracao : 350, //se falhar, atribui 350 segundos como valor padrão
            Generos = partes[3].Split(",", StringSplitOptions.TrimEntries),   
            DataLancamento = DateTime.TryParse(partes[4], out DateTime data) ? data : DateTime.Today
            };
            yield return musica;                          // Retorna a música atual e pausa a execução
        }
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

    public override string ToString()
    {
        return $"{this.Titulo} ({this.Artista}) - {this.Duracao}s [{this.DataLancamento}]";
    }
}