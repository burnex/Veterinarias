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

        //INDEX VETERINARIOS 
        [HttpGet]
        public IActionResult Index()
        {
            var veterinarios = _context.PR_VETERINARIOS_S01.FromSqlRaw("exec PR_VETERINARIOS_S01");
            return View(veterinarios);
        }

        //PARA ACTIVAR Y DESACTIVAR VETERINARIOS
        public async Task<IActionResult> Activar(int id)
        {
            var veterinarios = await _context.Veterinarios.FindAsync(id);
            veterinarios.Estado = true; // Establecer el estado como activado
            _context.Update(veterinarios);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Anular ANIMALES method GET
        public async Task<IActionResult> Anular(int id)
        {
            var veterinarios = await _context.Veterinarios.FindAsync(id);
            veterinarios.Estado = false; // Establecer el estado como anulado
            _context.Update(veterinarios);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
