using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Alejandro_Brito.Models;

public partial class AlejandroBritoContext : DbContext
{
    public AlejandroBritoContext()
    {
    }

    public AlejandroBritoContext(DbContextOptions<AlejandroBritoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Tarea> Tareas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK_Estado");

            entity.Property(e => e.Descripcion).HasMaxLength(50);
        });

        modelBuilder.Entity<Tarea>(entity =>
        {
            entity.HasKey(e => e.IdTarea).HasName("PK_Tarea");

            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Titulo).HasMaxLength(100);

            entity.HasOne(d => d.oEstado).WithMany(p => p.Tareas)
                .HasForeignKey(d => d.Estado)
                .HasConstraintName("FK__Tareas__Estado__4E88ABD4");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97F349A9CE");

            entity.ToTable("Usuario");

            entity.Property(e => e.Apellido).HasMaxLength(50);
            entity.Property(e => e.Cedula).HasMaxLength(50);
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.NombreCompleto).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
