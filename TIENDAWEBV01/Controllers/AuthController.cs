using Microsoft.AspNetCore.Mvc;

namespace TIENDAWEBV01.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

    }
}
