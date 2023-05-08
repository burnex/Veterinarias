using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veterinarias.Data;
using Veterinarias.Modelos;

namespace Veterinarias.Controllers
{
    public class RazasController : Controller
    {
        //conexion a la base de datos
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RazasController(DataContext dataContext, IWebHostEnvironment webHostEnvironment)
        {
            _context = dataContext;
            _webHostEnvironment = webHostEnvironment;
        }

        //TOMARLO COMO PRUEBA PARA CREAR PAGINA DENTRO DE OTRA

        //INDEX RAZAS GET : /<Controller>/
        //Index
        public async Task<IActionResult> Index(int id)
        {
            var razas = await _context.PR_RAZAS_S01.FromSqlRaw("EXECUTE dbo.PR_RAZAS_S01 @idAnimal={0}", id).ToListAsync();
            var modelAnimales = await _context.Animales.FindAsync(id);
            ViewBag.Animales = modelAnimales;
            return View(razas);
        }



        ////Create RAZAS
        public IActionResult Create(int idAnimal)
        {
            ViewBag.IdAnimal = idAnimal;
            return PartialView();
        }


        [HttpPost]
        public async Task<IActionResult> Create(int idAnimal, Razas nuevaRaza)
        {
            // Obtener el animal existente al que se agregará la nueva raza
            var animal = await _context.Animales.FindAsync(idAnimal);

            if (animal == null)
            {
                return NotFound();
            }

            // Establecer la relación entre la nueva raza y el animal correspondiente
            nuevaRaza.IdAnimal = animal.Id;

            // Agregar la nueva raza a la base de datos y guardar los cambios
            _context.Razas.Add(nuevaRaza);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { id = animal.Id });
        }




        ////EDIT RAZAS
        public async Task<IActionResult> EditAsync(int id)
        {
            var razas = await _context.Razas.FindAsync(id); //select * from ANIMALES where PK = id
            return PartialView(razas);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Razas raza)
        {
            if (id != raza.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(raza);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Razas.Any(r => r.Id == raza.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(raza);
        }



        //PARA ACTIVAR Y DESACTIVAR RAZAS
        public async Task<IActionResult> Activar(int id)
        {
            var idAnimales = 0;
            var raza = await _context.Razas.FindAsync(id);
            if (raza != null)
            {
                idAnimales = raza.IdAnimal; // Guardar el ID del animal antes de anular la raza
                raza.Estado = true; // Establecer el estado como activado
                _context.Update(raza);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index), new { id = idAnimales });

        }

        // Anular RAZAS method GET
        public async Task<IActionResult> Anular(int id)
        {
            var idAnimales = 0;
            var raza = await _context.Razas.FindAsync(id);
            if (raza != null)
            {
                idAnimales = raza.IdAnimal; // Guardar el ID del animal antes de anular la raza
                raza.Estado = false; // Establecer el estado como anulado
                _context.Update(raza);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index), new { id = idAnimales });

        }


    }
}