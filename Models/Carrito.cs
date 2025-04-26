using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appweb1.Modelos
{
    public class Carrito
    {
        public List<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();
    }
}