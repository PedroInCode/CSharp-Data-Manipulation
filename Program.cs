using System.Collections;

DiasDaSemana diasDaSemana = new DiasDaSemana(); 

var carrinho = new List<Produto>()
{
    new Produto { Nome = "Leite", Preco = 7.89 },
    new Produto { Nome = "Manteiga", Preco = 3.45 },
};

void percorrendoComEnumerator()
{
    var enumerator = diasDaSemana.GetEnumerator();
    while (enumerator.MoveNext())
    {
        var dia = enumerator.Current;
        Console.WriteLine(dia);
    }
}

void PercorrendoDiasDaSemana()
{
    foreach (var dia in diasDaSemana) // Precisa implementar o IEnumerable<string> para funcionar
    {
        Console.WriteLine(dia);
    }
}
void PercorrendoComFor()
{
    for (int i = 0; i < carrinho.Count; i++)
    {
        var produto = carrinho[i];
        Console.WriteLine($"Produto: {produto.Nome}");
    }
}

void PercorrendoComForEach()
{
    foreach (var produto in carrinho) //Melhor opção!
    {
        Console.WriteLine($"Produto: {produto.Nome}");
    }
}

class Produto
{
    public string Nome { get; set; }
    public double Preco { get; set; }
}

class DiasDaSemanaEnumerator : IEnumerator<string>
{
    private int position = -1;
    private string[] dias = { "Domingo", "Segunda", "Terça", "Quarta", "Quinta", "Sexta", "Sábado" };
    public string Current => dias[position];

    object IEnumerator.Current => Current;

    public void Dispose()
    {
    }

    public bool MoveNext()
    {
        position++;

        return position < dias.Length;
    }

    public void Reset()
    {
        position = -1;
    }
}

class DiasDaSemana : IEnumerable<string>
{
    
    public IEnumerator<string> GetEnumerator()
    {
        return new DiasDaSemanaEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

