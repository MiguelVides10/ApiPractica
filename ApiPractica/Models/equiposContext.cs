using Microsoft.EntityFrameworkCore;

namespace ApiPractica.Models
{
    public class equiposContext : DbContext
    {
        public equiposContext(DbContextOptions <equiposContext> options) : base(options) {
        }
        public DbSet<equipos> Equipos { get; set; }
        public DbSet<carreras> Carreras { get; set; }
        public DbSet<estados_equipo> Estados_Equipo { get; set; }
        public DbSet<estados_reserva> Estados_Reserva { get; set; }
        public DbSet<facultades> Facultades { get; set; }
        public DbSet<marcas> Marcas { get; set; }
        public DbSet<reservas> Reservas { get; set; }
        public DbSet<tipo_equipo> tipo_Equipo { get; set; }
        public DbSet<usuarios> Usuarios { get; set; }

    }
}
