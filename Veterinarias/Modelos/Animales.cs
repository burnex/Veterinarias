using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Veterinarias.Modelos
{
    public class Animales
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public bool Estado { get; set; }

    }
}
