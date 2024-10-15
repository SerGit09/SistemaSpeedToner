using CobranzaSP.Formularios;
using CobranzaSP.Lógica;
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
using CobranzaSP.Modelos;
using System.Windows.Forms;

namespace CobranzaSP
{
    public partial class ReporteRegistroPartesRicoh : Form
    {
        FuncionesFormularios FuncionFormulario = new FuncionesFormularios();
        AccionesLógica NuevaAccion = new AccionesLógica();
        LogicaRegistroInventarioRicoh lgRegistroPartes = new LogicaRegistroInventarioRicoh();

        string TipoBusqueda = "";
        bool Validado;
        public ReporteRegistroPartesRicoh()
        {
            InitializeComponent();
            InicioAplicacion();
        }


        #region Inicio
        public void InicioAplicacion()
        {
            LlenarComboBoxTipoBusqueda();
            cboTipoBusqueda.DropDownStyle = ComboBoxStyle.DropDownList;
            radTodasLasPartes.Checked = true;
            FuncionFormulario.LlenarComboBox(cboModelos, "spSeleccionarModelosPartes");
        }

        public void LlenarComboBoxTipoBusqueda()
        {
            string[] Opciones = { "", "Salidas", "Entradas", "Fecha" };
            cboTipoBusqueda.Items.AddRange(Opciones);
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

        private void cboTipoBusqueda_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            TipoBusqueda = cboTipoBusqueda.SelectedItem.ToString();
            if (TipoBusqueda == "")//Comprobar que no este vacio el combobox
            {
                return;
            }
            ReiniciarOpciones();
            btnGenerar.Enabled = true;//Habilitar Boton para generar el reporte

            switch (cboTipoBusqueda.SelectedItem.ToString())
            {
                case "Entradas":
                    MostrarCamposModelosPartes(true);
                    break;
                case "Salidas":
                    MostrarCamposModelosPartes(true);
                    break;
            }
        }
        public void ReiniciarOpciones()
        {
            MostrarGroupBoxPartes(false);
            MostrarCamposModelosPartes(false);
            MostrarSeriesEquipos(false);
            radTodasLasPartes.Checked = true;
            chkElegirSerie.Visible = false;
            btnGenerar.Enabled = false;//Desactivar boton generar
        }

        public void MostrarCamposModelosPartes(bool Mostrar)
        {
            lblModelos.Visible = Mostrar;
            cboModelos.Visible = Mostrar;
        }


        public void MostrarGroupBoxPartes(bool Mostrar)
        {
            grpPartes.Visible = Mostrar;
        }
        private void cboModelos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Modelo = (cboModelos.SelectedIndex != -1) ? cboModelos.SelectedItem.ToString() : " ";
            MostrarGroupBoxPartes(false);//Ocultamos nuevamente las partes
            if (Modelo != " ")
            {
                int IdModelo = NuevaAccion.BuscarId(Modelo, "spObtenerIdModeloPartes");
                FuncionFormulario.LlenarComboBox(cboPartes, "spSeleccionarPartes", IdModelo);
                //Cuando se escoja un cliente o proveedor mostramos el groupbox de Modelos Ricoh
                MostrarGroupBoxPartes(true);
            }
        }

        private void radTodasLasPartes_CheckedChanged(object sender, EventArgs e)
        {
            //Ocultamos los controles del groupbox de partes
            lblParte.Visible = false;
            cboPartes.Visible = false;
        }

        private void radUnaParte_CheckedChanged(object sender, EventArgs e)
        {
            lblParte.Visible = true;
            cboPartes.Visible = true;
        }
        private void cboPartes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Parte = (cboPartes.SelectedIndex != -1) ? cboPartes.SelectedItem.ToString() : " ";
            if (Parte != " ")//Validar que no este vacio
            {
                if (TipoBusqueda == "Salidas")
                {
                    chkElegirSerie.Visible = true;
                }
            }
        }

        public void MostrarSeriesEquipos(bool Mostrar)
        {
            lblSerieEquipo.Visible = Mostrar;
            cboSeriesEquipos.Visible = Mostrar;
        }

        //Cuando se necesite realizar un reporte de una serie en especifico
        private void chkElegirSerie_CheckedChanged(object sender, EventArgs e)
        {
            if (chkElegirSerie.Checked)
            {
                MostrarSeriesEquipos(true);
                FuncionFormulario.LlenarComboBox(cboSeriesEquipos, "spSeleccionarTodasSeriesRicoh");
            }
            else
            {
                MostrarSeriesEquipos(false);
            }
        }
        #endregion

        #region Validaciones
        public bool ValidarCamposVacios()
        {
            Validado = true;
            erReporteParteRicoh.Clear();
            if(TipoBusqueda == "Fecha")
            {
                return Validado;
            }
            ValidarCampo(cboModelos);
            if (radUnaParte.Checked)
            {
                ValidarCampo(cboPartes);
                if (TipoBusqueda == "Salidas")
                {
                    if (chkElegirSerie.Checked)
                    {
                        ValidarCampo(cboSeriesEquipos);
                    }
                }
            }
            return Validado;
        }

        public void ValidarCampo(Control c)
        {
            if (c is TextBox || c is RichTextBox)
            {
                if (string.IsNullOrEmpty(c.Text))
                {
                    erReporteParteRicoh.SetError(c, "Campo Obligatorio");
                    Validado = false;
                }
            }
            else if (c is ComboBox)
            {
                ComboBox combo = (ComboBox)c;
                if (combo.SelectedIndex == -1 || combo.SelectedIndex == 0)
                {
                    erReporteParteRicoh.SetError(c, "Seleccione una opcion");
                    Validado = false;
                }
            }
        }
        #endregion

        #region Botones
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (!ValidarCamposVacios())
            {
                return;
            }

            ReporteRegistroParte NuevoReporte = new ReporteRegistroParte()
            {
                TipoBusqueda = cboTipoBusqueda.Text,
                FechaInicio = dtpFechaInicio.Value,
                FechaFinal = dtpFechaFinal.Value,
                ModeloParteRicoh = cboModelos.SelectedItem.ToString(),
                Serie = "",
                Parte = ""
            };
            LlenarCamposReporteFaltantes(NuevoReporte);
            bool DatosEncontrados = lgRegistroPartes.DeterminarTipoReporte(NuevoReporte);
            if (!DatosEncontrados)
            {
                MessageBox.Show("NO SE ENCONTRARON REGISTROS", "NO SE GENERO EL REPORTE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //MessageBox.Show(NuevoReporte.FechaInicio.ToString() + "\n" +NuevoReporte.FechaFinal + " \n" + NuevoReporte.Serie + "\n" + NuevoReporte.Parte);
        }


        public void LlenarCamposReporteFaltantes(ReporteRegistroParte NuevoReporte)
        {
            if (TipoBusqueda == "Fecha")
            {
                return;
            }

            if (radUnaParte.Checked)
            {
                NuevoReporte.Parte = cboPartes.SelectedItem.ToString();
                if (TipoBusqueda == "Salidas")
                {
                    if (chkElegirSerie.Checked)
                    {
                        NuevoReporte.Serie = cboSeriesEquipos.SelectedItem.ToString();
                    }
                }
            }
        }
        #endregion

    }
}
