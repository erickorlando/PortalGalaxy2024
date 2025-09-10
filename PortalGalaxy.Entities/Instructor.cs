using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PortalGalaxy.Entities;

[Table("Instructor")]
[Index("CategoriaId", Name = "IX_Instructor_CategoriaId")]
[Index("NroDocumento", Name = "IX_Instructor_NroDocumento")]
public partial class Instructor : EntityBase
{
    [StringLength(100)]
    public string Nombres { get; set; } = null!;

    [StringLength(12)]
    public string NroDocumento { get; set; } = null!;

    public int CategoriaId { get; set; }

    [ForeignKey("CategoriaId")]
    [InverseProperty("Instructors")]
    public virtual Categoria Categoria { get; set; } = null!;

    [InverseProperty("Instructor")]
    public virtual ICollection<Taller> Tallers { get; set; } = new List<Taller>();
}
