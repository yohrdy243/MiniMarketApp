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
    public partial class AgregarProductoForm : Form
    {
        private AdministracionDatos administracionDatos = new AdministracionDatos();
        private RegistrarVenta registrarVenta;
        public AgregarProductoForm(RegistrarVenta registrarVenta)
        {
            InitializeComponent();
            this.registrarVenta = registrarVenta;
            listarProductos();
            Icon icon = new Icon(System.AppDomain.CurrentDomain.BaseDirectory + @"..\..\Resources\MiniMarketLogo.ico");
            this.Icon = icon;
        }
        private DataTable generarTabla(List<Producto> productos)
        {
            DataTable tablaProductos = new DataTable();

            tablaProductos.Columns.Add("ID Producto");
            tablaProductos.Columns.Add("Nombre");
            tablaProductos.Columns.Add("Categoria");
            tablaProductos.Columns.Add("Stock");
            tablaProductos.Columns.Add("Precio De Venta");
            tablaProductos.Columns.Add("Precio De Compra");

            foreach (Producto producto in productos)
            {
                DataRow row = tablaProductos.NewRow();

                row["ID Producto"] = producto.IdProducto;
                row["Nombre"] = producto.Nombre;
                row["Categoria"] = producto.Categoria.NombreCategoria;
                row["Stock"] = producto.Stock;
                row["Precio De Venta"] = producto.PrecioVenta;
                row["Precio De Compra"] = producto.PrecioCompra;

                tablaProductos.Rows.Add(row);

            }
            return tablaProductos;
        }

        public void listarProductos()
        {
            List<Producto> productos = administracionDatos.listarProductos();
            tablaProductos.DataSource = generarTabla(productos);
           

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void AgregarProductoForm_Load(object sender, EventArgs e)
        {
            List<Categoria> categorias = administracionDatos.listarCategoria();

            foreach (Categoria categoria in categorias)
            {
                comboBoxCategoria.Items.Add(categoria.NombreCategoria);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void buscarPorID_Click(object sender, EventArgs e)
        {
            List<Producto> productos = new List<Producto>();
            Producto producto = administracionDatos.buscarProducto(long.Parse(txtIdProducto.Text));
            productos.Add(producto);

            tablaProductos.DataSource = generarTabla(productos);
        }

        private void btnBuscarPorNombre_Click(object sender, EventArgs e)
        {
            List<Producto> productos = administracionDatos.listarProductosPorNombre(txtNombreProducto.Text.ToUpper());
            tablaProductos.DataSource = generarTabla(productos);
        }

        private void btnBuscarPorCategoria_Click(object sender, EventArgs e)
        {
            Categoria categoria = administracionDatos.CategoriaPorNombre(comboBoxCategoria.SelectedItem.ToString());
            List<Producto> productos = administracionDatos.listarProductosDeCategoria(categoria.IdCategoria);
            tablaProductos.DataSource = generarTabla(productos);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listarProductos();
        }

        private void txtIdProducto_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtIdProducto_KeyUp(object sender, KeyEventArgs e)
        {
            Validaciones.ValidarCampoEntero((TextBox)sender);
        }

        private void txtCantidad_KeyUp(object sender, KeyEventArgs e)
        {
            Validaciones.ValidarCampoEntero((TextBox)sender);
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
           
        }

        private void tablaProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            long idProducto = long.Parse(tablaProductos.Rows[e.RowIndex].Cells[0].Value.ToString());

            Producto producto = administracionDatos.buscarProducto(idProducto);
            Categoria categoria = administracionDatos.buscarCategoria(producto.Categoria.IdCategoria);

            producto.Categoria = categoria;
            if (String.IsNullOrEmpty(txtCantidad.Text) )
            {

            }
            else if (int.Parse(txtCantidad.Text) == 0)
            {
                MessageBox.Show("No se peude agregar un producto con la cantidad 0", "Agregar Producto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if(producto.Stock >= int.Parse(txtCantidad.Text))
            {
                DialogResult dialogResult = MessageBox.Show("¿Desea Agregar " + producto.Nombre + "?", "Agregar Producto", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (dialogResult == DialogResult.Yes)
                {
                    registrarVenta.agregarLineaDeVenta(producto,int.Parse(txtCantidad.Text));
                    this.Close();
                }
                txtCantidad.Text = "";
            }
            else
            {
                MessageBox.Show("El producto " + producto.Nombre + " no tiene el stock suficiente", "Agregar Producto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCantidad.Text = "";
            }
        }

        private void tablaProductos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (tablaProductos.Columns[e.ColumnIndex].Name == "Stock")
            {
                if (Convert.ToInt32(e.Value) <= 5)
                {
                    e.CellStyle.ForeColor = Color.White;
                    e.CellStyle.BackColor = Color.Red;
                }
            }
        }
    }
}
