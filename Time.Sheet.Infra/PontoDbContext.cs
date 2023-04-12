using Microsoft.EntityFrameworkCore;
using Time.Sheet.Domain.Models;

namespace Time.Sheet.Infra
{
    public class PontoDbContext : DbContext
    {
        public PontoDbContext(DbContextOptions<PontoDbContext> options) : base(options)
        {
        }

        public DbSet<Registro> Registros { get; set; }
        public DbSet<Horario> Horarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações adicionais do modelo aqui
            base.OnModelCreating(modelBuilder);
        }
    }
}
