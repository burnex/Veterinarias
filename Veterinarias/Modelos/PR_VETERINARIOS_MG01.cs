using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Veterinarias.Modelos
{
    public class PR_VETERINARIOS_MG01
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

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; } = string.Empty;
        public string SexoDescripcion { get; set; } = string.Empty;
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
