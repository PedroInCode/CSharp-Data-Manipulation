using x04_Pratica_POO;

// 1. Abre o fluxo do arquivo (Stream)
using (StreamReader stream = new StreamReader("musicas.csv"))
{
    // 2. Instancia o leitor e obtém a coleção de músicas do arquivo
    var leitor = new LeitorDeArquivo();
    IEnumerable<Musica> musicas = leitor.ObterMusicas(stream);

    // 3. Instancia o consultador INJETANDO as músicas que acabamos de ler
    var consultador = new ConsultadorDeMusicas(musicas);

    // 4. Escolha quais relatórios você quer rodar no console:
    Console.WriteLine("=== INICIANDO CONSULTAS POO ===");

    consultador.EstatisticasDeMusicas();
    consultador.OperacaoDeObtencaoDeElementos();
    consultador.OperacoesDeAgrupamento();
    consultador.OperacoesDeProjecao();
    consultador.OperacoesDeFiltroEOrdenacao();

    Console.WriteLine("\n=== FIM DAS CONSULTAS ===");
}