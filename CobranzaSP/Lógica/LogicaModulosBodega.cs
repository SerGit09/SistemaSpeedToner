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
using CobranzaSP.Formularios;
using System.IO;

namespace CobranzaSP.Lógica
{
    internal class LogicaModulosBodega
    {
        private CD_Conexion conexion = new CD_Conexion();
        SqlCommand comando = new SqlCommand();

        public string GuardarModulo(ModuloBodega NuevoModulo)
        {
            string AccionRealizada = "agrego";
            int respuesta;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "spGuardarModuloBodega";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            if (NuevoModulo.Id > 0)
            {
                AccionRealizada = "modifico";
            }
                
            comando.Parameters.AddWithValue("@Id", NuevoModulo.Id);
            comando.Parameters.AddWithValue("@IdModulo", NuevoModulo.IdModulo);
            comando.Parameters.AddWithValue("@Clave", NuevoModulo.Clave);

            respuesta = comando.ExecuteNonQuery();
            string Mensaje = (respuesta > 0) ? "Se " + AccionRealizada + " correctamente el catalogo" : "Algo salio mal, no se " + AccionRealizada + " el registro";

            conexion.CerrarConexion();
            return Mensaje;
        }

        public void GuardarNuevoModulo(ModuloBodega NuevoModulo)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "AgregarModuloACatalogo";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();

            comando.Parameters.AddWithValue("@IdModelo", NuevoModulo.IdModelo);
            comando.Parameters.AddWithValue("@Modulo", NuevoModulo.Modulo);
            comando.ExecuteNonQuery();

            conexion.CerrarConexion();
        }

        public int BuscarIdModulo(string campo,int IdModelo)
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
    }
}
