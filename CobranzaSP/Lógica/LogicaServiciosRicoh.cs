using CobranzaSP.Modelos;
using DocumentFormat.OpenXml.Bibliography;
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
using CobranzaSP.Formularios;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace CobranzaSP.Lógica
{
    internal class LogicaServiciosRicoh
    {
        private CD_Conexion conexion = new CD_Conexion();
        SqlCommand comando = new SqlCommand();
        SqlCommand SegundoComando = new SqlCommand();
        SqlDataReader reporte;
        SqlDataReader DatosModulos;
        LogicaModulosCliente lgModuloEquipo = new LogicaModulosCliente();
        LogicaModulosCliente lgModuloCliente = new LogicaModulosCliente();
        AccionesLógica NuevaAccion = new AccionesLógica();


        //LISTAS
        SortedSet<string> lstSeries = new SortedSet<string>();
        SortedSet<string> lstFechas = new SortedSet<string>();
        SortedSet<string> lstReportes = new SortedSet<string>();
        SortedSet<string> lstModulos = new SortedSet<string>();


        PdfPTable tblSeries;
        PdfPTable tblModulos;

        string TipoBusqueda = "";

        string Serie = "";

        public string ObtenerModeloEquipo(string SerieEquipo, string sp)
        {
            SqlDataReader leer;
            string Modelo = "";
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = sp;
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();

            comando.Parameters.AddWithValue("@Serie", SerieEquipo);
            leer = comando.ExecuteReader();
            while (leer.Read())
            {
                Modelo = leer[0].ToString();
            }
            conexion.CerrarConexion();
            leer.Close();
            return Modelo;
        }

        //Con este metodo agregaremos un nuevo registro por cada clave cada vez que se registre un reporte en base al contador
        public void ActualizarModulosEquipo(Servicio nuevoServicio)
        {
            string Modelo = DeterminarModeloModulo(nuevoServicio.Modelo);
            //Actualizaremos cada clave, tanto en los modulos_clientes como en historial_modulo
            foreach (ModuloEquipo Modulo in LogicaModulosCliente.ModulosEquipo)
            {
                ActualizarModulo(nuevoServicio, Modulo, Modelo);
            }

            lgModuloEquipo.ActualizarReporteActualModulos(nuevoServicio);
        }

        public void ActualizarModulo(Servicio nuevoServicio, ModuloEquipo Modulo, string Modelo)
        {
            Modulo_Cliente NuevoModulo = new Modulo_Cliente()
            {
                IdModelo = NuevaAccion.BuscarId(Modelo, "ObtenerIdModeloModulo"),

                Reporte = nuevoServicio.NumeroFolio,
                Serie = nuevoServicio.Serie,
                Paginas = nuevoServicio.Contador,
                Estado = "ACTUALIZADO",
                Clave = Modulo.Clave,
                Observacion = ""
            };
            NuevoModulo.IdModulo = lgModuloCliente.BuscarIdModulo(Modulo.Modulo, NuevoModulo.IdModelo);
            lgModuloEquipo.RegistrarModulo(NuevoModulo, "AgregarEstadoModuloEquipo");
        }



        public string DeterminarModeloModulo(string Modelo)
        {
            var prefijosValidos = new HashSet<string> { "MP-400", "MP-500", "90" };
            //Para saber si se trata de un fusor
            if (prefijosValidos.Any(prefijo => Modelo.StartsWith(prefijo)))
            {
                return "4002";
            }

            return "5054";
        }

    }
}
