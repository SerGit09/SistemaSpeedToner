using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobranzaSP.Modelos
{
    public class InformacionRemision
    {
        public int Folio {  get; set; }
        public double Total {  get; set; }
        public string TablaProductos {  get; set; }

        public DateTime Fecha { get; set; }
    }
}
