namespace CobranzaSP.Formularios
{
    partial class NuevoModeloParte
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NuevoModeloParte));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCerrar = new System.Windows.Forms.PictureBox();
            this.txtNuevoModelo = new System.Windows.Forms.TextBox();
            this.Mo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboMarcas = new System.Windows.Forms.ComboBox();
            this.cboModelos = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAgregarLista = new System.Windows.Forms.Button();
            this.btnGuardarModelo = new System.Windows.Forms.Button();
            this.lstModelosEquipos = new System.Windows.Forms.ListBox();
            this.btnEliminarLista = new System.Windows.Forms.Button();
            this.erNuevoModelo = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erNuevoModelo)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(117)))), ((int)(((byte)(20)))));
            this.panel1.Controls.Add(this.btnCerrar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(547, 35);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.ErrorImage = null;
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.InitialImage = null;
            this.btnCerrar.Location = new System.Drawing.Point(510, 3);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(25, 28);
            this.btnCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnCerrar.TabIndex = 3;
            this.btnCerrar.TabStop = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // txtNuevoModelo
            // 
            this.txtNuevoModelo.Location = new System.Drawing.Point(123, 53);
            this.txtNuevoModelo.Name = "txtNuevoModelo";
            this.txtNuevoModelo.Size = new System.Drawing.Size(402, 27);
            this.txtNuevoModelo.TabIndex = 68;
            // 
            // Mo
            // 
            this.Mo.AutoSize = true;
            this.Mo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Mo.ForeColor = System.Drawing.Color.White;
            this.Mo.Location = new System.Drawing.Point(51, 56);
            this.Mo.Name = "Mo";
            this.Mo.Size = new System.Drawing.Size(66, 19);
            this.Mo.TabIndex = 67;
            this.Mo.Text = "Modelo:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(51, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(476, 19);
            this.label1.TabIndex = 69;
            this.label1.Text = "MODELOS DE EQUIPOS COMPATIBLES CON MODELO DE PARTE";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(37, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 19);
            this.label2.TabIndex = 70;
            this.label2.Text = "Marca:";
            this.label2.Visible = false;
            // 
            // cboMarcas
            // 
            this.cboMarcas.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboMarcas.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboMarcas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMarcas.FormattingEnabled = true;
            this.cboMarcas.Location = new System.Drawing.Point(100, 139);
            this.cboMarcas.Margin = new System.Windows.Forms.Padding(4);
            this.cboMarcas.Name = "cboMarcas";
            this.cboMarcas.Size = new System.Drawing.Size(267, 27);
            this.cboMarcas.Sorted = true;
            this.cboMarcas.TabIndex = 71;
            this.cboMarcas.SelectedIndexChanged += new System.EventHandler(this.cboMarcas_SelectedIndexChanged);
            // 
            // cboModelos
            // 
            this.cboModelos.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboModelos.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboModelos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboModelos.Enabled = false;
            this.cboModelos.FormattingEnabled = true;
            this.cboModelos.Location = new System.Drawing.Point(100, 179);
            this.cboModelos.Margin = new System.Windows.Forms.Padding(4);
            this.cboModelos.Name = "cboModelos";
            this.cboModelos.Size = new System.Drawing.Size(267, 27);
            this.cboModelos.Sorted = true;
            this.cboModelos.TabIndex = 73;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(27, 182);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 19);
            this.label3.TabIndex = 72;
            this.label3.Text = "Modelo:";
            this.label3.Visible = false;
            // 
            // btnAgregarLista
            // 
            this.btnAgregarLista.FlatAppearance.BorderColor = System.Drawing.Color.Orange;
            this.btnAgregarLista.FlatAppearance.BorderSize = 2;
            this.btnAgregarLista.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarLista.ForeColor = System.Drawing.Color.White;
            this.btnAgregarLista.Location = new System.Drawing.Point(382, 136);
            this.btnAgregarLista.Name = "btnAgregarLista";
            this.btnAgregarLista.Size = new System.Drawing.Size(153, 31);
            this.btnAgregarLista.TabIndex = 74;
            this.btnAgregarLista.Text = "Agregar a lista";
            this.btnAgregarLista.UseVisualStyleBackColor = true;
            this.btnAgregarLista.Click += new System.EventHandler(this.btnAgregarLista_Click);
            // 
            // btnGuardarModelo
            // 
            this.btnGuardarModelo.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.btnGuardarModelo.FlatAppearance.BorderSize = 2;
            this.btnGuardarModelo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarModelo.ForeColor = System.Drawing.Color.White;
            this.btnGuardarModelo.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardarModelo.Image")));
            this.btnGuardarModelo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardarModelo.Location = new System.Drawing.Point(419, 276);
            this.btnGuardarModelo.Name = "btnGuardarModelo";
            this.btnGuardarModelo.Size = new System.Drawing.Size(117, 51);
            this.btnGuardarModelo.TabIndex = 75;
            this.btnGuardarModelo.Text = "Guardar";
            this.btnGuardarModelo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuardarModelo.UseVisualStyleBackColor = true;
            this.btnGuardarModelo.Click += new System.EventHandler(this.btnGuardarModelo_Click);
            // 
            // lstModelosEquipos
            // 
            this.lstModelosEquipos.FormattingEnabled = true;
            this.lstModelosEquipos.ItemHeight = 19;
            this.lstModelosEquipos.Location = new System.Drawing.Point(11, 217);
            this.lstModelosEquipos.Name = "lstModelosEquipos";
            this.lstModelosEquipos.Size = new System.Drawing.Size(356, 118);
            this.lstModelosEquipos.TabIndex = 76;
            // 
            // btnEliminarLista
            // 
            this.btnEliminarLista.FlatAppearance.BorderColor = System.Drawing.Color.Crimson;
            this.btnEliminarLista.FlatAppearance.BorderSize = 2;
            this.btnEliminarLista.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarLista.ForeColor = System.Drawing.Color.White;
            this.btnEliminarLista.Location = new System.Drawing.Point(383, 173);
            this.btnEliminarLista.Name = "btnEliminarLista";
            this.btnEliminarLista.Size = new System.Drawing.Size(153, 31);
            this.btnEliminarLista.TabIndex = 77;
            this.btnEliminarLista.Text = "Eliminar de lista";
            this.btnEliminarLista.UseVisualStyleBackColor = true;
            this.btnEliminarLista.Click += new System.EventHandler(this.btnEliminarLista_Click);
            // 
            // erNuevoModelo
            // 
            this.erNuevoModelo.ContainerControl = this;
            // 
            // NuevoModeloParte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(28)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(547, 338);
            this.Controls.Add(this.btnEliminarLista);
            this.Controls.Add(this.lstModelosEquipos);
            this.Controls.Add(this.btnGuardarModelo);
            this.Controls.Add(this.btnAgregarLista);
            this.Controls.Add(this.cboModelos);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboMarcas);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNuevoModelo);
            this.Controls.Add(this.Mo);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "NuevoModeloParte";
            this.Text = "NuevoModeloParte";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erNuevoModelo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox btnCerrar;
        private System.Windows.Forms.TextBox txtNuevoModelo;
        private System.Windows.Forms.Label Mo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboMarcas;
        private System.Windows.Forms.ComboBox cboModelos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAgregarLista;
        private System.Windows.Forms.Button btnGuardarModelo;
        private System.Windows.Forms.ListBox lstModelosEquipos;
        private System.Windows.Forms.Button btnEliminarLista;
        private System.Windows.Forms.ErrorProvider erNuevoModelo;
    }
}