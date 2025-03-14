using CobranzaSP.Lógica;
using DocumentFormat.OpenXml;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CobranzaSP.Modelos;
using System.Runtime.InteropServices;
using DocumentFormat.OpenXml.Vml.Spreadsheet;

namespace CobranzaSP.Formularios
{
    public partial class ReportesGarantias : Form
    {
        string TipoBusqueda = "";
        FuncionesFormularios Formulario = new FuncionesFormularios();
        AccionesLógica NuevaAccion = new AccionesLógica();
        LogicaGarantia lgGarantia = new LogicaGarantia();
        Reporte NuevoReporteGarantia = new Reporte();
        bool Validado;
        bool ReporteCliente = false;
        public ReportesGarantias()
        {
            InitializeComponent();
            InicioAplicacion();
        }

        #region Inicio

        public void InicioAplicacion()
        {
            //Opciones reporte
            string[] Opciones = { "", "Marca", "Cliente", "Rango Fecha" };
            Formulario.LlenarComboBox(cboMarca, "SeleccionarMarca");
            Formulario.LlenarComboBox(cboClientes, "SeleccionarClientesServicios");
            //Formulario.LlenarComboBox(cboClientes, "SeleccionarClientes");
            cboOpcionReporte.Items.AddRange(Opciones);
            cboOpcionReporte.SelectedIndex = 0;
            radTodasLasMarcas.Checked = true;

        }

        #endregion

        #region Validaciones
        public bool ValidarReporte()
        {
            Validado = true;
            erGarantia.Clear();

            switch (TipoBusqueda)
            {
                case "Marca":ValidarMarcaImpresora(); break;
                case "Cliente":
                    if (rdUnCliente.Checked)
                    {
                        ValidarCampo(cboClientes, "Seleccione un cliente");
                    }
                    ValidarMarcaImpresora();
                    ;break;
                case "Modelo":ValidarModeloImpresora();break;
            }

            return Validado;
        }

        public void ValidarMarcaImpresora()
        {
            if (radUnaMarca.Checked)
            {
                ValidarCampo(cboMarca, "Seleccione una marca");
            }
            ValidarModeloImpresora();
        }

        public void ValidarModeloImpresora()
        {
            if (radUnModelo.Checked)
            {
                ValidarCampo(cboModelos, "Seleccione un modelo");
            }
        }

