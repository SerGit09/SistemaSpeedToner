﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobranzaSP.Modelos
{
    public class EquipoBodega:Impresora
    {
        public int IdEquipo { get; set; }

        public double Precio { get; set; }

        public string Estado { get; set; }


        public string Notas { get; set; }
        public DateTime FechaDeLlegada { get; set; }
    }
}
