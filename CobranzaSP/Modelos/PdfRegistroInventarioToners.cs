using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobranzaSP.Modelos
{
    internal class PdfRegistroInventarioToners:RegistroInventarioToners
    {
        public string FechaActual { get; set; }

        public string Marca { get; set; }

        public string Cliente_Proveedor { get; set; }

        public string ClaveFusor { get; set; }

    }
}
