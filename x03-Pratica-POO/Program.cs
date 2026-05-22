using x03_Pratica_POO;

using var stream = new FileStream("musicas.csv", FileMode.Open, FileAccess.Read);
using var reader = new StreamReader(stream);

var leitor = new LeitorArquivo();
var Exibidor = new ConsoleTools();

var musicas = leitor.ObterMusicas(reader);
Exibidor.ExibirMusicas(musicas);

