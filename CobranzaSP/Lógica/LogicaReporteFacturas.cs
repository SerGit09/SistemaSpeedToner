﻿using CobranzaSP.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using CobranzaSP.Formularios;
using System.Windows.Forms;

namespace CobranzaSP.Lógica
{
    internal class LogicaReporteFacturas
    {
        private CD_Conexion conexion = new CD_Conexion();
        private AccionesLógica NuevaAccion = new AccionesLógica();
        SqlCommand comando = new SqlCommand();
        DataTable tblDatosReporte = new DataTable();
        DataTable tblDatosPagosFactura = new DataTable();
        SortedSet<string> lstClientes = new SortedSet<string>();
        Document document;
        PdfPTable tblFacturas;
        double TotalFacturas = 0;
        double TotalSaldo = 0;
        double TotalGeneralFacturas = 0;
        TotalesPorTipoFactura NuevoTotalFacturas;
        TotalesGeneralesFacturacion NuevoTotalGeneralFacturacion;

        public bool ObtenerDatosReporteFacturas(Reporte NuevoReporte)
        {
            bool DatosObtenidos = true;
            tblDatosReporte.Clear();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "spReporteFacturas";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@ParametroBusqueda", NuevoReporte.ParametroBusqueda);
            comando.Parameters.AddWithValue("@Cliente", NuevoReporte.Cliente);
            tblDatosReporte.Load(comando.ExecuteReader());
            comando.Dispose();

            if (tblDatosReporte.Rows.Count == 0)
            {
                return DatosObtenidos = false;
            }

            //Comenzamos a generar el reporte
            Pdf(NuevoReporte);
            return DatosObtenidos;
        }

        public bool ObtenerDatosReporteFacturasPromesaPago(Reporte NuevoReporte)
        {
            bool DatosObtenidos = true;
            tblDatosReporte.Clear();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "spReporteFacturasPromesaPago";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@Cliente", NuevoReporte.Cliente);
            tblDatosReporte.Load(comando.ExecuteReader());
            comando.Dispose();

            if (tblDatosReporte.Rows.Count == 0)
            {
                return DatosObtenidos = false;
            }

            //Comenzamos a generar el reporte
            Pdf(NuevoReporte);
            return DatosObtenidos;
        }

        public bool ObtenerDatosReporteTiposFacturas(Reporte NuevoReporte)
        {
            bool DatosObtenidos = true;
            tblDatosReporte.Clear();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "ObtenerDatosReporteTiposFacturas";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@Cliente", NuevoReporte.Cliente);
            comando.Parameters.AddWithValue("@FechaInicio", NuevoReporte.FechaInicio);
            comando.Parameters.AddWithValue("@FechaFinal", NuevoReporte.FechaFinal);
            tblDatosReporte.Load(comando.ExecuteReader());
            comando.Dispose();

            if (tblDatosReporte.Rows.Count == 0)
            {
                return DatosObtenidos = false;
            }

            //Comenzamos a generar el reporte
            Pdf(NuevoReporte);
            return DatosObtenidos;
        }

        public bool ObtenerDatosPagoFactura(DatosPdfFactura DatosFactura)
        {
            bool DatosObtenidos = true;
            tblDatosPagosFactura.Clear();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "ObtenerDatosPagoFactura";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@Factura", DatosFactura.NumeroFactura);
            tblDatosPagosFactura.Load(comando.ExecuteReader());
            comando.Dispose();

            if (tblDatosPagosFactura.Rows.Count == 0)
            {
                return DatosObtenidos = false;
            }
            return DatosObtenidos;
        }

        public void Pdf(Reporte NuevoReporte)
        {
            string RutaArchivo = ConfiguracionPdf.RutaReportesFacturas;
            string NombreArchivo = RutaArchivo + "ReporteFactura" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf";
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

            if(NuevoReporte.TipoBusqueda == "TIPOS FACTURAS")
            {
                Paragraph Fechas = new Paragraph("DEL " + NuevoReporte.FechaInicio.ToString("dd/MM/yyyy") + " AL " + NuevoReporte.FechaFinal.ToString("dd/MM/yyyy"), pe.FuenteFecha) { Alignment = Element.ALIGN_CENTER };
                document.Add(Fechas);
                document.Add(new Chunk("\n"));
            }

            switch (NuevoReporte.TipoBusqueda)
            {
                case "TIPOS FACTURAS":CrearReporteTiposFacturas();break;
                default:CrearReporteFacturas(NuevoReporte); break;
            }
            
            document.Close();


            //Abrimos el pdf 
            pe.AbrirPdf(NombreArchivo);
        }

