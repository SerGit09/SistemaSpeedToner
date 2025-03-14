using CobranzaSP.Lógica;
using CobranzaSP.Modelos;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CobranzaSP.Formularios
{
    public partial class ReporteServiciosRicoh : Form
    {
        public ReporteServiciosRicoh()
        {
            InitializeComponent();
            InicioAplicacion();
        }
        FuncionesFormularios Formulario = new FuncionesFormularios();
        LogicaServiciosRicoh lgServicioRicoh =  new LogicaServiciosRicoh();
        AccionesLógica NuevaAccion = new AccionesLógica();
        LogicaReportesModulo lgReporteModulo = new LogicaReportesModulo();

        //Variables para reporte
        string TipoBusqueda = "";
        bool Validado = true;
        bool SeleccionClienteHabilitada = true;

        #region Inicio
        public void InicioAplicacion()
        {
            OpcionesReportes();
            MostrarOpcionesModulo(false);
            btnGenerarReporte.Enabled = false;
            Formulario.LlenarComboBox(cboModelosModulos, "spSeleccionarModelosModulos");
        }

        public void OpcionesReportes()
        {
            string[] Opciones = 
                { "", "HISTORIAL MODULO","MODULOS POR SERIE DE EQUIPO","MODULOS","UBICACION MODULO","CLIENTE", "SERIE","RANGO DE FECHA", "MANTENIMIENTOS"
                };
            cboOpcionReporte.Items.AddRange(Opciones);
            cboOpcionReporte.SelectedIndex = 0;

            //Opciones para cboOrdenRangoFecha
            
        }

        #endregion

        #region Validaciones

        public bool ValidarOpcionesReporte()
        {
            erReporte.Clear();
            Validado = true;

            switch (TipoBusqueda)
            {
                case "RANGO DE FECHA":Validado = true; break;
                case "HISTORIAL MODULO":ValidarCampo(txtClaveModulo, "Escriba una clave de modulo"); break;
                case "UBICACION MODULO":ValidarCampo(txtClaveModulo, "Escriba una clave de modulo"); break;
                case "CLIENTE":
                    if (radTodosLosClientes.Checked)
                    {
                        Validado = true;
                    }
                    else 
                    {
                        ValidarCampo(cboClientes, "Seleccione un cliente");
                    }
                    ;break;
                case "MANTENIMIENTOS":
                    if (radTodosLosClientes.Checked)
                    {
                        Validado = true;
                    }
                    else
                    {
                        ValidarCampo(cboClientes, "Seleccione un cliente");
                    }
                    ; break;
                case "SERIE": ValidarCampo(cboOpcionElegida, "Seleccione una serie");break;
                case "MODULOS POR SERIE DE EQUIPO": ValidarCampo(cboOpcionElegida, "Seleccione una serie"); break;
            }

            return Validado;
        }

        public void ValidarCampo(Control c, string Mensaje)
        {
            if (c is TextBox || c is RichTextBox)
            {
                if (string.IsNullOrEmpty(c.Text))
                {
                    erReporte.SetError(c, Mensaje);
                    Validado = false;
                }
            }
            else if (c is ComboBox)
            {
                ComboBox combo = (ComboBox)c;
                if (combo.SelectedIndex == -1 || combo.SelectedIndex == 0)
                {
                    erReporte.SetError(c, Mensaje);
                    Validado = false;
                }
            }
        }

        public void ValidarClave()
        {
            if (string.IsNullOrWhiteSpace(txtClaveModulo.Text))
            {
                erReporte.SetError(txtClaveModulo, "Escriba una clave de modulo");
                Validado = false;
            }
            else
            {
                Validado = true;
            }
        }
        #endregion

        #region OpcionesReporte
        private void cboOpcionReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            TipoBusqueda = cboOpcionReporte.SelectedItem.ToString();
            ReiniciarOpciones();

            if (TipoBusqueda != "")
            {
                chkRango.Visible = true;
                btnGenerarReporte.Enabled = true;
                switch (TipoBusqueda)
                {
                    case "CLIENTE":
                        grpCliente.Visible = true;
                        radTodosLosClientes.Checked = true;
                        radTodosModelos.Checked = true;
                        break;
                    case "SERIE":
                        MostrarSeries(true);
                        Formulario.LlenarComboBox(cboOpcionElegida, "SeleccionarSeriesRicoh");
                        SeleccionClienteHabilitada = false;
                        break;
                    case "MODULOS":
                        grpModulos.Visible = true;
                        SeleccionClienteHabilitada = false;
                        break;
                    case "RANGO DE FECHA":
                        MostrarSeleccionarFechas(true);
                        SeleccionClienteHabilitada = false;

                        chkRango.Visible = false;
                        chkRango.Checked = true;
                        break;
                    case "MANTENIMIENTOS":
                        grpCliente.Visible = true;
                        radTodosLosClientes.Checked = true;
                        radTodosModelos.Checked = true;
                        chkRango.Visible = false;
                        ; break;
                    case "HISTORIAL MODULO":
                        MostrarSeleccionarFechas(true);
                        chkRango.Visible = false;
                        chkRango.Checked = true;
                        cboOpcionElegida.Visible = false;
                        MostrarTextBoxClave(true);
                        SeleccionClienteHabilitada = false;
                        break;
                    case "MODULOS POR SERIE DE EQUIPO":
                        MostrarSeries(true);
                        chkRango.Visible = false;
                        Formulario.LlenarComboBox(cboOpcionElegida, "SeleccionarSeriesRicoh");
                        //Se agrega la opcion "todos" para poder tener los modulos de todos los equipos
                        cboOpcionElegida.Items.Add("TODOS");
                        SeleccionClienteHabilitada = false;
                        break;
                    default:
                        MostrarTextBoxClave(true);
                        cboOpcionElegida.Visible = false;
                        chkRango.Visible = false;
                        MostrarOpcionesModulo(true);
                        SeleccionClienteHabilitada = false;
                        break;
                }
            }
        }

        //Utilizado cuando se elige generar reporte por "Serie" y por "Modulos por serie de equipo"
        public void MostrarSeries(bool MostrarControl)
        {
            cboOpcionElegida.Visible = MostrarControl;
            lblOpcionReporte.Visible = MostrarControl;
            lblOpcionReporte.Text = "Serie:";
        }

        //Utilizado cuando se elige generar reporte por "Historial Modulo"
        public void MostrarTextBoxClave(bool MostrarControl)
        {
            txtClaveModulo.Visible = MostrarControl;
            lblOpcionReporte.Visible = MostrarControl;
            lblOpcionReporte.Text = "Clave:";
        }

        public void MostrarOpcionesModulo(bool MostrarControl)
        {
            lblOpcionReporte.Visible = MostrarControl;
        }

        public void ReiniciarOpciones()
        {
            cboOpcionElegida.Visible = false;
            chkRango.Checked = false;
            MostrarSeleccionarFechas(false);
            btnGenerarReporte.Enabled= false;
            lblOpcionReporte.Visible = false;
            txtClaveModulo.Text = "";
            erReporte.Clear();
            MostrarTextBoxClave(false);
            MostrarSeries(false);


            radTodosLosClientes.Checked = false;
            //Ocultar clientes y modelos
            grpCliente.Visible = false;
            grpModelos.Visible = false;
            grpModulos.Visible = false;

            SeleccionClienteHabilitada = true;
        }
        public void MostrarSeleccionarFechas(bool mostrar)
        {
            lblFechaInicio.Visible = mostrar;
            lblFechaFinal.Visible = mostrar;
            dtpFechaFinal.Visible = mostrar;
            dtpFechaInicial.Visible = mostrar;
        }

        //Mostrar elegir un cliente
        private void rdUnCliente_CheckedChanged(object sender, EventArgs e)
        {
            cboClientes.Visible = true;
            lblCliente.Visible = true;
            Formulario.LlenarComboBox(cboClientes, "SeleccionarClientesServicios");
            //Formulario.LlenarComboBox(cboClientes, "SeleccionarClientes");
        }

        //Ocultar elegir un cliente
        private void radTodosLosClientes_CheckedChanged(object sender, EventArgs e)
        {
            cboClientes.Visible = false;
            lblCliente.Visible = false;

            //Mostrar las modelos
            grpModelos.Visible = true;
        }

        private void radTodosModelos_CheckedChanged(object sender, EventArgs e)
        {
            cboModelos.Visible = false;//Ocultar modelos
            if(TipoBusqueda != "MANTENIMIENTOS")
            {
                TipoBusqueda = "CLIENTE";
            }
        }

        private void radUnModelo_CheckedChanged(object sender, EventArgs e)
        {
            //Mostrar para elegir un modelo ricoh
            cboModelos.Visible = true;
            if(TipoBusqueda != "MANTENIMIENTOS")
            {
                TipoBusqueda = "MODELO";
            }
            Formulario.LlenarComboBox(cboModelos, "SeleccionarModelosRicoh");
        }
        #endregion

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            bool DatosEncontrados;
            if (!ValidarOpcionesReporte())
                return;
            try
            {
                Reporte NuevoReporte = new Reporte()
                {
                    FechaInicio = dtpFechaInicial.Value,
                    FechaFinal = dtpFechaFinal.Value,
                    TipoBusqueda = TipoBusqueda,
                    RangoHabilitado = chkRango.Checked
                };
                DeterminarParametroBusqueda(NuevoReporte);
                VerificarSeleccionDeCliente(NuevoReporte);

                //MessageBox.Show(NuevoReporte.ParametroBusqueda + "   " + NuevoReporte.TipoBusqueda);
                DatosEncontrados = lgReporteModulo.Pdf(NuevoReporte);

                if (!DatosEncontrados)
                {
                    MessageBox.Show("NO EXISTEN REGISTROS", "DATOS NO ENCONTRADOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ocurrio un error: " + ex.Message);
            }
        }

        public void VerificarSeleccionDeCliente(Reporte NuevoReporte)
        {
            if (radTodosLosClientes.Checked || SeleccionClienteHabilitada == false)
            {
                NuevoReporte.Cliente = "";
            }
            else
            {
                NuevoReporte.Cliente = cboClientes.SelectedItem.ToString();
            }
        }

        public void DeterminarParametroBusqueda(Reporte NuevoReporte)
        {
            NuevoReporte.ParametroBusqueda = "";
            string OpcionElegida = "";

            switch (NuevoReporte.TipoBusqueda)
            {
                case "HISTORIAL MODULO":
                    NuevoReporte.ParametroBusqueda = txtClaveModulo.Text;
                    break;
                case "UBICACION MODULO":
                    NuevoReporte.ParametroBusqueda = txtClaveModulo.Text;
                    break;
                case "MODULOS":
                    NuevoReporte.ParametroBusqueda = cboModulos.SelectedItem.ToString();
                    NuevoReporte.TipoBusquedaAdicional = cboModelosModulos.SelectedItem.ToString();
                    break;
                case "RANGO DE FECHA":
                    NuevoReporte.ParametroBusqueda = "";
                    break;
                case "MANTENIMIENTOS":
                    NuevoReporte.ParametroBusqueda = (radTodosModelos.Checked) ? "" : cboModelos.SelectedItem.ToString();
                    break;
                case "SERIE":
                    NuevoReporte.ParametroBusqueda = cboOpcionElegida.SelectedItem.ToString();
                    break;
                case "MODULOS POR SERIE DE EQUIPO":
                    OpcionElegida = cboOpcionElegida.SelectedItem.ToString();
                    NuevoReporte.ParametroBusqueda = (OpcionElegida != "TODOS")?OpcionElegida:"";
                    break;
                case "CLIENTE":
                    NuevoReporte.ParametroBusqueda = (radUnModelo.Checked) ? cboModelos.SelectedItem.ToString() : "";
                    break;
                case "MODELO":
                    NuevoReporte.ParametroBusqueda = cboModelos.SelectedItem.ToString();
                    break;
            }
        }

        private void chkRango_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRango.Checked)
            {
                MostrarSeleccionarFechas(true);
            }
            else
            {
                MostrarSeleccionarFechas(false);
            }
        }

        #region ReporteModulos
        private void cboModelosModulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboModelosModulos.SelectedItem.ToString() != " ")
            {
                int IdModelo = NuevaAccion.BuscarId(cboModelosModulos.SelectedItem.ToString(), "spObtenerIdModeloModulo");
                Formulario.LlenarComboBox(cboModulos, "spSeleccionarModulos", IdModelo);
            }
        }

        #endregion

        private void cboModulos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
