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
using Microsoft.Win32;
using System.Windows.Forms;

namespace CobranzaSP.Lógica
{
    internal class LogicaRegistroInventarioRicoh
    {
        private CD_Conexion conexion = new CD_Conexion();
        private AccionesLógica NuevaAccion = new AccionesLógica();
        SqlCommand comando = new SqlCommand();
        SqlDataReader reporte;
        PdfPTable tblRegistroPartesRicoh;
        DataTable tblDatosReporte = new DataTable();
        Document document;

        SortedSet<string> lstFechas = new SortedSet<string>();
        SortedSet<string> lstModelos = new SortedSet<string>();
        SortedSet<string> lstPartes = new SortedSet<string>();
        bool DatosObtenidos;
        string TipoBusqueda;

        #region CRUD
        public string AgregarRegistroInventario(MovimientoParteRicoh NuevoMovimiento)
        {
            SqlDataReader leer;
            int valor = 0;
            comando.Connection = conexion.AbrirConexion();

            comando.CommandText = "spModificarCantidadInventarioPartes";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@IdParte", NuevoMovimiento.IdParte);
            comando.Parameters.AddWithValue("@Cantidad", NuevoMovimiento.Cantidad);
            comando.Parameters.AddWithValue("@IdMovimiento", NuevoMovimiento.IdMovimiento);
            valor = comando.ExecuteNonQuery();
            //Se modificara el inventario dependiendo si es una entrada o una salida

            //Nos ayuda a comprobar si el inventario fue modificado(Dependiendo si se haya modificado algo o no)
            if (valor > 0)
            {
                return GuardarRegistro(NuevoMovimiento);
            }
            else
            {
                conexion.CerrarConexion();
                return "No se ha agregado el registro. La cantidad excede la cantidad en existencia";
            }
        }

        public string GuardarRegistro(MovimientoParteRicoh NuevoMovimiento)
        {
            int Respuesta = 0;
            string Mensaje = "";
            string AccionRealizada = (NuevoMovimiento.IdRegistro > 0) ? "modifico" : "agrego";
            comando.Connection = conexion.AbrirConexion();


            comando.CommandText = "spGuardarRegistroInventarioParte";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@IdRegistro", NuevoMovimiento.IdRegistro);
            comando.Parameters.AddWithValue("@IdParte", NuevoMovimiento.IdParte);
            comando.Parameters.AddWithValue("@IdMovimiento", NuevoMovimiento.IdMovimiento);
            comando.Parameters.AddWithValue("@IdEntidad", NuevoMovimiento.IdTipoPersona);
            comando.Parameters.AddWithValue("@Cantidad", NuevoMovimiento.Cantidad);
            comando.Parameters.AddWithValue("@Fecha", NuevoMovimiento.Fecha);
            comando.Parameters.AddWithValue("@Folio", NuevoMovimiento.Folio);
            Respuesta = comando.ExecuteNonQuery();
            Mensaje = (Respuesta > 0) ? "Registro se " + AccionRealizada + " correctamente" : "Algo salio mal, no se " + AccionRealizada;
            conexion.CerrarConexion();
            return Mensaje;
        }

        public string EliminarRegistro(MovimientoParteRicoh NuevoMovimiento)
        {
            int Respuesta = 0;
            string Mensaje = "";
            comando.Connection = conexion.AbrirConexion();


            comando.CommandText = "spEliminarRegistroInventarioPartes";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@IdRegistro", NuevoMovimiento.IdRegistro);
            comando.Parameters.AddWithValue("@IdMovimiento", NuevoMovimiento.IdMovimiento);
            comando.Parameters.AddWithValue("@IdParte", NuevoMovimiento.IdParte);
            comando.Parameters.AddWithValue("@Cantidad", NuevoMovimiento.Cantidad);

            Respuesta = comando.ExecuteNonQuery();
            Mensaje = (Respuesta > 0) ? "Registro se elimino correctamente" : "Algo salio mal, no se elimino el registro";
            conexion.CerrarConexion();
            return Mensaje;
        }

        public SqlDataReader LlenarComboBoxPartes(ComboBox cb,string Modelo)
        {
            SqlDataReader leer;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "spSeleccionarPartesModeloEquipoEspecifico";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@Modelo", Modelo);
            leer = comando.ExecuteReader();
            

            cb.Items.Clear();

            while (leer.Read())
            {
                //Agregamos las opciones dependiendo los registros que nos devolvieron
                cb.Items.Add(leer[0].ToString());
            }
            //Agregamos un espacio en blanco y lo asignamos como opcion por defecto
            cb.Items.Insert(0, " ");
            cb.SelectedIndex = 0;
            leer.Close();
            conexion.CerrarConexion();
            return leer;
        }

