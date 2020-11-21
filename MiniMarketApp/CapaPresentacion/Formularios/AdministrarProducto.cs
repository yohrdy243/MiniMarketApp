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
using CapaPresentacion.Formularios;
using CapaPresentacion.Servicios;

namespace CapaPresentacion
{
    public partial class FormEditarProducto : Form
    {
        AdministracionDatos administracionDatos = new AdministracionDatos();
        Producto producto = new Producto();
        MantenedorProducto mantendorProducto;
        public FormEditarProducto(Producto producto,MantenedorProducto mantendorProducto )
        {
            InitializeComponent();
            this.producto = producto;
            this.mantendorProducto = mantendorProducto;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormEditarProducto_Load(object sender, EventArgs e)
        {
            List<Categoria> categorias = administracionDatos.listarCategoria();

            foreach (Categoria categoria in categorias)
            {
                comboBoxCategoria.Items.Add(categoria.NombreCategoria);
            }
            txtNombre.Text = producto.Nombre;
            txtPrecioDeCompra.Text = producto.PrecioCompra.ToString();
            txtPrecioDeVenta.Text = producto.PrecioVenta.ToString();
            txtStock.Text = producto.Stock.ToString();

            comboBoxCategoria.Text = producto.Categoria.NombreCategoria;

        }

        private void FormEditarProducto_FormClosed(object sender, FormClosedEventArgs e)
        {
            mantendorProducto.cerrarMenu();
        }

        private void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            if(float.Parse(txtPrecioDeCompra.Text) == 0 || txtPrecioDeCompra.Text.Equals(""))
            {
                MessageBox.Show("Precio de Compra no valido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecioDeCompra.Text = "";
            
            }
            else if (float.Parse(txtPrecioDeVenta.Text) == 0 || txtPrecioDeVenta.Text.Equals(""))
            {
                MessageBox.Show("Precio de Venta no valido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecioDeVenta.Text = "";

            }
            else if (int.Parse(txtStock.Text) == 0 || txtStock.Text.Equals(""))
            {
                MessageBox.Show("Stock no valido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStock.Text = "";
            }
            else if (float.Parse(txtPrecioDeCompra.Text) >= float.Parse(txtPrecioDeVenta.Text))
            {
                MessageBox.Show("El precio de Venta no puede ser menor al Precio de Compra", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecioDeVenta.Text = "";
                txtPrecioDeCompra.Text = "";
            }
            else 
            {

                Producto productoActualizar = new Producto();
                productoActualizar.IdProducto = producto.IdProducto;
                productoActualizar.Nombre = txtNombre.Text;
                productoActualizar.Stock = Convert.ToInt32(txtStock.Text);
                productoActualizar.PrecioVenta = float.Parse(txtPrecioDeVenta.Text);
                productoActualizar.PrecioCompra = float.Parse(txtPrecioDeCompra.Text);
                productoActualizar.Categoria = administracionDatos.CategoriaPorNombre(comboBoxCategoria.SelectedItem.ToString());
                administracionDatos.editarProducto(productoActualizar);

                MessageBox.Show(producto.Nombre + " se ha editado", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            
        }

        private void txtPrecioDeCompra_Leave(object sender, EventArgs e)
        {
            
        }

        private void txtPrecioDeVenta_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtStock_TextChanged(object sender, EventArgs e)
        {
           
            
        }

        private void txtPrecioDeCompra_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtPrecioDeCompra_KeyUp(object sender, KeyEventArgs e)
        {
            Validaciones.ValidarCampoDecimal((TextBox)sender);
        }

        private void txtPrecioDeVenta_KeyUp(object sender, KeyEventArgs e)
        {
            Validaciones.ValidarCampoDecimal((TextBox)sender);
        }

        private void txtStock_KeyUp(object sender, KeyEventArgs e)
        {
            Validaciones.ValidarCampoEntero((TextBox)sender);
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Desea Eliminar "+producto.Nombre+"?","Advertencia",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            
            if (dialogResult == DialogResult.Yes)
            {
                administracionDatos.eliminarProducto(producto.IdProducto);
                this.Close();
            }
            
        }
    }
}
