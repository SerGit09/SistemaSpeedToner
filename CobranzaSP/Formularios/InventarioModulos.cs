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
    public partial class InventarioModulos : Form
    {
        public InventarioModulos()
        {
            InitializeComponent();
            InicioAplicacion();
        }
        //Saber si es inventario o en los movimientos de inventario
        bool Inventario = true;
        bool EstaModificando = false;

        //Guardara el id de un modulo seleccionado
        int Id = 0;
        AccionesLógica NuevaAccion = new AccionesLógica(); 
        CD_Conexion cn = new CD_Conexion();
        LogicaInventarioModulos lgInventarioModulo = new LogicaInventarioModulos();
        LogicaModulosBodega lgModuloBodega = new LogicaModulosBodega();
        FuncionesFormularios Formulario = new FuncionesFormularios();

        bool Validado;


        #region Inicio
        public void InicioAplicacion()
        {
            Formulario.LlenarComboBox(cboModelo, "spSeleccionarModelosModulos");

            MostrarDatosModulos();
            ControlesDesactivados(false);
            Formulario.PropiedadesDtg(dtgModulos);
        }

        public void ControlesDesactivados(bool Desactivado)
        {
            btnEliminar.Enabled = Desactivado;
            btnCancelar.Enabled = Desactivado;
        }

        public void MostrarDatosModulos()
        {
            //Limpiamos los datos del datagridview
            dtgModulos.DataSource = null;
            dtgModulos.Refresh();
            DataTable tabla = new DataTable();
            //Guardamos los registros dependiendo la consulta
            tabla = NuevaAccion.Mostrar("spMostrarModulosBodega");
            //Asignamos los registros que optuvimos al datagridview
            dtgModulos.DataSource = tabla;
            dtgModulos.Columns["Id"].Visible = false;
        }
        #endregion

        #region Validaciones
        public bool ValidarCamposCatalogo()
        {
            Validado = true;
            erInventarioModulos.Clear();

            ValidarCampo(cboModelo, "Seleccione un modelo");
            ValidarCampo(cboModulos, "Seleccione un modulo");
            ValidarCampo(txtClave, "Escriba una clave");

            return Validado;
        }

        public void ValidarCampo(Control c, string Mensaje)
        {
            if (c is TextBox || c is RichTextBox)
            {
                if (string.IsNullOrEmpty(c.Text))
                {
                    erInventarioModulos.SetError(c, Mensaje);
                    Validado = false;
                }
            }
            else if (c is ComboBox)
            {
                ComboBox combo = (ComboBox)c;
                if (combo.SelectedIndex == -1 || combo.SelectedIndex == 0)
                {
                    erInventarioModulos.SetError(c, Mensaje);
                    Validado = false;
                }
            }
        }
        #endregion

        #region Botones
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Inventario)
            {
                SeccionCatalogo();
            }
            else
            {
                //Seccion de registro de inventario
            }
        }

        #region Secciones
        public void SeccionCatalogo()
        {
            if (!ValidarCamposCatalogo())
                return;
            try
            {
                string Resultado;
                ModuloBodega NuevoModulo = new ModuloBodega()
                {
                    Id = Id,
                    IdModelo = NuevaAccion.BuscarId(cboModelo.SelectedItem.ToString(), "spObtenerIdModeloModulo"),
                    Clave = txtClave.Text
                };

                NuevoModulo.IdModulo = lgModuloBodega.BuscarIdModulo(cboModulos.SelectedItem.ToString(), NuevoModulo.IdModelo);
                if (EstaModificando)
                {
                    if (MessageBox.Show("¿Esta seguro de modificar el registro?", "CONFIRME LA MODIFICACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        MessageBox.Show("Modificacion cancelada!!", "CANCELADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else//Agregamos al catalogo
                {
                    bool ClaveRepetida = NuevaAccion.VerificarDuplicados(NuevoModulo.Clave, "VerificarClaveDuplicadaBodega");
                    if (ClaveRepetida)
                    {
                        MessageBox.Show("¡¡EL NUMERO DE FOLIO YA EXISTE!!", "DUPLICADO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                Resultado = lgModuloBodega.GuardarModulo(NuevoModulo);
                MessageBox.Show(Resultado, "REGISTRO INVENTARIO MODULOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarDatosModulos();//Actualizar modulos
                Reiniciar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error " + ex.Message);
            }
        }
        #endregion

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Reiniciar();
        }


        public void Reiniciar()
        {
            EstaModificando = false;
            ControlesDesactivados(false);
            LimpiarForm(grpDatosInventario);
            Id = 0;
            cboModelo.SelectedIndex = 0;
            cboModulos.SelectedIndex = 0;
        }

        private void btnImprimir_Click_1(object sender, EventArgs e)
        {
            lgInventarioModulo.ImprimirInventario();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Inventario)
            {
                EliminarModuloCatalogo();
            }
        }

        public void EliminarModuloCatalogo()
        {
            //Preguntamos si se esta seguro de eliminar el registro 
            if (MessageBox.Show("¿Esta seguro de eliminar el registro?", "CONFIRME LA ELIMINACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                MessageBox.Show("Elimacion cancelada!!", "CANCELADO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                LimpiarForm(grpDatosInventario);
                return;
            }

            try
            {
                NuevaAccion.Eliminar(Id, "spEliminarModuloABodega");
                MessageBox.Show("SE HA ACTUALIZADO EL INVENTARIO", "CLAVE ELIMINADA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarDatosModulos();
                Reiniciar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo eliminar la clave: " + ex, "OCURRIO UN PROBLEMA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion




        #region Eventos
        

        private void dtgModulos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            EstaModificando = true;
            ControlesDesactivados(true);
            SeleccionarDatos();
        }

        public void SeleccionarDatos()
        {
            if (Inventario)
            {
                LlenarDatosInventario();
            }
            else
            {

            }
        }

        public void LlenarDatosInventario()
        {
            EstaModificando = true;
            ControlesDesactivados(true);
            Id = int.Parse(dtgModulos.CurrentRow.Cells[0].Value.ToString());
            cboModelo.SelectedItem = dtgModulos.CurrentRow.Cells[1].Value.ToString();
            cboModulos.SelectedItem = dtgModulos.CurrentRow.Cells[2].Value.ToString();
            txtClave.Text = dtgModulos.CurrentRow.Cells[3].Value.ToString();
        }
        #endregion

        #region MetodosLocales
        private void LimpiarForm(GroupBox grp)
        {
            foreach (Control c in grp.Controls)
            {
                if (c is TextBox)
                {
                    c.Text = "";
                }
            }
        }
        #endregion



        private void cboModelo_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cboModelo.SelectedItem.ToString() != " ")
            {
                int IdModelo = NuevaAccion.BuscarId(cboModelo.SelectedItem.ToString(), "spObtenerIdModeloModulo");
                Formulario.LlenarComboBox(cboModulos, "spSeleccionarModulos", IdModelo);
            }
        }

        private void InventarioModulos_Load(object sender, EventArgs e)
        {

        }

        private void cboModulos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboMarcas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AbrirForm(object formNuevo)
        {
            //Declaramos la forma
            Form fh = formNuevo as Form;

            //Mostramos la forma 
            fh.Show();
        }

        private void btnGuardarInventario_Click(object sender, EventArgs e)
        {
            AbrirForm(new GuardarArchivoInventarioPartes(3));
        }

        private void btnAbrirInventariosPartes_Click(object sender, EventArgs e)
        {
            AbrirForm(new InventariosPartesGuardados(3));
        }

        private void cboModulos_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            
        }
    }
}
