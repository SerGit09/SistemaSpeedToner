using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using CobranzaSP.Lógica;
using CobranzaSP.Modelos;
using DocumentFormat.OpenXml.Vml.Spreadsheet;

namespace CobranzaSP.Formularios
{
    public partial class Inventario : Form
    {
        public Inventario()
        {
            InitializeComponent();
            InicioAplicacion();
        }
        AccionesLógica NuevaAccion = new AccionesLógica();
        CD_Conexion cn = new CD_Conexion();
        LogicaInventario AccionInventario = new LogicaInventario();
        LogicaRegistro AccionRegistro = new LogicaRegistro();
        AccionesLógica nuevaAccion = new AccionesLógica();
        FuncionesFormularios Formulario = new FuncionesFormularios();
        //Objeto que nos servirá para poder tener capturado en dado caso de una modificacion
        RegistroInventario RegistroAnterior;
        bool inventario = true;
        bool EstaModificando = false;
        int Id = 0;
        bool BuscandoRegistro = false;
        bool Validado;

        private void tabRegistros_Click(object sender, EventArgs e)
        {

        }

        #region Inicio
        public void InicioAplicacion()
        {
            Formulario.PropiedadesDtg(dtgCartuchos);

            //Metodo que controla los controles que seran utiles al iniciar la aplicacion
            ControlesDesactivados(false);

            //Agregamos opciones que estan en la base de datos
            Formulario.LlenarComboBox(cboClientes, "SeleccionarClientesServicios");
            
            //LlenarComboBox(cboModelos, "SeleccionarCartuchos", 0);
            Formulario.LlenarComboBox(cboMarca, "SeleccionarMarca");
            Formulario.LlenarComboBox(cboMarcas, "SeleccionarMarca");
            Formulario.LlenarComboBox(cboModelos, "SeleccionarCartuchos");
            Formulario.LlenarComboBox(cboFusor, "SeleccionarFusores");

            //Denegar escritura en combobox
            cboModelos.DropDownStyle = ComboBoxStyle.DropDownList;
            cboClientes.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMarcaSeleccionada.DropDownStyle = ComboBoxStyle.DropDownList;

            Mostrar("MostrarInventario");

            //Configuramos los DateTimePicker
            //dtpFechaInicio.MaxDate = DateTime.Today;
            //dtpFechaFinal.MaxDate = DateTime.Today;
            dtpFecha.MaxDate = DateTime.Now;
            dtpFechaRegistro.MaxDate = DateTime.Now;
            LlenarCboMarcaSeleccionada();
            LlenarOpcionesMostrarRegistros();
            //LlenarCboFechaRegistros();
            radEntrada.Checked = true;
        }

        public void LlenarCboMarcaSeleccionada()
        {
            string[] Marcas = { "", "Todos", "Hp", "Brother" ,"Canon","Xerox", "Ricoh", "Samsung"};
            cboMarcaSeleccionada.Items.AddRange(Marcas);
        }

        public void LlenarOpcionesMostrarRegistros()
        {
            string[] OpcionesMostrar = { "", "Ultima Semana", "Mes actual", "Mes Pasado", "Este año"};
            cboMostrarRegistros.Items.AddRange(OpcionesMostrar);
        }
        public void ControlesDesactivados(bool Desactivado)
        {
            btnEliminar.Enabled = Desactivado;
            btnCancelar.Enabled = Desactivado;
        }

        public void Mostrar(string sp)
        {
            //Limpiamos los datos del datagridview
            dtgCartuchos.DataSource = null;
            dtgCartuchos.Refresh();
            DataTable tabla = new DataTable();
            //Guardamos los registros dependiendo la consulta
            tabla = NuevaAccion.Mostrar(sp);
            //Asignamos los registros que optuvimos al datagridview
            dtgCartuchos.DataSource = tabla;
            if (inventario)
            {
                dtgCartuchos.Columns["Id"].Visible = false;
            }
            else
            {
                dtgCartuchos.Columns["IdRegistro"].Visible = false;
            }

        }

        #endregion

        #region Validaciones
        private bool ValidarCamposInventario()
        {
            bool Validado = true;
            erInventario.Clear();
            ValidarCampo(txtModelo, "Ingrese el modelo");
            ValidarCampo(txtOficina, "Ingrese cantidad en oficina");
            ValidarCampo(cboMarca, "Seleccione una marca");

            return Validado;
        }

