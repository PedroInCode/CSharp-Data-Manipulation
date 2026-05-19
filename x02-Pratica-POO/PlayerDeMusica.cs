using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColocandoNaPratica;

internal class PlayerDeMusica
{
    private Queue<Musica> filaDeReproducao = [];
    private Stack<Musica> pilhaDeReproducao = [];

    public void AdicionarNaFila(Musica musica)
    {
        filaDeReproducao.Enqueue(musica); // Adiciona a música à fila de reprodução
    }

    public void AdicionarNaFila(Playlist playlist)
    {
        foreach (var musica in playlist)
        {
            AdicionarNaFila(musica); // Adiciona cada música da playlist à fila de reprodução
        }
    }

    public IEnumerable<Musica> Fila()
    {
        foreach (var musica in filaDeReproducao)
        {
            yield return musica; // Retorna cada música da fila de reprodução
        }
    }

    public Musica? MusicaAnterior()
    {
        if (pilhaDeReproducao.Count == 0) return null; // Se a pilha de reprodução estiver vazia, retorna null
        return pilhaDeReproducao.Pop(); // Retorna a música anterior da pilha de reprodução
    }

    public Musica? ProximaMusicaDaFila()
    {
        if (filaDeReproducao.Count == 0) return null;
        var proximaMusica = filaDeReproducao.Dequeue();
        pilhaDeReproducao.Push(proximaMusica); // Adiciona a música atual à pilha de reprodução
        return proximaMusica; // Retorna a próxima música da fila de reprodução
    }

    public IEnumerable<Musica> Historico()
    {
        foreach (var musica in pilhaDeReproducao)
        {
            yield return musica; // Retorna cada música do histórico de reprodução
        }
    }
}
