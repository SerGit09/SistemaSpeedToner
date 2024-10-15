using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Configuration;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net.Mime;
using CobranzaSP.Modelos;


namespace CobranzaSP.Lógica
{
    internal class ContactoLogica
    {
        private CD_Conexion conexion = new CD_Conexion();
        SqlCommand comando = new SqlCommand();
        AccionesLógica NuevaAccion = new AccionesLógica();

        public string AgregarCliente(Contacto NuevoCliente)
        {
            int respuesta = 0;
            string mensaje = "";

            comando.Connection = conexion.AbrirConexion();

            comando.CommandText = "GuardarCliente";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@IdCliente", NuevoCliente.IdCliente);
            comando.Parameters.AddWithValue("@Cliente", NuevoCliente.Nombre);
            comando.Parameters.AddWithValue("@DiasCredito", NuevoCliente.DiasCredito);

            respuesta = comando.ExecuteNonQuery();
            string AccionRealizada = (NuevoCliente.IdCliente > 0) ?"MODIFICADO" :"AGREGADO";
            mensaje = (respuesta > 0) ? "Contacto "+ AccionRealizada + " correctamente" : "Algo ha salido mal, no agrego el registro";

            return mensaje.ToUpper();
        }

        public string AgregarCorreo(Contacto NuevoCorreo)
        {
            int respuesta = 0;
            string mensaje = "";

            comando.Connection = conexion.AbrirConexion();

            comando.CommandText = "GuardarCorreo";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@IdCorreo", NuevoCorreo.IdCorreo);
            comando.Parameters.AddWithValue("@IdCliente", NuevoCorreo.IdCliente);
            comando.Parameters.AddWithValue("@Correo", NuevoCorreo.Correo);

            respuesta = comando.ExecuteNonQuery();
            string AccionRealizada = (NuevoCorreo.IdCorreo > 0) ? "MODIFICADO" : "AGREGADO";
            mensaje = (respuesta > 0) ? "Correo " + AccionRealizada + " correctamente" : "Algo ha salido mal, no se agrego el correo";

            return mensaje.ToUpper();
        }

        public string EliminarContacto(int Id)
        {
            int respuesta = 0;
            string mensaje = "";
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "EliminarContacto";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@Id", Id);

            respuesta = comando.ExecuteNonQuery();
            mensaje = (respuesta > 0) ? "Contacto eliminado correctamente" : "Algo ha salido mal, no se elimino el registro";

            comando.Parameters.Clear();

            conexion.CerrarConexion();
            return mensaje;
        }

        public DataTable MostrarContactos(int IdCliente)
        {
            DataTable tabla = new DataTable();
            SqlDataReader leer;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "MostrarContactos";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@IdCliente", IdCliente);

            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conexion.CerrarConexion();
            return tabla;
        }
    }
}
