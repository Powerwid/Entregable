using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace appweb1.Modelos
{
    public class Compra
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCompra { get; set; }
        public int VentaIdVenta { get; set; }
        public string TipoComprobante { get; set; }
        public int NoComprobante { get; set; }
        public DateTime FechaEmision { get; set; }
    }
}