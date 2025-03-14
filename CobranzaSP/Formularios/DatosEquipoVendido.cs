using CobranzaSP.Lógica;
using CobranzaSP.Modelos;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CobranzaSP.Formularios
{
    public partial class DatosEquipoVendido : Form
    {
        public DatosEquipoVendido(Equipo EquipoVendido, int IdEquipo, EquiposBodega equipo)
        {
            InitializeComponent();
            InicioAplicacion();
            cboMarcas.SelectedItem = EquipoVendido.Marca;
            cboModelos.SelectedItem = EquipoVendido.Modelo;
            txtSerie.Text = EquipoVendido.Serie;
            txtPrecioEquipo.Text = EquipoVendido.PrecioRenta.ToString();
            this.IdEquipo = IdEquipo;
            this.IdSerie = EquipoVendido.IdSerie;
            this.equipo = equipo;
        }
        AccionesLógica NuevaAccion = new AccionesLógica();
        CD_Conexion cn = new CD_Conexion();
        LogicaEquipos lgEquipo = new LogicaEquipos();
        FuncionesFormularios Formulario = new FuncionesFormularios();
        int IdEquipo = 0;
        int IdSerie = 0;
        EquiposBodega equipo = new EquiposBodega();

        bool Validado;


        #region Inicio
        public void InicioAplicacion()
        {
            Formulario.LlenarComboBox(cboMarcas, "SeleccionarMarca");
            Formulario.LlenarComboBox(cboModelos, "SeleccionarModelos");
            Formulario.LlenarComboBox(cboClientes, "SeleccionarClientesServicios");
            //Formulario.LlenarComboBox(cboClientes, "SeleccionarClientes");


            //Deshabilitamos escritura en combobox
            cboMarcas.DropDownStyle = ComboBoxStyle.DropDownList;
            cboModelos.DropDownStyle = ComboBoxStyle.DropDownList;

            txtSerie.Enabled = false;
            cboMarcas.Enabled = false;
            cboModelos.Enabled = false;
        }
        #endregion

        #region Validaciones
        public bool ValidarCampos()
        {
            Validado = true;
            erEquipos.Clear();

            ValidarCampo(cboClientes);
            ValidarCampo(txtPrecioEquipo);
            return Validado;
        }

        public void ValidarCampo(Control c)
        {
            if (c is TextBox || c is RichTextBox)
            {
                if (string.IsNullOrEmpty(c.Text))
                {
                    erEquipos.SetError(c, "Campo Obligatorio");
                    Validado = false;
                }
            }
            else if (c is ComboBox)
            {
                ComboBox combo = (ComboBox)c;
                if (combo.SelectedIndex == -1 || combo.SelectedIndex == 0)
                {
                    erEquipos.SetError(c, "Seleccione una opcion");
                    Validado = false;
                }
            }
        }
        #endregion

        #region Botones
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;
            try
            {
                Equipo nuevoEquipo = new Equipo()
                {
                    IdCliente = NuevaAccion.BuscarId(cboClientes.SelectedItem.ToString(), "ObtenerIdCliente"),
                    IdMarca = NuevaAccion.BuscarId(cboMarcas.SelectedItem.ToString(), "ObtenerIdMarca"),
                    IdModelo = NuevaAccion.BuscarId(cboModelos.SelectedItem.ToString(), "ObtenerIdModelo"),
                    Serie = txtSerie.Text,
                    PrecioRenta = double.Parse(txtPrecioEquipo.Text.Replace(",", "")),
                    FechaVenta = dtpFecha.Value,
                    IdSerie = this.IdSerie
                };
                lgEquipo.GuardarEquipoVendido(nuevoEquipo);
                NuevaAccion.Eliminar(IdEquipo, "EliminarEquipoBodega");
                MessageBox.Show("Equipo vendido", "OPERACION EXITOSA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                equipo.LimpiarForm();
                equipo.MostrarDatosEquipos();
                equipo.Refresh();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ah ocurrido un error " + ex.Message);
            }
        }
        #endregion

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
