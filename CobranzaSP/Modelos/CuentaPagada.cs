using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobranzaSP.Modelos
{
    public class CuentaPagada:Cuenta
    {
        public int Id { get; set; }

        public DateTime FechaPago { get;set; }

        public bool CuentaSaldada {  get; set; }

        public double Saldo { get; set; }

        public string Documento { get; set; }
    }
}
