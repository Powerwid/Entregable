using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace appweb1.Modelos
{
    public class Pago
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPago { get; set; }
        public int VentaIdVenta { get; set; }
        public string MetodoPago { get; set; }
        public decimal MontoPagado { get; set; }
        public DateTime FechaPago { get; set; }
    }
}