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
using CobranzaSP.Formularios;

namespace CobranzaSP.Lógica
{
    internal class LogicaInventario
    {
        private CD_Conexion conexion = new CD_Conexion();
        SqlCommand comando = new SqlCommand();
        PdfPTable tblInventarioRegistrosSalidas;
        DataTable tblDatosFusor;
        DataTable tblDatosInventarioToners;
        LogicaRegistro lgRegistro = new LogicaRegistro();
        SqlDataReader Inventario;
        SqlDataReader Totales;
        Document document;


        public string RegistrarInventario(InventarioDatos nuevoCartucho)
        {
            string Mensaje;
            string Accion = " AGREGO ";
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "spGuardarTonerInventario";
            comando.CommandType = CommandType.StoredProcedure;

            if (nuevoCartucho.Id > 0)
            {
                Accion = " MODIFICO ";
            }
            comando.Parameters.AddWithValue("@Id", nuevoCartucho.Id);
            comando.Parameters.AddWithValue("@Modelo", nuevoCartucho.Modelo);
            comando.Parameters.AddWithValue("@IdMarca", nuevoCartucho.IdMarca);
            comando.Parameters.AddWithValue("@CantidadOficina", nuevoCartucho.CantidadOficina);
            comando.Parameters.AddWithValue("@Fecha", nuevoCartucho.Fecha);
            comando.Parameters.AddWithValue("@Precio", nuevoCartucho.Precio);
            comando.Parameters.AddWithValue("@IdTipoArticulo", nuevoCartucho.IdTipoArticulo);
            Mensaje = "TONER SE" + Accion + "CORRECTAMENTE";
            comando.ExecuteNonQuery();

            comando.Parameters.Clear();
            conexion.CerrarConexion();
            return Mensaje;
        }

        public int ValidarFusorRecuperado(DatosReporteInventarioToners reporteInventario, DateTime Fecha)
        {
            int IdTipoFusor = 0;
            tblDatosFusor = new DataTable();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "ValidarTipoFusor";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();

            comando.Parameters.AddWithValue("@IdCartucho", reporteInventario.IdCartucho);
            comando.Parameters.AddWithValue("@Fecha", Fecha);
            tblDatosFusor.Load(comando.ExecuteReader());

            if (tblDatosFusor.Rows.Count != 0)
            {
                foreach (DataRow fila in tblDatosFusor.Rows)
                {
                    IdTipoFusor = int.Parse(fila[0].ToString());

                    //Verifica si es un fusor recuperado el que se utilizo 
                    if(IdTipoFusor == 3)
                    {
                        //Si lo es, no lo cuenta como saida valida
                        reporteInventario.CantidadSalida -= 1;
                    }
                }
            }

            conexion.CerrarConexion();
            return IdTipoFusor;
        }

