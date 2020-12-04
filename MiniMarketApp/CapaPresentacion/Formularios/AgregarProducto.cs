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
namespace CapaPresentacion
{
    public partial class FormNuevoProducto : Form
    {
        AdministracionDatos administracionDatos = new AdministracionDatos();
        public FormNuevoProducto()
        {
            InitializeComponent();
            Icon icon = new Icon(System.AppDomain.CurrentDomain.BaseDirectory + @"..\..\Resources\MiniMarketLogo.ico");
            this.Icon = icon;
        }

        private void FormNuevoProducto_Load(object sender, EventArgs e)
        {
            List<Categoria> categorias = administracionDatos.listarCategoria();

            foreach (Categoria categoria in categorias)
            {
                comboBoxCategoria.Items.Add(categoria.NombreCategoria);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (float.Parse(textBox2.Text) == 0 || textBox2.Text.Equals(""))
            {
                MessageBox.Show("Precio de Compra no valido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Text = "";

            }
            else if (float.Parse(textBox4.Text) == 0 || textBox4.Text.Equals(""))
            {
                MessageBox.Show("Precio de Venta no valido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox4.Text = "";

            }
            else if (int.Parse(textBox3.Text) == 0 || textBox3.Text.Equals(""))
            {
                MessageBox.Show("Stock no valido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.Text = "";
            }
            else if (float.Parse(textBox2.Text) >= float.Parse(textBox4.Text))
            {
                MessageBox.Show("El precio de Venta no puede ser menor al Precio de Compra", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Text = "";
                textBox4.Text = "";
            }
            else
            {
                Producto producto = new Producto();
                producto.IdProducto = 0; 
                producto.Nombre = textBox5.Text.ToUpper();
                producto.Stock = Convert.ToInt32(textBox3.Text);
                producto.PrecioVenta = float.Parse(textBox4.Text);
                producto.PrecioCompra = float.Parse(textBox2.Text);
                producto.EsMasVendido = cBoxEsMas.Checked;
                producto.Categoria = administracionDatos.CategoriaPorNombre(comboBoxCategoria.SelectedItem.ToString());


                administracionDatos.guardarProducto(producto);

                MessageBox.Show("El Producto "+ producto.Nombre + " ha sido guardado correctamente", "Producto Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult dialogResult = MessageBox.Show("¿Desea Agregar Mas Productos?", "Agregar Productos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    textBox2.Text = "";
                    textBox4.Text = "";
                    textBox3.Text = "";
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            Validaciones.ValidarCampoDecimal((TextBox)sender);
        }

        private void textBox4_KeyUp(object sender, KeyEventArgs e)
        {
            Validaciones.ValidarCampoDecimal((TextBox)sender);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            Validaciones.ValidarCampoEntero((TextBox)sender);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
