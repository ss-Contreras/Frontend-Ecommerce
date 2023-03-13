using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TIENDAWEBV01.Models
{
    public class Productos
    {
        [Key]
        public int IdProducto { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public decimal Precio { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public string Ruta { get; set; }

        public int IdCategoria { get; set; }
        [ForeignKey("IdCategoria")]
        public Categorias Categorias { get; set; }
    }
}