        public void ImprimirInventario(DateTime FechaElegida)
        {
            //Convertir en fecha valida para sql server
            string FechaActual = FechaElegida.ToString("yyyy-MM-dd");
            //Primero obtenemos de la base de datos nuestro inventario 
            comando.Connection = conexion.AbrirConexion();
            //comando.CommandText = "ReporteInventario";
            comando.CommandText = "InventarioCartuchos";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@Fecha", FechaActual);
            tblDatosInventarioToners = new DataTable();
            tblDatosInventarioToners.Load(comando.ExecuteReader());

            string RutaArchivo = ConfiguracionPdf.RutaReportesInventarioToners;
            string NombreArchivo = RutaArchivo + "Inventario" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf";
            //string NombreArchivo = @"C:\Users\DELL PC\Documents\" + "Inventaarioooo" + DateTime.Now.ToString("dd-MM-yyyy") + ".pdf";
            //string NombreArchivo = @"\\Desktop-de0cg86\archivos compartidos\Reportes\" + "Reporte" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf";
            FileStream fs = new FileStream(NombreArchivo, FileMode.Create);
            document = new Document(PageSize.LETTER);
            document.SetMargins(25f, 25f, 25f, 25f);
            document.SetPageSize(iTextSharp.text.PageSize.LETTER);
            PdfWriter pw = PdfWriter.GetInstance(document, fs);

            var pe = new Pdf();
            pe.ColocarFormatoSuperior = false;
            pw.PageEvent = pe;

            document.Open();

            Paragraph titulo = new Paragraph("EXISTENCIAS CARTUCHOS SPEED TONER", pe.FuenteTitulo18);
            titulo.Alignment = Element.ALIGN_CENTER;
            document.Add(titulo);

            //Colocamos la imagen para gasolinas de los carros
            //iTextSharp.text.Image Logo = iTextSharp.text.Image.GetInstance(Properties.Resources.TanquesGasolina, System.Drawing.Imaging.ImageFormat.Png);
            //Logo.ScaleToFit(350, 250);
            //Logo.Alignment = iTextSharp.text.Image.UNDERLYING;
            //Logo.SetAbsolutePosition(document.LeftMargin, document.Top - 65);
            //document.Add(Logo);

            Paragraph Fecha = new Paragraph("Fecha: " + FechaElegida.ToString("dd/MM/yyyy"), pe.FuenteFechaGrande) { Alignment = Element.ALIGN_RIGHT };
            document.Add(Fecha);

            Paragraph Hora = new Paragraph("Hora: " + DateTime.Now.ToString("hh:mm:ss tt"), pe.FuenteFechaGrande) { Alignment = Element.ALIGN_RIGHT };
            document.Add(Hora);

            PdfPTable tblInventario = new PdfPTable(5);
            //PdfPTable tblInventario = new PdfPTable(5);

            document.Add(new Paragraph("\n"));

            //Agregamos los titulos de la tabla
            PdfPCell clModelo = new PdfPCell(new Phrase("MODELO", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 2 };
            PdfPCell clCantidadOficina = new PdfPCell(new Phrase("CANTIDAD OFICINA", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };

            PdfPCell clCantidadSalida = new PdfPCell(new Phrase("CANTIDAD SALIDA", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            PdfPCell clCantidadEntrada = new PdfPCell(new Phrase("CANTIDAD ENTRADA", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };

            tblInventario.AddCell(clModelo);
            tblInventario.AddCell(clCantidadOficina);
            tblInventario.AddCell(clCantidadSalida);
            tblInventario.AddCell(clCantidadEntrada);

            foreach (DataRow fila in tblDatosInventarioToners.Rows)
            {
                DatosReporteInventarioToners reporteInventario = new DatosReporteInventarioToners()
                {
                    Marca = fila[0].ToString(),
                    Modelo = fila[1].ToString(),
                    CantidadOficina = fila[2].ToString(),
                    CantidadEntrada = int.Parse(fila[4].ToString()),
                    CantidadSalida = int.Parse(fila[3].ToString()),
                    IdTipoArticulo = int.Parse(fila[5].ToString()),
                    IdCartucho = int.Parse(fila[6].ToString())
                };

                PdfPCell clModeloDato;
                //if (Inventario[1].ToString().StartsWith("RM1-") || Inventario[1].ToString().StartsWith("D01SE"))
                if (reporteInventario.IdTipoArticulo == 2 || reporteInventario.IdTipoArticulo == 3)
                {
                    clModeloDato = new PdfPCell(new Phrase(reporteInventario.Modelo, pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 2 };
                }
                else
                {
                    clModeloDato = new PdfPCell(new Phrase(reporteInventario.Marca + " " + reporteInventario.Modelo, pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 2 };
                }

                //Validar que se trate de un fusor y haya tenido salida
                if(reporteInventario.IdTipoArticulo == 3 && reporteInventario.CantidadSalida > 0)
                {
                    ValidarFusorRecuperado(reporteInventario, FechaElegida);
                }

                //PdfPCell clCantidadOficinaDato = new PdfPCell(new Phrase(Inventario[1].ToString(), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };
                PdfPCell clCantidadOficinaDato = new PdfPCell(new Phrase(ComprobarValoresO(reporteInventario.CantidadOficina.ToString()), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };
                PdfPCell clCantidadSalidaDato = new PdfPCell(new Phrase(ComprobarValoresO(reporteInventario.CantidadSalida.ToString()), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };
                PdfPCell clCantidadEntradaDato = new PdfPCell(new Phrase(ComprobarValoresO(reporteInventario.CantidadEntrada.ToString()), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };

                tblInventario.AddCell(clModeloDato);
                tblInventario.AddCell(clCantidadOficinaDato);
                tblInventario.AddCell(clCantidadSalidaDato);
                tblInventario.AddCell(clCantidadEntradaDato);
            }

            //Añadimos la tabla al documento
            document.Add(tblInventario);

            //Agregamos un salto de página para colocar los registros
            document.NewPage();
            AgregarMovimientosRegistro(FechaElegida);

            //Abrimos el pdf
            pe.AbrirPdf(NombreArchivo);
            conexion.CerrarConexion();
        }

        public void AgregarMovimientosRegistro(DateTime Fecha)
        {
            var pe = new Pdf();

            pe.ColocarFormatosSuperiores(document);

            Paragraph Fechas = new Paragraph("DE: " + Fecha.ToString("dd/MM/yyyy") + " AL: " + Fecha.ToString("dd/MM/yyyy"), pe.FuenteFecha) { Alignment = Element.ALIGN_CENTER };
            document.Add(Fechas);
            ReporteRegistroInventarioToners ReporteRegistro = new ReporteRegistroInventarioToners()
            {
                ParametroBusqueda = "",
                TipoBusqueda = "",
                Cliente = ""
            };
            Paragraph TituloReporte = new Paragraph(lgRegistro.CrearTituloReporte(ReporteRegistro), pe.FuenteTitulo) { Alignment = Element.ALIGN_CENTER };
            document.Add(TituloReporte);
            lgRegistro.tblInventarioRegistrosSalidas = new PdfPTable(5);//QUITAR EN CASO DE QUE NO QUEDE

            ReporteRegistroInventarioToners NuevoReporte = new ReporteRegistroInventarioToners()
            {
                FechaInicio = Fecha,
                FechaFinal = Fecha,
                ParametroBusqueda = "",
                Cliente = ""
            };

            //Validar existencia de movimientos de toners
            if (!lgRegistro.ObtenerDatosReporteRegistroInventario(NuevoReporte))
            {
                document.Close();
                return;
            }

            while(lgRegistro.Inventario.Read())
            {
                //DateTime fechaActual = Convert.ToDateTime(Inventario[5].ToString());
                DateTime fechaActual = Convert.ToDateTime(lgRegistro.Inventario[5].ToString());
                PdfRegistroInventarioToners nuevoReporteRegistro = new PdfRegistroInventarioToners()
                {
                    FechaActual = fechaActual.ToString("dd/MM/yyyy"),
                    Marca = lgRegistro.Inventario[0].ToString(),
                    Modelo = lgRegistro.Inventario[1].ToString(),
                    CantidadSalida = int.Parse(lgRegistro.Inventario[2].ToString()),
                    CantidadEntrada = int.Parse(lgRegistro.Inventario[3].ToString()),
                    CantidadGarantia = int.Parse(lgRegistro.Inventario[7].ToString()),
                    Cliente_Proveedor = lgRegistro.Inventario[4].ToString(),
                    ClaveFusor = lgRegistro.Inventario[6].ToString(),
                    IdTipoArticulo = int.Parse(lgRegistro.Inventario[8].ToString())
                };
                lgRegistro.AgregarRegistrosATabla(document, nuevoReporteRegistro);
            }
           

            document.Add(lgRegistro.tblInventarioRegistrosSalidas);//Finalmente agregamos el ultimo registro a la lista

            //Reiniciamos nuestras tablas, listas y cerramos en la base de datos
            //tblInventarioRegistrosSalidas = new PdfPTable(4);
            tblInventarioRegistrosSalidas = new PdfPTable(5);

            //Inventario.Close();
            lgRegistro.Inventario.Close();

            //TOTALES
            iTextSharp.text.pdf.draw.LineSeparator lineSeparator = new iTextSharp.text.pdf.draw.LineSeparator() { Offset = 2f };
            document.Add(new Chunk(lineSeparator));
            Paragraph Resumen = new Paragraph("RESUMEN ENTRADAS Y SALIDAS DEL " + Fecha.ToString("dd/MM/yyyy") + " - " + Fecha.ToString("dd/MM/yyyy"), pe.FuenteTitulo) { Alignment = Element.ALIGN_CENTER };
            document.Add(Resumen);
            document.Add(new Chunk());

            //ObtenerResumenTotales(NuevoReporte);

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "CalcularTotales";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@FechaInicio", Fecha);
            comando.Parameters.AddWithValue("@FechaFinal", Fecha);
            comando.Parameters.AddWithValue("@Parametro", "");
            comando.Parameters.AddWithValue("@Cliente", "");
            Totales = comando.ExecuteReader();

            //Definicion de columnas y asignacion de encabezados de columnas
            //PdfPTable tblTotalesRegistros = (BusquedaCliente) ? new PdfPTable(4) : new PdfPTable(5);
            PdfPTable tblTotalesRegistros = new PdfPTable(6);
            PdfPCell clTituloMarca = new PdfPCell(new Phrase("Marca", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            PdfPCell clTituloModelo = new PdfPCell(new Phrase("Modelo", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 2 };
            PdfPCell clTituloSalidas = new PdfPCell(new Phrase("Total Salida", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            PdfPCell clTituloEntradas = new PdfPCell(new Phrase("Total Entrada", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            PdfPCell clTituloGarantias = new PdfPCell(new Phrase("Total Garantias", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };

            tblTotalesRegistros.AddCell(clTituloMarca);
            tblTotalesRegistros.AddCell(clTituloModelo);
            tblTotalesRegistros.AddCell(clTituloSalidas);
            tblTotalesRegistros.AddCell(clTituloGarantias);
            //En dado caso de que se cumpla la condicion, quiere decir que estamos generando por cliente, por lo tanto solo veremos las salidas que se hayan tenido
            tblTotalesRegistros.AddCell(clTituloEntradas);


            while (Totales.Read())
            {
                PdfPCell clMarca = new PdfPCell(new Phrase(Totales[0].ToString(), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };
                PdfPCell clModelo = new PdfPCell(new Phrase(Totales[1].ToString(), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 2 };
                tblTotalesRegistros.AddCell(clMarca);
                tblTotalesRegistros.AddCell(clModelo);
                PdfPCell clTotalSalida = new PdfPCell(new Phrase(Totales[2].ToString(), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };
                PdfPCell clTotalEntrada = new PdfPCell(new Phrase(Totales[3].ToString(), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };
                PdfPCell clTotalGarantias = new PdfPCell(new Phrase(Totales[4].ToString(), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };


                tblTotalesRegistros.AddCell(clTotalSalida);
                tblTotalesRegistros.AddCell(clTotalGarantias);
                tblTotalesRegistros.AddCell(clTotalEntrada);
            }
            document.Add(tblTotalesRegistros);
            Totales.Close();
            lgRegistro.ReiniciarDatos();
            document.Close();
        }

        //Metodo para evitar que salgan valores en 0 en el inventario
        public string ComprobarValoresO(string Cantidad)
        {
            if (Cantidad == "0")
            {
                return " ";
            }
            else
            {
                return Cantidad;
            }
        }
    }
}
