using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

using var arquivo = new FileStream("musicas.csv", FileMode.Open, FileAccess.Read);
using var stream = new StreamReader(arquivo);

//var musicas = ObterMusicas(stream)
//    .Take(30);
//ExibirMusicasEmTabela(musicas);

artistaComCaracteresEspeciais();

    
void artistaComCaracteresEspeciais()
{
    var regex = new Regex(@"[^a-zA-Z0-9 ]");

    var artista = ObterMusicas(stream)
        .Where(m => regex.IsMatch(m.Artista))
        .Select(m => m.Artista)
        .Distinct()
        .OrderBy(a => a);

    foreach (var artist in artista)
    {
        Console.WriteLine(artist);
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
        var duracao = string.Format("{0, -10:F3}", musica.Duracao / 60.0); // Formata a duração em minutos com 3 casas decimais e alinha à esquerda em um campo de 10 caracteres
        var linha = $"{musica.Titulo,-40}{musica.Artista,-30}{duracao}{musica.DataLancamento,-15:dd/MM/yyyy}";
        Console.WriteLine(linha);
    }
}
IEnumerable<Musica> ObterMusicas(StreamReader stream)
{
    var linha = stream.ReadLine();                                 // Lê a primeira linha (cabeçalho)

    while (linha is not null)
    {
        var partes = linha.Split(';');                         

        int duracao = 350;
        var match = Regex.Match(linha, @"(\d?\d):(\d\d)");
        if (match.Success)
        {
            var minutos = int.Parse(match.Groups[1].Value);
            var segundos = int.Parse(match.Groups[2].Value);
            duracao = (minutos * 60) + segundos;
        }

        if (partes.Length == 5)
        {
            var musica = new Musica
            {
                Titulo = string.IsNullOrWhiteSpace(partes[0]) ? "Título Desconecido" : partes[0], 
                Artista = string.IsNullOrWhiteSpace(partes[1]) ? "Artista Desconhecido" : partes[1], 
                Duracao = duracao,                                 
                Generos = partes[3].Split(",", StringSplitOptions.TrimEntries),
                DataLancamento = DateTime.TryParse(partes[4], out DateTime data) ? data : DateTime.Today
            };
            yield return musica;                          
        }
        linha = stream.ReadLine();                 
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