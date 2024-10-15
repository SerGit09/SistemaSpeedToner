using CobranzaSP.Lógica;
using CobranzaSP.Modelos;
using DocumentFormat.OpenXml.Office2010.Excel;
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
    public partial class frDatosFacturaPagada : Form
    {
        CuentaPagada CuentaActual = new CuentaPagada();
        CobranzaLógica nuevoCobro = new CobranzaLógica();
        ICobranza frCobranza = new ICobranza();
        bool Validado;
        double CantidadRegistrada = 0;
        public frDatosFacturaPagada(ICobranza frCobranza,CuentaPagada nuevaCuentaPagada)
        {
            InitializeComponent();
            this.frCobranza = frCobranza;
            double ImporteRestante = nuevaCuentaPagada.Cantidad - nuevaCuentaPagada.Saldo;
            txtImporte.Text = nuevaCuentaPagada.Saldo.ToString();
            CantidadRegistrada = nuevaCuentaPagada.Saldo;

            //Asignamos para tener los datos de la factura
            CuentaActual = nuevaCuentaPagada;
        }


        #region PanelSuperior
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lparam);

        private void panel1_MouseDown(object sender, MouseEventArgs e)
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

        public bool ValidarDatos()
        {
            Validado = true;
            ValidarCampo(txtImporte,"Colocar monto a pagar");

            return Validado;
        }

        public void ValidarCampo(Control c, string Mensaje)
        {
            if (c is TextBox || c is RichTextBox)
            {
                if (string.IsNullOrEmpty(c.Text))
                {
                    erCuentaPagada.SetError(c, Mensaje);
                    Validado = false;
                }
            }
            else if (c is ComboBox)
            {
                ComboBox combo = (ComboBox)c;
                if (combo.SelectedIndex == -1 || combo.SelectedIndex == 0)
                {
                    erCuentaPagada.SetError(c, Mensaje);
                    Validado = false;
                }
            }
        }

        #endregion

        private void btnCobrado_Click(object sender, EventArgs e)
        {
            if (!ValidarDatos())
            {
                return;
            }

            if (MessageBox.Show("¿Esta seguro de realizar el importe?", "CONFIRME EL PAGO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                MessageBox.Show("!!CUENTA NO PAGADA!!", "OPERACION CANCELADA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }

            CuentaActual.Cantidad = double.Parse(txtImporte.Text);
            CuentaActual.FechaPago = dtpFechaPago.Value;

            //Antes se debe de definir la cantidad que quede restante
            CantidadRegistrada -= CuentaActual.Cantidad;

            CuentaActual.CuentaSaldada = (CantidadRegistrada == 0) ? true : false;

            MessageBox.Show(nuevoCobro.CuentaPagada(CuentaActual), "AGREGANDO NUEVO COBRO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            //Actualizar en interfaz de cobranza
            frCobranza.MostrarDatosCobranza();
            frCobranza.ActualizarTotalCobranza();
            frCobranza.LimpiarForm();

            this.Close();
        }

    }
}
