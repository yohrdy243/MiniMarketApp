using CapaPresentacion.Formularios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaAplicacion.Servicios;
using CapaDominio.Entidades;
using CapaPresentacion.Servicios;

namespace CapaPresentacion
{
    public partial class FormMenú : Form
    { 

        private Reportes reportes = new Reportes();

        public FormMenú()
        {
            InitializeComponent();
        }

        private void registrarVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegistrarVenta Venta = new RegistrarVenta();
            Venta.Show();
        }

        private void registarNuevoProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MantenedorProducto mantendorProductos = new MantenedorProducto();
            mantendorProductos.Show();
        }

        private void editarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void eliminarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void FormMenú_Load(object sender, EventArgs e)
        {

        }

        private void FormMenú_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void reporteDiarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reportes.repoteDiario();
        }
    }
}
