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
//using DocumentFormat.OpenXml.Drawing;
//using DocumentFormat.OpenXml.Wordprocessing;
//using DocumentFormat.OpenXml.Drawing;
//using DocumentFormat.OpenXml.Math;

namespace CobranzaSP.Lógica
{
    internal class LogicaRegistro
    {
        private CD_Conexion conexion = new CD_Conexion();
        private AccionesLógica NuevaAccion = new AccionesLógica();
        SqlCommand comando = new SqlCommand();

        //TABLAS
        public PdfPTable tblInventarioRegistrosSalidas;
        public PdfPTable tblTotalesRegistros;

        //LISTAS
        SortedSet<string> lstFechas = new SortedSet<string>();
        SortedSet<string> lstMarcas = new SortedSet<string>();
        SortedSet<string> lstModelos = new SortedSet<string>();
        public SqlDataReader Inventario;
        SqlDataReader Totales;


        string Parametro = "";
        //Variables para obtener el historial de salida de un toner en especifico
        int CantidadSalidaTotal = 0;
        int CantidadEntradaTotal = 0;
        int CantidadInicial = 0;

        #region CRUD
        public string AgregarRegistroInventario(RegistroInventario nuevoRegistro)
        {
            SqlDataReader leer;
            int valor = 0;
            comando.Connection = conexion.AbrirConexion();

            comando.CommandText = "AgregarSERegistro";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@IdCartucho", nuevoRegistro.IdCartucho);
            comando.Parameters.AddWithValue("@CantidadSalida", nuevoRegistro.CantidadSalida);
            comando.Parameters.AddWithValue("@CantidadEntrada", nuevoRegistro.CantidadEntrada);
            comando.Parameters.AddWithValue("@CantidadGarantia", nuevoRegistro.CantidadGarantia);


            valor = comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            //Nos ayuda a comprobar si el inventario fue modificado(Dependiendo si se haya modificado algo o no)
            if (valor > 0)
            {
                //En dado caso de que haya modificado el inventario, se agregara el registro a la tabla de registros

                //Este es el que se utiliza en mi base de datos
                comando.CommandText = "AgregarRegistroInventario";

                //BD PRINCIPAL
                //comando.CommandText = "AgregarRegistroInventarioRespaldo";

                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@IdMarca", nuevoRegistro.IdMarca);
                comando.Parameters.AddWithValue("@IdCartucho", nuevoRegistro.IdCartucho);
                comando.Parameters.AddWithValue("@CantidadSalida", nuevoRegistro.CantidadSalida);
                comando.Parameters.AddWithValue("@CantidadEntrada", nuevoRegistro.CantidadEntrada);
                comando.Parameters.AddWithValue("@CantidadGarantia", nuevoRegistro.CantidadGarantia);


                comando.Parameters.AddWithValue("@Cliente", nuevoRegistro.Cliente);
                comando.Parameters.AddWithValue("@Fecha", nuevoRegistro.Fecha.ToString("yyyy-MM-dd"));

                if (nuevoRegistro.NumeroSerie != null)
                {
                    comando.Parameters.AddWithValue("@NumeroSerie", nuevoRegistro.NumeroSerie);
                }
                else
                {
                    comando.Parameters.AddWithValue("@NumeroSerie", " ");
                }

                valor = comando.ExecuteNonQuery();
                comando.Parameters.Clear();
                conexion.CerrarConexion();
                return "Se ha agregado el resgitro correctamente. Se ha actualizado el inventario";
            }
            else
            {
                conexion.CerrarConexion();
                return "No se ha agregado el registro. La cantidad excede la cantidad en existencia";
            }
        }

        //Metodo que agregara al inventario fisico lo que se le ingrese ya sea entrada o salida
        public int ModificarInventario(RegistroInventario nuevoRegistro)
        {
            int valor;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "UpdateInventario";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@IdCartucho", nuevoRegistro.IdCartucho);
            comando.Parameters.AddWithValue("@CantidadSalida", nuevoRegistro.CantidadSalida);
            comando.Parameters.AddWithValue("@CantidadEntrada", nuevoRegistro.CantidadEntrada);
            valor = comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            return valor;
        }

