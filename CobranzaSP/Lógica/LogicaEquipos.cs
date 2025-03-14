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
using SpreadsheetLight;
using System.Xml;
using CobranzaSP.Formularios;
using DocumentFormat.OpenXml.Office2013.Excel;
using System.Windows.Forms;

namespace CobranzaSP.Lógica
{
    internal class LogicaEquipos
    {
        private CD_Conexion conexion = new CD_Conexion();
        SqlCommand comando = new SqlCommand();
        //PDF
        Document document;
        Reporte NuevoReporte;
        PdfPTable tblDatosEquipos; //Tabla para colocar datos dentro del PDFA

        //Tablas para guardar datos
        DataTable DatosEquiposRenta = new DataTable();
        DataTable tblPreciosTotalesEquipos = new DataTable();

        //Listas
        SortedSet<string> lstMarcas = new SortedSet<string>();
        SortedSet<string> lstModelos = new SortedSet<string>();
        SortedSet<string> lstClientes = new SortedSet<string>();

        #region CRUD
        public string GuardarEquipo(Equipo nuevoEquipo)
        {
            string Accion;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "GuardarEquipoRentado";
            comando.CommandType = CommandType.StoredProcedure;

            Accion = (nuevoEquipo.IdEquipo > 0) ? "modificado" : "agregado";

            comando.Parameters.AddWithValue("@Id", nuevoEquipo.IdEquipo);
            comando.Parameters.AddWithValue("@IdCliente", nuevoEquipo.IdCliente);
            comando.Parameters.AddWithValue("@Ubicacion", nuevoEquipo.Ubicacion);
            comando.Parameters.AddWithValue("@IdMarca", nuevoEquipo.IdMarca);
            comando.Parameters.AddWithValue("@IdModelo", nuevoEquipo.IdModelo);
            comando.Parameters.AddWithValue("@Serie", nuevoEquipo.Serie);
            comando.Parameters.AddWithValue("@IdRenta", nuevoEquipo.IdRenta);
            comando.Parameters.AddWithValue("@Precio", nuevoEquipo.PrecioRenta);
            comando.Parameters.AddWithValue("@Fecha_Pago", nuevoEquipo.FechaPago);
            comando.Parameters.AddWithValue("@Valor", nuevoEquipo.Valor);
            comando.Parameters.AddWithValue("@IdSerie", nuevoEquipo.IdSerie);

            comando.ExecuteNonQuery();

            comando.Parameters.Clear();
            conexion.CerrarConexion();
            return "Equipo " + Accion + " correctamente";
        }

        public void GuardarEquipoVendido(Equipo nuevoEquipo)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "AgregarEquipoVendido";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@IdMarca", nuevoEquipo.IdMarca);
            comando.Parameters.AddWithValue("@IdModelo", nuevoEquipo.IdModelo);
            comando.Parameters.AddWithValue("@IdSerie", nuevoEquipo.IdSerie);
            comando.Parameters.AddWithValue("@Fecha", nuevoEquipo.FechaVenta);
            comando.Parameters.AddWithValue("@Precio", nuevoEquipo.PrecioRenta);
            comando.Parameters.AddWithValue("@IdCliente", nuevoEquipo.IdCliente);

            comando.ExecuteNonQuery();


            conexion.CerrarConexion();
        }
        #endregion

        
        public bool ObtenerDatosReporteEquipos(Reporte NuevoReporte, string sp)
        {
            bool DatosObtenidos = true;
            this.NuevoReporte = NuevoReporte;
            DatosEquiposRenta.Clear();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = sp;
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@ParametroBusqueda", NuevoReporte.ParametroBusqueda);
            comando.Parameters.AddWithValue("@Cliente", NuevoReporte.Cliente);
            DatosEquiposRenta.Load(comando.ExecuteReader());
            comando.Dispose();
            
            //Validar que contenga informacion la tabla
            if (DatosEquiposRenta.Rows.Count == 0)
            {
                return DatosObtenidos = false;
            }

            //Comenzar a crear el pdf
            Pdf();
            return DatosObtenidos;
        }

