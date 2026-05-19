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

var player = new PlayerDeMusica();
player.AdicionarNaFila(musica3);
player.AdicionarNaFila(playlist1);

ExibirFila(player);
ExibirHistorico(player);

var proximaMusica = player.ProximaMusicaDaFila();
if (proximaMusica is not null)
    Console.WriteLine($"\nTocando: {proximaMusica.Titulo} - {proximaMusica.Artista}");
else
    Console.WriteLine("\nNão há mais músicas na fila de reprodução.");

ExibirFila(player);
ExibirHistorico(player);

var musicaAnterior = player.MusicaAnterior();
if (musicaAnterior is not null)
    Console.WriteLine($"\nMúsica anterior: {musicaAnterior.Titulo} - {musicaAnterior.Artista}");
else
    Console.WriteLine("\nNão há música anterior no histórico de reprodução.");

ExibirFila(player);
ExibirHistorico(player);










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
void ExibirFila(PlayerDeMusica player)
{
    Console.WriteLine("\nFila de reprodução:");
    foreach (var musica in player.Fila())
    {
        Console.WriteLine($"\t - {musica.Titulo} - {musica.Artista}");
    }
}
void ExibirHistorico(PlayerDeMusica player)
{
    Console.WriteLine("\nHistórico de reprodução:");
    foreach (var musica in player.Historico())
    {
        Console.WriteLine($"\t - {musica.Titulo} - {musica.Artista}");
    }
}