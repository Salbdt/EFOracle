using EFOracle.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFOracle.Data
{
    public class HRContext : DbContext
    {
        private readonly string _connectionString;

        public HRContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Inicializamos el motor de la BD elegida con la cadena de conexión
            optionsBuilder.UseOracle(_connectionString);
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }
    }
}
