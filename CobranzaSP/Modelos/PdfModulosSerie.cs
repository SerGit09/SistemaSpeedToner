﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobranzaSP.Modelos
{
    internal class PdfModulosSerie
    {
        public string Clave { get; set; }
        public string Modulo { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaInstalacion { get; set; }
        public string Folio { get; set; }
        public string UltimoReporte { get; set; }
        public string Serie { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Cliente { get; set; }
        public int Rendimiento { get; set; }
        public int ContadorInicial { get; set; }
        public int UltimoContador { get; set; }
    }
}
