using Microsoft.AspNetCore.Mvc;

namespace Veterinarias.Controllers
{
    public class PersonasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
