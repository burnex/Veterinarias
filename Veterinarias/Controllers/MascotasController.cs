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
        public async Task<IActionResult> BuscarPersonas(string busqueda)
        {
            var personas = await _context.Personas.Where(t => t.Nombres.Contains(busqueda)).ToListAsync();
            return PartialView(personas);
        }

        //VISUALIZACION INDEX
        [HttpGet]
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

        //CREAR NUEVO REGISTRO
        [HttpPost]
        public async Task<IActionResult> Create(Mascotas model)
        {
            model.Estado = "Regis"; // Asignar el estado 'Regis' al modelo

            //PARA CREAR FOTO
            var prueba1 = model.FotoIFormFile;

            if (model.FotoIFormFile != null)
            {
                model.Foto = await CargarDocumento0(model.FotoIFormFile, "MascotasFoto");
            }

            _context.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //PARA CARGAR FOTO
        private async Task<string> CargarDocumento0(IFormFile FotoIFormFile, string ruta)
        {
            var guid = Guid.NewGuid().ToString();
            var fileName = guid + Path.GetExtension(FotoIFormFile.FileName);
            //Obtengo extension del documento
            var carga1 = Path.Combine(_webHostEnvironment.WebRootPath, "images", ruta);
            /*var carga = Path.Combine(_webHostEnvironment.WebRootPath, string.Format("images\\{0}", ruta));*/
            using (var fileStream = new FileStream(Path.Combine(carga1, fileName), FileMode.Create))
            {
                await FotoIFormFile.CopyToAsync(fileStream);
            }
            return string.Format("/images/{0}/{1}", ruta, fileName);
        }

        //Edit
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _context.Mascotas.FindAsync(id);

            var Animales = await _context.Animales.ToListAsync();
            ViewBag.IdAnimal = new SelectList(Animales, "Id", "Nombre", model.IdAnimal);

            var Razas = await _context.Razas.Where(t => t.IdAnimal.Equals(model.IdAnimal)).ToListAsync();
            ViewBag.IdRaza = new SelectList(Razas, "Id", "Nombre", model.IdRaza);

            /*
             ViewBag de Departamento
             ViewBag de Tipos de documento
            */

            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Mascotas model)
        {
            var modelOld = await _context.Mascotas.FindAsync(model.Id);

            modelOld.IdAnimal = model.IdAnimal; // Actualizar el campo IdAnimal
            modelOld.IdRaza = model.IdRaza; // Actualizar el campo IdRaza
            modelOld.Nombres = model.Nombres; // Actualizar el campo Nombres
            modelOld.FechaNacimiento = model.FechaNacimiento; // Actualizar el campo FechaNacimiento
            modelOld.Color = model.Color; // Actualizar el campo Color
            modelOld.IdPersona = model.IdPersona; // Actualizar el campo IdPersona

            // Actualizar imagen si se proporciona un nuevo archivo
            if (model.FotoIFormFile != null)
            {
                // Eliminar la imagen anterior si existe
                if (!string.IsNullOrEmpty(modelOld.Foto))
                {
                    var FotoPath = Path.Combine(_webHostEnvironment.WebRootPath, modelOld.Foto.TrimStart('/'));
                    if (System.IO.File.Exists(FotoPath))
                    {
                        System.IO.File.Delete(FotoPath);
                    }
                }

                // Cargar la nueva foto
                modelOld.Foto = await CargarDocumento0(model.FotoIFormFile, "MascotasFoto");
            }

            _context.Update(modelOld);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
















    }
}
