using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Time.Sheet.Domain.Models;

namespace Time.Sheet.Domain.Infra
{
    public class PontoDbContext : DbContext
    {
        public PontoDbContext(DbContextOptions<PontoDbContext> options)
            : base(options)
        {
        }

        public DbSet<Registro> Registros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Registro>()
                .HasMany(r => r.Horarios)
                .WithOne(h => h.Registro)
                .HasForeignKey(h => h.Id);
        }
    }
}
