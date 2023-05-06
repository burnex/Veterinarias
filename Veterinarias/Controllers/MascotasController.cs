using Microsoft.AspNetCore.Mvc;

namespace Veterinarias.Controllers
{
    public class MascotasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
