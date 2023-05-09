using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Veterinarias.Modelos
{
    public class Persona
    {
        public int Id { get; set; }
        [DisplayName("Tipo Documento")]
        public string TipoDocumento { get; set; } = string.Empty;
        [Required]
        [DisplayName("Nro. Documento")]
        public string NroDocumento { get; set; } = string.Empty;
        [DisplayName("Nombres")]
        public string Nombres { get; set; } = string.Empty;
        [DisplayName("Foto")]
        public string ? Foto { get; set; } 
        public DateTime FechaNacimiento { get; set;  }

        public string Sexo { get; set; }
        public bool Estado { get; set; }
    }
}
