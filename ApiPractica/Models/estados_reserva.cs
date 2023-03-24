using System.ComponentModel.DataAnnotations;

namespace ApiPractica.Models
{
    public class estados_reserva
    {
        [Key]
        public int estado_res_id { get; set; }
        public string? estado { get; set; }

        public char? en_uso { get; set; }
    }
}
