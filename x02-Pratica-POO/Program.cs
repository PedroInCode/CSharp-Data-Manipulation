using ColocandoNaPratica;

Musica musica1 = new Musica("Inprevisível", "Tribo da Periferia", 250);
Musica musica2 = new Musica("Nosso Plano", "Tribo da Periferia", 220);
Musica musica3 = new Musica("Alma de Pipa", "Tribo da Periferia", 200);
Musica musica4 = new Musica("Por Mim", "Tribo da Periferia", 200);

Playlist playlist1 = new Playlist("Tribo da Periferia");
playlist1.Add(musica1);
playlist1.Add(musica2);
playlist1.Add(musica3);
playlist1.Add(musica4);

var playlist2 = new Playlist("Tribo da Periferia");
playlist2.Add(musica2);

ExibirMaisTocadas(playlist1, playlist2);

// Metodo do Program.cs para exibir as músicas mais tocadas entre duas playlists

void ExibirMaisTocadas(Playlist playlist1, Playlist playlist2)
{
    Dictionary<Musica, int> ranking = new Dictionary<Musica, int>(); // Dicionário para armazenar a contagem de vezes que cada música foi tocada
    foreach (var musica in playlist1)
    {
        ranking.Add(musica, 1); // Inicializa a contagem de cada música da primeira playlist com 1
    }

    foreach (var musica in playlist2)
    {
        if (ranking.TryGetValue(musica, out int contagem))
        {
            contagem++; // Incrementa a contagem se a música já estiver no ranking
            ranking[musica] = contagem; // Atualiza a contagem no dicionário
        }
        else
        {
            ranking[musica] = 1; // Adiciona a música ao ranking com contagem 1 se não estiver presente
        }
    }

    List<KeyValuePair<Musica, int>> top = new(ranking);// Converte o dicionário em uma lista de pares chave-valor para facilitar a ordenação
    top.Sort(new PorContagem()); // Ordena a lista de pares chave-valor com base na contagem de vezes que cada música foi tocada

    Console.WriteLine($"\nTop 5 Músicas mais incluidas nas playlists");
    int contador = 1;
    foreach (var par in top)
    {
        Console.WriteLine($"\t - {par.Key.Titulo}");
        contador++;
        if (contador > 5) break; // Exibe apenas as 5 músicas mais tocadas
    }
}