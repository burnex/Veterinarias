using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Veterinarias.Data;
using Veterinarias.Modelos;

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

        //BUSCAR TRABAJADOR
        public async Task<IActionResult> BuscarPersona(string busqueda)
        {
            var personas = await _context.Personas.Where(t => t.Nombres.Contains(busqueda)).ToListAsync();
            return PartialView(personas);
        }

        //VISUALIZACION INDEX
        public IActionResult Index()
        {
            var mascotas = _context.PR_MASCOTAS_S01.FromSqlRaw("exec PR_MASCOTAS_S01");
            return View(mascotas);
        }

        //CREATE ACTUALIZADO
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var Animales = await _context.Animales.ToListAsync();
            ViewData["IdAnimal"] = new SelectList(Animales, "Id", "Nombre");

            var mascotas = new Mascotas { FechaNacimiento = DateTime.Now.Date };
            return PartialView(mascotas);

        }

        [HttpGet]
        public async Task<JsonResult> CargarRazas(int id)
        {
            var razas = await _context.Razas.Where(r => r.IdAnimal == id).ToListAsync();
            return Json(razas);
        }

    }
}
