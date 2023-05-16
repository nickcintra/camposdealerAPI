using CamposDealer_React.Models;
using Microsoft.EntityFrameworkCore;

namespace CamposDealer_React.Data
{
    public class APIContext : DbContext
    {
        public APIContext(DbContextOptions<APIContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração da relação entre Venda e Cliente (muitos para um)
            modelBuilder.Entity<Venda>()
                .HasOne(v => v.Cliente)
                .WithMany(c => c.Vendas)
                .HasForeignKey(v => v.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuração da relação entre VendaProduto e Venda (muitos para um)
            modelBuilder.Entity<VendaProduto>()
                .HasOne(vp => vp.Venda)
                .WithMany(v => v.VendaProdutos)
                .HasForeignKey(vp => vp.VendaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuração da relação entre VendaProduto e Produto (muitos para um)
            modelBuilder.Entity<VendaProduto>()
                .HasOne(vp => vp.Produto)
                .WithMany(p => p.VendaProdutos)
                .HasForeignKey(vp => vp.ProdutoId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<VendaProduto> VendaProdutos { get; set; }
    }
}
