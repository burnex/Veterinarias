using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veterinarias.Data;
using Veterinarias.Modelos;

namespace Veterinarias.Controllers
{
    public class RazasController : Controller
    {
        private readonly DataContext _context;
        public RazasController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int id)
        {
            var razas = await _context.Razas.Where(t => t.IdAnimal.Equals(id)).ToListAsync();
            var modelAnimal = await _context.Animales.FindAsync(id);
            ViewBag.Animales = modelAnimal;
            return View(razas);
        }
        public IActionResult Create(int idAnimal)
        {
            var raza = new Razas { IdAnimal = idAnimal };
            return PartialView(raza);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Razas Model)
        {
            await _context.AddAsync(Model);   //Insert into
            await _context.SaveChangesAsync();     //Commit a la bd
            return RedirectToAction("Index", new { id = Model.IdAnimal });
        }
        public async Task<IActionResult> Edit(int id)
        {
            var razas = await _context.Razas.FindAsync(id);
            return PartialView(razas);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Razas model)
        {
            var modelOld = await _context.Razas.FindAsync(model.Id);
            modelOld.Nombre = model.Nombre;
            _context.Update(modelOld);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { id = model.IdAnimal });
        }
    }
}
