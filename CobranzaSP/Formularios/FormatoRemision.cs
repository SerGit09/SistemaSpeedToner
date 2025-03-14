using CobranzaSP.Lógica;
using CobranzaSP.Modelos;
using DocumentFormat.OpenXml.Spreadsheet;
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
    public partial class FormatoRemision : Form
    {
        public FormatoRemision()
        {
            InitializeComponent();
            InicioAplicacion();
        }
        RemisionLogica Remision = new RemisionLogica();
        FuncionesFormularios FuncionFormulario = new FuncionesFormularios();
        AccionesLógica NuevaAccion = new AccionesLógica();
        double Total;

        string TablaProductos;
        int posicion;
        bool Validado;


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

        #region Validaciones

        public bool ValidarCamposRemision()
        {
            Validado = true;
            erRemision.Clear();

            ValidarCampo(txtFolio, "Escriba el folio de la remision");

            if(dtgProductos.Rows.Count == 0)
            {
                MessageBox.Show("Eliga un producto", "DATOS VACIOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Validado = false;
            }


            return Validado;
        }

        public bool ValidarCamposProductos()
        {
            Validado = true;
            erRemision.Clear();

            ValidarCampo(cboMarcas, "Escoja una marca");
            ValidarCampo(cboModelos, "Escoja un modelo");
            ValidarCampo(txtCantidad, "Escriba la cantidad");
            ValidarCampo(txtPrecio, "Escriba el precio");

            return Validado;
        }

        public void ValidarCampo(System.Windows.Forms.Control c, string Mensaje)
        {
            if (c is TextBox || c is RichTextBox)
            {
                if (string.IsNullOrEmpty(c.Text))
                {
                    erRemision.SetError(c, Mensaje);
                    Validado = false;
                }
            }
            else if (c is ComboBox)
            {
                ComboBox combo = (ComboBox)c;
                if (combo.SelectedIndex == -1 || combo.SelectedIndex == 0)
                {
                    erRemision.SetError(c, Mensaje);
                    Validado = false;
                }
            }
        }
        #endregion

        #region Inicio
        public void InicioAplicacion()
        {
            FuncionFormulario.PropiedadesDtg(dtgProductos);
            TitulosTabla();

            FuncionFormulario.LlenarComboBox(cboMarcas, "SeleccionarMarca");

            ActivarDesactivarBotones(false);
        }

        public void TitulosTabla()
        {
            dtgProductos.Columns.Add("Cantidad", "Cantidad");
            dtgProductos.Columns.Add("Concepto","Concepto");
            dtgProductos.Columns.Add("Precio","Precio");
            dtgProductos.Columns.Add("Importe","Importe");
        }
        #endregion

        #region Botones
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (!ValidarCamposRemision())
            {
                return;
            }

            LlenarTablaProductos();

            InformacionRemision NuevaRemision = new InformacionRemision()
            {
                Folio = int.Parse(txtFolio.Text),
                Fecha = dtpFecha.Value,
                TablaProductos = TablaProductos,
                Total = Total
            };
            
            Remision.GenerarRemisionEspecial(NuevaRemision);
            Reiniciar();
        }

        public void LlenarTablaProductos()
        {
            TablaProductos = string.Empty;

            foreach(DataGridViewRow row in dtgProductos.Rows)
            {
                TablaProductos += "<tr>";
                double Precio = double.Parse(String.Format("{0:c}", row.Cells[2].Value.ToString()));
                MessageBox.Show(Precio.ToString());

                int Cantidad = int.Parse(row.Cells[0].Value.ToString());
                double Importe = Precio * Cantidad;
                Total += Importe;
                MessageBox.Show(String.Format(Importe.ToString()));

                TablaProductos += "<td class=\"texto-centrado\" colspan=\"1\">" + String.Format("{0:f}", row.Cells[0].Value) + "</td>";
                TablaProductos += "<td class=\"texto-centrado\" colspan=\"3\">" + row.Cells[1].Value.ToString() + "</td>";
                //TablaProductos += "<td class=\"texto-derecha\" colspan=\"1\">" + "$" + decimal.Parse(row.Cells[2].Value.ToString()) + "</td>";
                //TablaProductos += "<td class=\"texto-derecha\" colspan=\"1\">" + "$" + decimal.Parse(row.Cells[3].Value.ToString()) + "</td>";
                //String.Format("{0:c}", ValorRecbido)
                TablaProductos += "<td class=\"texto-derecha\" colspan=\"1\">" + String.Format("{0:c}", row.Cells[2].Value) + "</td>";
                TablaProductos += "<td class=\"texto-derecha\" colspan=\"1\">" + String.Format("{0:c}", row.Cells[3].Value) + "</td>";
                TablaProductos += "</tr>";

            }

            //Crear columnas debajo 

            for (int i = 0; i < 25; i++)
            {
                TablaProductos += "<tr><td colspan=\"6\">&nbsp;</td></tr>";
            }


        }

        #endregion

        private void cboMarcas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMarcas.SelectedItem.ToString() != " ")
            {
                int IdMarca = NuevaAccion.BuscarId(cboMarcas.SelectedItem.ToString(), "ObtenerIdMarca");
                FuncionFormulario.LlenarComboBox(cboModelos, "spSeleccionarCartuchosMarca", IdMarca);
            }
        }

        #region TablaProductos
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCamposProductos())
            {
                return;
            }

            int indice_fila = dtgProductos.Rows.Add();
            DataGridViewRow fila = dtgProductos.Rows[indice_fila];

            fila.Cells[0].Value = double.Parse(txtCantidad.Text);
            double Cantidad = double.Parse(txtCantidad.Text);

            string concepto = cboMarcas.SelectedItem.ToString() + " " + cboModelos.SelectedItem.ToString();
            fila.Cells[1].Value = concepto;
            fila.Cells[2].Value = double.Parse(txtPrecio.Text);
            double Precio = double.Parse(txtPrecio.Text);
            double Importe = Precio * Cantidad;
            fila.Cells[3].Value = Importe;
            
            LimpiarFormularioProductos();
        }
        public void LimpiarFormularioProductos()
        {
            foreach (System.Windows.Forms.Control c in grpProductos.Controls)
            {
                if (c is TextBox || c is RichTextBox)
                {
                    c.Text = "";
                }

                if(c is ComboBox combo)
                {
                    combo.SelectedIndex = 0;
                }
            }
        }
        private void dtgProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            dtgProductos.Rows.RemoveAt(posicion);
            btnEliminar.Enabled = false;
            posicion = 0;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ActivarDesactivarBotones(false);
        }

        private void dtgProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ActivarDesactivarBotones(true);
            if(dtgProductos.Rows.Count > 0)
            {
                posicion = dtgProductos.CurrentRow.Index;
            }
        }


        #endregion
        public void Reiniciar()
        {
            Total = 0;
            LimpiarFormularioProductos();
            txtFolio.Text = "";
            dtpFecha.Value = DateTime.Now;
            dtgProductos.Rows.Clear();
        }


        public void ActivarDesactivarBotones(bool Activar)
        {
            btnEliminar.Enabled = Activar;
            btnCancelar.Enabled = Activar;
        }
    }
}