        private bool ValidarRegistros()
        {
            bool Validado = true;
            erInventario.Clear();

            ValidarCampo(txtCantidad, "Ingrese una cantidad");
            ValidarCampo(cboModelos, "Seleccione un modelo");
            ValidarCampo(cboMarcas, "Seleccione una marca");
            ValidarCampo(cboClientes, "Seleccione un cliente");

            return Validado;
        }

        public void ValidarCampo(Control c, string Mensaje)
        {
            if (c is TextBox || c is RichTextBox)
            {
                if (string.IsNullOrEmpty(c.Text))
                {
                    erInventario.SetError(c, Mensaje);
                    Validado = false;
                }
            }
            else if (c is ComboBox)
            {
                ComboBox combo = (ComboBox)c;
                if (combo.SelectedIndex == -1 || combo.SelectedIndex == 0)
                {
                    erInventario.SetError(c, Mensaje);
                    Validado = false;
                }
            }
        }

        private void txtModelo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Validacion.SoloLetrasYNumeros(e);
        }

        private void txtOficina_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validacion.SoloNumeros(e);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validacion.SoloLetrasYNumeros(e);
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
                //Entrara en dado caso que nos encontremos en el inventario
                if (inventario)
                {
                    GuardarTonerEnInventario();
                }
                else//Estamos en los registros
                {
                    SeccionRegistro();
                }
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
                if (inventario)
                {

                    EliminarTonerDelInventario();
                }
                else
                {
                    EliminarRegistroInventario();
                }
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

