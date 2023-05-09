using System.ComponentModel.DataAnnotations;

namespace Veterinarias.Modelos
{
    public class Animal
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; } = string.Empty;
        public bool Estado { get; set; }
    }
}
