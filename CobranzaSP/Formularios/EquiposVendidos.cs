using CobranzaSP.Lógica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CobranzaSP.Formularios
{
    public partial class EquiposVendidos : Form
    {
        public EquiposVendidos()
        {
            InitializeComponent();
            InicioAplicacion();
        }
        AccionesLógica NuevaAccion = new AccionesLógica();
        FuncionesFormularios Formulario = new FuncionesFormularios();

        #region Inicio
        public void InicioAplicacion()
        {
            MostrarDatosEquipos();
            Formulario.PropiedadesDtg(dtgEquiposVendidos);
        }

        public void MostrarDatosEquipos()
        {
            //Limpiamos los datos del datagridview
            dtgEquiposVendidos.DataSource = null;
            dtgEquiposVendidos.Refresh();
            DataTable tabla = new DataTable();
            //Guardamos los registros dependiendo la consulta
            tabla = NuevaAccion.Mostrar("MostrarEquiposVendidos");
            //Asignamos los registros que optuvimos al datagridview
            dtgEquiposVendidos.DataSource = tabla;
            dtgEquiposVendidos.Columns["IdEquipo"].Visible = false;
        }
        #endregion
    }
}
