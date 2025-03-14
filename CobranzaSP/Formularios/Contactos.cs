using System;
using CobranzaSP.Lógica;
using CobranzaSP.Modelos;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Vml.Spreadsheet;

namespace CobranzaSP.Formularios
{
    public partial class Contactos : Form
    {
        public Contactos()
        {
            InitializeComponent();
            InicioAplicacion();
        }
        CD_Conexion cn = new CD_Conexion();
        AccionesLógica NuevaAccion = new AccionesLógica();
        ContactoLogica nuevoContactoLogica = new ContactoLogica();
        SqlCommand comando = new SqlCommand();
        bool EstaModificando = false;
        bool EsCliente = true;
        bool Validado = true;
        int IdCliente = 0;
        int IdCorreo = 0;
        FuncionesFormularios Formulario = new FuncionesFormularios();

        #region Inicio
        public void InicioAplicacion()
        {
            BotonesDesactivados(false);
            MostrarClientesAdeudos();

            Formulario.PropiedadesDtg(dtgCobranza);
            Formulario.PropiedadesDtg(dtgContactos);
            MostrarDatosCobranza();
            Formulario.LlenarComboBox(cboClientes, "SeleccionarClientes");
            //Formulario.LlenarComboBox(cboClientes, "SeleccionarClientesServicios");
        }

        public void BotonesDesactivados(bool activado)
        {
            btnCancelar.Enabled = activado;
            btnEliminar.Enabled = activado;
        }

        public void MostrarDatosCobranza()
        {
            //Limpiamos los datos del datagridview
            dtgCobranza.DataSource = null;
            dtgCobranza.Refresh();
            DataTable tabla = new DataTable();
            //Guardamos los registros dependiendo la consulta
            tabla = NuevaAccion.Mostrar("MostrarClientes");
            //Asignamos los registros que optuvimos al datagridview
            dtgCobranza.DataSource = tabla;
            dtgCobranza.Columns["IdCliente"].Visible = false;
        }

        //Mostrar clientes a los cuales se les enviara correo
        public void MostrarClientesAdeudos()
        {
            SqlDataReader leer;
            comando.Connection = cn.AbrirConexion();
            comando.CommandText = "CuentasAdeudas";
            comando.CommandType = CommandType.StoredProcedure;

            leer = comando.ExecuteReader();
            while (leer.Read())
            {
                lstListaCorreos.Items.Add(leer[0].ToString());
            }
        }
        #endregion

        #region Validaciones
        public bool ValidarDatos()
        {
            Validado = true;
            erContacto.Clear();

            if (EsCliente)
            {
                ValidarCampo(txtCliente, "Escriba el cliente");
                ValidarCampo(txtDiasCredito, "Escriba los dias de credito");
            }
            else
            {
                ValidarCampo(cboClientes, "Seleccione un cliente");
                ValidarCampo(txtCorreo, "Escriba el correo");
            }


            return Validado;
        }

        public void ValidarCampo(Control c, string Mensaje)
        {
            if (c is TextBox || c is RichTextBox)
            {
                if (string.IsNullOrEmpty(c.Text))
                {
                    erContacto.SetError(c, Mensaje);
                    Validado = false;
                }
            }
            else if (c is ComboBox)
            {
                ComboBox combo = (ComboBox)c;
                if (combo.SelectedIndex == -1 || combo.SelectedIndex == 0)
                {
                    erContacto.SetError(c, Mensaje);
                    Validado = false;
                }
            }
        }
        #endregion

        #region Botones
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarDatos())
            {
                return;
            }

