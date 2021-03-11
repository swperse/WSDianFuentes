namespace FacturacionElectronica.Vista
{
    partial class ContenedorFacturas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContenedorFacturas));
            this.GridViewFacturas = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.BtnFacturas = new System.Windows.Forms.Button();
            this.BtnNotasCredito = new System.Windows.Forms.Button();
            this.BtnNotasDebito = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtResultados = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnActualizar = new System.Windows.Forms.Button();
            this.LbLogo = new System.Windows.Forms.Label();
            this.BtnManual = new System.Windows.Forms.Button();
            this.BtnSalir = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewFacturas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GridViewFacturas
            // 
            this.GridViewFacturas.AllowUserToAddRows = false;
            this.GridViewFacturas.AllowUserToDeleteRows = false;
            this.GridViewFacturas.AllowUserToResizeColumns = false;
            this.GridViewFacturas.AllowUserToResizeRows = false;
            this.GridViewFacturas.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.GridViewFacturas.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.GridViewFacturas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridViewFacturas.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.GridViewFacturas.EnableHeadersVisualStyles = false;
            this.GridViewFacturas.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.GridViewFacturas.Location = new System.Drawing.Point(3, 3);
            this.GridViewFacturas.MultiSelect = false;
            this.GridViewFacturas.Name = "GridViewFacturas";
            this.GridViewFacturas.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.GridViewFacturas.RowHeadersVisible = false;
            this.GridViewFacturas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.GridViewFacturas.Size = new System.Drawing.Size(985, 234);
            this.GridViewFacturas.TabIndex = 0;
            this.GridViewFacturas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridViewFacturas_CellClick);
            this.GridViewFacturas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridViewFacturas_CellContentClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Location = new System.Drawing.Point(15, 163);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.BtnFacturas);
            this.splitContainer1.Panel1.Controls.Add(this.BtnNotasCredito);
            this.splitContainer1.Panel1.Controls.Add(this.BtnNotasDebito);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.GridViewFacturas);
            this.splitContainer1.Size = new System.Drawing.Size(1215, 242);
            this.splitContainer1.SplitterDistance = 218;
            this.splitContainer1.TabIndex = 4;
            // 
            // BtnFacturas
            // 
            this.BtnFacturas.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnFacturas.ForeColor = System.Drawing.Color.Teal;
            this.BtnFacturas.Image = global::FacturacionElectronica.Properties.Resources.Facturas;
            this.BtnFacturas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnFacturas.Location = new System.Drawing.Point(25, 3);
            this.BtnFacturas.Name = "BtnFacturas";
            this.BtnFacturas.Size = new System.Drawing.Size(177, 70);
            this.BtnFacturas.TabIndex = 1;
            this.BtnFacturas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnFacturas.UseVisualStyleBackColor = true;
            this.BtnFacturas.Click += new System.EventHandler(this.BtnFacturas_Click);
            // 
            // BtnNotasCredito
            // 
            this.BtnNotasCredito.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNotasCredito.ForeColor = System.Drawing.Color.Teal;
            this.BtnNotasCredito.Image = global::FacturacionElectronica.Properties.Resources.Creditos;
            this.BtnNotasCredito.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnNotasCredito.Location = new System.Drawing.Point(25, 79);
            this.BtnNotasCredito.Name = "BtnNotasCredito";
            this.BtnNotasCredito.Size = new System.Drawing.Size(177, 71);
            this.BtnNotasCredito.TabIndex = 2;
            this.BtnNotasCredito.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnNotasCredito.UseVisualStyleBackColor = true;
            this.BtnNotasCredito.Click += new System.EventHandler(this.BtnNotasCredito_Click);
            // 
            // BtnNotasDebito
            // 
            this.BtnNotasDebito.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNotasDebito.ForeColor = System.Drawing.Color.Teal;
            this.BtnNotasDebito.Image = global::FacturacionElectronica.Properties.Resources.Debitos;
            this.BtnNotasDebito.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnNotasDebito.Location = new System.Drawing.Point(25, 156);
            this.BtnNotasDebito.Name = "BtnNotasDebito";
            this.BtnNotasDebito.Size = new System.Drawing.Size(177, 72);
            this.BtnNotasDebito.TabIndex = 3;
            this.BtnNotasDebito.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnNotasDebito.UseVisualStyleBackColor = true;
            this.BtnNotasDebito.Click += new System.EventHandler(this.BtnNotasDebito_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Teal;
            this.label1.Location = new System.Drawing.Point(12, 408);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 18);
            this.label1.TabIndex = 7;
            this.label1.Text = "Resultados:";
            // 
            // TxtResultados
            // 
            this.TxtResultados.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.TxtResultados.Location = new System.Drawing.Point(15, 429);
            this.TxtResultados.Multiline = true;
            this.TxtResultados.Name = "TxtResultados";
            this.TxtResultados.ReadOnly = true;
            this.TxtResultados.Size = new System.Drawing.Size(1215, 163);
            this.TxtResultados.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(434, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(256, 31);
            this.label3.TabIndex = 9;
            this.label3.Text = "Web Service DIAN";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // BtnActualizar
            // 
            this.BtnActualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnActualizar.Image = ((System.Drawing.Image)(resources.GetObject("BtnActualizar.Image")));
            this.BtnActualizar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnActualizar.Location = new System.Drawing.Point(746, 66);
            this.BtnActualizar.Name = "BtnActualizar";
            this.BtnActualizar.Size = new System.Drawing.Size(151, 69);
            this.BtnActualizar.TabIndex = 10;
            this.BtnActualizar.Text = "ACTUALIZAR";
            this.BtnActualizar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnActualizar.UseVisualStyleBackColor = true;
            this.BtnActualizar.Click += new System.EventHandler(this.BtnActualizar_Click);
            // 
            // LbLogo
            // 
            this.LbLogo.Image = ((System.Drawing.Image)(resources.GetObject("LbLogo.Image")));
            this.LbLogo.Location = new System.Drawing.Point(12, 0);
            this.LbLogo.Name = "LbLogo";
            this.LbLogo.Size = new System.Drawing.Size(361, 148);
            this.LbLogo.TabIndex = 6;
            // 
            // BtnManual
            // 
            this.BtnManual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnManual.Image = ((System.Drawing.Image)(resources.GetObject("BtnManual.Image")));
            this.BtnManual.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnManual.Location = new System.Drawing.Point(923, 66);
            this.BtnManual.Name = "BtnManual";
            this.BtnManual.Size = new System.Drawing.Size(151, 69);
            this.BtnManual.TabIndex = 5;
            this.BtnManual.Text = "MANUAL";
            this.BtnManual.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnManual.UseVisualStyleBackColor = true;
            this.BtnManual.Click += new System.EventHandler(this.BtnManual_Click);
            // 
            // BtnSalir
            // 
            this.BtnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSalir.Image = ((System.Drawing.Image)(resources.GetObject("BtnSalir.Image")));
            this.BtnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnSalir.Location = new System.Drawing.Point(1090, 66);
            this.BtnSalir.Name = "BtnSalir";
            this.BtnSalir.Size = new System.Drawing.Size(140, 69);
            this.BtnSalir.TabIndex = 4;
            this.BtnSalir.Text = "SALIR";
            this.BtnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnSalir.UseVisualStyleBackColor = true;
            this.BtnSalir.Click += new System.EventHandler(this.BtnSalir_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(466, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(185, 26);
            this.label2.TabIndex = 11;
            this.label2.Text = "Versión 2020.10";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // ContenedorFacturas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1242, 604);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BtnActualizar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TxtResultados);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LbLogo);
            this.Controls.Add(this.BtnManual);
            this.Controls.Add(this.BtnSalir);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximumSize = new System.Drawing.Size(1258, 643);
            this.Name = "ContenedorFacturas";
            this.Text = "WS Dian";
            this.Load += new System.EventHandler(this.ContenedorFacturas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GridViewFacturas)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView GridViewFacturas;
        private System.Windows.Forms.Button BtnFacturas;
        private System.Windows.Forms.Button BtnNotasCredito;
        private System.Windows.Forms.Button BtnNotasDebito;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button BtnSalir;
        private System.Windows.Forms.Button BtnManual;
        private System.Windows.Forms.Label LbLogo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtResultados;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BtnActualizar;
        private System.Windows.Forms.Label label2;
    }
}