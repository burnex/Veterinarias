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
    public class PersonasController : Controller
    {
        //Conexion a la base de datos
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PersonasController(DataContext dataContext, IWebHostEnvironment webHostEnvironment)
        {
            _context = dataContext;
            _webHostEnvironment = webHostEnvironment;
        }

        //INDEX PERSONAS 
        [HttpGet]
        public IActionResult Index()
        {
            var personas = _context.PR_PERSONAS_S01.FromSqlRaw("exec PR_PERSONAS_S01");
            return View(personas);
        }

        //PARA ACTIVAR Y DESACTIVAR PERSONAS
        public async Task<IActionResult> Activar(int id)
        {
            var personas = await _context.Personas.FindAsync(id);
            personas.Estado = true; // Establecer el estado como activado
            _context.Update(personas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Anular ANIMALES method GET
        public async Task<IActionResult> Anular(int id)
        {
            var personas = await _context.Personas.FindAsync(id);
            personas.Estado = false; // Establecer el estado como anulado
            _context.Update(personas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
    }
