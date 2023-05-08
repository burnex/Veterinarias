using Microsoft.AspNetCore.Mvc;
using Veterinarias.Data;
using Veterinarias.Modelos;

namespace Veterinarias.Controllers
{
    public class AnimalesController : Controller
    {
        private readonly DataContext _context;
        public AnimalesController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var animales = _context.Animales.ToList();
            return View(animales);
        }
        public IActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Animales Model)
        {
            await _context.AddAsync(Model);   //Insert into
            await _context.SaveChangesAsync();     //Commit a la bd
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var animales = await _context.Animales.FindAsync(id);
            return PartialView(animales);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Animales model)
        {
            var modelOld = await _context.Animales.FindAsync(model.Id);
            modelOld.Nombre = model.Nombre;
            _context.Update(modelOld);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
