using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    class Program
    {


        static void Main(string[] args)
        {
            var fulano = new Cliente();
            fulano.Nome = "Fulano";
            fulano.EnderecoEntrega = new Endereco()
            {
                Numero = 12,
                Logradouro = "Rua dos Inválidos",
                Complemento = "sobrado",
                Bairro = "Centro",
                Cidade = "São Paulo"
            };

            using (var contexto = new LojaContext())
            {
                contexto.Clientes.Add(fulano);
                contexto.SaveChanges();
            }

            Console.ReadLine();
        }





        static void MuitosParaMuitos()
        {
            var p1 = new Produto();
            var p2 = new Produto();
            var p3 = new Produto();

            var promocaoDePascoa = new Promocao();
            promocaoDePascoa.Descricao = "Páscoa Feliz";
            promocaoDePascoa.DataInicio = DateTime.Now;
            promocaoDePascoa.DataTermino = DateTime.Now.AddMonths(3);

            promocaoDePascoa.IncluiProduto(p1);
            promocaoDePascoa.IncluiProduto(p2);
            promocaoDePascoa.IncluiProduto(p3);

            using (var contexto = new LojaContext())
            {
                contexto.Promocoes.Add(promocaoDePascoa);
                contexto.SaveChanges();
            }
        }

        static void UmParaMuitos()
        {
            var paoFrances = new Produto();
            paoFrances.Nome = "Pão Francês";
            paoFrances.PrecoUnitario = 0.4;
            paoFrances.Unidade = 1;
            paoFrances.Categoria = "Padaria";

            var compra = new Compra();
            compra.Quantidade = 6;
            compra.Produto = paoFrances;
            compra.Preco = compra.Produto.PrecoUnitario * compra.Quantidade;

            using (var contexto = new LojaContext())
            {
                contexto.Add(compra);
                contexto.SaveChanges();
            }
        }
    }



    //static void TesteCChangeTraker()
    //{
    //    using (var contexto = new LojaContext())
    //    {
    //        var produtos = contexto.Produtos.ToList();
    //        foreach (var produto in produtos)
    //        {
    //            Console.WriteLine(produto.Nome);
    //        }

    //        Console.WriteLine("============================");

    //        foreach (var e in contexto.ChangeTracker.Entries())
    //        {
    //            Console.WriteLine(e.State);//verifica o estado de cada dado do meu banco de dados
    //        }
    //        Console.WriteLine("============================");
    //        var p = produtos.Last();
    //        p.Nome = "Mudei o título do ultimo livro";

    //        foreach (var e in contexto.ChangeTracker.Entries())
    //        {
    //            Console.WriteLine(e.State);//verifica o estado de cada dado do meu banco de dados
    //        }
    //        Console.WriteLine("============================");
    //        var p1 = produtos.First();
    //        p1.Nome = "Mudei o título do primeiro livro";
    //        foreach (var e in contexto.ChangeTracker.Entries())
    //        {
    //            Console.WriteLine(e.State);//verifica o estado de cada dado do meu banco de dados
    //        }
    //        Console.WriteLine("============================");
    //        foreach (var produto in produtos)
    //        {
    //            Console.WriteLine(produto.Nome);
    //        }
    //        /*Added
    //            O objeto é novo, foi adicionado ao contexto, e o método SaveChanges ainda não foi executado. Depois que as mudanças são salvas, o estado do objeto muda para Unchanged. Objetos no estado Added não têm seus valores rastreados em sua instância de EntityEntry.

    //            Deleted
    //            O objeto foi excluído do contexto. Depois que as mudanças foram salvas, seu estado muda para Detached.

    //            Detached
    //            O objeto existe, mas não está sendo monitorado. Uma entidade fica nesse estado imediatamente após ter sido criada e antes de ser adicionada ao contexto. Ela também fica nesse estado depois que foi removida do contexto através do método Detach ou se é carregada por um método com opção NoTracking. Não existem instâncias de EntityEntry associadas a objetos com esse estado.

    //            Modified
    //            Uma das propriedades escalares do objeto foi modificada e o método SaveChanges ainda não foi executado. Quando o monitoramento automático de mudanças está desligado, o estado é alterado para Modified apenas quando o método DetectChanges é chamado. Quando as mudanças são salvas, o estado do objeto muda para Unchanged.

    //            Unchanged
    //            O objeto não foi modificado desde que foi anexado ao contexto ou desde a última vez que o método SaveChanges foi chamado.*/
    //    }
    //}
}





