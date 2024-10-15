using CobranzaSP.Lógica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using CobranzaSP.Modelos;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace CobranzaSP.Formularios
{
    public partial class Fusores : Form
    {
        public Fusores()
        {
            InitializeComponent();
            InicioAplicacion();
        }
        AccionesLógica NuevaAccion = new AccionesLógica();
        CD_Conexion cn = new CD_Conexion();
        LogicaFusor AccionFusor = new LogicaFusor();
        FuncionesFormularios Formulario = new FuncionesFormularios();
        bool Modificando = false;
        bool EsFusorReconstruido = false;
        bool Validado;
        int Id;

        #region Inicio

        public void InicioAplicacion()
        {
            string[] opcionesBusqueda = { "", "Habilitado", "Deshabilitada", "Rango Fecha", "Serie", "Todos" };
            cboBusqueda.Items.AddRange(opcionesBusqueda);
            cboBusqueda.SelectedIndex = 0;

            string[] NumeroDias = { "", "0", "30", "90", "120", "180","365" };
            cboDiasGarantía.Items.AddRange(NumeroDias);
            cboDiasGarantía.SelectedIndex = 0;

            radActivo.Checked = true;

            Formulario.PropiedadesDtg(dtgFusores);
            MostrarDatosFusores();
            ControlesDesactivadosInicialmente(false);
            //Llenar el combobox de los modelos
            Formulario.LlenarComboBox(cboModelos, "spSeleccionarModelosFusores");
            Formulario.LlenarComboBox(cboProveedores, "SeleccionarProveedores");
        }

        private void ControlesDesactivadosInicialmente(bool activado)
        {
            btnCancelar.Enabled = activado;
            btnEliminar.Enabled = activado;
            btnGenerarReporte.Enabled = activado;
        }

        public void MostrarDatosFusores()
        {
            //Limpiamos los datos del datagridview
            dtgFusores.DataSource = null;
            dtgFusores.Refresh();
            DataTable tabla = new DataTable();
            //Guardamos los registros dependiendo la consulta
            tabla = NuevaAccion.Mostrar("spMostrarTodosFusores");
            //Asignamos los registros que optuvimos al datagridview
            dtgFusores.DataSource = tabla;
        }

        #endregion

        #region Validaciones
        public bool ValidarCampos()
        {
            Validado = true;
            erFusores.Clear();

            Validado = ValidarCampo(txtSerie);
            Validado = ValidarCampo(txtSerieSp);
            Validado = ValidarCampo(cboModelos);
            if (!EsFusorReconstruido)
            {
                Validado = ValidarCampo(txtFactura);
                Validado = ValidarCampo(txtCosto);
                Validado = ValidarCampo(cboProveedores);
                Validado = ValidarCampo(cboDiasGarantía);
            }

            return Validado;
        }
        public bool ValidarCampo(Control control)
        {
            // Validar campos de tipo TextBox
            if (control is TextBox textBox && string.IsNullOrEmpty(textBox.Text))
            {
                erFusores.SetError(textBox, "Campo obligatorio");
                return Validado = false;
            }
            // Validar campos de tipo ComboBox
            else if (control is ComboBox comboBox && comboBox.SelectedIndex == 0)
            {
                erFusores.SetError(control, "Seleccione una opcion");
                return Validado = false;
            }
            return Validado;
        }

        public bool ValidarCamposReporte()
        {
            Validado = true;
            erFusores.Clear();

            ValidarCampo(cboBusqueda);
            string Parametro = cboBusqueda.SelectedItem.ToString();
            if (Parametro == "Serie")
            {
                ValidarCampo(txtSerieBusqueda);
            }
            return Validado;
        }
        #endregion

        #region Botones
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool Repetido = false;
            string Mensaje;
            try
            {
                if (!ValidarCampos())
                {
                    return;
                }
                Fusor nuevoFusor = new Fusor()
                {
                    IdFusor = Id,
                    SerieO = txtSerie.Text,
                    SerieS = txtSerieSp.Text,
                    FechaFactura = dtpFechaFactura.Value,
                    Proveedor = cboProveedores.SelectedItem.ToString(),
                    Estado = (radActivo.Checked) ? "Activado" : "Desactivado",
                    IdCartucho = NuevaAccion.BuscarId(cboModelos.SelectedItem.ToString(), "ObtenerIdCartucho")
                };
                VerificarFusorReconstruido(nuevoFusor);
                if (Modificando)
                {
                    if (MessageBox.Show("Desea modificar el registro?", "CONFIRME LA MODIFICACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        MessageBox.Show("Modificacion cancelada!!", "CANCELADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ReiniciarControles();
                        return;
                    }
                }
                else
                {
                    Repetido = NuevaAccion.VerificarDuplicados(nuevoFusor.SerieO, "spVerificarSerieExistenteFusor");
                    Repetido = NuevaAccion.VerificarDuplicados(nuevoFusor.SerieS, "spVerificarSerieExistenteFusor");
                    if (Repetido)
                    {
                        MessageBox.Show("Ingrese un numero de serie distinto", "SERIE YA EXISTENTE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                Mensaje = AccionFusor.GuardarFusor(nuevoFusor);
                MessageBox.Show(Mensaje, "OPERACION EXITOSA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarDatosFusores();
                ReiniciarControles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void VerificarFusorReconstruido(Fusor nuevoFusor)
        {
            if (EsFusorReconstruido)
            {
                nuevoFusor.NumeroFactura = "";
                nuevoFusor.Cantidad = 0;
                nuevoFusor.IdProveedor = 23;//SpeedToner

                nuevoFusor.DiasGarantia = 0;
            }
            else
            {
                nuevoFusor.NumeroFactura = txtFactura.Text;
                nuevoFusor.IdProveedor = NuevaAccion.BuscarId(cboProveedores.SelectedItem.ToString(), "ObtenerIdProveedor");
                nuevoFusor.Cantidad = double.Parse(txtCosto.Text.Replace(",", ""));
                nuevoFusor.DiasGarantia = int.Parse(cboDiasGarantía.SelectedItem.ToString());
            }
        }

        private void btnGenerarExcel_Click(object sender, EventArgs e)
        {
            AccionFusor.GenerarExcel();
        }

        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCamposReporte())
                    return;

                Reporte NuevoReporte = new Reporte()
                {
                    FechaInicio = dtpFechaInicio.Value,
                    FechaFinal = dtpFechaFinal.Value,
                    RangoHabilitado = (cboBusqueda.SelectedItem.ToString() == "Rango Fecha") ? true : false
                };
                DeterminarParametroBusqueda(NuevoReporte);
                AccionFusor.ReporteFusores(NuevoReporte);
                LimpiarForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DeterminarParametroBusqueda(Reporte NuevoReporte)
        {
            switch (cboBusqueda.SelectedItem.ToString())
            {
                case "Serie": NuevoReporte.ParametroBusqueda = txtSerieBusqueda.Text; break;
                case "Rango Fecha": NuevoReporte.ParametroBusqueda = ""; break;
                case "Todos": NuevoReporte.ParametroBusqueda = ""; break;
                default: NuevoReporte.ParametroBusqueda = cboBusqueda.SelectedItem.ToString(); break;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ReiniciarControles();
            txtSerie.Focus();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea eliminar el registro?", "CONFIRME LA ELIMINACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                MessageBox.Show("Eliminación cancelada!!", "OPERACION CANCELADA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReiniciarControles();
                return;
            }

            NuevaAccion.Eliminar(Id, "spEliminarFusor");
            MessageBox.Show("Fusor eliminado correctamente", "OPERACION EXITOSA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MostrarDatosFusores();
            ReiniciarControles();
        }
        #endregion

        #region Eventos
        private void cboBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReiniciarOpciones();
            string Parametro = cboBusqueda.SelectedItem.ToString();
            if (Parametro == "")
            {
                btnGenerarReporte.Enabled = false;
            }
            switch (Parametro)
            {
                case "Rango Fecha": MostrarFechas(true); ; break;
                case "Serie": txtSerieBusqueda.Visible = true; txtSerieBusqueda.Focus(); break;
            }
        }

        public void ReiniciarOpciones()
        {
            btnGenerarReporte.Enabled = true;
            MostrarFechas(false);
            txtSerieBusqueda.Visible = false;
            txtSerieBusqueda.Text = "";
        }

        private void dtgFusores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ControlesDesactivadosInicialmente(true);
            LimpiarForm();
            Modificando = true;
            bool activo;
            Id = int.Parse(dtgFusores.CurrentRow.Cells[0].Value.ToString());
            txtSerie.Text = dtgFusores.CurrentRow.Cells[1].Value.ToString();
            txtSerieSp.Text = dtgFusores.CurrentRow.Cells[2].Value.ToString();
            txtFactura.Text = dtgFusores.CurrentRow.Cells[3].Value.ToString();
            cboProveedores.SelectedItem = dtgFusores.CurrentRow.Cells[4].Value.ToString();
            dtpFechaFactura.Value = Convert.ToDateTime(dtgFusores.CurrentRow.Cells[5].Value.ToString());
            txtCosto.Text = dtgFusores.CurrentRow.Cells[7].Value.ToString().Replace("$", "");

            string Estado = dtgFusores.CurrentRow.Cells[10].Value.ToString();
            cboModelos.SelectedItem = dtgFusores.CurrentRow.Cells[11].Value.ToString();
            activo = (Estado == "Activado") ? true : false;

            cboDiasGarantía.SelectedItem = dtgFusores.CurrentRow.Cells[8].Value.ToString();


            if (activo)
            {
                radActivo.Checked = activo;
            }
            else
            {
                radInactivo.Checked = true;
            }
        }
        #endregion

        #region MetodosLocales
        public void MostrarFechas(bool Mostrar)
        {
            lblFechaFinal.Visible = Mostrar;
            lblFechaInicio.Visible = Mostrar;
            dtpFechaInicio.Visible = Mostrar;
            dtpFechaFinal.Visible = Mostrar;
        }

        public void LimpiarForm()
        {
            foreach (Control c in grpDatos.Controls)
            {
                if (c is TextBox)
                {
                    c.Text = "";
                }
            }
            dtpFechaFactura.Value = DateTime.Today;
            dtpFechaInicio.Value = DateTime.Today;
            dtpFechaFinal.Value = DateTime.Today;
            MostrarFechas(false);
            txtSerieBusqueda.Visible = false;
            cboBusqueda.Text = "";
            cboProveedores.SelectedIndex = 0;
            cboModelos.SelectedIndex = 0;
            cboDiasGarantía.SelectedIndex = 0;
            txtSerie.Focus();
        }

        public void ReiniciarControles()
        {
            LimpiarForm();
            ControlesDesactivadosInicialmente(false);
            Modificando = false;
            Id = 0;
        }
        #endregion

        private void cboModelos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #region OpcionesFusor
        private void chkFusorReconstruido_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFusorReconstruido.Checked)
            {
                EsFusorReconstruido = true;
                MostrarCampos(false);
                Formulario.LlenarComboBox(cboModelos, "spSeleccionarModelosFusoresReconstruidos");
            }
            else
            {
                EsFusorReconstruido = false;
                MostrarCampos(true);
                Formulario.LlenarComboBox(cboModelos, "spSeleccionarModelosFusores");
            }
        }

        public void MostrarCampos(bool Mostrar)
        {
            lblFactura.Visible = Mostrar;
            lblDiasGarantia.Visible = Mostrar;
            lblProveedor.Visible = Mostrar;
            lblCosto.Visible = Mostrar;

            txtFactura.Visible = Mostrar;
            cboProveedores.Visible = Mostrar;
            txtCosto.Visible = Mostrar;
            cboDiasGarantía.Visible = Mostrar;
        }
        #endregion

    }
}
