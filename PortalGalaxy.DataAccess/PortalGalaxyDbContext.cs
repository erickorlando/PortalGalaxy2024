using Microsoft.EntityFrameworkCore;
using PortalGalaxy.Entities;

namespace PortalGalaxy.DataAccess;

public partial class PortalGalaxyDbContext : DbContext
{
    public PortalGalaxyDbContext(DbContextOptions<PortalGalaxyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumno> Alumnos { get; set; } = default!;

    public virtual DbSet<Categoria> Categoria { get; set; }= default!;

    public virtual DbSet<Inscripcion> Inscripciones { get; set; }= default!;

    public virtual DbSet<Instructor> Instructores { get; set; }= default!;

    public virtual DbSet<Taller> Talleres { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.Property(e => e.Hora).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");
        });

        modelBuilder.Entity<Inscripcion>(entity =>
        {
            entity.HasOne(d => d.Alumno).WithMany(p => p.Inscripcions).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Taller).WithMany(p => p.Inscripcions).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Instructor>(entity =>
        {
            entity.HasOne(d => d.Categoria).WithMany(p => p.Instructors).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Taller>(entity =>
        {
            entity.HasOne(d => d.Categoria).WithMany(p => p.Tallers).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Instructor).WithMany(p => p.Tallers).OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
