using CobranzaSP.Lógica;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using CobranzaSP.Modelos;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CobranzaSP.Formularios
{
    public partial class ReporteFacturas : Form
    {
        public ReporteFacturas()
        {
            InitializeComponent();
            InicioAplicacion();
        }
        FuncionesFormularios Formulario = new FuncionesFormularios();
        LogicaReporteFacturas lgReporteFactura = new LogicaReporteFacturas();
        bool Validado;

        #region Inicio
        public void InicioAplicacion()
        {
            string[] Opciones = { "","FACTURAS VENCIDAS", "FACTURAS NO VENCIDAS","FACTURAS CON PROMESA DE PAGO VENCIDAS", "SALDOS POR CLIENTE", "TIPOS FACTURAS" };
            cboOpcionReporte.Items.AddRange(Opciones);
            cboOpcionReporte.SelectedIndex = 0;

            radTodosLosClientes.Checked = true;
        }
        #endregion

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
        private void btnCerrar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }


        #endregion

        #region Validaciones
        public bool ValidarCamposReporte()
        {
            Validado = true;
            erReporteFactura.Clear();

            ValidarCampo(cboOpcionReporte,"Elija una opción");

            if (rdUnCliente.Checked)
            {
                ValidarCampo(cboClientes, "Elija un cliente");
            }
            return Validado;
        }

        public void ValidarCampo(Control c,string Mensaje)
        {
            if (c is TextBox || c is RichTextBox)
            {
                if (string.IsNullOrEmpty(c.Text))
                {
                    erReporteFactura.SetError(c, Mensaje);
                    Validado = false;
                }
            }
            else if (c is ComboBox)
            {
                ComboBox combo = (ComboBox)c;
                if (combo.SelectedIndex == -1 || combo.SelectedIndex == 0)
                {
                    erReporteFactura.SetError(c, Mensaje);
                    Validado = false;
                }
            }
        }
        #endregion

        #region OpcionesReporte
        private void cboOpcionReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            grpCliente.Visible = false;
            radTodosLosClientes.Checked = true;
            MostrarCapturaFechas(false);
            string OpcionElegida = cboOpcionReporte.SelectedItem.ToString();
            if (OpcionElegida != "")
            {
                grpCliente.Visible = true;
                int IndiceOpcion = cboOpcionReporte.SelectedIndex;
                if(IndiceOpcion == 5)
                {
                    MostrarCapturaFechas(true);
                }
            }
        }

        public void MostrarCapturaFechas(bool Mostrar)
        {
            lblFechaInicio.Visible = Mostrar;
            lblFechaFinal.Visible = Mostrar;
            dtpFechaFinal.Visible = Mostrar;
            dtpFechaInicial.Visible = Mostrar;
        }

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

            Reporte NuevoReporte = new Reporte()
            {
                TipoBusqueda = cboOpcionReporte.SelectedItem.ToString(),
                Cliente = (rdUnCliente.Checked) ? cboClientes.SelectedItem.ToString() : ""
            };
            DeterminarParametroBusqueda(NuevoReporte);
            int IndiceOpcion = cboOpcionReporte.SelectedIndex;

            //MessageBox.Show(NuevoReporte.FechaFinal + " " + NuevoReporte.FechaInicio + " " + NuevoReporte.Cliente);

            switch (IndiceOpcion)
            {
                case 1: lgReporteFactura.ObtenerDatosReporteFacturasPromesaPago(NuevoReporte);  break;
                case 5: lgReporteFactura.ObtenerDatosReporteTiposFacturas(NuevoReporte);  break;
                default: lgReporteFactura.ObtenerDatosReporteFacturas(NuevoReporte); break;
            }
            

            //MessageBox.Show(NuevoReporte.TipoBusqueda + "\n" + NuevoReporte.ParametroBusqueda + "\n" + NuevoReporte.Cliente);
        }

        public void DeterminarParametroBusqueda(Reporte NuevoReporte)
        {
            int IndiceOpcion = cboOpcionReporte.SelectedIndex;

            switch (IndiceOpcion)
            {
                case 1: NuevoReporte.ParametroBusqueda = "SI";break;
                case 2:NuevoReporte.ParametroBusqueda = "Disponible";break;
                case 3: NuevoReporte.ParametroBusqueda = "Vencida";break;
                case 5:
                    NuevoReporte.FechaInicio = dtpFechaInicial.Value;
                    NuevoReporte.FechaFinal = dtpFechaFinal.Value;
                    break;
                default: NuevoReporte.ParametroBusqueda = "";break;
            }
        }
    }
}
