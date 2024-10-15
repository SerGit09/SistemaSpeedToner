using CobranzaSP.Lógica;
using DocumentFormat.OpenXml;
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

namespace CobranzaSP.Formularios
{
    public partial class ServicioReportes : Form
    {
        public ServicioReportes()
        {
            InitializeComponent();
            InicioAplicacion();
        }
        //Variable para validaciones
        bool Validado = true;
        
        //Variables para generar el reporte
        string Parametro = " ";
        string TipoBusqueda = " ";
        string TipoBusquedaAdicional = "";
        string Cliente = "";

        AccionesLógica NuevaAccion = new AccionesLógica();
        CD_Conexion cn = new CD_Conexion();
        LogicaReporteServicio LgReporteServicio = new LogicaReporteServicio();

        #region Inicio
        public void InicioAplicacion()
        {
            string[] OpcionesMostrar = { "", "Cliente", "Serie", "Fusor", "Fecha" };
            cboOpcionReporte.Items.AddRange(OpcionesMostrar);
        }

        public void LlenarComboBox(ComboBox cb, string sp, int Marca)
        {
            SqlDataReader dr;
            cb.Items.Clear();
            if (Marca != 0 || sp == "SeleccionarModelos")
            {
                dr = NuevaAccion.LlenarComboBoxEspecifico(sp, Marca);
            }
            else
            {
                dr = NuevaAccion.LlenarComboBox(sp);
            }

            while (dr.Read())
            {
                //Agregamos las opciones dependiendo los registros que nos devolvieron
                cb.Items.Add(dr[0].ToString());
            }
            //Agregamos un espacio en blanco y lo asignamos como opcion por defecto
            dr.Close();

            cb.Items.Insert(0, " ");
            cb.SelectedIndex = 0;
            cn.CerrarConexion();
        }
        #endregion

        #region Validaciones
        public bool DeterminarTipoValidacionDatos()
        {
            Validado = true;
            erReporte.Clear();
            switch (TipoBusqueda)
            {
                case "Serie":
                    ValidarCampo(cboOpcionElegida, "Seleccione una serie");
                    break;
                case "Fusor":
                    if (rdUnFusor.Checked)
                    {
                        ValidarCampo(cboFusor, "Seleccione una clave de fusor");
                    }
                    break;
                case "Cliente":
                    ValidarCampo(cboOpcionElegida, "Seleccione un cliente");
                    break;
                default: 
                    ValidarSeleccionDeMarca();break;
            }
            return Validado;
        }

        public void ValidarCampo(Control c, string Mensaje)
        {
            if (c is TextBox || c is RichTextBox)
            {
                if (string.IsNullOrEmpty(c.Text))
                {
                    erReporte.SetError(c, Mensaje);
                    Validado = false;
                }
            }
            else if (c is ComboBox)
            {
                ComboBox combo = (ComboBox)c;
                if (combo.SelectedIndex == -1 || combo.SelectedIndex == 0)
                {
                    erReporte.SetError(c, Mensaje);
                    Validado = false;
                }
            }
        }

        public void ValidarSeleccionDeMarca()
        {
            if (rdUnaMarca.Checked)
            {
                ValidarCampo(cboMarca, "Seleccione una marca");
                if (radUnModelo.Checked)
                {
                    ValidarCampo(cboModelo, "Seleccione un modelo");
                }
            }
        }
        #endregion

        #region OpcionesReportes
        private void cboOpcionReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReiniciarOpcionesReporte();
            btnGenerarReporte.Enabled = true;
            TipoBusqueda = cboOpcionReporte.SelectedItem.ToString();
            grpMarca.Visible = false;
            switch (TipoBusqueda)
            {
                case "Serie":
                    MostrarComboBox("SeleccionarSeriesEquipos", true); break;
                case "Fecha":
                    chkRango.Visible = false;
                    chkRango.Checked= true;
                    grpMarca.Visible = true;
                    break;
                case "Fusor":
                    grpFusor.Visible = true;
                    chkRango.Visible = true;
                    rdTodosLosFusores.Checked = true;
                    break;
                case "Todos Los Fusores":
                    //TipoBusqueda = "Fusor";
                    chkRango.Visible = true; break;
                case "Cliente":
                    MostrarComboBox("SeleccionarClientesServicios", true);
                    grpMarca.Visible = true;//Mostramos opciones de las marcas
                    TipoBusqueda = "Cliente";
                    break;
            }
        }

        //Metodo que nos ayudara a llenar el combobox, dependiendo el stored procedure que le pasemos y si queremos tener o no un rango de fechas definido
        private void MostrarComboBox(string sp, bool MostrarRango)
        {
            cboOpcionElegida.Visible = true;
            LlenarComboBox(cboOpcionElegida, sp, 0);

            chkRango.Visible = MostrarRango;
            //erReporte.Clear();
        }

        private void ReiniciarOpcionesReporte()
        {
            MostrarRangosFechas(false);
            cboOpcionElegida.Visible = false;
            grpFusor.Visible = false;
            chkRango.Visible = false;
            chkRango.Checked = false;
            Cliente = "";
            TipoBusqueda = "";
            Parametro = "";
            TipoBusquedaAdicional = "";
            grpMarca.Visible = false;
            grpModelo.Visible = false;
            erReporte.Clear();
        }

