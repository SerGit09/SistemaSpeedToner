using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CobranzaSP.Lógica;
using CobranzaSP.Modelos;
using CobranzaSP.Formularios;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace CobranzaSP
{
    public partial class Fr_Restauracion_Modulos : Form
    {
        public Fr_Restauracion_Modulos()
        {
            InitializeComponent();
            InicioAplicacion();
        }

        FuncionesFormularios Formulario = new FuncionesFormularios();
        AccionesLógica NuevaAccion = new AccionesLógica();
        LogicaModulosCliente lgModuloCliente = new LogicaModulosCliente();
        LogicaRegistroInventarioRicoh lgRegistroPartes = new LogicaRegistroInventarioRicoh();
        LogicaReportesModulosRestaurados lgReportesModulos = new LogicaReportesModulosRestaurados();
        bool Validado;
        int IdReporte = 0;
        int IdRegistro = 0;
        //Servira para el momento de eliminar una parte
        int IdParte = 0;

        #region Inicio
        public void InicioAplicacion()
        {
            Formulario.LlenarComboBox(cboModulos, "spSeleccionarModulos", 1);
            //lgRegistroPartes.LlenarComboBoxPartes(cboPartesRicoh, "MP-4002");
            Formulario.PropiedadesDtg(dtgReportes);
            Formulario.PropiedadesDtg(dtgPartesUsadas);

            MostrarReportes();
            ControlesDesactivadosInicialmente(false);
            ControlesDesactivadosEnPartes(false);
        }

        public void MostrarReportes()
        {
            //Limpiamos los datos del datagridview
            dtgReportes.DataSource = null;
            dtgReportes.Refresh();
            DataTable tabla = new DataTable();
            //Guardamos los registros dependiendo la consulta
            tabla = NuevaAccion.Mostrar("MostrarReportesModulosRestaurados");
            //Asignamos los registros que optuvimos al datagridview
            dtgReportes.DataSource = tabla;
            dtgReportes.Columns["IdReporte"].Visible = false;
            dtgReportes.Columns["ServicioRealizado"].Visible = false;
        }

        private void ControlesDesactivadosInicialmente(bool activado)
        {
            btnCancelarReporte.Enabled = activado;
            btnEliminarReporte.Enabled = activado;
        }

        private void ControlesDesactivadosEnPartes(bool activado)
        {
            btnCancelarParte.Enabled = activado;
            btnEliminarParte.Enabled = activado;
        }


        #endregion

        #region Validaciones
        public bool ValidarCampos()
        {
            Validado = true;
            erReportesModulos.Clear();

            ValidarCampo(txtNumeroFolio, "Coloque el numero de folio del reporte");
            ValidarCampo(cboModulos, "Seleccione un modulo");
            ValidarCampo(cboClaves, "Seleccione una clave de modulo");

            return Validado;
        }

        public bool ValidarCamposVaciosPartesUsadas()
        {
            Validado = true;
            erReportesModulos.Clear();

            ValidarCampo(txtNumeroFolio, "Coloque el numero de folio del reporte");
            ValidarCampo(cboPartesRicoh,"Seleccione una parte");
            ValidarCampo(txtCantidad,"Coloque la cantidad");
            return Validado;
        }

        public void ValidarCampo(Control c, string Mensaje)
        {
            if (c is TextBox || c is RichTextBox)
            {
                if (string.IsNullOrEmpty(c.Text))
                {
                    erReportesModulos.SetError(c, Mensaje);
                    Validado = false;
                }
            }
            else if (c is ComboBox)
            {
                ComboBox combo = (ComboBox)c;
                if (combo.SelectedIndex == -1 || combo.SelectedIndex == 0)
                {
                    erReportesModulos.SetError(c, Mensaje);
                    Validado = false;
                }
            }
        }
        #endregion

        private void grpDatosReporte_Enter(object sender, EventArgs e)
        {

        }

        #region Modulos
        private void cboModulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboModulos.SelectedIndex != 0)
            {
                int IdModulo = lgModuloCliente.BuscarIdModulo(cboModulos.SelectedItem.ToString(), 1);
                Formulario.LlenarComboBox(cboClaves, "SeleccionarClavesBodega", IdModulo);

                //Dependiendo el modulo elejido se seleccionaran las partes correspondientes a ese modulo
                Formulario.LlenarComboBox(cboPartesRicoh, "SeleccionarPartesDeModulo", IdModulo);

            }
        }
        #endregion

        #region BotonesReportes
        private void btnGuardarReporte_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
            {
                return;
            }
            ReporteModuloRestaurado ReporteModulo = new ReporteModuloRestaurado()
            {
                IdReporte = IdReporte,
                FolioReporte = txtNumeroFolio.Text,
                Fecha = dtpFecha.Value,
                IdModulo = lgModuloCliente.BuscarIdModulo(cboModulos.SelectedItem.ToString(), 1),
                IdClave = NuevaAccion.BuscarId(cboClaves.SelectedItem.ToString(), "ObtenerIdClaveModulo"),
                ServicioRealizado = rtxtServicio.Text
            };

            if(ReporteModulo.IdReporte > 0)
            {
                if (MessageBox.Show("¿Esta seguro de modificar el reporte?", "CONFIRME LA MODIFICACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    MessageBox.Show("Modificacion cancelada!!", "CANCELADO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    LimpiarForm();
                    return;
                }
            }

            //MessageBox.Show(Modulo.IdReporte + "\n" + Modulo.FolioReporte + "\n" + Modulo.Fecha + "\n" + Modulo.IdModulo + "\n" + Modulo.IdClave);
            MessageBox.Show(lgReportesModulos.GuardarReporte(ReporteModulo),"REGISTRO DE REPORTE",MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarForm();
            MostrarReportes();
        }

        private void btnCancelarReporte_Click(object sender, EventArgs e)
        {
            LimpiarForm();
            ControlesDesactivadosInicialmente(false);

            dtgPartesUsadas.DataSource = null;
            dtgPartesUsadas.Refresh();
        }

        private void btnEliminarReporte_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro de eliminar el reporte?", "CONFIRME LA ELIMINACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                MessageBox.Show("Eliminación cancelada!!", "CANCELADO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                LimpiarForm();
                return;
            }

            try
            {
                NuevaAccion.Eliminar(IdReporte, "EliminarReporteModuloRestaurado");
                MessageBox.Show("Se elimino el reporte", "OPERACION EXITOSA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarReportes();
                LimpiarForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo eliminar el registro: " + ex, "OCURRIO UN PROBLEMA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            ControlesDesactivadosInicialmente(false);
        }
        #endregion


        private void Fr_Restauracion_Modulos_Load(object sender, EventArgs e)
        {

        }

        #region DtgReportes
        private void dtgReportes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LimpiarForm();
            ControlesDesactivadosInicialmente(true);

            IdReporte = int.Parse(dtgReportes.CurrentRow.Cells[0].Value.ToString());
            txtNumeroFolio.Text = dtgReportes.CurrentRow.Cells[1].Value.ToString();
            dtpFecha.Value = DateTime.Parse(dtgReportes.CurrentRow.Cells[2].Value.ToString());
            cboModulos.SelectedItem = dtgReportes.CurrentRow.Cells[3].Value.ToString();
            cboClaves.SelectedItem = dtgReportes.CurrentRow.Cells[4].Value.ToString();
            rtxtServicio.Text = dtgReportes.CurrentRow.Cells[5].Value.ToString();

            MostrarPartesUsadas(txtNumeroFolio.Text);
        }
        #endregion


        #region MetodosLocales
        private void LimpiarForm()
        {
            foreach (Control c in grpDatosReporte.Controls)
            {
                if (c is TextBox || c is RichTextBox)
                {
                    c.Text = "";
                }
            }

            cboModulos.SelectedIndex = 0;

            //Validar que no este vacio
            if (cboClaves.Items.Count != 0)
            {
                cboClaves.SelectedIndex = 0;
            }
            

            dtpFecha.Value = DateTime.Today;
            txtNumeroFolio.Focus();
            IdReporte = 0;
        }

        private void LimpiarDatosPartes()
        {
            foreach (Control c in grpDatosPartes.Controls)
            {
                if (c is TextBox || c is RichTextBox)
                {
                    c.Text = "";
                }
            }

            cboPartesRicoh.SelectedIndex = 0;
            IdRegistro = 0;
        }
        #endregion

        #region BotonesPartesUsadas
        private void btnGuardarParte_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCamposVaciosPartesUsadas())
                    return;
                string ModeloImpresora = "MP-4002";
                string Mensaje;
                MovimientoParteRicoh nuevoMovimiento = new MovimientoParteRicoh()
                {
                    IdRegistro = IdRegistro,
                    IdTipoPersona = 295, //Speed Toner
                    IdParte = lgRegistroPartes.BuscarIdParte(cboPartesRicoh.SelectedItem.ToString(), ModeloImpresora),
                    IdMovimiento = 1,
                    Cantidad = int.Parse(txtCantidad.Text),
                    Fecha = dtpFecha.Value,
                    Folio = txtNumeroFolio.Text
                };

                Mensaje = lgRegistroPartes.AgregarRegistroInventario(nuevoMovimiento);
                MessageBox.Show(Mensaje, "REGISTRO INVENTARIO PARTES RICOH", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarDatosPartes();
                MostrarPartesUsadas(txtNumeroFolio.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron guardar los datos por: " + ex);
            }
        }

        private void btnEliminarParte_Click(object sender, EventArgs e)
        {
            string Mensaje;
            if (MessageBox.Show("¿Esta seguro de eliminar el registro?", "CONFIRME LA ELIMINACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                MessageBox.Show("!!Eliminacion cancelada!!", "CANCELADO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                LimpiarDatosPartes();
                return;
            }

            MovimientoParteRicoh ParteEliminada = new MovimientoParteRicoh()
            {
                IdRegistro = IdRegistro,
                IdParte = IdParte,
                IdMovimiento = 1,
                Cantidad = int.Parse(txtCantidad.Text)
            };
            Mensaje = lgRegistroPartes.EliminarRegistro(ParteEliminada);
            //Mostrar("MostrarRegistroInventarioPartesRicoh");
            MostrarPartesUsadas(txtNumeroFolio.Text);
            LimpiarDatosPartes();
        }

        private void btnCancelarParte_Click(object sender, EventArgs e)
        {
            LimpiarDatosPartes();

            ControlesDesactivadosEnPartes(false);
        }

        public void MostrarPartesUsadas(string NumeroFolio)
        {
            //Limpiamos los datos del datagridview
            dtgPartesUsadas.DataSource = null;
            dtgPartesUsadas.Refresh();
            DataTable tabla = new DataTable();
            //Guardamos los registros dependiendo la consulta
            tabla = lgRegistroPartes.MostrarPartes(NumeroFolio);
            //Asignamos los registros que optuvimos al datagridview
            dtgPartesUsadas.DataSource = tabla;
            dtgPartesUsadas.Columns["IdRegistro"].Visible = false;
        }
        private void dtgPartesUsadas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LimpiarDatosPartes();

            ControlesDesactivadosEnPartes(true);

            IdRegistro = int.Parse(dtgPartesUsadas.CurrentRow.Cells[0].Value.ToString());
            cboPartesRicoh.SelectedItem = dtgPartesUsadas.CurrentRow.Cells[1].Value.ToString();
            txtCantidad.Text = dtgPartesUsadas.CurrentRow.Cells[2].Value.ToString();
            IdParte = int.Parse(dtgPartesUsadas.CurrentRow.Cells[2].Value.ToString());
        }
        #endregion

    }
}
