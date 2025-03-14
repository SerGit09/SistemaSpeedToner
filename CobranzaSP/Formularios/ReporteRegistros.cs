using CobranzaSP.Lógica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using CobranzaSP.Modelos;
using DocumentFormat.OpenXml.Vml.Spreadsheet;

namespace CobranzaSP.Formularios
{
    public partial class ReporteRegistros : Form
    {
        public ReporteRegistros()
        {
            InitializeComponent();
            InicioAplicacion();
        }
        AccionesLógica NuevaAccion = new AccionesLógica();
        CD_Conexion cn = new CD_Conexion();
        LogicaRegistro AccionRegistro = new LogicaRegistro();
        FuncionesFormularios Formulario = new FuncionesFormularios();
        string TipoBusqueda;
        bool Validado;

        #region Inicio
        public void InicioAplicacion()
        {
            cboMarca.DropDownStyle = ComboBoxStyle.DropDownList;
            cboBusqueda.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTipoBusqueda.DropDownStyle = ComboBoxStyle.DropDownList;
            
            LlenarComboBox(cboCliente, "SeleccionarClientesServicios", 0);
            //LlenarComboBox(cboCliente, "SeleccionarClientes", 0);
            LlenarComboBoxTipoBusqueda();
        }

        public void LlenarComboBoxTipoBusqueda()
        {
            string[] Opciones = { "", "Cliente", "Modelo" };
            cboTipoBusqueda.Items.AddRange(Opciones);
        }

        public void LlenarComboBox(ComboBox cb, string sp, int indice)
        {
            SqlDataReader dr;
            cb.Items.Clear();
            if (sp == "SeleccionarModelos" || sp == "SeleccionarCartuchos")
            {
                dr = NuevaAccion.LlenarComboBoxModelos(sp, indice);
            }
            else
            {
                dr = NuevaAccion.LlenarComboBox(sp);
            }

            while (dr.Read())
            {
                //Agregamos las opciones dependiendo los registros que nos devolvieron
                cb.Items.Add(dr[0].ToString());
            }

            //Agregamos un espacio en blanco y lo asignamos como opcion por defecto
            cb.Items.Insert(0, " ");
            cb.SelectedIndex = 0;
            dr.Close();
            cn.CerrarConexion();
        }
        #endregion

        #region Validaciones
        public bool ValidarCampos()
        {   
            Validado = true;
            erRegistro.Clear();
            if (cboTipoBusqueda.SelectedIndex != -1)
            {
                string TipoBusqueda = cboTipoBusqueda.SelectedItem.ToString();
                switch (TipoBusqueda)
                {
                    case "Modelo": Validado = ValidarModelo(); break;
                    case "Cliente": ValidarCampo(cboCliente,"Seleccione un cliente"); break;
                }

            }
            return Validado;
        }

        public bool ValidarModelo()
        {
            ValidarCampo(cboMarca, "Seleccione una marca");
            ValidarCampo(cboBusqueda, "Seleccione un modelo");

            if (chkCliente.Checked)
            {
                ValidarCampo(cboCliente, "Seleccione un cliente");
            }

            return Validado;
        }

        public void ValidarCampo(Control c, string Mensaje)
        {
            if (c is TextBox || c is RichTextBox)
            {
                if (string.IsNullOrEmpty(c.Text))
                {
                    erRegistro.SetError(c, Mensaje);
                    Validado = false;
                }
            }
            else if (c is ComboBox)
            {
                ComboBox combo = (ComboBox)c;
                if (combo.SelectedIndex == -1 || combo.SelectedIndex == 0)
                {
                    erRegistro.SetError(c, Mensaje);
                    Validado = false;
                }
            }
        }
        #endregion


        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
            {
                return;
            }

            ReporteRegistroInventarioToners NuevoReporte = new ReporteRegistroInventarioToners()
            {
                FechaInicio = dtpFechaInicio.Value,
                FechaFinal = dtpFechaFinal.Value,
                TipoBusqueda = (cboTipoBusqueda.SelectedIndex == -1) ? "" : cboTipoBusqueda.SelectedItem.ToString()
            };
            //Comprobamos que si no se trata de un reporte por modelo para añadir los campos correspondientes
            DeterminarTipoBusqueda(NuevoReporte);
            //EsReporteModelo(NuevoReporte);

