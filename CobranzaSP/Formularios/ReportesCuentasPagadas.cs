using CobranzaSP.Lógica;
using CobranzaSP.Modelos;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
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

namespace CobranzaSP.Formularios
{
    public partial class ReportesCuentasPagadas : Form
    {
        public ReportesCuentasPagadas()
        {
            InitializeComponent();
            radTodosLosClientes.Checked = true;
        }

        FuncionesFormularios Formulario = new FuncionesFormularios();
        bool Validado;
        CuentasPagadasLogica lgCuentaPagada = new CuentasPagadasLogica();

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

        #region Validaciones
        public bool ValidarCamposReporte()
        {
            Validado = true;

            if (rdUnCliente.Checked)
            {
                ValidarCampo(cboClientes, "Seleccione un cliente");
            }

            return Validado;
        }

        public void ValidarCampo(Control c, string Mensaje)
        {
            if (c is TextBox || c is RichTextBox)
            {
                if (string.IsNullOrEmpty(c.Text))
                {
                    erReporteCuentasPagadas.SetError(c, Mensaje);
                    Validado = false;
                }
            }
            else if (c is ComboBox)
            {
                ComboBox combo = (ComboBox)c;
                if (combo.SelectedIndex == -1 || combo.SelectedIndex == 0)
                {
                    erReporteCuentasPagadas.SetError(c, Mensaje);
                    Validado = false;
                }
            }
        }
        #endregion

        #region OpcionesReportes
        private void radTodosLosClientes_CheckedChanged(object sender, EventArgs e)
        {
            cboClientes.Visible = false;
        }

        private void rdUnCliente_CheckedChanged(object sender, EventArgs e)
        {
            cboClientes.Visible = true;
            Formulario.LlenarComboBox(cboClientes, "SeleccionarClientes");
        }
        #endregion

        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            if (!ValidarCamposReporte())
            {
                return;
            }

            ReporteFactura NuevoReporte = new ReporteFactura()
            {
                Cliente = (rdUnCliente.Checked) ? cboClientes.SelectedItem.ToString() : "",
                FechaInicio = dtpFechaInicial.Value,
                FechaFinal = dtpFechaFinal.Value
            };

            bool DatosEncontrados = lgCuentaPagada.ObtenerDatosReporteCuentasPagadas(NuevoReporte);

            if (!DatosEncontrados)
            {
                MessageBox.Show("OCURRIO UN PROBLEMA", "DATOS NO ENCONTRADOS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
