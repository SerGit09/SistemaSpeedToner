using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobranzaSP.Modelos
{
    public class DatosReporteInventarioToners
    {
        public string Marca {  get; set; }
        public string Modelo {  get; set; }

        public string CantidadOficina { get; set; }
        public int CantidadSalida { get; set; }
        public int CantidadEntrada { get; set; }

        public int IdTipoArticulo { get; set; }
        public int IdCartucho { get; set; }
    }
}
