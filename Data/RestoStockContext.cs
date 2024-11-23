using Microsoft.EntityFrameworkCore;
using RestoStockDB1.Models;

namespace RestoStockDB1.Data
{
    public class RestoStockContext : DbContext
    {

        // Constructor que recibe DbContextOptions
        public RestoStockContext(DbContextOptions<RestoStockContext> options)
            : base(options)
        {
        }

        public DbSet<DetallePlato> DetallesPlatos { get; set; }
        public DbSet<Ingrediente> Ingredientes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Proovedor> Proovedores { get; set; }
        public DbSet<Plato> Platos { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RestauranteStock;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }
    }
}
