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
    public partial class ReporteRemisiones : Form
    {
        public ReporteRemisiones()
        {
            InitializeComponent();
            InicioAplicacion();
        }
        bool Validado = true;
        FuncionesFormularios Formulario = new FuncionesFormularios();
        RemisionLogica lgRemision = new RemisionLogica();

        #region Inicio
        public void InicioAplicacion()
        {
            string[] Opciones = { "", "SALDOS POR CLIENTE", "RANGO DE FECHA"};
            cboOpcionReporte.Items.AddRange(Opciones);
        }
        #endregion

        #region Validaciones
        public bool ValidarReporte()
        {
            Validado = true;
            erRemision.Clear();

            ValidarCampo(cboOpcionReporte, "Escoga una opcion");

            string TipoBusqueda = cboOpcionReporte.SelectedItem.ToString();
            switch (TipoBusqueda)
            {
                case "SALDOS POR CLIENTE":
                    if (rdUnCliente.Checked)
                    {
                        ValidarCampo(cboClientes, "Seleccione un cliente");
                    }
                    break;
            }

            return Validado;
        }

        public void ValidarCampo(Control c, string Mensaje)
        {
            if (c is TextBox || c is RichTextBox)
            {
                if (string.IsNullOrEmpty(c.Text))
                {
                    erRemision.SetError(c, Mensaje);
                    Validado = false;
                }
            }
            else if (c is ComboBox)
            {
                ComboBox combo = (ComboBox)c;
                if (combo.SelectedIndex == -1 || combo.SelectedIndex == 0)
                {
                    erRemision.SetError(c, Mensaje);
                    Validado = false;
                }
            }
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
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #endregion

        #region OpcionesReporte
        //ComboBox principal de opciones de generar reporte
        private void cboOpcionReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            string TipoBusqueda = cboOpcionReporte.SelectedItem.ToString();
            ReiniciarOpciones();
            switch (TipoBusqueda)
            {
                case "SALDOS POR CLIENTE":
                    grpCliente.Visible = true;
                    OcultarFechas(false);
                    radTodosLosClientes.Checked = true;
                    break;
                default:OcultarFechas(true);break;
            }
        }

        public void OcultarFechas(bool Mostrar)
        {
            lblFechaInicio.Visible = Mostrar;
            lblFechaFinal.Visible = Mostrar;
            dtpFechaInicial.Visible = Mostrar;
            dtpFechaFinal.Visible = Mostrar;
        }

        public void ReiniciarOpciones()
        {
            grpCliente.Visible = false;
            radTodosLosClientes.Checked = false;
        }

        #endregion

        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            if (!ValidarReporte())
            {
                return;
            }

            Reporte NuevoReporte = new Reporte()
            {
                TipoBusqueda = cboOpcionReporte.SelectedItem.ToString(),
                FechaInicio = dtpFechaInicial.Value,
                FechaFinal = dtpFechaFinal.Value
            };
            CapturarCliente(NuevoReporte);
            lgRemision.DeterminarTipoReporte(NuevoReporte);
            Reiniciar();

            //MessageBox.Show(NuevoReporte.TipoBusqueda + "\n" + NuevoReporte.FechaInicio + "\n" + NuevoReporte.FechaFinal + "\n" + NuevoReporte.Cliente);
        }

        public void CapturarCliente(Reporte NuevoReporte)
        {
            switch (NuevoReporte.TipoBusqueda)
            {
                case "SALDOS POR CLIENTE": NuevoReporte.Cliente = (radTodosLosClientes.Checked) ?"":cboClientes.SelectedItem.ToString() ; break;
                case "RANGO DE FECHA": NuevoReporte.Cliente = ""; break;
            }
        }

        public void Reiniciar()
        {
            ReiniciarOpciones();
            cboOpcionReporte.SelectedIndex = 0;
            dtpFechaFinal.Value = DateTime.Now;
            dtpFechaInicial.Value = DateTime.Now;
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
    }
}
