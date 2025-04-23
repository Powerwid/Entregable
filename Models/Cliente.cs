using System.ComponentModel.DataAnnotations;

namespace appweb1.Models
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }
        public String Nombres { get; set; }
        public String Apellidos { get; set; }
        public String Dni { get; set; }    
    }
}