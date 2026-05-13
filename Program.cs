using System.Collections;

DiasDaSemana diasDaSemana = new DiasDaSemana(); 

var carrinho = new List<Produto>()
{
    new Produto { Nome = "Leite", Preco = 7.89 },
    new Produto { Nome = "Manteiga", Preco = 3.45 },
};

var Pares = NumerosParesComYield();
var contador = 0;

foreach (var par in Pares)
{
    contador++; 
    Console.WriteLine(par);
    if (contador > 200) break;
    Console.WriteLine($"Contador: {contador}");
}

IEnumerable<int> NumerosParesSemYield(int limite) // Esse método precisa processar toda a coleção antes de retornar os resultados, o que pode ser ineficiente em termos de memória e tempo de processamento, especialmente para limites grandes.
{
    var Lista = new List<int>();
    for (int i=0; i <= limite; i++)
    {
        Console.WriteLine($"Processando elemento {i}"); // Simulando um processamento pesado)
        Lista.Add(i * 2);
    }

    return Lista;
}

IEnumerable<int> NumerosParesComYield() // O uso do yield permite que os números pares sejam gerados um a um, à medida que são solicitados, o que pode ser mais eficiente em termos de memória e tempo de processamento, especialmente para limites grandes.
{
    int i = 0;
    while (true)
    {
        Console.Write($"Processando elemento {i} -> "); 
        yield return i * 2;
        if (i >= 100) yield break; // Para evitar um loop infinito, limitamos a geração de números pares a 100.
        i++;
    }
}

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