        public void ModificarRegistro(RegistroInventario nuevoRegistro)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "ModificarRegistroInventario";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@IdRegistro", nuevoRegistro.Id);
            comando.Parameters.AddWithValue("@IdMarca", nuevoRegistro.IdMarca);
            comando.Parameters.AddWithValue("@IdCartucho", nuevoRegistro.IdCartucho);
            comando.Parameters.AddWithValue("@CantidadSalida", nuevoRegistro.CantidadSalida);
            comando.Parameters.AddWithValue("@CantidadEntrada", nuevoRegistro.CantidadEntrada);
            comando.Parameters.AddWithValue("@Cliente", nuevoRegistro.Cliente);
            comando.Parameters.AddWithValue("@Fecha", nuevoRegistro.Fecha);

            if (nuevoRegistro.NumeroSerie != null)
                comando.Parameters.AddWithValue("@NumeroSerie", nuevoRegistro.NumeroSerie);
            else
                comando.Parameters.AddWithValue("@NumeroSerie", " ");

            comando.ExecuteNonQuery();

            comando.Parameters.Clear();
        }

        //Metodo que se utilizara solo para las modificaciones de salidas
        public bool ModificarRegistroInventario(RegistroInventario nuevoRegistro, bool modificacionNormal)
        {
            int IdInventario;
            int Valor = 0;
            if (modificacionNormal || Valor > 0)
            {
                if (!modificacionNormal)
                {
                    IdInventario = ObtenerIdInventario(nuevoRegistro.IdCartucho);
                    if (nuevoRegistro.CantidadEntrada > 0)
                    {
                        Valor = ModificarInventario(nuevoRegistro);
                    }
                    else if (!ComprobarCantidadInventario(nuevoRegistro))
                    {
                        return false;
                    }
                    else
                    {
                        Valor = ModificarInventario(nuevoRegistro);
                    }
                }
                ModificarRegistro(nuevoRegistro);
                conexion.CerrarConexion();
                return true;
            }
            return false;
        }

        //Metodo que sera util para hacer cambios o eliminaciones del inventario, dependiendo el stored procedure que se utilice
        public void ModificarInventario(RegistroInventario registroAnterior, string sp)
        {
            int IdInventario = ObtenerIdInventario(registroAnterior.IdCartucho);
            comando.Connection = conexion.AbrirConexion();

            comando.CommandText = sp;
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@IdInventario", IdInventario);
            comando.Parameters.AddWithValue("@Id", registroAnterior.Id);
            comando.Parameters.AddWithValue("@CantidadSalida", registroAnterior.CantidadSalida);
            comando.Parameters.AddWithValue("@CantidadEntrada", registroAnterior.CantidadEntrada);
            comando.Parameters.AddWithValue("@CantidadGarantia", registroAnterior.CantidadGarantia);//QUITAR
            //Primero requerimos comprobar de que no se haya ingresado una cantidad de salida mayor a la que se tiene en el inventario


            comando.ExecuteNonQuery();

            comando.Parameters.Clear();
            conexion.CerrarConexion();
        }

        public bool ComprobarCantidadInventario(RegistroInventario registro)
        {
            bool CantidadSuficiente = false;
            int Id = ObtenerIdInventario(registro.IdCartucho);
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "ComprobarCantidadInventario";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@CantidadSalida", registro.CantidadSalida);
            comando.Parameters.AddWithValue("@IdInventario", Id);
            comando.Parameters.AddWithValue("@CantidadEntrada", registro.CantidadEntrada);


            SqlDataReader leer = comando.ExecuteReader();
            comando.Parameters.Clear();
            if (leer.Read())
            {
                CantidadSuficiente = true;
            }
            leer.Close();
            return CantidadSuficiente;
        }

        //Metodo que ayudara a buscar el id del inventario cuando se desee borrar algun registro de la base de datos
        public int ObtenerIdInventario(int Modelo)
        {
            SqlDataReader leer;
            int Id = 0;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "BuscarIdInventario";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Modelo", Modelo);
            leer = comando.ExecuteReader();
            if (leer.Read())
            {
                Id = int.Parse(leer[0].ToString());
            }
            comando.Parameters.Clear();
            leer.Close();
            conexion.CerrarConexion();
            return Id;
        }

        #endregion


        //Metodo que ayudara a saber si la cantidad ya sea de entrada o salida no excede las existencias en inventario

        #region Pdf

        #region ObtenerDatos

        public bool ObtenerDatosReporteRegistroInventario(ReporteRegistroInventario NuevoReporte)
        {
            bool DatosEncontrados = true;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "ReporteRegistros";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@FechaInicio", NuevoReporte.FechaInicio);
            comando.Parameters.AddWithValue("@FechaFinal", NuevoReporte.FechaFinal);
            comando.Parameters.AddWithValue("@Parametro", NuevoReporte.ParametroBusqueda);
            Inventario = comando.ExecuteReader();
            if (!Inventario.HasRows)
            {
                Inventario.Close();
                return DatosEncontrados = false;
            }
            return DatosEncontrados;
        }