        public void ValidarCampo(Control c, string Mensaje)
        {
            if (c is TextBox || c is RichTextBox)
            {
                if (string.IsNullOrEmpty(c.Text))
                {
                    erGarantia.SetError(c, Mensaje);
                    Validado = false;
                }
            }
            else if (c is ComboBox)
            {
                ComboBox combo = (ComboBox)c;
                if (combo.SelectedIndex == -1 || combo.SelectedIndex == 0)
                {
                    erGarantia.SetError(c, Mensaje);
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
        private void cboOpcionReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnGenerarReporte.Enabled = true;
            ReiniciarOpciones();
            NuevoReporteGarantia.TipoBusqueda = cboOpcionReporte.SelectedItem.ToString();

            if(NuevoReporteGarantia.TipoBusqueda != "")
            {
                grpMarcas.Visible = true;
                btnGenerarReporte.Enabled = true;
            }

            switch (NuevoReporteGarantia.TipoBusqueda)
            {
                case "Cliente":
                    ReporteCliente = true;
                    grpCliente.Visible = true;break;
                case "Rango Fecha": grpMarcas.Visible = false; break;
            }
        }

        public void ReiniciarOpciones()
        {
            radTodosLosClientes.Checked = true;
            radTodasLasMarcas.Checked = true;
            NuevoReporteGarantia.ParametroBusqueda = "";
            grpCliente.Visible = false;
            cboMarca.Visible = false;
            grpModelos.Visible = false;
            grpMarcas.Visible = false;
            btnGenerarReporte.Enabled = false;
            ReporteCliente = false;
        }

        #region RadioButtons
        private void radTodosLosClientes_CheckedChanged(object sender, EventArgs e)
        {
            MostrarControlesCapturaCliente(false);
            cboClientes.SelectedIndex = 0;
        }

        private void rdUnCliente_CheckedChanged(object sender, EventArgs e)
        {
            MostrarControlesCapturaCliente(true);
            Formulario.LlenarComboBox(cboClientes, "SeleccionarClientesServicios");
        }
        public void MostrarControlesCapturaCliente(bool Mostrar)
        {
            lblCliente.Visible = Mostrar;
            cboClientes.Visible = Mostrar;
        }

        private void radTodosModelos_CheckedChanged(object sender, EventArgs e)
        {
            cboModelos.SelectedIndex = 0;
            MostrarControlesCapturaModelo(false);
            NuevoReporteGarantia.TipoBusqueda = "Marca";
        }

        private void radUnModelo_CheckedChanged(object sender, EventArgs e)
        {
            MostrarControlesCapturaModelo(true);
            NuevoReporteGarantia.TipoBusqueda = "Modelo";
        }

        public void MostrarControlesCapturaModelo(bool Mostrar)
        {
            lblModelos.Visible = Mostrar;
            cboModelos.Visible = Mostrar;
        }

        private void radTodasLasMarcas_CheckedChanged(object sender, EventArgs e)
        {
            cboMarca.SelectedIndex = 0;
            MostrarControlesCapturaMarca(false);

            if (ReporteCliente)
            {
                NuevoReporteGarantia.TipoBusqueda = "Cliente";
            }
            else
            {
                NuevoReporteGarantia.TipoBusqueda = "Marca";
            }
            NuevoReporteGarantia.ParametroBusqueda = "";
        }

        private void radUnaMarca_CheckedChanged(object sender, EventArgs e)
        {
            MostrarControlesCapturaMarca(true);
        }

        public void MostrarControlesCapturaMarca(bool Mostrar)
        {
            lblMarca.Visible = Mostrar;
            cboMarca.Visible = Mostrar;
        }
        #endregion



        private void cboOpcionElegida_SelectedIndexChanged(object sender, EventArgs e)
        {
            grpModelos.Visible = false;
            //En dado caso que se haya seleccionado algo de las marcas y mientras no estemos buscando un registro en especifico
            if (cboMarca.SelectedItem.ToString() != " ")
            {
                grpModelos.Visible = true;
                NuevoReporteGarantia.TipoBusqueda = "Marca";
                int IdMarca = NuevaAccion.BuscarId(cboMarca.SelectedItem.ToString(), "ObtenerIdMarca");
                Formulario.LlenarComboBox(cboModelos, "spSeleccionarCartuchosMarca", IdMarca);
                radTodosModelos.Checked = true;
            }
        }

        #endregion

        #region Botones
        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            bool DatosEncontrados;
            if (!ValidarReporte())
                return;
            try
            {
                NuevoReporteGarantia.FechaInicio = dtpFechaInicial.Value;
                NuevoReporteGarantia.FechaFinal = dtpFechaFinal.Value;

                DeterminarParametroBusqueda();
                //MessageBox.Show("TIPO BUSQUEDA:" + NuevoReporteGarantia.TipoBusqueda + "\n Cliente:" + NuevoReporteGarantia.Cliente
                //    + "\n Parametro Busqueda:" + NuevoReporteGarantia.ParametroBusqueda);
                DatosEncontrados = lgGarantia.ObtenerDatosReporteGarantia(NuevoReporteGarantia);

                if (!DatosEncontrados)
                {
                    MessageBox.Show("NO EXISTEN REGISTROS", "DATOS NO ENCONTRADOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error: " + ex.Message);
            }
        }

        public void DeterminarParametroBusqueda()
        {
            if (!ReporteCliente)
            {
                NuevoReporteGarantia.Cliente = "";
            }
            
            switch (NuevoReporteGarantia.TipoBusqueda)
            {
                case "Marca":
                    CapturarMarcaImpresora();
                    break;
                case "Modelo":
                    NuevoReporteGarantia.ParametroBusqueda = cboModelos.SelectedItem.ToString();
                    break;
            }
        }

        public void CapturarMarcaImpresora()
        {
            NuevoReporteGarantia.ParametroBusqueda = "";
            if (radUnaMarca.Checked)
            {
                NuevoReporteGarantia.ParametroBusqueda = cboMarca.SelectedItem.ToString();
            }

            if (radUnModelo.Checked)
            {
                NuevoReporteGarantia.ParametroBusqueda = cboModelos.SelectedItem.ToString();
            }
        }

        #endregion

        private void cboClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            NuevoReporteGarantia.Cliente = "";
            if (cboClientes.SelectedItem.ToString() != " ")
            {
                NuevoReporteGarantia.Cliente = cboClientes.SelectedItem.ToString();
            }
        }
    }
}
