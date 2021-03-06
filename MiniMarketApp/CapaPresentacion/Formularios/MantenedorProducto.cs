﻿using System;
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

namespace CapaPresentacion.Formularios
{
    public partial class MantenedorProducto : Form
    {
        private AdministracionDatos administracionDatos = new AdministracionDatos();
        private Boolean menuAbierto = false;
        public MantenedorProducto()
        {
            InitializeComponent();
            listarProductos();
            Icon icon = new Icon(System.AppDomain.CurrentDomain.BaseDirectory + @"..\..\Resources\MiniMarketLogo.ico");
            this.Icon = icon;
        }

        private void Productos_Click(object sender, EventArgs e)
        {


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
            tablaProductos.Columns.Add("Mas Vendido");
            foreach(Producto producto in productos)
            {
                DataRow row = tablaProductos.NewRow();

                row["ID Producto"] = producto.IdProducto;
                row["Nombre"] = producto.Nombre;
                row["Categoria"] = producto.Categoria.NombreCategoria;
                row["Stock"] = producto.Stock;
                row["Precio De Venta"] = producto.PrecioVenta;
                row["Precio De Compra"] = producto.PrecioCompra;
                if (producto.EsMasVendido == true)
                {
                    row["Mas Vendido"] = "Si";
                }
                else
                {
                    row["Mas Vendido"] = "No";

                }

                tablaProductos.Rows.Add(row);

            }
            return tablaProductos;
        }
        public void listarProductos()
        {
            List<Producto> productos = administracionDatos.listarProductos();
            tablaProductos.DataSource = generarTabla(productos);
            
        }

        public void listarProductosAlfabeticamete()
        {
            List<Producto> productos = administracionDatos.listarProductosAlfabeticamente();
            tablaProductos.DataSource = generarTabla(productos);

        }
        public void listarProductosMasVendidos()
        {
            List<Producto> productos = administracionDatos.listarProductosMasVendidos();
            tablaProductos.DataSource = generarTabla(productos);

        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            FormNuevoProducto agregarProductoForm = new FormNuevoProducto();
            agregarProductoForm.Show();
        }

        private void tablaProductos_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        public void cerrarMenu()
        {
            menuAbierto = false;
        }


        private void tablaProductos_RowEnter_1(object sender, DataGridViewCellEventArgs e)
        {
            
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBuscarPorProducto_Click(object sender, EventArgs e)
        {
            List<Producto> productos = new List<Producto>();
            Producto producto = administracionDatos.buscarProducto(long.Parse(txtIdProducto.Text));
            productos.Add(producto);

            tablaProductos.DataSource = generarTabla(productos);
            
        }

        private void btnBuscarPorNombre_Click(object sender, EventArgs e)
        {
            List<Producto> productos = administracionDatos.listarProductosPorNombre(txtNombre.Text.ToUpper());
            tablaProductos.DataSource = generarTabla(productos);
        }

        private void comboBoxCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void MantenedorProducto_Load(object sender, EventArgs e)
        {
            cBoxOrden.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            List<Categoria> categorias = administracionDatos.listarCategoria();

            foreach (Categoria categoria in categorias)
            {
                comboBoxCategoria.Items.Add(categoria.NombreCategoria);
            }

            cBoxOrden.Items.Add("Por ID");
            cBoxOrden.Items.Add("Alfabéticamente");
            cBoxOrden.Items.Add("Los Mas Vendidos");
        }

        private void btnBuscarPorCategoria_Click(object sender, EventArgs e)
        {
            Categoria categoria = administracionDatos.CategoriaPorNombre(comboBoxCategoria.SelectedItem.ToString());
            List<Producto> productos = administracionDatos.listarProductosDeCategoria(categoria.IdCategoria);
            tablaProductos.DataSource = generarTabla(productos);
        }

        private void tablaProductos_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            listarProductos();
        }

        private void tablaProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            long idProducto = long.Parse(tablaProductos.Rows[e.RowIndex].Cells[0].Value.ToString());

            Producto producto = administracionDatos.buscarProducto(idProducto);
            FormEditarProducto formEditarProducto = new FormEditarProducto(producto, this);

            if (menuAbierto == false)
            {
                menuAbierto = true;
                formEditarProducto.Show();
            }
        }

        private void txtIdProducto_KeyUp(object sender, KeyEventArgs e)
        {
            Validaciones.ValidarCampoEntero((TextBox)sender);
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
                else if(Convert.ToInt32(e.Value) <= 10 && tablaProductos[6, Int32.Parse(e.RowIndex.ToString())].Value.ToString() == "Si" )
                {
                    e.CellStyle.ForeColor = Color.White;
                    e.CellStyle.BackColor = Color.Red;
                }
                Console.WriteLine();
            }
        }
        

        private void marcarStock(DataGridView tablaProductos)
        {

        }

        private void btnOrdenar_Click(object sender, EventArgs e)
        {
            switch (cBoxOrden.SelectedItem.ToString())
            {
                case "Por ID":
                    listarProductos();
                    break;
                case "Alfabéticamente":
                    listarProductosAlfabeticamete();
                    break;
                case "Los Mas Vendidos":
                    listarProductosMasVendidos();
                    break;
                default:
                    listarProductos();
                    break;
            }
            
        }

        private void tablaProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
