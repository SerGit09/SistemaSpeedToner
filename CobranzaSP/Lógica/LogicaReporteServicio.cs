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

namespace CobranzaSP.Lógica
{
    internal class LogicaReporteServicio
    {
        private CD_Conexion conexion = new CD_Conexion();
        private AccionesLógica NuevaAccion = new AccionesLógica();
        SqlCommand comando = new SqlCommand();
        SqlDataReader reporte;
        SqlDataReader DatosModulos;
        SortedSet<string> lstSeries = new SortedSet<string>();
        SortedSet<string> lstClientes = new SortedSet<string>();
        SortedSet<string> lstFechas = new SortedSet<string>();
        PdfPTable tblSeries;
        List<string> Fusores = new List<string>();
        bool RangoHabilitado = false;

        //Cambiar el orden en que se mostraran los datos
        bool EsReporteMantenimientos = false;
        string TipoBusqueda;
        string Cliente;
        int NumeroReportes = 0;
        DataTable primerDataTable;

        Document document;
        Pdf pe;

        #region DatosReportesServiciosGenerales
        public bool DeterminarTipoReporte(Reporte nuevoReporte)
        {
            bool DatosObtenidos = true;
            this.Cliente = Cliente;
            primerDataTable = new DataTable();
            //EL TIPO DE BUSQUEDA DETERMINARA LOS DATOS QUE SE BUSCARAN EN LA BASE DE DATOS
            switch (nuevoReporte.TipoBusqueda.ToUpper())
            {
                case "FUSOR": DatosObtenidos = ObtenerDatosReporteFusor(nuevoReporte); break;
                default: DatosObtenidos = ObtenerDatosReporte(nuevoReporte); break;
            }

            //ObtenerDatosReporteFusorPrueba

            return DatosObtenidos;
        }

        public bool ObtenerDatosReporte(Reporte NuevoReporte)
        {
            bool DatosObtenidos = true;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "GenerarReporteServicios";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@FechaInicio", NuevoReporte.FechaInicio);
            comando.Parameters.AddWithValue("@FechaFinal", NuevoReporte.FechaFinal);
            comando.Parameters.AddWithValue("@CampoBusqueda", NuevoReporte.ParametroBusqueda);
            comando.Parameters.AddWithValue("@RangoHabilitado", NuevoReporte.RangoHabilitado);
            comando.Parameters.AddWithValue("@Cliente", NuevoReporte.Cliente);
            primerDataTable.Load(comando.ExecuteReader());
            comando.Dispose();

            if (primerDataTable.Rows.Count == 0)
            {
                return DatosObtenidos = false;
            }
            Pdf(NuevoReporte);
            return DatosObtenidos;
        }

        public bool ObtenerDatosReporteFusor(Reporte NuevoReporte)
        {
            bool DatosObtenidos = true;
            
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "DatosReporteServiciosPorFusor";
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@Fusor", NuevoReporte.ParametroBusqueda);
            comando.Parameters.AddWithValue("@FechaInicio", NuevoReporte.FechaInicio);
            comando.Parameters.AddWithValue("@FechaFinal", NuevoReporte.FechaFinal);
            comando.Parameters.AddWithValue("@RangoHabilitado", NuevoReporte.RangoHabilitado);
            comando.CommandType = CommandType.StoredProcedure;
            //reporte = comando.ExecuteReader();
            primerDataTable.Load(comando.ExecuteReader());
            comando.Dispose();

            if (primerDataTable.Rows.Count == 0)
            {
                return DatosObtenidos = false;
            }

            //Comenzamos a generar el pdf
            Pdf(NuevoReporte);

            return DatosObtenidos;

        }
        #endregion

