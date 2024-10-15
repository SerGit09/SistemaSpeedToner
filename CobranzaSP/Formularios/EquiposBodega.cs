using CobranzaSP.Lógica;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CobranzaSP.Modelos;
using System.Windows.Forms.DataVisualization.Charting;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using DocumentFormat.OpenXml.Bibliography;
using System.Reflection;
using System.Runtime.Remoting.Messaging;

namespace CobranzaSP.Formularios
{
    public partial class EquiposBodega : Form
    {
        public EquiposBodega()
        {
            InitializeComponent();
            InicioAplicacion();
        }
        AccionesLógica nuevaAccion = new AccionesLógica();
        CD_Conexion cn = new CD_Conexion();
        LogicaEquiposBodega lgEquipoBodega = new LogicaEquiposBodega();
        LogicaServicio lgServicio = new LogicaServicio();
        FuncionesFormularios Formulario = new FuncionesFormularios();
        ReporteEquipo nuevoEquipoRentado;
        int IdEquipo = 0;
        int IdSerie = 0;
        bool EstaModificando = false;
        bool BuscandoFolio = false;
        string TipoBusqueda = "";

        bool Validado;
        #region Inicio

        public void InicioAplicacion()
        {
            ControlesDesactivadosInicialmente(false);

            //Llenar comboboxs
            Formulario.LlenarComboBox(cboMarcas, "SeleccionarMarca");
            Formulario.LlenarComboBox(cboModelos, "SeleccionarModelos");

            //Porpiedades de DTG y llenado
            Formulario.PropiedadesDtg(dtgEquipos);
            MostrarDatosEquipos();

            //Deshabilitamos escritura en combobox
            cboBusqueda.DropDownStyle = ComboBoxStyle.DropDownList;

            //Radio Buttons Seleccionados inicialmente
            rdTodasLasMarcas.Checked = true;
            rdTodosLosModelos.Checked = true;
            rdNueva.Checked = true;
        }

        private void ControlesDesactivadosInicialmente(bool activado)
        {
            btnCancelar.Enabled = activado;
            btnEliminar.Enabled = activado;
            btnRentada.Enabled = activado;
            btnVentaEquipo.Enabled = activado;
        }


        public void MostrarDatosEquipos()
        {
            //Limpiamos los datos del datagridview
            dtgEquipos.DataSource = null;
            dtgEquipos.Refresh();
            DataTable tabla = new DataTable();
            //Guardamos los registros dependiendo la consulta
            tabla = nuevaAccion.Mostrar("MostrarEquiposBodega");
            //Asignamos los registros que optuvimos al datagridview
            dtgEquipos.DataSource = tabla;
            dtgEquipos.Columns["IdEquipo"].Visible = false;
        }

        #endregion

        #region Validaciones

        public bool ValidarCampos()
        {
            Validado = true;
            erEquipos.Clear();

            ValidarCampo(cboMarcas);
            ValidarCampo(txtSerie);
            ValidarCampo(txtPrecio);

            if (chkNuevoModelo.Checked)
            {
                ValidarCampo(txtNuevoModelo);
            }
            else
            {
                ValidarCampo(cboModelos);
            }
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
        public bool ValidarCamposReporte()
        {
            Validado = true;
            erEquipos.Clear();
            if (!rdTodasLasMarcas.Checked)
            {
                if (rdUnaMarca.Checked)
                {
                    ValidarCampo(cboBusqueda);
                }
                if (radUnModelo.Checked)
                {
                    ValidarCampo(cboModelo);
                }
            }
            return Validado;
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == '.')
            {
                // Verificar si ya hay un punto en el texto
                if (txtPrecio.Text.Contains("."))
                {
                    // Ignorar el ingreso adicional de puntos
                    e.Handled = true;
                }   
            }
            else
            {
                // Permitir el ingreso de otras teclas
                Validacion.SoloNumeros(e);
            }
        }
        #endregion