            if (EsCliente)
            {
                GuardarCliente();
            }
            else
            {
                GuardarCorreo();
            }
            Reiniciar();
        }

        public void GuardarCliente()
        {
            Contacto NuevoCliente = new Contacto()
            {
                IdCliente = this.IdCliente,
                Nombre = txtCliente.Text,
                DiasCredito = int.Parse(txtDiasCredito.Text)
            };

            if (EstaModificando)
            {
                if (MessageBox.Show("¿Esta seguro de modificar el cliente?", "CONFIRME LA MODIFICACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    MessageBox.Show("!!Modificación cancelada!!", "OPERACION CANCELADA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarForm();
                    return;
                }
            }
            MessageBox.Show(nuevoContactoLogica.AgregarCliente(NuevoCliente), "AGREGANDO NUEVO CONTACTO", MessageBoxButtons.OK, MessageBoxIcon.Information);

            MostrarDatosCobranza();
            LimpiarForm();
        }

        public void GuardarCorreo()
        {
            Contacto NuevoCorreo = new Contacto()
            {
                IdCorreo = this.IdCorreo,
                IdCliente = NuevaAccion.BuscarId(cboClientes.SelectedItem.ToString(), "BuscarIdCliente"),
                //IdCliente = NuevaAccion.BuscarId(cboClientes.SelectedItem.ToString(), "ObtenerIdCliente"),
                Correo = txtCorreo.Text
            };

            if (EstaModificando)
            {
                if (MessageBox.Show("¿Esta seguro de modificar el correo?", "CONFIRME LA MODIFICACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    MessageBox.Show("!!Modificación cancelada!!", "OPERACION CANCELADA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarForm();
                    return;
                }
            }
            MessageBox.Show(nuevoContactoLogica.AgregarCorreo(NuevoCorreo), "AGREGANDO NUEVO CONTACTO", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (EstaModificando)
            {
                MostrarContactos();
            }
            else
            {
                dtgContactos.DataSource = null;
            }
            LimpiarForm();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string StoreProcedure = (EsCliente) ? "EliminarCliente": "EliminarCorreo";
            int Id = (EsCliente) ? IdCliente : IdCorreo;
            try
            {
                if (MessageBox.Show("¿Esta seguro de eliminar el contacto?", "CONFIRME LA ELIMINACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    MessageBox.Show("!!Eliminacion cancelada!!", "OPERACION CANCELADA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarForm();
                    return;
                }
                //MessageBox.Show(StoreProcedure + "\n" + Id);
                MessageBox.Show(NuevaAccion.Eliminar(Id, StoreProcedure));

                if (EsCliente)
                {
                    MostrarDatosCobranza();
                    dtgContactos.DataSource = null;
                }
                else
                {
                    MostrarContactos();
                }
                Reiniciar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Reiniciar()
        {
            EstaModificando = false;
            LimpiarForm();
            BotonesDesactivados(false);

            IdCliente = 0;
            IdCorreo = 0;
            chkAgregarCorreo.Checked = false;
            EsCliente = true;
            dtgContactos.DataSource = null;
            lblCliente.Visible = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Reiniciar();
            MostrarCampos(false);
        }
        #endregion


        public void LimpiarForm()
        {
            foreach (Control c in grpDatos.Controls)
            {
                if (c is TextBox || c is RichTextBox)
                {
                    c.Text = "";
                }
            }
            cboClientes.SelectedIndex = 0;
        }

        #region Eventos
        private void dtgCobranza_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            EstaModificando = true;
            BotonesDesactivados(true);
            IdCliente = int.Parse(dtgCobranza.CurrentRow.Cells[0].Value.ToString());
            txtCliente.Text = dtgCobranza.CurrentRow.Cells[1].Value.ToString();
            txtDiasCredito.Text = dtgCobranza.CurrentRow.Cells[02].Value.ToString();

            //Mostrar correos del cliente seleccionado
            MostrarContactos();

            MostrarCampos(false);
            EsCliente = true;
            lblCliente.Visible = true;
        }


        public void MostrarContactos()
        {
            DataTable tabla = new DataTable();
            tabla = nuevoContactoLogica.MostrarContactos(IdCliente);

            //Asignamos los registros que optuvimos al datagridview
            dtgContactos.DataSource = tabla;

            //Ocultar columnas
            dtgContactos.Columns["Id"].Visible = false;
        }
        #endregion


        private void chkAgregarCorreo_CheckedChanged(object sender, EventArgs e)
        {
            bool AgregandoCorreo = chkAgregarCorreo.Checked;
            txtCorreo.Text = "";
            MostrarCampos(AgregandoCorreo);
            EstaModificando = false;
            EsCliente = !AgregandoCorreo;
        }

        public void MostrarCampos(bool Mostrar)
        {
            //Habilitar o deshabilitar controles para agregar correo
            cboClientes.Visible = Mostrar;
            txtCorreo.Visible = Mostrar;
            lblCorreo.Visible = Mostrar;

            //Habilitar o deshabilitar controles para agregar cliente
            txtCliente.Visible = !Mostrar;
            lblDias.Visible = !Mostrar;
            txtDiasCredito.Visible = !Mostrar;
        }

        private void dtgContactos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            EstaModificando = true;
            lblCliente.Visible = false;
            IdCorreo = int.Parse(dtgContactos.CurrentRow.Cells[0].Value.ToString());
            txtCorreo.Text = dtgContactos.CurrentRow.Cells[1].Value.ToString();
            MostrarCampos(true);

            lblCliente.Visible = false;
            cboClientes.Visible = false;
            chkAgregarCorreo.Checked = false;

            //Esta modificando un correo no un cliente
            EsCliente = false;
        }
    }
}
