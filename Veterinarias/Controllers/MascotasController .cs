using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrabajadoresPrueba.Models;
using Veterinarias.Data;
using Veterinarias.Modelos;
using Veterinarias.Models;

namespace Veterinarias.Controllers
{
    public class MascotasController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MascotasController(DataContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;   
        }
        public async Task<IActionResult> BuscarDueño(string busqueda)
        {
            var dueños = await _context.Personas.Where(t => t.Nombres.Contains(busqueda)).ToListAsync();
            return PartialView(dueños);
        }
        public IActionResult Index()
        {
            //var listarVeterinarios = _context.Veterinarios.ToList();
            var listarVeterinarios = _context.PR_MASCOTAS_MG01.FromSqlRaw("exec PR_MASCOTAS_MG01");

            return View(listarVeterinarios);
        }
        public async Task<IActionResult> Create()
        {
            var listarMascotas = new Mascotas { FechaNacimiento = DateTime.Now.Date , FechaRegistro = DateTime.Now.Date};
            
            var Animal = await _context.Animales.ToListAsync();
            ViewData["IdAnimal"] = new SelectList(Animal, "Id", "Nombre");
            
            return PartialView(listarMascotas);
        }
        public async Task<JsonResult> CargarRaza(int id)
        {
            var listado = await _context.Razas.Where(t => t.IdAnimal.Equals(id)).ToListAsync();
            return Json(listado);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Mascotas Model)
        {

            if (Model.FotoIFormFile != null)
            {
                Model.Foto = await CargarDocumento(Model.FotoIFormFile, "FotoMascotas");
            }

            await _context.AddAsync(Model);   //Insert into
            await _context.SaveChangesAsync();     //Commit a la bd
            return RedirectToAction(nameof(Index));
        }
        private async Task<string> CargarDocumento(IFormFile fichaIFormFile, string ruta)
        {
            var guid = Guid.NewGuid().ToString(); //Obtiene un texo aleatorio  "xSDfs-asdf-safsd-sadf "
            var fileName = guid + Path.GetExtension(fichaIFormFile.FileName); //Obtengo extension del documento
            var carga1 = Path.Combine(_webHostEnvironment.WebRootPath, "images", ruta);
            var carga = Path.Combine(_webHostEnvironment.WebRootPath, string.Format("images/{0}", ruta));
            using (var fileStream = new FileStream(Path.Combine(carga, fileName), FileMode.Create))
            {
                await fichaIFormFile.CopyToAsync(fileStream);
            }
            return string.Format("/images/{0}/{1}", ruta, fileName);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var mascota = await _context.Mascotas.FindAsync(id);
            return PartialView(mascota);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Mascotas model)
        {
            var modelOld = await _context.Mascotas.FindAsync(model.Id);
            modelOld.IdAnimal = model.IdAnimal;
            modelOld.IdRaza = model.IdRaza;
            modelOld.Nombres = model.Nombres;
            modelOld.FechaNacimiento = model.FechaNacimiento;
            modelOld.Color = model.Color;
            modelOld.FotoIFormFile = model.FotoIFormFile;
            modelOld.IdPersona = model.IdPersona;
            _context.Update(modelOld);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
