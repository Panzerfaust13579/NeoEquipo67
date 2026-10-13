using Microsoft.EntityFrameworkCore;
using VideoGameAPI.Models;
using System.Collections.Generic;

namespace VideoGameAPI.Data;

public class AppDbContext (DbContextOptions<AppDbContext> options) : DbContext (options)
{

    public DbSet<Desarrollador> Desarrolladores { get; set; }
    public DbSet<Videojuego> Videojuegos { get; set; }
    public DbSet<Resena> Resenas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Desarrollador>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Nombre).IsRequired().HasMaxLength(150);
            entity.Property(d => d.Pais).IsRequired().HasMaxLength(100);
            entity.Property(d => d.SitioWeb).HasMaxLength(250);
            entity.HasMany(d => d.Videojuegos)
                  .WithOne(v => v.Desarrollador)
                  .HasForeignKey(v => v.DesarrolladorId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

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
