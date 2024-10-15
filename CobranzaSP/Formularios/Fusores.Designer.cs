namespace CobranzaSP.Formularios
{
    partial class Fusores
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Fusores));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpDatos = new System.Windows.Forms.GroupBox();
            this.chkFusorReconstruido = new System.Windows.Forms.CheckBox();
            this.cboModelos = new System.Windows.Forms.ComboBox();
            this.lblModelo = new System.Windows.Forms.Label();
            this.dtpFechaFactura = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.radInactivo = new System.Windows.Forms.RadioButton();
            this.grpReportes = new System.Windows.Forms.GroupBox();
            this.btnGenerarReporte = new System.Windows.Forms.Button();
            this.btnGenerarExcel = new System.Windows.Forms.Button();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.lblFechaFinal = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.lblFechaInicio = new System.Windows.Forms.Label();
            this.txtSerieBusqueda = new System.Windows.Forms.TextBox();
            this.cboBusqueda = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.radActivo = new System.Windows.Forms.RadioButton();
            this.cboDiasGarantía = new System.Windows.Forms.ComboBox();
            this.lblDiasGarantia = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtCosto = new System.Windows.Forms.TextBox();
            this.txtFactura = new System.Windows.Forms.TextBox();
            this.txtSerieSp = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.lblCosto = new System.Windows.Forms.Label();
            this.cboProveedores = new System.Windows.Forms.ComboBox();
            this.lblProveedor = new System.Windows.Forms.Label();
            this.lblFactura = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSerie = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtgFusores = new System.Windows.Forms.DataGridView();
            this.erFusores = new System.Windows.Forms.ErrorProvider(this.components);
            this.grpDatos.SuspendLayout();
            this.grpReportes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgFusores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erFusores)).BeginInit();
            this.SuspendLayout();
            // 
            // grpDatos
            // 
            this.grpDatos.Controls.Add(this.chkFusorReconstruido);
            this.grpDatos.Controls.Add(this.cboModelos);
            this.grpDatos.Controls.Add(this.lblModelo);
            this.grpDatos.Controls.Add(this.dtpFechaFactura);
            this.grpDatos.Controls.Add(this.label6);
            this.grpDatos.Controls.Add(this.radInactivo);
            this.grpDatos.Controls.Add(this.grpReportes);
            this.grpDatos.Controls.Add(this.radActivo);
            this.grpDatos.Controls.Add(this.cboDiasGarantía);
            this.grpDatos.Controls.Add(this.lblDiasGarantia);
            this.grpDatos.Controls.Add(this.label10);
            this.grpDatos.Controls.Add(this.txtCosto);
            this.grpDatos.Controls.Add(this.txtFactura);
            this.grpDatos.Controls.Add(this.txtSerieSp);
            this.grpDatos.Controls.Add(this.btnCancelar);
            this.grpDatos.Controls.Add(this.btnEliminar);
            this.grpDatos.Controls.Add(this.btnGuardar);
            this.grpDatos.Controls.Add(this.lblCosto);
            this.grpDatos.Controls.Add(this.cboProveedores);
            this.grpDatos.Controls.Add(this.lblProveedor);
            this.grpDatos.Controls.Add(this.lblFactura);
            this.grpDatos.Controls.Add(this.label2);
            this.grpDatos.Controls.Add(this.txtSerie);
            this.grpDatos.Controls.Add(this.label1);
            this.grpDatos.Location = new System.Drawing.Point(12, 0);
            this.grpDatos.Name = "grpDatos";
            this.grpDatos.Size = new System.Drawing.Size(1318, 321);
            this.grpDatos.TabIndex = 2;
            this.grpDatos.TabStop = false;
            this.grpDatos.Text = "Datos";
            // 
            // chkFusorReconstruido
            // 
            this.chkFusorReconstruido.AutoSize = true;
            this.chkFusorReconstruido.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.chkFusorReconstruido.Location = new System.Drawing.Point(85, 23);
            this.chkFusorReconstruido.Name = "chkFusorReconstruido";
            this.chkFusorReconstruido.Size = new System.Drawing.Size(164, 23);
            this.chkFusorReconstruido.TabIndex = 52;
            this.chkFusorReconstruido.Text = "Fusor Reconstruido";
            this.chkFusorReconstruido.UseVisualStyleBackColor = true;
            this.chkFusorReconstruido.CheckedChanged += new System.EventHandler(this.chkFusorReconstruido_CheckedChanged);
            // 
            // cboModelos
            // 
            this.cboModelos.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboModelos.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboModelos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboModelos.FormattingEnabled = true;
            this.cboModelos.Location = new System.Drawing.Point(97, 156);
            this.cboModelos.Margin = new System.Windows.Forms.Padding(4);
            this.cboModelos.Name = "cboModelos";
            this.cboModelos.Size = new System.Drawing.Size(432, 27);
            this.cboModelos.Sorted = true;
            this.cboModelos.TabIndex = 51;
            this.cboModelos.SelectedIndexChanged += new System.EventHandler(this.cboModelos_SelectedIndexChanged);
            // 
            // lblModelo
            // 
            this.lblModelo.AutoSize = true;
            this.lblModelo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModelo.ForeColor = System.Drawing.Color.White;
            this.lblModelo.Location = new System.Drawing.Point(24, 161);
            this.lblModelo.Name = "lblModelo";
            this.lblModelo.Size = new System.Drawing.Size(66, 19);
            this.lblModelo.TabIndex = 50;
            this.lblModelo.Text = "Modelo:";
            // 
            // dtpFechaFactura
            // 
            this.dtpFechaFactura.Location = new System.Drawing.Point(134, 120);
            this.dtpFechaFactura.Name = "dtpFechaFactura";
            this.dtpFechaFactura.Size = new System.Drawing.Size(395, 27);
            this.dtpFechaFactura.TabIndex = 49;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(17, 125);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 19);
            this.label6.TabIndex = 44;
            this.label6.Text = "Fecha Factura:";
            // 
            // radInactivo
            // 
            this.radInactivo.AutoSize = true;
            this.radInactivo.ForeColor = System.Drawing.Color.White;
            this.radInactivo.Location = new System.Drawing.Point(682, 20);
            this.radInactivo.Name = "radInactivo";
            this.radInactivo.Size = new System.Drawing.Size(83, 23);
            this.radInactivo.TabIndex = 43;
            this.radInactivo.TabStop = true;
            this.radInactivo.Text = "Inactivo";
            this.radInactivo.UseVisualStyleBackColor = true;
            // 
            // grpReportes
            // 
            this.grpReportes.Controls.Add(this.btnGenerarReporte);
            this.grpReportes.Controls.Add(this.btnGenerarExcel);
            this.grpReportes.Controls.Add(this.dtpFechaFinal);
            this.grpReportes.Controls.Add(this.lblFechaFinal);
            this.grpReportes.Controls.Add(this.dtpFechaInicio);
            this.grpReportes.Controls.Add(this.lblFechaInicio);
            this.grpReportes.Controls.Add(this.txtSerieBusqueda);
            this.grpReportes.Controls.Add(this.cboBusqueda);
            this.grpReportes.Controls.Add(this.label11);
            this.grpReportes.Location = new System.Drawing.Point(605, 85);
            this.grpReportes.Name = "grpReportes";
            this.grpReportes.Size = new System.Drawing.Size(707, 191);
            this.grpReportes.TabIndex = 40;
            this.grpReportes.TabStop = false;
            this.grpReportes.Text = "Reportes";
            // 
            // btnGenerarReporte
            // 
            this.btnGenerarReporte.Enabled = false;
            this.btnGenerarReporte.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnGenerarReporte.FlatAppearance.BorderSize = 2;
            this.btnGenerarReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarReporte.ForeColor = System.Drawing.Color.White;
            this.btnGenerarReporte.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarReporte.Image")));
            this.btnGenerarReporte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerarReporte.Location = new System.Drawing.Point(581, 134);
            this.btnGenerarReporte.Name = "btnGenerarReporte";
            this.btnGenerarReporte.Size = new System.Drawing.Size(120, 51);
            this.btnGenerarReporte.TabIndex = 48;
            this.btnGenerarReporte.Text = "Generar";
            this.btnGenerarReporte.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGenerarReporte.UseVisualStyleBackColor = true;
            this.btnGenerarReporte.Click += new System.EventHandler(this.btnGenerarReporte_Click);
            // 
            // btnGenerarExcel
            // 
            this.btnGenerarExcel.FlatAppearance.BorderColor = System.Drawing.Color.Coral;
            this.btnGenerarExcel.FlatAppearance.BorderSize = 2;
            this.btnGenerarExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarExcel.ForeColor = System.Drawing.Color.White;
            this.btnGenerarExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarExcel.Image")));
            this.btnGenerarExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerarExcel.Location = new System.Drawing.Point(10, 134);
            this.btnGenerarExcel.Name = "btnGenerarExcel";
            this.btnGenerarExcel.Size = new System.Drawing.Size(164, 51);
            this.btnGenerarExcel.TabIndex = 44;
            this.btnGenerarExcel.Text = "Generar Excel";
            this.btnGenerarExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGenerarExcel.UseVisualStyleBackColor = true;
            this.btnGenerarExcel.Click += new System.EventHandler(this.btnGenerarExcel_Click);
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.Location = new System.Drawing.Point(389, 104);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(313, 27);
            this.dtpFechaFinal.TabIndex = 46;
            this.dtpFechaFinal.Visible = false;
            // 
            // lblFechaFinal
            // 
            this.lblFechaFinal.AutoSize = true;
            this.lblFechaFinal.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaFinal.ForeColor = System.Drawing.Color.White;
            this.lblFechaFinal.Location = new System.Drawing.Point(385, 75);
            this.lblFechaFinal.Name = "lblFechaFinal";
            this.lblFechaFinal.Size = new System.Drawing.Size(93, 19);
            this.lblFechaFinal.TabIndex = 47;
            this.lblFechaFinal.Text = "Fecha Final:";
            this.lblFechaFinal.Visible = false;
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.Location = new System.Drawing.Point(389, 45);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(313, 27);
            this.dtpFechaInicio.TabIndex = 44;
            this.dtpFechaInicio.Visible = false;
            // 
            // lblFechaInicio
            // 
            this.lblFechaInicio.AutoSize = true;
            this.lblFechaInicio.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaInicio.ForeColor = System.Drawing.Color.White;
            this.lblFechaInicio.Location = new System.Drawing.Point(385, 23);
            this.lblFechaInicio.Name = "lblFechaInicio";
            this.lblFechaInicio.Size = new System.Drawing.Size(102, 19);
            this.lblFechaInicio.TabIndex = 45;
            this.lblFechaInicio.Text = "Fecha Inicial:";
            this.lblFechaInicio.Visible = false;
            // 
            // txtSerieBusqueda
            // 
            this.txtSerieBusqueda.Location = new System.Drawing.Point(10, 80);
            this.txtSerieBusqueda.Name = "txtSerieBusqueda";
            this.txtSerieBusqueda.Size = new System.Drawing.Size(272, 27);
            this.txtSerieBusqueda.TabIndex = 44;
            this.txtSerieBusqueda.Visible = false;
            // 
            // cboBusqueda
            // 
            this.cboBusqueda.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboBusqueda.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboBusqueda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBusqueda.FormattingEnabled = true;
            this.cboBusqueda.Location = new System.Drawing.Point(10, 46);
            this.cboBusqueda.Margin = new System.Windows.Forms.Padding(4);
            this.cboBusqueda.Name = "cboBusqueda";
            this.cboBusqueda.Size = new System.Drawing.Size(361, 27);
            this.cboBusqueda.Sorted = true;
            this.cboBusqueda.TabIndex = 44;
            this.cboBusqueda.SelectedIndexChanged += new System.EventHandler(this.cboBusqueda_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(6, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(112, 19);
            this.label11.TabIndex = 44;
            this.label11.Text = "Busqueda por:";
            // 
            // radActivo
            // 
            this.radActivo.AutoSize = true;
            this.radActivo.ForeColor = System.Drawing.Color.White;
            this.radActivo.Location = new System.Drawing.Point(605, 20);
            this.radActivo.Name = "radActivo";
            this.radActivo.Size = new System.Drawing.Size(71, 23);
            this.radActivo.TabIndex = 42;
            this.radActivo.TabStop = true;
            this.radActivo.Text = "Activo";
            this.radActivo.UseVisualStyleBackColor = true;
            // 
            // cboDiasGarantía
            // 
            this.cboDiasGarantía.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboDiasGarantía.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDiasGarantía.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDiasGarantía.FormattingEnabled = true;
            this.cboDiasGarantía.Location = new System.Drawing.Point(369, 225);
            this.cboDiasGarantía.Margin = new System.Windows.Forms.Padding(4);
            this.cboDiasGarantía.Name = "cboDiasGarantía";
            this.cboDiasGarantía.Size = new System.Drawing.Size(160, 27);
            this.cboDiasGarantía.Sorted = true;
            this.cboDiasGarantía.TabIndex = 38;
            // 
            // lblDiasGarantia
            // 
            this.lblDiasGarantia.AutoSize = true;
            this.lblDiasGarantia.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiasGarantia.ForeColor = System.Drawing.Color.White;
            this.lblDiasGarantia.Location = new System.Drawing.Point(260, 229);
            this.lblDiasGarantia.Name = "lblDiasGarantia";
            this.lblDiasGarantia.Size = new System.Drawing.Size(109, 19);
            this.lblDiasGarantia.TabIndex = 37;
            this.lblDiasGarantia.Text = "Días Garantía:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(537, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 19);
            this.label10.TabIndex = 41;
            this.label10.Text = "Estado:";
            // 
            // txtCosto
            // 
            this.txtCosto.Location = new System.Drawing.Point(85, 226);
            this.txtCosto.Name = "txtCosto";
            this.txtCosto.Size = new System.Drawing.Size(169, 27);
            this.txtCosto.TabIndex = 35;
            // 
            // txtFactura
            // 
            this.txtFactura.Location = new System.Drawing.Point(89, 190);
            this.txtFactura.Name = "txtFactura";
            this.txtFactura.Size = new System.Drawing.Size(158, 27);
            this.txtFactura.TabIndex = 32;
            // 
            // txtSerieSp
            // 
            this.txtSerieSp.Location = new System.Drawing.Point(99, 88);
            this.txtSerieSp.Name = "txtSerieSp";
            this.txtSerieSp.Size = new System.Drawing.Size(430, 27);
            this.txtSerieSp.TabIndex = 31;
            // 
            // btnCancelar
            // 
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnCancelar.FlatAppearance.BorderSize = 2;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(260, 263);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(117, 51);
            this.btnCancelar.TabIndex = 30;
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
            this.btnEliminar.Location = new System.Drawing.Point(137, 263);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(117, 51);
            this.btnEliminar.TabIndex = 29;
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
            this.btnGuardar.Location = new System.Drawing.Point(14, 263);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(117, 51);
            this.btnGuardar.TabIndex = 16;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // lblCosto
            // 
            this.lblCosto.AutoSize = true;
            this.lblCosto.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCosto.ForeColor = System.Drawing.Color.White;
            this.lblCosto.Location = new System.Drawing.Point(24, 229);
            this.lblCosto.Name = "lblCosto";
            this.lblCosto.Size = new System.Drawing.Size(55, 19);
            this.lblCosto.TabIndex = 13;
            this.lblCosto.Text = "Costo:";
            // 
            // cboProveedores
            // 
            this.cboProveedores.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboProveedores.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboProveedores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProveedores.FormattingEnabled = true;
            this.cboProveedores.Location = new System.Drawing.Point(342, 190);
            this.cboProveedores.Margin = new System.Windows.Forms.Padding(4);
            this.cboProveedores.Name = "cboProveedores";
            this.cboProveedores.Size = new System.Drawing.Size(187, 27);
            this.cboProveedores.Sorted = true;
            this.cboProveedores.TabIndex = 4;
            // 
            // lblProveedor
            // 
            this.lblProveedor.AutoSize = true;
            this.lblProveedor.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProveedor.ForeColor = System.Drawing.Color.White;
            this.lblProveedor.Location = new System.Drawing.Point(253, 194);
            this.lblProveedor.Name = "lblProveedor";
            this.lblProveedor.Size = new System.Drawing.Size(87, 19);
            this.lblProveedor.TabIndex = 6;
            this.lblProveedor.Text = "Proveedor:";
            // 
            // lblFactura
            // 
            this.lblFactura.AutoSize = true;
            this.lblFactura.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFactura.ForeColor = System.Drawing.Color.White;
            this.lblFactura.Location = new System.Drawing.Point(17, 194);
            this.lblFactura.Name = "lblFactura";
            this.lblFactura.Size = new System.Drawing.Size(66, 19);
            this.lblFactura.TabIndex = 4;
            this.lblFactura.Text = "Factura:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(17, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "#SerieSp:";
            // 
            // txtSerie
            // 
            this.txtSerie.Location = new System.Drawing.Point(85, 55);
            this.txtSerie.Name = "txtSerie";
            this.txtSerie.Size = new System.Drawing.Size(444, 27);
            this.txtSerie.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(17, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "#Serie:";
            // 
            // dtgFusores
            // 
            this.dtgFusores.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgFusores.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dtgFusores.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(90)))));
            this.dtgFusores.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(117)))), ((int)(((byte)(20)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(117)))), ((int)(((byte)(20)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgFusores.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgFusores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.InfoText;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgFusores.DefaultCellStyle = dataGridViewCellStyle2;
            this.dtgFusores.EnableHeadersVisualStyles = false;
            this.dtgFusores.Location = new System.Drawing.Point(12, 327);
            this.dtgFusores.Name = "dtgFusores";
            this.dtgFusores.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(90)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgFusores.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(90)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgFusores.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dtgFusores.RowTemplate.Height = 25;
            this.dtgFusores.Size = new System.Drawing.Size(1318, 638);
            this.dtgFusores.TabIndex = 17;
            this.dtgFusores.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgFusores_CellClick);
            // 
            // erFusores
            // 
            this.erFusores.ContainerControl = this;
            // 
            // Fusores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(69)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(1350, 977);
            this.Controls.Add(this.dtgFusores);
            this.Controls.Add(this.grpDatos);
            this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Fusores";
            this.Text = "Fusores";
            this.grpDatos.ResumeLayout(false);
            this.grpDatos.PerformLayout();
            this.grpReportes.ResumeLayout(false);
            this.grpReportes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgFusores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erFusores)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpDatos;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label lblCosto;
        private System.Windows.Forms.ComboBox cboProveedores;
        private System.Windows.Forms.Label lblProveedor;
        private System.Windows.Forms.Label lblFactura;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSerie;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSerieSp;
        private System.Windows.Forms.TextBox txtFactura;
        private System.Windows.Forms.ComboBox cboDiasGarantía;
        private System.Windows.Forms.Label lblDiasGarantia;
        private System.Windows.Forms.TextBox txtCosto;
        private System.Windows.Forms.RadioButton radInactivo;
        private System.Windows.Forms.RadioButton radActivo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox grpReportes;
        private System.Windows.Forms.Button btnGenerarReporte;
        private System.Windows.Forms.Button btnGenerarExcel;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label lblFechaFinal;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.Label lblFechaInicio;
        private System.Windows.Forms.TextBox txtSerieBusqueda;
        private System.Windows.Forms.ComboBox cboBusqueda;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView dtgFusores;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpFechaFactura;
        private System.Windows.Forms.ComboBox cboModelos;
        private System.Windows.Forms.Label lblModelo;
        private System.Windows.Forms.ErrorProvider erFusores;
        private System.Windows.Forms.CheckBox chkFusorReconstruido;
    }
}