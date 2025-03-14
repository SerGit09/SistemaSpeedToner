using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobranzaSP.Modelos
{
    public class DocumentoCobranza
    {
        public int IdCliente { get; set; }
        public double Cantidad { get; set; }

        public DateTime Fecha { get; set; }

        public DateTime FechaPago { get; set; }

    }
}
