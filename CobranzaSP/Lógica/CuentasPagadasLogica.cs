using CobranzaSP.Modelos;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobranzaSP.Lógica
{
    internal class CuentasPagadasLogica
    {
        private CD_Conexion cn = new CD_Conexion();
        SqlCommand comando = new SqlCommand();
        AccionesLógica NuevaAccion = new AccionesLógica();

        //Variables para Reporte
        DataTable tblDatosCuentasPagadas = new DataTable();
        DataTable tblDatosReporte = new DataTable();
        Document document;
        SortedSet<string> lstClientes = new SortedSet<string>();
        PdfPTable tblCuentasPagadas;
        bool ColocarNombreCliente = true;

        double Total;
        double TotalGeneral;

        public string ActualizarCobranzaMesEspecifico(int Mes, int Año)
        {
            string TotalCobranza;
            SqlCommand comando = new SqlCommand();
            comando.Connection = cn.AbrirConexion();
            comando.CommandText = "MostrarCuentasMesEspecifico";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Mes", Mes);
            //Mandamos la variable y especificamos el tipo de dato sql que sera
            SqlParameter total = new SqlParameter("@totalCobrado", SqlDbType.Money); total.Direction = ParameterDirection.Output;

            comando.Parameters.AddWithValue("@Año", Año);

            comando.Parameters.Add(total);
            //comando.CommandText = "ObtenerTotales";

            comando.ExecuteNonQuery();
            //lblTotalCobrado.Text = comando.Parameters["@totalCobranza"].Value.ToString();
            double ValorRecbido = double.Parse(comando.Parameters["@totalCobrado"].Value.ToString());//LINEA QUE ESTA FALLANDO
            comando.Parameters.Clear();
            cn.CerrarConexion();
            return TotalCobranza = String.Format("{0:c}", ValorRecbido);
            //lblTotalCobrado.Text = ValorRecbido +"";
        }

        public DataTable MostrarCuentasPagadasMesEspecifico(int Mes, int Año)
        {
            DataTable tabla = new DataTable();
            SqlDataReader leer;
            comando.Connection = cn.AbrirConexion();
            comando.CommandText = "MostrarCuentasPagadasMesElegido";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Mes", Mes);
            comando.Parameters.AddWithValue("@Año", Año);
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            comando.Parameters.Clear();
            cn.CerrarConexion();
            return tabla;
        }


        public void ModificarCuentaPagada(CuentaPagada CuentaPagada)
        {
            comando.Connection = cn.AbrirConexion();
            comando.CommandText = "ModificarCuentaPagada";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Id", CuentaPagada.Id);
            comando.Parameters.AddWithValue("@FechaPago", CuentaPagada.FechaPago);
            comando.ExecuteNonQuery();

            comando.Parameters.Clear();
        }

        #region Reportes
        public bool ObtenerDatosReporteCuentasPagadas(ReporteFactura NuevoReporte)
        {
            bool DatosObtenidos = true;
            tblDatosCuentasPagadas.Clear();
            comando.Connection = cn.AbrirConexion();
            comando.CommandText = "DatosReporteCuentasPagadas";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@Cliente", NuevoReporte.Cliente);
            comando.Parameters.AddWithValue("@FechaInicial", NuevoReporte.FechaInicio);
            comando.Parameters.AddWithValue("@FechaFinal", NuevoReporte.FechaFinal);

            tblDatosCuentasPagadas.Load(comando.ExecuteReader());
            comando.Dispose();

            if (tblDatosCuentasPagadas.Rows.Count == 0)
            {
                return DatosObtenidos = false;
            }

            //Comenzamos a generar el reporte
            Pdf(NuevoReporte);
            return DatosObtenidos;
        }

        public void Pdf(ReporteFactura NuevoReporte)
        {
            string NombreArchivo = @"\\DESKTOP-34U2P48\Archivos Compartidos\Reportes\Facturas\" + "ReporteFactura" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf";
            bool ColocarTotal = false;
            FileStream fs = new FileStream(NombreArchivo, FileMode.Create);
            document = new Document(PageSize.LETTER);
            document.SetMargins(25f, 25f, 25f, 25f);
            //Colocamos el pdf en horizontal
            document.SetPageSize(iTextSharp.text.PageSize.LETTER);
            PdfWriter pw = PdfWriter.GetInstance(document, fs);

            //Instanciamos la clase para la paginacion
            var pe = new Pdf();
            pe.ColocarFormatoSuperior = true;
            pw.PageEvent = pe;

            document.Open();


            string NombreReporte = ColocarNombreReporte(NuevoReporte);
            Paragraph titulo = new Paragraph(NombreReporte, pe.FuenteTitulo) { Alignment = Element.ALIGN_CENTER };
            document.Add(titulo);
            Paragraph Fechas = new Paragraph("DEL " + NuevoReporte.FechaInicio.ToString("dd/MM/yyyy") + " AL " + NuevoReporte.FechaFinal.ToString("dd/MM/yyyy"), pe.FuenteFecha) { Alignment = Element.ALIGN_CENTER };
            document.Add(Fechas);

            document.Add(new Paragraph("\n"));

            tblCuentasPagadas = new PdfPTable(4) { HorizontalAlignment = Element.ALIGN_LEFT, WidthPercentage = 80, PaddingTop = 10f };

            foreach (DataRow fila in tblDatosCuentasPagadas.Rows)
            {
                DatosPdfCuentaPagada DatosCuentaPagada = new DatosPdfCuentaPagada()
                {
                    Cliente = fila[0].ToString(),
                    Factura = fila[1].ToString(),
                    FechaPago = DateTime.Parse(fila[2].ToString()),
                    Cantidad = double.Parse(fila[3].ToString()),
                    Documento = fila[4].ToString()
                };
                AgregarClienteAlDocumento(DatosCuentaPagada);
            }

            //Agregar los datos del ultimo cliente
            ColocarTotalCliente();
            document.Add(tblCuentasPagadas);

            ColocarTotalGeneral();
            lstClientes.Clear();
            Total = 0;
            TotalGeneral = 0;
            ColocarNombreCliente = true;
            document.Close();


            //Abrimos el pdf 
            pe.AbrirPdf(NombreArchivo);
        }

        public string ColocarNombreReporte(ReporteFactura NuevoReporte)
        {
            StringBuilder Titulo = new StringBuilder("CUENTAS PAGADAS");
            if (NuevoReporte.Cliente != "")
            {
                Titulo.Append("\n").Append(NuevoReporte.Cliente);
                ColocarNombreCliente = false;
            }
            return Titulo.ToString();
        }

        public void AgregarClienteAlDocumento(DatosPdfCuentaPagada DatosCuentaPagada)
        {
            Pdf pe = new Pdf();
            if (!lstClientes.Contains(DatosCuentaPagada.Cliente))
            {
                if (lstClientes.Count > 0)
                {
                    //Agregar tabla que ya se tenia del cliente anterior
                    ColocarTotalCliente();
                    document.Add(tblCuentasPagadas);

                    //Sumar el total del cliente anterior al total general
                    Total = 0;
                }

                

                lstClientes.Add(DatosCuentaPagada.Cliente);
                if (ColocarNombreCliente)
                {
                    document.Add(new Paragraph(DatosCuentaPagada.Cliente, pe.FuenteFecha));
                }
                CrearTablaCuentasPagadas();
                AgregarDatosATablaFacturas(DatosCuentaPagada);
            }
            else
            {
                AgregarDatosATablaFacturas(DatosCuentaPagada);
            }
        }

        public void CrearTablaCuentasPagadas()
        {
            Pdf pdf = new Pdf();
            tblCuentasPagadas = new PdfPTable(4) { HorizontalAlignment = Element.ALIGN_LEFT, WidthPercentage = 80, PaddingTop = 10f };

            //Encabezados
            PdfPCell clEncabezadoFolio = new PdfPCell(new Phrase("FOLIO", pdf.FuenteParrafoBold)) { BorderWidth = .5f };
            PdfPCell clEncabezadoFechaPago = new PdfPCell(new Phrase("FECHA DE PAGO", pdf.FuenteParrafoBold)) { BorderWidth = .5f };
            PdfPCell clEncabezadoImporte = new PdfPCell(new Phrase("IMPORTE", pdf.FuenteParrafoBold)) { BorderWidth = .5f };
            PdfPCell clEncabezadoDocumento = new PdfPCell(new Phrase("DOCUMENTO", pdf.FuenteParrafoBold)) { BorderWidth = .5f };

            tblCuentasPagadas.AddCell(clEncabezadoFolio);
            tblCuentasPagadas.AddCell(clEncabezadoFechaPago);
            tblCuentasPagadas.AddCell(clEncabezadoImporte);
            tblCuentasPagadas.AddCell(clEncabezadoDocumento);
        }

        public void AgregarDatosATablaFacturas(DatosPdfCuentaPagada DatosCuentaPagada)
        {
            var pe = new Pdf();
            PdfPCell clFactura = new PdfPCell(new Phrase(DatosCuentaPagada.Factura, pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };
            PdfPCell clFechaPago = new PdfPCell(new Phrase(DatosCuentaPagada.FechaPago.ToString("dd/MM/yyyy"), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };
            PdfPCell clImporte = new PdfPCell(new Phrase("$" + String.Format("{0:n2}", DatosCuentaPagada.Cantidad), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };
            PdfPCell clDocumento = new PdfPCell(new Phrase(DatosCuentaPagada.Documento, pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };
            

            tblCuentasPagadas.AddCell(clFactura);
            tblCuentasPagadas.AddCell(clFechaPago);
            tblCuentasPagadas.AddCell(clImporte);
            tblCuentasPagadas.AddCell(clDocumento);

            //Sumar cantidad total
            Total += DatosCuentaPagada.Cantidad;
            TotalGeneral += DatosCuentaPagada.Cantidad;
        }

        public void ColocarTotalCliente()
        {
            var pe = new Pdf();
            PdfPCell clEspacioVacio = new PdfPCell(new Phrase("", pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };
            PdfPCell clTotal = new PdfPCell(new Phrase("TOTAL", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            PdfPCell clTotalCantidad = new PdfPCell(new Phrase("$" + String.Format("{0:n2}", Total), pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 2 };
            

            tblCuentasPagadas.AddCell(clEspacioVacio);
            tblCuentasPagadas.AddCell(clTotal);
            tblCuentasPagadas.AddCell(clTotalCantidad);
        }

        public void ColocarTotalGeneral()
        {
            Pdf pe = new Pdf();

            Paragraph CantidadReportes = new Paragraph("TOTAL $" + String.Format("{0:n2}", TotalGeneral), pe.FuenteFecha) { Alignment = Element.ALIGN_CENTER };
            document.Add(CantidadReportes);
        }
        #endregion
    }
}
