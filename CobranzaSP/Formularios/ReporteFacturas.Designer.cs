namespace CobranzaSP.Formularios
{
    partial class ReporteFacturas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReporteFacturas));
            this.pSuperior = new System.Windows.Forms.Panel();
            this.btnCerrar = new System.Windows.Forms.PictureBox();
            this.cboOpcionReporte = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGenerarReporte = new System.Windows.Forms.Button();
            this.grpCliente = new System.Windows.Forms.GroupBox();
            this.rdUnCliente = new System.Windows.Forms.RadioButton();
            this.cboClientes = new System.Windows.Forms.ComboBox();
            this.radTodosLosClientes = new System.Windows.Forms.RadioButton();
            this.erReporteFactura = new System.Windows.Forms.ErrorProvider(this.components);
            this.pSuperior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).BeginInit();
            this.grpCliente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erReporteFactura)).BeginInit();
            this.SuspendLayout();
            // 
            // pSuperior
            // 
            this.pSuperior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(117)))), ((int)(((byte)(20)))));
            this.pSuperior.Controls.Add(this.btnCerrar);
            this.pSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.pSuperior.Location = new System.Drawing.Point(0, 0);
            this.pSuperior.Name = "pSuperior";
            this.pSuperior.Size = new System.Drawing.Size(504, 36);
            this.pSuperior.TabIndex = 0;
            this.pSuperior.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pSuperior_MouseDown);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.ErrorImage = null;
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.InitialImage = null;
            this.btnCerrar.Location = new System.Drawing.Point(476, 3);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(25, 28);
            this.btnCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnCerrar.TabIndex = 24;
            this.btnCerrar.TabStop = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click_1);
            // 
            // cboOpcionReporte
            // 
            this.cboOpcionReporte.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboOpcionReporte.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboOpcionReporte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOpcionReporte.FormattingEnabled = true;
            this.cboOpcionReporte.Location = new System.Drawing.Point(146, 54);
            this.cboOpcionReporte.Margin = new System.Windows.Forms.Padding(4);
            this.cboOpcionReporte.Name = "cboOpcionReporte";
            this.cboOpcionReporte.Size = new System.Drawing.Size(322, 27);
            this.cboOpcionReporte.Sorted = true;
            this.cboOpcionReporte.TabIndex = 23;
            this.cboOpcionReporte.SelectedIndexChanged += new System.EventHandler(this.cboOpcionReporte_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(39, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 19);
            this.label1.TabIndex = 22;
            this.label1.Text = "Generar por:";
            // 
            // btnGenerarReporte
            // 
            this.btnGenerarReporte.FlatAppearance.BorderColor = System.Drawing.Color.Crimson;
            this.btnGenerarReporte.FlatAppearance.BorderSize = 2;
            this.btnGenerarReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarReporte.ForeColor = System.Drawing.Color.White;
            this.btnGenerarReporte.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarReporte.Image")));
            this.btnGenerarReporte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerarReporte.Location = new System.Drawing.Point(351, 196);
            this.btnGenerarReporte.Name = "btnGenerarReporte";
            this.btnGenerarReporte.Size = new System.Drawing.Size(117, 51);
            this.btnGenerarReporte.TabIndex = 29;
            this.btnGenerarReporte.Text = "Generar";
            this.btnGenerarReporte.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGenerarReporte.UseVisualStyleBackColor = true;
            this.btnGenerarReporte.Click += new System.EventHandler(this.btnGenerarReporte_Click);
            // 
            // grpCliente
            // 
            this.grpCliente.Controls.Add(this.rdUnCliente);
            this.grpCliente.Controls.Add(this.cboClientes);
            this.grpCliente.Controls.Add(this.radTodosLosClientes);
            this.grpCliente.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.grpCliente.Location = new System.Drawing.Point(63, 85);
            this.grpCliente.Name = "grpCliente";
            this.grpCliente.Size = new System.Drawing.Size(402, 82);
            this.grpCliente.TabIndex = 84;
            this.grpCliente.TabStop = false;
            this.grpCliente.Text = "Cliente";
            this.grpCliente.Visible = false;
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
            // 
            // radTodosLosClientes
            // 
            this.radTodosLosClientes.AutoSize = true;
            this.radTodosLosClientes.Location = new System.Drawing.Point(155, 17);
            this.radTodosLosClientes.Name = "radTodosLosClientes";
            this.radTodosLosClientes.Size = new System.Drawing.Size(71, 23);
            this.radTodosLosClientes.TabIndex = 71;
            this.radTodosLosClientes.TabStop = true;
            this.radTodosLosClientes.Text = "Todos";
            this.radTodosLosClientes.UseVisualStyleBackColor = true;
            this.radTodosLosClientes.CheckedChanged += new System.EventHandler(this.radTodosLosClientes_CheckedChanged);
            // 
            // erReporteFactura
            // 
            this.erReporteFactura.ContainerControl = this;
            // 
            // ReporteFacturas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(57)))), ((int)(((byte)(80)))));
            this.ClientSize = new System.Drawing.Size(504, 263);
            this.Controls.Add(this.grpCliente);
            this.Controls.Add(this.btnGenerarReporte);
            this.Controls.Add(this.cboOpcionReporte);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pSuperior);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ReporteFacturas";
            this.Text = "ReporteFacturas";
            this.pSuperior.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).EndInit();
            this.grpCliente.ResumeLayout(false);
            this.grpCliente.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erReporteFactura)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pSuperior;
        private System.Windows.Forms.ComboBox cboOpcionReporte;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox btnCerrar;
        private System.Windows.Forms.Button btnGenerarReporte;
        private System.Windows.Forms.GroupBox grpCliente;
        private System.Windows.Forms.RadioButton rdUnCliente;
        private System.Windows.Forms.ComboBox cboClientes;
        private System.Windows.Forms.RadioButton radTodosLosClientes;
        private System.Windows.Forms.ErrorProvider erReporteFactura;
    }
}