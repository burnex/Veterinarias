namespace Veterinarias.Modelos
{
    public class Veterinarios
    {
        public int Id { get; set; }
        public string TipoDocumento { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Foto { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; } = DateTime.Now.Date;
        public string Sexo { get; set; } = string.Empty;
        public string Colegio { get; set; } = string.Empty;
        public bool Estado { get; set; }
    }
}
