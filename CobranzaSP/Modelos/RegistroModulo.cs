using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobranzaSP.Modelos
{
    public class RegistroModulo:ModuloRicoh
    {
        public int IdRegistroModulo { get; set; }
        public string Serie { get; set; }
        public string Reporte { get; set; }
        public string Estado { get; set; }

        public int Paginas { get; set; }

        public string Observacion { get; set; }

        public string ClaveAnterior { get; set; }

    }
}
