using System.Collections.Generic;

namespace Alura.Loja.Testes.ConsoleApp
{
    internal class Produto
    {
        public int Id { get; internal set; }
        public string Nome { get; internal set; }
        public string Categoria { get; internal set; }
        public double PrecoUnitario { get; internal set; }
        public int Unidade { get; set; }

        public IList<PromocaoProduto> Promocoes { get; set; }
    }
} 