using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using CobranzaSP.Lógica;
using CobranzaSP.Modelos;

namespace CobranzaSP.Formularios
{
    public partial class ReportesServicioModulos : Form
    {
        public ReportesServicioModulos()
        {
            InitializeComponent();
            InicioAplicacion();
        }
        FuncionesFormularios Formulario = new FuncionesFormularios();
        LogicaModulosCliente lgModuloCliente = new LogicaModulosCliente();
        AccionesLógica NuevaAccion = new AccionesLógica();
        bool Validado;
        int IdModeloImpresora = 0;

        #region InicioAplicacion
        public void InicioAplicacion()
        {
            Formulario.LlenarComboBox(cboModeloImpresora, "spSeleccionarModelosModulos");
            string[] Opciones =
                { "", "RANGO DE FECHAS", "MODULO"
                };
            cboOpcionesReportes.Items.AddRange(Opciones);
            cboOpcionesReportes.SelectedIndex = 0;
        }


        #endregion

        #region OpcionesReportes
        private void cboOpcionesReportes_SelectedIndexChanged(object sender, EventArgs e)
        {
            grpModulos.Visible = false;
            int Opcion = cboOpcionesReportes.SelectedIndex;
            if (Opcion != 0)
            {
                switch (Opcion)
                {
                    case 1:grpModulos.Visible = true;break;
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

        #region Validaciones
        public bool ValidarCampos()
        {
            Validado = true;
            erReporteServiciosModulos.Clear();
            ValidarCampo(cboOpcionesReportes, "Seleccione una opción de reporte");

            if(cboOpcionesReportes.SelectedIndex == 1)
            {
                ValidarCampo(cboModulos, "Seleccione un modulo");
                ValidarCampo(cboClaves, "Seleccione una clave de modulo");
                ValidarCampo(cboModeloImpresora, "Seleccione un modelo de impresora");
            }

            return Validado;
        }

        public void ValidarCampo(Control c, string Mensaje)
        {
            if (c is TextBox || c is RichTextBox)
            {
                if (string.IsNullOrEmpty(c.Text))
                {
                    erReporteServiciosModulos.SetError(c, Mensaje);
                    Validado = false;
                }
            }
            else if (c is ComboBox)
            {
                ComboBox combo = (ComboBox)c;
                if (combo.SelectedIndex == -1 || combo.SelectedIndex == 0)
                {
                    erReporteServiciosModulos.SetError(c, Mensaje);
                    Validado = false;
                }
            }
        }
        #endregion

        private void cboModeloImpresora_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboModeloImpresora.SelectedItem.ToString() != " ")
            {
                IdModeloImpresora = NuevaAccion.BuscarId(cboModeloImpresora.SelectedItem.ToString(), "spObtenerIdModeloModulo");
                Formulario.LlenarComboBox(cboModulos, "spSeleccionarModulos", IdModeloImpresora);
            }
        }

        private void cboModulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboModulos.SelectedIndex != 0)
            {
                int IdModulo = lgModuloCliente.BuscarIdModulo(cboModulos.SelectedItem.ToString(), IdModeloImpresora);
                //Formulario.LlenarComboBox(cboClaves, "SeleccionarClavesBodega", IdModulo);
                Formulario.LlenarComboBox(cboClaves, "sp_SeleccionarClavesModulos", IdModulo);
            }
        }

        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            bool DatosEncontrados;
            if (!ValidarCampos())
            {
                return;
            }

            Reporte NuevoReporte = new Reporte()
            {
                TipoBusqueda = cboOpcionesReportes.SelectedItem.ToString(),
                FechaInicio = dtpFechaInicial.Value,
                FechaFinal = dtpFechaFinal.Value
            };
            DeterminarParametroBusqueda(NuevoReporte);
            MessageBox.Show(NuevoReporte.ParametroBusqueda + "\n" + NuevoReporte.FechaInicio + "\n" + NuevoReporte.FechaFinal);
            LogicaReportesModulosRestaurados lgReporteModulos = new LogicaReportesModulosRestaurados();
            DatosEncontrados = lgReporteModulos.ObtenerDatosReporte(NuevoReporte);

            if (!DatosEncontrados)
            {
                MessageBox.Show("NO SE ENCONTRARON REPORTES DEL MODULO ELEGIDO", "REPORTE SERVICIOS DE MODULOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DeterminarParametroBusqueda(Reporte NuevoReporte)
        {
            int Opcion =  cboOpcionesReportes.SelectedIndex;
            switch (Opcion)
            {
                case 1: NuevoReporte.ParametroBusqueda = cboClaves.SelectedItem.ToString(); break;
                case 2: NuevoReporte.ParametroBusqueda = "";break;
            }
        }
    }
}
