using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veterinarias.Modelos
{
    public class Persona
    {
        public int Id { get; set; }
        [DisplayName("Tipo Documento")]
        public string TipoDocumento { get; set; } = string.Empty;
        [Required]
        [DisplayName("Nro. Documento")]
        public string NumeroDocumento { get; set; } = string.Empty;
        [DisplayName("Nombres")]
        public string Nombres { get; set; } = string.Empty;

        public DateTime FechaNacimiento { get; set;  }

        public string Sexo { get; set; }
        public bool Estado { get; set; }

        [DisplayName("Foto")]
        public string? Foto { get; set; }
        [NotMapped]
        public IFormFile FotoIFormFile { get; set; }

        public string FotoURL => Foto == null ? "" : Foto;
        public string FotoURL2
        {
            get
            {
                if (string.IsNullOrEmpty(Foto))
                {
                    return "";
                }
                else
                {
                    return $"https://localhost:7149/{Foto}";
                }
            }
        }
    }
}
