using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Veterinarias.Modelos;

namespace Veterinarias.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Animal> Animales { get; set; }
        public DbSet<Raza> Razas { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Veterinario> Veterinarios { get; set; }
    }
}
