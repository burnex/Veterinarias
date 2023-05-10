namespace Veterinarias.Modelos
{
    public class Animales
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public bool Estado { get; set; }
        public string EstadoIcon => Estado ? "bi bi-check-square-fill" : "bi bi-app";
    }
}
