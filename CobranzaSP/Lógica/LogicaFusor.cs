using System.Data.SqlClient;
using System.Data;

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using SpreadsheetLight;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
//using Excel = Microsoft.Office.Interop.Excel;

using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Style.XmlAccess;
using Color = System.Drawing.Color;
using CobranzaSP.Modelos;
using System.IO;
using System;
using CobranzaSP.Formularios;
using System.Windows.Forms;
using System.Text;
using System.Security.Permissions;

namespace CobranzaSP.Lógica
{
    internal class LogicaFusor
    {
        private CD_Conexion conexion = new CD_Conexion();
        private AccionesLógica NuevaAccion = new AccionesLógica();
        SqlCommand comando = new SqlCommand();
        bool Modificando = false;
        DataTable tbDatosFusores = new DataTable();
        PdfPTable Fusores;

        public string GuardarFusor(Fusor NuevoFusor)
        {
            comando.Connection = conexion.AbrirConexion();
            int respuesta = 0;
            string AccionRealizada = "agrego";
            comando.CommandText = "spGuardarFusor";
            comando.CommandType = CommandType.StoredProcedure;

            if (NuevoFusor.IdFusor > 0)
            {
                Modificando = true;
                AccionRealizada = "modifico";
            }

            comando.Parameters.AddWithValue("@Id", NuevoFusor.IdFusor);
            comando.Parameters.AddWithValue("@NumeroSerieO", NuevoFusor.SerieO);
            comando.Parameters.AddWithValue("@NumeroSerieS", NuevoFusor.SerieS);
            comando.Parameters.AddWithValue("@NumeroFactura", NuevoFusor.NumeroFactura);
            comando.Parameters.AddWithValue("@IdProveedor", NuevoFusor.IdProveedor);
            comando.Parameters.AddWithValue("@FechaFactura", NuevoFusor.FechaFactura);
            comando.Parameters.AddWithValue("@Costo", NuevoFusor.Precio);
            comando.Parameters.AddWithValue("@DiasGarantia", NuevoFusor.DiasGarantia);
            comando.Parameters.AddWithValue("@Estado", NuevoFusor.Estado);
            comando.Parameters.AddWithValue("@IdCartucho", NuevoFusor.IdCartucho);
            comando.Parameters.AddWithValue("@IdTipoFusor", NuevoFusor.IdTipoFusor);
            comando.Parameters.AddWithValue("@FechaLlegada", NuevoFusor.FechaLlegada);
            respuesta = comando.ExecuteNonQuery();
            string Mensaje = (respuesta > 0) ? "Fusor se " + AccionRealizada + " correctamente" : "Algo salio mal, no se " + AccionRealizada + " el registro";

            comando.Parameters.Clear();

            AgregarFusorAInventario(NuevoFusor);
            conexion.CerrarConexion();
            return Mensaje;
        }

