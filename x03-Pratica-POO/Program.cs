using x03_Pratica_POO;

using var stream = new FileStream("musicas.csv", FileMode.Open, FileAccess.Read);
using var reader = new StreamReader(stream);

var leitor = new LeitorArquivo();
var Exibidor = new ConsoleTools();

var musicas = leitor.ObterMusicas(reader);
var apenasColdplay = musicas.FiltrarPor(musicas => musicas.Artista.Equals("COLDPLAY", StringComparison.OrdinalIgnoreCase))
    .FiltrarPor(musicas => musicas.Duracao >= 400);
Exibidor.ExibirMusicas(apenasColdplay);

