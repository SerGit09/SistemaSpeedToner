using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobranzaSP.Modelos
{
    public class MovimientoParteRicoh:ParteRicoh
    {
        public int IdRegistro { get; set; }
        public int IdTipoPersona { get; set; }

        public int IdMovimiento { get; set; }
        public string ClienteProveedor { get; set; }

        public DateTime Fecha { get; set; }

        public string Folio { get; set; }
    }
}