        public DataTable MostrarPartes(string NumeroFolio)
        {
            DataTable tabla = new DataTable();
            SqlDataReader leer;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "MostrarRegistrosPartesUsadas";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@NumeroFolio", NumeroFolio);
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conexion.CerrarConexion();
            leer.Close();
            return tabla;
        }

        public int BuscarIdParte(string Parte, string ModeloImpresora)
        {
            SqlDataReader ver;
            int id = 0;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "spObtenerIdParte";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@Parte", Parte);
            comando.Parameters.AddWithValue("@Modelo", ModeloImpresora);
            ver = comando.ExecuteReader();

            while (ver.Read())
            {
                id = int.Parse(ver[0].ToString());
            }

            conexion.CerrarConexion();
            ver.Close();
            return id;
        }
        #endregion


        #region PDF
        public bool DeterminarTipoReporte(ReporteRegistroParte NuevoReporte)
        {
            DatosObtenidos = true;
            TipoBusqueda = NuevoReporte.TipoBusqueda;
            tblDatosReporte.Clear();
            switch (NuevoReporte.TipoBusqueda)
            {
                case "Salidas": ObtenerDatosReporteSalidasPartesRicoh(NuevoReporte); break;
                case "Entradas": ObtenerDatosReporteEntradasPartesRicoh(NuevoReporte); break;
                case "Fecha": ObtenerDatosReporteMovimientosPartesRicoh(NuevoReporte); break;
            }
            return DatosObtenidos;
        }

        #region ObtenerDatosParaReporte
        public void ObtenerDatosReporteSalidasPartesRicoh(ReporteRegistroParte NuevoReporte)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "spReporteRegistroPartesSalidas";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@FechaInicio", NuevoReporte.FechaInicio);
            comando.Parameters.AddWithValue("@FechaFinal", NuevoReporte.FechaFinal);
            comando.Parameters.AddWithValue("@ModeloParteRicoh", NuevoReporte.ModeloParteRicoh);
            comando.Parameters.AddWithValue("@ParteRicoh", NuevoReporte.Parte);
            comando.Parameters.AddWithValue("@NumeroSerie", NuevoReporte.Serie);
            tblDatosReporte.Load(comando.ExecuteReader());
            comando.Dispose();

            if (tblDatosReporte.Rows.Count == 0)
            {
                DatosObtenidos = false;
                return;
            }

            Pdf(NuevoReporte);
        }

        public void ObtenerDatosReporteEntradasPartesRicoh(ReporteRegistroParte NuevoReporte)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "spReporteRegistroPartesEntradas";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@FechaInicio", NuevoReporte.FechaInicio);
            comando.Parameters.AddWithValue("@FechaFinal", NuevoReporte.FechaFinal);
            comando.Parameters.AddWithValue("@ModeloParteRicoh", NuevoReporte.ModeloParteRicoh);
            comando.Parameters.AddWithValue("@ParteRicoh", NuevoReporte.Parte);
            tblDatosReporte.Load(comando.ExecuteReader());
            comando.Dispose();

            if (tblDatosReporte.Rows.Count == 0)
            {
                DatosObtenidos = false;
                return;
            }

