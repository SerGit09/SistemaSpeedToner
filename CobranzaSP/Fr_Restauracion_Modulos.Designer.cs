namespace CobranzaSP
{
    partial class Fr_Restauracion_Modulos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Fr_Restauracion_Modulos));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNumeroFolio = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpDatosReporte = new System.Windows.Forms.GroupBox();
            this.rtxtServicio = new System.Windows.Forms.RichTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.cboModulos = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.grpDatosPartes = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.btnGuardarParte = new System.Windows.Forms.Button();
            this.btnCancelarParte = new System.Windows.Forms.Button();
            this.cboPartesRicoh = new System.Windows.Forms.ComboBox();
            this.btnEliminarParte = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.btnCancelarReporte = new System.Windows.Forms.Button();
            this.btnEliminarReporte = new System.Windows.Forms.Button();
            this.dtgPartesUsadas = new System.Windows.Forms.DataGridView();
            this.dtgReportes = new System.Windows.Forms.DataGridView();
            this.erReportesModulos = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnGuardarReporte = new System.Windows.Forms.Button();
            this.cboClaves = new System.Windows.Forms.ComboBox();
            this.grpDatosReporte.SuspendLayout();
            this.grpDatosPartes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgPartesUsadas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgReportes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erReportesModulos)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpFecha
            // 
            this.dtpFecha.Location = new System.Drawing.Point(143, 54);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(288, 27);
            this.dtpFecha.TabIndex = 47;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(80, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 19);
            this.label7.TabIndex = 49;
            this.label7.Text = "Fecha:";
            // 
            // txtNumeroFolio
            // 
            this.txtNumeroFolio.Location = new System.Drawing.Point(141, 20);
            this.txtNumeroFolio.Name = "txtNumeroFolio";
            this.txtNumeroFolio.Size = new System.Drawing.Size(290, 27);
            this.txtNumeroFolio.TabIndex = 35;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(24, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 19);
            this.label1.TabIndex = 34;
            this.label1.Text = "Numero Folio:";
            // 
            // grpDatosReporte
            // 
            this.grpDatosReporte.Controls.Add(this.rtxtServicio);
            this.grpDatosReporte.Controls.Add(this.label11);
            this.grpDatosReporte.Controls.Add(this.label15);
            this.grpDatosReporte.Controls.Add(this.cboClaves);
            this.grpDatosReporte.Controls.Add(this.cboModulos);
            this.grpDatosReporte.Controls.Add(this.label14);
            this.grpDatosReporte.Controls.Add(this.label1);
            this.grpDatosReporte.Controls.Add(this.txtNumeroFolio);
            this.grpDatosReporte.Controls.Add(this.dtpFecha);
            this.grpDatosReporte.Controls.Add(this.label7);
            this.grpDatosReporte.ForeColor = System.Drawing.SystemColors.Control;
            this.grpDatosReporte.Location = new System.Drawing.Point(12, 12);
            this.grpDatosReporte.Name = "grpDatosReporte";
            this.grpDatosReporte.Size = new System.Drawing.Size(463, 298);
            this.grpDatosReporte.TabIndex = 51;
            this.grpDatosReporte.TabStop = false;
            this.grpDatosReporte.Text = "DATOS:";
            this.grpDatosReporte.Enter += new System.EventHandler(this.grpDatosReporte_Enter);
            // 
            // rtxtServicio
            // 
            this.rtxtServicio.Location = new System.Drawing.Point(14, 184);
            this.rtxtServicio.Name = "rtxtServicio";
            this.rtxtServicio.Size = new System.Drawing.Size(443, 94);
            this.rtxtServicio.TabIndex = 70;
            this.rtxtServicio.Text = "";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(10, 162);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(142, 19);
            this.label11.TabIndex = 71;
            this.label11.Text = "Servicio Realizado:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(68, 94);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(67, 19);
            this.label15.TabIndex = 65;
            this.label15.Text = "Modulo:";
            // 
            // cboModulos
            // 
            this.cboModulos.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboModulos.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboModulos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboModulos.FormattingEnabled = true;
            this.cboModulos.Location = new System.Drawing.Point(142, 88);
            this.cboModulos.Margin = new System.Windows.Forms.Padding(4);
            this.cboModulos.Name = "cboModulos";
            this.cboModulos.Size = new System.Drawing.Size(289, 27);
            this.cboModulos.Sorted = true;
            this.cboModulos.TabIndex = 68;
            this.cboModulos.SelectedIndexChanged += new System.EventHandler(this.cboModulos_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(80, 126);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 19);
            this.label14.TabIndex = 66;
            this.label14.Text = "Clave:";
            // 
            // grpDatosPartes
            // 
            this.grpDatosPartes.Controls.Add(this.label3);
            this.grpDatosPartes.Controls.Add(this.txtCantidad);
            this.grpDatosPartes.Controls.Add(this.btnGuardarParte);
            this.grpDatosPartes.Controls.Add(this.btnCancelarParte);
            this.grpDatosPartes.Controls.Add(this.cboPartesRicoh);
            this.grpDatosPartes.Controls.Add(this.btnEliminarParte);
            this.grpDatosPartes.Controls.Add(this.label10);
            this.grpDatosPartes.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.grpDatosPartes.Location = new System.Drawing.Point(516, 21);
            this.grpDatosPartes.Name = "grpDatosPartes";
            this.grpDatosPartes.Size = new System.Drawing.Size(654, 172);
            this.grpDatosPartes.TabIndex = 52;
            this.grpDatosPartes.TabStop = false;
            this.grpDatosPartes.Text = "PARTE UTILIZADA";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(2, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 19);
            this.label3.TabIndex = 59;
            this.label3.Text = "Cantidad:";
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(85, 68);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(85, 27);
            this.txtCantidad.TabIndex = 58;
            // 
            // btnGuardarParte
            // 
            this.btnGuardarParte.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.btnGuardarParte.FlatAppearance.BorderSize = 2;
            this.btnGuardarParte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarParte.ForeColor = System.Drawing.Color.White;
            this.btnGuardarParte.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardarParte.Image")));
            this.btnGuardarParte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardarParte.Location = new System.Drawing.Point(13, 114);
            this.btnGuardarParte.Name = "btnGuardarParte";
            this.btnGuardarParte.Size = new System.Drawing.Size(117, 51);
            this.btnGuardarParte.TabIndex = 57;
            this.btnGuardarParte.Text = "Guardar";
            this.btnGuardarParte.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuardarParte.UseVisualStyleBackColor = true;
            this.btnGuardarParte.Click += new System.EventHandler(this.btnGuardarParte_Click);
            // 
            // btnCancelarParte
            // 
            this.btnCancelarParte.Enabled = false;
            this.btnCancelarParte.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnCancelarParte.FlatAppearance.BorderSize = 2;
            this.btnCancelarParte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelarParte.ForeColor = System.Drawing.Color.White;
            this.btnCancelarParte.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelarParte.Image")));
            this.btnCancelarParte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelarParte.Location = new System.Drawing.Point(259, 114);
            this.btnCancelarParte.Name = "btnCancelarParte";
            this.btnCancelarParte.Size = new System.Drawing.Size(117, 51);
            this.btnCancelarParte.TabIndex = 56;
            this.btnCancelarParte.Text = "Cancelar";
            this.btnCancelarParte.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelarParte.UseVisualStyleBackColor = true;
            this.btnCancelarParte.Click += new System.EventHandler(this.btnCancelarParte_Click);
            // 
            // cboPartesRicoh
            // 
            this.cboPartesRicoh.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboPartesRicoh.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboPartesRicoh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPartesRicoh.FormattingEnabled = true;
            this.cboPartesRicoh.Location = new System.Drawing.Point(85, 33);
            this.cboPartesRicoh.Margin = new System.Windows.Forms.Padding(4);
            this.cboPartesRicoh.Name = "cboPartesRicoh";
            this.cboPartesRicoh.Size = new System.Drawing.Size(345, 27);
            this.cboPartesRicoh.Sorted = true;
            this.cboPartesRicoh.TabIndex = 11;
            // 
            // btnEliminarParte
            // 
            this.btnEliminarParte.Enabled = false;
            this.btnEliminarParte.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnEliminarParte.FlatAppearance.BorderSize = 2;
            this.btnEliminarParte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarParte.ForeColor = System.Drawing.Color.White;
            this.btnEliminarParte.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminarParte.Image")));
            this.btnEliminarParte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEliminarParte.Location = new System.Drawing.Point(136, 114);
            this.btnEliminarParte.Name = "btnEliminarParte";
            this.btnEliminarParte.Size = new System.Drawing.Size(117, 51);
            this.btnEliminarParte.TabIndex = 55;
            this.btnEliminarParte.Text = "Eliminar";
            this.btnEliminarParte.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEliminarParte.UseVisualStyleBackColor = true;
            this.btnEliminarParte.Click += new System.EventHandler(this.btnEliminarParte_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(28, 41);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 19);
            this.label10.TabIndex = 40;
            this.label10.Text = "Parte:";
            // 
            // btnCancelarReporte
            // 
            this.btnCancelarReporte.Enabled = false;
            this.btnCancelarReporte.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnCancelarReporte.FlatAppearance.BorderSize = 2;
            this.btnCancelarReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelarReporte.ForeColor = System.Drawing.Color.White;
            this.btnCancelarReporte.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelarReporte.Image")));
            this.btnCancelarReporte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelarReporte.Location = new System.Drawing.Point(258, 316);
            this.btnCancelarReporte.Name = "btnCancelarReporte";
            this.btnCancelarReporte.Size = new System.Drawing.Size(117, 51);
            this.btnCancelarReporte.TabIndex = 47;
            this.btnCancelarReporte.Text = "Cancelar";
            this.btnCancelarReporte.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelarReporte.UseVisualStyleBackColor = true;
            this.btnCancelarReporte.Click += new System.EventHandler(this.btnCancelarReporte_Click);
            // 
            // btnEliminarReporte
            // 
            this.btnEliminarReporte.Enabled = false;
            this.btnEliminarReporte.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnEliminarReporte.FlatAppearance.BorderSize = 2;
            this.btnEliminarReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarReporte.ForeColor = System.Drawing.Color.White;
            this.btnEliminarReporte.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminarReporte.Image")));
            this.btnEliminarReporte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEliminarReporte.Location = new System.Drawing.Point(135, 316);
            this.btnEliminarReporte.Name = "btnEliminarReporte";
            this.btnEliminarReporte.Size = new System.Drawing.Size(117, 51);
            this.btnEliminarReporte.TabIndex = 46;
            this.btnEliminarReporte.Text = "Eliminar";
            this.btnEliminarReporte.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEliminarReporte.UseVisualStyleBackColor = true;
            this.btnEliminarReporte.Click += new System.EventHandler(this.btnEliminarReporte_Click);
            // 
            // dtgPartesUsadas
            // 
            this.dtgPartesUsadas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgPartesUsadas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dtgPartesUsadas.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(90)))));
            this.dtgPartesUsadas.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(117)))), ((int)(((byte)(20)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(117)))), ((int)(((byte)(20)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgPartesUsadas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgPartesUsadas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.InfoText;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgPartesUsadas.DefaultCellStyle = dataGridViewCellStyle2;
            this.dtgPartesUsadas.EnableHeadersVisualStyles = false;
            this.dtgPartesUsadas.Location = new System.Drawing.Point(516, 196);
            this.dtgPartesUsadas.Name = "dtgPartesUsadas";
            this.dtgPartesUsadas.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(90)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgPartesUsadas.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(90)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgPartesUsadas.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dtgPartesUsadas.RowTemplate.Height = 25;
            this.dtgPartesUsadas.Size = new System.Drawing.Size(654, 164);
            this.dtgPartesUsadas.TabIndex = 53;
            this.dtgPartesUsadas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgPartesUsadas_CellClick);
            // 
            // dtgReportes
            // 
            this.dtgReportes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgReportes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dtgReportes.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(90)))));
            this.dtgReportes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(117)))), ((int)(((byte)(20)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(117)))), ((int)(((byte)(20)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgReportes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dtgReportes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.InfoText;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgReportes.DefaultCellStyle = dataGridViewCellStyle6;
            this.dtgReportes.EnableHeadersVisualStyles = false;
            this.dtgReportes.Location = new System.Drawing.Point(12, 373);
            this.dtgReportes.Name = "dtgReportes";
            this.dtgReportes.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(90)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgReportes.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(90)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgReportes.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dtgReportes.RowTemplate.Height = 25;
            this.dtgReportes.Size = new System.Drawing.Size(1158, 592);
            this.dtgReportes.TabIndex = 54;
            this.dtgReportes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgReportes_CellClick);
            // 
            // erReportesModulos
            // 
            this.erReportesModulos.ContainerControl = this;
            // 
            // btnGuardarReporte
            // 
            this.btnGuardarReporte.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.btnGuardarReporte.FlatAppearance.BorderSize = 2;
            this.btnGuardarReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarReporte.ForeColor = System.Drawing.Color.White;
            this.btnGuardarReporte.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardarReporte.Image")));
            this.btnGuardarReporte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardarReporte.Location = new System.Drawing.Point(12, 316);
            this.btnGuardarReporte.Name = "btnGuardarReporte";
            this.btnGuardarReporte.Size = new System.Drawing.Size(117, 51);
            this.btnGuardarReporte.TabIndex = 58;
            this.btnGuardarReporte.Text = "Guardar";
            this.btnGuardarReporte.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuardarReporte.UseVisualStyleBackColor = true;
            this.btnGuardarReporte.Click += new System.EventHandler(this.btnGuardarReporte_Click);
            // 
            // cboClaves
            // 
            this.cboClaves.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboClaves.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboClaves.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClaves.FormattingEnabled = true;
            this.cboClaves.Location = new System.Drawing.Point(142, 123);
            this.cboClaves.Margin = new System.Windows.Forms.Padding(4);
            this.cboClaves.Name = "cboClaves";
            this.cboClaves.Size = new System.Drawing.Size(289, 27);
            this.cboClaves.Sorted = true;
            this.cboClaves.TabIndex = 69;
            // 
            // Fr_Restauracion_Modulos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(69)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(1350, 977);
            this.Controls.Add(this.btnGuardarReporte);
            this.Controls.Add(this.btnCancelarReporte);
            this.Controls.Add(this.dtgReportes);
            this.Controls.Add(this.btnEliminarReporte);
            this.Controls.Add(this.dtgPartesUsadas);
            this.Controls.Add(this.grpDatosPartes);
            this.Controls.Add(this.grpDatosReporte);
            this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Fr_Restauracion_Modulos";
            this.Text = "Fr_Restauracion_Modulos";
            this.Load += new System.EventHandler(this.Fr_Restauracion_Modulos_Load);
            this.grpDatosReporte.ResumeLayout(false);
            this.grpDatosReporte.PerformLayout();
            this.grpDatosPartes.ResumeLayout(false);
            this.grpDatosPartes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgPartesUsadas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgReportes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erReportesModulos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNumeroFolio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpDatosReporte;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cboModulos;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox grpDatosPartes;
        private System.Windows.Forms.ComboBox cboPartesRicoh;
        private System.Windows.Forms.Button btnCancelarReporte;
        private System.Windows.Forms.Button btnEliminarReporte;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView dtgPartesUsadas;
        private System.Windows.Forms.DataGridView dtgReportes;
        private System.Windows.Forms.Button btnGuardarParte;
        private System.Windows.Forms.Button btnCancelarParte;
        private System.Windows.Forms.Button btnEliminarParte;
        private System.Windows.Forms.ErrorProvider erReportesModulos;
        private System.Windows.Forms.Button btnGuardarReporte;
        private System.Windows.Forms.RichTextBox rtxtServicio;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.ComboBox cboClaves;
    }
}