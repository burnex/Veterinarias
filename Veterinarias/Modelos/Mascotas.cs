using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veterinarias.Modelos
{
    public class Mascotas
    {
        [Key]
        public int Id { get; set; }
        [NotMapped]
        [DisplayName("Animal")]
        public int IdAnimal { get; set; }  
        [DisplayName("Raza")]
        public int IdRaza { get; set; }
        [Required]
        [DisplayName("Nombres y Apellidos")]
        public string Nombres { get; set; } = string.Empty;
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }
        public string Color { get; set; } = string.Empty;
        public int IdPersona { get; set; }
        public string? Foto { get; set; } = string.Empty;
        [NotMapped]
        [DisplayName("Foto")]
        public IFormFile FotoIFormFile { get; set; }
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }

    }
}
