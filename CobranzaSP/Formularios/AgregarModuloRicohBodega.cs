using CobranzaSP.Lógica;
using CobranzaSP.Modelos;
using DocumentFormat.OpenXml.Office2010.Excel;
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
    public partial class AgregarModuloRicohBodega : Form
    {
        public AgregarModuloRicohBodega(ModuloNuevo Modulo, string ModeloModulo, string TipoModulo)
        {
            InterfazModuloNuevo = Modulo;
            this.Modulo = TipoModulo;
            this.ModeloModulo = ModeloModulo;
            InitializeComponent();
            InicioAplicacion();
        }
        ModuloNuevo InterfazModuloNuevo;
        AccionesLógica NuevaAccion = new AccionesLógica();
        FuncionesFormularios Formulario = new FuncionesFormularios();
        LogicaModulosBodega lgModuloBodega = new LogicaModulosBodega();
        bool Validado;
        string ModeloModulo;
        string Modulo;

        #region Inicio
        public void InicioAplicacion()
        {
            
        }
        #endregion

        #region Validaciones
        public bool ValidarCamposCatalogo()
        {
            Validado = true;
            erInventarioModulos.Clear();

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

        #region PanelSuperior
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lparam);
        private void pSuperior_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCamposCatalogo())
                return;
            try
            {
                string Resultado;
                ModuloBodega NuevoModulo = new ModuloBodega()
                {
                    Id = 0,
                    IdModelo = NuevaAccion.BuscarId(ModeloModulo, "spObtenerIdModeloModulo"),
                    Clave = txtClave.Text
                };

                NuevoModulo.IdModulo = lgModuloBodega.BuscarIdModulo(Modulo, NuevoModulo.IdModelo);

                bool ClaveRepetida = NuevaAccion.VerificarDuplicados(NuevoModulo.Clave, "VerificarClaveDuplicadaBodega");
                if (ClaveRepetida)
                {
                    MessageBox.Show("¡¡EL NUMERO DE FOLIO YA EXISTE!!", "DUPLICADO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Resultado = lgModuloBodega.GuardarModulo(NuevoModulo);
                MessageBox.Show(Resultado, "REGISTRO INVENTARIO MODULOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                InterfazModuloNuevo.LlenarClavesModulos();
                
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error " + ex.Message);
            }
        }
    }
}
