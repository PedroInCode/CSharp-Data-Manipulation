using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace x04_Pratica_POO;

internal class LeitorDeArquivo
{
    IEnumerable<Musica> ObterMusicas(StreamReader stream)
    {
        var linha = stream.ReadLine();                               // Lê a primeira linha (cabeçalho)
        while (linha is not null)                                   // Continua lendo até o final do arquivo
        {
            var partes = linha.Split(';');                        // Divide a linha em partes usando o ponto e vírgula como separador
            var musica = new Musica
            {
                Titulo = partes[0],
                Artista = partes[1],
                Duracao = Convert.ToInt32(partes[2]),
                Generos = partes[3].Split(",").Select(g => g.Trim())   // Divide os gêneros em partes usando a vírgula como separador e remove os espaços em branco usando Trim
            };
            yield return musica;                          // Retorna a música atual e pausa a execução
            linha = stream.ReadLine();                   // Lê a próxima linha
        }
    }
}
