using Microsoft.AspNetCore.Mvc;

namespace Veterinarias.Controllers
{
    public class RazasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