        #region DatosReportesServiciosRicoh
        public bool DeterminarTipoReporteRicoh(Reporte nuevoReporte)
        {
            bool DatosObtenidos = true;
            EsReporteMantenimientos = false;
            primerDataTable = new DataTable();
            switch (nuevoReporte.TipoBusqueda)
            {
                case "MANTENIMIENTOS": DatosObtenidos = ObtenerDatosReporteMantenimientosRicoh(nuevoReporte); break;
                default: DatosObtenidos = ObtenerDatosReporteRicoh(nuevoReporte); break;
            }
            return DatosObtenidos;
        }
        public bool ObtenerDatosReporteMantenimientosRicoh(Reporte NuevoReporte)
        {
            bool DatosObtenidos = true;
            EsReporteMantenimientos = true;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "DatosReporteMantenimientosRicoh";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@FechaInicio", NuevoReporte.FechaInicio);
            comando.Parameters.AddWithValue("@FechaFinal", NuevoReporte.FechaFinal);
            comando.Parameters.AddWithValue("@Cliente", NuevoReporte.Cliente);
            comando.Parameters.AddWithValue("@CampoBusqueda", NuevoReporte.ParametroBusqueda);
            primerDataTable.Load(comando.ExecuteReader());
            comando.Dispose();

            if (primerDataTable.Rows.Count == 0)
            {
                return DatosObtenidos = false;
            }
            Pdf(NuevoReporte);
            return DatosObtenidos;
        }

        public bool ObtenerDatosReporteRicoh(Reporte NuevoReporte)
        {
            bool DatosObtenidos = true;
            EsReporteMantenimientos = false;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "DatosReporteServicioRicoh";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@FechaInicio", NuevoReporte.FechaInicio);
            comando.Parameters.AddWithValue("@FechaFinal", NuevoReporte.FechaFinal);
            comando.Parameters.AddWithValue("@Cliente", NuevoReporte.Cliente);
            comando.Parameters.AddWithValue("@CampoBusqueda", NuevoReporte.ParametroBusqueda);
            comando.Parameters.AddWithValue("@RangoHabilitado", NuevoReporte.RangoHabilitado);
            primerDataTable.Load(comando.ExecuteReader());
            comando.Dispose();

            if (primerDataTable.Rows.Count == 0)
            {
                return DatosObtenidos = false;
            }
            Pdf(NuevoReporte);
            return DatosObtenidos;
        }
        #endregion


        //Nos ayuda a determinar si el reporte estara de acuerdo a un rango de fecha o no

        public void Pdf(Reporte NuevoReporte)
        {
            //string NombreArchivo = @"\\DESKTOP-34U2P48\Archivos Compartidos\Reportes\Servicios\" + NuevoReporte.TipoBusqueda + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf";
            string NombreArchivo = DefinirNombreArchivoPdf(NuevoReporte);
            bool ColocarTotal = false;
            FileStream fs = new FileStream(NombreArchivo, FileMode.Create);
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
            string NombreReporte = ColocarNombreReporte(NuevoReporte);
            Paragraph titulo = new Paragraph(NombreReporte, pe.FuenteTitulo) { Alignment = Element.ALIGN_CENTER };
            document.Add(titulo);

            //Verificamos si tenemos rango de fecha
            if (NuevoReporte.RangoHabilitado || NuevoReporte.TipoBusqueda == "FECHA")
            {
                Paragraph Fechas = new Paragraph("DEL " + NuevoReporte.FechaInicio.ToString("dd/MM/yyyy") + " AL " + NuevoReporte.FechaFinal.ToString("dd/MM/yyyy"), pe.FuenteFecha) { Alignment = Element.ALIGN_CENTER };
                document.Add(Fechas);
            }

            //A partir de aqui va a variar el contenido de los pdfs, dependiendo cual se eligio
            switch (NuevoReporte.TipoBusqueda.ToUpper())
            {
                //case "FUSORES":GenerarReporteFusor(document); break;
                case "FUSOR": GenerarReporteFusor(); break;
                default: GenerarReporte( NuevoReporte); ColocarTotal = true; break;
            }

            //AGREGAR NUMERO DE REPORTES
            if (ColocarTotal)
            {
                Paragraph CantidadReportes = new Paragraph("TOTAL DE REPORTES " + NumeroReportes, pe.FuenteFecha) { Alignment = Element.ALIGN_CENTER };
                document.Add(CantidadReportes);
            }

            lstClientes.Clear();
            lstSeries.Clear();
            //reporte.Close();
            document.Close();
            NumeroReportes = 0;


            //Abrimos el pdf 
            pe.AbrirPdf(NombreArchivo);
        }

