using System.Collections;

DiasDaSemana diasDaSemana = new DiasDaSemana(); 

var carrinho = new List<Produto>()
{
    new Produto { Nome = "Leite", Preco = 7.89 },
    new Produto { Nome = "Manteiga", Preco = 3.45 },
};

percorrendoComEnumerator();

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

class DiasDaSemana : IEnumerable<string>
{
    
    public IEnumerator<string> GetEnumerator()
    {
        yield return "Domingo";
        yield return "Segunda";
        yield return "Terça";
        yield return "Quarta";
        yield return "Quinta";
        yield return "Sexta";
        yield return "Sábado";
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

