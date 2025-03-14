using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobranzaSP.Modelos
{
    internal class PdfModuloEquipo
    {
        public string Serie { get; set; }
        public string Cliente { get; set; }
        public string Folio { get; set; }
        public string Estado { get; set; }
        public string Observacion { get; set; }

        public DateTime Fecha { get; set; }

        public int Contador { get; set; }

        public string Clave { get; set; }

    }
}
