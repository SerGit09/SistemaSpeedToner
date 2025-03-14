using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobranzaSP.Modelos
{
    public class ReporteRegistroInventarioToners:Reporte
    {
        public string Marca { get; set; }

        public int IdTipoArticulo { get; set; }
    }
}
