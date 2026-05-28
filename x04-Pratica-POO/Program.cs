using x04_Pratica_POO;

// 1. Abre o fluxo do arquivo (Stream)
using (StreamReader stream = new StreamReader("musicas.csv"))
{
    var leitor = new LeitorDeArquivo();
    IEnumerable<Musica> musicas = leitor.ObterMusicas(stream); // 2. Lê o arquivo e obtém as músicas

    var consultador = new ConsultadorDeMusicas(musicas); // 3. Cria o consultador de músicas

    Console.WriteLine("=== INICIANDO CONSULTAS POO ===");

    // 4. Chamada do seu novo método (sem passar nada nos parênteses!)
    consultador.OperacoesDeVerificacaoDeExistencia();

    Console.WriteLine("\n=== FIM DAS CONSULTAS ===");
}