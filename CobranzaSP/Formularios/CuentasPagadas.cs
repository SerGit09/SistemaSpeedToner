using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using CobranzaSP.Lógica;
using CobranzaSP.Modelos;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CobranzaSP.Formularios
{
    public partial class CuentasPagadas : Form
    {
        public CuentasPagadas()
        {
            InitializeComponent();
            InicioAplicacion();
        }

        CD_Conexion cn = new CD_Conexion();
        AccionesLógica NuevaAccion = new AccionesLógica();
        CuentasPagadasLogica AccionCuentasPagadas = new CuentasPagadasLogica();
        FuncionesFormularios Formulario = new FuncionesFormularios();
        int Id = 0;
        bool MesElegido = false;
        int Mes;
        int Year;

        #region Inicio
        public void InicioAplicacion()
        {
            PropiedadesDtgCobranza();
            MostrarDatosCuentasPagadas();
            ActualizarTotalCobrado();
            Meses();
            dtpAño.Format = DateTimePickerFormat.Custom;
            dtpAño.CustomFormat = "yyyy";
            dtpAño.ShowUpDown = true;
            //dtpAño.CustomFormat("yyyy");
            HabilitarBotones(false);

            Formulario.LlenarComboBox(cboTipoFactura, "SeleccionarTiposFacturas", 0);
        }

        public void PropiedadesDtgCobranza()
        {
            //Solo lectura
            dtgPagado.ReadOnly = true;

            //No agregar renglones
            dtgPagado.AllowUserToAddRows = false;

            //No borrar renglones
            dtgPagado.AllowUserToDeleteRows = false;

            //Ajustar automaticamente el ancho de las columnas
            dtgPagado.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //dtgServicios.AutoResizeColumns(DataGridViewAutoSizeColumnsMo‌​de.Fill);
            dtgPagado.AutoResizeColumns();
        }

        public void Meses()
        {
            cboMeses.Items.Add("Enero");
            cboMeses.Items.Add("Febrero");
            cboMeses.Items.Add("Marzo");
            cboMeses.Items.Add("Abril");
            cboMeses.Items.Add("Mayo");
            cboMeses.Items.Add("Junio");
            cboMeses.Items.Add("Julio");
            cboMeses.Items.Add("Agosto");
            cboMeses.Items.Add("Septiembre");
            cboMeses.Items.Add("Octubre");
            cboMeses.Items.Add("Noviembre");
            cboMeses.Items.Add("Diciembre");
        }

        public void MostrarDatosCuentasPagadas()
        {
            //Limpiamos los datos del datagridview
            dtgPagado.DataSource = null;
            dtgPagado.Refresh();
            DataTable tabla = new DataTable();
            //Guardamos los registros dependiendo la consulta
            tabla = NuevaAccion.Mostrar("MostrarCuentasPagadas");
            //Asignamos los registros que optuvimos al datagridview
            dtgPagado.DataSource = tabla;
        }

        public void MostrarDatosMesElegido(int Mes, int Año)
        {
            //Limpiamos los datos del datagridview
            dtgPagado.DataSource = null;
            dtgPagado.Refresh();
            DataTable tabla = new DataTable();
            //Guardamos los registros dependiendo la consulta
            tabla = AccionCuentasPagadas.MostrarCuentasPagadasMesEspecifico(Mes, Año);
            //Asignamos los registros que optuvimos al datagridview
            dtgPagado.DataSource = tabla;
            if (tabla.Rows.Count <= 0)
            {
                MessageBox.Show("No se encontraron datos sobre dicho mes", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarTotalCobrado()
        {
            //CHECAR CUANDO SE QUEDE EN 0
            lblCobrado.Text = AccionCuentasPagadas.ActualizarCobranzaMesEspecifico(DateTime.Now.Month, DateTime.Now.Year);
        }
        #endregion

        #region Botones
        private void btnMostrar_Click(object sender, EventArgs e)
        {
            //VALIDAR QUE DICHO MES CONTENGA REGISTROS
            Mes = cboMeses.SelectedIndex + 1;
            Year = int.Parse(dtpAño.Value.Year.ToString());
            MostrarDatosMesElegido(Mes, Year);
            lblCobrado.Text = AccionCuentasPagadas.ActualizarCobranzaMesEspecifico(Mes, Year);
            MesElegido= true;
        }

        private void btnBusqueda_Click(object sender, EventArgs e)
        {
            SqlDataReader dr;
            if (txtBusqueda.Text != "")
            {
                dr = NuevaAccion.Buscar(txtBusqueda.Text, "BuscarCuentaPagada");
                if (dr.Read())
                {
                    DatosCuentasPagadas datos = new DatosCuentasPagadas();
                    datos.txtFactura.Text = dr[0].ToString();
                    datos.txtCliente.Text = dr[1].ToString();
                    datos.txtPrecio.Text = dr[2].ToString();
                    datos.dtFechaFactura.Value = Convert.ToDateTime(dr[3].ToString());
                    datos.dtFechaDePago.Value = Convert.ToDateTime(dr[4].ToString());
                    datos.txtTipoPago.Text = dr[5].ToString();
                    datos.Show();
                    dr.Close();
                }
                else
                {
                    MessageBox.Show("El numero de factura no existe", "REGISTRO NO ENCONTRADO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dr.Close();
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                CuentaPagada nuevaCuentaPagada = new CuentaPagada()
                {
                    Id = Id,
                    FechaPago = dtpFechaPago.Value,
                    IdTipoFactura = NuevaAccion.BuscarId(cboTipoFactura.SelectedItem.ToString(), "BuscarIdTipoFactura")
                };

                if (MessageBox.Show("¿Esta seguro la fecha de pago?", "CONFIRME LA MODIFICACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    MessageBox.Show("Modificacion cancelada!!", "CANCELADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ReiniciarControles();
                    return;
                }

                AccionCuentasPagadas.ModificarCuentaPagada(nuevaCuentaPagada);
                MessageBox.Show("FACTURA MODIFICADA CORRECTAMENTE");
                ReiniciarControles();

                if (MesElegido)
                {
                    MostrarDatosMesElegido(Mes, Year);
                    lblCobrado.Text = AccionCuentasPagadas.ActualizarCobranzaMesEspecifico(Mes, Year);
                }
                else
                {
                    MostrarDatosCuentasPagadas();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region MetodosLocales
        public void ReiniciarControles()
        {
            HabilitarBotones(false);
            dtpFechaPago.Value = DateTime.Now;
            Id = 0;
            cboTipoFactura.Enabled = false;
        }

        public void HabilitarBotones(bool EstaHabilitado)
        {
            btnGuardar.Enabled = EstaHabilitado;
            btnEliminar.Enabled = EstaHabilitado;
            btnCancelar.Enabled = EstaHabilitado;
        }
        #endregion

        #region Eventos
        private void dtgPagado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            HabilitarBotones(true);
            cboTipoFactura.Enabled = true;
            Id = int.Parse(dtgPagado.CurrentRow.Cells[0].Value.ToString());
            dtpFechaPago.Value = DateTime.Parse(dtgPagado.CurrentRow.Cells[5].Value.ToString());
            cboTipoFactura.SelectedItem = dtgPagado.CurrentRow.Cells[7].Value.ToString();
        }
        #endregion

        private void btnReportes_Click(object sender, EventArgs e)
        {
            ReportesCuentasPagadas forma = new ReportesCuentasPagadas();
            forma.Show();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Esta seguro de eliminar la factura?", "CONFIRME LA ELIMINACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    MessageBox.Show("!!Eliminacion cancelada!!", "CANCELADO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    ReiniciarControles();
                    return;
                }
                NuevaAccion.Eliminar(Id, "EliminarFacturaPagada");
                MessageBox.Show("Se ha eliminado la factura correctamente", "ELIMINACION CONFIRMADA", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                MostrarDatosCuentasPagadas();
                //ActualizarTotalCobrado();
                ReiniciarControles();
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
    }
}