        public void AgregarFusorAInventario(Fusor NuevoFusor)
        {

            if (Modificando)
                return;
            //Restaremos del inventario el fusor que se utilizo en base a su modelo
            LogicaRegistro AccionRegistro = new LogicaRegistro();

            //Con ayuda de la clave del fusor, podemos obtener a traves de su modelo el idcartucho
            LogicaServicio logicaServicio = new LogicaServicio();
            RegistroInventarioToners registroFusor = new RegistroInventarioToners()
            {
                Cliente = NuevoFusor.Proveedor,
                IdMarca = logicaServicio.ObtenerMarcaFusor(NuevoFusor.IdCartucho),
                IdCartucho = NuevoFusor.IdCartucho,
                CantidadSalida = 0,
                CantidadEntrada = 1,
                Fecha = NuevoFusor.FechaLlegada,
                NumeroSerie = NuevoFusor.SerieS
            };
            string Mensaje = AccionRegistro.AgregarRegistroInventario(registroFusor, false);
            MessageBox.Show(Mensaje, "REGISTRO INVENTARIO", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public SqlDataReader Mostrar(string sp)
        {
            SqlDataReader leer;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = sp;
            comando.CommandType = CommandType.StoredProcedure;
            leer = comando.ExecuteReader();
            //conexion.CerrarConexion();
            return leer;
        }

        #region GeneracionExcel
        public void GenerarExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage ExcelPkg = new ExcelPackage();
            ExcelWorksheet wsSheet1 = ExcelPkg.Workbook.Worksheets.Add("Pagina 1");

            //Nos traemos los datos
            SqlDataReader fusores = Mostrar("spMostrarTodosFusores");

            //Definimos los encabezados de cada columna de la tabla
            AgregarATablaExcel(wsSheet1, 1, 1, "Numero de serie", 14, true);
            AgregarATablaExcel(wsSheet1, 2, 1, "Numero de serie SpeedToner", 14, true);
            AgregarATablaExcel(wsSheet1, 3, 1, "Factura", 14, true); 
            AgregarATablaExcel(wsSheet1, 4, 1, "Proveedor", 14, true);
            AgregarATablaExcel(wsSheet1, 5, 1, "Fecha factura", 14, true);
            AgregarATablaExcel(wsSheet1, 6, 1, "Dias restantes", 14, true);
            AgregarATablaExcel(wsSheet1, 7, 1, "Precio", 14, true);
            AgregarATablaExcel(wsSheet1, 8, 1, "Dias Garantía", 14, true);
            AgregarATablaExcel(wsSheet1, 9, 1, "Garantía", 14, true);
            AgregarATablaExcel(wsSheet1, 10, 1, "Estado", 14, true);
            AgregarATablaExcel(wsSheet1, 11, 1, "Modelo", 14, true);

            //Insertamos los datos
            int fila = 2;
            while (fusores.Read())
            {
                DateTime FechaFactura = Convert.ToDateTime(fusores[5].ToString());

                AgregarATablaExcel(wsSheet1, 1, fila, fusores[1].ToString(), 12, false);
                AgregarATablaExcel(wsSheet1, 2, fila, fusores[2].ToString(), 12, false);
                AgregarATablaExcel(wsSheet1, 3, fila, fusores[3].ToString(), 12, false);
                AgregarATablaExcel(wsSheet1, 4, fila, fusores[4].ToString(), 12, false);
                AgregarATablaExcel(wsSheet1, 5, fila, FechaFactura.ToString("dd/MM/yyyy"), 12, false);
                AgregarATablaExcel(wsSheet1, 6, fila, fusores[6].ToString(), 12, false);
                AgregarATablaExcel(wsSheet1, 7, fila, fusores[7].ToString(), 12, false);
                AgregarATablaExcel(wsSheet1, 8, fila, fusores[8].ToString(), 12, false);
                AgregarATablaExcel(wsSheet1, 9, fila, fusores[9].ToString(), 12, false);
                AgregarATablaExcel(wsSheet1, 10, fila, fusores[10].ToString(), 12, false);
                AgregarATablaExcel(wsSheet1, 11, fila, fusores[11].ToString(), 12, false);
                fila++;
            }

            //AJUSTES AL DOCUMENTO AL FINAL
            wsSheet1.Row(4).Height = 30;
            //Ajustamos la celda al tamaño de su contenido
            wsSheet1.Cells[wsSheet1.Dimension.Address].AutoFitColumns();
            wsSheet1.Protection.IsProtected = false;
            wsSheet1.Protection.AllowSelectLockedCells = false;
            ExcelPkg.SaveAs(new FileInfo(@"\\DESKTOP-34U2P48\Archivos Compartidos\Reportes\Fusores\" + "ExcelFusores" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx"));
            conexion.CerrarConexion();
        }

        public void AgregarATablaExcel(ExcelWorksheet wsSheet1, int columna, int fila, string Valor, int TamañoLetra, bool negritas)
        {
            using (ExcelRange Rng = wsSheet1.Cells[fila, columna])
            {
                Rng.Value = Valor;
                Rng.Style.Font.Size = TamañoLetra;
                Rng.Style.Font.Bold = negritas;
            }
        }
        #endregion

        #region Pdf
        public bool ReporteFusores(Reporte NuevoReporte)
        {
            tbDatosFusores.Clear();
            bool DatosObtenidos = true;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "spReporteFusores";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@Parametro", NuevoReporte.ParametroBusqueda);
            comando.Parameters.AddWithValue("@FechaInicio", NuevoReporte.FechaInicio);
            comando.Parameters.AddWithValue("@FechaFinal", NuevoReporte.FechaFinal);
            comando.Parameters.AddWithValue("@RangoFecha", NuevoReporte.RangoHabilitado);
            tbDatosFusores.Load(comando.ExecuteReader());
            comando.Dispose();

            if (tbDatosFusores.Rows.Count == 0)
            {
                return DatosObtenidos = false;
            }
            GenerarReporteFusores(NuevoReporte);
            comando.Parameters.Clear();
            return DatosObtenidos;
        }
        public void GenerarReporteFusores(Reporte NuevoReporte)
        {
            string RutaArchivo = ConfiguracionPdf.RutaReportesFusores;
            string NombreArchivo = RutaArchivo + "ReporteFusores" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf";
            //string NombreArchivo = @"C:\Users\DELL PC\Documents\Base de datos\" + "ReporteFusores" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf";
            //Lista de las series para evitar que se repitan las series en algunas consultas
            FileStream fs = new FileStream(NombreArchivo, FileMode.Create);
            Document document = new Document(PageSize.LETTER);
            document.SetMargins(25f, 25f, 25f, 25f);
            //Colocamos el pdf en horizontal
            document.SetPageSize(iTextSharp.text.PageSize.LETTER.Rotate());
            PdfWriter pw = PdfWriter.GetInstance(document, fs);

            //Variable que servira para que dependiendo lo que eliga el usuario, una columna abarque dos espacios, ya que una columna en especifico no aparecera
            

            //Instanciamos para que podamos obtener los indices de cada hoja
            var pe = new Pdf();
            pw.PageEvent = pe;
            pe.ColocarFormatoSuperior = true;

            document.Open();

            Paragraph titulo = new Paragraph(ColocarTituloReporte(NuevoReporte).ToUpper(), pe.FuenteTitulo);
            titulo.Alignment = Element.ALIGN_CENTER;
            document.Add(titulo);

            document.Add(new Chunk());

            CrearTablaFusor(NuevoReporte);

            foreach (DataRow fila in tbDatosFusores.Rows)
            {
                PdfPCell clSerie = new PdfPCell(new Phrase(fila[1].ToString(), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 2 };
                Fusores.AddCell(clSerie);
                //Solo se mostrara en dado caso de que no estemos generando reporte por el numero de serie
                if (NuevoReporte.ParametroBusqueda != "Serie")
                {
                    PdfPCell clSerieSp = new PdfPCell(new Phrase(fila[2].ToString(), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };
                    Fusores.AddCell(clSerieSp);
                }

                PdfPCell clFactura = new PdfPCell(new Phrase(fila[3].ToString(), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };
                PdfPCell clProveedor = new PdfPCell(new Phrase(fila[5].ToString(), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };
                PdfPCell clFechaFactura = new PdfPCell(new Phrase(string.Format("{0:d}", fila[4]), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };
                PdfPCell clPrecio = new PdfPCell(new Phrase(fila[7].ToString(), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };

                Fusores.AddCell(clFactura);
                Fusores.AddCell(clFechaFactura);
                Fusores.AddCell(clProveedor);
                Fusores.AddCell(clPrecio);
                //Solo se mostraran los dias restantes en dado caso de que no estemos generando reporte por fusores deshabilitados
                if (NuevoReporte.ParametroBusqueda != "Deshabilitada")
                {
                    PdfPCell clDiasRestantes = new PdfPCell(new Phrase(fila[6].ToString(), pe.FuenteParrafo)) { BorderWidth = .5f };
                    Fusores.AddCell(clDiasRestantes);
                }
            }
            document.Add(Fusores);
            document.Close();
            //Abrimos el pdf 
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(NombreArchivo)
            {
                UseShellExecute = true
            };
            p.Start();
        }

        public void CrearTablaFusor(Reporte NuevoReporte)
        {
            Pdf pe = new Pdf();

            Fusores = new PdfPTable(8);
            switch (NuevoReporte.ParametroBusqueda)
            {
                case "Serie":
                    Fusores = new PdfPTable(7);
                    break;
                case "Deshabilitada":
                    Fusores = new PdfPTable(7);
                    break;
            }
            Fusores.WidthPercentage = 100;

            PdfPCell cltSerie = new PdfPCell(new Phrase("Serie", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 2};
            Fusores.AddCell(cltSerie);
            if (NuevoReporte.ParametroBusqueda != "Serie")
            {
                PdfPCell cltSerieSp = new PdfPCell(new Phrase("Serie Sp", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
                Fusores.AddCell(cltSerieSp);
            }
            PdfPCell cltFactura = new PdfPCell(new Phrase("#Factura", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            PdfPCell cltFechaFactura = new PdfPCell(new Phrase("FechaFactura", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            PdfPCell cltProveedor = new PdfPCell(new Phrase("Proveedor", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            PdfPCell cltPrecio = new PdfPCell(new Phrase("Precio", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };


            Fusores.AddCell(cltFactura);
            Fusores.AddCell(cltFechaFactura);
            Fusores.AddCell(cltProveedor);
            Fusores.AddCell(cltPrecio);
            //Solo se añadira la columna dias restantes en dado caso de que sea diferente de fusores deshabilitados
            if (NuevoReporte.ParametroBusqueda != "Deshabilitada")
            {
                PdfPCell cltDiasRestantes = new PdfPCell(new Phrase("Dias Restantes", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
                Fusores.AddCell(cltDiasRestantes);
            }
        }

        public string ColocarTituloReporte(Reporte NuevoReporte)
        {
            StringBuilder TituloBuilder = new StringBuilder("REPORTE FUSORES");

            switch (NuevoReporte.ParametroBusqueda)
            {
                case "Serie":
                    TituloBuilder.Clear();
                    TituloBuilder.Append(" SERIE: ").Append(NuevoReporte.ParametroBusqueda);
                    break;
                case "Rango Fecha":
                    TituloBuilder.Append("\nDEL: " ).Append(NuevoReporte.FechaInicio).Append(" AL ").Append(NuevoReporte.FechaFinal);
                    break;
                case "Habilitado":
                    TituloBuilder.Append(" GARANTIA ").Append(NuevoReporte.ParametroBusqueda);
                    break;
                case "Deshabilitada":
                    TituloBuilder.Append(" GARANTIA ").Append(NuevoReporte.ParametroBusqueda);
                    break;
            }

            return TituloBuilder.ToString();
        }
        #endregion
    }
}
