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

namespace CobranzaSP.Formularios
{
    public partial class Servicios : Form
    {
        public Servicios()
        {
            InitializeComponent();
            InicioAplicacion();
        }
        AccionesLógica nuevaAccion = new AccionesLógica();
        LogicaServicio lgServicio = new LogicaServicio();
        LogicaRegistroInventarioRicoh lgRegistroPartes = new LogicaRegistroInventarioRicoh();
        FuncionesFormularios Formulario = new FuncionesFormularios();
        
        bool Validado = true;
        int IdRegistroParte = 0;
        string NumeroFolio = "";
        string SerieImpresora = "";

        //Sabremos cuando estamos añadiendo un nuevo registro o modificando
        bool Modificar = false;

        #region Inicio
        public void InicioAplicacion()
        {
            //Asignar propiedades a datagridview
            Formulario.PropiedadesDtg(dtgServicios);

            //Desactivar controles al iniciar la aplicacion
            ControlesDesactivadosInicialmente(false);

            //Llenar combobox
            Formulario.LlenarComboBox(cboClientes, "SeleccionarClientesServicios");
            Formulario.LlenarComboBox(cboMarca, "SeleccionarMarca");
            cboModelos.Items.Add("");
            Formulario.LlenarComboBox(cboFusor, "SeleccionarFusores", 0);
            Formulario.LlenarComboBox(cboFusorRetirado, "SeleccionarFusores", 0);

            //Denegar escritura en combobox
            cboMarca.DropDownStyle = ComboBoxStyle.DropDownList;
            cboClientes.DropDownStyle = ComboBoxStyle.DropDownList;
            cboModelos.DropDownStyle = ComboBoxStyle.DropDownList;

            //Mostrar los reportes de servicio
            MostrarDatosServicios();

            dtpFecha.MaxDate = DateTime.Today;

            //Opciones de tecnicos
            string[] Tecnicos = { "", "ALVARO", "MIGUEL", };
            cboTecnico.Items.AddRange(Tecnicos);

            string[] OpcionesMostrar = { "", "Ultima Semana", "Ultimo Mes", "Mes Pasado", "Este año", "Todos" };
            cboMostrar.Items.AddRange(OpcionesMostrar);

            HabilitarMarcaModelo(false);

            //Por defecto la mayoria de reportes son de tipo servicio
            radServicio.Checked = true;
        }

        private void ControlesDesactivadosInicialmente(bool activado)
        {
            btnCancelar.Enabled = activado;
            btnEliminar.Enabled = activado;
        }


        public void MostrarDatosServicios()
        {
            //Limpiamos los datos del datagridview
            dtgServicios.DataSource = null;
            dtgServicios.Refresh();
            DataTable tabla = new DataTable();
            //Guardamos los registros dependiendo la consulta
            tabla = nuevaAccion.Mostrar("SeleccionarTodosLosServicios");
            //Asignamos los registros que optuvimos al datagridview
            dtgServicios.DataSource = tabla;
        }
        #endregion

        #region Validaciones

        public bool ValidarCampos()
        {
            Validado = true;
            erServicios.Clear();

            ValidarCampo(txtNumeroFolio, "Ingrese numero de folio");
            ValidarCampo(txtContador, "Ingrese contador");
            ValidarCampo(cboClientes, "Seleccione un cliente");
            if (chkSerie.Checked)
            {
                ValidarCampo(txtNumeroSerie, "Ingrese el numero de serie");
                ValidarCampo(cboMarca, "Seleccione una marca");
                if (checkBox1.Checked)
                {
                    ValidarCampo(txtModelo, "Ingrese un modelo");
                }
                else
                {
                    ValidarCampo(cboModelos, "Seleccione un modelo");
                }
            }
            else
            {
                ValidarCampo(cboNumeroSerie, "Seleccione una serie");
            }
            ValidarCampo(cboTecnico, "Seleccione un tecnico");

            if (chkFusor.Checked)
            {
                ValidarCampo(cboFusor, "Seleccione un fusor");
                ValidarCampo(cboFusorRetirado, "Seleccione un fusor");
            }
            ValidarCampo(rtxtFallas, "Ingrese falla reportada");
            ValidarCampo(rtxtServicio, "Ingrese servicio realizado");
            return Validado;
        }

        public bool ValidarCamposPartes()
        {
            Validado = true;
            erServicios.Clear();

            ValidarCampo(cboPartes, "Seleccione una parte");
            ValidarCampo(txtCantidad, "Ingrese la cantidad");

            return Validado;
        }

