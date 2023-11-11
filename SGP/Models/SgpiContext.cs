using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SGP.Models;

public partial class SgpiContext : DbContext
{
    public SgpiContext()
    {
    }

    public SgpiContext(DbContextOptions<SgpiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asignatura> Asignaturas { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Homologacion> Homologacions { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Programa> Programas { get; set; }

    public virtual DbSet<Programacion> Programacions { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; }

    public virtual DbSet<TipoHomologacion> TipoHomologacions { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-H0LAHD60;Database=SGPI;Trusted_Connection=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asignatura>(entity =>
        {
            entity.HasKey(e => e.IdAsignatura);

            entity.ToTable("Asignatura");

            entity.Property(e => e.IdAsignatura).HasColumnName("id_asignatura");
            entity.Property(e => e.Aula).HasColumnName("aula");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Fecha)
                .HasColumnType("date")
                .HasColumnName("fecha");
            entity.Property(e => e.Grupo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("grupo");
            entity.Property(e => e.PeriodoAcademico).HasColumnName("periodo_academico");
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.IdGenero);

            entity.ToTable("Genero");

            entity.Property(e => e.IdGenero).HasColumnName("id_genero");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Homologacion>(entity =>
        {
            entity.HasKey(e => e.IdHomologacion);

            entity.ToTable("Homologacion");

            entity.Property(e => e.IdHomologacion).HasColumnName("id_homologacion");
            entity.Property(e => e.AsignaturaAnterior)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("asignatura_anterior");
            entity.Property(e => e.AsignaturaNueva)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("asignatura_nueva");
            entity.Property(e => e.CreditoAnterior).HasColumnName("credito_anterior");
            entity.Property(e => e.CreditoNuevo).HasColumnName("credito_nuevo");
            entity.Property(e => e.Descripcion)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdAsignatura).HasColumnName("id_asignatura");
            entity.Property(e => e.IdTipohomologacion).HasColumnName("id_tipohomologacion");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.NivelAnterior).HasColumnName("nivel_anterior");
            entity.Property(e => e.NivelNuevo).HasColumnName("nivel_nuevo");
            entity.Property(e => e.Nota).HasColumnName("nota");
            entity.Property(e => e.NumeroDoc).HasColumnName("numero_doc");
            entity.Property(e => e.Programa)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("programa");
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_documento");

            entity.HasOne(d => d.IdAsignaturaNavigation).WithMany(p => p.Homologacions)
                .HasForeignKey(d => d.IdAsignatura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Homologacion_Asignatura");

            entity.HasOne(d => d.IdTipohomologacionNavigation).WithMany(p => p.Homologacions)
                .HasForeignKey(d => d.IdTipohomologacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Homologacion_TipoHomologacion");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Homologacions)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Homologacion_Usuarios");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.IdPago);

            entity.ToTable("Pago");

            entity.Property(e => e.IdPago).HasColumnName("id_pago");
            entity.Property(e => e.Fecha)
                .HasColumnType("date")
                .HasColumnName("fecha");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.ValorPago).HasColumnName("valor_pago");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pago_Usuarios");
        });

        modelBuilder.Entity<Programa>(entity =>
        {
            entity.HasKey(e => e.IdPrograma);

            entity.ToTable("Programa");

            entity.Property(e => e.IdPrograma).HasColumnName("id_programa");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Programacion>(entity =>
        {
            entity.HasKey(e => e.IdProgramacion);

            entity.ToTable("Programacion");

            entity.Property(e => e.IdProgramacion).HasColumnName("id_programacion");
            entity.Property(e => e.Asignatura)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("asignatura");
            entity.Property(e => e.Aula)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("aula");
            entity.Property(e => e.Fecha)
                .HasColumnType("date")
                .HasColumnName("fecha");
            entity.Property(e => e.Grupo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("grupo");
            entity.Property(e => e.IdPrograma).HasColumnName("id_programa");
            entity.Property(e => e.Periodo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("periodo");

            entity.HasOne(d => d.IdProgramaNavigation).WithMany(p => p.Programacions)
                .HasForeignKey(d => d.IdPrograma)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Programacion_Programa");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol);

            entity.ToTable("Rol");

            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<TipoDocumento>(entity =>
        {
            entity.HasKey(e => e.IdDoc);

            entity.ToTable("TipoDocumento");

            entity.Property(e => e.IdDoc).HasColumnName("id_doc");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<TipoHomologacion>(entity =>
        {
            entity.HasKey(e => e.IdTipohomologacion);

            entity.ToTable("TipoHomologacion");

            entity.Property(e => e.IdTipohomologacion).HasColumnName("id_tipohomologacion");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.IdDoc).HasColumnName("id_doc");
            entity.Property(e => e.IdGenero).HasColumnName("id_genero");
            entity.Property(e => e.IdPrograma).HasColumnName("id_programa");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.NumeroDoc).HasColumnName("numero_doc");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.PrimerApellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("primer_apellido");
            entity.Property(e => e.SegundoApellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("segundo_apellido");

            entity.HasOne(d => d.IdDocNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdDoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_TipoDocumento");

            entity.HasOne(d => d.IdGeneroNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdGenero)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_Genero");

            entity.HasOne(d => d.IdProgramaNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdPrograma)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_Programa");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_Rol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
