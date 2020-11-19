
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
            this.reportePorProductoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.administrarProductosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registarNuevoProductoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarProductoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarProductoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip2.SuspendLayout();
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
            this.reportePorProductoToolStripMenuItem});
            this.realizarReporteToolStripMenuItem.Name = "realizarReporteToolStripMenuItem";
            this.realizarReporteToolStripMenuItem.Size = new System.Drawing.Size(103, 20);
            this.realizarReporteToolStripMenuItem.Text = "Realizar Reporte";
            // 
            // reporteDiarioToolStripMenuItem
            // 
            this.reporteDiarioToolStripMenuItem.Name = "reporteDiarioToolStripMenuItem";
            this.reporteDiarioToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.reporteDiarioToolStripMenuItem.Text = "Reporte Diario";
            // 
            // reportePorProductoToolStripMenuItem
            // 
            this.reportePorProductoToolStripMenuItem.Name = "reportePorProductoToolStripMenuItem";
            this.reportePorProductoToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.reportePorProductoToolStripMenuItem.Text = "Reporte Por Producto";
            // 
            // administrarProductosToolStripMenuItem
            // 
            this.administrarProductosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registarNuevoProductoToolStripMenuItem,
            this.editarProductoToolStripMenuItem,
            this.eliminarProductoToolStripMenuItem});
            this.administrarProductosToolStripMenuItem.Name = "administrarProductosToolStripMenuItem";
            this.administrarProductosToolStripMenuItem.Size = new System.Drawing.Size(138, 20);
            this.administrarProductosToolStripMenuItem.Text = "Administrar Productos";
            // 
            // registarNuevoProductoToolStripMenuItem
            // 
            this.registarNuevoProductoToolStripMenuItem.Name = "registarNuevoProductoToolStripMenuItem";
            this.registarNuevoProductoToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.registarNuevoProductoToolStripMenuItem.Text = "Registar Nuevo Producto";
            this.registarNuevoProductoToolStripMenuItem.Click += new System.EventHandler(this.registarNuevoProductoToolStripMenuItem_Click);
            // 
            // editarProductoToolStripMenuItem
            // 
            this.editarProductoToolStripMenuItem.Name = "editarProductoToolStripMenuItem";
            this.editarProductoToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.editarProductoToolStripMenuItem.Text = "Editar Producto";
            this.editarProductoToolStripMenuItem.Click += new System.EventHandler(this.editarProductoToolStripMenuItem_Click);
            // 
            // eliminarProductoToolStripMenuItem
            // 
            this.eliminarProductoToolStripMenuItem.Name = "eliminarProductoToolStripMenuItem";
            this.eliminarProductoToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.eliminarProductoToolStripMenuItem.Text = "Eliminar Producto";
            this.eliminarProductoToolStripMenuItem.Click += new System.EventHandler(this.eliminarProductoToolStripMenuItem_Click);
            // 
            // FormMenú
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 405);
            this.Controls.Add(this.menuStrip2);
            this.Name = "FormMenú";
            this.Text = "Minimarket Sunset";
            this.Load += new System.EventHandler(this.FormMenú_Load);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem registrarVentaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem realizarReporteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem administrarProductosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registarNuevoProductoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editarProductoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarProductoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteDiarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportePorProductoToolStripMenuItem;
    }
}