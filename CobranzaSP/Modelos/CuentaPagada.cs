using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobranzaSP.Modelos
{
    //public class CuentaPagada:Cuenta
    public class CuentaPagada: DocumentoCobranza
    {
        public int Id { get; set; }
        public int IdTipoFactura { get; set; }

        public DateTime FechaFactura { get;set; }
        
        public bool CuentaSaldada {  get; set; }

        public double Saldo { get; set; }

        public string Documento { get; set; }
        public string Cliente { get; set; }
        public string Factura { get; set; }
    }
}
