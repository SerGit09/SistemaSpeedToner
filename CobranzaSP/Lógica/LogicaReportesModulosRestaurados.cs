using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CobranzaSP.Lógica;
using CobranzaSP.Modelos;
using System.Data.SqlClient;
using System.Data;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace CobranzaSP.Lógica
{
    public class LogicaReportesModulosRestaurados
    {
        private CD_Conexion conexion = new CD_Conexion();
        SqlCommand comando = new SqlCommand();
        DataTable tblDatosReporte;
        Pdf pe;
        Document document;
        int NumeroReportes = 0;
        SortedSet<string> lstFechas = new SortedSet<string>();
        string TipoBusqueda = "";

        public string GuardarReporte(ReporteModuloRestaurado ReporteModulo)
        {
            int respuesta = 0;
            string AccionRealizada = "agrego";
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "GuardarReporteModulosRestaurados";
            comando.CommandType = CommandType.StoredProcedure;

            if (ReporteModulo.IdReporte > 0)
            {
                AccionRealizada = "modifico";
            }
            comando.Parameters.AddWithValue("@IdReporte", ReporteModulo.IdReporte);
            comando.Parameters.AddWithValue("@FolioReporte", ReporteModulo.FolioReporte);
            comando.Parameters.AddWithValue("@Fecha", ReporteModulo.Fecha);
            comando.Parameters.AddWithValue("@NumeroPaginas", ReporteModulo.NumeroPaginas);
            comando.Parameters.AddWithValue("@IdModulo", ReporteModulo.IdModulo);
            comando.Parameters.AddWithValue("@IdClaveModulo", ReporteModulo.IdClaveModulo);
            comando.Parameters.AddWithValue("@Clave", ReporteModulo.Clave);
            comando.Parameters.AddWithValue("@ServicioRealizado", ReporteModulo.ServicioRealizado);

            respuesta = comando.ExecuteNonQuery();
            string Mensaje = (respuesta > 0) ? "Reporte se " + AccionRealizada + " correctamente" : "Algo salio mal, no se " + AccionRealizada + " la garantia";

            comando.Parameters.Clear();
            conexion.CerrarConexion();
            return Mensaje;
        }

        #region PDF
        public bool ObtenerDatosReporte(Reporte NuevoReporte)
        {
            bool DatosObtenidos = true;
            TipoBusqueda = NuevoReporte.TipoBusqueda;
            tblDatosReporte = new DataTable();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "ObtenerDatosReporteServiciosModulos";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@ClaveModulo", NuevoReporte.ParametroBusqueda);
            comando.Parameters.AddWithValue("@FechaInicio", NuevoReporte.FechaInicio);
            comando.Parameters.AddWithValue("@FechaFinal", NuevoReporte.FechaFinal);
            tblDatosReporte.Load(comando.ExecuteReader());
            comando.Dispose();

            if (tblDatosReporte.Rows.Count == 0)
            {
                return DatosObtenidos = false;
            }
            Pdf(NuevoReporte);
            return DatosObtenidos;
        }

        public void Pdf(Reporte NuevoReporte)
        {
            string RutaArchivo = ConfiguracionPdf.RutaReportesServicios;
            string Fecha = DateTime.Now.ToString("ddMMyyyyHHmmss");
            StringBuilder NombreArchivo = new StringBuilder();
            NombreArchivo.Append(RutaArchivo).Append("ReporteServicioModulo ").Append(NuevoReporte.ParametroBusqueda).Append(Fecha).Append(".pdf");
            bool ColocarTotal = false;
            FileStream fs = new FileStream(NombreArchivo.ToString(), FileMode.Create);
            document = new Document(PageSize.LETTER);
            document.SetMargins(25f, 25f, 25f, 25f);
            //Colocamos el pdf en horizontal
            document.SetPageSize(iTextSharp.text.PageSize.LETTER);
            PdfWriter pw = PdfWriter.GetInstance(document, fs);

            //Instanciamos la clase para la paginacion
            pe = new Pdf();
            pe.ColocarFormatoSuperior = true;
            pw.PageEvent = pe;

            document.Open();

            //Colocar el titulo del reporte
            string NombreReporte = TituloReporte(NuevoReporte);
            Paragraph titulo = new Paragraph(NombreReporte, pe.FuenteTitulo) { Alignment = Element.ALIGN_CENTER };
            document.Add(titulo);

            foreach (DataRow fila in tblDatosReporte.Rows)
            {
                ServicioModulo reporteServicio = new ServicioModulo()
                {
                    NumeroFolio = fila[0].ToString(),
                    ClaveModulo = fila[1].ToString(),
                    Fecha = Convert.ToDateTime(fila[2].ToString()),
                    ServicioRealizado = fila[3].ToString(),
                    Contador = int.Parse(fila[4].ToString()),
                };

                NumeroReportes++;
                AgregarFechaAlDocumento(reporteServicio);
            }

            Paragraph CantidadReportes = new Paragraph("TOTAL DE REPORTES " + NumeroReportes, pe.FuenteFecha) { Alignment = Element.ALIGN_CENTER };
            document.Add(CantidadReportes);

            lstFechas.Clear();
            document.Close();
            NumeroReportes = 0;
            tblDatosReporte = new DataTable();

            //Abrimos el pdf 
            pe.AbrirPdf(NombreArchivo.ToString());
        }

        public string TituloReporte(Reporte NuevoReporte)
        {
            StringBuilder NombreReporte = new StringBuilder("REPORTES SERVICIOS MODULOS");

            if (TipoBusqueda == "MODULO")
            {
                NombreReporte.Append("\n").Append("MODULO: ").Append(NuevoReporte.ParametroBusqueda);
            }


            return NombreReporte.ToString();
        }

        public void AgregarFechaAlDocumento(ServicioModulo ReporteServicio)
        {
            string fechaString = ReporteServicio.Fecha.ToString("dd/MM/yyyy");
            if (!lstFechas.Contains(ReporteServicio.Fecha.ToString()))
            {
                lstFechas.Add(ReporteServicio.Fecha.ToString());
                document.Add(new Paragraph(fechaString, pe.FuenteFecha));
            }

            AgregarDatosServicio(ReporteServicio);

        }

        public void AgregarDatosServicio(ServicioModulo ReporteServicio)
        {

            document.Add(new Paragraph("# REPORTE: " + ReporteServicio.NumeroFolio.ToUpper(), pe.FuenteParrafoBold));

            if(TipoBusqueda != "MODULO")
            {
                document.Add(new Paragraph("CLAVE MODULO: " + ReporteServicio.ClaveModulo.ToUpper(), pe.FuenteParrafoBold));
            }
            document.Add(new Paragraph("SERVICIO: " + ReporteServicio.ServicioRealizado.ToUpper(), pe.FuenteParrafo));

            document.Add(new Paragraph("CONTADOR: " + string.Format("{0:n0}", int.Parse(ReporteServicio.Contador.ToString())), pe.FuenteParrafo));
        }
        #endregion
    }
}
