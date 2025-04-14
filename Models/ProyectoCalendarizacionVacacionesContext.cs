using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace ProyectoCalendarizacionDeVacaciones.Models;

public partial class ProyectoCalendarizacionVacacionesContext : DbContext
{
    public ProyectoCalendarizacionVacacionesContext()
    {
    }

    public ProyectoCalendarizacionVacacionesContext(DbContextOptions<ProyectoCalendarizacionVacacionesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Departamentos> Departamentos { get; set; }

    public virtual DbSet<EstadoSolicitud> EstadoSolicitud { get; set; }

    public virtual DbSet<Rol> Rol { get; set; }

    public virtual DbSet<SolicitudVacaciones> SolicitudVacaciones { get; set; }

    public virtual DbSet<TipoContrato> TipoContrato { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;password=root;database=proyecto_calendarizacion_vacaciones", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Departamentos>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("departamentos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NombreDepartamento)
                .HasMaxLength(100)
                .HasColumnName("nombre_departamento");
        });

        modelBuilder.Entity<EstadoSolicitud>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("estado_solicitud");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(25)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("rol");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<SolicitudVacaciones>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("solicitud_vacaciones");

            entity.HasIndex(e => e.EstadoId, "estado_id");

            entity.HasIndex(e => e.JefeResponsableId, "jefe_responsable_id");

            entity.HasIndex(e => e.UsuarioId, "usuario_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EstadoId).HasColumnName("estado_id");
            entity.Property(e => e.FechaFin).HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio).HasColumnName("fecha_inicio");
            entity.Property(e => e.FechaRespuesta).HasColumnName("fecha_respuesta");
            entity.Property(e => e.FechaSolicitud).HasColumnName("fecha_solicitud");
            entity.Property(e => e.JefeResponsableId).HasColumnName("jefe_responsable_id");
            entity.Property(e => e.MotivoRechazo)
                .HasColumnType("text")
                .HasColumnName("motivo_rechazo");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Estado).WithMany(p => p.SolicitudVacaciones)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("solicitud_vacaciones_ibfk_2");

            entity.HasOne(d => d.JefeResponsable).WithMany(p => p.SolicitudVacacionesJefeResponsable)
                .HasForeignKey(d => d.JefeResponsableId)
                .HasConstraintName("solicitud_vacaciones_ibfk_3");

            entity.HasOne(d => d.Usuario).WithMany(p => p.SolicitudVacacionesUsuario)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("solicitud_vacaciones_ibfk_1");
        });

        modelBuilder.Entity<TipoContrato>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tipo_contrato");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Tipo)
                .HasMaxLength(100)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.DepartamentoId, "departamento_id");

            entity.HasIndex(e => e.JefeDepartamentoId, "jefe_departamento_id");

            entity.HasIndex(e => e.RolId, "rol_id");

            entity.HasIndex(e => e.TipoContratoId, "tipo_contrato_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(255)
                .HasColumnName("contraseña");
            entity.Property(e => e.DepartamentoId).HasColumnName("departamento_id");
            entity.Property(e => e.EsJefeDepartamento)
                .HasDefaultValueSql("'0'")
                .HasColumnName("es_jefe_departamento");
            entity.Property(e => e.FechaIngreso).HasColumnName("fecha_ingreso");
            entity.Property(e => e.JefeDepartamentoId).HasColumnName("jefe_departamento_id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.RolId).HasColumnName("rol_id");
            entity.Property(e => e.RpeRtt)
                .HasMaxLength(50)
                .HasColumnName("rpe_rtt");
            entity.Property(e => e.TipoContratoId).HasColumnName("tipo_contrato_id");

            entity.HasOne(d => d.Departamento).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.DepartamentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usuario_ibfk_1");

            entity.HasOne(d => d.JefeDepartamento).WithMany(p => p.InverseJefeDepartamento)
                .HasForeignKey(d => d.JefeDepartamentoId)
                .HasConstraintName("usuario_ibfk_4");

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usuario_ibfk_3");

            entity.HasOne(d => d.TipoContrato).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.TipoContratoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usuario_ibfk_2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
