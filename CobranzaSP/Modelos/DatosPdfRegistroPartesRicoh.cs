using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobranzaSP.Modelos
{
    internal class DatosPdfRegistroPartesRicoh
    {
        public DateTime Fecha { get; set; }

        public string ModeloParteRicoh {  get; set; }
        public string TipoMovimiento {  get; set; }
        public string Parte {  get; set; }
        public string Entidad {  get; set; }
        public string Folio {  get; set; }
        public int Cantidad {  get; set; }

        //Parametros utilizados unicamente en las salidas

        public string SerieEquipo { get; set; }
        public string MarcaEquipo { get; set; }
        public string ModeloEquipo { get; set; }

    }
}
