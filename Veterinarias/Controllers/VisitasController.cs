using Microsoft.AspNetCore.Mvc;

namespace Veterinarias.Controllers
{
    public class VisitasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
