﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobranzaSP.Modelos
{
    internal class Remision:DocumentoCobranza
    {
        public int NumeroFolio { get; set; }

        public int DiasCredito { get; set; }

        public int IdTipoFactura {  get; set; }
    }
}
