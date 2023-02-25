using Microsoft.EntityFrameworkCore;

namespace ApiPractica.Models
{
    public class equiposContext : DbContext
    {
        public equiposContext(DbContextOptions <equiposContext> options) : base(options) {
        }
        public DbSet<equipos> Equipos { get; set; }
    }
}