        #region PDF
        public void Pdf()
        {
            //Ruta donde se guardara el pdf
            string RutaArchivo = ConfiguracionPdf.RutaReportesEquiposRenta;
            string NombreArchivo = RutaArchivo + "ReporteEquipos" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf";
            //string NombreArchivo = @"\\administracion-pc\ARCHIVOS COMPARTIDOS\Reportes\Equipos\" + "ReporteEquipos" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf";

            //Configuracion de pdf
            FileStream fs = new FileStream(NombreArchivo, FileMode.Create);
            document = new Document(PageSize.LETTER);
            document.SetMargins(25f, 25f, 25f, 25f);
            //Colocamos el pdf en horizontal
            document.SetPageSize(iTextSharp.text.PageSize.LETTER.Rotate());
            PdfWriter pw = PdfWriter.GetInstance(document, fs);

            //Instanciamos la clase para la paginacion
            var pe = new Pdf();
            pe.ColocarFormatoSuperior = true;
            pw.PageEvent = pe;

            document.Open();

            //Colocar el titulo del reporte
            string NombreReporte = ColocarTituloReporte();
            Paragraph titulo = new Paragraph(NombreReporte, pe.FuenteTitulo) { Alignment = Element.ALIGN_CENTER };
            document.Add(titulo);
            document.Add(new Chunk("\n"));

            //Determinar tipo de reporte a generar
            switch (NuevoReporte.TipoBusqueda)
            {
                case "Cliente":
                    ReporteEquiposPorCliente();
                    break;
                case "Precios de Equipos":
                    ReporteEquiposRentaPrecios();
                    break;
            }

            //Agregar ultimo registro
            document.Add(tblDatosEquipos);

            if (NuevoReporte.TipoBusqueda == "Precios de Equipos")
            {
                //Creacion de tabla de Totales
                ColocarTotales();
            }

            //Reiniciar
            lstClientes.Clear();
            lstMarcas.Clear();
            lstModelos.Clear();
            document.Close();


            //Abrimos el pdf 
            pe.AbrirPdf(NombreArchivo);
        }

        public string ColocarTituloReporte()
        {
            StringBuilder TituloBuilder = new StringBuilder("EQUIPOS EN RENTA ");

            switch (NuevoReporte.TipoBusqueda)
            {
                case "Cliente":
                    string EncabezadoCliente = (NuevoReporte.Cliente != "") ? "CLIENTE: " + NuevoReporte.Cliente.ToUpper() : "CLIENTES";
                    TituloBuilder.Append(EncabezadoCliente);
                    TituloBuilder.Append(ColocarSubtituloMarcaModelo());
                    break;
                case "Precios de Equipos":
                    TituloBuilder.Append("VALOR DE EQUIPOS EN RENTA");
                    TituloBuilder.Append(ColocarSubtituloMarcaModelo());
                    break;
            }

            return TituloBuilder.ToString();
        }

        public string ColocarSubtituloMarcaModelo()
        {
            StringBuilder Subtitulo = new StringBuilder();
            if (NuevoReporte.ParametroBusqueda != "")
            {
                switch (NuevoReporte.TipoBusquedaAdicional)
                {
                    case "Marca": Subtitulo.Append("\nMARCA: "); break;
                    case "Modelo": Subtitulo.Append("\nMODELO: "); break;
                }
                Subtitulo.Append(NuevoReporte.ParametroBusqueda.ToUpper());
            }

            return Subtitulo.ToString();
        }

