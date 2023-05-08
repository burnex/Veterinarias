using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrabajadoresPrueba.Models;
using Veterinarias.Data;
using Veterinarias.Modelos;
using Veterinarias.Models;

namespace Veterinarias.Controllers
{
    public class VeterinariosController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public VeterinariosController(DataContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;   
        }
        public IActionResult Index()
        {
            var listarVeterinarios = _context.Veterinarios.ToList();
            return View(listarVeterinarios);
        }
        public IActionResult Create()
        {
            var TiposDocumentos = new List<TiposDocumentos>();
            TiposDocumentos.Add(new TiposDocumentos { TipoDocumento = "DNI", NombreDocumento = "DNI" });
            TiposDocumentos.Add(new TiposDocumentos { TipoDocumento = "CXE", NombreDocumento = "Carnet Extranjería" });
            TiposDocumentos.Add(new TiposDocumentos { TipoDocumento = "PAS", NombreDocumento = "Pasaporte" });

            ViewData["TipoDocumento"] = new SelectList(TiposDocumentos, "TipoDocumento", "NombreDocumento");

            var Sexo = new List<Sexo>();
            Sexo.Add(new Models.Sexo { SexoItem = "F", SexoDescripcion = "Femenino" });
            Sexo.Add(new Models.Sexo { SexoItem = "M", SexoDescripcion = "Masculino" });

            ViewData["Sexo"] = new SelectList(Sexo, "SexoItem", "SexoDescripcion");

            var veterinario = new Veterinarios { FechaNacimiento = DateTime.Now.Date };
            return PartialView(veterinario);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Veterinarios Model)
        {

            if (Model.FotoIFormFile != null)
            {
                Model.Foto = await CargarDocumento(Model.FotoIFormFile, "FotoVeterinarios");
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
            var veterinario = await _context.Veterinarios.FindAsync(id);

            var TiposDocumentos = new List<TiposDocumentos>();
            TiposDocumentos.Add(new TiposDocumentos { TipoDocumento = "DNI", NombreDocumento = "DNI" });
            TiposDocumentos.Add(new TiposDocumentos { TipoDocumento = "CXE", NombreDocumento = "Carnet Extranjería" });
            TiposDocumentos.Add(new TiposDocumentos { TipoDocumento = "PAS", NombreDocumento = "Pasaporte" });

            ViewData["TipoDocumento"] = new SelectList(TiposDocumentos, "TipoDocumento", "NombreDocumento", veterinario.TipoDocumento);

            var Sexo = new List<Sexo>();
            Sexo.Add(new Models.Sexo { SexoItem = "F", SexoDescripcion = "Femenino" });
            Sexo.Add(new Models.Sexo { SexoItem = "M", SexoDescripcion = "Masculino" });

            ViewData["Sexo"] = new SelectList(Sexo, "SexoItem", "SexoDescripcion", veterinario.Sexo);

            return PartialView(veterinario);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Veterinarios model)
        {
            var modelOld = await _context.Veterinarios.FindAsync(model.Id);
            modelOld.TipoDocumento = model.TipoDocumento;
            modelOld.NumeroDocumento = model.NumeroDocumento;
            modelOld.Nombres = model.Nombres;
            modelOld.FotoIFormFile   = model.FotoIFormFile;
            modelOld.FechaNacimiento = model.FechaNacimiento;
            //modelOld.Edad = model.Edad;
            modelOld.Sexo = model.Sexo;
            modelOld.Colegio = model.Colegio;
            _context.Update(modelOld);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