        #region Botones

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string Mensaje;
            bool SerieRepetida;
            try
            {
                if (!ValidarCampos())
                    return;

                NuevaSerie();

                EquipoBodega NuevoEquipo = new EquipoBodega()
                {
                    IdEquipo = IdEquipo,
                    IdMarca = nuevaAccion.BuscarId(cboMarcas.SelectedItem.ToString(), "ObtenerIdMarca"),
                    IdSerie = nuevaAccion.BuscarId(txtSerie.Text, "ObtenerIdSerie"),
                    Serie = txtSerie.Text,
                    Precio = double.Parse(txtPrecio.Text),
                    Notas = rtxtNotas.Text,
                    FechaDeLlegada = dtpFechaLlegada.Value
                };
                //Verificar si agregamos un nuevo modelo o uno ya existente
                VerificarNuevoModelo(NuevoEquipo);
                NuevoEquipo.Estado = (rdNueva.Checked) ? "Nueva" : "Usada";
                if (EstaModificando)
                {
                    if (MessageBox.Show("¿Esta seguro de modificar el registro?", "CONFIRME LA MODIFICACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        MessageBox.Show("Modificacion cancelada!!", "CANCELADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarForm();
                        return;
                    }
                }
                else
                {
                    //Validar que no se repita una serie
                    SerieRepetida = nuevaAccion.VerificarDuplicados(NuevoEquipo.Serie, "VerificarSerieEquiposBodega");
                    if (SerieRepetida)
                    {
                        MessageBox.Show("Ingrese un numero de serie distinto", "SERIE YA EXISTENTE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LimpiarForm();
                        return;
                    }
                }

                Mensaje = lgEquipoBodega.GuardarEquipo(NuevoEquipo);
                ReiniciarControles();
                MessageBox.Show(Mensaje, "REGISTRO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarForm();
                MostrarDatosEquipos();

            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron guardar los datos por: " + ex);
            }
        }

        public void NuevaSerie()
        {
            LogicaServicio lgServicio = new LogicaServicio();
            string SerieImpresora = txtSerie.Text;
            lgServicio.AñadirSerie(SerieImpresora);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Preguntamos si se esta seguro de eliminar el registro 
            if (MessageBox.Show("¿Esta seguro de eliminar el registro?", "CONFIRME LA ELIMINACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                MessageBox.Show("Elimacion cancelada!!", "CANCELADO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                ReiniciarControles();
                return;
            }

            try
            {
                nuevaAccion.Eliminar(IdEquipo, "EliminarEquipoBodega");
                MessageBox.Show("Se elimino el registro", "OPERACION EXITOSA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarDatosEquipos();
                ReiniciarControles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo eliminar el registro: " + ex, "OCURRIO UN PROBLEMA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ReiniciarControles();
        }

        public void ReiniciarControles()
        {
            ControlesDesactivadosInicialmente(false);
            LimpiarForm();
            EstaModificando = false;
            IdEquipo = 0;
        }

        private void btnRentada_Click(object sender, EventArgs e)
        {
            DatosImpresoraRentada forma = new DatosImpresoraRentada(this, nuevoEquipoRentado, IdEquipo);
            forma.Show();
        }

        private void btnVentaEquipo_Click(object sender, EventArgs e)
        {
            DatosEquipoVendido forma = new DatosEquipoVendido(nuevoEquipoRentado, IdEquipo, this);
            forma.Show();
        }
        #endregion


        #region Eventos
        private void cboMarcas_SelectedIndexChanged(object sender, EventArgs e)
        {
            //En dado caso que se haya seleccionado algo de las marcas y mientras no estemos buscando un registro en especifico
            if (cboMarcas.SelectedItem.ToString() != " " && BuscandoFolio == false)
            {
                int IdMarca = nuevaAccion.BuscarId(cboMarcas.SelectedItem.ToString(), "ObtenerIdMarca");
                Formulario.LlenarComboBox(cboModelos, "SeleccionarModelos", IdMarca);
            }
        }

        private void dtgEquipos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LimpiarForm();
            //Una vez que se escoga alguna fila podremos activar los botones para poder modificar y eliminar
            ControlesDesactivadosInicialmente(true);
            EstaModificando = true;

            //Asignacion a los controles
            IdEquipo = int.Parse(dtgEquipos.CurrentRow.Cells[0].Value.ToString());
            cboMarcas.SelectedItem = dtgEquipos.CurrentRow.Cells[1].Value.ToString();
            cboModelos.SelectedItem = dtgEquipos.CurrentRow.Cells[2].Value.ToString();
            txtSerie.Text = dtgEquipos.CurrentRow.Cells[3].Value.ToString();
            txtPrecio.Text = dtgEquipos.CurrentRow.Cells[4].Value.ToString().Replace("$", "");
            string Estado = dtgEquipos.CurrentRow.Cells[5].Value.ToString();
            dtpFechaLlegada.Value = DateTime.Parse(dtgEquipos.CurrentRow.Cells[7].Value.ToString());
            IdSerie = int.Parse(dtgEquipos.CurrentRow.Cells[8].Value.ToString());


            nuevoEquipoRentado = new ReporteEquipo()
            {
                Marca = dtgEquipos.CurrentRow.Cells[1].Value.ToString(),
                Modelo = dtgEquipos.CurrentRow.Cells[2].Value.ToString(),
                Serie = txtSerie.Text,
                Precio = double.Parse(txtPrecio.Text),
                IdSerie = IdSerie
            };

            rdNueva.Checked = (Estado == "Nueva") ? true : false;

            rtxtNotas.Text = dtgEquipos.CurrentRow.Cells[6].Value.ToString();
        }
        #endregion

        #region MetodosLocales
        public void LimpiarForm()
        {
            foreach (Control c in grpDatos.Controls)
            {
                if (c is TextBox || c is RichTextBox)
                {
                    c.Text = "";
                }
            }
            cboMarcas.SelectedIndex = 0;
            cboModelos.SelectedIndex = 0;
        }
        #endregion

        #region Reporte
        private void btnMostrar_Click(object sender, EventArgs e)
        {
            string Parametro = "";
            //PENDIENTE EL PDF AUN
            bool DatosEncontrados = true;

            if (!ValidarCamposReporte())
            {
                return;
            }

            Reporte NuevoReporte = new Reporte()
            {
                TipoBusqueda = "Marca",
                ParametroBusqueda = ""
            };
            DeterminarParametroDeBusqueda(NuevoReporte);
            DatosEncontrados = lgEquipoBodega.ObtenerDatosEquiposBodega(NuevoReporte);

            if (!DatosEncontrados)
            {
                MessageBox.Show("¡NO SE ENCONTRARON EQUIPOS EN BODEGA!", "EQUIPOS NO ENCONTRADOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DeterminarParametroDeBusqueda(Reporte NuevoReporte)
        {
            if (rdTodasLasMarcas.Checked)
            {
                return;
            }

            if (rdUnaMarca.Checked)
            {
                NuevoReporte.ParametroBusqueda = cboBusqueda.SelectedItem.ToString();
            }

            if (radUnModelo.Checked)
            {
                NuevoReporte.TipoBusqueda = "Modelo";
                NuevoReporte.ParametroBusqueda = cboModelo.SelectedItem.ToString();
            }
        }
        private void rdUnaMarca_CheckedChanged(object sender, EventArgs e)
        {
            cboBusqueda.Visible = true;
            Formulario.LlenarComboBox(cboBusqueda, "SeleccionarMarca");
        }

        private void rdTodasLasMarcas_CheckedChanged(object sender, EventArgs e)
        {
            grpModelo.Visible = false;
            cboBusqueda.Visible = false;
        }

        private void cboBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {
            rdTodosLosModelos.Checked = true;
            if (cboBusqueda.SelectedItem.ToString() != " ")
            {
                int IdMarca = nuevaAccion.BuscarId(cboBusqueda.SelectedItem.ToString(), "ObtenerIdMarca");
                Formulario.LlenarComboBox(cboModelo, "SeleccionarModelos", IdMarca);
                grpModelo.Visible = true;
            }
            else
            {
                grpModelo.Visible = false;
            }
        }

        private void radUnModelo_CheckedChanged(object sender, EventArgs e)
        {
            if (radUnModelo.Checked)
            {
                cboModelo.Visible = true;
            }
            else
            {
                cboModelo.Visible = false;
            }
        }
        #endregion

        #region NuevoModelo
        private void chkNuevoModelo_CheckedChanged(object sender, EventArgs e)
        {
            txtNuevoModelo.Visible = chkNuevoModelo.Checked;
            cboModelos.Visible = !chkNuevoModelo.Checked;
        }

        public void VerificarNuevoModelo(EquipoBodega NuevoEquipo)
        {
            if (chkNuevoModelo.Checked)
            {
                lgServicio.AñadirModelo(txtNuevoModelo.Text, NuevoEquipo.IdMarca);
                NuevoEquipo.IdModelo = nuevaAccion.BuscarId(txtNuevoModelo.Text, "ObtenerIdModelo");
            }
            else
            {
                NuevoEquipo.IdModelo = nuevaAccion.BuscarId(cboModelos.SelectedItem.ToString(), "ObtenerIdModelo");
            }
        }
        #endregion



        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
