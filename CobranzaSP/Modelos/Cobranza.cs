using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobranzaSP.Modelos
{
    public class Cobranza:Factura
    {
        public string FormaPago { get; set; }

        public int DiasCredito { get; set; }
        public string Observaciones { get; set; }

        public string PromesaPago { get; set; }

        public DateTime? FechaPromesaPago { get; set; }
    }
}
