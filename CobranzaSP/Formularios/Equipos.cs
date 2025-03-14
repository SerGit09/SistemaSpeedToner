using CobranzaSP.Lógica;
using CobranzaSP.Modelos;
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
    public partial class Equipos : Form
    {
        public Equipos()
        {
            InitializeComponent();
            InicioAplicacion();
        }
        AccionesLógica NuevaAccion = new AccionesLógica();
        CD_Conexion cn = new CD_Conexion();
        LogicaEquipos lgEquipo = new LogicaEquipos();
        LogicaEquiposBodega lgEquipoBodega = new LogicaEquiposBodega();
        FuncionesFormularios Formulario = new FuncionesFormularios();
        //Variable que servira para los  reportes
        string TipoBusqueda = "";
        string Parametro = "";

        //Sabremos cuando estamos añadiendo un nuevo registro o modificando
        bool Modificando = false;
        bool BuscandoFolio = false;
        bool Validado;
        private bool Buscando = false;
        int Id;
        int IdSerie;

        #region Inicio
        public void InicioAplicacion()
        {
            Formulario.PropiedadesDtg(dtgEquipos);

            ControlesDesactivados(false);

            //Llenar combobox
            Formulario.LlenarComboBox(cboClientes, "SeleccionarClientesServicios");
            //Formulario.LlenarComboBox(cboClientes, "SeleccionarClientes");
            Formulario.LlenarComboBox(cboTipoRenta, "spSeleccionarTipoRenta");
            Formulario.LlenarComboBox(cboMarcas, "SeleccionarMarca");
            Formulario.LlenarComboBox(cboModelos, "SeleccionarModelos");

            //Llenamos el datagridview
            Mostrar("spMostrarEquiposRentados");

            //Deshabilitamos escritura en combobox
            cboClientes.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTipoRenta.DropDownStyle = ComboBoxStyle.DropDownList;
        }


        public void ControlesDesactivados(bool Desactivado)
        {
            btnEliminar.Enabled = Desactivado;
            btnCancelar.Enabled = Desactivado;
            btnEnviarABodega.Enabled = Desactivado;
        }

        public void Mostrar(string sp)
        {
            //Limpiamos los datos del datagridview
            dtgEquipos.DataSource = null;
            dtgEquipos.Refresh();
            DataTable tabla = new DataTable();
            //Guardamos los registros dependiendo la consulta
            tabla = NuevaAccion.Mostrar(sp);
            //Asignamos los registros que optuvimos al datagridview
            dtgEquipos.DataSource = tabla;
            dtgEquipos.Columns["IdSerie"].Visible = false;
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
            ValidarCampo(txtPrecioRenta, "Ingrese el monto de la renta del equipo");
            ValidarCampo(txtFechaPago, "Ingrese la fecha de pago");
            ValidarCampo(txtPrecioEquipo, "Ingrese el precio del equipo");

            return Validado;
        }

        public void ValidarCampo(Control c,string Mensaje)
        {
            if (c is TextBox || c is RichTextBox)
            {
                if (string.IsNullOrEmpty(c.Text))
                {
                    erEquipos.SetError(c,Mensaje);
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

        

        private void txtSerie_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validacion.SoloLetrasYNumeros(e);
        }

        private void txtReferencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validacion.SoloLetrasYNumeros(e);
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validacion.SoloNumeros(e);
            if ((e.KeyChar == '.') && (!txtPrecioRenta.Text.Contains(".")))
            {
                e.Handled = false;
            }
        }

        private void txtFechaPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validacion.SoloLetrasYNumeros(e);
        }
        #endregion

        #region Botones
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool SerieRepetida = false;
            try
            {
                if (!ValidarCampos())
                    return;

                Equipo nuevoEquipo = new Equipo()
                {
                    IdEquipo = Id,
                    IdCliente = NuevaAccion.BuscarId(cboClientes.SelectedItem.ToString(), "ObtenerIdCliente"),
                    Ubicacion = txtReferencia.Text,
                    IdMarca = NuevaAccion.BuscarId(cboMarcas.SelectedItem.ToString(), "ObtenerIdMarca"),
                    IdModelo = NuevaAccion.BuscarId(cboModelos.SelectedItem.ToString(), "ObtenerIdModelo"),
                    Serie = txtSerie.Text,
                    IdSerie = IdSerie,
                    IdRenta = NuevaAccion.BuscarId(cboTipoRenta.SelectedItem.ToString(), "ObtenerIdTipoRenta"),
                    PrecioRenta = double.Parse(txtPrecioRenta.Text.Replace(",", "")),
                    FechaPago = txtFechaPago.Text,
                    Valor = double.Parse(txtPrecioEquipo.Text)
                };
                MessageBox.Show(nuevoEquipo.IdSerie + "");
                if (Modificando)
                {
                    //Modificar stop procedure de modificar, para que se pueda cambiar la fecha
                    if (MessageBox.Show("¿Esta seguro de modificar el registro?", "CONFIRME LA MODIFICACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        MessageBox.Show("!!Modificación cancelada!!", "OPERACION EXITOSA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarForm();
                        return;
                    }
                }
                else
                {
                    SerieRepetida = NuevaAccion.VerificarDuplicados(nuevoEquipo.Serie, "spVerificarDuplicadoSerieEquipos");
                    if (SerieRepetida)
                    {
                        MessageBox.Show("Ingrese un numero de serie distinto", "SERIE YA EXISTENTE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                string Mensaje = lgEquipo.GuardarEquipo(nuevoEquipo);

                MessageBox.Show(Mensaje, "OPERACION EXITOSA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Mostrar("spMostrarEquiposRentados");
                //LimpiarForm();
                ReiniciarControles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error" + ex);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Esta seguro de eliminar el registro?", "CONFIRME LA ELIMINACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    MessageBox.Show("!!Eliminacion cancelada!!", "CANCELADO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    LimpiarForm();
                    return;
                }
                NuevaAccion.Eliminar(Id, "spEliminarEquipoRentado");
                MessageBox.Show("Se ha eliminado el registro correctamente", "ELIMINACION CONFIRMADA", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Mostrar("spMostrarEquiposRentados");
                LimpiarForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ReiniciarControles();
        }

        #endregion

        #region Eventos
        private void cboMarcas_SelectedIndexChanged(object sender, EventArgs e)
        {
            //En dado caso que se haya seleccionado algo de las marcas
            if (cboMarcas.SelectedItem.ToString() != " " && Buscando == false)
            {
                int IdMarca = NuevaAccion.BuscarId(cboMarcas.SelectedItem.ToString(), "ObtenerIdMarca");
                //Se llenara de acuerdo a la marca que se haya escogido
                Formulario.LlenarComboBox(cboModelos, "SeleccionarModelos", IdMarca);
            }
        }
        private void dtgEquipos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LimpiarForm();
            ControlesDesactivados(true);
            Modificando = true;
            erEquipos.Clear();

            Id = int.Parse(dtgEquipos.CurrentRow.Cells[0].Value.ToString());
            cboClientes.SelectedItem = dtgEquipos.CurrentRow.Cells[1].Value.ToString();
            txtReferencia.Text = dtgEquipos.CurrentRow.Cells[2].Value.ToString();
            cboMarcas.SelectedItem = dtgEquipos.CurrentRow.Cells[3].Value.ToString();
            cboModelos.SelectedItem = dtgEquipos.CurrentRow.Cells[4].Value.ToString();
            txtSerie.Text = dtgEquipos.CurrentRow.Cells[5].Value.ToString();
            cboTipoRenta.SelectedItem = dtgEquipos.CurrentRow.Cells[6].Value.ToString();
            txtPrecioRenta.Text = dtgEquipos.CurrentRow.Cells[7].Value.ToString().Replace("$", "");
            txtFechaPago.Text = dtgEquipos.CurrentRow.Cells[8].Value.ToString();
            txtPrecioEquipo.Text = dtgEquipos.CurrentRow.Cells[9].Value.ToString().Replace("$", "");
            IdSerie = int.Parse(dtgEquipos.CurrentRow.Cells[10].Value.ToString());
        }

        #endregion


        #region MetodosLocales
        public void LimpiarForm()
        {
            foreach (Control c in grpDatos.Controls)
            {
                if (c is TextBox)
                {
                    c.Text = "";
                }
            }
            cboClientes.Focus();

            cboClientes.SelectedIndex = 0;
            cboTipoRenta.SelectedIndex = 0;
            cboMarcas.SelectedIndex = 0;
            cboModelos.SelectedIndex = 0;
            Formulario.LlenarComboBox(cboModelos, "SeleccionarModelos");
        }

        public void ReiniciarControles()
        {
            ControlesDesactivados(false);
            LimpiarForm();
            Modificando = false;
        }

        #endregion

        private void grpDatos_Enter(object sender, EventArgs e)
        {

        }



        private void btnEnviarABodega_Click(object sender, EventArgs e)
        {
            string Mensaje;
            bool SerieRepetida;

            if (MessageBox.Show("¿Esta seguro de enviar el equipo a bodega?", "CONFIRMAR ACCION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                MessageBox.Show("!!Envio cancelado!!", "CANCELADO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                LimpiarForm();
                return;
            }

            try
            {
                if (!ValidarCampos())
                    return;

                EquipoBodega NuevoEquipo = new EquipoBodega()
                {
                    IdMarca = NuevaAccion.BuscarId(cboMarcas.SelectedItem.ToString(), "ObtenerIdMarca"),
                    IdModelo = NuevaAccion.BuscarId(cboModelos.SelectedItem.ToString(), "ObtenerIdModelo"),
                    Serie = txtSerie.Text,
                    Precio = double.Parse(txtPrecioEquipo.Text),
                    Estado = "Usada",
                    Notas = "",
                    FechaDeLlegada = DateTime.Now
                };
                SerieRepetida = NuevaAccion.VerificarDuplicados(NuevoEquipo.Serie, "VerificarSerieEquiposBodega");
                if (SerieRepetida)
                {
                    MessageBox.Show("Ingrese un numero de serie distinto", "SERIE YA EXISTENTE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LimpiarForm();
                    return;
                }
                Mensaje = lgEquipoBodega.GuardarEquipo(NuevoEquipo);

                //Eliminarlo de los equipos en renta
                NuevaAccion.Eliminar(Id, "spEliminarEquipoRentado");

                LimpiarForm();
                Mostrar("spMostrarEquiposRentados");//Actualizar equipos en renta
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron guardar los datos por: " + ex);
            }
        }


        private void btnReportes_Click(object sender, EventArgs e)
        {
            ReporteEquipoRenta NuevoReporte = new ReporteEquipoRenta();
            NuevoReporte.Show();
        }
    }
}
