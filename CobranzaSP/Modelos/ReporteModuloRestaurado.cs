using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobranzaSP.Modelos
{
    public class ReporteModuloRestaurado
    {
        public int IdReporte { get; set; }
        public string Clave { get; set; }
        public int IdModulo { get; set; }

        public DateTime Fecha { get; set; }

        public string FolioReporte { get; set; }
        public string ServicioRealizado { get; set; }
    }
}
