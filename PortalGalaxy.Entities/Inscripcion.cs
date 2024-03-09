using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalGalaxy.Entities;

[Table("Inscripcion")]
[Index("AlumnoId", Name = "IX_Inscripcion_AlumnoId")]
[Index("TallerId", Name = "IX_Inscripcion_TallerId")]
public partial class Inscripcion : EntityBase
{
    public int AlumnoId { get; set; }

    public int TallerId { get; set; }

    public SituacionInscripcion Situacion { get; set; }

    [ForeignKey("AlumnoId")]
    [InverseProperty("Inscripcions")]
    public virtual Alumno Alumno { get; set; } = null!;

    [ForeignKey("TallerId")]
    [InverseProperty("Inscripcions")]
    public virtual Taller Taller { get; set; } = null!;
}
