using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace Alura.Loja.Testes.ConsoleApp
{
    internal class LojaContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Compra> Compras { get; set; }

        public DbSet<Promocao> Promocoes { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PromocaoProduto>().HasKey(pp => new { pp.ProdutoId, pp.PromocaoId });
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Endereco>().ToTable("Enderecos"); //Aleterando Nome da Tabela

            //Criamos para a tabela endereco uma propriedade oculta para nosso programa que é somente visivel no banco de dados
            modelBuilder.Entity<Endereco>().Property<int>("ClienteId");
            //Definimos que está proprieade será a chave de nossa tabela Endereco
            modelBuilder.Entity<Endereco>().HasKey("ClienteId");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=LojaDB;Trusted_Connection=true;");
        }
    }
}