        #region SeccionInventario
        public void GuardarTonerEnInventario()
        {
            string Mensaje;
            if (!ValidarCamposInventario())
                return;
            InventarioDatos nuevoCartucho = new InventarioDatos()
            {
                Id = Id,
                Modelo = txtModelo.Text,
                IdMarca = NuevaAccion.BuscarId(cboMarca.SelectedItem.ToString(), "ObtenerIdMarca"),
                CantidadOficina = int.Parse(txtOficina.Text),
                Fecha = dtpFecha.Value,
                Precio = double.Parse(txtPrecio.Text)
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
                bool ModeloDuplicado = NuevaAccion.VerificarDuplicados(nuevoCartucho.Modelo, "VerificarModeloExistente");
                if (ModeloDuplicado)
                {
                    MessageBox.Show("Ingrese un modelo distinto", "EL MODELO YA EXISTE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LimpiarForm(grpDatosInventario);
                    return;
                }
            }
            
            Mensaje = AccionInventario.RegistrarInventario(nuevoCartucho);
            MessageBox.Show(Mensaje, "REGISTRO INVENTARIO TONERS", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //ESTA LA USO EN MI BASE DE DATOS
            Mostrar("MostrarInventario");

            LimpiarForm(grpDatosInventario);

        }
        public void EliminarTonerDelInventario()
        {
            if (MessageBox.Show("¿Esta seguro de eliminar el toner del inventario?", "CONFIRMAR ELIMINACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                MessageBox.Show("!!Eliminación cancelada!!", "CANCELADO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                LimpiarForm(grpDatosRegistro);
                return;
            }
            if (inventario)
            {
                NuevaAccion.Eliminar(Id, "EliminarCartuchoInventario");
                MessageBox.Show("Registro eliminado correctamente", "ELIMINACION EXITOSA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Mostrar("MostrarInventario");
                LimpiarForm(grpDatosInventario);
            }
        }
        #endregion

        #region SeccionRegistroInventario
        public void SeccionRegistro()
        {
            if (!ValidarRegistros())
                return;

            int IdMarcaEncontrada = NuevaAccion.BuscarId(cboMarcas.SelectedItem.ToString(), "ObtenerIdMarca");
            RegistroInventario nuevoRegistro = new RegistroInventario()
            {
                Id = Id,
                Cliente = cboClientes.SelectedItem.ToString(),
                IdMarca = IdMarcaEncontrada,
                IdCartucho = NuevaAccion.BuscarId(cboModelos.SelectedItem.ToString(), "ObtenerIdCartucho"),
                Fecha = dtpFechaRegistro.Value
            };
            ColocarCantidadesMovimiento(nuevoRegistro);

            //Verificamos que la clave del fusor este en los fusores o si esta vacio
            if (ComprobarNumeroSerie())
            {
                nuevoRegistro.NumeroSerie = cboFusor.SelectedItem.ToString();
            }


            if (EstaModificando)
            {
                if (MessageBox.Show("¿Esta seguro de modificar el registro?", "CONFIRME LA MODIFICACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    MessageBox.Show("!!Modificación cancelada!!", "CANCELADO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    LimpiarForm(grpDatosRegistro);
                    return;
                }

                //En dado caso de que sean diferentes quiere decir que cambio el modelo o la marca, por lo que se tiene que modificar
                //tambien ese registro anterior que se tenia
                bool CantidadesModificadas = RegistroAnterior.IdMarca != nuevoRegistro.IdMarca || RegistroAnterior.CantidadSalida != nuevoRegistro.CantidadSalida || RegistroAnterior.CantidadEntrada != nuevoRegistro.CantidadEntrada || RegistroAnterior.IdCartucho != nuevoRegistro.IdCartucho;
                if (CantidadesModificadas)
                {
                    if (nuevoRegistro.CantidadSalida > 0)//Comprobamos si se trata de una salida
                    {
                        if (AccionRegistro.ModificarRegistroInventario(nuevoRegistro, false))//Comprobamos si podemos realizar los cambios
                        {
                            //Ahora deberemos de modificar el registro anterior
                            AccionRegistro.ModificarInventario(RegistroAnterior, "ModificarTonerInventario");
                            MessageBox.Show("Se ha actualizado el registro", "MODIFICACION EXITOSA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("La cantidad de salida excede la cantidad disponible en oficina", "FALLO DE MODIFICACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else//Si no, se trata de una entrada
                    {
                        //Comprobamos que podamos quitarle las entradas al cartucho anterior y si podemos hacer nuevo registro
                        if (AccionRegistro.ComprobarCantidadInventario(RegistroAnterior) && AccionRegistro.ModificarRegistroInventario(nuevoRegistro, false))
                        {
                            AccionRegistro.ModificarInventario(RegistroAnterior, "ModificarTonerInventario");
                        }
                        else
                        {
                            MessageBox.Show("La cantidad de entrada excede la cantidad disponible en oficina del registro anterior", "FALLO DE MODIFICACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    //Si no hay modificaciones acerca de cantidades, marcas o modelos, solamente se modificara el registro en sí
                    if (AccionRegistro.ModificarRegistroInventario(nuevoRegistro, true))
                    {
                        MessageBox.Show("Registro modificado correctamente", "MODIFICACION EXITOSA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Algo salió mal al realizar la modificación", "ERROR EN MODIFICACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
            else
            {
                string Mensaje = AccionRegistro.AgregarRegistroInventario(nuevoRegistro);
                MessageBox.Show(Mensaje, "REGISTRO INVENTARIO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Mostrar("VerRegistroInventario");
            LimpiarForm(grpDatosRegistro);
        }
        public void ColocarCantidadesMovimiento(RegistroInventario nuevoRegistro)
        {
            nuevoRegistro.CantidadEntrada = 0;
            nuevoRegistro.CantidadSalida = 0;
            nuevoRegistro.CantidadGarantia = 0;
            if (radEntrada.Checked)
            {
                nuevoRegistro.CantidadEntrada = int.Parse(txtCantidad.Text);
            }
            else if (radSalida.Checked)
            {
                nuevoRegistro.CantidadSalida = int.Parse(txtCantidad.Text);
            }
            else
            {
                nuevoRegistro.CantidadGarantia = int.Parse(txtCantidad.Text);
            }
        }

        public bool ComprobarNumeroSerie()
        {
            bool DrumUtilizado = false;
            if (cboFusor.SelectedIndex > 0)
            {
                DrumUtilizado = true;
            }
            return DrumUtilizado;
        }

        public void EliminarRegistroInventario()
        {
            int IdMarca = NuevaAccion.BuscarId(cboMarcas.SelectedItem.ToString(), "ObtenerIdMarca");
            if (MessageBox.Show("¿Esta seguro de eliminar el registro?", "CONFIRMAR ELIMINACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                MessageBox.Show("!!Eliminación cancelada!!", "CANCELADO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                LimpiarForm(grpDatosRegistro);
                return;
            }

            RegistroInventario eliminarRegistro = new RegistroInventario()
            {
                Id = Id,
                IdMarca = IdMarca,
                IdCartucho = NuevaAccion.BuscarId(cboModelos.SelectedItem.ToString(), "ObtenerIdCartucho"),
                NumeroSerie = cboFusor.SelectedItem.ToString()
            };
            ColocarCantidadesMovimiento(eliminarRegistro);

            if (eliminarRegistro.CantidadEntrada > 0)//Comprobamos que se trata de una entrada
            {
                //Primero verifiquemos que la cantidad a restar en el anterior registro no exceda a las que cuenta
                if (AccionRegistro.ComprobarCantidadInventario(eliminarRegistro))
                {
                    AccionRegistro.ModificarInventario(eliminarRegistro, "EliminarRegistroInventario");
                }
                else
                {
                    MessageBox.Show("La cantidad que se quiere restar de entrada excede la cantidad del toner", "ERROR AL ELIMINAR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarForm(grpDatosRegistro);
                    return;
                }
            }
            else
            {
                AccionRegistro.ModificarInventario(eliminarRegistro, "EliminarRegistroInventario");
            }
            //VERIFICAR ESTA PARTE, NO COLOCAR ESTA CONDICION CUANDO ELIMINEMOS UNA ENTRADA



            MessageBox.Show("Registro eliminado correctamente", "ELIMINAR REGISTRO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Mostrar("VerRegistroInventario");
            ReiniciarControles();
        }
        #endregion

        #endregion

        #region Eventos
        
        //Evento que nos ayudara a mostrar en el combobox la informacion del inventario o de los registros dependiendo cual seleccionemos
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReiniciarControles();
            if (tabControl1.SelectedTab == tabInventario)
            {
                inventario = true;
                Mostrar("MostrarInventario");
                
            }
            else
            {
                inventario = false;
                Mostrar("VerRegistroInventario");
            }
        }

        public void SeleccionarDatosRegistros()
        {
            if (inventario)
            {
                LlenarCamposInventario();
            }
            else
            {
                LlenarCamposRegistroInventario();
            }
        }

        public void LlenarCamposRegistroInventario()
        {
            int IdMarcaSeleccionada = NuevaAccion.BuscarId(dtgCartuchos.CurrentRow.Cells[1].Value.ToString(), "ObtenerIdMarca");
            Id = int.Parse(dtgCartuchos.CurrentRow.Cells[0].Value.ToString());

            cboMarcas.SelectedItem = dtgCartuchos.CurrentRow.Cells[1].Value.ToString();
            cboModelos.SelectedItem = dtgCartuchos.CurrentRow.Cells[2].Value.ToString();
            //Aqui se requiere capturar de nuevo el modelo y la marca en dado caso de que se vaya cambiar el modelo o la marca
            //Esto para tener capturado la marca y el modelo anterior para realizar la respectiva modificacion en el inventario
            RegistroAnterior = new RegistroInventario()
            {
                IdMarca = IdMarcaSeleccionada,
                IdCartucho = NuevaAccion.BuscarId(dtgCartuchos.CurrentRow.Cells[2].Value.ToString(), "ObtenerIdCartucho"),
                CantidadSalida = int.Parse(dtgCartuchos.CurrentRow.Cells[3].Value.ToString()),
                CantidadEntrada = int.Parse(dtgCartuchos.CurrentRow.Cells[4].Value.ToString()),
                CantidadGarantia = int.Parse(dtgCartuchos.CurrentRow.Cells[5].Value.ToString())
            };
            SeleccionarRadioButtonTipoMovimiento(RegistroAnterior);


            cboClientes.SelectedItem = dtgCartuchos.CurrentRow.Cells[6].Value.ToString();
            dtpFechaRegistro.Value = DateTime.Parse(dtgCartuchos.CurrentRow.Cells[7].Value.ToString());

            //Comprobamos que tenga un numero de serie
            if (dtgCartuchos.CurrentRow.Cells[8].Value.ToString() != " ")
            {
                MostrarCapturaSerie(true);
                cboFusor.SelectedItem = dtgCartuchos.CurrentRow.Cells[8].Value.ToString();
                RegistroAnterior.NumeroSerie = dtgCartuchos.CurrentRow.Cells[8].Value.ToString();
            }
        }

        public void SeleccionarRadioButtonTipoMovimiento(RegistroInventario RegistroSeleccionado)
        {
            if (RegistroSeleccionado.CantidadSalida > 0)
            {
                radSalida.Checked = true;
                txtCantidad.Text = RegistroSeleccionado.CantidadSalida.ToString();
            }
            else if (RegistroSeleccionado.CantidadEntrada > 0)
            {
                radEntrada.Checked = true;
                txtCantidad.Text = RegistroSeleccionado.CantidadEntrada.ToString();
            }
            else
            {
                radGarantia.Checked = true;
                txtCantidad.Text = RegistroSeleccionado.CantidadGarantia.ToString();
            }
        }

        public void LlenarCamposInventario()
        {
            Id = int.Parse(dtgCartuchos.CurrentRow.Cells[0].Value.ToString());
            cboMarca.SelectedItem = dtgCartuchos.CurrentRow.Cells[1].Value.ToString();
            txtModelo.Text = dtgCartuchos.CurrentRow.Cells[2].Value.ToString();
            txtOficina.Text = dtgCartuchos.CurrentRow.Cells[3].Value.ToString();
            dtpFecha.Value = DateTime.Parse(dtgCartuchos.CurrentRow.Cells[4].Value.ToString());
            txtPrecio.Text = dtgCartuchos.CurrentRow.Cells[5].Value.ToString().Replace("$", "").Replace(",", "");
        }

        public void MostrarCapturaSerie(bool Mostrar)
        {
            lblSerieP.Visible = Mostrar;
            cboFusor.Visible = Mostrar;
        }

        private void dtgCartuchos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            EstaModificando = true;
            ControlesDesactivados(true);
            SeleccionarDatosRegistros();
        }

        private void cboMarcaP_SelectedIndexChanged(object sender, EventArgs e)
        {
            //En dado caso que se haya seleccionado algo de las marcas y mientras no estemos buscando un registro en especifico
            if (cboMarcas.SelectedItem.ToString() != " " && BuscandoRegistro == false)
            {
                int IdMarca = NuevaAccion.BuscarId(cboMarcas.SelectedItem.ToString(), "ObtenerIdMarca");
                Formulario.LlenarComboBox(cboModelos, "spSeleccionarCartuchosMarca", IdMarca);
            }
        }

        private void cboModelosP_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Cartucho = cboModelos.SelectedItem.ToString();
            var prefijosValidos = new HashSet<string> { "RM1-", "D01SE", "DR", "RM2-","R" };
            //Para saber si se trata de un fusor
            if (prefijosValidos.Any(prefijo => Cartucho.StartsWith(prefijo)))
            {
                MostrarCapturaSerie(true);
                //Si entra en la condicion quiere decir que se trata e un drum
                if (Cartucho.StartsWith("DR"))
                {
                    Formulario.LlenarComboBox(cboFusor, "SeleccionarSeriesBrother");
                    return;
                }
                Formulario.LlenarComboBox(cboFusor, "SeleccionarFusores");
            }
            else
            {
                MostrarCapturaSerie(false);
            }
        }

        #endregion

        #region MetodosLocales
        private void AbrirForm(object formNuevo)
        {
            //Declaramos la forma
            Form fh = formNuevo as Form;

            //Mostramos la forma 
            fh.Show();
        }

        private void LimpiarForm(GroupBox grp)
        {
            foreach (Control c in grp.Controls)
            {
                if (c is TextBox)
                {
                    c.Text = "";
                }
            }
            txtModelo.Focus();

            cboClientes.SelectedIndex = 0;
            cboModelos.SelectedIndex = 0;
            cboMarcas.SelectedIndex = 0;
            cboMarca.SelectedIndex = 0;
            cboMarcaSeleccionada.SelectedIndex = 0;
            cboFusor.SelectedIndex = 0;
            dtpFechaRegistro.Value = DateTime.Today;
        }

        public void ReiniciarControles()
        {
            EstaModificando = false;
            ControlesDesactivados(false);
            LimpiarForm(grpDatosRegistro);
            LimpiarForm(grpDatosInventario);
            Id = 0;
            dtpFechaRegistro.Value = DateTime.Today;
        }

        

        

        #endregion

        #region Formas
        private void btnAbrirReportes_Click(object sender, EventArgs e)
        {
            AbrirForm(new ReporteRegistros());
        }
        private void btnAbrirReporteExistencias_Click(object sender, EventArgs e)
        {
            AbrirForm(new ReporteInventario());
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            AbrirForm(new ReporteInventarioToners());
            //AccionInventario.ImprimirInventario();
        }
        #endregion


        private void btnGarantias_Click(object sender, EventArgs e)
        {
            AbrirForm(new Garantias());
        }

        private void cboClientes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboMostrarRegistros_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable tabla = new DataTable();
            //Dependiendo la opcion se enviaran diferentes stop procedures al metodo mostrar
            switch (cboMostrarRegistros.SelectedItem.ToString())
            {
                case "Ultima Semana": tabla = nuevaAccion.Mostrar("MostrarUltimaSemanarRegistroInventario"); break;
                case "Mes Pasado": tabla = nuevaAccion.Mostrar("MostrarMesPasadoRegistroInventario"); break;
                case "Ultimo Mes": tabla = nuevaAccion.Mostrar("VerRegistroInventario"); break;
                case "Este año": tabla = nuevaAccion.Mostrar("MostrarAñoActualRegistroInventario"); break;
             
            }
            //Asignamos los registros a nuestro datagridview
            dtgCartuchos.DataSource = tabla;
        }

        private void Inventario_Load(object sender, EventArgs e)
        {

        }

        private void cboMarca_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
