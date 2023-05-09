using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veterinarias.Data;
using Veterinarias.Modelos;

namespace Veterinarias.Controllers
{
    public class RazasController : Controller
    {
        private readonly DataContext _context;

        public RazasController(DataContext dataContext)
        {
            _context = dataContext;
        }

        //GET
        public async Task<IActionResult> Index(int id) //
        {
            var razas = await _context.Razas.Where(t => t.IdAnimal.Equals(id)).ToListAsync(); //dentro de la vista de animales
            var modelAnimal = await _context.Animales.FindAsync(id);
            ViewBag.Animal = modelAnimal;
            return View(razas); //vista de razas
        }

        //GET 
        public IActionResult Create(int idAnimal) 
        {
            var raza = new Raza { IdAnimal = idAnimal };
            return View(raza);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Raza model)
        {
            await _context.Razas.AddAsync(model); 
            await _context.SaveChangesAsync(); 
            return RedirectToAction("Index", new { id = model.IdAnimal });
        }

        //GET 
        public async Task<IActionResult> Edit(int id) //abrir la pantalla
        {
            var raza = await _context.Razas.FindAsync(id); 
            return View(raza); //retornar vista
        }

        [HttpPost] //-> hacer el cambio 
        public async Task<IActionResult> Edit(Raza model)
        {
            var modelOld = await _context.Razas.FindAsync(model.Id);
            modelOld.Nombre = model.Nombre;
            _context.Update(modelOld); 
            await _context.SaveChangesAsync(); 
            return RedirectToAction("Index", new { id = model.IdAnimal });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var idAnimal = 0;
            var raza = await _context.Razas.FindAsync(id);
            if (raza != null)
            {
                idAnimal = raza.IdAnimal;
                _context.Remove(raza);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", new { id = idAnimal });

        }
    }
}