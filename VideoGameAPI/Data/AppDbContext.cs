using Microsoft.EntityFrameworkCore;
using VideoGameAPI.Models;

namespace VideoGameAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Videojuego> Videojuegos => Set<Videojuego>();
    public DbSet<Resena> Resenas => Set<Resena>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Videojuego>(entity =>
        {
            entity.HasKey(v => v.Id);
            entity.Property(v => v.Nombre).IsRequired().HasMaxLength(150);
            entity.Property(v => v.Precio).HasColumnType("decimal(18,2)");
            entity.HasMany(v => v.Resenas)
                  .WithOne(r => r.Videojuego)
                  .HasForeignKey(r => r.VideojuegoId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Resena>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Calificacion).IsRequired();
            entity.Property(r => r.Comentario).HasMaxLength(500);
            entity.Property(r => r.Autor).HasMaxLength(100);
        });
    }
}
