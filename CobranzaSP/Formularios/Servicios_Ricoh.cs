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
using DocumentFormat.OpenXml.Office2010.Excel;
using System.Runtime.CompilerServices;

namespace CobranzaSP.Formularios
{
    public partial class Servicios_Ricoh : Form
    {
        public Servicios_Ricoh()
        {
            InitializeComponent();
            InicioAplicacion();
        }
        AccionesLógica nuevaAccion = new AccionesLógica();
        CD_Conexion cn = new CD_Conexion();
        LogicaServiciosRicoh lgServicioRicoh = new LogicaServiciosRicoh();
        LogicaServicio lgServicio = new LogicaServicio();
        LogicaRegistroInventarioRicoh lgRegistroPartes = new LogicaRegistroInventarioRicoh();
        LogicaModulosCliente lgModuloCliente = new LogicaModulosCliente();
        FuncionesFormularios FuncionFormulario = new FuncionesFormularios();

        //Objeto para guardar los datos cuando buscamos un folio
        Modulo_Cliente ModuloSeleccionado;

        #region Variables
        //Generales
        string Serie = "";

        //Validaciones
        bool Validado = true;
        bool ValidadoPartes = true;

        //Modificaciones
        bool Modificar = false;
        bool ModificarParte = false;
        bool ModificarModulo = false;
        bool EstaModuloHabilitado = false;
        bool RetirarModulo = false;

        bool ModulosLlenos = false;
        bool BuscandoFolio = false;

        //Ids para modificar o eliminar
        int IdRegistro = 0;
        int IdParte = 0;
        int IdServicio = 0;
        int Id = 0;
        #endregion

        #region Inicio

        public void InicioAplicacion()
        {
            FuncionFormulario.PropiedadesDtg(dtgModulos);
            FuncionFormulario.PropiedadesDtg(dtgPartesUsadas);

            FuncionFormulario.LlenarComboBox(cboClientes, "SeleccionarClientesServicios", 0);
            FuncionFormulario.LlenarComboBox(cboModelos, "SeleccionarModelosRicoh", 0);

            //LlenarComboBox(cboDescripciones, "SeleccionarDescripcionesPartesRicoh", 0);

            string[] Tecnicos = { "", "ALVARO", "MIGUEL", };
            cboTecnico.Items.AddRange(Tecnicos);
            HabilitarBotonesModulos(false);
            ControlesDesactivadosInicialmente(false);
            EstablecerBotonesPartesHabilitadas(false);

            //dtgModulos.Columns["Id"].Visible = false;
            MostrarControlesBusquedaFolio(false);

            btnAgregar.Enabled = false;
                                                                            
            radServicio.Checked = true;
            cboModelos.Enabled = false;
        }


        private void ControlesDesactivadosInicialmente(bool activado)
        {
            btnCancelar.Enabled = activado;
            btnEliminar.Enabled = activado;
        }

        private void EstablecerBotonesPartesHabilitadas(bool activado)
        {
            btnEliminarParte.Enabled = activado;
            btnCancelarParte.Enabled = activado;
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
            dtgPartesUsadas.Columns["IdParte"].Visible = false;
        }
        #endregion

