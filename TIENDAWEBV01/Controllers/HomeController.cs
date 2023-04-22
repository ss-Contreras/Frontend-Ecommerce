using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TIENDAWEBV01.Models;
using TIENDAWEBV01.ViewModels;
using TIENDAWEBV01.Service;

namespace TIENDAWEBV01.Controllers
{
    public class HomeController : Controller
    {
        private Services _service;
        private IConfiguration _configuration;
        private readonly string _host;
        //static HttpClient client = new HttpClient();
        static HttpClient conexion;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            conexion = new HttpClient();
            conexion.BaseAddress = new Uri(_configuration.GetConnectionString("tiendaApi"));
            _service = new Services(configuration, httpContextAccessor);
        }

        [Route("home/Index/{pageNumber}/{pageSize}")]
        [Route("/")]
        public async Task <IActionResult> Index(int page = 1, int pageSize = 12)
        {
            var cookies = _service.VerificarCookie();
            IndexVM vm = new IndexVM();
            string apiString;
            using (HttpResponseMessage response = await conexion.GetAsync("categorias/categorias"))
            {
                apiString = await response.Content.ReadAsStringAsync();
                vm.categorias = JsonConvert.DeserializeObject<List<Categorias>>(apiString);
            }
            using (HttpResponseMessage response = await conexion.GetAsync("productos/getproductos"))
            {
                apiString = await response.Content.ReadAsStringAsync();
                vm.productos = JsonConvert.DeserializeObject<List<Productos>>(apiString);
                vm.TotalCount = vm.productos.Count;
            }

            using (HttpResponseMessage response = await conexion.GetAsync("productos/GetProductosRango/" + page + "/" + pageSize ))
            {
                apiString = await response.Content.ReadAsStringAsync();
                vm.productos = JsonConvert.DeserializeObject<List<Productos>>(apiString);
            }

            vm.PageNumber = page;
            vm.PageSize = pageSize;


            //mostrar productos

            return View(vm);
        }
        [Route("home/DetalleProducto/{IdProducto}")]
        public async Task<IActionResult> DetalleProducto(int IdProducto)
        {
            Productos productos = new Productos();
            string apiString;
            using (HttpResponseMessage response = await conexion.GetAsync("Productos/ObtenerProductoByIdProducto/" + IdProducto))
            {
                apiString = await response.Content.ReadAsStringAsync();
                productos = JsonConvert.DeserializeObject<Productos>(apiString);
            }

            return View(productos);
        }

        public async Task<IActionResult> AboutUs()
        {

            return View();
        }

        public async Task<IActionResult> Cart()
        {
            List<CarritoVM> vm = new List<CarritoVM>();
            string apiString;
            
            
            using (HttpResponseMessage response = await conexion.GetAsync("Carritos/GetCarritoByIdUsuario/1"))
            {

                apiString = await response.Content.ReadAsStringAsync();
                vm = JsonConvert.DeserializeObject<List<CarritoVM>>(apiString);
            }
            return View(vm);
        }
        public async Task<IActionResult> CheckOut()
        {
            IndexVM vm = new IndexVM();
            string apiString;
            using (HttpResponseMessage response = await conexion.GetAsync("categorias/categorias"))
            {
                apiString = await response.Content.ReadAsStringAsync();
                vm.categorias = JsonConvert.DeserializeObject<List<Categorias>>(apiString);
            }
            using (HttpResponseMessage response = await conexion.GetAsync("productos/getproductos"))
            {
                apiString = await response.Content.ReadAsStringAsync();
                vm.productos = JsonConvert.DeserializeObject<List<Productos>>(apiString);
            }
            return View(vm);
        }
        //public async Task<IActionResult> IndexPaginado(int page = 1, int pageSize = 12)
        //{
        //    var productos = await _productosService.GetProductosRango(page, pageSize);
        //    return View(productos);
        //}

        public async Task<IActionResult> Filtros()
        {
            IndexVM vm = new IndexVM();
            string apiString;
            using (HttpResponseMessage response = await conexion.GetAsync("categorias/categorias"))
            {
                apiString = await response.Content.ReadAsStringAsync();
                vm.categorias = JsonConvert.DeserializeObject<List<Categorias>>(apiString);
            }
            using (HttpResponseMessage response = await conexion.GetAsync("productos/getproductos"))
            {
                apiString = await response.Content.ReadAsStringAsync();
                vm.productos = JsonConvert.DeserializeObject<List<Productos>>(apiString);
            }
            using (HttpResponseMessage response = await conexion.GetAsync("productos/GetProductosRango/1/12"))
            {
                apiString = await response.Content.ReadAsStringAsync();
                vm.productos = JsonConvert.DeserializeObject<List<Productos>>(apiString);
            }
            return View(vm);
        }

        public async Task<IActionResult> Login()
        {

            return View();
        }

		public async Task<IActionResult> Paginar()
		{

			return View();
		}



	}
}