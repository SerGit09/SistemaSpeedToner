using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobranzaSP.Modelos
{
    public class Fusor
    {
        public int IdFusor { get; set; }

        public string SerieO { get; set; }

        public string SerieS { get; set; }

        public string Estado { get; set; }
        public string NumeroFactura { get; set; }
        public string Proveedor { get; set; }
        public int IdProveedor { get; set; }

        public int DiasGarantia { get; set; }

        public int IdCartucho { get; set; }
        public double Precio { get; set; }
        public int IdTipoFusor { get; set; }
        public DateTime FechaLlegada { get; set; }
        public DateTime FechaFactura { get; set; }

    }
}
