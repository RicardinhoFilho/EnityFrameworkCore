using Microsoft.EntityFrameworkCore;
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

           
        }

        static void JoinUmParaUmMaisFiltro()
        {
            using (var contexto = new LojaContext())
            {
                var produto = contexto.Produtos.Include(p => p.Compras).Where(p => p.Id == 1).FirstOrDefault();
                contexto.Entry(produto).Collection(p => p.Compras).Query().Where(c => c.Quantidade > 5).Load();

                Console.WriteLine($"Mostrando as compras do produto {produto.Nome}");
            }
        }
        static void JoinsUmParUm()
        {
            using (var contexto = new LojaContext())
            {
                var cliente = contexto.Clientes.Include(c => c.EnderecoEntrega).FirstOrDefault();//Pegando primeiro cliente

                Console.WriteLine($"Endereço de entrega: {cliente.EnderecoEntrega.Logradouro}");

                var produto = contexto.Produtos.Include(p => p.Compras).Where(p => p.Id == 1).FirstOrDefault();

                Console.WriteLine($"Mostrando as compras do produto {produto.Nome}");
                foreach (var item in produto.Compras)
                {
                    Console.WriteLine(item.Quantidade);
                }
            }
        }

        static void JoinsUmParaVarios()
        {
            using (var contexto2 = new LojaContext())
            {
                var promocao = contexto2
                    .Promocoes
                    .Include(p => p.Produtos)
                    .ThenInclude(pp => pp.Produto)
                    .Last();

                Console.WriteLine("\nMotrando os produtos da promoção...");
                foreach (var item in promocao.Produtos)
                {
                    Console.WriteLine(item.Produto.Nome);
                }
            }
        }

        static void IncluiBebidasEPromocao()
        {
            using (var contexto = new LojaContext())
            {
                var produto1 = new Produto()
                {
                    Categoria = "Bebida",
                    Nome = "Refrigerante",
                    PrecoUnitario = 4.5,
                    Unidade = 1
                };
                var produto2 = new Produto()
                {
                    Categoria = "Enlatado",
                    Nome = "Atum",
                    PrecoUnitario = 6,
                    Unidade = 1
                };
                var produto3 = new Produto()
                {
                    Categoria = "Bebida",
                    Nome = "Suco",
                    PrecoUnitario = 4.3,
                    Unidade = 1
                };
                var produto4 = new Produto()
                {
                    Categoria = "Bebida",
                    Nome = "Vodka",
                    PrecoUnitario = 13,
                    Unidade = 1
                };

                contexto.Produtos.Add(produto1);
                contexto.Produtos.Add(produto2);
                contexto.Produtos.Add(produto3);
                contexto.Produtos.Add(produto4);

                contexto.SaveChanges();

                var promocao = new Promocao();
                promocao.Descricao = "Qeima Total Janeiro 2017";
                promocao.DataInicio = new DateTime(2021, 1, 1);
                promocao.DataTermino = new DateTime(2021, 1, 31);

                var produtos = contexto.Produtos.Where(p => p.Categoria == "Bebida").ToList();

                foreach (var produto in produtos)
                {
                    promocao.IncluiProduto(produto);
                }

                contexto.Promocoes.Add(promocao);

                contexto.SaveChanges();
            }

        }


        static void UmParaUm()
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