        public void CrearReporteTiposFacturas()
        {
            ReiniciarTotalesFacturas();
            CrearTablaTotalesTiposFacturas();

            NuevoTotalGeneralFacturacion = new TotalesGeneralesFacturacion()
            {
                TotalGeneralRenta = 0,
                TotalGeneralServicios = 0,
                TotalGeneralToner = 0
            };

            foreach (DataRow fila in tblDatosReporte.Rows)
            {
                PdfTipoFactura DatosTipoFactura = new PdfTipoFactura()
                {
                    IdTipoFactura = int.Parse(fila[0].ToString()),
                    Cliente = fila[1].ToString(),
                    Cantidad = double.Parse(fila[2].ToString())
                };
                AgregarClienteAlDocumento(DatosTipoFactura);
            }
            AgregarTotalesATabla(NuevoTotalFacturas.Cliente);
            AgregarTotalesGeneralesATabla();
            lstClientes.Clear();
            //Se agrega el total general
            document.Add(tblFacturas);
        }

        public void ReiniciarTotalesFacturas()
        {
            NuevoTotalFacturas = new TotalesPorTipoFactura()
            {
                Cliente = "",
                TotalRenta = 0,
                TotalGeneral = 0,
                TotalToner = 0,
                TotalServicios = 0,
            };
        }

        public void AgregarClienteAlDocumento(PdfTipoFactura DatosTipoFactura)
        {
            Pdf pe = new Pdf();
            if (!lstClientes.Contains(DatosTipoFactura.Cliente))
            {
                if (lstClientes.Count > 0)
                {
                    AgregarTotalesATabla(NuevoTotalFacturas.Cliente);
                    ReiniciarTotalesFacturas();
                }
                NuevoTotalFacturas.Cliente = DatosTipoFactura.Cliente;
                lstClientes.Add(DatosTipoFactura.Cliente);
                AgregarTotalesFacturas(DatosTipoFactura);
            }
            else
            {
                AgregarTotalesFacturas(DatosTipoFactura);
            }
        }

        public void CrearTablaTotalesTiposFacturas()
        {
            Pdf pdf = new Pdf();
            tblFacturas = new PdfPTable(5) { HorizontalAlignment = Element.ALIGN_LEFT, WidthPercentage = 100, PaddingTop = 10f };

            //Encabezados
            PdfPCell clCliente = new PdfPCell(new Phrase("CLIENTE", pdf.FuenteParrafoBold)) { BorderWidth = .5f,Colspan =2 };
            PdfPCell clTotalToner = new PdfPCell(new Phrase("TONER", pdf.FuenteParrafoBold)) { BorderWidth = .5f };
            PdfPCell clTotalServicios = new PdfPCell(new Phrase("SERVICIOS TÉCNICOS", pdf.FuenteParrafoBold)) { BorderWidth = .5f };
            PdfPCell clTotalRenta = new PdfPCell(new Phrase("RENTA", pdf.FuenteParrafoBold)) { BorderWidth = .5f };

            tblFacturas.AddCell(clCliente);
            tblFacturas.AddCell(clTotalToner);
            tblFacturas.AddCell(clTotalServicios);
            tblFacturas.AddCell(clTotalRenta);
        }

        public void AgregarTotalesFacturas(PdfTipoFactura DatosTipoFactura)
        {
            NuevoTotalFacturas.TotalGeneral += DatosTipoFactura.Cantidad;
            switch (DatosTipoFactura.IdTipoFactura)
            {
                case 1:
                    NuevoTotalFacturas.TotalToner = DatosTipoFactura.Cantidad;
                    NuevoTotalGeneralFacturacion.TotalGeneralToner += DatosTipoFactura.Cantidad;
                        ; break;
                case 2: 
                    NuevoTotalFacturas.TotalServicios = DatosTipoFactura.Cantidad;
                    NuevoTotalGeneralFacturacion.TotalGeneralServicios += DatosTipoFactura.Cantidad;
                    break;
                case 3: 
                    NuevoTotalFacturas.TotalRenta = DatosTipoFactura.Cantidad;
                    NuevoTotalGeneralFacturacion.TotalGeneralRenta += DatosTipoFactura.Cantidad;
                    break;
            }
        }