        public bool ValidarCampoBusqueda()
        {
            Validado = true;
            erServicios.Clear();

            ValidarCampo(txtBusqueda,"Ingrese numero de serie");
            return Validado;
        }

        public void ValidarCampo(Control c, string Mensaje)
        {
            if (c is TextBox || c is RichTextBox)
            {
                if (string.IsNullOrEmpty(c.Text))
                {
                    erServicios.SetError(c, Mensaje);
                    Validado = false;
                }
            }
            else if (c is ComboBox)
            {
                ComboBox combo = (ComboBox)c;
                if (combo.SelectedIndex == -1 || combo.SelectedIndex == 0)
                {
                    erServicios.SetError(c, Mensaje);
                    Validado = false;
                }
            }
        }

        private void txtNumeroFolio_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validacion.SoloLetrasYNumeros(e);
        }

        private void txtNumeroSerie_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validacion.SoloLetrasYNumeros(e);
        }

        private void txtModelo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validacion.SoloLetrasYNumeros(e);
        }

        private void txtContador_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validacion.SoloNumeros(e);
        }
        #endregion

        #region Botones

        public void VerificarUsoFusor(Servicio servicio)
        {
            if (!chkFusor.Checked)
            {
                servicio.Fusor = " ";
                servicio.FusorSaliente = " ";
                return;
            }

            string NombreCliente = cboClientes.SelectedItem.ToString();

            //Restaremos del inventario el fusor que se utilizo en base a su modelo

            servicio.Fusor = cboFusor.SelectedItem.ToString();
            servicio.FusorSaliente = cboFusorRetirado.SelectedItem.ToString();
            if (Modificar && servicio.Fusor != "S/N")
            {
                return;
            }
            LogicaRegistro AccionRegistro = new LogicaRegistro();
            //Con ayuda de la clave del fusor, podemos obtener a traves de su modelo el idcartucho 
            int IdCartucho = nuevaAccion.BuscarId(servicio.Fusor, "ObtenerModeloFusor");

            RegistroInventario registroFusor = new RegistroInventario()
            {
                Cliente = cboClientes.SelectedItem.ToString(),
                IdMarca = lgServicio.ObtenerMarcaFusor(IdCartucho),
                IdCartucho = IdCartucho,
                Fecha = servicio.Fecha,
                NumeroSerie = servicio.Fusor
            };

            //Definimos la cantidad de entrada y salida dependiendo si el cliente es "Speed Toner"
            registroFusor.CantidadSalida = (NombreCliente == "SPEED TONER") ? 0 : 1; // La cantidad de salida se establece en 1 si el nombre del cliente no es "SPEED TONER", de lo contrario se mantiene en 0
            registroFusor.CantidadEntrada = (NombreCliente == "SPEED TONER") ? 1 : 0; // La cantidad de entrada se establece en 1 si el nombre del cliente es "SPEED TONER", de lo contrario se mantiene en 0

            string Mensaje = AccionRegistro.AgregarRegistroInventario(registroFusor);
            MessageBox.Show(Mensaje, "REGISTRO INVENTARIO", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        public void VerificarNuevoModelo(Servicio servicio)
        {
            if (checkBox1.Checked)
            {
                lgServicio.AñadirModelo(txtModelo.Text, servicio.IdMarca);
                servicio.IdModelo = nuevaAccion.BuscarId(txtModelo.Text, "ObtenerIdModelo");
            }
            else
                servicio.IdModelo = nuevaAccion.BuscarId(cboModelos.SelectedItem.ToString(), "ObtenerIdModelo");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Validado = true;
            try
            {
                if (!ValidarCampos())
                    return;

                ComprobarNuevaSerie();

                string Mensaje;
                Servicio nuevoServicio = new Servicio()
                {
                    //NumeroFolio = NumeroFolio,
                    NumeroFolio = txtNumeroFolio.Text,
                    IdCliente = nuevaAccion.BuscarId(cboClientes.SelectedItem.ToString(), "ObtenerIdCliente"),
                    IdMarca = nuevaAccion.BuscarId(cboMarca.SelectedItem.ToString(), "ObtenerIdMarca"),
                    IdSerie = nuevaAccion.BuscarId(SerieImpresora, "ObtenerIdSerie"),
                    Contador = int.Parse(txtContador.Text.Replace(",", "")),
                    Fecha = dtpFecha.Value,
                    Tecnico = cboTecnico.SelectedItem.ToString(),
                    ServicioRealizado = rtxtServicio.Text,
                    ReporteFallo = rtxtFallas.Text,
                    SeDioMantenimiento = false, 
                    EstaModificando = Modificar
                };
                VerificarNuevoModelo(nuevoServicio);
                DeterminarTipoDeReporte(nuevoServicio);
                
                //Capturar serie existente o una nueva que debe de ser capturada por el usuario
                nuevoServicio.Serie = (chkSerie.Checked) ? txtNumeroSerie.Text : cboNumeroSerie.SelectedItem.ToString();

                if (Modificar)
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
                    bool FolioRepetido = nuevaAccion.VerificarDuplicados(nuevoServicio.NumeroFolio, "VerificarFolioExistente");
                    if (FolioRepetido)
                    {
                        MessageBox.Show("El numero de folio ya existe!!", "DUPLICADO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LimpiarForm();
                        cboNumeroSerie.SelectedIndex = 0;
                        return;
                    }
                }

                VerificarUsoFusor(nuevoServicio);
                //Mensaje = lgServicio.RegistroServicio(nuevoServicio, "ModificarServicio");
                Mensaje = lgServicio.RegistroServicio(nuevoServicio);
                MessageBox.Show(Mensaje, "REPORTES DE SERVICIOS TECNICOS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarForm();
                cboNumeroSerie.SelectedIndex = 0;
                MostrarDatosServicios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron guardar los datos por: " + ex);
            }
        }

        public void ComprobarNuevaSerie()
        {
            if (chkSerie.Checked)
            {
                SerieImpresora = txtNumeroSerie.Text;
                lgServicio.AñadirSerie(SerieImpresora);
            }
            else
            {
                SerieImpresora = cboNumeroSerie.SelectedItem.ToString();
            }
        }

        public void DeterminarTipoDeReporte(Servicio nuevoServicio)
        {
            if (radServicio.Checked)
            {
                //Servicio
                nuevoServicio.IdTipoServicio = 1;
            }
            else if (radInstalacion.Checked)
            {
                //Instalacion
                nuevoServicio.IdTipoServicio = 2;
            }
            else if (radRetirado.Checked)
            {
                //Retiro
                nuevoServicio.IdTipoServicio = 3;
            }
            else
            {
                //Mantenimiento
                nuevoServicio.IdTipoServicio = 4;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Preguntamos si se esta seguro de eliminar el registro 
            if (MessageBox.Show("¿Esta seguro de eliminar el registro?", "CONFIRME LA ELIMINACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                MessageBox.Show("Elimacion cancelada!!", "CANCELADO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                LimpiarForm();
                return;
            }

            try
            {
                string NumeroFolio = txtNumeroFolio.Text;
                lgServicio.EliminarRegistro(NumeroFolio, "EliminarServicio");
                MessageBox.Show("Se elimino el registro", "OPERACION EXITOSA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarDatosServicios();
                LimpiarForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo eliminar el registro: " + ex, "OCURRIO UN PROBLEMA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //Regresaremos a como era al iniciar el programa
            ControlesDesactivadosInicialmente(false);
            btnGuardar.Enabled = true;
            LimpiarForm();
            Modificar = false;
        }

        private void btnBusqueda_Click(object sender, EventArgs e)
        {
            if (!ValidarCampoBusqueda())
            {
                return;
            }
            Modificar = true;
            string str = txtBusqueda.Text;
            DataTable tblServicio = new DataTable();

            tblServicio = nuevaAccion.BuscarPrueba(txtBusqueda.Text, "BuscarServicioFolio");
            btnCancelar.Enabled = true;

            foreach(DataRow fila in tblServicio.Rows)
            {
                txtNumeroFolio.Text = (fila[0].ToString());
                cboClientes.SelectedItem = (fila[1].ToString());
                cboMarca.SelectedItem = (fila[2].ToString());
                cboModelos.SelectedItem = (fila[3].ToString());
                int valor = cboNumeroSerie.FindString(fila[4].ToString());

                if(valor > 0)
                {
                    cboNumeroSerie.SelectedItem = fila[4].ToString();
                }
                else
                {
                    chkSerie.Checked = true;
                    txtNumeroSerie.Text = fila[4].ToString();
                }
                //if (cboNumeroSerie.Text != fila[4].ToString())
                //{
                //    chkSerie.Checked = true;
                //    txtNumeroSerie.Text = fila[4].ToString();
                //}
                //else
                //    cboNumeroSerie.SelectedItem = fila[4].ToString();
                txtContador.Text = (fila[5].ToString());
                DateTime FechaRegistro = Convert.ToDateTime((fila[6].ToString()));
                dtpFecha.Value = FechaRegistro;
                cboTecnico.SelectedItem = (fila[7].ToString());


                string Fusor = fila[8].ToString();
                if (Fusor != "" && Fusor != " ")
                {
                    chkFusor.Checked = true;
                    cboFusor.SelectedItem = fila[8].ToString();
                    cboFusorRetirado.SelectedItem = fila[9].ToString();
                }
                rtxtServicio.Text = (fila[10].ToString());
                rtxtFallas.Text = (fila[11].ToString());
                //Agregamos las opciones dependiendo los registros que nos devolvieron
            }
            MostrarPartesUsadas(txtNumeroFolio.Text);
            txtBusqueda.Text = "";

        }

        //Abrir interfaz de reportes
        private void btnreportes2_Click(object sender, EventArgs e)
        {
            AbrirForm(new ServicioReportes());
        }
        #endregion

        #region Eventos

        #region MovimientosEnCampos
        //Evento que ocurre en caso de que la serie no este con el cliente o sea una serie que no se tiene registro
        private void chkSerie_CheckedChanged(object sender, EventArgs e)
        {
            cboNumeroSerie.Visible = !chkSerie.Checked;
            txtNumeroSerie.Visible = chkSerie.Checked;

            if (chkSerie.Checked)
            {
                //Habilita para capturar la serie manualmente
                txtNumeroSerie.Focus();
                //Se habilitan la captura de marca y modelo de impresora
                HabilitarMarcaModelo(true);
            }
            else
            {
                //Se desactivan la captura de marca y modelo para evitar que sean cambiados por el usuario
                HabilitarMarcaModelo(false);
                cboNumeroSerie.Focus();
            }
        }
        //Evento que ocurre en dado caso de que llegue un reporte con cambio de fusor
        private void chkFusor_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFusor.Checked)
                MostrarCamposFusor(true);
            else
                MostrarCamposFusor(false);
        }

        public void MostrarCamposFusor(bool Mostrar)
        {
            lblFusorInstalado.Visible = Mostrar;
            lblFusorRetirado.Visible = Mostrar;
            cboFusor.Visible = Mostrar;
            cboFusorRetirado.Visible = Mostrar;
        }
        //Dependiendo el cliente que se elija se rellenaran las series con las que cuenta dicho cliente
        private void cboClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarSeries();
        }

        private void cboClientes_DropDownClosed(object sender, EventArgs e)
        {
            LlenarSeries();
        }

        public void LlenarSeries()
        {
            if (cboClientes.SelectedItem == null)
            {
                return;
            }

            if (cboClientes.SelectedItem.ToString() != " ")
            {
                int IdCliente = nuevaAccion.BuscarId(cboClientes.SelectedItem.ToString(), "ObtenerIdCliente");
                string Cliente = cboClientes.SelectedItem.ToString();
                if (Cliente == "SPEED TONER")
                {
                    Formulario.LlenarComboBox(cboNumeroSerie, "SeleccionarSeriesEnBodega", 0);
                }
                else
                {
                    Formulario.LlenarComboBox(cboNumeroSerie, "SeleccionarNumeroSerieEquipo", IdCliente);
                }
            }

        }
        //Evento que ocurre cuando se requerira capturar un nuevo modelo de equipo de alguna marca
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtModelo.Visible = checkBox1.Checked;
            cboModelos.Visible = !checkBox1.Checked;
        }

        //Evento que ocurre para cuando se seleccione una marca, rellenar los modelos de unicamente esa marca
        private void cboMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboModelos.Enabled = false;
            //En dado caso que se haya seleccionado algo de las marcas y mientras no estemos buscando un registro en especifico
            if (cboMarca.SelectedItem.ToString() != " ")
            {
                int IdMarca = nuevaAccion.BuscarId(cboMarca.SelectedItem.ToString(), "ObtenerIdMarca");
                cboModelos.Enabled = true;
                Formulario.LlenarComboBox(cboModelos, "SeleccionarModelos", IdMarca);
                cboModelos.SelectedIndex = 0;
            }
        }

        //Evento que dependiendo la serie que se elija, se buscara de que modelo y marca de impresora se trata dicha serie
        private void cboNumeroSerie_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Serie;
            DataTable tbMarcaModelo;
            cboMarca.SelectedIndex = 0;
            cboModelos.SelectedIndex = 0;

            if (cboNumeroSerie.SelectedItem.ToString() != " ")
            {
                Serie = cboNumeroSerie.SelectedItem.ToString();

                //Llenamos el modelo dependiendo del numero de Serie
                tbMarcaModelo = lgServicio.ObtenerMarcaModeloEquipo(Serie);

                foreach (DataRow fila in tbMarcaModelo.Rows)
                {
                    cboMarca.SelectedItem = fila[1].ToString();
                    cboModelos.SelectedItem = fila[0].ToString();
                }
                HabilitarMarcaModelo(false);
                return;
            }
        }

        public void HabilitarMarcaModelo(bool habilitado)
        {
            cboMarca.Enabled = habilitado;
            cboModelos.Enabled = habilitado;
            checkBox1.Enabled = habilitado;
        }
        #endregion


        //Evento que ocurre cuando se selecciona un registro del dtg y rellena los campos en el formulario de dicho registro para su modificacion o eliminación
        private void dtgServicios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LimpiarForm();
            //Una vez que se escoga alguna fila podremos activar los botones para poder modificar y eliminar
            btnGuardar.Enabled = true;
            ControlesDesactivadosInicialmente(true);
            Modificar = true;

            //Asignacion a los controles
            txtNumeroFolio.Text = dtgServicios.CurrentRow.Cells[0].Value.ToString();
            NumeroFolio = dtgServicios.CurrentRow.Cells[0].Value.ToString();
            cboClientes.SelectedItem = dtgServicios.CurrentRow.Cells[1].Value.ToString();

            cboMarca.SelectedItem = dtgServicios.CurrentRow.Cells[2].Value.ToString();
            cboModelos.SelectedItem = dtgServicios.CurrentRow.Cells[3].Value.ToString();
            cboNumeroSerie.SelectedItem = dtgServicios.CurrentRow.Cells[4].Value.ToString();

            if (cboNumeroSerie.Text != dtgServicios.CurrentRow.Cells[4].Value.ToString())
            {
                chkSerie.Checked = true;
                txtNumeroSerie.Text = dtgServicios.CurrentRow.Cells[4].Value.ToString();
            }
            string Fusor = dtgServicios.CurrentRow.Cells[8].Value.ToString();
            if (Fusor != "" && Fusor != " ")
            {
                chkFusor.Checked = true;
                cboFusor.SelectedItem = dtgServicios.CurrentRow.Cells[8].Value.ToString();
                cboFusorRetirado.SelectedItem = dtgServicios.CurrentRow.Cells[9].Value.ToString();
            }

            txtContador.Text = dtgServicios.CurrentRow.Cells[5].Value.ToString();
            DateTime FechaRegistro = Convert.ToDateTime(dtgServicios.CurrentRow.Cells[6].Value.ToString());
            dtpFecha.Value = FechaRegistro;
            cboTecnico.SelectedItem = dtgServicios.CurrentRow.Cells[7].Value.ToString();

            rtxtServicio.Text = dtgServicios.CurrentRow.Cells[10].Value.ToString();
            rtxtFallas.Text = dtgServicios.CurrentRow.Cells[11].Value.ToString();
        }

        private void cboClientes_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }


        private void cboMostrar_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable tabla = new DataTable();
            //Dependiendo la opcion se enviaran diferentes stop procedures al metodo mostrar
            switch (cboMostrar.SelectedItem.ToString())
            {
                case "Ultima Semana": tabla = nuevaAccion.Mostrar("MostrarReportesUltimaSemana"); break;
                case "Mes Pasado": tabla = nuevaAccion.Mostrar("MostrarReportesMesPasado"); break;
                case "Ultimo Mes": tabla = nuevaAccion.Mostrar("MostrarReportesUltimoMes"); break;
                case "Este año": tabla = nuevaAccion.Mostrar("MostrarReportesAñoActual"); break;
                case "Todos": tabla = nuevaAccion.Mostrar("SeleccionarTodosLosServicios"); break;
            }
            //Asignamos los registros a nuestro datagridview
            dtgServicios.DataSource = tabla;
        }
        #endregion

        #region MetodosLocales
        private void LimpiarForm()
        {
            foreach (Control c in grpDatos.Controls)
            {
                if (c is TextBox || c is RichTextBox)
                {
                    c.Text = "";
                }
            }

            cboFusor.SelectedIndex = 0;
            cboFusorRetirado.SelectedIndex = 0;
            cboClientes.SelectedIndex = 0;
            cboMarca.SelectedIndex = 0;
            cboTecnico.SelectedIndex = 0;

            dtpFecha.Value = DateTime.Today;
            //Y volvemos a llenar todo el combobox con todos los modelos
            Formulario.LlenarComboBox(cboModelos, "SeleccionarModelos", 0);
            chkSerie.Checked = false;
            chkFusor.Checked = false;
            checkBox1.Checked = false;
            txtNumeroFolio.Focus();
            radServicio.Checked = true;
        }

        private void AbrirForm(object formNuevo)
        {
            //Declaramos la forma
            Form fh = formNuevo as Form;

            //Mostramos la forma 
            fh.Show();
        }
        #endregion


        #region Partes
        private void cboModelos_SelectedIndexChanged(object sender, EventArgs e)
        {
            grpPartesUsadas.Visible = false;
            
            if (cboModelos.SelectedItem.ToString() != " ")
            {
                string Modelo = cboModelos.SelectedItem.ToString();
                lgRegistroPartes.LlenarComboBoxPartes(cboPartes, Modelo);
                if(cboPartes.Items.Count > 1)
                {
                    grpPartesUsadas.Visible = true;
                }
            }
        }

        private void btnGuardarParte_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCamposPartes())
                    return;

                string Mensaje;
                MovimientoParteRicoh nuevoMovimiento = new MovimientoParteRicoh()
                {
                    IdRegistro = IdRegistroParte,
                    IdTipoPersona = nuevaAccion.BuscarId(cboClientes.SelectedItem.ToString(), "ObtenerIdCliente"),
                    IdParte = nuevaAccion.BuscarId(cboPartes.SelectedItem.ToString(), "spObtenerIdParte"),
                    IdMovimiento = 1,
                    Cantidad = int.Parse(txtCantidad.Text),
                    Fecha = dtpFecha.Value,
                    Folio = txtNumeroFolio.Text
                };

                Mensaje = lgRegistroPartes.AgregarRegistroInventario(nuevoMovimiento);
                MessageBox.Show(Mensaje, "REGISTRO INVENTARIO PARTES RICOH", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReiniciarPartes();
                MostrarPartesUsadas(txtNumeroFolio.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron guardar los datos por: " + ex);
            }
        }

        public void ReiniciarPartes()
        {
            cboPartes.SelectedIndex = 0;
            txtCantidad.Text = "";
            EstablecerBotonesPartes(false);
            IdRegistroParte = 0;
        }

        public void EstablecerBotonesPartes(bool Habilitado)
        {
            btnCancelarParte.Enabled = Habilitado;
            btnEliminarParte.Enabled = Habilitado;
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
        private void btnEliminarParte_Click(object sender, EventArgs e)
        {
            string Mensaje = "";

            if (MessageBox.Show("¿Esta seguro de elimnar el registro?", "CONFIRME LA MODIFICACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                MessageBox.Show("!!Eliminacion cancelada!!", "CANCELADO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                ReiniciarPartes();
                return;
            }

            MovimientoParteRicoh ParteEliminada = new MovimientoParteRicoh()
            {
                IdRegistro = IdRegistroParte,
                IdParte = nuevaAccion.BuscarId(cboPartes.SelectedItem.ToString(), "ObtenerIdParteRicoh"),
                IdMovimiento = 1,
                Cantidad = int.Parse(txtCantidad.Text)
            };
            Mensaje = lgRegistroPartes.EliminarRegistro(ParteEliminada);
            MostrarPartesUsadas(txtNumeroFolio.Text);
            ReiniciarPartes();
        }

        private void btnCancelarParte_Click(object sender, EventArgs e)
        {
            ReiniciarPartes();
        }
        private void dtgPartesUsadas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            EstablecerBotonesPartes(true);
            IdRegistroParte = int.Parse(dtgPartesUsadas.CurrentRow.Cells[0].Value.ToString());
            cboPartes.SelectedItem = dtgPartesUsadas.CurrentRow.Cells[1].Value.ToString();
            txtCantidad.Text = dtgPartesUsadas.CurrentRow.Cells[2].Value.ToString();
        }
        #endregion

    }
}