        #region Validaciones
        private void txtNumeroFolio_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validacion.SoloNumeros(e);
        }
        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validacion.SoloLetrasYNumeros(e);
        }

        public bool ValidarCamposVaciosServicios()
        {
            erServicios.Clear();
            Validado = true;

            ValidarCampo(txtNumeroFolio, "Ingrese número de folio del reporte");
            ValidarCampo(cboClientes, "Seleccione un cliente");
            ValidarCampo(cboNumeroSerie, "Seleccione una serie de impresora");

            //Validar si no hay un nuevo modelo Ricoh
            if (checkBox1.Checked)
            {
                ValidarCampo(txtModelo, "Ingrese el modelo de impresora");
            }
            else
            {
                ValidarCampo(cboModelos, "Seleccione un modelo");
            }

            ValidarCampo(txtContador, "Ingrese contador del equipo");
            ValidarCampo(cboTecnico, "Seleccione un tecnico");
            ValidarCampo(rtxtFallas, "Ingrese falla reportada");
            ValidarCampo(rtxtServicio, "Ingrese servicio realizado");

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
                    return;
                }
            }
            else if (c is ComboBox)
            {
                ComboBox combo = (ComboBox)c;
                if (combo.SelectedIndex == -1 || combo.SelectedIndex == 0)
                {
                    erServicios.SetError(c, Mensaje);
                    Validado = false;
                    return;
                }
            }
        }

        public bool ValidarCampoVacioBusqueda()
        {
            erServicios.Clear();
            Validado = true;

            ValidarCampo(txtBusqueda, "Ingrese numero de folio");
            return Validado;
        }

        public bool ValidarCamposVaciosPartesUsadas()
        {
            Validado = true;
            erServicios.Clear();

            ValidarCampo(txtNumeroFolio,"Ingrese el numero de folio del reporte");
            ValidarCampo(cboModelos,"Seleccione un modelo de impresora");
            ValidarCampo(cboPartesRicoh,"Seleccione una parte de Ricoh");
            ValidarCampo(txtCantidad,"Ingrese la cantidad utilizada");
            return Validado;
        }
        #endregion

        #region Botones

        #region Servicios
        private void btnAgregarReporte_Click(object sender, EventArgs e)
        {
            Reiniciar();
            
            grpDatos.Visible = true;//Mostrar campos para capturar un servicio
            txtNumeroFolio.Focus();
            
            Limpiar();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCamposVaciosServicios())
                    return;

                string Mensaje;
                Servicio nuevoServicio = new Servicio()
                {
                    NumeroFolio = txtNumeroFolio.Text,
                    IdCliente = nuevaAccion.BuscarId(cboClientes.SelectedItem.ToString(), "ObtenerIdCliente"),
                    Contador = int.Parse(txtContador.Text.Replace(",", "")),
                    Modelo = cboModelos.SelectedItem.ToString(),
                    IdMarca = 6,
                    IdSerie = nuevaAccion.BuscarId(Serie, "ObtenerIdSerie"),
                    Fecha = dtpFecha.Value,
                    Tecnico = cboTecnico.SelectedItem.ToString(),
                    ServicioRealizado = rtxtServicio.Text,
                    ReporteFallo = rtxtFallas.Text,
                    Fusor = "",
                    FusorSaliente = "",
                    EstaModificando = Modificar
                };
                VerificarNuevoModeloRicoh(nuevoServicio);
                DeterminarTipoDeReporte(nuevoServicio);
                nuevoServicio.Serie = (chkSerie.Checked) ? txtSerie.Text : cboNumeroSerie.SelectedItem.ToString();

                if (Modificar)
                {
                    if (MessageBox.Show("¿Esta seguro de modificar el registro?", "CONFIRME LA MODIFICACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        MessageBox.Show("Modificacion cancelada!!", "CANCELADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtpFecha.Value = DateTime.Now;
                        LimpiarForm(grpDatos);
                        return;
                    }
                }
                else
                {
                    bool FolioRepetido = nuevaAccion.VerificarDuplicados(nuevoServicio.NumeroFolio, "VerificarFolioExistente");
                    if (FolioRepetido)
                    {
                        MessageBox.Show("El numero de folio ya existe!!", "DUPLICADO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LimpiarForm(grpDatos);
                        return;
                    }

                    //Actualizamos los contadores de cada uno de los modulos
                    if (ModulosLlenos && BuscandoFolio == false)
                    {
                        //Agregar un registro con el contador actualizado de cada modulo
                        lgServicioRicoh.ActualizarModulosEquipo(nuevoServicio);
                    }
                }
                Mensaje = lgServicio.RegistroServicio(nuevoServicio);
                MessageBox.Show(Mensaje, "GUARDAR SERVICIO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarForm(grpDatos);
                tabControl1.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron guardar los datos por: " + ex);
            }
        }
        //Validar si se agrego un modelo nuevo de ricoh o se selecciono un modelo ya existente(Usado en agregar nuevo servicio)
        public void VerificarNuevoModeloRicoh(Servicio servicio)
        {
            if (checkBox1.Checked)
            {
                lgServicio.AñadirModelo(txtModelo.Text, servicio.IdMarca);
                servicio.IdModelo = nuevaAccion.BuscarId(txtModelo.Text, "ObtenerIdModelo");
            }
            else
                servicio.IdModelo = nuevaAccion.BuscarId(cboModelos.SelectedItem.ToString(), "ObtenerIdModelo");
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

        public void ComprobarNuevaSerie()
        {
            if (chkSerie.Checked)
            {
                Serie = txtSerie.Text;
                lgServicio.AñadirSerie(Serie);
            }
            else
            {
                Serie = cboNumeroSerie.SelectedItem.ToString();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Preguntamos si se esta seguro de eliminar el registro 
            if (MessageBox.Show("¿Esta seguro de eliminar el registro?", "CONFIRME LA ELIMINACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                MessageBox.Show("Elimacion cancelada!!", "CANCELADO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                LimpiarForm(grpDatos);
                return;
            }

            try
            {
                string NumeroFolio = txtNumeroFolio.Text;
                string Serie = cboNumeroSerie.SelectedItem.ToString();
                lgServicio.EliminarRegistro(NumeroFolio, "EliminarServicio");
                //En caso de tratarse el ultimo reporte de alguna serie, se debera actualizar el ultimo
                //reporte de los modulos de dicha serie
                lgModuloCliente.CambiarUltimoReporteEnSerieModulos(Serie, NumeroFolio);
                MessageBox.Show("Se elimino el registro", "OPERACION EXITOSA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reiniciar();

                //Mostramos los datos de los servicios por si se desea capturar otro
                tabControl1.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo eliminar el registro: " + ex, "OCURRIO UN PROBLEMA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Reiniciar();
        }

        public void Reiniciar()
        {
            BuscandoFolio = false;
            Modificar = false;
            LimpiarForm(grpDatos);
            LogicaModulosCliente.ClavesObservaciones.Clear();//Limpiar lista de Claves con observacion
            EstablecerBotonesPartesHabilitadas(false);
            ControlesDesactivadosInicialmente(false);
            HabilitarBotonesModulos(false);
            MostrarControlesBusquedaFolio(false);
            Modificar = false;
            tabControl1.Visible = false;//Ocultar campos de servicio

            //Limpiamos el DataGridView de los modulos
            dtgModulos.DataSource = null;
            dtgModulos.Refresh();

            //Limpiar dtg de partes
            dtgPartesUsadas.DataSource = null;
            dtgPartesUsadas.Refresh();

            btnEliminar.Enabled = false;
            radServicio.Checked = true;
        }

        private void btnBuscarReporte_Click(object sender, EventArgs e)
        {
            grpDatos.Visible = false;
            //Si no hay campos vacios, quiere decir que el formulario esta lleno
            Limpiar();
            tabControl1.Visible = false;
            //LimpiarForm(grpDatos);
            MostrarControlesBusquedaFolio(true);
            txtBusqueda.Focus();
        }
        public void MostrarControlesBusquedaFolio(bool mostrar)
        {
            lblNumeroFolio.Visible = mostrar;
            txtBusqueda.Visible = mostrar;
            btnBusqueda.Visible = mostrar;
        }
        private void btnBusqueda_Click(object sender, EventArgs e)
        {
            BuscandoFolio = true;
            Modificar = true;
            btnCancelar.Enabled = true;
            btnEliminar.Enabled = true;

            if (!ValidarCampoVacioBusqueda())
            {
                return;
            }

            ObtenerDatosServicioBuscado();
            MostrarPartesUsadas(txtNumeroFolio.Text);

            txtBusqueda.Text = "";
            //MostrarPartesUsadas(txtNumeroFolio.Text);
        }
        public void ObtenerDatosServicioBuscado()
        {
            Servicio ServicioBuscado = new Servicio();
            string NumeroFolio = txtBusqueda.Text;
            DataTable tblServicio = new DataTable();

            tblServicio = nuevaAccion.BuscarPrueba(NumeroFolio, "BuscarServicioFolio");
            //BuscandoFolio = true;
            btnCancelar.Enabled = true;

            if (tblServicio.Rows.Count == 0)
            {
                MessageBox.Show("El número de folio no esta registrado en la base de datos", "REGISTRO NO ENCONTRADO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (DataRow fila in tblServicio.Rows)
            {
                grpDatos.Visible = true;
                ServicioBuscado = new Servicio()
                {
                    //IdServicio = int.Parse(dr[0].ToString()),
                    NumeroFolio = (fila[0].ToString()),
                    Cliente = fila[1].ToString(),
                    Modelo = fila[3].ToString(),
                    Serie = fila[4].ToString(),
                    Contador = int.Parse(fila[5].ToString()),
                    Fecha = Convert.ToDateTime((fila[6].ToString())),
                    Tecnico = (fila[7].ToString()),
                    ServicioRealizado = fila[10].ToString(),
                    ReporteFallo = fila[11].ToString()
                };
            }
            LlenarFormulario(ServicioBuscado);
        }

        public void LlenarFormulario(Servicio ServicioBuscado)
        {
            //Llenamos los campos con los datos que guardamos en el objeto
            //IdServicio = ServicioBuscado.IdServicio;
            txtNumeroFolio.Text = ServicioBuscado.NumeroFolio;
            cboClientes.SelectedItem = ServicioBuscado.Cliente;
            cboModelos.SelectedItem = ServicioBuscado.Modelo;
            cboNumeroSerie.SelectedItem = ServicioBuscado.Serie;
            txtContador.Text = ServicioBuscado.Contador.ToString();
            dtpFecha.Value = ServicioBuscado.Fecha;
            cboTecnico.SelectedItem = ServicioBuscado.Tecnico;
            rtxtServicio.Text = ServicioBuscado.ServicioRealizado;
            rtxtFallas.Text = ServicioBuscado.ReporteFallo;
        }
        private void btnReportes_Click(object sender, EventArgs e)
        {
            ReporteServiciosRicoh forma = new ReporteServiciosRicoh();
            forma.Show();
        }
        #endregion

        #region Partes
        private void btnGuardarParte_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCamposVaciosPartesUsadas())
                    return;
                string ModeloImpresora = cboModelos.SelectedItem.ToString();
                string Mensaje;
                MovimientoParteRicoh nuevoMovimiento = new MovimientoParteRicoh()
                {
                    IdRegistro = IdRegistro,
                    IdTipoPersona = nuevaAccion.BuscarId(cboClientes.SelectedItem.ToString(), "ObtenerIdCliente"),
                    IdParte = lgRegistroPartes.BuscarIdParte(cboPartesRicoh.SelectedItem.ToString(), ModeloImpresora),
                    IdMovimiento = 1,
                    Cantidad = int.Parse(txtCantidad.Text),
                    Fecha = dtpFecha.Value,
                    Folio = txtNumeroFolio.Text
                };

                Mensaje = lgRegistroPartes.AgregarRegistroInventario(nuevoMovimiento);
                MessageBox.Show(Mensaje, "REGISTRO INVENTARIO PARTES RICOH", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarForm(grpDatosPartes);
                MostrarPartesUsadas(txtNumeroFolio.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron guardar los datos por: " + ex);
            }
        }

        private void btnEliminarParte_Click_1(object sender, EventArgs e)
        {
            string Mensaje = "";

            if (MessageBox.Show("¿Esta seguro de elimnar el registro?", "CONFIRME LA MODIFICACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                MessageBox.Show("!!Eliminacion cancelada!!", "CANCELADO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                LimpiarForm(grpDatosPartes);
                return;
            }

            MovimientoParteRicoh ParteEliminada = new MovimientoParteRicoh()
            {
                IdRegistro = IdRegistro,
                IdParte = this.IdParte,
                IdMovimiento = 1,
                Cantidad = int.Parse(txtCantidad.Text)
            };
            Mensaje = lgRegistroPartes.EliminarRegistro(ParteEliminada);
            MostrarPartesUsadas(txtNumeroFolio.Text);
            LimpiarForm(grpDatosPartes);
            EstablecerBotonesPartesHabilitadas(false);
        }

        private void btnCancelarParte_Click_1(object sender, EventArgs e)
        {
            LimpiarForm(grpDatosPartes);
            EstablecerBotonesPartesHabilitadas(false);
        }

        private void dtgPartesUsadas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            EstablecerBotonesPartesHabilitadas(true);
            IdRegistro = int.Parse(dtgPartesUsadas.CurrentRow.Cells[0].Value.ToString());
            cboPartesRicoh.SelectedItem = dtgPartesUsadas.CurrentRow.Cells[1].Value.ToString();
            txtCantidad.Text = dtgPartesUsadas.CurrentRow.Cells[2].Value.ToString();
            IdParte = int.Parse(dtgPartesUsadas.CurrentRow.Cells[3].Value.ToString());
        }
        #endregion
        #endregion

        #region Eventos

        //Evento que ocurre cuando se selecciona un cliente
        private void cboClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarSeriesClienteSeleccionado();
        }

        public void LlenarSeriesClienteSeleccionado()
        {
            if(cboClientes.SelectedItem == null)
            {
                return;
            }

            if (cboClientes.SelectedItem.ToString() != " ")
            {
                int IdCliente = nuevaAccion.BuscarId(cboClientes.SelectedItem.ToString(), "ObtenerIdCliente");
                string Cliente = cboClientes.SelectedItem.ToString();
                if (Cliente == "SPEED TONER")
                {
                    FuncionFormulario.LlenarComboBox(cboNumeroSerie, "SeleccionarSeriesEnBodegaRicoh", 0);
                }
                else
                {
                    FuncionFormulario.LlenarComboBox(cboNumeroSerie, "SeleccionarNumeroSerieEquiposRicoh", IdCliente);
                }
            }
        }

        private void cboNumeroSerie_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboNumeroSerie.SelectedItem == null)
            {
                return;
            }
            ModulosLlenos = false;


            if (cboNumeroSerie.SelectedItem.ToString() != " ")
            {
                Serie = cboNumeroSerie.SelectedItem.ToString();
                MostrarModulosCliente(Serie, true);//true para limpiar la lista

                //Llenamos el modelo dependiendo del numero de Serie
                string Cliente = cboClientes.SelectedItem.ToString();
                if(Cliente == "SPEED TONER")
                {
                    cboModelos.SelectedItem = lgServicioRicoh.ObtenerModeloEquipo(Serie, "SeleccionarModeloEquipoBodega");
                }
                else
                {
                    cboModelos.SelectedItem = lgServicioRicoh.ObtenerModeloEquipo(Serie, "SeleccionarModeloEquipo");
                }

                //Mostramos el tab donde estan los modulos
                tabControl1.Visible = true;

                btnAgregar.Enabled = true;//Habilitamos para poder agregar un modulo
                EstaModuloHabilitado = true;
                return;
            }
        }

        public void MostrarModulosCliente(string NumeroSerie, bool LimpiarLista)
        {
            string FolioReporte = txtNumeroFolio.Text;
            //Limpiamos los datos del datagridview
            dtgModulos.DataSource = null;
            dtgModulos.Refresh();
            DataTable tabla = new DataTable();


            if (!BuscandoFolio)
            {
                FolioReporte = "";
            }
            tabla = lgModuloCliente.MostrarModulosCliente(NumeroSerie, FolioReporte);


            //Asignamos los registros que optuvimos al datagridview
            dtgModulos.DataSource = tabla;

            //Ocultar columnas
            dtgModulos.Columns["Id"].Visible = false;
            dtgModulos.Columns["Estado"].Visible = false;
            dtgModulos.Columns["Folio"].Visible = false;

            //Si no estamos buscando un numero de folio de reporte
            if (!BuscandoFolio)
            {
                foreach (DataGridViewRow row in dtgModulos.Rows)
                {
                    string Clave = row.Cells["Clave"].Value.ToString();
                    //Solamente borraremos las observaciones de claves a las que nos se les haya asignado apenas una observación
                    if (!LogicaModulosCliente.ClavesObservaciones.Contains(Clave))
                    {
                        // Establecer el valor de la celda en blanco de los registros anteriores de cada modulo
                        row.Cells["Observacion"].Value = "";
                    }
                }
            }

            LlenarListaModulos(LimpiarLista);
        }

        //Guardamos los modulos en una lista
        private void LlenarListaModulos(bool LimpiarLista)
        {
            //Dependiendo el valor que mandemos decidiremos si limpiar o no la lista
            if (!LimpiarLista)
                return;

            if (ModulosLlenos)
                return;

            LogicaModulosCliente.ModulosEquipo.Clear();

            foreach (DataGridViewRow fila in dtgModulos.Rows)// Iterar a través de cada fila del DataGridView
            {
                // Asegurarse de que la fila no sea la fila de encabezado
                if (!fila.IsNewRow)
                {
                    //Quiere decir que al menos tenemos un registro
                    ModulosLlenos = true;

                    //Asignamos el numero de folio que tiene la fila
                    string FolioTabla = fila.Cells[5].Value.ToString();
                    //Validamos que sea diferente al reporte actual
                    if (txtNumeroFolio.Text != FolioTabla)//Si es el igual realizamos una modificacion a dicho modulo
                    {
                        //Quiere decir que no ha sido modificado dicho modulo
                        ModuloEquipo NuevoModulo = new ModuloEquipo()
                        {
                            Modulo = fila.Cells[1].Value.ToString(),
                            Clave = fila.Cells[2].Value.ToString()
                        };
                        LogicaModulosCliente.ModulosEquipo.Add(NuevoModulo);
                    }
                }
            }
        }

        //Evento que ocurre cuando se tienen que llenar las partes que utiliza el modelo de impresora actual
        private void cboModelos_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboPartesRicoh.Items.Clear();
            if (cboModelos.SelectedItem == null)
            {
                return;
            }
            if (cboModelos.SelectedItem.ToString() != " ")
            {
                string ModeloRicoh = cboModelos.SelectedItem.ToString();
                lgRegistroPartes.LlenarComboBoxPartes(cboPartesRicoh, ModeloRicoh);
            }
        }

        #region MostrarModulos
        
        #endregion
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
                else if (c is ComboBox)
                {
                    ComboBox comboBox = (ComboBox)c;
                    comboBox.SelectedIndex = -1;
                }
            }
            dtpFecha.Value = DateTime.Now;
        }

        //Utilizado cuando se agrege o se busque un reporte para limpiar el formulario
        public void Limpiar()
        {
            bool FormularioLleno = ValidarCamposVaciosServicios();
            erServicios.Clear();
            if (FormularioLleno)
            {
                //Por lo tanto limpiamos lo que teniamos seleccionado
                tabControl1.Visible = false;
                erServicios.Clear();
                LimpiarForm(grpDatos);
                cboClientes.SelectedItem = 0;
                cboNumeroSerie.SelectedItem = 0;
            }

        }


        #endregion


        #region Modulos
        //Boton que nos abrira una forma para colocar un modulo nuevo
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            int contador = int.Parse(txtContador.Text);
            Servicio NuevoServicio = new Servicio()
            {
                Contador = int.Parse(txtContador.Text),
                Serie = Serie,
                Modelo = cboModelos.SelectedItem.ToString(),
                NumeroFolio = txtNumeroFolio.Text
            };
            //ModuloNuevo modulo = new ModuloNuevo(this, cboModelos.SelectedItem.ToString(), Serie, txtNumeroFolio.Text, contador, ModificarModulo);
            ModuloNuevo modulo = new ModuloNuevo(this, NuevoServicio);
            modulo.Show();
        }

        //Abrira una forma, donde tendremos ya cargados los datos
        private void btnModificarModulo_Click(object sender, EventArgs e)
        {
            //int Contador = int.Parse(txtContador.Text);
            //string Modelo = cboModelos.SelectedItem.ToString();
            //string NumeroFolio = txtNumeroFolio.Text;
            Servicio NuevoServicio = new Servicio()
            {
                Contador = int.Parse(txtContador.Text),
                Serie = Serie,
                Modelo = cboModelos.SelectedItem.ToString(),
                NumeroFolio = txtNumeroFolio.Text
            };
            ModuloNuevo modulo = new ModuloNuevo(this, ModuloSeleccionado, NuevoServicio, ModificarModulo, RetirarModulo, BuscandoFolio);
            modulo.Show();
        }

        private void btnRetirarModulo_Click(object sender, EventArgs e)
        {
            ModuloNuevo modulo;
            int contador = int.Parse(txtContador.Text);
            RetirarModulo = true;

            Servicio DatosServicio = new Servicio()
            {
                
                Contador = int.Parse(txtContador.Text),
                Serie = Serie,
                Modelo = cboModelos.SelectedItem.ToString(),
                NumeroFolio = txtNumeroFolio.Text
            };

            //modulo = new ModuloNuevo(this, cboModelos.SelectedItem.ToString(), ModuloSeleccionado, Serie, txtNumeroFolio.Text, contador, ModificarModulo, RetirarModulo, BuscandoFolio);
            modulo = new ModuloNuevo(this, ModuloSeleccionado, DatosServicio, ModificarModulo, RetirarModulo, BuscandoFolio);

            //if (ModificarModulo)
            //{
            //    //Alguna variable para cargar
            //    modulo = new ModuloNuevo(this, cboModelos.SelectedItem.ToString(), ModuloSeleccionado, Serie, txtNumeroFolio.Text, contador, ModificarModulo, RetirarModulo, BuscandoFolio);

            //    //modulo = new ModuloNuevo(this, cboModelos.SelectedItem.ToString(), Serie, txtNumeroFolio.Text, contador, ModificarModulo, RetirarModulo);
            //}
            //else
            //{
            //    modulo = new ModuloNuevo(this, cboModelos.SelectedItem.ToString(), Serie, txtNumeroFolio.Text, contador, ModificarModulo, RetirarModulo);
            //}

            modulo.Show();
        }

        private void btnCance_Click(object sender, EventArgs e)
        {
            ReiniciarCapturaModulo();
        }
        public void ReiniciarCapturaModulo()
        {
            HabilitarBotonesModulos(false);
            ModificarModulo = false;
            btnAgregar.Enabled = true;
            RetirarModulo = false;
        }
        

        public void HabilitarBotonesModulos(bool habilitar)
        {
            btnModificarModulo.Enabled = habilitar;
            btnEliminarModulo.Enabled = habilitar;
            btnRetirarModulo.Enabled = habilitar;
        }

        //Seleccionar un modulo del datagridview de Modulos
        private void dtgModulos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            HabilitarBotonesModulos(true);
            btnAgregar.Enabled = false;
            ModificarModulo = true;

            ModuloSeleccionado = new Modulo_Cliente()
            {
                Id = int.Parse(dtgModulos.CurrentRow.Cells[0].Value.ToString()),
                //Modelo = dtgModulos.CurrentRow.Cells[1].Value.ToString(),
                Modulo = dtgModulos.CurrentRow.Cells[1].Value.ToString(),
                //Serie = dtgModulos.CurrentRow.Cells[3].Value.ToString(),
                Clave = dtgModulos.CurrentRow.Cells[2].Value.ToString(),
                //Paginas = int.Parse(txtContador.Text),
                Estado = dtgModulos.CurrentRow.Cells[4].Value.ToString(),
                Observacion = dtgModulos.CurrentRow.Cells[3].Value.ToString()
            };
        }


        #endregion

        private void chkSerie_CheckedChanged(object sender, EventArgs e)
        {
            cboNumeroSerie.Visible = !chkSerie.Checked;
            txtSerie.Visible = chkSerie.Checked;

            if (chkSerie.Checked)
            {
                //Habilita para capturar la serie manualmente
                txtSerie.Focus();
                //Se habilitan la captura de marca y modelo de impresora
                cboModelos.Enabled = true;
            }
            else
            {
                //Se desactivan la captura de marca y modelo para evitar que sean cambiados por el usuario
                cboModelos.Enabled = false;
                cboNumeroSerie.Focus();
            }
        }
    }
}
