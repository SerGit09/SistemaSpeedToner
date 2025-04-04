﻿using CobranzaSP.Modelos;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobranzaSP.Lógica
{
    internal class LogicaModulosCliente
    {
        CD_Conexion conexion = new CD_Conexion();
        SqlCommand comando = new SqlCommand();

        
        //public static List<string> Claves { get; set; } = new List<string>();
        public static List<ModuloRicoh> ModulosEquipo { get; set; } = new List<ModuloRicoh>();
        //Lista que nos servira para las claves a las cuales se le modifico su observacion
        public static List<string> ClavesObservaciones = new List<string>();
        LogicaModulosBodega lgModuloBodega = new LogicaModulosBodega();
        LogicaReportesModulo lgReporteModulo = new LogicaReportesModulo();
        public string RegistrarModulo(RegistroModulo NuevoModulo, string sp)
        {
            string AccionRealizada = "agrego";
            int respuesta;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = sp;
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            if (NuevoModulo.IdRegistroModulo > 0)
            {
                AccionRealizada = "modifico";
                comando.Parameters.AddWithValue("@Id", NuevoModulo.IdRegistroModulo);
            }
            comando.Parameters.AddWithValue("@IdModulo", NuevoModulo.IdModulo);
            comando.Parameters.AddWithValue("@Clave", NuevoModulo.Clave);
            comando.Parameters.AddWithValue("@Folio", NuevoModulo.Reporte);
            comando.Parameters.AddWithValue("@Contador", NuevoModulo.Paginas);
            comando.Parameters.AddWithValue("@Estado", NuevoModulo.Estado);
            comando.Parameters.AddWithValue("@Serie", NuevoModulo.Serie);
            comando.Parameters.AddWithValue("@Observacion", NuevoModulo.Observacion);

            respuesta = comando.ExecuteNonQuery();
            string Mensaje = (respuesta > 0) ? "Se " + AccionRealizada + " el modulo correctamente" : "Algo salio mal, no se " + AccionRealizada + " el modulo";


            comando.Parameters.Clear();
            conexion.CerrarConexion();
            return Mensaje;
        }

        public void ActualizarReporteActualModulos(Servicio DatosServicio)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "ActualizarReporteActualAModulo";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@ReporteActual", DatosServicio.NumeroFolio);
            comando.Parameters.AddWithValue("@Serie", DatosServicio.Serie);
            comando.ExecuteNonQuery();
            conexion.CerrarConexion();
        }

        public void RetirarModulo(RegistroModulo NuevoModulo, bool ModuloSeleccionado)
        {
            int respuesta;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "AgregarEstadoModuloEquipo";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@IdModulo", NuevoModulo.IdModulo);
            comando.Parameters.AddWithValue("@Clave", NuevoModulo.ClaveAnterior);
            comando.Parameters.AddWithValue("@Folio", NuevoModulo.Reporte);
            comando.Parameters.AddWithValue("@Contador", NuevoModulo.Paginas);
            comando.Parameters.AddWithValue("@Estado", "RETIRADO");
            comando.Parameters.AddWithValue("@Serie", NuevoModulo.Serie);
            comando.Parameters.AddWithValue("@Observacion", NuevoModulo.Observacion);


            respuesta = comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            conexion.CerrarConexion();

            //Una vez que acabemos de retirar el modulo, sera necesario borrar del inventario de bodega el 
            //Modulo que se dejo instalado, y agregar el que se retiro
            //DEBEMOS DE VALIDAR CUANDO SEA UN RETIRO CON UN MODULO SELECCIONADO
            if (ModuloSeleccionado)
            {
                //Deberemos de agregar en el inventario la que retiramos y eliminar la que dejamos instalada
                ModificarInventarioModulosBodega(NuevoModulo);
            }
            //else//Si no se selecciono un modulo
            //{
            //    //Solamente agregaremos la que se retiro
            //    RegistrarModuloABodega(NuevoModulo);
            //}
        }

        public string ObtenerSerieClaveRepetida(string Clave)
        {
            SqlDataReader ver;
            string Serie = "";
            string Equipo = "";
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "ObtenerSerieClaveAsignada";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@Clave", Clave);
            ver = comando.ExecuteReader();

            while (ver.Read())
            {
                Serie = ver[0].ToString();
            }

            conexion.CerrarConexion();
            ver.Close();
            Equipo = lgReporteModulo.ColocarMarcaModeloEquipo(Serie) + " CON LA SERIE: " +Serie;
            return Equipo;
        }

        public void RegistrarModuloABodega(RegistroModulo NuevoModulo)
        {
            int respuesta;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "AgregarModuloBodega";    
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@IdModulo", NuevoModulo.IdModulo);
            comando.Parameters.AddWithValue("@Clave", NuevoModulo.ClaveAnterior);

            comando.ExecuteNonQuery();
            conexion.CerrarConexion();
        }

        public void EliminarClaveModuloBodega(string ClaveModulo)
        {
            int respuesta;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "EliminarClaveModuloBodega";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@ClaveModulo", ClaveModulo);

            comando.ExecuteNonQuery();
            conexion.CerrarConexion();
        }

        //Quizas una funcion solo para agregar el modelo que fue retirado en bodega de modulos

        //Metodo que nos ayuda a agregar el modulo que fue retirado al inventario de modulos
        //Y retirar el que se le coloco ahora al equipo
        public void ModificarInventarioModulosBodega(RegistroModulo NuevoModulo)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "AgregarYEliminarModuloBodega";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@IdModulo", NuevoModulo.IdModulo);
            comando.Parameters.AddWithValue("@Clave", NuevoModulo.Clave);
            comando.Parameters.AddWithValue("@ClaveAnterior", NuevoModulo.ClaveAnterior);

            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            conexion.CerrarConexion();
        }

        //Modificar al retirar modulo en serie_x_modulo o dar de alta en serie_x_modulo en caso de que se instale por primera vez
        public void GuardarClaveDeSerie(RegistroModulo NuevoModulo, bool CambioClave)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "GuardarClaveModuloSerie";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@IdModulo", NuevoModulo.IdModulo);
            comando.Parameters.AddWithValue("@Clave", NuevoModulo.Clave);
            comando.Parameters.AddWithValue("@NumeroFolio", NuevoModulo.Reporte);
            comando.Parameters.AddWithValue("@Serie", NuevoModulo.Serie);
            comando.Parameters.AddWithValue("@CambioClave", CambioClave);
            comando.ExecuteNonQuery();
            conexion.CerrarConexion();

            if (!CambioClave)
            {
                //Eliminar la clave que fue seleccionada del inventario de modulos para instalación
                EliminarClaveModuloBodega(NuevoModulo.Clave);
            }

        }

        //Utilizado al eliminar un reporte de servicio, para comprobar que no se elimine el ultimo reporte que tienen los modulos
        public void CambiarUltimoReporteEnSerieModulos(string Serie, string NumeroFolio)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "CambiarUltimoReporteEnSerieModulo";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@Serie", Serie);
            comando.Parameters.AddWithValue("@NumeroReporte", NumeroFolio);

            comando.ExecuteNonQuery();
            conexion.CerrarConexion();
        }

        public DataTable MostrarModulosCliente(string Serie, string Folio)
        {
            DataTable tabla = new DataTable();
            SqlDataReader leer;
            comando.Connection = conexion.AbrirConexion();
            //comando.CommandText = "MostrarModulosCliente";
            comando.CommandText = "spMostrarModulosEquipo";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@Serie", Serie);
            comando.Parameters.AddWithValue("@NumeroFolio", Folio);

            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conexion.CerrarConexion();
            return tabla;
        }

        public int BuscarIdModulo(string campo, int IdModelo)
        {
            SqlDataReader ver;
            int id = 0;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "BuscarIdModulo";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@CampoBusqueda", campo);
            comando.Parameters.AddWithValue("@IdModelo", IdModelo);
            ver = comando.ExecuteReader();

            while (ver.Read())
            {
                id = int.Parse(ver[0].ToString());
            }

            conexion.CerrarConexion();
            ver.Close();
            return id;
        }

        //Nos ayudara a saber si un cliente ya tiene ese modulo con una clave
        //CAMBIAR AQU Y EN BASE DE DATOS
        public bool VerificarModuloAsignado(string Serie, int IdModulo)
        {
            SqlDataReader leer;
            bool FolioRepetido = false;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "VerificarModuloCliente";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@NumeroSerie", Serie);
            comando.Parameters.AddWithValue("@IdModulo", IdModulo);


            leer = comando.ExecuteReader();
            

            //Nos ayuda a comprobar si el inventario fue modificado(Dependiendo si se haya modificado algo o no)
            if (leer.Read())
            {
                leer.Close();
                return FolioRepetido = true;
            }
            else
            {
                leer.Close();
                return FolioRepetido;
            }
        }

        public bool VerificarClaveDuplicada(string ParametroBusqueda)
        {
            SqlDataReader leer;
            bool FolioRepetido = false;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "ValidarClaveModulo";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();

            comando.Parameters.AddWithValue("@ParametroBusqueda", ParametroBusqueda);


            leer = comando.ExecuteReader();

            //Nos ayuda a comprobar si el inventario fue modificado(Dependiendo si se haya modificado algo o no)
            if (leer.Read())
            {
                string Estado = leer[0].ToString();
                if(Estado != "RETIRADO")
                {
                    leer.Close();
                    return FolioRepetido = true;
                }
            }
            leer.Close();
            return FolioRepetido;
        }


    }
}
