using Microsoft.AspNetCore.Mvc;

namespace Veterinarias.Controllers
{
    public class VeterinariosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
