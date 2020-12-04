
namespace CapaPresentacion
{
    partial class FormMenú
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
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.registrarVentaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.realizarReporteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteDiarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportePorFechaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteEnUnRangoDeTiempoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.administrarProductosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MantendorProdcutosStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comprobantesDePagoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registrarVentaToolStripMenuItem,
            this.realizarReporteToolStripMenuItem,
            this.administrarProductosToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(762, 24);
            this.menuStrip2.TabIndex = 1;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // registrarVentaToolStripMenuItem
            // 
            this.registrarVentaToolStripMenuItem.Name = "registrarVentaToolStripMenuItem";
            this.registrarVentaToolStripMenuItem.Size = new System.Drawing.Size(97, 20);
            this.registrarVentaToolStripMenuItem.Text = "Registrar Venta";
            this.registrarVentaToolStripMenuItem.Click += new System.EventHandler(this.registrarVentaToolStripMenuItem_Click);
            // 
            // realizarReporteToolStripMenuItem
            // 
            this.realizarReporteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reporteDiarioToolStripMenuItem,
            this.reportePorFechaToolStripMenuItem,
            this.reporteEnUnRangoDeTiempoToolStripMenuItem});
            this.realizarReporteToolStripMenuItem.Name = "realizarReporteToolStripMenuItem";
            this.realizarReporteToolStripMenuItem.Size = new System.Drawing.Size(103, 20);
            this.realizarReporteToolStripMenuItem.Text = "Realizar Reporte";
            // 
            // reporteDiarioToolStripMenuItem
            // 
            this.reporteDiarioToolStripMenuItem.Name = "reporteDiarioToolStripMenuItem";
            this.reporteDiarioToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.reporteDiarioToolStripMenuItem.Text = "Reporte Diario";
            this.reporteDiarioToolStripMenuItem.Click += new System.EventHandler(this.reporteDiarioToolStripMenuItem_Click);
            // 
            // reportePorFechaToolStripMenuItem
            // 
            this.reportePorFechaToolStripMenuItem.Name = "reportePorFechaToolStripMenuItem";
            this.reportePorFechaToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.reportePorFechaToolStripMenuItem.Text = "Reporte por Fecha";
            this.reportePorFechaToolStripMenuItem.Click += new System.EventHandler(this.reportePorFechaToolStripMenuItem_Click);
            // 
            // reporteEnUnRangoDeTiempoToolStripMenuItem
            // 
            this.reporteEnUnRangoDeTiempoToolStripMenuItem.Name = "reporteEnUnRangoDeTiempoToolStripMenuItem";
            this.reporteEnUnRangoDeTiempoToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.reporteEnUnRangoDeTiempoToolStripMenuItem.Text = "Reporte entre Fechas";
            this.reporteEnUnRangoDeTiempoToolStripMenuItem.Click += new System.EventHandler(this.reporteEnUnRangoDeTiempoToolStripMenuItem_Click);
            // 
            // administrarProductosToolStripMenuItem
            // 
            this.administrarProductosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MantendorProdcutosStripMenuItem,
            this.comprobantesDePagoToolStripMenuItem});
            this.administrarProductosToolStripMenuItem.Name = "administrarProductosToolStripMenuItem";
            this.administrarProductosToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.administrarProductosToolStripMenuItem.Text = "Administrar";
            // 
            // MantendorProdcutosStripMenuItem
            // 
            this.MantendorProdcutosStripMenuItem.Name = "MantendorProdcutosStripMenuItem";
            this.MantendorProdcutosStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.MantendorProdcutosStripMenuItem.Text = "Productos";
            this.MantendorProdcutosStripMenuItem.Click += new System.EventHandler(this.registarNuevoProductoToolStripMenuItem_Click);
            // 
            // comprobantesDePagoToolStripMenuItem
            // 
            this.comprobantesDePagoToolStripMenuItem.Name = "comprobantesDePagoToolStripMenuItem";
            this.comprobantesDePagoToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.comprobantesDePagoToolStripMenuItem.Text = "Comprobantes de Pago";
            this.comprobantesDePagoToolStripMenuItem.Click += new System.EventHandler(this.comprobantesDePagoToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MiniMarketApp.Properties.Resources.MiniMarketLogo;
            this.pictureBox1.Location = new System.Drawing.Point(260, 70);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(250, 250);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // FormMenú
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 405);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip2);
            this.Name = "FormMenú";
            this.Text = "Minimarket Sunset";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMenú_FormClosed);
            this.Load += new System.EventHandler(this.FormMenú_Load);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem registrarVentaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem realizarReporteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem administrarProductosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MantendorProdcutosStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteDiarioToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem reportePorFechaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem comprobantesDePagoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteEnUnRangoDeTiempoToolStripMenuItem;
    }
}