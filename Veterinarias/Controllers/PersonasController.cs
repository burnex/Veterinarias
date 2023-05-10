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
    public class PersonasController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PersonasController(DataContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;   
        }
        public IActionResult Index()
        {
            //var listarPersonas = _context.Personas.ToList();
            var listarPersonas = _context.PR_PERSONAS_MG01.FromSqlRaw("exec PR_PERSONAS_MG01");

            return View(listarPersonas);
        }
        public IActionResult Create()
        {
            var TiposDocumentos = new List<TiposDocumentos>();
            TiposDocumentos.Add(new TiposDocumentos { TipoDocumento = "DNI", NombreDocumento = "DNI" });
            TiposDocumentos.Add(new TiposDocumentos { TipoDocumento = "CXE", NombreDocumento = "Carnet Extranjería" });
            TiposDocumentos.Add(new TiposDocumentos { TipoDocumento = "PAS", NombreDocumento = "Pasaporte" });

            ViewData["TipoDocumento"] = new SelectList(TiposDocumentos, "TipoDocumento", "NombreDocumento");

            var Sexo = new List<Sexo>();
            Sexo.Add(new Models.Sexo {SexoItem = "F", SexoDesc = "Femenino" });
            Sexo.Add(new Models.Sexo {SexoItem = "M", SexoDesc = "Masculino" });

            ViewData["Sexo"] = new SelectList(Sexo, "SexoItem", "SexoDesc");

            var persona = new Personas { FechaNacimiento = DateTime.Now.Date };
            return PartialView(persona);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Personas Model)
        {
            //Model.Estado = true;

            if (Model.FotoIFormFile != null)
            {
                Model.Foto = await CargarDocumento(Model.FotoIFormFile, "FotoPersonas");
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
            var personas = await _context.Personas.FindAsync(id);

            var TiposDocumentos = new List<TiposDocumentos>();
            TiposDocumentos.Add(new TiposDocumentos { TipoDocumento = "DNI", NombreDocumento = "DNI" });
            TiposDocumentos.Add(new TiposDocumentos { TipoDocumento = "CXE", NombreDocumento = "Carnet Extranjería" });
            TiposDocumentos.Add(new TiposDocumentos { TipoDocumento = "PAS", NombreDocumento = "Pasaporte" });

            //personas.FotoIFormFile = personas.Foto;

            ViewData["TipoDocumento"] = new SelectList(TiposDocumentos, "TipoDocumento", "NombreDocumento", personas.TipoDocumento);

            var Sexo = new List<Sexo>();
            Sexo.Add(new Models.Sexo {SexoItem = "F", SexoDesc = "Femenino" });
            Sexo.Add(new Models.Sexo {SexoItem = "M", SexoDesc = "Masculino" });

            ViewData["Sexo"] = new SelectList(Sexo, "SexoItem", "SexoDesc", personas.Sexo);

            return PartialView(personas);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Personas model)
        {
            var modelOld = await _context.Personas.FindAsync(model.Id);
            modelOld.TipoDocumento = model.TipoDocumento;
            modelOld.NumeroDocumento = model.NumeroDocumento;
            modelOld.Nombres = model.Nombres;
            modelOld.FotoIFormFile   = model.FotoIFormFile;
            modelOld.FechaNacimiento = model.FechaNacimiento;
            modelOld.Edad = model.Edad;
            modelOld.Sexo = model.Sexo;
            _context.Update(modelOld);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Activar(int id)
        {
            var personas = await _context.Personas.FindAsync(id);
            personas.Estado = true; 
            _context.Update(personas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Anular(int id)
        {
            var personas = await _context.Personas.FindAsync(id);
            personas.Estado = false;
            _context.Update(personas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
