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
    internal class LogicaInventarioPartesRicoh
    {
        private CD_Conexion conexion = new CD_Conexion();
        SqlCommand comando = new SqlCommand();



        public string GuardarParte(ParteRicoh NuevaParte)
        {
            int respuesta = 0;
            string AccionRealizada;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "spGuardarParte";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();

            //Preguntamos que accion realizaremos en la base de datos para posteriormente mostrale al usuario la accion que realizo
            AccionRealizada = (NuevaParte.IdParte > 0) ? "modifico" : "agrego";

            comando.Parameters.AddWithValue("@IdNumeroParte", NuevaParte.IdParte);
            comando.Parameters.AddWithValue("@NumeroParte", NuevaParte.NumeroParte);
            comando.Parameters.AddWithValue("@IdModelo", NuevaParte.IdModelo);
            comando.Parameters.AddWithValue("@Descripcion", NuevaParte.Descripcion);
            comando.Parameters.AddWithValue("@Cantidad", NuevaParte.Cantidad);
            comando.Parameters.AddWithValue("@UrlImagen", NuevaParte.UrlImagen);


            respuesta = comando.ExecuteNonQuery();
            string Mensaje = (respuesta > 0) ? "Parte se " + AccionRealizada + " correctamente" : "Algo salio mal, no se " + AccionRealizada + " el registro";

            comando.Parameters.Clear();
            conexion.CerrarConexion();
            return Mensaje;
        }

        

        public string GuardarModeloParte(string Modelo)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "spAgregarModeloParte";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@Modelo", Modelo);
            comando.ExecuteNonQuery();
            string Mensaje = "Se agrego el modelo de parte correctamente";

            comando.Parameters.Clear();
            conexion.CerrarConexion();
            return Mensaje;
        }

        public void GuardarRelacionModeloPartes(ModeloParte NuevoModelo)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "spAgregarRelacionModeloParte";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@IdModeloParte", NuevoModelo.IdModelo);
            comando.Parameters.AddWithValue("@IdModeloImpresora", NuevoModelo.IdModeloParte);
            comando.ExecuteNonQuery();

            comando.Parameters.Clear();
            conexion.CerrarConexion();
        }


        #region Pdf
        #endregion

        public void ImprimirInventario(bool FormatoBlanco)
        {
            string NombreArchivo = CrearPDF(FormatoBlanco, DateTime.Now);
            var pe = new Pdf();

            //Abrimos el pdf
            pe.AbrirPdf(NombreArchivo);
        }

        public string CrearPDF(bool FormatoBlanco, DateTime FechaCreacion)
        {
            DataTable tblDatosInventario = new DataTable();
            //Primero obtenemos de la base de datos nuestro inventario 
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "spReporteInventarioPartes";
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@FormatoBlanco", FormatoBlanco);

            comando.CommandType = CommandType.StoredProcedure;
            tblDatosInventario.Load(comando.ExecuteReader());

            //CONFIGURAR DOCUMENTO PDF
            string RutaArchivo = ConfiguracionPdf.RutaReportesInventarioPartes;
            string NombreArchivo = RutaArchivo + "Inventario" + FechaCreacion.ToString("ddMMyyyyHHmmss") + ".pdf";

            FileStream fs = new FileStream(NombreArchivo, FileMode.Create);
            Document document = new Document(PageSize.LETTER);
            document.SetMargins(25f, 25f, 25f, 25f);
            document.SetPageSize(iTextSharp.text.PageSize.LETTER);
            PdfWriter pw = PdfWriter.GetInstance(document, fs);

            var pe = new Pdf();
            pe.ColocarFormatoSuperior = false;
            pw.PageEvent = pe;

            document.Open();

            Paragraph titulo = new Paragraph("FORMATO INVENTARIO PARTES RICOH", pe.FuenteTitulo18);
            titulo.Alignment = Element.ALIGN_CENTER;
            document.Add(titulo);

            Paragraph Fecha = new Paragraph("Fecha: " + FechaCreacion.ToString("dd/MM/yyyy"), pe.FuenteFechaGrande) { Alignment = Element.ALIGN_CENTER };
            document.Add(Fecha);

            //Paragraph Hora = new Paragraph("Hora: " + DateTime.Now.ToString("hh:mm:ss tt"), pe.FuenteFechaGrande) { Alignment = Element.ALIGN_CENTER };
            //document.Add(Hora);

            PdfPTable tblInventario = new PdfPTable(5);
            //PdfPTable tblInventario = new PdfPTable(5);

            document.Add(new Paragraph("\n"));

            //Agregamos los titulos de la tabla
            PdfPCell clClave = new PdfPCell(new Phrase("MODELO", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            PdfPCell clNumeroParte = new PdfPCell(new Phrase("NUMERO DE PARTE", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            PdfPCell clDescripcion = new PdfPCell(new Phrase("DESCRIPCION", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 2 };
            PdfPCell clExistencias = new PdfPCell(new Phrase("EXISTENCIA", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            //PdfPCell clSalidas = new PdfPCell(new Phrase("SALIDAS", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };
            //PdfPCell clEntradas = new PdfPCell(new Phrase("ENTRADAS", pe.FuenteParrafoBold)) { BorderWidth = .5f, Colspan = 1 };

            tblInventario.AddCell(clClave);
            tblInventario.AddCell(clNumeroParte);
            tblInventario.AddCell(clDescripcion);
            tblInventario.AddCell(clExistencias);
            //tblInventario.AddCell(clSalidas);
            //tblInventario.AddCell(clEntradas);

            foreach (DataRow fila in tblDatosInventario.Rows)
            {
                PdfPCell clModeloDato = new PdfPCell(new Phrase(fila[0].ToString(), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };
                PdfPCell clNumeroParteDato = new PdfPCell(new Phrase(fila[1].ToString(), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };
                PdfPCell clDescripcionDato = new PdfPCell(new Phrase(fila[2].ToString(), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 2 };
                string Cantidad = (FormatoBlanco) ?"": fila[3].ToString();
                PdfPCell clExistenciasDato = new PdfPCell(new Phrase(Cantidad, pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };
                //PdfPCell clSalidasDato = new PdfPCell(new Phrase(ComprobarValoresO(fila[3].ToString()), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };
                //PdfPCell clEntradasDato = new PdfPCell(new Phrase(ComprobarValoresO(fila[4].ToString()), pe.FuenteParrafo)) { BorderWidth = .5f, Colspan = 1 };

                tblInventario.AddCell(clModeloDato);
                tblInventario.AddCell(clNumeroParteDato);
                tblInventario.AddCell(clDescripcionDato);
                tblInventario.AddCell(clExistenciasDato);
            }

            //Añadimos la tabla al documento
            document.Add(tblInventario);
            tblDatosInventario.Clear();
            document.Close();
            conexion.CerrarConexion();
            return NombreArchivo;
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

        //Guardar el inventario de ricoh


    }
}
