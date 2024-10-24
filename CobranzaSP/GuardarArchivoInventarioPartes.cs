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
        public GuardarArchivoInventarioPartes()
        {
            InitializeComponent();
        }

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


            LogicaInventarioPartesRicoh InventarioPartes = new LogicaInventarioPartesRicoh();
            InventarioPartesDatos NuevoInventarioPartes = new InventarioPartesDatos()
            {
                IdInventarioPartes = 0,
                Fecha = dtpFecha.Value,
                UrlArchivo = InventarioPartes.CrearPDF(false, dtpFecha.Value)
            };
            InventarioPartes.GuardarArchivoInventarioPartes(NuevoInventarioPartes);
            MessageBox.Show("ARCHIVO DE INVENTARIO DE PARTES GUARDADO CORRECTAMENTE");
            this.Close();
        }
        #endregion

    }
}
