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

playlist1.ExibirPlaylist();

Console.WriteLine($"-------------------------------------------------------------------------------");

var musicaEncontrada = playlist1.ObterPeloTitulo("Alma de Pipa");
if(musicaEncontrada is not null)
{
    Console.WriteLine($"Removendo a música {musicaEncontrada.Titulo} da playlist");
    playlist1.Remove(musicaEncontrada);
}
else
{
    Console.WriteLine("Música não encontrada na playlist..");
}

Console.WriteLine($"Playlist atualizada:\n");
playlist1.ExibirPlaylist();