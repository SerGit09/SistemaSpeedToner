using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobranzaSP.Modelos
{
    internal class DatosPdfFactura:Cobranza
    {
        public string NombreCliente { get; set; }

        public DateTime FechaVencimiento {  get; set; }

        public double Saldo { get; set; }
    }
}
