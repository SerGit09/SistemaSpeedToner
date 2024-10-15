using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobranzaSP.Modelos
{
    public class ReporteRegistroParte:Reporte
    {
        public string Serie { get; set; }
        public string ModeloParteRicoh { get; set; }
        public string Parte { get; set; }
    }
}
