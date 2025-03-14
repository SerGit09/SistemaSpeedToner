using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobranzaSP.Modelos
{
    public class ArchivoInventarioDatos
    {
        public int IdArchivoInventario { get; set; }
        public int IdTipoInventario { get; set; }
        public DateTime Fecha { get; set; }
        public string UrlArchivo { get; set; }
    }
}