        public bool ObtenerDatosReporteRegistroCartucho(ReporteRegistroInventario NuevoReporte)
        {
            bool DatosEncontrados = true;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = (NuevoReporte.Cliente != "") ? "ReporteClienteCartuchoPorFecha" : "ReporteRegistros";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@FechaInicio", NuevoReporte.FechaInicio);
            comando.Parameters.AddWithValue("@FechaFinal", NuevoReporte.FechaFinal);
            comando.Parameters.AddWithValue("@Parametro", NuevoReporte.ParametroBusqueda);
            if (NuevoReporte.Cliente != "")
            {
                comando.Parameters.AddWithValue("@Cliente", NuevoReporte.Cliente);
            }
            Inventario = comando.ExecuteReader();
            

            if (!Inventario.HasRows)
            {
                Inventario.Close();
                return DatosEncontrados = false;
            }
            //GenerarPdf(NuevoReporte);
            return DatosEncontrados;
        }

        public void ObtenerDatosResumenTotales(Reporte NuevoReporte)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "CalcularTotales";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@FechaInicio", NuevoReporte.FechaInicio);
            comando.Parameters.AddWithValue("@FechaFinal", NuevoReporte.FechaFinal);
            comando.Parameters.AddWithValue("@Parametro", NuevoReporte.ParametroBusqueda);
            Totales = comando.ExecuteReader();
        }

        //Metodo para obtener cantidad inicial del toner o modelo
        public int CalcularTotalCartucho(DateTime FechaInicio, DateTime FechaFinal, string Modelo)
        {
            int valor = 0;
            SqlDataReader dr;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "CalcularTotalCartucho";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();

            comando.Parameters.AddWithValue("@FechaInicio", FechaInicio);
            comando.Parameters.AddWithValue("@FechaFinal", FechaFinal);
            comando.Parameters.AddWithValue("@Modelo", Modelo);
            dr = comando.ExecuteReader();
            if (dr.Read())
            {
                valor = int.Parse(dr[2].ToString());
            }
            comando.Parameters.Clear();
            dr.Close();
            return valor;
        }
        #endregion 

        public bool GenerarPdf(ReporteRegistroInventario NuevoReporte)
        {
            //string NombreArchivo = @"\\administracion-pc\ARCHIVOS COMPARTIDOS\Reportes\" + "Reporte" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf";
            //string NombreArchivo = @"C:\Users\DELL PC\Documents\Base de datos\" + "ReporteRegistros" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf";
            string NombreArchivo = @"\\DESKTOP-34U2P48\Archivos Compartidos\Reportes\Registro Inventario\" + "ReporteRegistro" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf";


            FileStream fs = new FileStream(NombreArchivo, FileMode.Create);
            Document document = new Document(PageSize.LETTER);
            //Ayudará a validar que la generacion del reporte no quede en blanco
            bool DatosEncontrados = true;
            document.SetMargins(25f, 25f, 25f, 25f);
            //Colocamos el pdf en horizontal
            document.SetPageSize(iTextSharp.text.PageSize.LETTER);
            PdfWriter pw = PdfWriter.GetInstance(document, fs);

            //Instanciamos la clase para la paginacion
            var pe = new Pdf();
            pw.PageEvent = pe;
            pe.ColocarFormatoSuperior = true;
            document.Open();
            Parametro = NuevoReporte.Cliente;


            bool BusquedaCliente = (NuevoReporte.Cliente != " ") ? true : false;
            //bool BusquedaCliente = (Parametro == "CLIENTE") ? true : false;

            //RANGO DE FECHA
            Paragraph Fechas = new Paragraph("FECHA DE INICIO: " + NuevoReporte.FechaInicio.ToString("dd/MM/yyyy") + "       FECHA FINAL: " + NuevoReporte.FechaFinal.ToString("dd/MM/yyyy"), pe.FuenteFecha) { Alignment = Element.ALIGN_CENTER };
            document.Add(Fechas);

            tblInventarioRegistrosSalidas = new PdfPTable(4) { WidthPercentage = 100 };

            //string Titulo = ColocarTituloReporte(NuevoReporte.TipoBusqueda, document, NuevoReporte.Marca);
            string Titulo = CrearTituloReporte(NuevoReporte);
            Paragraph TituloReporte =  new Paragraph(Titulo, pe.FuenteTitulo) { Alignment  = Element.ALIGN_CENTER };
            document.Add(TituloReporte);

            switch (NuevoReporte.TipoBusqueda)
            {
                case"Modelo":CrearReporteCartucho(NuevoReporte, document);break;
                default: DatosEncontrados = CrearReporteRegistros(NuevoReporte, document); break;
            }

            document.Close();
            CantidadInicial = 0;
            if (!DatosEncontrados)
            {
                return DatosEncontrados;
            }

            //Abrimos el pdf
            pe.AbrirPdf(NombreArchivo);
            return DatosEncontrados;
        }

        #region PartesPdf

        public string CrearTituloReporte(ReporteRegistroInventario NuevoReporte)
        {
            StringBuilder TituloBuilder = new StringBuilder("DETALLADO ENTRADAS Y SALIDAS");

            switch (NuevoReporte.TipoBusqueda)
            {
                case "Cliente":
                    TituloBuilder.Clear();
                    TituloBuilder.Append(TituloReporteCliente(NuevoReporte.ParametroBusqueda));
                    break;
                case "Modelo":
                    TituloBuilder.Append("\nModelo: ");
                    if (NuevoReporte.ParametroBusqueda.ToString().StartsWith("RM1-") || NuevoReporte.ParametroBusqueda.ToString().StartsWith("D01SE") || NuevoReporte.ParametroBusqueda.ToString().StartsWith("R6"))
                        TituloBuilder.Append(NuevoReporte.ParametroBusqueda);
                    else
                        TituloBuilder.Append(NuevoReporte.Marca).Append(" ").Append(NuevoReporte.ParametroBusqueda);
                    TituloBuilder.Append((NuevoReporte.Cliente == "") ?"": "\n CLIENTE: " + NuevoReporte.Cliente) ;
                    break;
            }

            return TituloBuilder.ToString();
        }


        public string TituloReporteCliente(string Parametro)
        {
            string Titulo = "";
            Titulo = (Parametro == "CADTONER") ? "DETALLADO ENTRADAS" : "DETALLADO SALIDAS";
            Titulo += "\n";
            Titulo += (Parametro == "CADTONER") ? "PROVEEDOR: " + Parametro : "CLIENTE: " + Parametro;

            return Titulo;
        }

        public bool CrearReporteRegistros(ReporteRegistroInventario NuevoReporte, Document document)
        {
            Pdf pe = new Pdf();
            //DATOS
            if (!ObtenerDatosReporteRegistroInventario(NuevoReporte))
            {
                return false;
            }

            while (Inventario.Read())
            {
                DateTime fechaActual = Convert.ToDateTime(Inventario[5].ToString());
                ReporteRegistro nuevoReporteRegistro = new ReporteRegistro()
                {
                    FechaActual = fechaActual.ToString("dd/MM/yyyy"),
                    Marca = Inventario[0].ToString(),
                    Modelo = Inventario[1].ToString(),
                    CantidadSalida = int.Parse(Inventario[2].ToString()),
                    CantidadEntrada = int.Parse(Inventario[3].ToString()),
                    CantidadGarantia = int.Parse(Inventario[7].ToString()),
                    Cliente_Proveedor = Inventario[4].ToString(),
                    BusquedaCliente = (NuevoReporte.TipoBusqueda == "Cliente") ? true : false,
                    ClaveFusor = Inventario[6].ToString()
                };
                AgregarRegistrosATabla(document, nuevoReporteRegistro);

            }
            document.Add(tblInventarioRegistrosSalidas);//Finalmente agregamos el ultimo registro a la lista

            //REINICIAMOS LISTAS
            lstFechas.Clear();
            lstMarcas.Clear();
            lstModelos.Clear();
            Inventario.Close();

            //TOTALES
            ColocarTotales(NuevoReporte, document);
            return true;
        }

        public void ColocarTotales(ReporteRegistroInventario NuevoReporte,Document document)
        {
            Pdf pe = new Pdf();
            iTextSharp.text.pdf.draw.LineSeparator lineSeparator = new iTextSharp.text.pdf.draw.LineSeparator() { Offset = 2f };
            document.Add(new Chunk(lineSeparator));
            Paragraph Resumen = new Paragraph("RESUMEN ENTRADAS Y SALIDAS DEL " + NuevoReporte.FechaInicio.ToString("dd/MM/yyyy") + " - " + NuevoReporte.FechaFinal.ToString("dd/MM/yyyy"), pe.FuenteTitulo) { Alignment = Element.ALIGN_CENTER };
            document.Add(Resumen);
            document.Add(new Chunk());

            //DATOS
            ObtenerDatosResumenTotales(NuevoReporte);

            CrearTablaResumenTotales(NuevoReporte);
            document.Add(tblTotalesRegistros);
            Totales.Close();
        }

        public void CrearTablaResumenTotales(ReporteRegistroInventario NuevoReporte)
        {
            Pdf pe = new Pdf();
            tblTotalesRegistros = new PdfPTable(6);
            //En dado caso que estemos generando por 
            if (NuevoReporte.TipoBusqueda == "Cliente")
            {
                tblTotalesRegistros = (NuevoReporte.ParametroBusqueda == "CADTONER")? new PdfPTable(4): new PdfPTable(5);
            }

            PdfPCell clTituloMarca = new PdfPCell(new Phrase("Marca", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            PdfPCell clTituloModelo = new PdfPCell(new Phrase("Modelo", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 2 };
            tblTotalesRegistros.AddCell(clTituloMarca);
            tblTotalesRegistros.AddCell(clTituloModelo);


            // Definir variables de celdas fuera del switch para reutilizarlas
            PdfPCell clTituloTotalSalida = new PdfPCell(new Phrase("Total Salida", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            PdfPCell clTituloTotalEntrada = new PdfPCell(new Phrase("Total Entrada", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            PdfPCell clTituloGarantias = new PdfPCell(new Phrase("Total Garantias", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };

            switch (NuevoReporte.TipoBusqueda)
            {
                case "Cliente":
                    if (NuevoReporte.ParametroBusqueda == "CADTONER")
                    {
                        tblTotalesRegistros.AddCell(clTituloTotalEntrada); // Agregar total de entradas
                    }
                    else
                    {
                        tblTotalesRegistros.AddCell(clTituloTotalSalida); // Agregar total de salidas
                        tblTotalesRegistros.AddCell(clTituloGarantias);  // Agregar total de garantias
                    }
                    break;
                default:
                    tblTotalesRegistros.AddCell(clTituloTotalSalida); // Agregar total de salidas
                    tblTotalesRegistros.AddCell(clTituloGarantias);  // Agregar total de garantias
                    tblTotalesRegistros.AddCell(clTituloTotalEntrada); // Agregar total de entradas
                    break;
            }

            while (Totales.Read())
            {
                PdfPCell clMarca = new PdfPCell(new Phrase(Totales[0].ToString(), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };
                PdfPCell clModelo = new PdfPCell(new Phrase(Totales[1].ToString(), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 2 };
                tblTotalesRegistros.AddCell(clMarca);
                tblTotalesRegistros.AddCell(clModelo);

                PdfPCell clTotalSalida = new PdfPCell(new Phrase(Totales[2].ToString(), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };
                PdfPCell clTotalEntrada = new PdfPCell(new Phrase(Totales[3].ToString(), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };
                PdfPCell clTotalGarantias = new PdfPCell(new Phrase(Totales[4].ToString(), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };

                if (NuevoReporte.TipoBusqueda == "Cliente")
                {
                    if (NuevoReporte.ParametroBusqueda == "CADTONER")//Proveedor
                    {
                        tblTotalesRegistros.AddCell(clTotalEntrada);// Agregar total entradas
                    }
                    else
                    {
                        tblTotalesRegistros.AddCell(clTotalSalida);// Agregar total salidas
                        tblTotalesRegistros.AddCell(clTotalGarantias);
                    }
                }
                else
                {
                    //Agregar entradas y salidas
                    tblTotalesRegistros.AddCell(clTotalSalida);
                    tblTotalesRegistros.AddCell(clTotalGarantias);
                    tblTotalesRegistros.AddCell(clTotalEntrada);
                }
            }
            CantidadInicial = CantidadInicial - (CantidadSalidaTotal + CantidadEntradaTotal);


            PdfPCell clTotal = new PdfPCell(new Phrase("TOTAL ", pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 3 };
            PdfPCell clCantidadTotal = new PdfPCell(new Phrase(CantidadInicial.ToString(), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 3 };

            tblTotalesRegistros.AddCell(clTotal);
            tblTotalesRegistros.AddCell(clCantidadTotal);
        }

        public void CrearReporteCartucho(ReporteRegistroInventario NuevoReporte, Document document)
        {
            Pdf pe = new Pdf();

            if (NuevoReporte.Cliente == "")
            {
                CantidadInicial = CalcularTotalCartucho(NuevoReporte.FechaInicio, NuevoReporte.FechaFinal, NuevoReporte.ParametroBusqueda);
            }

            ObtenerDatosReporteRegistroCartucho(NuevoReporte);

            while (Inventario.Read())
            {
                DateTime fechaActual = Convert.ToDateTime(Inventario[5].ToString());
                ReporteRegistro nuevoReporteRegistro = new ReporteRegistro()
                {
                    FechaActual = fechaActual.ToString("dd/MM/yyyy"),
                    Marca = Inventario[0].ToString(),
                    Modelo = Inventario[1].ToString(),
                    CantidadSalida = int.Parse(Inventario[2].ToString()),
                    CantidadEntrada = int.Parse(Inventario[3].ToString()),
                    CantidadGarantia = int.Parse(Inventario[7].ToString()),
                    Cliente_Proveedor = Inventario[4].ToString(),
                    BusquedaCliente = (NuevoReporte.TipoBusqueda == "Cliente") ? true : false,
                    ClaveFusor = Inventario[6].ToString(),
                    //Comprobacion al buscar por modelo por cliente en especifico
                    ClienteEspecifico = (NuevoReporte.Cliente != "") ? true : false
                };

                AgregarRegistroCartucho(document, nuevoReporteRegistro);
            }

            document.Add(tblInventarioRegistrosSalidas);//Finalmente agregamos el ultimo registro a la lista 
            //Paragraph Cantidad = new Paragraph("                                                                                                                                                              " + CantidadInicial, pe.FuenteParrafoGrande) { Alignment = Element.ALIGN_CENTER };
            //document.Add(Cantidad);

            if(NuevoReporte.Cliente == "")
            {
                string espacio = new string('\u00A0', 152); // Genera 152 espacios no rompibles
                Paragraph Cantidad = new Paragraph(espacio + CantidadInicial, pe.FuenteParrafoGrande) { Alignment = Element.ALIGN_CENTER };
                document.Add(Cantidad);
            }

            //Reinicio
            ReiniciarDatos();
            Inventario.Close();

            if(NuevoReporte.Cliente == "")
            {
                ColocarTotales(NuevoReporte,document);
            }
        }

        public void ReiniciarDatos()
        {
            tblInventarioRegistrosSalidas = new PdfPTable(4);
            lstFechas.Clear();
            lstMarcas.Clear();
            lstModelos.Clear();
        }

        #endregion

        //Metodo para agregar los registros cuando estamos filtrando por modelo
        public void AgregarRegistroCartucho(Document document, ReporteRegistro nuevoReporte)
        {
            Pdf pe = new Pdf();
            //Verificamos que dicha fecha no este en la lista
            if (!lstFechas.Contains(nuevoReporte.FechaActual.ToString()))
            {
                //Si tenemos dicha fecha en la lista, agregamos los datos que tenemos de la anterior fecha
                document.Add(tblInventarioRegistrosSalidas);//Requerimos mover esta linea a otra
                //Reiniciamos nuestra tabla para agregarle datos de otra fecha, si hay
                tblInventarioRegistrosSalidas = new PdfPTable(4);

                iTextSharp.text.pdf.draw.LineSeparator lineSeparator = new iTextSharp.text.pdf.draw.LineSeparator() { Offset = 2f };
                document.Add(new Chunk(lineSeparator));

                Paragraph Fecha;

                if (nuevoReporte.ClienteEspecifico)
                {
                    Fecha = new Paragraph(nuevoReporte.FechaActual.ToString(), pe.FuenteParrafoGrande) { Alignment = Element.ALIGN_LEFT };
                }
                else
                {
                    string espacio = new string('\u00A0', 152); // Genera 152 espacios no rompibles
                    //string Cantidad = "                                                                                                                                                       " + CantidadInicial;
                    Fecha = new Paragraph(nuevoReporte.FechaActual +espacio+ CantidadInicial, pe.FuenteParrafoGrande) { Alignment = Element.ALIGN_LEFT };

                }
                //Añadimos una cadena con un espacio muy grande para que se posicione a una distancia alejada

                //Agregamos la fecha tanto a la lista que tenemos de fechas, como al documento colocando la cantidad que tenemos en dicho día
                document.Add(Fecha);
                lstFechas.Add(nuevoReporte.FechaActual.ToString());

                //Creamos una nueva tabla con su primer registro
                CrearTablaModelo(document, nuevoReporte);
            }
            else
            {
                //Como estamos en la misma fecha solamente le agregamos más datos a la tabla en la que estamos actualmente
                AgregarModeloATabla(nuevoReporte);
            }

        }

        //PARTE QUE SE VA A MODIFICAR 
        public void AgregarRegistrosATabla(Document document, ReporteRegistro nuevoReporte)
        {
            Pdf pdf = new Pdf();

            //Preguntamos si no tenemos agregada dicha fecha
            if (!lstFechas.Contains(nuevoReporte.FechaActual.ToString()))//Si se cumple quiere decir no tenemos dicha fecha
            {
                //Reiniciamos todas nuestras listas
                lstMarcas.Clear();
                lstModelos.Clear();

                document.Add(tblInventarioRegistrosSalidas);//Agregar tabla registro actual

                //Determinamos de que tipo de busqueda se trata para saber las columnas que tendra nuesta columna
                //tblInventarioRegistrosSalidas = (nuevoReporte.BusquedaCliente) ? new PdfPTable(2) : new PdfPTable(4);
                tblInventarioRegistrosSalidas = (nuevoReporte.BusquedaCliente) ? new PdfPTable(2) : new PdfPTable(5);

                //Linea separadora
                iTextSharp.text.pdf.draw.LineSeparator lineSeparator = new iTextSharp.text.pdf.draw.LineSeparator() { Offset = 2f };
                document.Add(new Chunk(lineSeparator));
                //Agregamos la fecha tanto al pdf, como al documento
                Paragraph Fecha = new Paragraph(nuevoReporte.FechaActual.ToString(), pdf.FuenteParrafoGrande) { Alignment = Element.ALIGN_LEFT };
                document.Add(Fecha);
                lstFechas.Add(nuevoReporte.FechaActual.ToString());

                //Agregamos la primer marca y modelo de dicha fecha
                lstMarcas.Add(nuevoReporte.Marca);
                document.Add(new Paragraph("-> " + nuevoReporte.Marca));

                //EN ESTA PARTE AGREGAR UNA NUEVA TABLA
                RegistrarModelo(document, nuevoReporte);
            }
            else//Si no, estamos en la misma fecha
            {
                //Solo agregamos lo que ira en las tablas
                if (lstMarcas.Contains(nuevoReporte.Marca))//Si es la misma marca entonces entramos a agregar modelos
                {
                    //document.Add(tblInventarioRegistrosSalidas);    
                    //Si ya esta la marca, preguntamos ahora por lo modelos
                    if (!lstModelos.Contains(nuevoReporte.Modelo))//Si no esta el modelo
                    {
                        //Como ya cambiamos de modelo, agregamos al documento el modelo anterior, es decir, su tabla
                        document.Add(tblInventarioRegistrosSalidas);
                        //Agregamos el modelo
                        RegistrarModelo(document, nuevoReporte);
                    }
                    else //Si esta ese modelo le seguimos agregando
                    {
                        AgregarModeloATabla(nuevoReporte);
                    }
                }
                else//Si no quiere decir que es una nueva marca
                {
                    //Agregamos la última tabla de modelos de esa marca
                    document.Add(tblInventarioRegistrosSalidas);
                    //Colocamos en el documento la nueva marca
                    lstMarcas.Add(nuevoReporte.Marca);
                    document.Add(new Paragraph("-> " + nuevoReporte.Marca));

                    //Agregamos el primer modelo
                    RegistrarModelo(document, nuevoReporte);
                }
            }
        }

        //REGITRAR MODELO EN DOCUMENTO Y LISTA, POSTERIORMENTE CREAR DICHA TABLA
        public void RegistrarModelo(Document document, ReporteRegistro nuevoReporte)
        {
            lstModelos.Add(nuevoReporte.Modelo);//Agregar a la lista de Modelos
            document.Add(new Paragraph("      " + nuevoReporte.Modelo));
            
            CrearTablaModelo(document, nuevoReporte);
        }


        //COLOCAR EL MODELO EN UNA TABLA YA EXITENTE
        public void AgregarModeloATabla(ReporteRegistro registro)
        {
            Pdf pe = new Pdf();
            PdfPCell clCantidadSalidaDato = new PdfPCell(new Phrase(ComprobarValoresO(registro.CantidadSalida.ToString()), pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            PdfPCell clCantidadEntradaDato = new PdfPCell(new Phrase(ComprobarValoresO(registro.CantidadEntrada.ToString()), pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            PdfPCell clCantidadGarantiaDato = new PdfPCell(new Phrase(ComprobarValoresO(registro.CantidadGarantia.ToString()), pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };//QUITAR


            tblInventarioRegistrosSalidas.AddCell(clCantidadSalidaDato);
            tblInventarioRegistrosSalidas.AddCell(clCantidadEntradaDato);
            tblInventarioRegistrosSalidas.AddCell(clCantidadGarantiaDato);//QUITAR
            //Sumar
            if (registro.BusquedaCliente)
            {
                ActualizarTotalCantidadInicial(registro);
            }
            else 
            {
                ModificarCantidadInicial(registro);
            }

            if (!registro.BusquedaCliente)
            {
                PdfPCell clClienteProveedorDato = new PdfPCell(new Phrase(registro.Cliente_Proveedor, pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 2 };
                tblInventarioRegistrosSalidas.AddCell(clClienteProveedorDato);
            }

            if (registro.ClaveFusor != "" && registro.ClaveFusor != " " || registro.Modelo.StartsWith("DR"))
            {
                PdfPCell clFusorDato = new PdfPCell(new Phrase(registro.ClaveFusor, pe.FuenteParrafoBold10)) { BorderWidth = .5f, Colspan = 1 };
                tblInventarioRegistrosSalidas.AddCell(clFusorDato);
            }
            //else
            //{
            //    PdfPCell clFusorDato = new PdfPCell(new Phrase(" ", pe.FuenteParrafoBold10)) { BorderWidth = .5f, Colspan = 1 };
            //    tblInventarioRegistrosSalidas.AddCell(clFusorDato);
            //}
        }

        //SUMAMOS O RESTANOS DEPENDIENDO EL TIPO DE ENTRADA
        public void ModificarCantidadInicial(ReporteRegistro registro)
        {
            if (registro.CantidadSalida > 0)
            {
                CantidadInicial -= registro.CantidadSalida;
            }
            else if (registro.CantidadGarantia > 0)
            {
                CantidadInicial -= registro.CantidadGarantia;
            }
            else if (registro.CantidadEntrada > 0)
            {
                CantidadInicial += registro.CantidadEntrada;
            }
        }

        //SUMAR SALIDAS DE UN CLIENTE O SUMAR ENTRADAS DE UN PROVEEDOR
        public void ActualizarTotalCantidadInicial(ReporteRegistro registro)
        {
            if (registro.CantidadSalida > 0)
            {
                CantidadInicial += registro.CantidadSalida;
            }
            else if (registro.CantidadGarantia > 0)
            {
                CantidadInicial += registro.CantidadGarantia;
            }
            else if(registro.CantidadEntrada > 0)
            {
                CantidadInicial += registro.CantidadEntrada;
            }
        }

        public void CrearTablaModelo(Document document, ReporteRegistro nuevoReporte)
        {
            Pdf pe = new Pdf();
            int NumeroColumnas = nuevoReporte.BusquedaCliente ? 3 : 5; // Determina el número de columnas basado en si es una búsqueda de cliente o no

            var prefijosValidos = new HashSet<string> { "RM1-", "D01SE", "R6", "DR","MFC" };
            // Para saber si se trata de un fusor o drum de brother
            if (prefijosValidos.Any(prefijo => nuevoReporte.Modelo.StartsWith(prefijo)))
            {
                NumeroColumnas = 6;
                if (nuevoReporte.CantidadSalida < 0 || nuevoReporte.CantidadGarantia < 0)
                {
                    NumeroColumnas = 5;
                }

                if (nuevoReporte.BusquedaCliente)
                {
                    NumeroColumnas = 4;
                }
            }

            tblInventarioRegistrosSalidas = new PdfPTable(NumeroColumnas);
            AgregarTituloColumnaTabla("Salida", 1);
            AgregarTituloColumnaTabla("Entrada", 1);
            AgregarTituloColumnaTabla("Garantia", 1);

            if (!nuevoReporte.BusquedaCliente)//Si no estamos buscando por cliente
            {
                AgregarTituloColumnaTabla("Cliente/Proveedor", 2);//Agregar cliente o proveedor

                if (NumeroColumnas > 5)
                {
                    AgregarTituloColumnaTabla("Clave", 1);
                }
            }

            if (nuevoReporte.BusquedaCliente && NumeroColumnas == 4)
            {
                AgregarTituloColumnaTabla("Clave", 1);
            }

            AgregarModeloATabla(nuevoReporte);
        }

        public void AgregarTituloColumnaTabla(string celda, int colspan)
        {
            Pdf pdf = new Pdf();
            PdfPCell clTitulo = new PdfPCell(new Phrase(celda, pdf.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = colspan };
            tblInventarioRegistrosSalidas.AddCell(clTitulo);
        }


        public string ComprobarValoresO(string Cantidad)
        {
            return Cantidad = (Cantidad == "0") ?" ":Cantidad;
        }
        #endregion
    }
}
