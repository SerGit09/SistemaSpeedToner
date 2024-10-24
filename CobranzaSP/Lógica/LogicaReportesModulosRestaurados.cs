using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CobranzaSP.Lógica;
using CobranzaSP.Modelos;
using System.Data.SqlClient;
using System.Data;

namespace CobranzaSP.Lógica
{
    public class LogicaReportesModulosRestaurados
    {
        private CD_Conexion conexion = new CD_Conexion();
        SqlCommand comando = new SqlCommand();
        public string GuardarReporte(ReporteModuloRestaurado ReporteModulo)
        {
            int respuesta = 0;
            string AccionRealizada = "agrego";
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "GuardarReporteModulosRestaurados";
            comando.CommandType = CommandType.StoredProcedure;

            if (ReporteModulo.IdReporte > 0)
            {
                AccionRealizada = "modifico";
            }
            comando.Parameters.AddWithValue("@IdReporte", ReporteModulo.IdReporte);
            comando.Parameters.AddWithValue("@FolioReporte", ReporteModulo.FolioReporte);
            comando.Parameters.AddWithValue("@Fecha", ReporteModulo.Fecha);
            comando.Parameters.AddWithValue("@IdModulo", ReporteModulo.IdModulo);
            comando.Parameters.AddWithValue("@Clave", ReporteModulo.Clave);
            comando.Parameters.AddWithValue("@ServicioRealizado", ReporteModulo.ServicioRealizado);

            respuesta = comando.ExecuteNonQuery();
            string Mensaje = (respuesta > 0) ? "Reporte se " + AccionRealizada + " correctamente" : "Algo salio mal, no se " + AccionRealizada + " la garantia";

            comando.Parameters.Clear();
            conexion.CerrarConexion();
            return Mensaje;
        }
    }
}
