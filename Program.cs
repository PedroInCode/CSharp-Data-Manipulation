using System.Collections;

var diasDaSemana = new string[] { "Domingo", "Segunda-feira", "Terça-feira", "Quarta-feira", "Quinta-feira", "Sexta-feira", "Sábado" };

//var carrinho = new ArrayList()
//-> Problema: ArrayList é uma coleção não genérica, o que significa que ela pode conter objetos de qualquer tipo. Isso pode levar a problemas de segurança de tipo e desempenho, pois é necessário fazer castings para acessar os elementos corretamente. 

var carrinho = new List<Produto>()
{
    new Produto { Nome = "Leite", Preco = 7.89 },
    new Produto { Nome = "Manteiga", Preco = 3.45 },
};

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