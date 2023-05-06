using Microsoft.AspNetCore.Mvc;

namespace Veterinarias.Controllers
{
    public class BandejaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
