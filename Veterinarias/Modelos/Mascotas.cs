namespace Veterinarias.Modelos
{
    public class Mascotas
    {
        public int Id { get; set; }
        public int IdRaza { get; set; }
        public string Nombres { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; } = DateTime.Now.Date;
        public string Color { get; set; } = string.Empty;
        public int IdPersona { get; set; }
        public string Foto { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; } = DateTime.Now.Date;
    }
}
