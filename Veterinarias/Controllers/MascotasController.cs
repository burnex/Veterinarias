using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veterinarias.Data;

namespace Veterinarias.Controllers
{
    public class MascotasController : Controller
    {
        //Conexion a la base de datos
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MascotasController(DataContext dataContext, IWebHostEnvironment webHostEnvironment)
        {
            _context = dataContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var mascotas = _context.PR_MASCOTAS_S01.FromSqlRaw("exec PR_MASCOTAS_S01");
            return View(mascotas);
        }
    }
}
