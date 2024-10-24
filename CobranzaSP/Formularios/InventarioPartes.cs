using CobranzaSP.Lógica;
using CobranzaSP.Modelos;
using DocumentFormat.OpenXml.Office2010.Excel;
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
using DocumentFormat.OpenXml.Vml.Spreadsheet;


namespace CobranzaSP.Formularios
{
    public partial class InventarioPartes : Form
    {
        public InventarioPartes()
        {
            InitializeComponent();
            InicioAplicacion();
        }

        CD_Conexion cn = new CD_Conexion();
        FuncionesFormularios Formulario = new FuncionesFormularios();
        LogicaInventarioPartesRicoh lgInventarioPartes = new LogicaInventarioPartesRicoh();
        LogicaRegistroInventarioRicoh lgRegistroPartes = new LogicaRegistroInventarioRicoh();
        //Objeto que nos servirá para poder tener capturado en dado caso de una modificacion
        RegistroInventario RegistroAnterior;
        AccionesLógica NuevaAccion = new AccionesLógica();

        bool Inventario = true;
        bool EstaModificando = false;
        int Id = 0;
        //Servira para el momento de eliminar un registro de parte
        int IdParte = 0;
        int IdMovimiento = 2;
        bool BuscandoRegistro = false;
        bool Validado;

        #region Inicio

        public void InicioAplicacion()
        {
            Formulario.PropiedadesDtg(dtgInventario);
            Mostrar("spMostrarInventarioPartes");
            Formulario.LlenarComboBox(cboModelos, "spSeleccionarModelosPartes");
            Formulario.LlenarComboBox(cboModelo, "spSeleccionarModelosPartes");
            Formulario.LlenarComboBox(cboProveedores, "SeleccionarProveedores");
            ControlesDesactivados(false);
        }

        public void RecargarModelosPartes()
        {
            Formulario.LlenarComboBox(cboModelos, "spSeleccionarModelosPartes");
        }

        public void ControlesDesactivados(bool Desactivado)
        {
            btnEliminar.Enabled = Desactivado;
            btnCancelar.Enabled = Desactivado;
        }

        public void Mostrar(string sp)
        {
            //Limpiamos los datos del datagridview
            dtgInventario.DataSource = null;
            dtgInventario.Refresh();
            DataTable tabla = new DataTable();
            //Guardamos los registros dependiendo la consulta
            tabla = NuevaAccion.Mostrar(sp);
            //Asignamos los registros que optuvimos al datagridview
            dtgInventario.DataSource = tabla;
            if (Inventario)
            {
                dtgInventario.Columns["IdNumeroParte"].Visible = false;
                dtgInventario.Columns["UrlImagen"].Visible = false;
            }
            else
            {
                dtgInventario.Columns["IdRegistroParte"].Visible = false;
                dtgInventario.Columns["IdNumeroParte"].Visible = false;
            }
        }

        #endregion

        #region Validaciones
        public bool ValidarCamposInventario()
        {
            Validado = true;
            erInventarioPartesRicoh.Clear();

            ValidarCampo(cboModelos);
            ValidarCampo(txtNumeroParte);
            ValidarCampo(rtxtDescripcion);
            ValidarCampo(txtCantidadExistencia);

            //if(pcbParte.Tag == "")
            //{
            //    erInventarioPartesRicoh.SetError(pcbParte, "Elige una imagen");
            //}

            return Validado;
        }

        public bool ValidarCamposRegistro()
        {
            Validado = true;
            erInventarioPartesRicoh.Clear();

            ValidarCampo(cboModelo);
            ValidarCampo(cboPartesRicoh);
            ValidarCampo(txtFolio);
            ValidarCampo(cboProveedores);
            ValidarCampo(txtCantidad);

            return Validado;
        }

