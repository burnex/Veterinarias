using System.ComponentModel.DataAnnotations;

namespace Veterinarias.Modelos
{
    public class Raza
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; } = string.Empty;
        public bool Estado { get; set; }
        public int IdAnimal { get; set; }
    }
}
