using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobranzaSP.Modelos
{
    public class PdfTipoFactura
    {
        public int IdTipoFactura { get; set; }
        public double Cantidad { get; set; }
        public string Cliente { get; set; }
    }
}
