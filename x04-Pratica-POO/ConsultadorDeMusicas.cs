using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace x04_Pratica_POO;

class ConsultadorDeMusicas
{
    private readonly IEnumerable<Musica> _musicas;

    // Construtor recebe as músicas que o seu LeitorDeArquivo carregou
    public ConsultadorDeMusicas(IEnumerable<Musica> musicas) => _musicas = musicas;

    public void OperacaoDeObtencaoDeElementos()
    {
        var lista = _musicas.ToList(); // Materializa uma vez aqui dentro se necessário

        var primeiraMusica = lista.First();
        Console.WriteLine($"A primeira música da coleção é '{primeiraMusica.Titulo}' do artista {primeiraMusica.Artista}.");

        var maiorDuracao = lista.MaxBy(m => m.Duracao);
        Console.WriteLine($"A música com maior duração é {maiorDuracao.Titulo} -> {maiorDuracao.Duracao} segundos.");
    }

    public void OperacoesDeAgrupamento()
    {
        var artistas = _musicas.GroupBy(m => m.Artista);

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

    public void EstatisticasDeMusicas()
    {
        var lista = _musicas.ToList();
        Console.WriteLine($"\nExistem {lista.Count()} músicas na coleção.");
        Console.WriteLine($"\nExistem {lista.Count(m => m.Duracao > 600)} músicas com mais de 10 minutos.");
        Console.WriteLine($"\nA música com menor duração leva {lista.Min(m => m.Duracao)} segundos.");
        Console.WriteLine($"\nA música com maior duração leva {lista.Max(m => m.Duracao)} segundos.");
        Console.WriteLine($"\nA duração média é {lista.Average(m => m.Duracao):F2} segundos.");
    }

    public void OperacoesDeProjecao2()
    {
        var generos = _musicas
            .SelectMany(musica => musica.Generos)
            .Distinct()
            .OrderBy(genero => genero);

        foreach (var genero in generos)
        {
            Console.WriteLine(genero);
        }
    }

    public void OperacoesDeProjecao()
    {
        var artistas = _musicas
            .Select(musica => musica.Artista)
            .Distinct()
            .OrderBy(artista => artista);

        foreach (var artista in artistas)
        {
            Console.WriteLine(artista);
        }
    }

    public void OperacoesDeFiltroEOrdenacao()
    {
        var musicasDoColdplay = _musicas
            .Where(musica => musica.Artista == "Coldplay")
            .OrderBy(musica => musica.Titulo)
            .Skip(10)
            .Take(5);

        ExibirMusicas(musicasDoColdplay);
    }

    private void ExibirMusicas(IEnumerable<Musica> musicas)
    {
        var contador = 1;
        Console.WriteLine("\nExibindo as Músicas:");
        foreach (var musica in musicas)
        {
            Console.WriteLine($"\t - {musica.Titulo} ({musica.Artista}) - {musica.Duracao} segundos");
            contador++;
            if (contador > 10) break;
        }
    }

    public void artistaComMaiorQuantidade()
    {
        var artistaComMaiorQuantidadeDeMusicas = _musicas
            .GroupBy(m => m.Artista)
            .Select(g => new { Artista = g.Key, Musicas = g, Total = g.Count()})
            .MaxBy(a => a.Total);

        if (artistaComMaiorQuantidadeDeMusicas != null)
        {
            Console.WriteLine($"O artista com maior quantidade de músicas é {artistaComMaiorQuantidadeDeMusicas.Artista} com {artistaComMaiorQuantidadeDeMusicas.Total} músicas.");
        }
    }

    public void OperacoesDeVerificacaoDeExistencia()
    {
        var musicas = _musicas.ToList(); // Converte o IEnumerable<Musica> para List<Musica> para evitar múltiplas iterações sobre o arquivo
        var artistas = musicas
            .GroupBy(m => m.Artista)                   // Agrupa as músicas por artista usando GroupBy do LINQ
            .Where(g => g.Any(m => m.Duracao > 470)); // Filtra os grupos de artistas que possuem pelo menos uma música com duração maior do que 470 segundos usando Where e Any do LINQ

        Console.WriteLine("Artistas que possuem pelo menos uma música com duração maior do que 470 segundos:");
        foreach (var artista in artistas)
        {
            Console.WriteLine($"\t - {artista.Key}");
        }

        var reggae = musicas
            .GroupBy(m => m.Artista)
            .Where(g => g.Any(m => m.Generos.Contains("Reggae")));

        Console.WriteLine("\nArtistas que possuem pelo menos uma música do gênero Reggae:");
        foreach (var artista in reggae)
        {
            Console.WriteLine($"\t - {artista.Key}");
        }
    }
}