        public void AgregarTotalesATabla(string Cliente)
        {
            var pe = new Pdf();
            
            PdfPCell clCliente = new PdfPCell(new Phrase( Cliente,pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 2 };
            PdfPCell clTotalToner = new PdfPCell(new Phrase("$" + String.Format("{0:n2}", NuevoTotalFacturas.TotalToner), pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            PdfPCell clTotalServicios = new PdfPCell(new Phrase("$" + String.Format("{0:n2}", NuevoTotalFacturas.TotalServicios), pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            PdfPCell clTotalRenta = new PdfPCell(new Phrase("$" + String.Format("{0:n2}", NuevoTotalFacturas.TotalRenta), pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };

            tblFacturas.AddCell(clCliente);
            tblFacturas.AddCell(clTotalToner);
            tblFacturas.AddCell(clTotalServicios);
            tblFacturas.AddCell(clTotalRenta);
        }

        public void AgregarTotalesGeneralesATabla()
        {
            var pe = new Pdf();
            PdfPCell clCliente = new PdfPCell(new Phrase("TOTALES", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 2 };
            PdfPCell clTotalToner = new PdfPCell(new Phrase("$" + String.Format("{0:n2}", NuevoTotalGeneralFacturacion.TotalGeneralToner), pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            PdfPCell clTotalServicios = new PdfPCell(new Phrase("$" + String.Format("{0:n2}", NuevoTotalGeneralFacturacion.TotalGeneralServicios), pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            PdfPCell clTotalRenta = new PdfPCell(new Phrase("$" + String.Format("{0:n2}", NuevoTotalGeneralFacturacion.TotalGeneralRenta), pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };

            tblFacturas.AddCell(clCliente);
            tblFacturas.AddCell(clTotalToner);
            tblFacturas.AddCell(clTotalServicios);
            tblFacturas.AddCell(clTotalRenta);
        }

        public void CrearReporteFacturas(Reporte NuevoReporte)
        {
            foreach (DataRow fila in tblDatosReporte.Rows)
            {
                DatosPdfFactura DatosFactura = new DatosPdfFactura()
                {
                    NumeroFactura = fila[0].ToString(),
                    NombreCliente = fila[1].ToString(),
                    Cantidad = double.Parse(fila[2].ToString()),
                    FechaFactura = DateTime.Parse(fila[3].ToString()),
                    FechaVencimiento = DateTime.Parse(fila[4].ToString()),
                    Observaciones = fila[6].ToString(),
                    Saldo = double.Parse(fila[8].ToString())
                };
                string FechaPromesaPago = fila[5].ToString();
                ColocarFechaPromesaPago(DatosFactura, FechaPromesaPago);
                AgregarClienteAlDocumento(DatosFactura);
            }
            AgregarTotalFacturas();
            document.Add(tblFacturas);
            ColocarTotalGeneral(NuevoReporte);

            lstClientes.Clear();
            TotalFacturas = 0;
            TotalSaldo = 0;

            TotalGeneralFacturas = 0;
        }

        public void ColocarFechaPromesaPago(DatosPdfFactura DatosFactura, string FechaPromesaPago)
        {
            if(FechaPromesaPago != "")
            {
                DatosFactura.FechaPromesaPago = DateTime.Parse(FechaPromesaPago);
            }
            else
            {
                DatosFactura.FechaPromesaPago = DatosFactura.FechaVencimiento;
            }
        }

        public string ColocarNombreReporte(Reporte NuevoReporte)
        {
            StringBuilder Titulo = new StringBuilder("REPORTE ");
            if (NuevoReporte.Cliente != "" && NuevoReporte.TipoBusqueda != "TIPOS FACTURAS")
            {
                Titulo.Append(NuevoReporte.TipoBusqueda).Append("\n").Append(NuevoReporte.Cliente);
            }
            else
            {
                Titulo.Append(NuevoReporte.TipoBusqueda);
            }
            return Titulo.ToString();
        }

        public void AgregarClienteAlDocumento(DatosPdfFactura DatosFactura)
        {
            Pdf pe = new Pdf();
            if (!lstClientes.Contains(DatosFactura.NombreCliente))
            {
                if(lstClientes.Count > 0)
                {
                    AgregarTotalFacturas();
                    document.Add(tblFacturas);
                    TotalFacturas = 0;
                    TotalSaldo = 0;
                }
                lstClientes.Add(DatosFactura.NombreCliente);
                document.Add(new Paragraph(DatosFactura.NombreCliente, pe.FuenteFecha));
                CrearTablaFacturas();
                AgregarDatosATablaFacturas(DatosFactura);
            }
            else
            {
                AgregarDatosATablaFacturas(DatosFactura);
            }
        }

        public void CrearTablaFacturas()
        {
            Pdf pdf = new Pdf();
            tblFacturas = new PdfPTable(6) { HorizontalAlignment = Element.ALIGN_LEFT, WidthPercentage = 80, PaddingTop = 10f };
            
            //Encabezados
            PdfPCell clEncabezadoFactura = new PdfPCell(new Phrase("FACTURA", pdf.FuenteParrafoBold)) { BorderWidth = .5f };
            PdfPCell clEncabezadoFechaFactura = new PdfPCell(new Phrase("FECHA FACTURA", pdf.FuenteParrafoBold)) { BorderWidth = .5f };
            PdfPCell clEncabezadoFechaVencimiento = new PdfPCell(new Phrase("FECHA DE VENCIMIENTO", pdf.FuenteParrafoBold)) { BorderWidth = .5f };
            PdfPCell clEncabezadoFechaPromesaPago = new PdfPCell(new Phrase("FECHA PROMESA PAGO", pdf.FuenteParrafoBold)) { BorderWidth = .5f };
            PdfPCell clEncabezadoImporte = new PdfPCell(new Phrase("IMPORTE", pdf.FuenteParrafoBold)) { BorderWidth = .5f };
            PdfPCell clEncabezadoSaldo = new PdfPCell(new Phrase("SALDO", pdf.FuenteParrafoBold)) { BorderWidth = .5f };

            tblFacturas.AddCell(clEncabezadoFactura);
            tblFacturas.AddCell(clEncabezadoFechaFactura);
            tblFacturas.AddCell(clEncabezadoFechaVencimiento);
            tblFacturas.AddCell(clEncabezadoFechaPromesaPago);
            tblFacturas.AddCell(clEncabezadoImporte);
            tblFacturas.AddCell(clEncabezadoSaldo);
        }

        public void AgregarDatosATablaFacturas(DatosPdfFactura DatosFactura)
        {
            var pe = new Pdf();
            PdfPCell clFactura = new PdfPCell(new Phrase(DatosFactura.NumeroFactura, pe.FuenteParrafo)) { BorderWidth = .5f , Colspan =1};
            PdfPCell clFechaFactura = new PdfPCell(new Phrase(DatosFactura.FechaFactura.ToString("dd/MM/yyyy"), pe.FuenteParrafo)) { BorderWidth = .5f , Colspan =1};
            PdfPCell clFechaVencimiento = new PdfPCell(new Phrase(DatosFactura.FechaVencimiento.ToString("dd/MM/yyyy"), pe.FuenteParrafo)) { BorderWidth = .5f , Colspan =1};
            PdfPCell clFechaPromesaPago;
            if (DatosFactura.FechaPromesaPago.HasValue)
            {
                clFechaPromesaPago = new PdfPCell(new Phrase(DatosFactura.FechaPromesaPago.Value.ToString("dd/MM/yyyy"), pe.FuenteParrafo)) { BorderWidth = .5f , Colspan =1};
            }
            else
            {
                clFechaPromesaPago = new PdfPCell(new Phrase("", pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };
            }
            PdfPCell clImporte = new PdfPCell(new Phrase("$" + String.Format("{0:n2}", DatosFactura.Cantidad), pe.FuenteParrafo)) { BorderWidth = .5f , Colspan =1};
            PdfPCell clSaldo = new PdfPCell(new Phrase("$" + String.Format("{0:n2}", DatosFactura.Saldo), pe.FuenteParrafo)) { BorderWidth = .5f , Colspan =1};
            
            tblFacturas.AddCell(clFactura);
            tblFacturas.AddCell(clFechaFactura);
            tblFacturas.AddCell(clFechaVencimiento);
            tblFacturas.AddCell(clFechaPromesaPago);
            tblFacturas.AddCell(clImporte);
            tblFacturas.AddCell(clSaldo);
            PdfPCell clObservacion = new PdfPCell(new Phrase(DatosFactura.Observaciones, pe.FuenteParrafo)) { BorderWidth = .5f , Colspan =6};
            tblFacturas.AddCell(clObservacion);

            //Sumar cantidad total
            TotalFacturas += DatosFactura.Cantidad;
            TotalSaldo += DatosFactura.Saldo;
            TotalGeneralFacturas += DatosFactura.Saldo;
            
            
        }

        public void ColocarSaldoExistente(DatosPdfFactura DatosFactura)
        {
            var pe = new Pdf();

            //ENCABEZADOS
            PdfPCell clTituloVacio = new PdfPCell(new Phrase("", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 3 };
            PdfPCell clTituloFechaPago = new PdfPCell(new Phrase("FECHA DE PAGO", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            PdfPCell clTituloImporte = new PdfPCell(new Phrase("", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            PdfPCell clTituloSaldo = new PdfPCell(new Phrase("", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            tblFacturas.AddCell(clTituloVacio);
            tblFacturas.AddCell(clTituloFechaPago);
            tblFacturas.AddCell(clTituloImporte);
            tblFacturas.AddCell(clTituloSaldo);

            if (ObtenerDatosPagoFactura(DatosFactura))
            {
                foreach (DataRow fila in tblDatosPagosFactura.Rows)
                {
                    PdfPCell clVacio = new PdfPCell(new Phrase("", pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 3 };
                    DateTime FechaPago = DateTime.Parse(fila[0].ToString());
                    PdfPCell clFechaPago = new PdfPCell(new Phrase(FechaPago.ToString("dd/MM/yyyy"), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };
                    double Importe = double.Parse(fila[1].ToString());
                    PdfPCell clImporte = new PdfPCell(new Phrase("$" + String.Format("{0:n2}", Importe), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };
                    double SaldoTotal = DatosFactura.Cantidad - Importe;
                    PdfPCell clSaldo = new PdfPCell(new Phrase("$" + String.Format("{0:n2}", SaldoTotal.ToString()), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };
                    tblFacturas.AddCell(clVacio);
                    tblFacturas.AddCell(clFechaPago);
                    tblFacturas.AddCell(clImporte);
                    tblFacturas.AddCell(clSaldo);
                }
            }
        }

        public void AgregarTotalFacturas()
        {
            var pe = new Pdf();
            PdfPCell clEspacio = new PdfPCell(new Phrase("", pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 3 };
            PdfPCell clTotal = new PdfPCell(new Phrase("TOTAL", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            PdfPCell clCantidadTotal = new PdfPCell(new Phrase("$" + String.Format("{0:n2}", TotalFacturas), pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            PdfPCell clCantidadSaldo = new PdfPCell(new Phrase("$" + String.Format("{0:n2}", TotalSaldo), pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };

            tblFacturas.AddCell(clEspacio);
            tblFacturas.AddCell(clTotal);
            tblFacturas.AddCell(clCantidadTotal);
            tblFacturas.AddCell(clCantidadSaldo);
        }

        public void ColocarTotalGeneral(Reporte NuevoReporte)
        {
            Pdf pe = new Pdf();

            //Sumar los gastos fantasma
            if(NuevoReporte.TipoBusqueda == "SALDOS POR CLIENTE")
            {
                if(NuevoReporte.Cliente == "")
                {
                    TotalGeneralFacturas += 7905.65;
                }
            }
            

            Paragraph CantidadReportes = new Paragraph("TOTAL $" + String.Format("{0:n2}", TotalGeneralFacturas), pe.FuenteFecha) { Alignment = Element.ALIGN_CENTER };
            document.Add(CantidadReportes);
        }
    }
}
