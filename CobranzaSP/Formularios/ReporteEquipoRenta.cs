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
    public partial class ReporteEquipoRenta : Form
    {
        public ReporteEquipoRenta()
        {
            InitializeComponent();
            InicioAplicacion();
        }
        FuncionesFormularios AccionFormulario = new FuncionesFormularios();
        AccionesLógica NuevaAccion = new AccionesLógica();
        LogicaEquipos lgEquipo = new LogicaEquipos();

        string Parametro = "";
        string TipoBusqueda = "";

        //Parametros para los reportes
        string Marca = "";
        string Modelo = "";
        bool Validado;


        #region Inicio

        public void InicioAplicacion()
        {
            LlenarCboOpcionesMostrar();
            btnMostrar.Enabled = false;
        }

        public void LlenarCboOpcionesMostrar()
        {
            string[] Opciones = { "", "Precios de Equipos", "Cliente" };
            cboOpcionMostrar.Items.AddRange(Opciones);
        }
        #endregion

        #region Validaciones
        public bool ValidarCamposReporte()
        {
            Validado = true;
            erReporteEquipos.Clear();
            //Validar que se elija una opción
            ValidarCampo(cboOpcionMostrar, "Seleccione un tipo de reporte");
            string TipoBusqueda = cboOpcionMostrar.SelectedItem.ToString();
            if (TipoBusqueda == "Cliente")
            {
                if (radUnCliente.Checked)
                {
                    ValidarCampo(cboClienteElegido, "Seleccione un cliente");
                }
            }

            //Validar al elegir una marca
            if (rdUnaMarca.Checked)
            {
                ValidarCampo(cboMarca, "Seleccione una marca");
            }

            //Validar al elegir un modelo
            if (radUnModelo.Checked)
            {
                ValidarCampo(cboModelo, "Seleccione un modelo");
            }

            return Validado;
        }

        public void ValidarCampo(Control c, string Mensaje)
        {
            if (c is TextBox || c is RichTextBox)
            {
                if (string.IsNullOrEmpty(c.Text))
                {
                    erReporteEquipos.SetError(c, Mensaje);
                    Validado = false;
                }
            }
            else if (c is ComboBox)
            {
                ComboBox combo = (ComboBox)c;
                if (combo.SelectedIndex == -1 || combo.SelectedIndex == 0)
                {
                    erReporteEquipos.SetError(c, Mensaje);
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

        private void ReporteEquipoRenta_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Opciones Reporte
        private void cboOpcionMostrar_SelectedIndexChanged(object sender, EventArgs e)
        {
            TipoBusqueda = cboOpcionMostrar.SelectedItem.ToString();
            ReiniciarOpciones();
            if (TipoBusqueda != "")
            {
                btnMostrar.Enabled = true;
                switch (TipoBusqueda)
                {
                    case "Cliente":
                        MostrarOpcionesClientes(true);
                        break;
                    case "Precios de Equipos":
                        MostrarOpcionesMarca(true);
                        break;
                }
            }
        }

        public void ReiniciarOpciones()
        {
            MostrarOpcionesClientes(false);
            rdTodosLosModelos.Checked = false;
            btnMostrar.Enabled = false;
        }

        public void MostrarOpcionesClientes(bool Habilitar)
        {
            grpClientes.Visible = Habilitar;
            radTodosLosClientes.Checked = Habilitar;
            MostrarOpcionesMarca(Habilitar);
        }

        public void MostrarOpcionesMarca(bool Habilitar)
        {
            grpMarcas.Visible = Habilitar;
            rdTodasLasMarcas.Checked = Habilitar;
        }

        //RADIO BUTTONS CLIENTES
        private void radTodosLosClientes_CheckedChanged(object sender, EventArgs e)
        {
            //Ocultar cb de clientes
            cboClienteElegido.Visible = false;
        }

        private void radUnCliente_CheckedChanged(object sender, EventArgs e)
        {
            cboClienteElegido.Visible = true;
            AccionFormulario.LlenarComboBox(cboClienteElegido, "SeleccionarClientes");
        }

        //RADIO BUTTONS MARCAS
        private void rdTodasLasMarcas_CheckedChanged(object sender, EventArgs e)
        {
            cboMarca.Visible = false;
            AccionFormulario.LlenarComboBox(cboMarca, "SeleccionarMarca");
            grpModelo.Visible = false;
            rdTodosLosModelos.Checked = true;
        }

        private void rdUnaMarca_CheckedChanged(object sender, EventArgs e)
        {
            cboMarca.Visible = true;
            grpModelo.Visible = true;
        }

        //RADIO BUTTONS MODELOS
        private void rdTodosLosModelos_CheckedChanged(object sender, EventArgs e)
        {
            cboModelo.Visible = false;
        }

        private void radUnModelo_CheckedChanged(object sender, EventArgs e)
        {
            cboModelo.Visible = true;
        }

        private void cboMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMarca.SelectedIndex != 0)
            {
                int IdMarca = NuevaAccion.BuscarId(cboMarca.SelectedItem.ToString(), "ObtenerIdMarca");
                AccionFormulario.LlenarComboBox(cboModelo, "SeleccionarModelos", IdMarca);
            }
        }

        #endregion

        #region Botones
        #endregion

        #region Eventos
        #endregion

        #region Metodos Locales
        #endregion

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            bool DatosEncontrados = true;
            string StoredProcedure = "";


            if (!ValidarCamposReporte())
                return;

            switch (TipoBusqueda)
            {
                case "Cliente":
                    StoredProcedure = "ReporteEquiposRenta";
                    break;
                case "Precios de Equipos":
                    StoredProcedure = "ReporteEquiposRentaPrecios";
                    break;
            }

            Reporte NuevoReporte = new Reporte()
            {
                TipoBusqueda = cboOpcionMostrar.SelectedItem.ToString(),
                Cliente = (radUnCliente.Checked) ? cboClienteElegido.SelectedItem.ToString() : ""
            };
            DeterminarParametroBusqueda(NuevoReporte);
            DatosEncontrados = lgEquipo.ObtenerDatosReporteEquipos(NuevoReporte,StoredProcedure);

            if (!DatosEncontrados)
            {
                MessageBox.Show("!!DATOS NO ENCONTRADOS!!", "NO SE PUDO GENERAR EL REPORTE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DeterminarParametroBusqueda(Reporte NuevoReportePrueba)
        {
            NuevoReportePrueba.ParametroBusqueda = "";
            if (rdUnaMarca.Checked)
            {
                NuevoReportePrueba.ParametroBusqueda = cboMarca.SelectedItem.ToString();
                NuevoReportePrueba.TipoBusquedaAdicional = "Marca";
            }

            if (radUnModelo.Checked)
            {
                NuevoReportePrueba.ParametroBusqueda = cboModelo.SelectedItem.ToString();
                NuevoReportePrueba.TipoBusquedaAdicional = "Modelo";
            }
        }
    }
}