        private void MostrarRangosFechas(bool mostrar)
        {
            lblFechaFinal.Visible = mostrar;
            lblFechaInicio.Visible = mostrar;
            dtpFechaFinal.Visible = mostrar;
            dtpFechaInicial.Visible = mostrar;
            chkRango.Checked = mostrar;
        }

        private void rdTodasLasMarcas_CheckedChanged(object sender, EventArgs e)
        {
            cboMarca.Visible = false;
            grpModelo.Visible = false;
            rdTodosLosModelos.Checked = true;
            TipoBusqueda = cboOpcionReporte.SelectedItem.ToString();
        }

        //Evento que nos llena de las marcas con las que contamos para poder elegir alguna
        private void rdUnaMarca_CheckedChanged(object sender, EventArgs e)
        {
            cboMarca.Visible = true;
            rdTodosLosModelos.Checked = true;
            LlenarComboBox(cboMarca, "SeleccionarMarca", 0);
        }

        //Evento dependiendo si escogemos una opcion, nos mostrara las opciones para elegir uno o todos los modelos
        private void cboMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMarca.SelectedItem.ToString() != " ")
            {
                grpModelo.Visible = true;
            }
        }

        //Evento que nos ayudara a llenar los modelos en el combo box dependiendo la marca que se haya seleccionado
        private void radUnModelo_CheckedChanged(object sender, EventArgs e)
        {
            cboModelo.Visible = true;
            int IdMarca = NuevaAccion.BuscarId(cboMarca.SelectedItem.ToString(), "ObtenerIdMarca");
            LlenarComboBox(cboModelo, "SeleccionarModelos", IdMarca);
        }

        //Evento que nos ayudara a mostrar la fecha incicial y final, en dado caso que asi se requiera en el inventario
        private void chkRango_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRango.Checked)
                MostrarRangosFechas(true);
            else
                MostrarRangosFechas(false);
        }

        //Evento para que en dado caso de que este seleccionado, no mostrará los modelos
        private void rdTodosLosModelos_CheckedChanged(object sender, EventArgs e)
        {
            cboModelo.Visible = false;
        }
        private void rdUnFusor_CheckedChanged(object sender, EventArgs e)
        {
            cboFusor.Visible = true;
            LlenarComboBox(cboFusor, "SeleccionarFusores", 0);
            chkRango.Visible = false;
        }

        private void rdTodosLosFusores_CheckedChanged(object sender, EventArgs e)
        {
            cboFusor.Visible = false;
            chkRango.Visible = true;
        }

        private void cboOpcionElegida_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboOpcionElegida.SelectedItem.ToString() != "")
            {
                rdTodasLasMarcas.Checked = true;
            }
        }
        #endregion

        #region PanelSuperior
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            if (!DeterminarTipoValidacionDatos())
                return;

            try
            {
                
                Reporte nuevoReporte = new Reporte()
                {
                    FechaInicio = dtpFechaInicial.Value,
                    FechaFinal = dtpFechaFinal.Value,
                    RangoHabilitado = chkRango.Checked,
                    TipoBusqueda = cboOpcionReporte.SelectedItem.ToString(),
                    TipoBusquedaAdicional = TipoBusquedaAdicional
                };
                VerificarSeleccionCliente(nuevoReporte);
                VerificarParametroBusqueda(nuevoReporte);
                //MessageBox.Show(nuevoReporte.TipoBusqueda);
                bool DatosEncontrados = LgReporteServicio.DeterminarTipoReporte(nuevoReporte);
                

                if (!DatosEncontrados)
                    MessageBox.Show("¡NO SE ENCONTRARON REGISTROS!", "DATOS NO ENCONTRADOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ocurrio un error " + ex.Message);
            }
        }

        public void VerificarSeleccionCliente(Reporte nuevoReporte)
        {
            if(TipoBusqueda == "Cliente")
            {
                nuevoReporte.Cliente = cboOpcionElegida.SelectedItem.ToString();
            }
            else
            {
                nuevoReporte.Cliente = "";
            }
        }
        

        public void VerificarParametroBusqueda(Reporte nuevoReporte)
        {
            nuevoReporte.ParametroBusqueda = "";
            switch (nuevoReporte.TipoBusqueda)
            {
                case "Serie":
                    nuevoReporte.ParametroBusqueda = cboOpcionElegida.SelectedItem.ToString();
                    break;
                case "Fusor":
                    if (rdUnFusor.Checked)
                    {
                        nuevoReporte.ParametroBusqueda = cboFusor.SelectedItem.ToString();
                    }
                    break;
                case "Cliente":
                    nuevoReporte.ParametroBusqueda = "";
                    nuevoReporte.Cliente = cboOpcionElegida.SelectedItem.ToString();
                    VerificarMarcaEspecifica(nuevoReporte);
                    break;
                case "Fecha":
                    VerificarMarcaEspecifica(nuevoReporte);
                    break;
                default:
                    VerificarMarcaEspecifica(nuevoReporte); break;
            }
        }

        public void VerificarMarcaEspecifica(Reporte nuevoReporte)
        {
            //Cliente = cboOpcionElegida.SelectedItem.ToString();
            if (rdUnaMarca.Checked)
            {
                nuevoReporte.TipoBusquedaAdicional = "Marca";
                nuevoReporte.ParametroBusqueda = cboMarca.SelectedItem.ToString();
            }
            if (radUnModelo.Checked)
            {
                nuevoReporte.TipoBusquedaAdicional = "Modelo";
                nuevoReporte.ParametroBusqueda = cboModelo.SelectedItem.ToString();
            }
        }

    }

}
