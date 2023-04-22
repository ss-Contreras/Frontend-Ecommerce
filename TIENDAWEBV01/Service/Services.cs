using TIENDAWEBV01.Models;

namespace TIENDAWEBV01.Service
{
    public class Services
    {
        private IConfiguration _configuration;
        private readonly string _host;
        static HttpClient client = new HttpClient();
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Services(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _host = _configuration.GetSection("ConnectionStrings").GetSection("ApiRifa").Value; ;
        }
        public CookiesModel VerificarCookie()
        {
            CookiesModel cookies = new CookiesModel();
            var cookieOptions = new CookieOptions();
            cookieOptions.Secure = true;
            cookieOptions.Expires = DateTime.Now.AddDays(100);
            cookieOptions.Path = "/";

            if (!_httpContextAccessor.HttpContext.Request.Cookies.ContainsKey("IdUsuario"))
            {
                var año = DateTime.Now.Year.ToString();
                var mes = DateTime.Now.Month.ToString();
                var dia = DateTime.Now.Day.ToString();
                var hora = DateTime.Now.Hour.ToString();
                var minuto = DateTime.Now.Minute.ToString();
                var segundo = DateTime.Now.Second.ToString();

                string fecha = año + mes + dia + hora + minuto + segundo;

                _httpContextAccessor.HttpContext.Response.Cookies.Append("IdUsuario", fecha);
                cookies.IdUsuario = fecha;
            }
            else
            {
                cookies.IdUsuario = _httpContextAccessor.HttpContext.Request.Cookies["IdUsuario"];
            }



            return cookies;
        }
    }

}
