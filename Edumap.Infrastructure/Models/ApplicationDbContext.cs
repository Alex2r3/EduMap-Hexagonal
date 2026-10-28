using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Edumap.Infrastructure.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Distrito> Distritos { get; set; }

    public virtual DbSet<Institucion> Institucions { get; set; }

    public virtual DbSet<NivelEducativo> NivelEducativos { get; set; }

    public virtual DbSet<OfertaEducativa> OfertaEducativas { get; set; }

    public virtual DbSet<Resena> Resenas { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<VwCalificacionInstitucion> VwCalificacionInstitucions { get; set; }

    public virtual DbSet<VwInstitucionResuman> VwInstitucionResumen { get; set; }

    public virtual DbSet<VwReporteCategoria> VwReporteCategorias { get; set; }

    public virtual DbSet<VwReporteParticipacion> VwReporteParticipacions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=edumap;Username=postgres;Password=Clave321;Port=5432");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("categoria_pkey");

            entity.ToTable("categoria", tb => tb.HasComment("Tipo de institucion: colegio, academia, instituto, universidad"));

            entity.HasIndex(e => e.Nombre, "categoria_nombre_key").IsUnique();

            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.IconoUrl)
                .HasMaxLength(255)
                .HasColumnName("icono_url");
            entity.Property(e => e.Nombre)
                .HasMaxLength(40)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Distrito>(entity =>
        {
            entity.HasKey(e => e.IdDistrito).HasName("distrito_pkey");

            entity.ToTable("distrito", tb => tb.HasComment("Ubicacion geografica para filtros"));

            entity.HasIndex(e => e.Provincia, "idx_distrito_provincia");

            entity.HasIndex(e => new { e.Provincia, e.Nombre }, "uq_distrito").IsUnique();

            entity.Property(e => e.IdDistrito).HasColumnName("id_distrito");
            entity.Property(e => e.Nombre)
                .HasMaxLength(80)
                .HasColumnName("nombre");
            entity.Property(e => e.Provincia)
                .HasMaxLength(60)
                .HasColumnName("provincia");
        });

        modelBuilder.Entity<Institucion>(entity =>
        {
            entity.HasKey(e => e.IdInstitucion).HasName("institucion_pkey");

            entity.ToTable("institucion", tb => tb.HasComment("Directorio de instituciones educativas con ubicacion y contacto"));

            entity.HasIndex(e => e.IdCategoria, "idx_inst_categoria");

            entity.HasIndex(e => new { e.Latitud, e.Longitud }, "idx_inst_coord");

            entity.HasIndex(e => e.IdDistrito, "idx_inst_distrito");

            entity.HasIndex(e => e.Estado, "idx_inst_estado");

            entity.HasIndex(e => e.IdRepresentante, "idx_inst_representante");

            entity.Property(e => e.IdInstitucion).HasColumnName("id_institucion");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .HasColumnName("direccion");
            entity.Property(e => e.Email)
                .HasMaxLength(120)
                .HasColumnName("email");
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .HasDefaultValueSql("'PENDIENTE'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.FechaActualizacion)
                .HasDefaultValueSql("now()")
                .HasColumnName("fecha_actualizacion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("now()")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.IdDistrito).HasColumnName("id_distrito");
            entity.Property(e => e.IdRepresentante)
                .HasComment("Usuario con rol REPRESENTANTE que administra la institucion (opcional)")
                .HasColumnName("id_representante");
            entity.Property(e => e.Latitud).HasColumnName("latitud");
            entity.Property(e => e.LogoUrl)
                .HasMaxLength(255)
                .HasColumnName("logo_url");
            entity.Property(e => e.Longitud).HasColumnName("longitud");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("nombre");
            entity.Property(e => e.SitioWeb)
                .HasMaxLength(200)
                .HasColumnName("sitio_web");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
            entity.Property(e => e.TipoGestion)
                .HasMaxLength(10)
                .HasDefaultValueSql("'PRIVADA'::character varying")
                .HasColumnName("tipo_gestion");
            entity.Property(e => e.Whatsapp)
                .HasMaxLength(20)
                .HasColumnName("whatsapp");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Institucions)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("institucion_id_categoria_fkey");

            entity.HasOne(d => d.IdDistritoNavigation).WithMany(p => p.Institucions)
                .HasForeignKey(d => d.IdDistrito)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("institucion_id_distrito_fkey");

            entity.HasOne(d => d.IdRepresentanteNavigation).WithMany(p => p.Institucions)
                .HasForeignKey(d => d.IdRepresentante)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("institucion_id_representante_fkey");
        });

        modelBuilder.Entity<NivelEducativo>(entity =>
        {
            entity.HasKey(e => e.IdNivel).HasName("nivel_educativo_pkey");

            entity.ToTable("nivel_educativo", tb => tb.HasComment("Niveles: inicial, primaria, secundaria, pregrado, etc."));

            entity.HasIndex(e => e.Nombre, "nivel_educativo_nombre_key").IsUnique();

            entity.Property(e => e.IdNivel).HasColumnName("id_nivel");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<OfertaEducativa>(entity =>
        {
            entity.HasKey(e => e.IdOferta).HasName("oferta_educativa_pkey");

            entity.ToTable("oferta_educativa", tb => tb.HasComment("Programas o niveles que ofrece cada institucion, con pension estimada"));

            entity.HasIndex(e => e.IdInstitucion, "idx_oferta_institucion");

            entity.HasIndex(e => e.IdNivel, "idx_oferta_nivel");

            entity.HasIndex(e => new { e.PensionMin, e.PensionMax }, "idx_oferta_pension");

            entity.HasIndex(e => new { e.IdInstitucion, e.IdNivel, e.Nombre, e.Modalidad }, "uq_oferta").IsUnique();

            entity.Property(e => e.IdOferta).HasColumnName("id_oferta");
            entity.Property(e => e.Activa)
                .HasDefaultValue(true)
                .HasColumnName("activa");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.DuracionMeses).HasColumnName("duracion_meses");
            entity.Property(e => e.IdInstitucion).HasColumnName("id_institucion");
            entity.Property(e => e.IdNivel).HasColumnName("id_nivel");
            entity.Property(e => e.Matricula)
                .HasPrecision(10, 2)
                .HasColumnName("matricula");
            entity.Property(e => e.Modalidad)
                .HasMaxLength(15)
                .HasDefaultValueSql("'PRESENCIAL'::character varying")
                .HasColumnName("modalidad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("nombre");
            entity.Property(e => e.PensionMax)
                .HasPrecision(10, 2)
                .HasColumnName("pension_max");
            entity.Property(e => e.PensionMin)
                .HasPrecision(10, 2)
                .HasColumnName("pension_min");

            entity.HasOne(d => d.IdInstitucionNavigation).WithMany(p => p.OfertaEducativas)
                .HasForeignKey(d => d.IdInstitucion)
                .HasConstraintName("oferta_educativa_id_institucion_fkey");

            entity.HasOne(d => d.IdNivelNavigation).WithMany(p => p.OfertaEducativas)
                .HasForeignKey(d => d.IdNivel)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("oferta_educativa_id_nivel_fkey");
        });

        modelBuilder.Entity<Resena>(entity =>
        {
            entity.HasKey(e => e.IdResena).HasName("resena_pkey");

            entity.ToTable("resena", tb => tb.HasComment("Calificacion (1-5) y comentario; una por usuario e institucion"));

            entity.HasIndex(e => e.IdInstitucion, "idx_resena_institucion");

            entity.HasIndex(e => e.IdUsuario, "idx_resena_usuario");

            entity.HasIndex(e => new { e.IdUsuario, e.IdInstitucion }, "uq_resena_usuario_inst").IsUnique();

            entity.Property(e => e.IdResena).HasColumnName("id_resena");
            entity.Property(e => e.Calificacion).HasColumnName("calificacion");
            entity.Property(e => e.Comentario).HasColumnName("comentario");
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .HasDefaultValueSql("'VISIBLE'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.FechaActualizacion).HasColumnName("fecha_actualizacion");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("now()")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.IdInstitucion).HasColumnName("id_institucion");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

            entity.HasOne(d => d.IdInstitucionNavigation).WithMany(p => p.Resenas)
                .HasForeignKey(d => d.IdInstitucion)
                .HasConstraintName("resena_id_institucion_fkey");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Resenas)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("resena_id_usuario_fkey");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("rol_pkey");

            entity.ToTable("rol", tb => tb.HasComment("Roles de acceso: USUARIO, REPRESENTANTE, ADMINISTRADOR"));

            entity.HasIndex(e => e.Nombre, "rol_nombre_key").IsUnique();

            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("usuario_pkey");

            entity.ToTable("usuario", tb => tb.HasComment("Cuentas del sistema con su rol"));

            entity.HasIndex(e => e.IdRol, "idx_usuario_rol");

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(80)
                .HasColumnName("apellidos");
            entity.Property(e => e.Email)
                .HasMaxLength(120)
                .HasColumnName("email");
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .HasDefaultValueSql("'ACTIVO'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("now()")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.FotoUrl)
                .HasMaxLength(255)
                .HasColumnName("foto_url");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Nombres)
                .HasMaxLength(80)
                .HasColumnName("nombres");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
            entity.Property(e => e.UltimoAcceso).HasColumnName("ultimo_acceso");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("usuario_id_rol_fkey");
        });

        modelBuilder.Entity<VwCalificacionInstitucion>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_calificacion_institucion");

            entity.Property(e => e.IdInstitucion).HasColumnName("id_institucion");
            entity.Property(e => e.PromedioCalificacion).HasColumnName("promedio_calificacion");
            entity.Property(e => e.TotalResenas).HasColumnName("total_resenas");
        });

        modelBuilder.Entity<VwInstitucionResuman>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_institucion_resumen");

            entity.Property(e => e.Categoria)
                .HasMaxLength(40)
                .HasColumnName("categoria");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .HasColumnName("direccion");
            entity.Property(e => e.Distrito)
                .HasMaxLength(80)
                .HasColumnName("distrito");
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .HasColumnName("estado");
            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.IdDistrito).HasColumnName("id_distrito");
            entity.Property(e => e.IdInstitucion).HasColumnName("id_institucion");
            entity.Property(e => e.Latitud).HasColumnName("latitud");
            entity.Property(e => e.Longitud).HasColumnName("longitud");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("nombre");
            entity.Property(e => e.PensionMax).HasColumnName("pension_max");
            entity.Property(e => e.PensionMin).HasColumnName("pension_min");
            entity.Property(e => e.PromedioCalificacion).HasColumnName("promedio_calificacion");
            entity.Property(e => e.Provincia)
                .HasMaxLength(60)
                .HasColumnName("provincia");
            entity.Property(e => e.TipoGestion)
                .HasMaxLength(10)
                .HasColumnName("tipo_gestion");
            entity.Property(e => e.TotalResenas).HasColumnName("total_resenas");
        });

        modelBuilder.Entity<VwReporteCategoria>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_reporte_categorias");

            entity.Property(e => e.CalificacionPromedio).HasColumnName("calificacion_promedio");
            entity.Property(e => e.Categoria)
                .HasMaxLength(40)
                .HasColumnName("categoria");
            entity.Property(e => e.TotalInstituciones).HasColumnName("total_instituciones");
        });

        modelBuilder.Entity<VwReporteParticipacion>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_reporte_participacion");

            entity.Property(e => e.TotalResenas).HasColumnName("total_resenas");
            entity.Property(e => e.TotalUsuarios).HasColumnName("total_usuarios");
            entity.Property(e => e.UsuariosActivos).HasColumnName("usuarios_activos");
            entity.Property(e => e.UsuariosConResenas).HasColumnName("usuarios_con_resenas");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
