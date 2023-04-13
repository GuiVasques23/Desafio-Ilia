using Microsoft.EntityFrameworkCore;
using Time.Sheet.Domain.Models;

namespace Time.Sheet.Infraestructure
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
                .HasKey(r => r.Id);
            modelBuilder.Entity<Registro>()
                .Property(r => r.Dia)
                .HasField("date");
            modelBuilder.Entity<Registro>()
                .HasIndex(r => r.Dia)
                .IsUnique();
            modelBuilder.Entity<Registro>()
                .HasMany(r => r.Horarios)
                .WithOne()
                .HasForeignKey("RegistroId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}