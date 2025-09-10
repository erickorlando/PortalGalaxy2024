using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalGalaxy.Entities;

[Table("Taller")]
[Index("CategoriaId", Name = "IX_Taller_CategoriaId")]
[Index("InstructorId", Name = "IX_Taller_InstructorId")]
[Index("Nombre", Name = "IX_Taller_Nombre")]
public partial class Taller : EntityBase
{
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    public int CategoriaId { get; set; }

    public int InstructorId { get; set; }

    [Column(TypeName = "date")]
    public DateTime FechaInicio { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime HoraInicio { get; set; }

    public SituacionTaller Situacion { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? PortadaUrl { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? TemarioUrl { get; set; }

    [StringLength(700)]
    public string? Descripcion { get; set; }

    [ForeignKey("CategoriaId")]
    [InverseProperty("Tallers")]
    public virtual Categoria Categoria { get; set; } = null!;

    [InverseProperty("Taller")]
    public virtual ICollection<Inscripcion> Inscripcions { get; set; } = new List<Inscripcion>();

    [ForeignKey("InstructorId")]
    [InverseProperty("Tallers")]
    public virtual Instructor Instructor { get; set; } = null!;
}
