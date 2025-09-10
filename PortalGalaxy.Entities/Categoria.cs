using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalGalaxy.Entities;

public partial class Categoria : EntityBase
{
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    public DateTime Hora { get; set; }

    [StringLength(50)]
    public string? Descripcion { get; set; }

    [InverseProperty("Categoria")]
    public virtual ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();

    [InverseProperty("Categoria")]
    public virtual ICollection<Taller> Tallers { get; set; } = new List<Taller>();
}
