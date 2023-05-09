using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Veterinarias.Modelos
{
    public class PR_MASCOTAS_MG01
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Animal")]
        public string NombreAnimal { get; set; } = string.Empty;
        [Required]
        [DisplayName("Raza")]
        public string NombreRaza { get; set; } = string.Empty;
        [Required]
        public string Nombres { get; set; } = string.Empty;

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public string Color { get; set; } = string.Empty;
        public string? Foto { get; set; } = string.Empty;
        [Required]
        [DisplayName("Dueño")]
        public string NombreDueño { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Date)]
        public DateTime FechaRegistro { get; set; }
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
