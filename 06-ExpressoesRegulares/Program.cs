using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

using var arquivo = new FileStream("musicas.csv", FileMode.Open, FileAccess.Read);
using var stream = new StreamReader(arquivo);

//var musicas = ObterMusicas(stream)
//    .Take(30);
//ExibirMusicasEmTabela(musicas);

/// <summary>
/// Filtra e exibe músicas que possuem títulos com três ou mais 
/// letras idênticas coladas/consecutivas (ex: "Gooool" ou "Uhuuu").
/// </summary>
void TitulosComLetrasConsecutivas()
{
    // Regex de Caracteres Consecutivos:
    // \w* -> Aceita letras/números opcionais antes do padrão
    // (\w)  -> Grupo 1: Captura a letra base que será testada
    // \1    -> Faz referência à letra do Grupo 1...
    // {2,}  -> ...e exige que ela se repita 2 ou MAIS vezes seguidas (Totalizando 3+ letras iguais)
    // \w    -> Garante mais um caractere alfanumérico na sequência
    var regex = new Regex(@"\w*(\w)\1{2,}\w");

    // Processamento dos dados com LINQ expressões
    var titulosFiltrados = ObterMusicas(stream)
        .Where(m => regex.IsMatch(m.Titulo)) // Filtra os títulos com 3+ letras coladas
        .Take(20);                           // Limita a 20 registros por performance

    // Renderiza o resultado na tela usando a estrutura de tabela do projeto
    Console.WriteLine("--- Títulos com 3 ou mais letras consecutivas encontrados ---");
    ExibirMusicasEmTabela(titulosFiltrados);
}

/// <summary>
/// Filtra e exibe as primeiras 20 músicas cujo título 
/// começa e termina exatamente com a mesma palavra.
/// </summary>
void TitulosComEco()
{
    // Regex com Backreference (\1):
    // ^      -> Início da string
    // (\w+)  -> Grupo 1: Captura a primeira palavra
    // .* -> Aceita qualquer texto (ou nenhum) no meio do título
    // \1     -> Exige EXATAMENTE o mesmo conteúdo capturado no Grupo 1
    // $      -> Fim da string
    var regex = new Regex(@"^(\w+).*\1$");

    // Processamento dos dados com LINQ expressões
    var titulosFiltrados = ObterMusicas(stream)
        .Where(m => regex.IsMatch(m.Titulo)) // Filtra títulos com "eco" (Regex = true)
        .Take(20);                           // Limpa a performance pegando apenas os primeiros 20

    // Renderiza o resultado na tela usando a estrutura de tabela do projeto
    Console.WriteLine("--- Títulos que começam e terminam com a mesma palavra ---");
    ExibirMusicasEmTabela(titulosFiltrados);
}

/// <summary>
/// Filtra, isola e exibe em ordem alfabética todos os títulos de músicas
/// que possuem EXATAMENTE duas palavras no nome.
/// </summary>
void TitulosComDuasPalavras()
{
    // Regex de correspondência exata para duas palavras:
    // ^   -> Garante que o padrão começa no início do título
    // \w+ -> Primeira palavra (um ou mais caracteres alfanuméricos)
    //     -> Um espaço em branco obrigatório entre elas
    // \w+ -> Segunda palavra (um ou mais caracteres alfanuméricos)
    // $   -> Garante que o título termina exatamente ali (bloqueia 3 ou mais palavras)
    var regex = new Regex(@"^\w+ \w+$");

    // Processamento dos dados com LINQ expressions
    var titulosFiltrados = ObterMusicas(stream)
        .Where(m => regex.IsMatch(m.Titulo)) // 1. Mantém apenas títulos com exatamente duas palavras (Regex = true)
        .Select(m => m.Titulo)               // 2. Extrai apenas o título da música (converte de Musica para string)
        .Distinct()                          // 3. Remove os títulos duplicados
        .OrderBy(t => t);                    // 4. Ordena o resultado de A a Z

    // Exibe os resultados tratados no console
    Console.WriteLine("--- Títulos com exatamente duas palavras encontrados ---");
    foreach (var titulo in titulosFiltrados)
    {
        Console.WriteLine($"- {titulo}");
    }
}

/// <summary>
/// Filtra, isola e exibe em ordem alfabética todos os artistas 
/// que possuem caracteres especiais ou acentuação no nome.
/// </summary>
void ArtistaComCaracteresEspeciais()
{
    // Regex de Negação: O '[^ ]' indica que a Regex dará MATCH em tudo o que NÃO for:
    // a-z (letras minúsculas), A-Z (letras maiúsculas), 0-9 (números) ou espaço em branco.
    // Qualquer acento (á, ç, é) ou símbolo (@, !) ativará esta Regex.
    var regex = new Regex(@"[^a-zA-Z0-9 ]");

    // Processamento dos dados usando LINQ expressions
    var artistasFiltrados = ObterMusicas(stream)
        .Where(m => regex.IsMatch(m.Artista)) // 1. Mantém apenas músicas com artistas "inválidos" (Regex = true)
        .Select(m => m.Artista)               // 2. Extrai apenas o nome do artista (converte de Musica para string)
        .Distinct()                           // 3. Remove os nomes duplicados da lista
        .OrderBy(a => a);                     // 4. Ordena o resultado final de A a Z

    // Exibe os resultados tratados no console
    Console.WriteLine("--- Artistas com caracteres especiais encontrados ---");
    foreach (var artista in artistasFiltrados)
    {
        Console.WriteLine($"- {artista}");
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