using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veterinarias.Data;

namespace Veterinarias.Controllers
{
    public class BandejaController : Controller
    {
        //Conexion a la base de datos
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BandejaController(DataContext dataContext, IWebHostEnvironment webHostEnvironment)
        {
            _context = dataContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            //var bandeja = _context.PR_MASCOTAS_S01.FromSqlRaw("exec PR_MASCOTAS_S01");
            //return View(bandeja);
            var bandeja = _context.Mascotas.FromSqlRaw("EXEC PR_MASCOTAS_S01").AsEnumerable().ToList();
            return View(bandeja);
        } 

        //PARA APROBAR Y RECHAZAR REGISTROS EN BANDEJA MASCOTAS
        public async Task<IActionResult> Aprobar(int id)
        {
            var bandeja = await _context.Mascotas.FindAsync(id);
            bandeja.Estado = "Aprobado"; // Establecer el estado como aprobado
            _context.Update(bandeja);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Anular ANIMALES method GET
        public async Task<IActionResult> Rechazar(int id)
        {
            var bandeja = await _context.Mascotas.FindAsync(id);
            bandeja.Estado = "Rechazado"; // Establecer el estado como rechazado
            _context.Update(bandeja);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
