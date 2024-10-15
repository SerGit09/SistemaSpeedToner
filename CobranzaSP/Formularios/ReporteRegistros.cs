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

        #region Inicio
        public void InicioAplicacion()
        {
            cboMarca.DropDownStyle = ComboBoxStyle.DropDownList;
            cboBusqueda.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTipoBusqueda.DropDownStyle = ComboBoxStyle.DropDownList;
            LlenarComboBox(cboBusqueda, "SeleccionarClientesServicios", 0);
            LlenarComboBox(cboClienteEspecifico, "SeleccionarClientesServicios", 0);
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
            bool Validado = true;
            erRegistro.Clear();
            if (cboTipoBusqueda.SelectedIndex != -1)
            {
                string TipoBusqueda = cboTipoBusqueda.SelectedItem.ToString();
                switch (TipoBusqueda)
                {
                    case "Modelo": Validado = ValidarModelo(); break;
                    case "Cliente": Validado = ValidarParametroBusqueda(); break;
                }

            }
            return Validado;
        }

        public bool ValidarModelo()
        {
            bool Validado = true;
            if (cboMarca.SelectedItem == " ")
            {
                erRegistro.SetError(cboMarca, "Seleccione una marca");
                return Validado = false;
            }
            if (cboBusqueda.SelectedItem == " ")
            {
                erRegistro.SetError(cboBusqueda, "Seleccione un modelo");
                Validado = false;
            }

            if (chkCliente.Checked)
            {
                if (cboClienteEspecifico.SelectedItem == " ")
                {
                    erRegistro.SetError(cboClienteEspecifico, "Seleccione un cliente");
                    Validado = false;
                }
            }

            return Validado;
        }

        public bool ValidarParametroBusqueda()
        {
            bool Validado = true;
            if (cboBusqueda.SelectedItem == " ")
            {
                erRegistro.SetError(cboBusqueda, "Seleccione una opcion");
                Validado = false;
            }
            return Validado;
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

            ReporteRegistroInventario NuevoReporte = new ReporteRegistroInventario()
            {
                FechaInicio = dtpFechaInicio.Value,
                FechaFinal = dtpFechaFinal.Value,
                TipoBusqueda = (cboTipoBusqueda.SelectedIndex == -1) ? "" : cboTipoBusqueda.SelectedItem.ToString(),
                //Verificamos que tengamos algun parametro, en dado caso de que no se tenga se deja en blanco
                ParametroBusqueda = (cboBusqueda.SelectedIndex == -1) ? "" : cboBusqueda.SelectedItem.ToString(),
            };
            //Comprobamos que si no se trata de un reporte por modelo para añadir los campos correspondientes
            EsReporteModelo(NuevoReporte);

            //AQUI ES DONDE SE PONDRA EL GLOBAL PARA LOS 2, ES DECIR EL PDF PARA LOS 2
            if (!AccionRegistro.GenerarPdf(NuevoReporte))
            {
                MessageBox.Show("¡NO SE ENCONTRARON REGISTROS!", "DATOS NO ENCONTRADOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cboBusqueda.SelectedIndex = 0;
        }

        //METODOS QUE SE ESTAN CREANDO PARA SER MAS ENTENDIBLE LOS REPORTES
        public void EsReporteModelo(ReporteRegistroInventario NuevoReporte)
        {
            if (NuevoReporte.TipoBusqueda == "Modelo")
            {
                NuevoReporte.Marca = cboMarca.SelectedItem.ToString();
                NuevoReporte.Cliente = (chkCliente.Checked) ? cboClienteEspecifico.SelectedItem.ToString() : "";
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

            cboBusqueda.Visible = false;

            switch (cboTipoBusqueda.SelectedItem.ToString())
            {
                case "Cliente":
                    LlenarComboBox(cboBusqueda, "SeleccionarClientesServicios", 0);
                    cboMarca.Visible = false;
                    HabilitarClienteEspecifico(false);
                    cboBusqueda.Visible = true;
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
            cboClienteEspecifico.Visible = (chkCliente.Checked) ? true : false;
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
