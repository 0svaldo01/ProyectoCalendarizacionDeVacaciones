using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace ProyectoCalendarizacionDeVacaciones.Models;

public partial class ProyectoCfeContext : DbContext
{
    public ProyectoCfeContext()
    {
    }

    public ProyectoCfeContext(DbContextOptions<ProyectoCfeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Departamento> Departamento { get; set; }

    public virtual DbSet<Puesto> Puesto { get; set; }

    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<Solicitudvacacion> Solicitudvacacion { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    public virtual DbSet<Vacaciones> Vacaciones { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;password=root;database=ProyectoCFE", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento).HasName("PRIMARY");

            entity.ToTable("departamento");

            entity.Property(e => e.IdDepartamento).HasColumnName("idDepartamento");
            entity.Property(e => e.NombreDepartamento).HasMaxLength(45);
        });

        modelBuilder.Entity<Puesto>(entity =>
        {
            entity.HasKey(e => e.IdPuesto).HasName("PRIMARY");

            entity.ToTable("puesto");

            entity.Property(e => e.IdPuesto).HasColumnName("idPuesto");
            entity.Property(e => e.NombrePuesto).HasMaxLength(45);
        });

        modelBuilder.Entity<Roles>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.NombreRol).HasMaxLength(45);
        });

        modelBuilder.Entity<Solicitudvacacion>(entity =>
        {
            entity.HasKey(e => e.IdSolicitudVacacion).HasName("PRIMARY");

            entity.ToTable("solicitudvacacion");

            entity.HasIndex(e => e.IdUsuario, "FkUsuariov_idx");

            entity.Property(e => e.IdSolicitudVacacion)
                .ValueGeneratedNever()
                .HasColumnName("idSolicitudVacacion");
            entity.Property(e => e.Comentarios).HasColumnType("text");
            entity.Property(e => e.Estado).HasMaxLength(45);
            entity.Property(e => e.FechaInicioFin).HasColumnType("datetime");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Solicitudvacacion)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FkUsuariov");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.IdDepartamento, "FkDepartamento_idx");

            entity.HasIndex(e => e.IdPuesto, "FkPuesto_idx");

            entity.HasIndex(e => e.IdRol, "FkRol_idx");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.FechaDeIngreso).HasColumnType("datetime");
            entity.Property(e => e.JefeDirecto).HasMaxLength(45);
            entity.Property(e => e.Nombre).HasMaxLength(200);
            entity.Property(e => e.Password).HasMaxLength(200);
            entity.Property(e => e.RpRt)
                .HasMaxLength(45)
                .HasColumnName("RP-RT");

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.IdDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FkDepartamento");

            entity.HasOne(d => d.IdPuestoNavigation).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.IdPuesto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FkPuesto");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FkRol");
        });

        modelBuilder.Entity<Vacaciones>(entity =>
        {
            entity.HasKey(e => e.Idvacacion).HasName("PRIMARY");

            entity.ToTable("vacaciones");

            entity.HasIndex(e => e.IdUsuario, "FkUsuario_idx");

            entity.Property(e => e.Idvacacion)
                .ValueGeneratedNever()
                .HasColumnName("idvacacion");
            entity.Property(e => e.Vigencia).HasColumnType("datetime");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Vacaciones)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FkUsuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
