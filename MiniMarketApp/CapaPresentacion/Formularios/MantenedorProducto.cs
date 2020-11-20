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

namespace CapaPresentacion.Formularios
{
    public partial class MantenedorProducto : Form
    {
        private AdministracionDatos administracionDatos = new AdministracionDatos();
        public MantenedorProducto()
        {
            InitializeComponent();
            listarProductos();
        }

        private void Productos_Click(object sender, EventArgs e)
        {


        }
        public void listarProductos()
        {
            List<Producto> productos = administracionDatos.listarProductos();
            if (productos.Count < 0)
            {
                tablaProductos.Columns.Clear();
                BindingSource datosEnlazados = new BindingSource();
                datosEnlazados.DataSource = productos;
                tablaProductos.Rows[0].Selected = false;
            }
        } 
    }
}
