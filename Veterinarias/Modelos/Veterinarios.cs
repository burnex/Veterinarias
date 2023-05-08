using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veterinarias.Modelos
{
    public class Veterinarios
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Tipo Documento")]
        public string TipoDocumento { get; set; } = string.Empty;
        [Required]
        [DisplayName("Nro Documento")]
        public string NumeroDocumento { get; set; } = string.Empty;
        [Required]
        [DisplayName("Nombres y Apellidos")]
        public string Nombres { get; set; } = string.Empty;
        public string? Foto { get; set; } = string.Empty;
        [NotMapped]
        [DisplayName("Foto")]
        public IFormFile FotoIFormFile { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }
        public string Sexo { get; set; } = string.Empty;
        public string Colegio { get; set; } = string.Empty;
        public bool Estado { get; set; }
        public string SrcFoto
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
