using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobranzaSP.Modelos
{
    public class ReporteRegistroInventario:Reporte
    {
        public string Cliente { get; set; }
        public string Marca { get; set; }
    }
}
