using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Veterinarias.Modelos
{
    public class Razas
    {
        [Key]
        public int Id { get; set; }
        public int IdAnimal { get; set; }
        [Required]
        [DisplayName("Nombre Raza")]
        public string Nombre { get; set; } = string.Empty;
        public bool Estado { get; set; }
    }
}
