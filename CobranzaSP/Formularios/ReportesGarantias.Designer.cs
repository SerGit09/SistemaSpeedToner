namespace CobranzaSP.Formularios
{
    partial class ReportesGarantias
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportesGarantias));
            this.pSuperior = new System.Windows.Forms.Panel();
            this.btnCerrar = new System.Windows.Forms.PictureBox();
            this.cboModelos = new System.Windows.Forms.ComboBox();
            this.radTodosLosClientes = new System.Windows.Forms.RadioButton();
            this.rdUnCliente = new System.Windows.Forms.RadioButton();
            this.cboMarca = new System.Windows.Forms.ComboBox();
            this.btnGenerarReporte = new System.Windows.Forms.Button();
            this.cboOpcionReporte = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMarca = new System.Windows.Forms.Label();
            this.lblModelos = new System.Windows.Forms.Label();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.lblFechaFinal = new System.Windows.Forms.Label();
            this.lblFechaInicio = new System.Windows.Forms.Label();
            this.cboClientes = new System.Windows.Forms.ComboBox();
            this.grpCliente = new System.Windows.Forms.GroupBox();
            this.lblCliente = new System.Windows.Forms.Label();
            this.grpModelos = new System.Windows.Forms.GroupBox();
            this.radUnModelo = new System.Windows.Forms.RadioButton();
            this.radTodosModelos = new System.Windows.Forms.RadioButton();
            this.grpMarcas = new System.Windows.Forms.GroupBox();
            this.radUnaMarca = new System.Windows.Forms.RadioButton();
            this.radTodasLasMarcas = new System.Windows.Forms.RadioButton();
            this.erGarantia = new System.Windows.Forms.ErrorProvider(this.components);
            this.pSuperior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).BeginInit();
            this.grpCliente.SuspendLayout();
            this.grpModelos.SuspendLayout();
            this.grpMarcas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erGarantia)).BeginInit();
            this.SuspendLayout();
            // 
            // pSuperior
            // 
            this.pSuperior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(117)))), ((int)(((byte)(20)))));
            this.pSuperior.Controls.Add(this.btnCerrar);
            this.pSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.pSuperior.Location = new System.Drawing.Point(0, 0);
            this.pSuperior.Name = "pSuperior";
            this.pSuperior.Size = new System.Drawing.Size(529, 36);
            this.pSuperior.TabIndex = 1;
            this.pSuperior.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pSuperior_MouseDown);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.ErrorImage = null;
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.InitialImage = null;
            this.btnCerrar.Location = new System.Drawing.Point(501, 3);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(25, 28);
            this.btnCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnCerrar.TabIndex = 6;
            this.btnCerrar.TabStop = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // cboModelos
            // 
            this.cboModelos.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboModelos.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboModelos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboModelos.FormattingEnabled = true;
            this.cboModelos.Location = new System.Drawing.Point(90, 45);
            this.cboModelos.Margin = new System.Windows.Forms.Padding(4);
            this.cboModelos.Name = "cboModelos";
            this.cboModelos.Size = new System.Drawing.Size(303, 27);
            this.cboModelos.Sorted = true;
            this.cboModelos.TabIndex = 54;
            this.cboModelos.Visible = false;
            // 
            // radTodosLosClientes
            // 
            this.radTodosLosClientes.AutoSize = true;
            this.radTodosLosClientes.Location = new System.Drawing.Point(155, 17);
            this.radTodosLosClientes.Name = "radTodosLosClientes";
            this.radTodosLosClientes.Size = new System.Drawing.Size(70, 23);
            this.radTodosLosClientes.TabIndex = 71;
            this.radTodosLosClientes.TabStop = true;
            this.radTodosLosClientes.Text = "Todas";
            this.radTodosLosClientes.UseVisualStyleBackColor = true;
            this.radTodosLosClientes.CheckedChanged += new System.EventHandler(this.radTodosLosClientes_CheckedChanged);
            // 
            // rdUnCliente
            // 
            this.rdUnCliente.AutoSize = true;
            this.rdUnCliente.Location = new System.Drawing.Point(93, 17);
            this.rdUnCliente.Name = "rdUnCliente";
            this.rdUnCliente.Size = new System.Drawing.Size(56, 23);
            this.rdUnCliente.TabIndex = 70;
            this.rdUnCliente.TabStop = true;
            this.rdUnCliente.Text = "Uno";
            this.rdUnCliente.UseVisualStyleBackColor = true;
            this.rdUnCliente.CheckedChanged += new System.EventHandler(this.rdUnCliente_CheckedChanged);
            // 
            // cboMarca
            // 
            this.cboMarca.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboMarca.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboMarca.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMarca.FormattingEnabled = true;
            this.cboMarca.Location = new System.Drawing.Point(90, 44);
            this.cboMarca.Margin = new System.Windows.Forms.Padding(4);
            this.cboMarca.Name = "cboMarca";
            this.cboMarca.Size = new System.Drawing.Size(303, 27);
            this.cboMarca.Sorted = true;
            this.cboMarca.TabIndex = 69;
            this.cboMarca.Visible = false;
            this.cboMarca.SelectedIndexChanged += new System.EventHandler(this.cboOpcionElegida_SelectedIndexChanged);
            // 
            // btnGenerarReporte
            // 
            this.btnGenerarReporte.Enabled = false;
            this.btnGenerarReporte.FlatAppearance.BorderColor = System.Drawing.Color.Crimson;
            this.btnGenerarReporte.FlatAppearance.BorderSize = 2;
            this.btnGenerarReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarReporte.ForeColor = System.Drawing.Color.White;
            this.btnGenerarReporte.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarReporte.Image")));
            this.btnGenerarReporte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerarReporte.Location = new System.Drawing.Point(400, 430);
            this.btnGenerarReporte.Name = "btnGenerarReporte";
            this.btnGenerarReporte.Size = new System.Drawing.Size(117, 51);
            this.btnGenerarReporte.TabIndex = 68;
            this.btnGenerarReporte.Text = "Generar";
            this.btnGenerarReporte.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGenerarReporte.UseVisualStyleBackColor = true;
            this.btnGenerarReporte.Click += new System.EventHandler(this.btnGenerarReporte_Click);
            // 
            // cboOpcionReporte
            // 
            this.cboOpcionReporte.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboOpcionReporte.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboOpcionReporte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOpcionReporte.FormattingEnabled = true;
            this.cboOpcionReporte.Location = new System.Drawing.Point(163, 46);
            this.cboOpcionReporte.Margin = new System.Windows.Forms.Padding(4);
            this.cboOpcionReporte.Name = "cboOpcionReporte";
            this.cboOpcionReporte.Size = new System.Drawing.Size(304, 27);
            this.cboOpcionReporte.Sorted = true;
            this.cboOpcionReporte.TabIndex = 67;
            this.cboOpcionReporte.SelectedIndexChanged += new System.EventHandler(this.cboOpcionReporte_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(56, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 19);
            this.label1.TabIndex = 66;
            this.label1.Text = "Generar por:";
            // 
            // lblMarca
            // 
            this.lblMarca.AutoSize = true;
            this.lblMarca.ForeColor = System.Drawing.Color.White;
            this.lblMarca.Location = new System.Drawing.Point(27, 47);
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.Size = new System.Drawing.Size(56, 19);
            this.lblMarca.TabIndex = 74;
            this.lblMarca.Text = "Marca:";
            this.lblMarca.Visible = false;
            // 
            // lblModelos
            // 
            this.lblModelos.AutoSize = true;
            this.lblModelos.ForeColor = System.Drawing.Color.White;
            this.lblModelos.Location = new System.Drawing.Point(17, 48);
            this.lblModelos.Name = "lblModelos";
            this.lblModelos.Size = new System.Drawing.Size(66, 19);
            this.lblModelos.TabIndex = 75;
            this.lblModelos.Text = "Modelo:";
            this.lblModelos.Visible = false;
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.Location = new System.Drawing.Point(212, 393);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(303, 27);
            this.dtpFechaFinal.TabIndex = 79;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.Location = new System.Drawing.Point(213, 352);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(303, 27);
            this.dtpFechaInicial.TabIndex = 78;
            // 
            // lblFechaFinal
            // 
            this.lblFechaFinal.AutoSize = true;
            this.lblFechaFinal.ForeColor = System.Drawing.Color.White;
            this.lblFechaFinal.Location = new System.Drawing.Point(176, 399);
            this.lblFechaFinal.Name = "lblFechaFinal";
            this.lblFechaFinal.Size = new System.Drawing.Size(30, 19);
            this.lblFechaFinal.TabIndex = 77;
            this.lblFechaFinal.Text = "Al:";
            // 
            // lblFechaInicio
            // 
            this.lblFechaInicio.AutoSize = true;
            this.lblFechaInicio.ForeColor = System.Drawing.Color.White;
            this.lblFechaInicio.Location = new System.Drawing.Point(173, 358);
            this.lblFechaInicio.Name = "lblFechaInicio";
            this.lblFechaInicio.Size = new System.Drawing.Size(34, 19);
            this.lblFechaInicio.TabIndex = 76;
            this.lblFechaInicio.Text = "De:";
            // 
            // cboClientes
            // 
            this.cboClientes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboClientes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboClientes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClientes.FormattingEnabled = true;
            this.cboClientes.Location = new System.Drawing.Point(93, 45);
            this.cboClientes.Margin = new System.Windows.Forms.Padding(4);
            this.cboClientes.Name = "cboClientes";
            this.cboClientes.Size = new System.Drawing.Size(303, 27);
            this.cboClientes.Sorted = true;
            this.cboClientes.TabIndex = 81;
            this.cboClientes.Visible = false;
            this.cboClientes.SelectedIndexChanged += new System.EventHandler(this.cboClientes_SelectedIndexChanged);
            // 
            // grpCliente
            // 
            this.grpCliente.Controls.Add(this.lblCliente);
            this.grpCliente.Controls.Add(this.rdUnCliente);
            this.grpCliente.Controls.Add(this.cboClientes);
            this.grpCliente.Controls.Add(this.radTodosLosClientes);
            this.grpCliente.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.grpCliente.Location = new System.Drawing.Point(73, 86);
            this.grpCliente.Name = "grpCliente";
            this.grpCliente.Size = new System.Drawing.Size(444, 82);
            this.grpCliente.TabIndex = 83;
            this.grpCliente.TabStop = false;
            this.grpCliente.Text = "Clientes";
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.ForeColor = System.Drawing.Color.White;
            this.lblCliente.Location = new System.Drawing.Point(23, 48);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(63, 19);
            this.lblCliente.TabIndex = 82;
            this.lblCliente.Text = "Cliente:";
            this.lblCliente.Visible = false;
            // 
            // grpModelos
            // 
            this.grpModelos.Controls.Add(this.radUnModelo);
            this.grpModelos.Controls.Add(this.radTodosModelos);
            this.grpModelos.Controls.Add(this.lblModelos);
            this.grpModelos.Controls.Add(this.cboModelos);
            this.grpModelos.ForeColor = System.Drawing.SystemColors.Control;
            this.grpModelos.Location = new System.Drawing.Point(73, 262);
            this.grpModelos.Name = "grpModelos";
            this.grpModelos.Size = new System.Drawing.Size(444, 82);
            this.grpModelos.TabIndex = 84;
            this.grpModelos.TabStop = false;
            this.grpModelos.Text = "Modelos";
            this.grpModelos.Visible = false;
            // 
            // radUnModelo
            // 
            this.radUnModelo.AutoSize = true;
            this.radUnModelo.ForeColor = System.Drawing.SystemColors.Control;
            this.radUnModelo.Location = new System.Drawing.Point(93, 15);
            this.radUnModelo.Name = "radUnModelo";
            this.radUnModelo.Size = new System.Drawing.Size(56, 23);
            this.radUnModelo.TabIndex = 83;
            this.radUnModelo.TabStop = true;
            this.radUnModelo.Text = "Uno";
            this.radUnModelo.UseVisualStyleBackColor = true;
            this.radUnModelo.CheckedChanged += new System.EventHandler(this.radUnModelo_CheckedChanged);
            // 
            // radTodosModelos
            // 
            this.radTodosModelos.AutoSize = true;
            this.radTodosModelos.ForeColor = System.Drawing.SystemColors.Control;
            this.radTodosModelos.Location = new System.Drawing.Point(155, 15);
            this.radTodosModelos.Name = "radTodosModelos";
            this.radTodosModelos.Size = new System.Drawing.Size(70, 23);
            this.radTodosModelos.TabIndex = 84;
            this.radTodosModelos.TabStop = true;
            this.radTodosModelos.Text = "Todas";
            this.radTodosModelos.UseVisualStyleBackColor = true;
            this.radTodosModelos.CheckedChanged += new System.EventHandler(this.radTodosModelos_CheckedChanged);
            // 
            // grpMarcas
            // 
            this.grpMarcas.Controls.Add(this.radUnaMarca);
            this.grpMarcas.Controls.Add(this.radTodasLasMarcas);
            this.grpMarcas.Controls.Add(this.lblMarca);
            this.grpMarcas.Controls.Add(this.cboMarca);
            this.grpMarcas.ForeColor = System.Drawing.SystemColors.Control;
            this.grpMarcas.Location = new System.Drawing.Point(73, 174);
            this.grpMarcas.Name = "grpMarcas";
            this.grpMarcas.Size = new System.Drawing.Size(444, 82);
            this.grpMarcas.TabIndex = 85;
            this.grpMarcas.TabStop = false;
            this.grpMarcas.Text = "Marcas";
            this.grpMarcas.Visible = false;
            // 
            // radUnaMarca
            // 
            this.radUnaMarca.AutoSize = true;
            this.radUnaMarca.ForeColor = System.Drawing.SystemColors.Control;
            this.radUnaMarca.Location = new System.Drawing.Point(93, 15);
            this.radUnaMarca.Name = "radUnaMarca";
            this.radUnaMarca.Size = new System.Drawing.Size(56, 23);
            this.radUnaMarca.TabIndex = 83;
            this.radUnaMarca.TabStop = true;
            this.radUnaMarca.Text = "Uno";
            this.radUnaMarca.UseVisualStyleBackColor = true;
            this.radUnaMarca.CheckedChanged += new System.EventHandler(this.radUnaMarca_CheckedChanged);
            // 
            // radTodasLasMarcas
            // 
            this.radTodasLasMarcas.AutoSize = true;
            this.radTodasLasMarcas.ForeColor = System.Drawing.SystemColors.Control;
            this.radTodasLasMarcas.Location = new System.Drawing.Point(155, 15);
            this.radTodasLasMarcas.Name = "radTodasLasMarcas";
            this.radTodasLasMarcas.Size = new System.Drawing.Size(70, 23);
            this.radTodasLasMarcas.TabIndex = 84;
            this.radTodasLasMarcas.TabStop = true;
            this.radTodasLasMarcas.Text = "Todas";
            this.radTodasLasMarcas.UseVisualStyleBackColor = true;
            this.radTodasLasMarcas.CheckedChanged += new System.EventHandler(this.radTodasLasMarcas_CheckedChanged);
            // 
            // erGarantia
            // 
            this.erGarantia.ContainerControl = this;
            // 
            // ReportesGarantias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(57)))), ((int)(((byte)(80)))));
            this.ClientSize = new System.Drawing.Size(529, 493);
            this.Controls.Add(this.grpMarcas);
            this.Controls.Add(this.grpModelos);
            this.Controls.Add(this.grpCliente);
            this.Controls.Add(this.dtpFechaFinal);
            this.Controls.Add(this.dtpFechaInicial);
            this.Controls.Add(this.lblFechaFinal);
            this.Controls.Add(this.lblFechaInicio);
            this.Controls.Add(this.btnGenerarReporte);
            this.Controls.Add(this.cboOpcionReporte);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pSuperior);
            this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ReportesGarantias";
            this.Text = "ReportesGarantias";
            this.pSuperior.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).EndInit();
            this.grpCliente.ResumeLayout(false);
            this.grpCliente.PerformLayout();
            this.grpModelos.ResumeLayout(false);
            this.grpModelos.PerformLayout();
            this.grpMarcas.ResumeLayout(false);
            this.grpMarcas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erGarantia)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pSuperior;
        private System.Windows.Forms.PictureBox btnCerrar;
        private System.Windows.Forms.ComboBox cboModelos;
        private System.Windows.Forms.RadioButton radTodosLosClientes;
        private System.Windows.Forms.RadioButton rdUnCliente;
        private System.Windows.Forms.ComboBox cboMarca;
        private System.Windows.Forms.Button btnGenerarReporte;
        private System.Windows.Forms.ComboBox cboOpcionReporte;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMarca;
        private System.Windows.Forms.Label lblModelos;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.Label lblFechaFinal;
        private System.Windows.Forms.Label lblFechaInicio;
        private System.Windows.Forms.ComboBox cboClientes;
        private System.Windows.Forms.GroupBox grpCliente;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.GroupBox grpModelos;
        private System.Windows.Forms.RadioButton radUnModelo;
        private System.Windows.Forms.RadioButton radTodosModelos;
        private System.Windows.Forms.GroupBox grpMarcas;
        private System.Windows.Forms.RadioButton radUnaMarca;
        private System.Windows.Forms.RadioButton radTodasLasMarcas;
        private System.Windows.Forms.ErrorProvider erGarantia;
    }
}