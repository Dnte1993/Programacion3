//Data/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using ProductoCRUD.Models;

namespace ProductoCRUD.Data
{
    public class AppDbContext : DbContext
    {
        //DbSet representa la tabla "Productos" en la base de datos
        public DbSet<Producto> Productos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Cadena de conexión a SQL Server LocalDB
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=ProductoCRUDDb;Trusted_Connection=True;"
            );
            // Opcional: Habilitar logging de SQL generado
            optionsBuilder.LogTo(Console.WriteLine,
            Microsoft.Extensions.Logging.LogLevel.Information);

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuraciones adicionales del modelo
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.Property(p => p.Nombre).HasMaxLength(200).IsRequired();
                entity.Property(p => p.Precio).HasColumnType("decimal(18,2)");

            });
        }
    }
}