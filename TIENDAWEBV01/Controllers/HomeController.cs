using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Drawing;
using TIENDAWEBV01.Models;
using TIENDAWEBV01.ViewModels;
using System.Net.Http;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIENDAWEBV01.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration _configuration;
        private readonly string _host;
        static HttpClient client = new HttpClient();
        static HttpClient conexion;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
            conexion = new HttpClient();
            conexion.BaseAddress = new Uri(_configuration.GetConnectionString("tiendaApi"));
        }

        public async Task <IActionResult> Index()
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

            //mostrar productos

            return View(vm);
        }

    }
}