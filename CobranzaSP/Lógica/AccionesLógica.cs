﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Office2010.Excel;
using CobranzaSP.Modelos;

namespace CobranzaSP.Lógica
{
    //Clase que contiene metodos que varias interfaces pueden utilizar
    internal class AccionesLógica
    {
        CD_Conexion conexion = new CD_Conexion();
        SqlCommand comando = new SqlCommand();

        //Metodo que llena cada uno de los datagridView del programa, dependiendo el stored procedure que se mande
        public DataTable Mostrar(string sp)
        {
            DataTable tabla = new DataTable();
            SqlDataReader leer;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = sp;
            comando.Parameters.Clear();
            comando.CommandType = CommandType.StoredProcedure;
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conexion.CerrarConexion();
            return tabla;
        }

        public string Eliminar(int Id, string sp)
        {
            int respuesta = 0;
            string mensaje = "";
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = sp;
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@Id", Id);

            respuesta = comando.ExecuteNonQuery();
            mensaje = (respuesta > 0) ? "Registro eliminado correctamente" : "Algo ha salido mal, no se elimino el registro";


            conexion.CerrarConexion();
            return mensaje;
        }

        public int BuscarId(string campo, string sp)
        {
            SqlDataReader ver;
            int id = 0;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = sp;
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@CampoBusqueda", campo);
            ver = comando.ExecuteReader();

            while (ver.Read())
            {
                id = int.Parse(ver[0].ToString());
            }

            conexion.CerrarConexion();
            ver.Close();
            return id;
        }

        public int BuscarIdEntidad(int IdMovimiento, string Entidad,string sp)
        {
            SqlDataReader ver;
            int id = 0;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = sp;
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@IdMovimiento", IdMovimiento);
            comando.Parameters.AddWithValue("@Entidad", Entidad);
            ver = comando.ExecuteReader();

            while (ver.Read())
            {
                id = int.Parse(ver[0].ToString());
            }

            conexion.CerrarConexion();
            ver.Close();
            return id;
        }


        public SqlDataReader LlenarComboBox(string sp)
        {
            SqlDataReader leer;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = sp;
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            leer = comando.ExecuteReader();
            return leer;
        }

        public SqlDataReader LlenarComboBoxEspecifico(string sp, int Id)
        {
            SqlDataReader leer;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = sp;
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@Id", Id);
            leer = comando.ExecuteReader();
            return leer;
        }

        ////Metodo que hara una consulta a la base de datos en base a la marca que se haya seleccionado, seleccionando solamente los modelos de dicha marca
        public SqlDataReader LlenarComboBoxModelos(string sp, int IdMarca)
        {
            SqlDataReader leer;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = sp;
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@IdMarca", IdMarca);
            leer = comando.ExecuteReader();
            comando.Parameters.Clear();
            return leer;
        }

        public bool VerificarDuplicados(string ParametroBusqueda, string sp)
        {
            SqlDataReader leer;
            bool FolioRepetido = false;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = sp;
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@ParametroBusqueda", ParametroBusqueda);
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


        public bool VerificarDuplicados(int ParametroBusqueda, string sp)
        {
            SqlDataReader leer;
            bool FolioRepetido = false;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = sp;
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@ParametroBusqueda", ParametroBusqueda);


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

        public void GuardarArchivoInventario(ArchivoInventarioDatos NuevoInventarioPartes)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "GuardarArchivoInventario";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@IdArchivoInventario", NuevoInventarioPartes.IdArchivoInventario);
            comando.Parameters.AddWithValue("@IdTipoInventario", NuevoInventarioPartes.IdTipoInventario);
            comando.Parameters.AddWithValue("@Fecha", NuevoInventarioPartes.Fecha);
            comando.Parameters.AddWithValue("@UrlArchivo", NuevoInventarioPartes.UrlArchivo);
            comando.ExecuteNonQuery();

            comando.Parameters.Clear();
            conexion.CerrarConexion();
        }

        public DataTable MostrarArchivosInventario(string sp, int IdTipoInventario)
        {
            DataTable tabla = new DataTable();
            SqlDataReader leer;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = sp;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@IdTipoInventario", IdTipoInventario);
            comando.CommandType = CommandType.StoredProcedure;
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conexion.CerrarConexion();
            return tabla;
        }


        public bool VerificarExistenciaRegistro(string ParametroBusqueda, string sp)
        {
            SqlDataReader leer;
            bool Encontrado = false;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = sp;
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@ParametroBusqueda", ParametroBusqueda);
            leer = comando.ExecuteReader();
            comando.Parameters.Clear();

            Encontrado = (leer.Read()) ? true : false;
            leer.Close();
            return Encontrado;
        }

        public SqlDataReader Buscar(string Serie, string sp)
        {
            SqlDataReader leer;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = sp;
            comando.Parameters.Clear();
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@ParametroBusqueda", Serie);
            leer = comando.ExecuteReader();
            return leer;
        }

        public DataTable BuscarPrueba(string Serie, string sp)
        {
            SqlDataReader leer;
            DataTable tblRegistro = new DataTable();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = sp;
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@ParametroBusqueda", Serie);
            tblRegistro.Load(comando.ExecuteReader());
            comando.Dispose();
            conexion.CerrarConexion();
            return tblRegistro;
        }
    }
}
