using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FormMenú : Form
    {
        public FormMenú()
        {
            InitializeComponent();
        }

        private void registrarVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void registarNuevoProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormNuevoProducto formNuevoProducto = new FormNuevoProducto();
            formNuevoProducto.Show();
        }

        private void editarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEditarProducto formEditarProducto = new FormEditarProducto();
            formEditarProducto.Show();
        }

        private void eliminarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormElimniarProducto formEliminarProducto = new FormElimniarProducto();
            formEliminarProducto.Show();
        }
    }
}
