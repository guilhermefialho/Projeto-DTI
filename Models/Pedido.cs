using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;



namespace ProjetoDti.Models{
public class Pedido
{
    public int ID { get; set; }
    public DateTime Data { get; set; }
    public string Cliente { get; set; }
    public string Produto { get; set; }
    public int Quantidade { get; set; } 

public static List<Pedido> LerPedidosDoCSV(string filePath)
    {
        List<Pedido> pedidos = new List<Pedido>();

        try
        {
            using (var reader = new StreamReader(filePath))
            {
                // Lê a primeira linha (cabeçalho) para ignorá-la, se necessário
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    string linha = reader.ReadLine();
                    string[] colunas = linha.Split(',');

                    int id = int.Parse(colunas[0]);
                    DateTime data = DateTime.ParseExact(colunas[1], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    string produto = colunas[2];
                    string cliente = colunas[3];
                    int quantidade = int.Parse(colunas[4]);

                    Pedido pedido = new Pedido
                    {
                        ID = id,
                        Data = data,
                        Cliente = cliente,
                        Produto = produto,
                        Quantidade = quantidade
                    };

                    pedidos.Add(pedido);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocorreu um erro: {ex.Message}");
        }

        return pedidos;
    }

    public static string EncontrarProdutoMaisVendido(List<Pedido> pedidos)
    {
        var produtoQuantidade = pedidos
            .GroupBy(p => p.Produto)
            .Select(g => new { Produto = g.Key, Quantidade = g.Sum(p => p.Quantidade) })
            .OrderByDescending(x => x.Quantidade)
            .First();

        return produtoQuantidade.Produto;
    }

    public static Dictionary<int, int> CalcularQuantidadeTotalPorMes(List<Pedido> pedidos)
    {
        var quantidadePorMes = new Dictionary<int, int>();

        foreach (var pedido in pedidos)
        {
            int mes = pedido.Data.Month;
            int quantidade = pedido.Quantidade;
             if (quantidadePorMes.ContainsKey(mes))
            {
                quantidadePorMes[mes] += quantidade;
            }
            else
            {
                quantidadePorMes[mes] = quantidade;
            }
        }

        // Ordenar o dicionário por mês
        var quantidadePorMesOrdenado = quantidadePorMes
        .OrderBy(kvp => kvp.Key) // Ordenar por número de mês
        .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        return quantidadePorMesOrdenado;
    }

     public static Dictionary<int, string> EncontrarProdutoMaisVendidoPorMes(List<Pedido> pedidos)
    {
        var produtoMaisVendidoPorMes = new Dictionary<int, string>();

        var gruposPorMes = pedidos.GroupBy(p => p.Data.Month);

        foreach (var grupo in gruposPorMes.OrderBy(g => g.Key)) // Ordenar por número de mês
        {
            int mes = grupo.Key;

            var produtoQuantidade = grupo
                .GroupBy(p => p.Produto)
                .Select(g => new { Produto = g.Key, Quantidade = g.Sum(p => p.Quantidade) })
                .OrderByDescending(x => x.Quantidade)
                .First();

                      produtoMaisVendidoPorMes[mes] = produtoQuantidade.Produto;
        }

        return produtoMaisVendidoPorMes;
    }


}


}