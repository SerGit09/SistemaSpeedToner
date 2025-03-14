using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobranzaSP.Modelos
{
    public class PdfEquipoRentado:Equipo
    {
        public string Cliente { get; set; }

        public string TipoRenta { get; set; }
    }
}