            //AQUI ES DONDE SE PONDRA EL GLOBAL PARA LOS 2, ES DECIR EL PDF PARA LOS 2
            if (!AccionRegistro.GenerarPdf(NuevoReporte))
            {
                MessageBox.Show("¡NO SE ENCONTRARON REGISTROS!", "DATOS NO ENCONTRADOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //cboBusqueda.SelectedIndex = 0;
        }

        public void DeterminarTipoBusqueda(ReporteRegistroInventarioToners NuevoReporte)
        {
            if(NuevoReporte.TipoBusqueda == "Cliente")
            {
                NuevoReporte.Cliente = cboCliente.SelectedItem.ToString();
                NuevoReporte.ParametroBusqueda = "";
            }
            else if (NuevoReporte.TipoBusqueda == "Modelo")
            {
                NuevoReporte.Marca = cboMarca.SelectedItem.ToString();
                NuevoReporte.ParametroBusqueda = cboBusqueda.SelectedItem.ToString();
                NuevoReporte.Cliente = (chkCliente.Checked) ? cboCliente.SelectedItem.ToString() : "";
                NuevoReporte.IdTipoArticulo = NuevaAccion.BuscarId(NuevoReporte.ParametroBusqueda, "ObtenerIdTipoArticulo");
            }
        }

        //METODOS QUE SE ESTAN CREANDO PARA SER MAS ENTENDIBLE LOS REPORTES
        public void EsReporteModelo(ReporteRegistroInventarioToners NuevoReporte)
        {
            if (NuevoReporte.TipoBusqueda == "Modelo")
            {
                NuevoReporte.Marca = cboMarca.SelectedItem.ToString();
                NuevoReporte.Cliente = (chkCliente.Checked) ? cboCliente.SelectedItem.ToString() : "";
                NuevoReporte.IdTipoArticulo = NuevaAccion.BuscarId(NuevoReporte.ParametroBusqueda, "ObtenerIdTipoArticulo");
            }

        }

        #region PanelSuperior
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lparam);

        private void panelSuperior_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion

        private void cboBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboTipoBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnGenerar.Enabled = true;
            TipoBusqueda = cboTipoBusqueda.SelectedItem.ToString();

            cboCliente.Visible = false;
            cboBusqueda.Visible = false;
            HabilitarClienteEspecifico(false);

            switch (cboTipoBusqueda.SelectedItem.ToString())
            {
                case "Cliente":
                    LlenarComboBox(cboCliente, "SeleccionarClientesServicios", 0);
                    //LlenarComboBox(cboCliente, "SeleccionarClientes", 0);
                    cboCliente.Visible = true;
                    break;
                case "Modelo":
                    LlenarComboBox(cboMarca, "SeleccionarMarca", 0);
                    cboMarca.Visible = true;
                    HabilitarClienteEspecifico(true);
                    break;
            }
            
        }

        public void HabilitarClienteEspecifico(bool mostrar)
        {
            chkCliente.Visible = mostrar;
        }

        private void cboMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMarca.SelectedItem.ToString() != " ")
            {
                int IdMarca = NuevaAccion.BuscarId(cboMarca.SelectedItem.ToString(), "ObtenerIdMarca");
                Formulario.LlenarComboBox(cboBusqueda, "spSeleccionarCartuchosMarca", IdMarca);
            }
        }

        private void chkCliente_CheckedChanged(object sender, EventArgs e)
        {
            cboCliente.Visible = (chkCliente.Checked) ? true : false;
        }

        private void cboMarca_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cboMarca.SelectedItem.ToString() != " ")
            {
                int IdMarca = NuevaAccion.BuscarId(cboMarca.SelectedItem.ToString(), "ObtenerIdMarca");
                Formulario.LlenarComboBox(cboBusqueda, "spSeleccionarCartuchosMarca", IdMarca);
                cboBusqueda.Visible = true;
            }
        }

        private void cboBusqueda_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
