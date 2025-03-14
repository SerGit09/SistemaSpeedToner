using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using CobranzaSP.Lógica;
using CobranzaSP.Modelos;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Xml.Linq;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace CobranzaSP.Lógica
{
    internal class LogicaGarantia
    {
        private CD_Conexion conexion = new CD_Conexion();
        SqlCommand comando = new SqlCommand();
        SqlDataReader leer;
        PdfPTable tblGarantias;
        string TipoBusqueda = "";
        string ParametroBusqueda = "";
        string SegundoParametro = "";

        SortedSet<string> lstFechas = new SortedSet<string>();
        SortedSet<string> lstModelos = new SortedSet<string>();
        SortedSet<string> lstMarcas = new SortedSet<string>();
        SortedSet<string> lstClientes = new SortedSet<string>();

        Document document;
        Reporte NuevoReporte = new Reporte();
        Pdf pe;
        DataTable dtDatosReporteGarantia;
        DataTable dtDatosTotalesGarantias;

        public string RegistrarGarantia(Garantia NuevaGarantia, string sp)
        {
            int respuesta = 0;
            string AccionRealizada;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = sp;
            comando.CommandType = CommandType.StoredProcedure;

            //Preguntamos que accion realizaremos en la base de datos para posteriormente mostrale al usuario la accion que realizo
            AccionRealizada = (sp == "AgregarGarantia") ? "agrego" : "modifico";

            if (NuevaGarantia.Id > 0)
            {
                comando.Parameters.AddWithValue("@IdGarantia", NuevaGarantia.Id);
            }
            comando.Parameters.AddWithValue("@IdMarca", NuevaGarantia.IdMarca);
            comando.Parameters.AddWithValue("@IdCartucho", NuevaGarantia.IdCartucho);
            comando.Parameters.AddWithValue("@IdCliente", NuevaGarantia.IdCliente);
            comando.Parameters.AddWithValue("@Fecha", NuevaGarantia.Fecha);
            comando.Parameters.AddWithValue("@Cantidad", NuevaGarantia.Cantidad);
            comando.Parameters.AddWithValue("@Descripcion", NuevaGarantia.Descripcion);

            respuesta = comando.ExecuteNonQuery();
            string Mensaje = (respuesta > 0) ? "Garantia se " + AccionRealizada + " correctamente" : "Algo salio mal, no se " + AccionRealizada + " la garantia";

            comando.Parameters.Clear();
            conexion.CerrarConexion();
            return Mensaje;
        }

        #region PDF

        #region ObtenerDatos

        public bool ObtenerDatosReporteGarantia(Reporte NuevoReporteGarantia)
        {
            bool DatosEncontrados = true;
            dtDatosReporteGarantia = new DataTable();
            //Se asigna a la variable global para usarlo en varias partes del sistema
            NuevoReporte = NuevoReporteGarantia;
            string StoreProcedure = 
                (NuevoReporte.Cliente != "") 
                ? "ObtenerDatosReporteGarantiaClientes" 
                : "ObtenerDatosReporteGarantia";
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = StoreProcedure;
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            if(NuevoReporte.Cliente != "")
            {
                comando.Parameters.AddWithValue("@Cliente", NuevoReporte.Cliente);
            }

            comando.Parameters.AddWithValue("@ParametroBusqueda", NuevoReporte.ParametroBusqueda);
            comando.Parameters.AddWithValue("@FechaInicial", NuevoReporte.FechaInicio);
            comando.Parameters.AddWithValue("@FechaFinal", NuevoReporte.FechaFinal);
            dtDatosReporteGarantia.Load(comando.ExecuteReader());
            if (dtDatosReporteGarantia.Rows.Count == 0)
            {
                return DatosEncontrados = false;
            }
            Pdf();
            return DatosEncontrados;
        }

        public void ObtenerTotalesGarantia()
        {
            comando.Connection = conexion.AbrirConexion();
            dtDatosTotalesGarantias = new DataTable();
            comando.CommandText = "ObtenerTotalesGarantias";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@ParametroBusqueda", NuevoReporte.ParametroBusqueda);
            comando.Parameters.AddWithValue("@Cliente", NuevoReporte.Cliente);
            comando.Parameters.AddWithValue("@FechaInicial", NuevoReporte.FechaInicio);
            comando.Parameters.AddWithValue("@FechaFinal", NuevoReporte.FechaFinal);
            dtDatosTotalesGarantias.Load(comando.ExecuteReader());
        }
        #endregion

        #region CreacionPdf
        public void Pdf()
        {
            string RutaArchivo = ConfiguracionPdf.RutaReportesGarantias;
            string NombreArchivo = RutaArchivo + "ReporteGarantia" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf";
            //string NombreArchivo = @"\\administracion-pc\ARCHIVOS COMPARTIDOS\Reportes\Equipos\" + "ReporteEquipos" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf";


            FileStream fs = new FileStream(NombreArchivo, FileMode.Create);
            document = new Document(PageSize.LETTER);
            document.SetMargins(25f, 25f, 25f, 25f);
            //Colocamos el pdf en horizontal
            document.SetPageSize(iTextSharp.text.PageSize.LETTER.Rotate());
            PdfWriter pw = PdfWriter.GetInstance(document, fs);

            //Instanciamos la clase para la paginacion
            pe = new Pdf();
            pe.ColocarFormatoSuperior = true;
            pw.PageEvent = pe;

            document.Open();
            string NombreReporte = ColocarTituloReporte();
            //Colocar el titulo del reporte
            Paragraph titulo = new Paragraph(NombreReporte, pe.FuenteTitulo) { Alignment = Element.ALIGN_CENTER };
            document.Add(titulo);

            Paragraph Fechas = new Paragraph("DE: " + NuevoReporte.FechaInicio.ToString("dd/MM/yyyy") + "       AL: " + NuevoReporte.FechaFinal.ToString("dd/MM/yyyy"), pe.FuenteFecha) { Alignment = Element.ALIGN_CENTER };
            document.Add(Fechas);

            tblGarantias = new PdfPTable(4);

            GenerarPdf();


            document.Add(tblGarantias);

            lstClientes.Clear();
            lstMarcas.Clear();
            lstFechas.Clear();
            lstModelos.Clear();

            //Separamos con una linea los totales
            iTextSharp.text.pdf.draw.LineSeparator lineSeparator = new iTextSharp.text.pdf.draw.LineSeparator() { Offset = 2f };
            document.Add(new Chunk(lineSeparator));
            //TITULO DE LOS TOTALES
            Paragraph TituloTotales = new Paragraph("RESUMEN GARANTIAS DEL " + NuevoReporte.FechaInicio.ToString("dd/MM/yyyy") + " - " + NuevoReporte.FechaFinal.ToString("dd/MM/yyyy"), pe.FuenteTitulo) { Alignment = Element.ALIGN_CENTER };
            document.Add(TituloTotales);
            document.Add(new Chunk());

            ObtenerTotalesGarantia();
            CrearTablaTotales();
            document.Add(tblGarantias);
            //leer.Close();
            document.Close();
            //Abrimos el pdf

            pe.AbrirPdf(NombreArchivo);
        }

        public string ColocarTituloReporte()
        {
            StringBuilder Titulo = new StringBuilder("GARANTIAS ");

            Titulo.Append(ColocarClienteEnTitulo());

            switch (NuevoReporte.TipoBusqueda)
            {
                case "Modelo":
                    Titulo.Append("MODELO: ").Append(NuevoReporte.ParametroBusqueda);
                    break;
                case "Marca":
                    Titulo.Append((NuevoReporte.ParametroBusqueda == "") ?"MARCAS: ":"MARCA:").Append(NuevoReporte.ParametroBusqueda);break;
            }
            return Titulo.ToString();
        }

        public string ColocarClienteEnTitulo()
        {
            StringBuilder TituloCliente = new StringBuilder("");

            if (NuevoReporte.Cliente != "")
            {
                TituloCliente.Append("CLIENTE: ").Append(NuevoReporte.Cliente);

                if (NuevoReporte.ParametroBusqueda != "")
                {
                    TituloCliente.Append("\n");
                }
            }
            else
            {
                if(NuevoReporte.TipoBusqueda == "Cliente")
                {
                    TituloCliente.Append("CLIENTES");
                }
            }

            return TituloCliente.ToString();
        }

        public void GenerarPdf()
        {

            foreach (DataRow fila in dtDatosReporteGarantia.Rows)
            {
                ReporteGarantia ReporteGarantia = new ReporteGarantia()
                {
                    Marca = fila[0].ToString(),
                    Cartucho = fila[1].ToString(),
                    Fecha = DateTime.Parse(fila[2].ToString()),
                    Cliente = fila[3].ToString(),
                    Cantidad = int.Parse(fila[4].ToString()),
                    Descripcion = fila[5].ToString()
                };
                if (NuevoReporte.TipoBusqueda != "Cliente")
                {
                    AgregarMarca(ReporteGarantia.Marca, ReporteGarantia);
                }
                else
                {
                    AgregarCliente(ReporteGarantia.Cliente, ReporteGarantia);
                }
            }
        }

        public void AgregarModelo(string Modelo, ReporteGarantia ReporteGarantia)
        {

            if (!lstModelos.Contains(Modelo))
            {
                document.Add(tblGarantias);//Requerimos mover esta linea a otra
                if (NuevoReporte.TipoBusqueda != "Modelo")
                {
                    Paragraph ParrafoModelo;
                    ParrafoModelo = new Paragraph(Modelo, pe.FuenteParrafoGrande) { Alignment = Element.ALIGN_LEFT };
                    //Agregamos la fecha tanto a la lista que tenemos de fechas, como al documento colocando la cantidad que tenemos en dicho día
                    document.Add(ParrafoModelo);
                }

                lstModelos.Add(Modelo);
                CrearTablaGarantia();
                AgregarGarantiaATabla(ReporteGarantia);
            }
            else
            {
                AgregarGarantiaATabla(ReporteGarantia);
            }
        }

        public void CrearTablaGarantia()
        {
            tblGarantias = (NuevoReporte.TipoBusqueda != "Cliente") ? new PdfPTable(5) : new PdfPTable(3);

            if (NuevoReporte.TipoBusqueda != "Cliente")
            {
                PdfPCell clCliente = new PdfPCell(new Phrase("CLIENTE", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 2 };
                tblGarantias.AddCell(clCliente);
            }
            PdfPCell clFecha = new PdfPCell(new Phrase("FECHA", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            tblGarantias.AddCell(clFecha);
            PdfPCell clCantidad = new PdfPCell(new Phrase("CANTIDAD", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            tblGarantias.AddCell(clCantidad);
            PdfPCell clDescripcion = new PdfPCell(new Phrase("DESCRIPCION", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            tblGarantias.AddCell(clDescripcion);
        }

        public void AgregarGarantiaATabla(ReporteGarantia ReporteGarantia)
        {

            if (NuevoReporte.TipoBusqueda != "Cliente")
            {
                PdfPCell clCliente = new PdfPCell(new Phrase(ReporteGarantia.Cliente, pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 2 };
                tblGarantias.AddCell(clCliente);
            }
            else
            {
            }
            PdfPCell clFecha = new PdfPCell(new Phrase(ReporteGarantia.Fecha.ToString("dd/MM/yyyy"), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };
            tblGarantias.AddCell(clFecha);
            PdfPCell clCantidad = new PdfPCell(new Phrase(ReporteGarantia.Cantidad.ToString(), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };
            tblGarantias.AddCell(clCantidad);
            PdfPCell clDescripcion = new PdfPCell(new Phrase(ReporteGarantia.Descripcion, pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };
            tblGarantias.AddCell(clDescripcion);
        }

        public void AgregarMarca(string Marca, ReporteGarantia ReporteGarantia)
        {

            if (!lstMarcas.Contains(Marca))
            {
                //Si tenemos dicha fecha en la lista, agregamos los datos que tenemos de la anterior fecha
                document.Add(tblGarantias);//Requerimos mover esta linea a otra
                //lstMarcas.Clear();
                lstModelos.Clear();
                tblGarantias = new PdfPTable(4);
                Paragraph ParrafoMarca;
                if (NuevoReporte.TipoBusqueda == "Rango Fecha")
                {
                    ParrafoMarca = new Paragraph(Marca, pe.FuenteParrafoGrande) { Alignment = Element.ALIGN_LEFT };
                    //Agregamos la fecha tanto a la lista que tenemos de fechas, como al documento colocando la cantidad que tenemos en dicho día
                    document.Add(ParrafoMarca);
                }
                lstMarcas.Add(Marca);

                AgregarModelo(ReporteGarantia.Cartucho, ReporteGarantia);
            }
            else
            {
                AgregarModelo(ReporteGarantia.Cartucho, ReporteGarantia);
            }
        }

        public void AgregarCliente(string Cliente, ReporteGarantia ReporteGarantia)
        {

            if (!lstClientes.Contains(Cliente))
            {

                document.Add(tblGarantias);//Requerimos mover esta linea a otra
                iTextSharp.text.pdf.draw.LineSeparator lineSeparator = new iTextSharp.text.pdf.draw.LineSeparator() { Offset = 2f };
                document.Add(new Chunk(lineSeparator));
                //lstMarcas.Clear();
                tblGarantias = new PdfPTable(4);
                lstMarcas.Clear();
                if (NuevoReporte.TipoBusqueda != "Cliente" || NuevoReporte.Cliente == "")
                {
                    Paragraph ParrafoCliente;
                    ParrafoCliente = new Paragraph(Cliente, pe.FuenteParrafoGrande) { Alignment = Element.ALIGN_LEFT };
                    //Agregamos la fecha tanto a la lista que tenemos de fechas, como al documento colocando la cantidad que tenemos en dicho día
                    document.Add(ParrafoCliente);
                }
                lstClientes.Add(Cliente);
                AgregarMarca(ReporteGarantia.Marca, ReporteGarantia);
            }
            else
            {
                AgregarMarca(ReporteGarantia.Marca, ReporteGarantia);
            }
        }


        

        public void CrearTablaTotales()
        {
            tblGarantias = new PdfPTable(3);

            PdfPCell clMarca = new PdfPCell(new Phrase("MARCA", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            tblGarantias.AddCell(clMarca);
            PdfPCell clModelo = new PdfPCell(new Phrase("MODELO", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            tblGarantias.AddCell(clModelo);
            PdfPCell clTotal = new PdfPCell(new Phrase("TOTAL", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            tblGarantias.AddCell(clTotal);
            foreach (DataRow fila in dtDatosTotalesGarantias.Rows)
            {
                PdfPCell clDatoMarca = new PdfPCell(new Phrase(fila[0].ToString(), pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
                tblGarantias.AddCell(clDatoMarca);
                PdfPCell clDatoModelo = new PdfPCell(new Phrase(fila[1].ToString(), pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
                tblGarantias.AddCell(clDatoModelo);
                PdfPCell clDatoTotal = new PdfPCell(new Phrase(fila[2].ToString(), pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
                tblGarantias.AddCell(clDatoTotal);
            }

        }

        #endregion


        #endregion
    }
}
