using System.ComponentModel.DataAnnotations;

namespace TIENDAWEBV01.Models
{
    public class Categorias
    {
        [Key]
        public int IdCategoria { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
    }
}
