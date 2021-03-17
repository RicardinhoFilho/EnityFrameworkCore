using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    //Tabela de JOIN
    internal class PromocaoProduto
    {
        public Produto Produto { get; set; }
        public int ProdutoId { get; set; }

        public Promocao Promocao { get; set; }
        public int PromocaoId { get; set; }

    }
}
