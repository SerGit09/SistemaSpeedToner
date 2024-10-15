using CobranzaSP.Lógica;
using CobranzaSP.Modelos;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CobranzaSP.Formularios
{
    public partial class NuevoModeloParte : Form
    {
        public NuevoModeloParte(InventarioPartes Inventario)
        {
            InventarioPartes = Inventario;
            InitializeComponent();
            InicioAplicacion();
        }

        FuncionesFormularios Formulario = new FuncionesFormularios();
        LogicaInventarioPartesRicoh lgInventarioPartes = new LogicaInventarioPartesRicoh();
        InventarioPartes InventarioPartes = new InventarioPartes();
        AccionesLógica NuevaAccion = new AccionesLógica();
        bool Validado;

        #region Inicio
        public void InicioAplicacion()
        {
            Formulario.LlenarComboBox(cboMarcas, "SeleccionarMarca", 0);
            Formulario.LlenarComboBox(cboModelos, "SeleccionarModelos", 0);
        }
        #endregion

        #region PanelSuperior
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lparam);
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Validaciones
        public bool ValidarCampos()
        {
            Validado = true;
            erNuevoModelo.Clear();

            ValidarCampo(txtNuevoModelo);
            

            return Validado;
        }

        public bool ValidarCamposListas()
        {
            Validado = true;
            erNuevoModelo.Clear();
            ValidarCampo(cboMarcas);
            ValidarCampo(cboModelos);

            return Validado;
        }


        public void ValidarCampo(Control c)
        {
            if (c is TextBox || c is RichTextBox)
            {
                if (string.IsNullOrEmpty(c.Text))
                {
                    erNuevoModelo.SetError(c, "Campo Obligatorio");
                    Validado = false;
                }
            }
            else if (c is ComboBox)
            {
                ComboBox combo = (ComboBox)c;
                if (combo.SelectedIndex == -1 || combo.SelectedIndex == 0)
                {
                    erNuevoModelo.SetError(c, "Seleccione una opcion");
                    Validado = false;
                }
            }
        }
        #endregion

        #region ListaModelos
        private void btnAgregarLista_Click(object sender, EventArgs e)
        {
            if (!ValidarCamposListas())
            {
                return;
            }
            string ModeloEquipo = cboModelos.SelectedItem.ToString();
            if (!lstModelosEquipos.Items.Contains(ModeloEquipo))//Validar Modelos repetidos
            {
                lstModelosEquipos.Items.Add(ModeloEquipo);
                ReiniciarComboBox();
            }
            else
            {
                MessageBox.Show("Modelo de Equipo ya esta en lista");
                ReiniciarComboBox();
            }
        }

        public void ReiniciarComboBox()
        {
            cboMarcas.SelectedIndex = 0;
            cboModelos.SelectedIndex = 0;
        }

        private void btnEliminarLista_Click(object sender, EventArgs e)
        {
            lstModelosEquipos.Items.Remove(lstModelosEquipos.SelectedItem);
        }
        #endregion

        private void btnGuardarModelo_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
            {
                return;
            }

            if(lstModelosEquipos.Items.Count == 0)
            {
                MessageBox.Show("DEBE AGREGAR AL MENOS UN MODELO DE EQUIPO PARA ESTE MODELO DE PARTE", "LISTAS DE MODELOS VACIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string ModeloParte = txtNuevoModelo.Text;
            string Mensaje = lgInventarioPartes.GuardarModeloParte(ModeloParte);
            //Agregamos los modelos de equipos de la lista a nuestra tabla relacional
            foreach (string modelo in lstModelosEquipos.Items)
            {
                ModeloParte NuevoModelo = new ModeloParte()
                {
                    IdModelo = NuevaAccion.BuscarId(modelo, "ObtenerIdModelo"),
                    IdModeloParte = NuevaAccion.BuscarId(ModeloParte, "spObtenerIdModeloPartes")
                };
                lgInventarioPartes.GuardarRelacionModeloPartes(NuevoModelo);
            }
            MessageBox.Show(Mensaje, "REGISTRO MODELO PARTES", MessageBoxButtons.OK, MessageBoxIcon.Information);
            InventarioPartes.RecargarModelosPartes();//Actualizar el combobox de modelos en la interfaz de Inventario de Partes
            this.Close();//Cerrar interfaz de nuevo modelo
        }

        private void cboMarcas_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboModelos.Enabled = false;//Deshabilitar combobox Modelo
            if (cboMarcas.SelectedItem.ToString() != " ")
            {
                int IdMarca = NuevaAccion.BuscarId(cboMarcas.SelectedItem.ToString(), "ObtenerIdMarca");
                cboModelos.Enabled = true;//Habilitar combobox Modelo
                Formulario.LlenarComboBox(cboModelos, "SeleccionarModelos", IdMarca);
            }
        }
    }
}