            Pdf(NuevoReporte);
        }

        public void ObtenerDatosReporteMovimientosPartesRicoh(ReporteRegistroParte NuevoReporte)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "spReporteRegistroPartesFecha";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@FechaInicio", NuevoReporte.FechaInicio);
            comando.Parameters.AddWithValue("@FechaFinal", NuevoReporte.FechaFinal);
            tblDatosReporte.Load(comando.ExecuteReader());
            comando.Dispose();

            if (tblDatosReporte.Rows.Count == 0)
            {
                DatosObtenidos = false;
                return;
            }

            Pdf(NuevoReporte);
        }


        public void ObtenerDatosResumen(Reporte NuevoReporte)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "CalcularTotalesInventarioRicoh";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@FechaInicio", NuevoReporte.FechaInicio);
            comando.Parameters.AddWithValue("@FechaFinal", NuevoReporte.FechaFinal);
            comando.Parameters.AddWithValue("@Parametro", NuevoReporte.ParametroBusqueda);
            reporte = comando.ExecuteReader();
            if (!reporte.HasRows)
            {
                reporte.Close();
            }

        }
        #endregion


        public void Pdf(ReporteRegistroParte NuevoReporte)
        {
            string NombreArchivo = @"\\DESKTOP-34U2P48\Archivos Compartidos\Reportes\Partes\" + "RegistroPartesRicoh" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf";

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
            tblRegistroPartesRicoh = new PdfPTable(5);
            document.Open();

            Paragraph Fechas = new Paragraph("DEL " + NuevoReporte.FechaInicio.ToString("dd/MM/yyyy") + " AL " + NuevoReporte.FechaFinal.ToString("dd/MM/yyyy"), pe.FuenteFecha) { Alignment = Element.ALIGN_CENTER };
            document.Add(Fechas);


            //Titulo del Reporte
            string NombreReporte = ColocarTituloReporte(NuevoReporte);
            Paragraph titulo = new Paragraph(NombreReporte, pe.FuenteTitulo) { Alignment = Element.ALIGN_CENTER };
            document.Add(titulo);

            //Verificamos si tenemos rango de fecha
            CrearReporteRegistroPartesRicoh(NuevoReporte);


            
            //Agregamos el ultimo registro
            //document.Add(tblClavesUsadas);
            
            document.Add(new Chunk());

            ////Agregamos el resumen de las salidas y entradas
            //Paragraph Resumen = new Paragraph("RESUMEN ENTRADAS Y SALIDAS DEL " + NuevoReporte.FechaInicio.ToString("dd/MM/yyyy") + " - " + NuevoReporte.FechaFinal.ToString("dd/MM/yyyy"), pe.FuenteTitulo) { Alignment = Element.ALIGN_CENTER };
            //document.Add(Resumen);
            //document.Add(new Chunk());
            //ObtenerDatosResumen(NuevoReporte);
            //CrearTablaResumen();

            //document.Add(tblClavesUsadas);
            document.Add(tblRegistroPartesRicoh);

            //Reinicamos
            ReiniciarListas();
            lstFechas.Clear();
            document.Close();

            //Abrimos el pdf 
            pe.AbrirPdf(NombreArchivo);
        }

        public void ReiniciarListas()
        {
            
            lstModelos.Clear();
            lstPartes.Clear();
        }

        public string ColocarTituloReporte(ReporteRegistroParte NuevoReporte)
        {
            StringBuilder TituloBuilder = new StringBuilder("DETALLADO ENTRADAS Y SALIDAS DE PARTES RICOH");

            if(NuevoReporte.TipoBusqueda == "Fecha")
            {
                return TituloBuilder.ToString();
            }
            TituloBuilder.Clear();
            string TituloPrincipal = (NuevoReporte.TipoBusqueda == "Salidas") ? "DETALLADO SALIDAS DE PARTES RICOH" : "DETALLADO ENTRADAS DE PARTES RICOH";
            TituloBuilder.Append(TituloPrincipal);
            if (NuevoReporte.Parte != "")
            {
                TituloBuilder.Append("\n PARTE:" + NuevoReporte.Parte);
            }
            if (NuevoReporte.TipoBusqueda == "Salidas")
            {
                if (NuevoReporte.Serie != "")
                {
                    TituloBuilder.Append("\n Serie:" + NuevoReporte.Serie);
                }
            }
            return TituloBuilder.ToString();
        }

        public void CrearReporteRegistroPartesRicoh(ReporteRegistroParte NuevoReporte)
        {
            foreach (DataRow fila in tblDatosReporte.Rows)
            {
                DatosPdfRegistroPartesRicoh DatosPdf = new DatosPdfRegistroPartesRicoh()
                {
                    ModeloParteRicoh = fila[0].ToString(),
                    Parte = fila[1].ToString(),
                    Entidad = fila[2].ToString(),
                    Cantidad = int.Parse(fila[3].ToString()),
                    TipoMovimiento = fila[4].ToString(),
                    Fecha = DateTime.Parse(fila[5].ToString()),
                    Folio = fila[6].ToString()
                };
                if(TipoBusqueda == "Salidas")
                {
                    DatosPdf.SerieEquipo = fila[7].ToString();
                    DatosPdf.MarcaEquipo = fila[8].ToString();
                    DatosPdf.ModeloEquipo = fila[9].ToString();
                }
                AgregarFechaAlDocumento(DatosPdf);
            }
        }

        public void AgregarFechaAlDocumento(DatosPdfRegistroPartesRicoh DatosPdf)
        {
            Pdf pe = new Pdf();
            if (!lstFechas.Contains(DatosPdf.Fecha.ToString()))
            {
                document.Add(tblRegistroPartesRicoh);
                tblRegistroPartesRicoh = new PdfPTable(3);
                lstFechas.Add(DatosPdf.Fecha.ToString());
                document.Add(new Paragraph(DatosPdf.Fecha.ToString("dd/MM/yyyy"), pe.FuenteFecha));
                ReiniciarListas();
                AgregarModeloRicohAlDocumento(DatosPdf);
            }
            else
            {
                AgregarModeloRicohAlDocumento(DatosPdf);
            }
        }

        public void CrearTablaMovimientosPartesRicoh()
        {
            Pdf pe = new Pdf();

            //Determinar tamaño de la tabla
            switch (TipoBusqueda)
            {
                case "Salidas":tblRegistroPartesRicoh = new PdfPTable(6);break;//revisarlo al ultimo
                case "Entradas": tblRegistroPartesRicoh = new PdfPTable(4); break;
                default: tblRegistroPartesRicoh = new PdfPTable(5); break;
            }
            if(TipoBusqueda != "Entradas")
            {
                AgregarTituloCelda("SALIDAS", 1);
            }
            if(TipoBusqueda == "Fecha" || TipoBusqueda == "Entradas")
            {
                AgregarTituloCelda("ENTRADAS", 1);
            }
            AgregarTituloCelda("CLIENTE/PROVEEDOR", 2);
            AgregarTituloCelda("FOLIO", 1);

            //QUEDARAN PENDIENTES LOS CAMPOS DE LAS SALIDAS
            if(TipoBusqueda == "Salidas")
            {
                AgregarTituloCelda("SERIE EQUIPO", 1);
                AgregarTituloCelda("EQUIPO", 1);
            }
        }

        void AgregarTituloCelda(string Titulo, int colspan)
        {
            Pdf pe = new Pdf();
            PdfPCell Celda = new PdfPCell(new Phrase(Titulo, pe.FuenteParrafoBold))
            {
                BorderWidth = .5f,
                Padding = 1,
                Colspan = colspan
            };
            tblRegistroPartesRicoh.AddCell(Celda);
        }


        public void AgregarModeloRicohAlDocumento(DatosPdfRegistroPartesRicoh DatosPdf)
        {
            Pdf pe = new Pdf();
            
            if (!lstModelos.Contains(DatosPdf.ModeloParteRicoh.ToString()))
            {
                lstPartes.Clear();
                document.Add(tblRegistroPartesRicoh);
                tblRegistroPartesRicoh = new PdfPTable(3);
                string espacio = new string('\u00A0', 3);
                document.Add(new Paragraph(espacio + DatosPdf.ModeloParteRicoh));
                lstModelos.Add(DatosPdf.ModeloParteRicoh);
                AgregarParteRicohAlDocumento(DatosPdf);
            }
            else
            {
                AgregarParteRicohAlDocumento(DatosPdf);
            }
        }

        public void AgregarParteRicohAlDocumento(DatosPdfRegistroPartesRicoh DatosPdf)
        {
            Pdf pe = new Pdf();

            if (!lstPartes.Contains(DatosPdf.Parte.ToString()))
            {
                document.Add(tblRegistroPartesRicoh);
                string espacio = new string('\u00A0', 6);
                document.Add(new Paragraph(espacio + DatosPdf.Parte));
                lstPartes.Add(DatosPdf.Parte);
                CrearTablaMovimientosPartesRicoh();
                AgregarDatosATabla(DatosPdf);
            }
            else
            {
                AgregarDatosATabla(DatosPdf);
            }
        }


        public void AgregarDatosATabla(DatosPdfRegistroPartesRicoh DatosPdf)
        {
            if(TipoBusqueda == "Fecha")
            {
                if(DatosPdf.TipoMovimiento == "Entrada")
                {
                    AgregarColumnaATabla("0", 1);
                    AgregarColumnaATabla(DatosPdf.Cantidad.ToString(),1);
                }
                else
                {
                    AgregarColumnaATabla(DatosPdf.Cantidad.ToString(), 1);
                    AgregarColumnaATabla("0", 1);
                }
            }
            else
            {
                AgregarColumnaATabla(DatosPdf.Cantidad.ToString(), 1);
            }
            AgregarColumnaATabla(DatosPdf.Entidad.ToString(), 2);
            AgregarColumnaATabla(DatosPdf.Folio.ToString(), 1);

            if(TipoBusqueda == "Salidas")
            {
                AgregarColumnaATabla(DatosPdf.SerieEquipo.ToString(), 1);
                AgregarColumnaATabla(DatosPdf.MarcaEquipo.ToString() + " " + DatosPdf.ModeloEquipo.ToString(), 1);
            }


        }
        public void AgregarColumnaATabla(string texto, int ancho)
        {
            Pdf pe = new Pdf();
            PdfPCell clCelda = new PdfPCell(new Phrase(texto, pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = ancho };
            tblRegistroPartesRicoh.AddCell(clCelda);
        }
        #endregion

    }
}
