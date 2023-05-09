using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Veterinarias.Data;
using Veterinarias.Modelos;
using Veterinarias.Models;

namespace Veterinarias.Controllers
{
    public class PersonasController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PersonasController(DataContext dataContext, IWebHostEnvironment webHostEnvironment)
        {
            _context = dataContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var listado = _context.Personas.ToList();
            return View(listado);
        }

        public async Task<IActionResult> Create()
        {
            var TiposDocumentos = new List<TiposDocumentos>();
            TiposDocumentos.Add(new TiposDocumentos { TipoDocumento = "DNI", NombreDocumento = "DNI" });
            TiposDocumentos.Add(new TiposDocumentos { TipoDocumento = "CXE", NombreDocumento = "Carnet Extranjeria" });
            TiposDocumentos.Add(new TiposDocumentos { TipoDocumento = "PAS", NombreDocumento = "Pasaporte" });

            ViewBag.TipoDocumento = new SelectList(TiposDocumentos, "TipoDocumento", "NombreDocumento");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Persona model)
        {

            var prueba2 = model.FotoIFormFile;
            if (model.FotoIFormFile != null)
            {
                model.Foto = await CargarDocumento2(model.FotoIFormFile, "Foto");
            }

            _context.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<String> CargarDocumento2(IFormFile fotoIformFile, string ruta)
        {
            var guid = Guid.NewGuid().ToString();
            var fileName = guid + Path.GetExtension(fotoIformFile.FileName);
            var carga1 = Path.Combine(_webHostEnvironment.WebRootPath, "images", ruta);
            //var carga = Path.Combine(_webHostEnvironment.WebRootPath, string.Format("Imagenes/{0}", ruta));
            using (var fileStream = new FileStream(Path.Combine(carga1, fileName), FileMode.Create))
            {
                await fotoIformFile.CopyToAsync(fileStream);
            }
            return string.Format("/images/{0}/{1}", ruta, fileName);
        }
    }
}
