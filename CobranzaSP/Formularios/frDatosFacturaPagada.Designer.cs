namespace CobranzaSP.Formularios
{
    partial class frDatosFacturaPagada
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frDatosFacturaPagada));
            this.pSuperior = new System.Windows.Forms.Panel();
            this.btnCerrar = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtImporte = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCobrado = new System.Windows.Forms.Button();
            this.dtpFechaPago = new System.Windows.Forms.DateTimePicker();
            this.erCuentaPagada = new System.Windows.Forms.ErrorProvider(this.components);
            this.pSuperior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erCuentaPagada)).BeginInit();
            this.SuspendLayout();
            // 
            // pSuperior
            // 
            this.pSuperior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(117)))), ((int)(((byte)(20)))));
            this.pSuperior.Controls.Add(this.btnCerrar);
            this.pSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.pSuperior.Location = new System.Drawing.Point(0, 0);
            this.pSuperior.Name = "pSuperior";
            this.pSuperior.Size = new System.Drawing.Size(441, 36);
            this.pSuperior.TabIndex = 0;
            this.pSuperior.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.ErrorImage = null;
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.InitialImage = null;
            this.btnCerrar.Location = new System.Drawing.Point(412, 4);
            this.btnCerrar.Margin = new System.Windows.Forms.Padding(4);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(25, 28);
            this.btnCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnCerrar.TabIndex = 89;
            this.btnCerrar.TabStop = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(57, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 19);
            this.label1.TabIndex = 86;
            this.label1.Text = "Importe:";
            // 
            // txtImporte
            // 
            this.txtImporte.Location = new System.Drawing.Point(135, 44);
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.Size = new System.Drawing.Size(141, 27);
            this.txtImporte.TabIndex = 87;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 19);
            this.label2.TabIndex = 88;
            this.label2.Text = "Fecha de Pago:";
            // 
            // btnCobrado
            // 
            this.btnCobrado.FlatAppearance.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.btnCobrado.FlatAppearance.BorderSize = 2;
            this.btnCobrado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCobrado.ForeColor = System.Drawing.Color.White;
            this.btnCobrado.Image = ((System.Drawing.Image)(resources.GetObject("btnCobrado.Image")));
            this.btnCobrado.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCobrado.Location = new System.Drawing.Point(310, 110);
            this.btnCobrado.Name = "btnCobrado";
            this.btnCobrado.Size = new System.Drawing.Size(117, 51);
            this.btnCobrado.TabIndex = 89;
            this.btnCobrado.Text = "Cobrar";
            this.btnCobrado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCobrado.UseVisualStyleBackColor = true;
            this.btnCobrado.Click += new System.EventHandler(this.btnCobrado_Click);
            // 
            // dtpFechaPago
            // 
            this.dtpFechaPago.Location = new System.Drawing.Point(135, 77);
            this.dtpFechaPago.Name = "dtpFechaPago";
            this.dtpFechaPago.Size = new System.Drawing.Size(292, 27);
            this.dtpFechaPago.TabIndex = 90;
            // 
            // erCuentaPagada
            // 
            this.erCuentaPagada.ContainerControl = this;
            // 
            // frDatosFacturaPagada
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(57)))), ((int)(((byte)(80)))));
            this.ClientSize = new System.Drawing.Size(441, 171);
            this.Controls.Add(this.dtpFechaPago);
            this.Controls.Add(this.btnCobrado);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtImporte);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pSuperior);
            this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frDatosFacturaPagada";
            this.Text = "frDatosFacturaPagada";
            this.pSuperior.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erCuentaPagada)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pSuperior;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtImporte;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox btnCerrar;
        private System.Windows.Forms.Button btnCobrado;
        private System.Windows.Forms.DateTimePicker dtpFechaPago;
        private System.Windows.Forms.ErrorProvider erCuentaPagada;
    }
}