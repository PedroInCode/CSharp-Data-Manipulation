using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColocandoNaPratica;

internal class Musica
{
    public string Titulo { get; set; } = string.Empty;
    public string Artista { get; set; }
    public int Duracao { get; set; } // Duração em segundos

    public Musica(string titulo, string artista, int duracao)
    {
        Titulo = titulo;
        Artista = artista;
        Duracao = duracao;
    }

    public override bool Equals(object? obj) // Sobrescreve o método Equals para comparar músicas com base no título e artista
    {
        if (obj is null) return false; // Verifica se o objeto é nulo
        if (obj is Musica outraMusica) // Verifica se o objeto é do tipo Musica
            return this.Titulo.Equals(outraMusica.Titulo) && this.Artista.Equals(outraMusica.Artista); // Compara o título e o artista para determinar se as músicas são iguais
        return false; // Retorna false se o objeto não for do tipo Musica

    }

    public override int GetHashCode() // Sobrescreve o método GetHashCode para gerar um código hash baseado no título e artista
    {
        return this.Titulo.GetHashCode() ^ this.Artista.GetHashCode(); // Combina os códigos hash do título e do artista usando o operador XOR para gerar um código hash único para a música
    }
}
