namespace CobranzaSP.Formularios
{
    partial class InventarioPartes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InventarioPartes));
            this.dtgInventario = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabInventario = new System.Windows.Forms.TabPage();
            this.grpDatosInventario = new System.Windows.Forms.GroupBox();
            this.btnNuevoModelo = new System.Windows.Forms.Button();
            this.grpReporteInventario = new System.Windows.Forms.GroupBox();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.cboModelos = new System.Windows.Forms.ComboBox();
            this.txtCantidadExistencia = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rtxtDescripcion = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabRegistros = new System.Windows.Forms.TabPage();
            this.grpDatosRegistro = new System.Windows.Forms.GroupBox();
            this.txtFolio = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboPartesRicoh = new System.Windows.Forms.ComboBox();
            this.btnGenerarReporte = new System.Windows.Forms.Button();
            this.lblTipoPersona = new System.Windows.Forms.Label();
            this.cboProveedores = new System.Windows.Forms.ComboBox();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboModelo = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.erInventarioPartesRicoh = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dtgInventario)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabInventario.SuspendLayout();
            this.grpDatosInventario.SuspendLayout();
            this.grpReporteInventario.SuspendLayout();
            this.tabRegistros.SuspendLayout();
            this.grpDatosRegistro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erInventarioPartesRicoh)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgInventario
            // 
            this.dtgInventario.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgInventario.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dtgInventario.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(90)))));
            this.dtgInventario.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(117)))), ((int)(((byte)(20)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(117)))), ((int)(((byte)(20)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgInventario.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgInventario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.InfoText;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgInventario.DefaultCellStyle = dataGridViewCellStyle2;
            this.dtgInventario.EnableHeadersVisualStyles = false;
            this.dtgInventario.Location = new System.Drawing.Point(12, 284);
            this.dtgInventario.Name = "dtgInventario";
            this.dtgInventario.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(90)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgInventario.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(90)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgInventario.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dtgInventario.RowTemplate.Height = 25;
            this.dtgInventario.Size = new System.Drawing.Size(1326, 681);
            this.dtgInventario.TabIndex = 39;
            this.dtgInventario.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgInventario_CellClick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabInventario);
            this.tabControl1.Controls.Add(this.tabRegistros);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1141, 209);
            this.tabControl1.TabIndex = 35;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabInventario
            // 
            this.tabInventario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(57)))), ((int)(((byte)(80)))));
            this.tabInventario.Controls.Add(this.grpDatosInventario);
            this.tabInventario.Location = new System.Drawing.Point(4, 28);
            this.tabInventario.Name = "tabInventario";
            this.tabInventario.Padding = new System.Windows.Forms.Padding(3);
            this.tabInventario.Size = new System.Drawing.Size(1133, 177);
            this.tabInventario.TabIndex = 0;
            this.tabInventario.Text = "Catalogo";
            // 
            // grpDatosInventario
            // 
            this.grpDatosInventario.Controls.Add(this.btnNuevoModelo);
            this.grpDatosInventario.Controls.Add(this.grpReporteInventario);
            this.grpDatosInventario.Controls.Add(this.cboModelos);
            this.grpDatosInventario.Controls.Add(this.txtCantidadExistencia);
            this.grpDatosInventario.Controls.Add(this.label2);
            this.grpDatosInventario.Controls.Add(this.rtxtDescripcion);
            this.grpDatosInventario.Controls.Add(this.label4);
            this.grpDatosInventario.Controls.Add(this.label1);
            this.grpDatosInventario.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.grpDatosInventario.Location = new System.Drawing.Point(6, 6);
            this.grpDatosInventario.Name = "grpDatosInventario";
            this.grpDatosInventario.Size = new System.Drawing.Size(1111, 156);
            this.grpDatosInventario.TabIndex = 0;
            this.grpDatosInventario.TabStop = false;
            this.grpDatosInventario.Text = "Datos";
            // 
            // btnNuevoModelo
            // 
            this.btnNuevoModelo.FlatAppearance.BorderColor = System.Drawing.Color.Gold;
            this.btnNuevoModelo.FlatAppearance.BorderSize = 2;
            this.btnNuevoModelo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevoModelo.ForeColor = System.Drawing.Color.White;
            this.btnNuevoModelo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNuevoModelo.Location = new System.Drawing.Point(698, 90);
            this.btnNuevoModelo.Name = "btnNuevoModelo";
            this.btnNuevoModelo.Size = new System.Drawing.Size(83, 51);
            this.btnNuevoModelo.TabIndex = 53;
            this.btnNuevoModelo.Text = "Nuevo Modelo";
            this.btnNuevoModelo.UseVisualStyleBackColor = true;
            this.btnNuevoModelo.Click += new System.EventHandler(this.btnNuevoModelo_Click);
            // 
            // grpReporteInventario
            // 
            this.grpReporteInventario.Controls.Add(this.dtpFecha);
            this.grpReporteInventario.Controls.Add(this.btnImprimir);
            this.grpReporteInventario.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.grpReporteInventario.Location = new System.Drawing.Point(787, 16);
            this.grpReporteInventario.Name = "grpReporteInventario";
            this.grpReporteInventario.Size = new System.Drawing.Size(309, 125);
            this.grpReporteInventario.TabIndex = 52;
            this.grpReporteInventario.TabStop = false;
            this.grpReporteInventario.Text = "Reporte Inventario";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Location = new System.Drawing.Point(10, 26);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(293, 27);
            this.dtpFecha.TabIndex = 37;
            // 
            // btnImprimir
            // 
            this.btnImprimir.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnImprimir.FlatAppearance.BorderSize = 2;
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimir.ForeColor = System.Drawing.Color.White;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.Location = new System.Drawing.Point(245, 59);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(58, 49);
            this.btnImprimir.TabIndex = 36;
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // cboModelos
            // 
            this.cboModelos.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboModelos.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboModelos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboModelos.FormattingEnabled = true;
            this.cboModelos.Location = new System.Drawing.Point(79, 16);
            this.cboModelos.Margin = new System.Windows.Forms.Padding(4);
            this.cboModelos.Name = "cboModelos";
            this.cboModelos.Size = new System.Drawing.Size(237, 27);
            this.cboModelos.Sorted = true;
            this.cboModelos.TabIndex = 1;
            this.cboModelos.SelectedIndexChanged += new System.EventHandler(this.cboModelos_SelectedIndexChanged);
            // 
            // txtCantidadExistencia
            // 
            this.txtCantidadExistencia.Location = new System.Drawing.Point(416, 16);
            this.txtCantidadExistencia.Name = "txtCantidadExistencia";
            this.txtCantidadExistencia.Size = new System.Drawing.Size(85, 27);
            this.txtCantidadExistencia.TabIndex = 3;
            this.txtCantidadExistencia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidadExistencia_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(333, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 19);
            this.label2.TabIndex = 51;
            this.label2.Text = "Cantidad:";
            // 
            // rtxtDescripcion
            // 
            this.rtxtDescripcion.Location = new System.Drawing.Point(10, 68);
            this.rtxtDescripcion.Name = "rtxtDescripcion";
            this.rtxtDescripcion.Size = new System.Drawing.Size(370, 73);
            this.rtxtDescripcion.TabIndex = 2;
            this.rtxtDescripcion.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(6, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 19);
            this.label4.TabIndex = 39;
            this.label4.Text = "Descripcion";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 19);
            this.label1.TabIndex = 4;
            this.label1.Text = "Modelo:";
            // 
            // tabRegistros
            // 
            this.tabRegistros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(57)))), ((int)(((byte)(80)))));
            this.tabRegistros.Controls.Add(this.grpDatosRegistro);
            this.tabRegistros.Location = new System.Drawing.Point(4, 28);
            this.tabRegistros.Name = "tabRegistros";
            this.tabRegistros.Padding = new System.Windows.Forms.Padding(3);
            this.tabRegistros.Size = new System.Drawing.Size(1133, 177);
            this.tabRegistros.TabIndex = 1;
            this.tabRegistros.Text = "Movimientos";
            // 
            // grpDatosRegistro
            // 
            this.grpDatosRegistro.Controls.Add(this.txtFolio);
            this.grpDatosRegistro.Controls.Add(this.label5);
            this.grpDatosRegistro.Controls.Add(this.label3);
            this.grpDatosRegistro.Controls.Add(this.cboPartesRicoh);
            this.grpDatosRegistro.Controls.Add(this.btnGenerarReporte);
            this.grpDatosRegistro.Controls.Add(this.lblTipoPersona);
            this.grpDatosRegistro.Controls.Add(this.cboProveedores);
            this.grpDatosRegistro.Controls.Add(this.txtCantidad);
            this.grpDatosRegistro.Controls.Add(this.label7);
            this.grpDatosRegistro.Controls.Add(this.cboModelo);
            this.grpDatosRegistro.Controls.Add(this.label12);
            this.grpDatosRegistro.Controls.Add(this.dtpFechaRegistro);
            this.grpDatosRegistro.Controls.Add(this.label9);
            this.grpDatosRegistro.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.grpDatosRegistro.Location = new System.Drawing.Point(11, 5);
            this.grpDatosRegistro.Name = "grpDatosRegistro";
            this.grpDatosRegistro.Size = new System.Drawing.Size(1111, 166);
            this.grpDatosRegistro.TabIndex = 1;
            this.grpDatosRegistro.TabStop = false;
            this.grpDatosRegistro.Text = "Datos";
            this.grpDatosRegistro.Enter += new System.EventHandler(this.grpDatosRegistro_Enter);
            // 
            // txtFolio
            // 
            this.txtFolio.Location = new System.Drawing.Point(89, 121);
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.Size = new System.Drawing.Size(293, 27);
            this.txtFolio.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(28, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 19);
            this.label5.TabIndex = 52;
            this.label5.Text = "Folio:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(28, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 19);
            this.label3.TabIndex = 50;
            this.label3.Text = "Parte:";
            // 
            // cboPartesRicoh
            // 
            this.cboPartesRicoh.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboPartesRicoh.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboPartesRicoh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPartesRicoh.FormattingEnabled = true;
            this.cboPartesRicoh.Location = new System.Drawing.Point(89, 87);
            this.cboPartesRicoh.Margin = new System.Windows.Forms.Padding(4);
            this.cboPartesRicoh.Name = "cboPartesRicoh";
            this.cboPartesRicoh.Size = new System.Drawing.Size(293, 27);
            this.cboPartesRicoh.Sorted = true;
            this.cboPartesRicoh.TabIndex = 3;
            // 
            // btnGenerarReporte
            // 
            this.btnGenerarReporte.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnGenerarReporte.FlatAppearance.BorderSize = 2;
            this.btnGenerarReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarReporte.ForeColor = System.Drawing.Color.White;
            this.btnGenerarReporte.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarReporte.Image")));
            this.btnGenerarReporte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerarReporte.Location = new System.Drawing.Point(972, 18);
            this.btnGenerarReporte.Name = "btnGenerarReporte";
            this.btnGenerarReporte.Size = new System.Drawing.Size(133, 56);
            this.btnGenerarReporte.TabIndex = 35;
            this.btnGenerarReporte.Text = "Reporte";
            this.btnGenerarReporte.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGenerarReporte.UseVisualStyleBackColor = true;
            this.btnGenerarReporte.Click += new System.EventHandler(this.btnGenerarReporte_Click);
            // 
            // lblTipoPersona
            // 
            this.lblTipoPersona.AutoSize = true;
            this.lblTipoPersona.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoPersona.ForeColor = System.Drawing.Color.White;
            this.lblTipoPersona.Location = new System.Drawing.Point(404, 23);
            this.lblTipoPersona.Name = "lblTipoPersona";
            this.lblTipoPersona.Size = new System.Drawing.Size(87, 19);
            this.lblTipoPersona.TabIndex = 44;
            this.lblTipoPersona.Text = "Proveedor:";
            // 
            // cboProveedores
            // 
            this.cboProveedores.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboProveedores.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboProveedores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProveedores.FormattingEnabled = true;
            this.cboProveedores.Location = new System.Drawing.Point(498, 18);
            this.cboProveedores.Margin = new System.Windows.Forms.Padding(4);
            this.cboProveedores.Name = "cboProveedores";
            this.cboProveedores.Size = new System.Drawing.Size(268, 27);
            this.cboProveedores.Sorted = true;
            this.cboProveedores.TabIndex = 5;
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(498, 55);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(93, 27);
            this.txtCantidad.TabIndex = 6;
            this.txtCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidad_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(414, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 19);
            this.label7.TabIndex = 39;
            this.label7.Text = "Cantidad:";
            // 
            // cboModelo
            // 
            this.cboModelo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboModelo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboModelo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboModelo.FormattingEnabled = true;
            this.cboModelo.Location = new System.Drawing.Point(89, 52);
            this.cboModelo.Margin = new System.Windows.Forms.Padding(4);
            this.cboModelo.Name = "cboModelo";
            this.cboModelo.Size = new System.Drawing.Size(293, 27);
            this.cboModelo.Sorted = true;
            this.cboModelo.TabIndex = 2;
            this.cboModelo.SelectedIndexChanged += new System.EventHandler(this.cboModelo_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(16, 55);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(66, 19);
            this.label12.TabIndex = 36;
            this.label12.Text = "Modelo:";
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.Location = new System.Drawing.Point(89, 20);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(293, 27);
            this.dtpFechaRegistro.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(28, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 19);
            this.label9.TabIndex = 6;
            this.label9.Text = "Fecha:";
            // 
            // btnCancelar
            // 
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnCancelar.FlatAppearance.BorderSize = 2;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(258, 227);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(117, 51);
            this.btnCancelar.TabIndex = 38;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnEliminar.FlatAppearance.BorderSize = 2;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEliminar.Location = new System.Drawing.Point(135, 227);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(117, 51);
            this.btnEliminar.TabIndex = 37;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.btnGuardar.FlatAppearance.BorderSize = 2;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(12, 227);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(117, 51);
            this.btnGuardar.TabIndex = 36;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // erInventarioPartesRicoh
            // 
            this.erInventarioPartesRicoh.ContainerControl = this;
            // 
            // InventarioPartes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(69)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(1350, 977);
            this.Controls.Add(this.dtgInventario);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnGuardar);
            this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "InventarioPartes";
            this.Text = "InventarioPartesRicoh";
            ((System.ComponentModel.ISupportInitialize)(this.dtgInventario)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabInventario.ResumeLayout(false);
            this.grpDatosInventario.ResumeLayout(false);
            this.grpDatosInventario.PerformLayout();
            this.grpReporteInventario.ResumeLayout(false);
            this.tabRegistros.ResumeLayout(false);
            this.grpDatosRegistro.ResumeLayout(false);
            this.grpDatosRegistro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erInventarioPartesRicoh)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgInventario;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabInventario;
        private System.Windows.Forms.GroupBox grpDatosInventario;
        private System.Windows.Forms.RichTextBox rtxtDescripcion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabRegistros;
        private System.Windows.Forms.GroupBox grpDatosRegistro;
        private System.Windows.Forms.Button btnGenerarReporte;
        private System.Windows.Forms.Label lblTipoPersona;
        private System.Windows.Forms.ComboBox cboProveedores;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboModelo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCantidadExistencia;
        private System.Windows.Forms.ComboBox cboModelos;
        private System.Windows.Forms.ErrorProvider erInventarioPartesRicoh;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboPartesRicoh;
        private System.Windows.Forms.TextBox txtFolio;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox grpReporteInventario;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Button btnNuevoModelo;
    }
}