using Microsoft.EntityFrameworkCore;
using Venta.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ) : base ( options )
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Venta.Domain.Entities.Venta>()
                 .Property(u => u.MetodoPago)
                 .HasConversion<String>();

            modelBuilder.Entity<Venta.Domain.Entities.Venta>()
                 .Property(u => u.Estado)
                 .HasConversion<String>();

            modelBuilder.Entity<CrearCuenta>()
                .Property(u => u.Role)
                .HasConversion<String>();
        }

        public DbSet<Venta.Domain.Entities.Venta> Ventas { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Producto> Productos { get; set; }

        public DbSet<DetalleVenta> DetalleVentas { get; set; }

        public DbSet<CrearCuenta> CrearCuentas { get; set; }

        public DbSet<Persona> Personas { get; set; }
    }
}
