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
    public partial class DatosImpresoraRentada : Form
    {
        private EquiposBodega EquipoBodega;
        public DatosImpresoraRentada(EquiposBodega EquipoBodega, Equipo nuevoEquipo, int Id)
        {
            InitializeComponent();
            InicioAplicacion();
            this.EquipoBodega = EquipoBodega;
            idEquipo = Id;
            this.IdSerie = nuevoEquipo.IdSerie;
            //Cargamos los datos que teniamos desde bodega
            txtPrecioEquipo.Text = nuevoEquipo.PrecioRenta.ToString();
            cboMarcas.SelectedItem = nuevoEquipo.Marca;
            cboModelos.SelectedItem = nuevoEquipo.Modelo;
            txtSerie.Text = nuevoEquipo.Serie.ToString();
        }
        AccionesLógica NuevaAccion = new AccionesLógica();
        CD_Conexion cn = new CD_Conexion();
        LogicaEquipos lgEquipo = new LogicaEquipos();
        FuncionesFormularios Formulario = new FuncionesFormularios();
        int idEquipo;
        int IdSerie;
        bool Validado;


        #region Inicio
        public void InicioAplicacion()
        {
            Formulario.LlenarComboBox(cboClientes, "SeleccionarClientesServicios");
            //Formulario.LlenarComboBox(cboClientes, "SeleccionarClientes");
            Formulario.LlenarComboBox(cboTipoRenta, "spSeleccionarTipoRenta");
            Formulario.LlenarComboBox(cboMarcas, "SeleccionarMarca");
            Formulario.LlenarComboBox(cboModelos, "SeleccionarModelos");

            //Deshabilitamos escritura en combobox
            cboClientes.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTipoRenta.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        #endregion

        #region Validaciones
        public bool ValidarCampos()
        {
            Validado = true;
            erEquipos.Clear();

            ValidarCampo(cboClientes, "Seleccione un cliente");
            ValidarCampo(cboMarcas, "Seleccione una marca");
            ValidarCampo(cboModelos, "Seleccione un modelo");
            ValidarCampo(txtSerie, "Ingrese serie de equipo");
            ValidarCampo(cboTipoRenta, "Seleccione el tipo de renta");
            ValidarCampo(txtPrecio, "Ingrese el monto de la renta del equipo");
            ValidarCampo(txtFechaPago, "Ingrese la fecha de pago");
            ValidarCampo(txtPrecioEquipo, "Ingrese el precio del equipo");

            return Validado;
        }

        public void ValidarCampo(Control c, string Mensaje)
        {
            if (c is TextBox || c is RichTextBox)
            {
                if (string.IsNullOrEmpty(c.Text))
                {
                    erEquipos.SetError(c, Mensaje);
                    Validado = false;
                }
            }
            else if (c is ComboBox)
            {
                ComboBox combo = (ComboBox)c;
                if (combo.SelectedIndex == -1 || combo.SelectedIndex == 0)
                {
                    erEquipos.SetError(c, Mensaje);
                    Validado = false;
                }
            }
        }
        #endregion

        #region Botones

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool SerieRepetida = false;
            if (!ValidarCampos())
                return;
            try
            {
                Equipo nuevoEquipo = new Equipo()
                {
                    IdCliente = NuevaAccion.BuscarId(cboClientes.SelectedItem.ToString(), "ObtenerIdCliente"),
                    Ubicacion = txtReferencia.Text,
                    IdMarca = NuevaAccion.BuscarId(cboMarcas.SelectedItem.ToString(), "ObtenerIdMarca"),
                    IdModelo = NuevaAccion.BuscarId(cboModelos.SelectedItem.ToString(), "ObtenerIdModelo"),
                    Serie = txtSerie.Text,
                    IdRenta = NuevaAccion.BuscarId(cboTipoRenta.SelectedItem.ToString(), "ObtenerIdTipoRenta"),
                    PrecioRenta = double.Parse(txtPrecio.Text.Replace(",", "")),
                    FechaPago = txtFechaPago.Text,
                    Valor = double.Parse(txtPrecioEquipo.Text),
                    IdSerie = this.IdSerie
                };
                SerieRepetida = NuevaAccion.VerificarDuplicados(nuevoEquipo.Serie, "spVerificarDuplicadoSerieEquipos");
                if (SerieRepetida)
                {
                    MessageBox.Show("Ingrese un numero de serie distinto", "SERIE YA EXISTENTE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                lgEquipo.GuardarEquipo(nuevoEquipo);
                NuevaAccion.Eliminar(idEquipo, "EliminarEquipoBodega");
                MessageBox.Show("Equipo agregado correctamente", "OPERACION EXITOSA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Limpiamos entonces el equipo que seleccionamos en bodega y que ahora se ira a equipos en renta
                EquipoBodega.LimpiarForm();
                EquipoBodega.MostrarDatosEquipos();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ah ocurrido un error " + ex.Message);
            }
        }

        #endregion
    }
}