        public void ValidarCampo(Control c)
        {
            if (c is TextBox || c is RichTextBox)
            {
                if (string.IsNullOrEmpty(c.Text))
                {
                    erInventarioPartesRicoh.SetError(c, "Campo Obligatorio");
                    Validado = false;
                }
            }
            else if (c is ComboBox)
            {
                ComboBox combo = (ComboBox)c;
                if (combo.SelectedIndex == -1 || combo.SelectedIndex == 0)
                {
                    erInventarioPartesRicoh.SetError(c, "Seleccione una opcion");
                    Validado = false;
                }
            }
        }

        private void txtCantidadExistencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validacion.SoloNumeros(e);
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validacion.SoloNumeros(e);
        }
        #endregion

        #region Botones
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Inventario)
                {
                    CapturaAInventario();
                }
                else
                {
                    CapturaMovimientoDeInventario();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CapturaMovimientoDeInventario()
        {
            //FALTA VALIDAR CAMPOS
            if (!ValidarCamposRegistro())
            {
                return;
            }
            string Mensaje;
            MovimientoParteRicoh nuevoMovimiento = new MovimientoParteRicoh()
            {
                IdRegistro = Id,
                IdTipoPersona = NuevaAccion.BuscarIdEntidad(IdMovimiento, cboProveedores.SelectedItem.ToString(), "spObtenerIdEntidad"),
                IdParte = lgRegistroPartes.BuscarIdParte(cboPartesRicoh.SelectedItem.ToString(), cboModelo.SelectedItem.ToString()),
                IdMovimiento = IdMovimiento,
                Cantidad = int.Parse(txtCantidad.Text),
                Fecha = dtpFechaRegistro.Value,
                Folio = txtFolio.Text,
                UrlImagen = ""
            };

            if (EstaModificando)
            {
                if (MessageBox.Show("¿Esta seguro de modificar el registro?", "CONFIRME LA MODIFICACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    MessageBox.Show("!!Modificación cancelada!!", "CANCELADO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    LimpiarForm(grpDatosInventario);
                    return;
                }
                //Unicamente modifica el registro, sin alterar el inventario
                Mensaje = lgRegistroPartes.GuardarRegistro(nuevoMovimiento);
            }
            else
            {

                //Modificar inventario y agregar registro
                Mensaje = lgRegistroPartes.AgregarRegistroInventario(nuevoMovimiento);
            }
            MessageBox.Show(Mensaje, "REGISTRO INVENTARIO PARTES RICOH", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarForm(grpDatosRegistro);
            Mostrar("spMostrarRegistroInventarioPartes");
        }

        public void ColocarIdTipoPersona(MovimientoParteRicoh nuevoMovimiento)
        {
            //if (radSalida.Checked)
            //{
            //    nuevoMovimiento.IdTipoPersona = NuevaAccion.BuscarId(cboTipoPersona.SelectedItem.ToString(), "ObtenerIdCliente");
            //}
            //else
            //{
            //    nuevoMovimiento.IdTipoPersona = NuevaAccion.BuscarId(cboTipoPersona.SelectedItem.ToString(), "ObtenerIdProveedor");
            //    if (chkCliente.Checked)
            //    {
            //        nuevoMovimiento.IdTipoPersona = NuevaAccion.BuscarId(cboTipoPersona.SelectedItem.ToString(), "ObtenerIdCliente");
            //    }
            //}
        }

        public void CapturaAInventario()
        {
            if (!ValidarCamposInventario())
                return;
            if (pcbParte.ImageLocation == null)
            {
                pcbParte.ImageLocation = "";
            }
            ParteRicoh ParteNueva = new ParteRicoh()
            {
                IdParte = Id,
                NumeroParte = txtNumeroParte.Text,
                IdModelo = NuevaAccion.BuscarId(cboModelos.SelectedItem.ToString(), "spObtenerIdModeloPartes"),
                Cantidad = int.Parse(txtCantidadExistencia.Text),
                Descripcion = rtxtDescripcion.Text,
                UrlImagen = pcbParte.ImageLocation
            };


            if (EstaModificando)
            {
                //Modificar stop procedure de modificar, para que se pueda cambiar la fecha
                if (MessageBox.Show("¿Esta seguro de modificar el registro?", "CONFIRME LA MODIFICACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    MessageBox.Show("!!Modificación cancelada!!", "CANCELADO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    LimpiarForm(grpDatosInventario);
                    return;
                }
            }
            else
            {
                bool FolioRepetido = NuevaAccion.VerificarDuplicados(ParteNueva.NumeroParte, "VerificarNumeroParteDuplicada");
                if (FolioRepetido)
                {
                    MessageBox.Show("El numero de parte ya esta registrado en el inventario de partes!!", "DUPLICADO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LimpiarForm(grpDatosRegistro);
                    return;
                }

            }
            string Mensaje = lgInventarioPartes.GuardarParte(ParteNueva);
            MessageBox.Show(Mensaje, "MOVIMIENTO INVENTARIO PARTES RICOH", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Actualizar datos
            Mostrar("spMostrarInventarioPartes");
            LimpiarForm(grpDatosInventario);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string Mensaje = "";

            if (MessageBox.Show("¿Esta seguro de eliminar el registro?", "CONFIRME LA ELIMINACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                MessageBox.Show("!!Eliminacion cancelada!!", "CANCELADO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                LimpiarForm(grpDatosInventario);
                LimpiarForm(grpDatosRegistro);
                return;
            }

            if (Inventario)
            {
                Mensaje = NuevaAccion.Eliminar(Id, "spEliminarParte");
                Mostrar("spMostrarInventarioPartes");
                LimpiarForm(grpDatosInventario);
            }
            else
            {
                MovimientoParteRicoh ParteEliminada = new MovimientoParteRicoh()
                {
                    IdRegistro = Id,
                    IdParte = IdParte,
                    IdMovimiento = IdMovimiento,
                    Cantidad = int.Parse(txtCantidad.Text)
                };
                Mensaje = lgRegistroPartes.EliminarRegistro(ParteEliminada);
                //Mostrar("MostrarRegistroInventarioPartesRicoh");
                Mostrar("spMostrarRegistroInventarioPartes");
                LimpiarForm(grpDatosRegistro);
            }
            MessageBox.Show(Mensaje, "ELIMINACION DE REGISTRO", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LlenarProveedores();
            if (Inventario)
            {
                LimpiarForm(grpDatosInventario);
            }
            else
            {
                LimpiarForm(grpDatosRegistro);
            }
            IdParte = 0;
            Id = 0;

        }

        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            AbrirForm(new ReporteRegistroPartesRicoh());
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            bool FormatoBlanco = chkSoloPlantilla.Checked;
            lgInventarioPartes.ImprimirInventario(FormatoBlanco);
            chkSoloPlantilla.Checked = false;
        }
        #endregion

        #region Eventos
        private void dtgInventario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ControlesDesactivados(true);
            EstaModificando = true;
            if (Inventario)
            {
                LlenarCamposInventario();
            }
            else
            {
                LlenarCamposRegistroInventario();
            }
        }

        public void LlenarCamposInventario()
        {
            Id = int.Parse(dtgInventario.CurrentRow.Cells[0].Value.ToString());
            txtNumeroParte.Text = dtgInventario.CurrentRow.Cells[1].Value.ToString();
            cboModelos.SelectedItem = dtgInventario.CurrentRow.Cells[2].Value.ToString();
            rtxtDescripcion.Text = dtgInventario.CurrentRow.Cells[3].Value.ToString();
            txtCantidadExistencia.Text = dtgInventario.CurrentRow.Cells[4].Value.ToString();
            AbrirImagen(dtgInventario.CurrentRow.Cells[5].Value.ToString());
        }

        public void LlenarCamposRegistroInventario()
        {
            Id = int.Parse(dtgInventario.CurrentRow.Cells[0].Value.ToString());
            IdParte = int.Parse(dtgInventario.CurrentRow.Cells[1].Value.ToString());
            cboPartesRicoh.SelectedItem = dtgInventario.CurrentRow.Cells[2].Value.ToString();
            cboModelo.SelectedItem = dtgInventario.CurrentRow.Cells[3].Value.ToString();
            string TipoMovimiento = dtgInventario.CurrentRow.Cells[4].Value.ToString();
            LlenarComboBoxEntidad(TipoMovimiento);
            txtCantidad.Text = dtgInventario.CurrentRow.Cells[5].Value.ToString();
            cboProveedores.SelectedItem = dtgInventario.CurrentRow.Cells[6].Value.ToString();
            dtpFechaRegistro.Value = DateTime.Parse(dtgInventario.CurrentRow.Cells[7].Value.ToString());
            txtFolio.Text = dtgInventario.CurrentRow.Cells[8].Value.ToString();
        }

        public void LlenarComboBoxEntidad(string TipoMovimiento)
        {
            if (TipoMovimiento == "Entrada")
            {
                IdMovimiento = 2;
                LlenarProveedores();
            }
            else
            {
                IdMovimiento = 1;
                LlenarClientes();
            }
        }

        public void LlenarProveedores()
        {
            lblTipoPersona.Text = "Proveedor:";
            Formulario.LlenarComboBox(cboProveedores, "SeleccionarProveedores");
        }

        public void LlenarClientes()
        {
            lblTipoPersona.Text = "Cliente:";
            Formulario.LlenarComboBox(cboProveedores, "SeleccionarClientesServicios");
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabInventario)
            {
                Inventario = true;
                Mostrar("spMostrarInventarioPartes");
            }
            else
            {
                Inventario = false;
                Mostrar("spMostrarRegistroInventarioPartes");
            }
        }
        #endregion

        #region MetodosLocales
        private void LimpiarForm(GroupBox grp)
        {
            foreach (Control c in grp.Controls)
            {
                if (c is TextBox || c is RichTextBox)
                {
                    c.Text = "";
                }
            }
            cboModelo.SelectedIndex = -1;
            cboModelos.SelectedIndex = -1;
            cboProveedores.SelectedIndex = -1;
            cboPartesRicoh.SelectedIndex = -1;
            Id = 0;
            ControlesDesactivados(false);
            pcbParte.ImageLocation = "";
            EstaModificando = false;
            IdMovimiento = 2;
        }
        private void AbrirForm(object formNuevo)
        {
            //Declaramos la forma
            Form fh = formNuevo as Form;

            //Mostramos la forma 
            fh.Show();
        }
        #endregion


        private void grpDatosRegistro_Enter(object sender, EventArgs e)
        {

        }
        #region SeleccionarPartes
        private void cboModelo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Modelo = (cboModelo.SelectedIndex != -1) ? cboModelo.SelectedItem.ToString() : " ";
            if (Modelo != " ")
            {
                int IdModelo = NuevaAccion.BuscarId(Modelo, "spObtenerIdModeloPartesRicoh");
                Formulario.LlenarComboBox(cboPartesRicoh, "spSeleccionarPartes", IdModelo);
            }
        }

        private void btnNuevoModelo_Click(object sender, EventArgs e)
        {
            AbrirForm(new NuevoModeloParte(this));
        }



        #endregion

        private void cboModelos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnPruebaImagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrirImagen = new OpenFileDialog();

            if (abrirImagen.ShowDialog() == DialogResult.OK)
            {
                AbrirImagen(abrirImagen.FileName);
            }
        }

        public void AbrirImagen(string RutaImagen)
        {
            pcbParte.ImageLocation = RutaImagen;
            pcbParte.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void btnGuardarInventario_Click(object sender, EventArgs e)
        {
            AbrirForm(new GuardarArchivoInventarioPartes());

            //MessageBox.Show(NuevoInventarioPartes.IdInventarioPartes + "" + NuevoInventarioPartes.Fecha + "\n" + NuevoInventarioPartes.UrlArchivo);
        }

        private void btnAbrirInventariosPartes_Click(object sender, EventArgs e)
        {
            AbrirForm(new InventariosPartesGuardados());
        }
    }
}
