using CobranzaSP.Formularios;
using CobranzaSP.Lógica;
using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CobranzaSP
{
    public partial class InventariosPartesGuardados : Form
    {
        public InventariosPartesGuardados()
        {
            InitializeComponent();
            Inicio();
        }
        AccionesLógica NuevaAccion = new AccionesLógica();
        FuncionesFormularios Formulario = new FuncionesFormularios();
        string UrlArchivo = "";
        int IdArchivoInventarioPartes = 0;

        #region PanelSuperior
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lparam);
        private void pSuperior_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region InicioAplicacion
            
        public void Inicio()
        {
            Formulario.PropiedadesDtg(dtgInventariosPartes);
            Mostrar("MostrarInventariosGuardadosPartes");

            btnAbrirArchivo.Enabled = false;
            btnEliminar.Enabled = false;
        }

        public void Mostrar(string sp)
        {
            //Limpiamos los datos del datagridview
            dtgInventariosPartes.DataSource = null;
            dtgInventariosPartes.Refresh();
            DataTable tabla = new DataTable();
            //Guardamos los registros dependiendo la consulta
            tabla = NuevaAccion.Mostrar(sp);
            //Asignamos los registros que optuvimos al datagridview
            dtgInventariosPartes.DataSource = tabla;
            dtgInventariosPartes.Columns["UrlArchivo"].Visible = false;
            dtgInventariosPartes.Columns["IdInventarioPartes"].Visible = false;
        }
        #endregion

        #region Botones
        private void btnAbrirArchivo_Click(object sender, EventArgs e)
        {
            if (UrlArchivo != "")
            {
                var pe = new Pdf();

                //Abrimos el pdf
                pe.AbrirPdf(UrlArchivo);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro de eliminar el inventario guardado?", "CONFIRME LA ELIMINACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                MessageBox.Show("!!Eliminacion cancelada!!", "CANCELADO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                IdArchivoInventarioPartes = 0;
                return;
            }


            string Mensaje = NuevaAccion.Eliminar(IdArchivoInventarioPartes, "EliminarArchivoInventarioPartes");
            Mostrar("MostrarInventariosGuardadosPartes");
            MessageBox.Show(Mensaje, "ELIMINACION DE REGISTRO", MessageBoxButtons.OK, MessageBoxIcon.Information);

            IdArchivoInventarioPartes = 0;
            btnEliminar.Enabled = false;
            btnAbrirArchivo.Enabled = false;
        }
        #endregion

        private void dtgInventariosPartes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAbrirArchivo.Enabled = true;
            btnEliminar.Enabled = true;

            IdArchivoInventarioPartes = int.Parse(dtgInventariosPartes.CurrentRow.Cells[0].Value.ToString());
            UrlArchivo = dtgInventariosPartes.CurrentRow.Cells[2].Value.ToString();
        }
    }
}
