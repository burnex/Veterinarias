namespace Veterinarias.Modelos
{
    public class Visitas
    {
        public int Id { get; set; }
        public int IdMascota { get; set; }
        public DateTime FechaVisita { get; set; } = DateTime.Now;
        public int IdVeterinario { get; set; }
        public DateTime HoraIngreso { get; set; }
        public string Notas { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

    }
}
