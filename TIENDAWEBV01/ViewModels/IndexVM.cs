using TIENDAWEBV01.Models;

namespace TIENDAWEBV01.ViewModels
{
    public class IndexVM
    {
        public List<Categorias> categorias { get; set; }
        public List<Productos> productos { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}
