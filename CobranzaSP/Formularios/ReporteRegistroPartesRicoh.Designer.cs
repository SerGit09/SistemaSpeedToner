namespace CobranzaSP
{
    partial class ReporteRegistroPartesRicoh
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReporteRegistroPartesRicoh));
            this.pSuperior = new System.Windows.Forms.Panel();
            this.btnCerrar = new System.Windows.Forms.PictureBox();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblModelos = new System.Windows.Forms.Label();
            this.cboModelos = new System.Windows.Forms.ComboBox();
            this.cboTipoBusqueda = new System.Windows.Forms.ComboBox();
            this.grpPartes = new System.Windows.Forms.GroupBox();
            this.radUnaParte = new System.Windows.Forms.RadioButton();
            this.radTodasLasPartes = new System.Windows.Forms.RadioButton();
            this.lblParte = new System.Windows.Forms.Label();
            this.cboPartes = new System.Windows.Forms.ComboBox();
            this.lblSerieEquipo = new System.Windows.Forms.Label();
            this.cboSeriesEquipos = new System.Windows.Forms.ComboBox();
            this.chkElegirSerie = new System.Windows.Forms.CheckBox();
            this.erReporteParteRicoh = new System.Windows.Forms.ErrorProvider(this.components);
            this.pSuperior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).BeginInit();
            this.grpPartes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erReporteParteRicoh)).BeginInit();
            this.SuspendLayout();
            // 
            // pSuperior
            // 
            this.pSuperior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(117)))), ((int)(((byte)(20)))));
            this.pSuperior.Controls.Add(this.btnCerrar);
            this.pSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.pSuperior.Location = new System.Drawing.Point(0, 0);
            this.pSuperior.Margin = new System.Windows.Forms.Padding(4);
            this.pSuperior.Name = "pSuperior";
            this.pSuperior.Size = new System.Drawing.Size(576, 35);
            this.pSuperior.TabIndex = 0;
            this.pSuperior.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pSuperior_MouseDown);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.ErrorImage = null;
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.InitialImage = null;
            this.btnCerrar.Location = new System.Drawing.Point(547, 4);
            this.btnCerrar.Margin = new System.Windows.Forms.Padding(4);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(25, 28);
            this.btnCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnCerrar.TabIndex = 3;
            this.btnCerrar.TabStop = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnGenerar
            // 
            this.btnGenerar.Enabled = false;
            this.btnGenerar.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
            this.btnGenerar.FlatAppearance.BorderSize = 2;
            this.btnGenerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerar.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerar.ForeColor = System.Drawing.Color.White;
            this.btnGenerar.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerar.Image")));
            this.btnGenerar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerar.Location = new System.Drawing.Point(334, 373);
            this.btnGenerar.Margin = new System.Windows.Forms.Padding(4);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnGenerar.Size = new System.Drawing.Size(179, 72);
            this.btnGenerar.TabIndex = 29;
            this.btnGenerar.Text = "Generar";
            this.btnGenerar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.Location = new System.Drawing.Point(183, 338);
            this.dtpFechaFinal.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(330, 27);
            this.dtpFechaFinal.TabIndex = 28;
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.Location = new System.Drawing.Point(183, 303);
            this.dtpFechaInicio.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(330, 27);
            this.dtpFechaInicio.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(145, 344);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 19);
            this.label2.TabIndex = 26;
            this.label2.Text = "Al:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(141, 309);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 19);
            this.label1.TabIndex = 25;
            this.label1.Text = "De:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(4, 57);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 19);
            this.label9.TabIndex = 24;
            this.label9.Text = "Generar por:";
            // 
            // lblModelos
            // 
            this.lblModelos.AutoSize = true;
            this.lblModelos.ForeColor = System.Drawing.Color.White;
            this.lblModelos.Location = new System.Drawing.Point(38, 104);
            this.lblModelos.Name = "lblModelos";
            this.lblModelos.Size = new System.Drawing.Size(66, 19);
            this.lblModelos.TabIndex = 75;
            this.lblModelos.Text = "Modelo:";
            this.lblModelos.Visible = false;
            // 
            // cboModelos
            // 
            this.cboModelos.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboModelos.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboModelos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboModelos.FormattingEnabled = true;
            this.cboModelos.Location = new System.Drawing.Point(111, 96);
            this.cboModelos.Margin = new System.Windows.Forms.Padding(4);
            this.cboModelos.Name = "cboModelos";
            this.cboModelos.Size = new System.Drawing.Size(402, 27);
            this.cboModelos.Sorted = true;
            this.cboModelos.TabIndex = 54;
            this.cboModelos.Visible = false;
            this.cboModelos.SelectedIndexChanged += new System.EventHandler(this.cboModelos_SelectedIndexChanged);
            // 
            // cboTipoBusqueda
            // 
            this.cboTipoBusqueda.FormattingEnabled = true;
            this.cboTipoBusqueda.Location = new System.Drawing.Point(111, 54);
            this.cboTipoBusqueda.Name = "cboTipoBusqueda";
            this.cboTipoBusqueda.Size = new System.Drawing.Size(402, 27);
            this.cboTipoBusqueda.TabIndex = 85;
            this.cboTipoBusqueda.SelectedIndexChanged += new System.EventHandler(this.cboTipoBusqueda_SelectedIndexChanged_1);
            // 
            // grpPartes
            // 
            this.grpPartes.Controls.Add(this.radUnaParte);
            this.grpPartes.Controls.Add(this.radTodasLasPartes);
            this.grpPartes.Controls.Add(this.lblParte);
            this.grpPartes.Controls.Add(this.cboPartes);
            this.grpPartes.Location = new System.Drawing.Point(26, 130);
            this.grpPartes.Name = "grpPartes";
            this.grpPartes.Size = new System.Drawing.Size(496, 82);
            this.grpPartes.TabIndex = 86;
            this.grpPartes.TabStop = false;
            this.grpPartes.Visible = false;
            // 
            // radUnaParte
            // 
            this.radUnaParte.AutoSize = true;
            this.radUnaParte.ForeColor = System.Drawing.SystemColors.Control;
            this.radUnaParte.Location = new System.Drawing.Point(134, 14);
            this.radUnaParte.Name = "radUnaParte";
            this.radUnaParte.Size = new System.Drawing.Size(96, 23);
            this.radUnaParte.TabIndex = 83;
            this.radUnaParte.TabStop = true;
            this.radUnaParte.Text = "Una Parte";
            this.radUnaParte.UseVisualStyleBackColor = true;
            this.radUnaParte.CheckedChanged += new System.EventHandler(this.radUnaParte_CheckedChanged);
            // 
            // radTodasLasPartes
            // 
            this.radTodasLasPartes.AutoSize = true;
            this.radTodasLasPartes.ForeColor = System.Drawing.SystemColors.Control;
            this.radTodasLasPartes.Location = new System.Drawing.Point(243, 14);
            this.radTodasLasPartes.Name = "radTodasLasPartes";
            this.radTodasLasPartes.Size = new System.Drawing.Size(142, 23);
            this.radTodasLasPartes.TabIndex = 84;
            this.radTodasLasPartes.TabStop = true;
            this.radTodasLasPartes.Text = "Todas las partes";
            this.radTodasLasPartes.UseVisualStyleBackColor = true;
            this.radTodasLasPartes.CheckedChanged += new System.EventHandler(this.radTodasLasPartes_CheckedChanged);
            // 
            // lblParte
            // 
            this.lblParte.AutoSize = true;
            this.lblParte.ForeColor = System.Drawing.Color.White;
            this.lblParte.Location = new System.Drawing.Point(79, 42);
            this.lblParte.Name = "lblParte";
            this.lblParte.Size = new System.Drawing.Size(51, 19);
            this.lblParte.TabIndex = 75;
            this.lblParte.Text = "Parte:";
            // 
            // cboPartes
            // 
            this.cboPartes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboPartes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboPartes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPartes.FormattingEnabled = true;
            this.cboPartes.Location = new System.Drawing.Point(137, 39);
            this.cboPartes.Margin = new System.Windows.Forms.Padding(4);
            this.cboPartes.Name = "cboPartes";
            this.cboPartes.Size = new System.Drawing.Size(303, 27);
            this.cboPartes.Sorted = true;
            this.cboPartes.TabIndex = 54;
            this.cboPartes.SelectedIndexChanged += new System.EventHandler(this.cboPartes_SelectedIndexChanged);
            // 
            // lblSerieEquipo
            // 
            this.lblSerieEquipo.AutoSize = true;
            this.lblSerieEquipo.ForeColor = System.Drawing.Color.White;
            this.lblSerieEquipo.Location = new System.Drawing.Point(54, 259);
            this.lblSerieEquipo.Name = "lblSerieEquipo";
            this.lblSerieEquipo.Size = new System.Drawing.Size(50, 19);
            this.lblSerieEquipo.TabIndex = 88;
            this.lblSerieEquipo.Text = "Serie:";
            this.lblSerieEquipo.Visible = false;
            // 
            // cboSeriesEquipos
            // 
            this.cboSeriesEquipos.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboSeriesEquipos.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboSeriesEquipos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSeriesEquipos.FormattingEnabled = true;
            this.cboSeriesEquipos.Location = new System.Drawing.Point(111, 256);
            this.cboSeriesEquipos.Margin = new System.Windows.Forms.Padding(4);
            this.cboSeriesEquipos.Name = "cboSeriesEquipos";
            this.cboSeriesEquipos.Size = new System.Drawing.Size(402, 27);
            this.cboSeriesEquipos.Sorted = true;
            this.cboSeriesEquipos.TabIndex = 87;
            this.cboSeriesEquipos.Visible = false;
            // 
            // chkElegirSerie
            // 
            this.chkElegirSerie.AutoSize = true;
            this.chkElegirSerie.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.chkElegirSerie.Location = new System.Drawing.Point(111, 226);
            this.chkElegirSerie.Name = "chkElegirSerie";
            this.chkElegirSerie.Size = new System.Drawing.Size(199, 23);
            this.chkElegirSerie.TabIndex = 89;
            this.chkElegirSerie.Text = "Escoger Serie de Equipo";
            this.chkElegirSerie.UseVisualStyleBackColor = true;
            this.chkElegirSerie.Visible = false;
            this.chkElegirSerie.CheckedChanged += new System.EventHandler(this.chkElegirSerie_CheckedChanged);
            // 
            // erReporteParteRicoh
            // 
            this.erReporteParteRicoh.ContainerControl = this;
            // 
            // ReporteRegistroPartesRicoh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(69)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(576, 456);
            this.Controls.Add(this.chkElegirSerie);
            this.Controls.Add(this.lblSerieEquipo);
            this.Controls.Add(this.cboSeriesEquipos);
            this.Controls.Add(this.grpPartes);
            this.Controls.Add(this.cboTipoBusqueda);
            this.Controls.Add(this.lblModelos);
            this.Controls.Add(this.cboModelos);
            this.Controls.Add(this.btnGenerar);
            this.Controls.Add(this.dtpFechaFinal);
            this.Controls.Add(this.dtpFechaInicio);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.pSuperior);
            this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ReporteRegistroPartesRicoh";
            this.Text = "ReporteRegistroPartesRicoh";
            this.pSuperior.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).EndInit();
            this.grpPartes.ResumeLayout(false);
            this.grpPartes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erReporteParteRicoh)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pSuperior;
        private System.Windows.Forms.PictureBox btnCerrar;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblModelos;
        private System.Windows.Forms.ComboBox cboModelos;
        private System.Windows.Forms.ComboBox cboTipoBusqueda;
        private System.Windows.Forms.GroupBox grpPartes;
        private System.Windows.Forms.RadioButton radUnaParte;
        private System.Windows.Forms.RadioButton radTodasLasPartes;
        private System.Windows.Forms.Label lblParte;
        private System.Windows.Forms.ComboBox cboPartes;
        private System.Windows.Forms.Label lblSerieEquipo;
        private System.Windows.Forms.ComboBox cboSeriesEquipos;
        private System.Windows.Forms.CheckBox chkElegirSerie;
        private System.Windows.Forms.ErrorProvider erReporteParteRicoh;
    }
}