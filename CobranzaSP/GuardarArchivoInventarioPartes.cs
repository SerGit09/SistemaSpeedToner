using CobranzaSP.Lógica;
using CobranzaSP.Modelos;
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
    public partial class GuardarArchivoInventarioPartes : Form
    {
        public GuardarArchivoInventarioPartes(int idTipoInventario)
        {
            InitializeComponent();
            IdTipoInventario = idTipoInventario;
        }
        int IdTipoInventario;
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

        #region Botones
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro que deseas guardar este inventario de partes?", "CONFIRMAR GUARDAR INVENTARIO PARTES", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                MessageBox.Show("!!EL INVENTARIO NO SE GUARDO!!", "CANCELADO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }


            AccionesLógica NuevaAccion = new AccionesLógica();
            ArchivoInventarioDatos NuevoInventarioPartes = new ArchivoInventarioDatos()
            {
                IdArchivoInventario = 0,
                IdTipoInventario = IdTipoInventario,
                Fecha = dtpFecha.Value
            };
            CrearPdfInventario(NuevoInventarioPartes);
            NuevaAccion.GuardarArchivoInventario(NuevoInventarioPartes);
            MessageBox.Show("ARCHIVO DE INVENTARIO GUARDADO CORRECTAMENTE");
            this.Close();
        }

        public void CrearPdfInventario(ArchivoInventarioDatos ArchivoInventario)
        {
            switch (IdTipoInventario)
            {
                case 2:
                    LogicaInventarioPartesRicoh InventarioPartes = new LogicaInventarioPartesRicoh();
                    ArchivoInventario.UrlArchivo = InventarioPartes.CrearPDF(false, dtpFecha.Value);
                    break;
                case 3:
                    LogicaInventarioModulos InventarioModulos = new LogicaInventarioModulos();
                    ArchivoInventario.UrlArchivo = InventarioModulos.CrearPDF(dtpFecha.Value);
                    ; break;
            }
        }
        #endregion

    }
}
