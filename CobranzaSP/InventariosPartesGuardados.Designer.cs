namespace CobranzaSP
{
    partial class InventariosPartesGuardados
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InventariosPartesGuardados));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pSuperior = new System.Windows.Forms.Panel();
            this.btnCerrar = new System.Windows.Forms.PictureBox();
            this.dtgInventariosPartes = new System.Windows.Forms.DataGridView();
            this.btnAbrirArchivo = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pSuperior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgInventariosPartes)).BeginInit();
            this.SuspendLayout();
            // 
            // pSuperior
            // 
            this.pSuperior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(117)))), ((int)(((byte)(20)))));
            this.pSuperior.Controls.Add(this.btnCerrar);
            this.pSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.pSuperior.Location = new System.Drawing.Point(0, 0);
            this.pSuperior.Name = "pSuperior";
            this.pSuperior.Size = new System.Drawing.Size(438, 36);
            this.pSuperior.TabIndex = 0;
            this.pSuperior.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pSuperior_MouseDown);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.ErrorImage = null;
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.InitialImage = null;
            this.btnCerrar.Location = new System.Drawing.Point(410, 3);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(25, 28);
            this.btnCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnCerrar.TabIndex = 7;
            this.btnCerrar.TabStop = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // dtgInventariosPartes
            // 
            this.dtgInventariosPartes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgInventariosPartes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dtgInventariosPartes.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(90)))));
            this.dtgInventariosPartes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(117)))), ((int)(((byte)(20)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(117)))), ((int)(((byte)(20)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgInventariosPartes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dtgInventariosPartes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.InfoText;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgInventariosPartes.DefaultCellStyle = dataGridViewCellStyle6;
            this.dtgInventariosPartes.EnableHeadersVisualStyles = false;
            this.dtgInventariosPartes.Location = new System.Drawing.Point(21, 163);
            this.dtgInventariosPartes.Name = "dtgInventariosPartes";
            this.dtgInventariosPartes.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(90)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgInventariosPartes.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(90)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgInventariosPartes.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dtgInventariosPartes.RowTemplate.Height = 25;
            this.dtgInventariosPartes.Size = new System.Drawing.Size(397, 349);
            this.dtgInventariosPartes.TabIndex = 40;
            this.dtgInventariosPartes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgInventariosPartes_CellClick);
            // 
            // btnAbrirArchivo
            // 
            this.btnAbrirArchivo.FlatAppearance.BorderColor = System.Drawing.Color.DarkBlue;
            this.btnAbrirArchivo.FlatAppearance.BorderSize = 2;
            this.btnAbrirArchivo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbrirArchivo.ForeColor = System.Drawing.Color.White;
            this.btnAbrirArchivo.Image = ((System.Drawing.Image)(resources.GetObject("btnAbrirArchivo.Image")));
            this.btnAbrirArchivo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAbrirArchivo.Location = new System.Drawing.Point(258, 106);
            this.btnAbrirArchivo.Name = "btnAbrirArchivo";
            this.btnAbrirArchivo.Size = new System.Drawing.Size(160, 51);
            this.btnAbrirArchivo.TabIndex = 87;
            this.btnAbrirArchivo.Text = "Abrir Archivo";
            this.btnAbrirArchivo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAbrirArchivo.UseVisualStyleBackColor = true;
            this.btnAbrirArchivo.Click += new System.EventHandler(this.btnAbrirArchivo_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnEliminar.FlatAppearance.BorderSize = 2;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEliminar.Location = new System.Drawing.Point(21, 106);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(117, 51);
            this.btnEliminar.TabIndex = 88;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(71, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(304, 19);
            this.label1.TabIndex = 89;
            this.label1.Text = "INVENTARIOS DE PARTES GUARDADOS";
            // 
            // InventariosPartesGuardados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(69)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(438, 524);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnAbrirArchivo);
            this.Controls.Add(this.dtgInventariosPartes);
            this.Controls.Add(this.pSuperior);
            this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "InventariosPartesGuardados";
            this.Text = "InventariosPartesGuardados";
            this.pSuperior.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgInventariosPartes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pSuperior;
        private System.Windows.Forms.PictureBox btnCerrar;
        private System.Windows.Forms.DataGridView dtgInventariosPartes;
        private System.Windows.Forms.Button btnAbrirArchivo;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Label label1;
    }
}