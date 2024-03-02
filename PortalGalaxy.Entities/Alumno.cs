using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalGalaxy.Entities;

[Table("Alumno")]
public partial class Alumno : EntityBase
{
    [StringLength(100)]
    public string NroDocumento { get; set; } = null!;

    [StringLength(100)]
    public string NombreCompleto { get; set; } = null!;

    [StringLength(100)]
    public string Correo { get; set; } = null!;

    [StringLength(100)]
    public string? Telefono { get; set; }

    [StringLength(100)]
    public string Departamento { get; set; } = null!;

    [StringLength(100)]
    public string Provincia { get; set; } = null!;

    [StringLength(100)]
    public string Distrito { get; set; } = null!;

    [Column(TypeName = "date")]
    public DateTime FechaInscripcion { get; set; }

    [InverseProperty("Alumno")]
    public virtual ICollection<Inscripcion> Inscripcions { get; set; } = new List<Inscripcion>();
}
