using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using HubCount.Model;

namespace HubCount.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Pedidos> Pedidos { get; set; }
        public DbSet<Produtos> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clientes>()
                .HasIndex(c => c.Documento)
                .IsUnique();

            modelBuilder.Entity<Clientes>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Pedidos>()
                .HasOne<Clientes>()
                .WithMany(cliente => cliente.Pedidos)
                .HasForeignKey(pedido => pedido.Documento);

            modelBuilder.Entity<Pedidos>()
                .HasOne(pedido => pedido.Produto)
                .WithMany()
                .HasForeignKey(pedido => pedido.ProdutoId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
