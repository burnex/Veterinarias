namespace Veterinarias.Modelos
{
    public class Personas
    {
        public int Id { get; set; }
        public string TipoDocumento { get; set; } = string.Empty;
        public string NumeroDocumento { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Foto { get; set; } = string.Empty;
        public DateTime FechaNacimiento = DateTime.Now.Date;
        public string Sexo { get; set; } = string.Empty;
        public bool Estado { get; set; }

    }
}
