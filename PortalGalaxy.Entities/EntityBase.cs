using System.ComponentModel.DataAnnotations;

namespace PortalGalaxy.Entities
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }

        public bool Estado { get; set; }

        public DateTime FechaCreacion { get; set; }

        protected EntityBase()
        {
            FechaCreacion = DateTime.Now;
            Estado = true;
        }
    }
}