        public string DefinirNombreArchivoPdf(Reporte NuevoReporte)
        {
            //@"\\DESKTOP-34U2P48\Archivos Compartidos\Reportes\Servicios\" + NuevoReporte.TipoBusqueda + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf";
            StringBuilder NombreArchivo = new StringBuilder(@"\\DESKTOP-34U2P48\Archivos Compartidos\Reportes\Servicios\");
            switch (NuevoReporte.TipoBusqueda.ToUpper())
            {
                case "CLIENTE": NombreArchivo.Append(NuevoReporte.TipoBusqueda).Append("-").Append(NuevoReporte.Cliente); break;
                case "SERIE": NombreArchivo.Append(NuevoReporte.TipoBusqueda).Append("-").Append(NuevoReporte.ParametroBusqueda); break;
                //case "FUSORES": TituloReporte.Append(" FUSORES"); break;
                case "FUSOR":
                    if (NuevoReporte.ParametroBusqueda == " ")
                    {
                        NombreArchivo.Append("FUSORES");
                    }
                    else
                    {
                        NombreArchivo.Append(NuevoReporte.TipoBusqueda).Append("-").Append(NuevoReporte.ParametroBusqueda);
                    }
                    break;
                case "MARCA": NombreArchivo.Append(NuevoReporte.TipoBusqueda).Append("-").Append(NuevoReporte.ParametroBusqueda); break;
                case "MODELO": NombreArchivo.Append(NuevoReporte.TipoBusqueda).Append("-").Append(NuevoReporte.ParametroBusqueda); break;
                case "MANTENIMIENTOS":
                    NombreArchivo.Append("REPORTE-MANTENIMIENTO-RICOH");
                    break;
            }
            string Fecha = DateTime.Now.ToString("ddMMyyyyHHmmss");
            return NombreArchivo.Append("-").Append(Fecha).Append(".pdf").ToString();
        }

        public string ColocarNombreReporte(Reporte NuevoReporte)
        {
            StringBuilder TituloReporte = new StringBuilder("REPORTE SERVICIOS TECNICOS");
            switch (NuevoReporte.TipoBusqueda.ToUpper())
            {
                case "CLIENTE": TituloReporte.Append(TituloReporteCliente(NuevoReporte)); break;
                case "SERIE": TituloReporte.Append(" POR SERIE: ").Append(NuevoReporte.ParametroBusqueda.ToUpper()); break;
                //case "FUSORES": TituloReporte.Append(" FUSORES"); break;
                case "FUSOR":
                    if (NuevoReporte.ParametroBusqueda == " ")
                    {
                        TituloReporte.Append(" FUSORES");
                    }
                    else
                    {
                        TituloReporte.Append(" POR FUSOR: ").Append(NuevoReporte.ParametroBusqueda);
                    }
                    break;
                case "MARCA": TituloReporte.Append(" POR MARCA: ").Append(NuevoReporte.ParametroBusqueda); break;
                case "MODELO": TituloReporte.Append("POR MODELO: ").Append(NuevoReporte.ParametroBusqueda); break;
                case "MANTENIMIENTOS":
                    TituloReporte = new StringBuilder("");
                    TituloReporte.Append("REPORTE MANTENIMIENTO RICOH").Append(TituloReporteCliente(NuevoReporte));
                    break;
            }
            return TituloReporte.ToString();
        }

        public string TituloReporteCliente(Reporte NuevoReporte)
        {
            StringBuilder TituloReporte = new StringBuilder(" POR ");

            //Validar si se eligio un cliente o todos
            if (NuevoReporte.Cliente == "")
            {
                TituloReporte.Append("CLIENTES");
            }
            else
            {
                //Agregar nombre de cliente al reporte
                TituloReporte.Append("CLIENTE").Append("\n").Append(NuevoReporte.Cliente);
            }

            //Validar si se eligio un modelo
            if (NuevoReporte.ParametroBusqueda != "")
            {
                TituloReporte.Append("\nPOR ").Append(NuevoReporte.TipoBusquedaAdicional.ToUpper()).Append(": ").Append(NuevoReporte.ParametroBusqueda);
            }

            return TituloReporte.ToString();
        }

        public void GenerarReporteFusor()
        {
            DateTime Fecha;

            PdfPTable tblFusor = new PdfPTable(6) { WidthPercentage = 100 };

            document.Add(new Paragraph("\n"));

            PdfPCell clSerieFusor = new PdfPCell(new Phrase("Serie Fusor", pe.FuenteParrafoBold)) { BorderWidth = .5f };
            PdfPCell clModelo = new PdfPCell(new Phrase("Modelo", pe.FuenteParrafoBold)) { BorderWidth = .5f };
            PdfPCell clInstalado = new PdfPCell(new Phrase("Estado", pe.FuenteParrafoBold)) { BorderWidth = .5f };
            PdfPCell clFecha = new PdfPCell(new Phrase("Fecha", pe.FuenteParrafoBold)) { BorderWidth = .5f };
            PdfPCell clFolio = new PdfPCell(new Phrase("Folio Reporte", pe.FuenteParrafoBold)) { BorderWidth = .5f };
            PdfPCell clCliente = new PdfPCell(new Phrase("Ubicación", pe.FuenteParrafoBold)) { BorderWidth = .5f };

            tblFusor.AddCell(clFolio);
            tblFusor.AddCell(clCliente);
            tblFusor.AddCell(clFecha);
            tblFusor.AddCell(clModelo);
            tblFusor.AddCell(clSerieFusor);
            tblFusor.AddCell(clInstalado);

            tblFusor.HorizontalAlignment = Element.ALIGN_LEFT;
            //document.Add(new Paragraph(ParametroBusqueda, pe.FuenteParrafoBold));

            //while (reporte.Read())
            foreach (DataRow fila in primerDataTable.Rows)
            {
                PdfPCell cSerieFusor = new PdfPCell(new Phrase(fila[0].ToString(), pe.FuenteParrafo)) { BorderWidth = .5f, Padding = 2 };
                PdfPCell cModelo = new PdfPCell(new Phrase(fila[1].ToString(), pe.FuenteParrafo)) { BorderWidth = .5f, Padding = 2, Rowspan = 2 };
                PdfPCell cInstalado = new PdfPCell(new Phrase("Instalado", pe.FuenteParrafo)) { BorderWidth = .5f, Padding = 2 };
                Fecha = Convert.ToDateTime(fila[2].ToString());
                PdfPCell cFecha = new PdfPCell(new Phrase(Fecha.ToString("dd/MM/yyyy"), pe.FuenteParrafo)) { BorderWidth = .5f, Padding = 2, Rowspan = 2 };

                PdfPCell cFolio = new PdfPCell(new Phrase(fila[3].ToString(), pe.FuenteParrafo)) { BorderWidth = .5f, Padding = 2, Rowspan = 2 };
                PdfPCell cCliente = new PdfPCell(new Phrase(fila[4].ToString(), pe.FuenteParrafo)) { BorderWidth = .5f, Padding = 2, Rowspan = 2 };

                PdfPCell cSerieSaliente = new PdfPCell(new Phrase(fila[5].ToString(), pe.FuenteParrafo)) { BorderWidth = .5f, Padding = 2 };
                PdfPCell cInstaladoR = new PdfPCell(new Phrase("Retirado", pe.FuenteParrafo)) { BorderWidth = .5f, Padding = 2 };

                tblFusor.AddCell(cFolio);
                tblFusor.AddCell(cCliente);
                tblFusor.AddCell(cFecha);
                tblFusor.AddCell(cModelo);
                tblFusor.AddCell(cSerieFusor);
                tblFusor.AddCell(cInstalado);

                tblFusor.AddCell(cSerieSaliente);
                tblFusor.AddCell(cInstaladoR);
            }
            document.Add(tblFusor);
        }

        public void GenerarReporte(Reporte nuevoReporte)
        {
            //Tabla para cuando se requiera hacer reporte por cliente
            //PdfPTable tblCliente = new PdfPTable(4) { WidthPercentage = 80 };
            PdfPTable tblCliente = (RangoHabilitado) ? new PdfPTable(3) { WidthPercentage = 80 } : new PdfPTable(4) { WidthPercentage = 80 }; ;


            this.TipoBusqueda = nuevoReporte.TipoBusqueda;
            document.Add(new Paragraph("\n"));

            //Si estamos filtrando por cliente, entonces se añade una tabla con los titulos de los datos que nos arrojara
            if (nuevoReporte.TipoBusqueda == "Cliente")
            {
                PdfPCell clSerie = new PdfPCell(new Phrase("Serie", pe.FuenteParrafoBold)) { BorderWidth = .5f };
                tblCliente.AddCell(clSerie);
                if (!nuevoReporte.RangoHabilitado)
                {
                    PdfPCell clFecha = new PdfPCell(new Phrase("Fecha Servicio", pe.FuenteParrafoBold)) { BorderWidth = .5f };
                    tblCliente.AddCell(clFecha);
                }
                PdfPCell clTecnico = new PdfPCell(new Phrase("Técnico", pe.FuenteParrafoBold)) { BorderWidth = .5f };
                PdfPCell clFolio = new PdfPCell(new Phrase("Numero Folio", pe.FuenteParrafoBold)) { BorderWidth = .5f };

                tblCliente.AddCell(clTecnico);
                tblCliente.AddCell(clFolio);

                tblCliente.HorizontalAlignment = Element.ALIGN_LEFT;
                document.Add(tblCliente);
            }
            bool PrimerRegistro = true;
            //Recorremos el arreglo que nos genero la consulta
            //ESTA PARTE SERA CAMBIADA
            //while (reporte.Read())
            foreach (DataRow fila in primerDataTable.Rows)
            {
                Servicio reporteServicio = new Servicio()
                {
                    NumeroFolio = fila[0].ToString(),
                    Cliente = fila[1].ToString(),
                    Marca = fila[2].ToString(),
                    Modelo = fila[3].ToString(),
                    Serie = fila[4].ToString(),
                    Contador = int.Parse(fila[5].ToString()),
                    Fecha = Convert.ToDateTime(fila[6].ToString()),
                    Tecnico = fila[7].ToString(),
                    Fusor = fila[8].ToString(),
                    FusorSaliente = fila[9].ToString(),
                    ServicioRealizado = fila[10].ToString(),
                    ReporteFallo = fila[11].ToString(),
                    IdTipoServicio = int.Parse(fila[12].ToString())
                };

                NumeroReportes++;

                //Colocar condicion para cambiar orden en que se mostraran los datos
                if (EsReporteMantenimientos)
                {
                    //Orden cliente, serie y fecha
                    AgregarClienteAlDocumento(reporteServicio);
                }
                else
                {
                    //Orden fecha
                    AgregarFechaAlDocumento(document, reporteServicio);
                }
            }
            lstClientes.Clear();
            lstFechas.Clear();
            lstSeries.Clear();
            //reporte.Close();
        }

        public void AgregarClienteAlDocumento(Servicio ReporteServicio)
        {
            if (!lstClientes.Contains(ReporteServicio.Cliente))
            {
                iTextSharp.text.pdf.draw.LineSeparator LineaSeparacion = new iTextSharp.text.pdf.draw.LineSeparator() { Offset = 2f };
                document.Add(new Chunk(LineaSeparacion));
                lstClientes.Add(ReporteServicio.Cliente);
                document.Add(new Paragraph(ReporteServicio.Cliente, pe.FuenteFecha));
                lstFechas.Clear();
            }
            AgregarSerieAlDocumento(ReporteServicio);
        }

        public void AgregarFechaAlDocumento(Document document, Servicio ReporteServicio)
        {
            string fechaString = ReporteServicio.Fecha.ToString("dd/MM/yyyy");
            if (!lstFechas.Contains(ReporteServicio.Fecha.ToString()))
            {
                
                lstFechas.Add(ReporteServicio.Fecha.ToString());
                document.Add(new Paragraph(fechaString, pe.FuenteFecha));

                if (!EsReporteMantenimientos && TipoBusqueda != "SERIE")
                {
                    lstSeries.Clear();
                }
            }

            //Condicion para verificar si se esta generando reporte por mantenimiento de equipos
            if (EsReporteMantenimientos)
            {
                //Se agregan los datos despues de la fecha
                AgregarDatosServicio(ReporteServicio);
            }
            else
            {
                //Se agregan las series después de la fecha
                AgregarSerieAlDocumento(ReporteServicio);
            }
        }
        public void AgregarSerieAlDocumento(Servicio ReporteServicio)
        {
            //Verificamos que la serie no este en la lista y que no se trate de un reporte de instalacion 
            if (!lstSeries.Contains(ReporteServicio.Serie) && ReporteServicio.IdTipoServicio != 2 && ReporteServicio.IdTipoServicio != 3)
            {
                document.Add(new Chunk());
                if (EsReporteMantenimientos)
                {
                    //Si es reporte de mantenimientos, debemos limpiar la lista de fechas cada que sea una nueva serie de equipo
                    lstFechas.Clear();
                }
                lstSeries.Add(ReporteServicio.Serie);
                tblSeries = new PdfPTable(3) { WidthPercentage = 80 };

                tblSeries = AgregarTablaSerie(ReporteServicio.Serie, ReporteServicio.Marca, ReporteServicio.Modelo);
                document.Add(tblSeries);
            }
            else
            {
                //if (!EsReporteMantenimientos)
                //{
                //    iTextSharp.text.pdf.draw.LineSeparator LineaSeparacion = new iTextSharp.text.pdf.draw.LineSeparator() { Offset = 2f };
                //    document.Add(new Chunk(LineaSeparacion));
                //}
            }

            if (EsReporteMantenimientos)
            {
                //Agregaremos las fechas despues de las serie
                AgregarFechaAlDocumento(document, ReporteServicio);
            }
            else
            {
                //Agregamos los datos del servicio despues de la serie
                AgregarDatosServicio(ReporteServicio);
                document.Add(new Paragraph("\n"));
            }
        }

        public PdfPTable AgregarTablaSerie(string Serie, string Marca, string Modelo)
        {
            //Encabezados
            PdfPTable tblSerie = new PdfPTable(3) { HorizontalAlignment = Element.ALIGN_LEFT, WidthPercentage = 80, PaddingTop = 10f };
            PdfPCell clMarca;
            PdfPCell clModelo;

            //Encabezados
            PdfPCell clEncabezadoSerie = new PdfPCell(new Phrase("SERIE", pe.FuenteParrafoBold)) { BorderWidth = .5f };
            PdfPCell clEncabezadoMarca = new PdfPCell(new Phrase("MARCA", pe.FuenteParrafoBold)) { BorderWidth = .5f };
            PdfPCell clEncabezadoModelo = new PdfPCell(new Phrase("MODELO", pe.FuenteParrafoBold)) { BorderWidth = .5f };

            tblSerie.AddCell(clEncabezadoSerie);
            tblSerie.AddCell(clEncabezadoMarca);
            tblSerie.AddCell(clEncabezadoModelo);

            //PdfPCell clSerie = (TipoBusqueda == "Serie") ? new PdfPCell(new Phrase("", pdf.FuenteParrafoBold)) { BorderWidth = .5f }: new PdfPCell(new Phrase(Serie, pdf.FuenteParrafoBold)) { BorderWidth = .5f };
            PdfPCell clSerie = new PdfPCell(new Phrase(Serie, pe.FuenteParrafo)) { BorderWidth = .5f };
            tblSerie.AddCell(clSerie);

            clMarca = (TipoBusqueda != "MARCA" && TipoBusqueda != "MODELO") ? new PdfPCell(new Phrase(Marca, pe.FuenteParrafo)) { BorderWidth = .5f } : new PdfPCell(new Phrase("", pe.FuenteParrafo)) { BorderWidth = .5f };
            clModelo = (TipoBusqueda != "MODELO") ? new PdfPCell(new Phrase(Modelo, pe.FuenteParrafo)) { BorderWidth = .5f } : new PdfPCell(new Phrase("", pe.FuenteParrafo)) { BorderWidth = .5f };

            tblSerie.AddCell(clMarca);
            tblSerie.AddCell(clModelo);

            return tblSerie;
        }

        //Esta parte no me gusta hay que cambiarla
        public void AgregarDatosServicio(Servicio ReporteServicio)
        {
            string Cliente = "";
            //Colocamos los datos del servicio
            DateTime Fecha = Convert.ToDateTime(ReporteServicio.Fecha);
            if (ReporteServicio.IdTipoServicio == 1 || ReporteServicio.IdTipoServicio == 4) //Validamos que no sea un reporte de instalacion
            {
                Cliente = (EsReporteMantenimientos) ? "" : ReporteServicio.Cliente;
                int Espaciado = 60;
                if(Cliente.Length > Espaciado)
                {
                    int EspacioAdicional = (Cliente.Length + 5) - Cliente.Length;
                    Espaciado += EspacioAdicional;
                }
                document.Add(new Paragraph(Cliente + new string(' ', Espaciado - Cliente.Length) + ReporteServicio.NumeroFolio.ToString(), pe.FuenteParrafoBold));
            }
            else//Se creara la tabla de reporte de instalacion
            {
                CrearTablaReporteEspecifico(ReporteServicio);
            }

            document.Add(new Paragraph("DIAGNOSTICO: " + ReporteServicio.ReporteFallo.ToUpper(), pe.FuenteParrafo));
            document.Add(new Paragraph("SERVICIO: " + ReporteServicio.ServicioRealizado.ToUpper(), pe.FuenteParrafo));
            string Fusor = ReporteServicio.Fusor;

            //Validar que se haya cambiado algun fusor
            if (!string.IsNullOrWhiteSpace(Fusor))
            {
                document.Add(new Paragraph("FUSOR INSTALADO: " + ReporteServicio.Fusor.ToUpper() + "                " + "FUSOR RETIRADO: " + ReporteServicio.FusorSaliente.ToUpper(), pe.FuenteParrafo));
            }

            if (ReporteServicio.Marca == "Ricoh")
            {
                ComprobarModulosModificados(ReporteServicio);
            }

            document.Add(new Paragraph("CONTADOR: " + string.Format("{0:n0}", int.Parse(ReporteServicio.Contador.ToString())) + "      TECNICO: " +  ReporteServicio.Tecnico.ToUpper(), pe.FuenteParrafo));
        }

        //Crea una tabla para distinguir entre todos los reportes, aquellos que sean de instalación o de retiro de equipos
        public void CrearTablaReporteEspecifico(Servicio ReporteServicio)
        {
            PdfPTable tblReporteInstalacion = new PdfPTable(4) { HorizontalAlignment = Element.ALIGN_LEFT, WidthPercentage = 80, PaddingTop = 10f };

            string TipoReporte = (ReporteServicio.IdTipoServicio == 2) ?"INSTALACION":"RETIRO";

            //Encabezados
            PdfPCell clEncabezadoInstalacion = new PdfPCell(new Phrase("REPORTE DE " + TipoReporte, pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER };
            tblReporteInstalacion.AddCell(clEncabezadoInstalacion);

            PdfPCell clEncabezadoCliente = new PdfPCell(new Phrase(ReporteServicio.Cliente, pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 2 , HorizontalAlignment = Element.ALIGN_CENTER };
            PdfPCell clReporte = new PdfPCell(new Phrase(ReporteServicio.NumeroFolio, pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 , HorizontalAlignment = Element.ALIGN_CENTER };
            PdfPCell clSerie = new PdfPCell(new Phrase(ReporteServicio.Serie, pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 , HorizontalAlignment = Element.ALIGN_CENTER };

            tblReporteInstalacion.AddCell(clEncabezadoCliente);
            tblReporteInstalacion.AddCell(clReporte);
            tblReporteInstalacion.AddCell(clSerie);

            document.Add(tblReporteInstalacion);
        }

        //Obtener los modulos que fueron retirados e instalados
        public void ComprobarModulosModificados(Servicio ReporteServicio)
        {
            bool ColocarEspacio = false;
            if (ObtenerModulosModificados(ReporteServicio))
            {
                StringBuilder ModulosModificado = new StringBuilder();
                while (DatosModulos.Read())
                {
                    string Clave = DatosModulos[0].ToString();
                    string Estado = DatosModulos[1].ToString();
                    if (Estado == "RETIRADO")
                    {
                        ModulosModificado.Append("MODULO ").Append(Estado).Append(" : ").Append(Clave);
                        ColocarEspacio = true;
                    }
                    else
                    {
                        string espacio = (ColocarEspacio) ? new string('\u00A0', 20) : new string('\u00A0', 0);

                        ModulosModificado.Append(espacio).Append("MODULO ").Append(Estado).Append(" : ").Append(Clave).Append("   ");
                    }
                }
                document.Add(new Paragraph(ModulosModificado.ToString(), pe.FuenteParrafo));
                DatosModulos.Close();
            }
        }

        public bool ObtenerModulosModificados(Servicio ReporteServicio)
        {
            bool DatosObtenidos = true;
            SqlCommand SegundoComando = new SqlCommand();

            SegundoComando.Connection = conexion.AbrirConexion();
            SegundoComando.CommandText = "ObtenerModulosModificados";
            SegundoComando.CommandType = CommandType.StoredProcedure;
            SegundoComando.Parameters.Clear();
            SegundoComando.Parameters.AddWithValue("@NumeroFolio", ReporteServicio.NumeroFolio);
            DatosModulos = SegundoComando.ExecuteReader();
            if (!DatosModulos.HasRows)
            {
                DatosModulos.Close();
                return DatosObtenidos = false;
            }

            return DatosObtenidos;
        }


    }
}
