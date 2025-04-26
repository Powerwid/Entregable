using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace appweb1.Modelos
{
    public class Comprobante
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdComprobante { get; set; }
        public int UsuarioIdUsuario { get; set; }
        public DateTime FechaCompra { get; set; }
        public decimal MontoTotal { get; set; }
        public string Estado { get; set; }
    }
}