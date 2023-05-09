using Microsoft.AspNetCore.Mvc;
using Veterinarias.Data;
using Veterinarias.Modelos;

namespace Veterinarias.Controllers
{
    public class AnimalesController : Controller
    {
        private readonly DataContext _context;

        public AnimalesController(DataContext dataContext)
        {
            _context = dataContext;
        }
        public IActionResult Index()
        {
            var animales = _context.Animales.ToList();
            return View(animales);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        public async Task<IActionResult> Create(Animal model)
        {
            await _context.AddAsync(model); //(Insertar
            await _context.SaveChangesAsync(); //Commit a la base de datos 
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int id)
        {
            var animal = await _context.Animales.FindAsync(id);
            return View(animal);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Animal model)
        {
            var modelOld = await _context.Animales.FindAsync(model.Id);
            modelOld.Nombre = model.Nombre;
            _context.Update(modelOld);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            var animal = await _context.Animales.FindAsync(id);
            _context.Remove(animal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
