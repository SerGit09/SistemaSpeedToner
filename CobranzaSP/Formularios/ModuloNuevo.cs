using CobranzaSP.Lógica;
using CobranzaSP.Modelos;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CobranzaSP.Formularios
{
    public partial class ModuloNuevo : Form
    {
        private Servicios_Ricoh servicioRicoh;
        FuncionesFormularios funcion = new FuncionesFormularios();
        AccionesLógica NuevaAccion = new AccionesLógica();
        LogicaModulosCliente lgModuloCliente = new LogicaModulosCliente();
        Modulo_Cliente NuevoModulo = new Modulo_Cliente();

        Servicio DatosServicio = new Servicio();

        bool EstaModificando;
        bool ModuloSeleccionado;
        bool ModulosLlenos = true;
        int Id = 0;
        string NumeroReporte = "";
        string Modelo = "";
        int Contador = 0;
        bool CambioModulo = false;
        bool BuscandoFolio = false;
        bool Validado;

        private string Serie;
        //Variables cuando se cambie el modulo en algun reporte
        private string ClaveAnterior;

        #region Constructores
        //Constructor al agregar nuevo modulo
        public ModuloNuevo(Servicios_Ricoh servicioRicoh, string Modelo, string Serie, string Folio, int Contador, bool EstaModificando)
        {
            InitializeComponent();
            //Asignamos la instancia que ya tenemos de los servicios ricoh, esto para poder manipular funciones en tiempo real
            this.servicioRicoh = servicioRicoh;

            this.Serie = Serie;
            this.EstaModificando = EstaModificando;
            this.Modelo = DeterminarModeloModulo(Modelo);
            this.Contador = Contador;
            this.NumeroReporte = Folio;
            InicioAplicacion();
            txtClaveRetirada.Enabled = EstaModificando;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        public ModuloNuevo(Servicios_Ricoh servicioRicoh, Servicio DatosServicio)
        {
            InitializeComponent();
            //Asignamos la instancia que ya tenemos de los servicios ricoh, esto para poder manipular funciones en tiempo real
            this.servicioRicoh = servicioRicoh;
            this.DatosServicio = DatosServicio;

            this.EstaModificando = false;
            this.DatosServicio.Modelo = DeterminarModeloModulo(DatosServicio.Modelo);
            InicioAplicacion();
            txtClaveRetirada.Enabled = false;

            //Posicionar forma en el centro
            this.StartPosition = FormStartPosition.CenterScreen;
            ClaveAnterior = "";
        }

        //Constructor al querer modificar modulo de un cliente
        public ModuloNuevo(Servicios_Ricoh servicioRicoh, string Modelo, Modulo_Cliente NuevoModulo, string Serie, string Folio, int Contador, bool EstaModificando, bool RetirarModulo, bool BuscandoFolio)
        {
            InitializeComponent();
            //Asignamos la instancia que ya tenemos de los servicios ricoh, esto para poder manipular funciones en tiempo real
            this.servicioRicoh = servicioRicoh;

            //Agregamos los parametros a nuestras variables que no seran de utilidad
            this.Serie = Serie;
            this.EstaModificando = EstaModificando;
            this.Modelo = DeterminarModeloModulo(Modelo);
            this.Contador = Contador;
            this.NumeroReporte = Folio;
            this.NuevoModulo = NuevoModulo;
            InicioAplicacion();
            this.StartPosition = FormStartPosition.CenterScreen;
            LlenarDatosModulo();

            //Habrá un cambio de modulo
            CambioModulo = RetirarModulo;
            ModuloSeleccionado = true;
            VerificarRetiroModulo(RetirarModulo);
            txtClaveRetirada.Enabled = false;

            //Deshabilitamos cambiar de modelo y modulo
            //cboModelo.Enabled = false;
            cboModulos.Enabled = false;
            this.BuscandoFolio = BuscandoFolio;
            //chkCambioClave.Visible = true;
            //cboModelo.SelectedItem= this.Modelo;
        }

        public ModuloNuevo(Servicios_Ricoh servicioRicoh, Modulo_Cliente NuevoModulo, Servicio DatosServicio, bool EstaModificando, bool RetirarModulo, bool BuscandoFolio)
        {
            InitializeComponent();
            //asignamos la instancia que ya tenemos de los servicios ricoh, esto para poder manipular funciones en tiempo real
            this.servicioRicoh = servicioRicoh;
            this.DatosServicio = DatosServicio;
            this.DatosServicio.Modelo = DeterminarModeloModulo(DatosServicio.Modelo);
            this.NuevoModulo = NuevoModulo;

            //agregamos los parametros a nuestras variables que no seran de utilidad

            this.EstaModificando = EstaModificando;
            InicioAplicacion();
            this.StartPosition = FormStartPosition.CenterScreen;
            LlenarDatosModulo();

            //Habrá un cambio de modulo
            CambioModulo = RetirarModulo;
            ModuloSeleccionado = true;
            VerificarRetiroModulo(RetirarModulo);
            txtClaveRetirada.Enabled = false;
            cboModulos.Enabled = false;
            this.BuscandoFolio = BuscandoFolio;
        }

        //Constructor para cuando se quiera instalar y retirar algun modulo sin tener que seleccionar previamente uno
        public ModuloNuevo(Servicios_Ricoh servicioRicoh, string Modelo, string Serie, string Folio, int Contador, bool EstaModificando, bool RetirarModulo)
        {
            InitializeComponent();
            //Instancia de servico ricoh, usar metodos en tiempo real
            this.servicioRicoh = servicioRicoh;

            this.Modelo = DeterminarModeloModulo(Modelo);
            this.Serie = Serie;
            this.EstaModificando = EstaModificando;
            this.NumeroReporte = Folio;
            this.Contador = Contador;
            InicioAplicacion();
            this.StartPosition = FormStartPosition.CenterScreen;
            CambioModulo = RetirarModulo;
            VerificarRetiroModulo(RetirarModulo);
            txtClaveRetirada.Enabled = !EstaModificando;
            ModuloSeleccionado = EstaModificando;

            //Habilitamos para poder seleccionar el tipo de modulo
            cboModulos.Enabled = true;
        }

        public void VerificarRetiroModulo(bool RetiroDeModulo)
        {
            if (RetiroDeModulo)
            {
                MostrarControlesCambioClave(true);
                if (ModuloSeleccionado)
                {
                    //En base al modulo que hayamos seleccionado, obtendremos las claves disponibles en bodega
                    NuevoModulo.IdModelo = NuevaAccion.BuscarId(DatosServicio.Modelo, "spObtenerIdModeloModulo");
                    int IdModulo = lgModuloCliente.BuscarIdModulo(cboModulos.SelectedItem.ToString(), NuevoModulo.IdModelo);
                    //int IdModulo = NuevaAccion.BuscarId(cboModulos.SelectedItem.ToString(), "ObtenerIdModuloCliente");
                    funcion.LlenarComboBox(cboClaves, "SeleccionarModuloBodega", IdModulo);
                }

                //Se esta retirando modulos no modificando por lo tanto se agergaran 2 nuevos registros(retiro de modulo e instalacion de modulo)
                EstaModificando = false;
            }
        }

        public string DeterminarModeloModulo(string Modelo)
        {
            var prefijosValidos = new HashSet<string> { "MP-400", "MP-500", "90" };
            //Para saber si se trata de un fusor
            if (prefijosValidos.Any(prefijo => Modelo.StartsWith(prefijo)))
            {
                return "4002";
            }

            return "5054";
        }

        //Utilizado cuando se va a modificar un modulo en la observacion o de la clave
        public void LlenarDatosModulo()
        {
            Id = NuevoModulo.Id;
            //cboModelo.SelectedItem = NuevoModulo.Modelo;
            cboModulos.SelectedItem = NuevoModulo.Modulo;
            txtClave.Text = NuevoModulo.Clave;
            //txtPaginas.Text = NuevoModulo.Paginas.ToString();
            ClaveAnterior = NuevoModulo.Clave;
            rtxtObsrevacion.Text = NuevoModulo.Observacion;
        }
        #endregion

        #region Inicio
        public void InicioAplicacion()
        {
            //Llenar los modulos correspondientes dependiendo el modelo del equipo
            int IdModelo = NuevaAccion.BuscarId(DatosServicio.Modelo, "spObtenerIdModeloModulo");
            funcion.LlenarComboBox(cboModulos, "spSeleccionarModulos", IdModelo);
        }
        #endregion

        #region PanelSuperior
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lparam);


        private void panelSuperior_MouseDown_1(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion

        #region Validaciones

        public bool ValidarCamposModulos()
        {
            Validado = true;
            erModulo.Clear();
            ValidarCampo(cboModulos, "Seleccione un modulo");

            if (CambioModulo)
            {
                ValidarCampo(cboClaves, "Seleccione una clave del inventario");
            }
            else
            {
                ValidarCampo(txtClave, "Escriba una clave para el modulo");
            }


            return Validado;
        }

        public void ValidarCampo(Control c, string Mensaje)
        {
            if (c is TextBox || c is RichTextBox)
            {
                if (string.IsNullOrEmpty(c.Text))
                {
                    erModulo.SetError(c, Mensaje);
                    Validado = false;
                }
            }
            else if (c is ComboBox)
            {
                ComboBox combo = (ComboBox)c;
                if (combo.SelectedIndex == -1 || combo.SelectedIndex == 0)
                {
                    erModulo.SetError(c, Mensaje);
                    Validado = false;
                }
            }
        }
        #endregion
        #region Botones
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region AgregarModificar
        private void btnAgregarModulo_Click(object sender, EventArgs e)
        {
            if (!ValidarCamposModulos())
                return;

            try
            {
                string Resultado;
                Modulo_Cliente NuevoModulo = new Modulo_Cliente()
                {
                    Id = Id,
                    //IdModelo = NuevaAccion.BuscarId(this.Modelo, "spObtenerIdModeloModulo"),
                    //Reporte = NumeroReporte,
                    //Serie = Serie,
                    //Paginas = Contador,
                    IdModelo = NuevaAccion.BuscarId(this.DatosServicio.Modelo, "ObtenerIdModeloModulo"),
                    Reporte = this.DatosServicio.NumeroFolio,
                    Serie = this.DatosServicio.Serie,
                    Paginas = this.DatosServicio.Contador,
                    Clave = (CambioModulo) ? cboClaves.SelectedItem.ToString() : txtClave.Text,
                    ClaveAnterior = ClaveAnterior,
                    Observacion = rtxtObsrevacion.Text
                };
                ColocarClaveModulo(NuevoModulo);

                if (CambioModulo)
                {
                    ColocarClaveAnterior(NuevoModulo);
                }

                NuevoModulo.IdModulo = lgModuloCliente.BuscarIdModulo(cboModulos.SelectedItem.ToString(), NuevoModulo.IdModelo);
                if (EstaModificando)
                {
                    if (MessageBox.Show("¿Esta seguro de modificar el registro?", "CONFIRME LA MODIFICACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        MessageBox.Show("Modificacion cancelada!!", "CANCELADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    NuevoModulo.Estado = NuevoModulo.Estado;

                    //CHECAR
                    if (NuevoModulo.Observacion != "" && BuscandoFolio == false)
                    {
                        //Al añadir una observacion se agregara un nuevo registro al historial
                        NuevoModulo.Estado = "ACTUALIZADO";
                        NuevoModulo.Id = 0;
                        lgModuloCliente.RegistrarModulo(NuevoModulo, "AgregarEstadoModuloEquipo");

                        //Eliminamos la clave para que al guardar el reporte no se actualice nuevamente la clave a la cual se agrego una observacion
                        EliminarClaveDeLista(NuevoModulo.Clave);

                        //Agregarla a la lista de claves con observaciones para que no se limpie la observacion que se acaba de añadir
                        LogicaModulosCliente.ClavesObservaciones.Add(NuevoModulo.Clave);
                    }
                    else//Añadir observacion a una clave de un reporte buscado
                    {
                        Resultado = lgModuloCliente.RegistrarModulo(NuevoModulo, "ModificarEstadoModuloEquipo");
                        MessageBox.Show(Resultado, "REGISTRO INVENTARIO MODULOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    //ASI ES COMO ESTABA ANTES
                    //Resultado = lgModuloCliente.RegistrarModulo(NuevoModulo, "ModificarEstadoModuloEquipo");
                    //MessageBox.Show(Resultado, "REGISTRO INVENTARIO MODULOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    servicioRicoh.MostrarModulosCliente(DatosServicio.Serie, false);
                }
                else//Agregamos al catalogo
                {
                    if (CambioModulo)
                    {
                        //Retiramos el modulo
                        CambiarModulo(NuevoModulo);
                    }
                    ////Comprobar si no hubo retiro de modulo
                    //if (ComprobarCambioModulo(NuevoModulo))
                    //{
                    //    //Agregamos el modulo que se esta retirando
                    //    ModificarHistorialModulos(NuevoModulo);
                    //    NuevoModulo.Id = 0;
                    //}
                    else
                    {
                        //Verificar que no tenga el modulo ya instalado
                        bool ModuloAsignado = lgModuloCliente.VerificarModuloAsignado(NuevoModulo.Serie, NuevoModulo.IdModulo);
                        if (ModuloAsignado)
                        {
                            MessageBox.Show("¡¡ESTE EQUIPO YA CUENTA CON UN MODULO ASIGNADO!!", "MODULO ASIGNADO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        //Validar que la clave no se encuentre en otro equipo
                        bool ClaveAsignada = lgModuloCliente.VerificarClaveDuplicada(NuevoModulo.Clave);
                        if (ClaveAsignada)
                        {
                            MessageBox.Show("CLAVE ASIGNADA EN EL EQUIPO " + lgModuloCliente.ObtenerSerieClaveRepetida(NuevoModulo.Clave), "!CLAVE YA EXISTE EN OTRO EQUIPO!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    NuevoModulo.Estado = "INSTALADO";
                    //Agregamos la clave a la lista
                    LogicaModulosCliente.ClavesObservaciones.Add(NuevoModulo.Clave);
                    Resultado = lgModuloCliente.RegistrarModulo(NuevoModulo, "AgregarEstadoModuloEquipo");
                    MessageBox.Show(Resultado, "REGISTRO MODULO EN EQUIPO DE CLIENTE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (CambioModulo)
                    {
                        servicioRicoh.MostrarModulosCliente(DatosServicio.Serie, false);
                        //Cambiaremos el modulo al nuevo que se instalo
                        lgModuloCliente.GuardarClaveDeSerie(NuevoModulo, true);
                    }
                    else
                    {
                        servicioRicoh.MostrarModulosCliente(DatosServicio.Serie, true);
                        //Se colocara el modulo que se acaba de instalar
                        lgModuloCliente.GuardarClaveDeSerie(NuevoModulo, false);
                    }
                }
                //Actualizamos en el formulario de servicios ricoh

                servicioRicoh.ReiniciarCapturaModulo();
                //Reiniciar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error " + ex.Message);
            }
            //MessageBox.Show(Serie);
            this.Close();
        }

        //Metodo que nos permitira saber cuando se retire o actualice un modulo, o en dado cado de que se instale uno
        public void ColocarClaveModulo(Modulo_Cliente NuevoModulo)
        {
            //if (ModuloSeleccionado)
            //{
            //    if (EstaModificando)
            //    {
            //        NuevoModulo.Clave = txtClave.Text;
            //        return;
            //    }
            //    NuevoModulo.Clave = cboClaves.SelectedItem.ToString();
            //}
            //else
            //{
            //    NuevoModulo.Clave = txtClave.Text;
            //}
        }

        public void ColocarClaveAnterior(Modulo_Cliente NuevoModulo)
        {
            //if (!ModuloSeleccionado)
            //{
            //    NuevoModulo.ClaveAnterior = txtClaveRetirada.Text;
            //    ClaveAnterior = txtClaveRetirada.Text;
            //}
            NuevoModulo.ClaveAnterior = txtClaveRetirada.Text;
            ClaveAnterior = txtClaveRetirada.Text;
        }


        #endregion
        #endregion

        #region RetiroDeModulo
        public void MostrarControlesCambioClave(bool mostrar)
        {
            cboClaves.Visible = ModuloSeleccionado;
            txtClave.Visible = !ModuloSeleccionado;
            txtClaveRetirada.Visible = mostrar;
            lblAnteriorClave.Visible = mostrar;
            CambioModulo = mostrar;
            txtClaveRetirada.Text = NuevoModulo.Clave;
        }


        public bool ComprobarCambioModulo(Modulo_Cliente NuevoModulo)
        {
            //Posiblemente algunas cosas esten de más
            if (ClaveAnterior != NuevoModulo.Clave && CambioModulo)
            {
                MessageBox.Show("Se instalo " + NuevoModulo.Clave + " y se retiro " + ClaveAnterior);
                NuevoModulo.ClaveAnterior = ClaveAnterior;
                EliminarClaveDeLista(ClaveAnterior);
                CambioModulo = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CambiarModulo(Modulo_Cliente NuevoModulo)
        {
            MessageBox.Show("Se instalo " + NuevoModulo.Clave + " y se retiro " + ClaveAnterior);
            NuevoModulo.ClaveAnterior = ClaveAnterior;
            EliminarClaveDeLista(ClaveAnterior);
            CambioModulo = true;

            //Agregamos el modulo que se esta retirando al historial
            lgModuloCliente.RetirarModulo(NuevoModulo, ModuloSeleccionado);
            NuevoModulo.Estado = "INSTALADO";
            NuevoModulo.Id = 0;
        }

        public void EliminarClaveDeLista(string Clave)
        {
            ModuloEquipo ModuloAEliminar = LogicaModulosCliente.ModulosEquipo.FirstOrDefault(Modulo => Modulo.Clave == Clave);

            if (ModuloAEliminar != null)
            {
                LogicaModulosCliente.ModulosEquipo.Remove(ModuloAEliminar);
            }
        }
        #endregion



        #region Eventos
        private void cboModulos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ModuloNuevo_Load(object sender, EventArgs e)
        {
            //this.Location = new Point(300, 300);

        }


        #endregion

    }
}
