using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Veterinarias.Data;
using Veterinarias.Modelos;

namespace Veterinarias.Controllers
{
    public class VisitasController : Controller
    {
        //Conexion a la base de datos
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public VisitasController(DataContext dataContext, IWebHostEnvironment webHostEnvironment)
        {
            _context = dataContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {

            var visitas = _context.PR_VISITAS_S01.FromSqlRaw("exec PR_VISITAS_S01");
            return View(visitas);
        }
    }
}
