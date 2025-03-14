using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobranzaSP.Modelos
{
    public class TotalesPorTipoFactura
    {
        public string Cliente {  get; set; }
        public double TotalToner {  get; set; }
        public double TotalServicios {  get; set; }
        public double TotalRenta {  get; set; }
        public double TotalGeneral {  get; set; }

    }
}
