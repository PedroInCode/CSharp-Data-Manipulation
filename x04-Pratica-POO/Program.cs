using x04_Pratica_POO;

// 1. Abre o fluxo do arquivo (Stream)
using (StreamReader stream = new StreamReader("musicas.csv"))
{
    var consultador = new ConsultadorDeMusicas(new LeitorDeArquivo().ObterMusicas(stream));
    consultador.OperacoesDeVerificacaoDeExistencia(stream);
}