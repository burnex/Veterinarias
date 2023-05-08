using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using Veterinarias.Data;
using Veterinarias.Modelos;
using Veterinarias.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Veterinarias.Controllers
{
    public class VeterinariosController : Controller
    {
        //Conexion a la base de datos
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public VeterinariosController(DataContext dataContext, IWebHostEnvironment webHostEnvironment)
        {
            _context = dataContext;
            _webHostEnvironment = webHostEnvironment;
        }

        //index TRABAJADORES
        [HttpGet]
        public IActionResult Index()
        {

            var listado2 = _context.PR_VETERINARIOS_S01.FromSqlRaw("exec PR_VETERINARIOS_S01");
            return View(listado2);
        }


    }
}
