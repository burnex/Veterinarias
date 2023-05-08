using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrabajadoresPrueba.Models;
using Veterinarias.Modelos;
using Veterinarias.Models;

namespace Veterinarias.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Animales> Animales { get; set; }
        public DbSet<Mascotas> Mascotas { get; set; }
        public DbSet<Personas> Personas { get; set; }
        public DbSet<Razas> Razas { get; set; }
        public DbSet<Veterinarios> Veterinarios { get; set; }
        public DbSet<TiposDocumentos> TiposDocumentos { get; set; }
        public DbSet<Sexo> Sexo { get; set; }

    }
}