        public void ColocarTotales()
        {
            var pe = new Pdf();
            int CantidadTotal = 0;
            double PrecioTotal = 0;

            Paragraph titulo = new Paragraph("TOTAL", pe.FuenteTitulo) { Alignment = Element.ALIGN_CENTER };
            document.Add(titulo);

            //Creación de tabla y titulos de tabla
            tblDatosEquipos = new PdfPTable(3);
            PdfPCell clTipo = new PdfPCell();
            clTipo = new PdfPCell(new Phrase("MARCA", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };

            if (NuevoReporte.TipoBusquedaAdicional == "Modelo")
            {
                clTipo = new PdfPCell(new Phrase("MODELO", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            }

            PdfPCell clPrecio = new PdfPCell(new Phrase("PRECIO", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            PdfPCell clCantidad = new PdfPCell(new Phrase("CANTIDAD", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            tblDatosEquipos.AddCell(clTipo);
            tblDatosEquipos.AddCell(clPrecio);
            tblDatosEquipos.AddCell(clCantidad);

            ObtenerPreciosTotalesEquipos();

            foreach (DataRow fila in tblPreciosTotalesEquipos.Rows)
            {
                PdfPCell clDatoTipo = (NuevoReporte.TipoBusquedaAdicional == "Modelo") ? new PdfPCell(new Phrase(NuevoReporte.ParametroBusqueda, pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 } : new PdfPCell(new Phrase(fila[0].ToString(), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };
                PdfPCell clDatosTotal = new PdfPCell(new Phrase("$" + String.Format("{0:n2}", double.Parse(fila[1].ToString())), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };
                PdfPCell clDatosCantidad = new PdfPCell(new Phrase(fila[2].ToString(), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };
                PrecioTotal += double.Parse(fila[1].ToString().Replace("$", "").Replace(",", ""));
                CantidadTotal += int.Parse(fila[2].ToString());
                tblDatosEquipos.AddCell(clDatoTipo);
                tblDatosEquipos.AddCell(clDatosTotal);
                tblDatosEquipos.AddCell(clDatosCantidad);
            }

            //Agregar Total General
            PdfPCell clVacio = new PdfPCell(new Phrase("", pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };
            PdfPCell clSumaTotal = new PdfPCell(new Phrase("$" + String.Format("{0:n2}", PrecioTotal), pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            PdfPCell clCantidadTotal = new PdfPCell(new Phrase(CantidadTotal.ToString(), pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            tblDatosEquipos.AddCell(clVacio);
            tblDatosEquipos.AddCell(clSumaTotal);
            tblDatosEquipos.AddCell(clCantidadTotal);

            document.Add(tblDatosEquipos);
        }

        public void ObtenerPreciosTotalesEquipos()
        {
            tblPreciosTotalesEquipos.Clear();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "ObtenerTotalPreciosEquiposEnRenta";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@Parametro", NuevoReporte.ParametroBusqueda);
            tblPreciosTotalesEquipos.Load(comando.ExecuteReader());
            comando.Dispose();
        }
        #endregion 

        #region ReporteCliente
        public void ReporteEquiposPorCliente()
        {
            Pdf pe = new Pdf();
            tblDatosEquipos = new PdfPTable(5);

            //Recorrer informacion obtenida de la consulta a la base de datos
            foreach (DataRow fila in DatosEquiposRenta.Rows)
            {
                //Guardar cada registro en un objeto
                PdfEquipoRentado DatosEquipo = new PdfEquipoRentado()
                {
                    Cliente = fila[1].ToString(),
                    Marca = fila[3].ToString(),
                    Modelo = fila[4].ToString(),
                    Serie = fila[5].ToString(),
                    TipoRenta = fila[6].ToString(),
                    PrecioRenta = double.Parse(fila[7].ToString()),
                    FechaPago = fila[8].ToString()
                };

                //Validar que el cliente no este en la lista
                if (!lstClientes.Contains(DatosEquipo.Cliente))
                {
                    //Reinicio de marcas
                    lstMarcas.Clear();
                    document.Add(tblDatosEquipos);//Agregar la anterior tabla al documento
                    tblDatosEquipos = new PdfPTable(5);
                    //Agregamos el cliente al documento y a la lista
                    if (NuevoReporte.Cliente == "")
                    {
                        Paragraph Cliente = new Paragraph(DatosEquipo.Cliente, pe.FuenteParrafoGrande) { Alignment = Element.ALIGN_LEFT };
                        document.Add(Cliente);
                    }
                    lstClientes.Add(DatosEquipo.Cliente);


                    AgregarMarcaAlDocumento(DatosEquipo);
                }
                else //En caso de que el cliente este en la lista, se agregan más registros a dicho cliente
                {
                    AgregarMarcaAlDocumento(DatosEquipo);
                }
            }
        }

        public void AgregarMarcaAlDocumento(PdfEquipoRentado DatosReporte)
        {
            var pe = new Pdf();
            if (!lstMarcas.Contains(DatosReporte.Marca))
            {
                document.Add(tblDatosEquipos);
                //Solo colocaremos la marca en el documento si el tipo de busqueda adicional no es marca o modelo
                if (NuevoReporte.TipoBusquedaAdicional != "Marca" && NuevoReporte.TipoBusquedaAdicional != "Modelo")
                {
                    //Agregamos la marca al documento y a la lista
                    Paragraph Marca = new Paragraph(DatosReporte.Marca, pe.FuenteParrafoGrande) { Alignment = Element.ALIGN_LEFT };
                    document.Add(Marca);
                }
                lstMarcas.Add(DatosReporte.Marca);
                CrearTablaDatosEquipos(DatosReporte);
            }
            else
            {
                AgregarDatosATabla(DatosReporte);
            }
        }

        public void CrearTablaDatosEquipos(PdfEquipoRentado reporte)
        {
            tblDatosEquipos = new PdfPTable(4);
            if (NuevoReporte.TipoBusquedaAdicional != "Modelo")
            {
                tblDatosEquipos = new PdfPTable(5);
                AgregarTituloColumnaTabla("MODELO", 1);
            }
            AgregarTituloColumnaTabla("SERIE", 1);
            AgregarTituloColumnaTabla("TIPO RENTA", 1);
            AgregarTituloColumnaTabla("PRECIO", 1);
            AgregarTituloColumnaTabla("FECHA DE PAGO", 1);

            AgregarDatosATabla(reporte);
        }

        public void AgregarDatosATabla(PdfEquipoRentado DatosEquipo)
        {
            Pdf pe = new Pdf();
            //if(TipoBusqueda != "Modelo")
            if (NuevoReporte.TipoBusquedaAdicional != "Modelo")
            {
                PdfPCell clModeloDato = new PdfPCell(new Phrase(DatosEquipo.Modelo, pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
                tblDatosEquipos.AddCell(clModeloDato);
            }
            PdfPCell clSerieDato = new PdfPCell(new Phrase(DatosEquipo.Serie, pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            PdfPCell clTipoPagoDato = new PdfPCell(new Phrase(DatosEquipo.TipoRenta, pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            PdfPCell clPrecioDato = new PdfPCell(new Phrase("$" + DatosEquipo.PrecioRenta, pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };
            PdfPCell clFechaPagoDato = new PdfPCell(new Phrase(DatosEquipo.FechaPago, pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };

            tblDatosEquipos.AddCell(clSerieDato);
            tblDatosEquipos.AddCell(clTipoPagoDato);
            tblDatosEquipos.AddCell(clPrecioDato);
            tblDatosEquipos.AddCell(clFechaPagoDato);
        }
        #endregion

        #region ReporteEquiposRentaPrecios
        public void ReporteEquiposRentaPrecios()
        {
            Pdf pe = new Pdf();
            tblDatosEquipos = new PdfPTable(5);

            foreach (DataRow fila in DatosEquiposRenta.Rows)
            {
                PdfEquipoRentado equipo = new PdfEquipoRentado()
                {
                    Marca = fila[3].ToString(),
                    Modelo = fila[4].ToString(),
                    Serie = fila[5].ToString(),
                    PrecioRenta = double.Parse(fila[7].ToString()),
                    Cliente = fila[1].ToString()
                };

                ColocarSerieADocumento(equipo);
            }
        }

        public void ColocarSerieADocumento(PdfEquipoRentado equipo)
        {
            var pe = new Pdf();

            //Verificar que la marca no este en la lista
            if (!lstMarcas.Contains(equipo.Marca))
            {
                document.Add(tblDatosEquipos);
                tblDatosEquipos = (NuevoReporte.TipoBusquedaAdicional == "Modelo") ? new PdfPTable(5) : new PdfPTable(6);

                //Agregamos la marca al documento y a la lista
                lstMarcas.Add(equipo.Marca);
                if (NuevoReporte.TipoBusquedaAdicional != "Marca")
                {
                    Paragraph Marca = new Paragraph(equipo.Marca, pe.FuenteParrafoGrande) { Alignment = Element.ALIGN_LEFT };
                    document.Add(Marca);
                }

                CrearTablaPreciosEquipos(equipo);

            }
            else//En caso de que si se encuentre la marca solo seguiremos agregando a la tabla
            {
                AgregarModelosATabla(equipo);
            }
        }

        public void CrearTablaPreciosEquipos(PdfEquipoRentado reporte)
        {
            tblDatosEquipos = new PdfPTable(5);
            //if(TipoBusqueda != "Modelo")
            if (NuevoReporte.TipoBusqueda != "Modelo")
            {
                tblDatosEquipos = new PdfPTable(6);
                AgregarTituloColumnaTabla("MODELO", 1);
            }
            AgregarTituloColumnaTabla("CLIENTE", 2);
            AgregarTituloColumnaTabla("SERIE", 2);
            AgregarTituloColumnaTabla("PRECIO", 1);

            AgregarModelosATabla(reporte);
        }

        public void AgregarTituloColumnaTabla(string celda, int colspan)
        {
            Pdf pdf = new Pdf();
            PdfPCell clTitulo = new PdfPCell(new Phrase(celda, pdf.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = colspan };
            tblDatosEquipos.AddCell(clTitulo);
        }
        public void AgregarModelosATabla(PdfEquipoRentado equipo)
        {
            var pe = new Pdf();

            if (NuevoReporte.TipoBusqueda != "Modelo")
            {
                PdfPCell clModelo = new PdfPCell(new Phrase(equipo.Modelo, pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };
                tblDatosEquipos.AddCell(clModelo);
            }
            PdfPCell clCliente = new PdfPCell(new Phrase(equipo.Cliente, pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 2 };
            PdfPCell clSerie = new PdfPCell(new Phrase(equipo.Serie, pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 2 };
            PdfPCell clPrecio = new PdfPCell(new Phrase("$" + String.Format("{0:n2}", equipo.PrecioRenta), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };

            tblDatosEquipos.AddCell(clCliente);
            tblDatosEquipos.AddCell(clSerie);
            tblDatosEquipos.AddCell(clPrecio);
        }
        #endregion
    }
}
