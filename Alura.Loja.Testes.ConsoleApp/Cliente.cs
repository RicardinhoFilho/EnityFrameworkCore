﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    internal class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Endereco EnderecoEntrega { get; set; }
    }
}
