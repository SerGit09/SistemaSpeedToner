using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobranzaSP.Lógica
{
    public static class ConfiguracionPdf
    {
        //public static string RutaPdf { get; set; } = @"\\DESKTOP-34U2P48\Archivos Compartidos\";
        //private static readonly string RutaPdf = @"C:\Users\acer azul\Documents\Reportes\";
        private static readonly string RutaPdf = "\\\\DESKTOP-5MS680E\\Archivos Compartidos\\Reportes";

        //Declaracion de las rutas
        public static string RutaReportesServicios { get; set; }
        public static string RutaReportesModulos { get; set; }
        public static string RutaReportesEquiposRenta { get; set; }
        public static string RutaReportesEquiposBodega { get; set; }
        public static string RutaReportesFusores { get; set; }
        public static string RutaReportesInventarioToners { get; set; }
        public static string RutaReportesInventarioPartes { get; set; }
        public static string RutaReportesInventarioModulos { get; set; }
        public static string RutaReportesFacturas { get; set; }
        public static string RutaReportesEstadosCuentas { get; set; }
        public static string RutaReportesFacturasPagadas { get; set; }
        public static string RutaReportesGarantias { get; set; }
        public static string RutaReportesRegistrosInventarioToners { get; set; }
        public static string RutaReportesRegistrosInventarioPartes { get; set; }
        public static string RutaReportesRemisiones { get; set; }

        // Constructor estático para inicializar las propiedades y asignarles la carpeta donde se guardaran los pdfs
        static ConfiguracionPdf()
        {
            RutaReportesServicios = CrearRuta("Servicios");
            RutaReportesModulos = CrearRuta("Modulos");
            RutaReportesEquiposRenta = CrearRuta("Equipos");
            RutaReportesEquiposBodega = CrearRuta("EquiposBodega");
            RutaReportesFusores = CrearRuta("Fusores");
            RutaReportesInventarioToners = CrearRuta("InventarioToners");
            RutaReportesInventarioPartes = CrearRuta("InventarioPartes");
            RutaReportesInventarioModulos = CrearRuta("InventarioModulos");
            RutaReportesFacturas = CrearRuta("Facturas");
            RutaReportesGarantias = CrearRuta("Garantias");
            RutaReportesFacturasPagadas = CrearRuta("FacturasPagadas");
            RutaReportesRegistrosInventarioToners = CrearRuta("RegistrosInventarioToners");
            RutaReportesRegistrosInventarioPartes = CrearRuta("RegistrosInventarioPartes");
            RutaReportesRemisiones = CrearRuta("Remisiones");
            RutaReportesEstadosCuentas = CrearRuta("Estados de Cuenta");
        }

        private static string CrearRuta(string subcarpeta)
        {
            return Path.Combine(RutaPdf, subcarpeta) + "\\";
        }
    }
}
