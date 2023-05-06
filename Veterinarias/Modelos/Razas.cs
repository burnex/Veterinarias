namespace Veterinarias.Modelos
{
    public class Razas
    {
        public int Id { get; set; }
        public int IdAnimal { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public bool Estado { get; set; }
    }
}
