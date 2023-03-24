using System.ComponentModel.DataAnnotations;

namespace ApiPractica.Models
{
    public class marcas
    {
        [Key]
        public int id_marcas { get; set; }
        public string? nombre_marca { get; set; }
        public char estados { get; set; }

    }
}
