using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using ProjetoDti.Models;


class Program
{
    static void Main()
    {
        string filePath = @"C:\Users\guilh\Documents\Projetos GitHub\Projeto DTI\vendas.csv";

        List<Pedido> pedidos = Pedido.LerPedidosDoCSV(filePath);

        // Produto mais vendido (quantidade)
        string produtoMaisVendido = Pedido.EncontrarProdutoMaisVendido(pedidos);
        Console.WriteLine($"Produto Mais Vendido (Quantidade): {produtoMaisVendido}");
        Console.WriteLine("");

        // Quantidade total vendida por mês
        Dictionary<int, int> quantidadePorMes = Pedido.CalcularQuantidadeTotalPorMes(pedidos);
        Console.WriteLine("Quantidade Total Vendida por Mês:");
        foreach (var kvp in quantidadePorMes)
        {
            Console.WriteLine($"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(kvp.Key)}: {kvp.Value}");
        }

         // Produto mais vendido por mês
        Dictionary<int, string> produtoMaisVendidoPorMes = Pedido.EncontrarProdutoMaisVendidoPorMes(pedidos);
        Console.WriteLine("");
        Console.WriteLine("Produto Mais Vendido por Mês:");
        foreach (var kvp in produtoMaisVendidoPorMes)
        {
            Console.WriteLine($"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(kvp.Key)}: {kvp.Value}");
        }

    }

}



