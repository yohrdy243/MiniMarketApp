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

namespace MiniMarketApp.Formularios
{
    public partial class AdministrarComprobante : Form
    {
        AdministracionDatos administracionDatos = new AdministracionDatos();
        ComprobanteDePago comprobanteDePago = new ComprobanteDePago();
        MantenedorComprobanteDePago mantenedorComprobante;
        public AdministrarComprobante(ComprobanteDePago comprobanteDePago,MantenedorComprobanteDePago mantenedorComprobante)
        {
            InitializeComponent();
            this.comprobanteDePago = comprobanteDePago;
            this.mantenedorComprobante = mantenedorComprobante;
            

        }

        private DataTable generarTabla()
        {
            DataTable tablaLineasDeVenta = new DataTable();

            tablaLineasDeVenta.Columns.Add("N°");
            tablaLineasDeVenta.Columns.Add("Nombre De Producto");
            tablaLineasDeVenta.Columns.Add("Categoria");
            tablaLineasDeVenta.Columns.Add("Cantidad");
            tablaLineasDeVenta.Columns.Add("Precio Unitario");
            tablaLineasDeVenta.Columns.Add("Precio Total Del Producto");

            int i = 1;
            foreach (LineaDeVenta lineaDeVenta in comprobanteDePago.LineasDeVenta)
            {
                DataRow row = tablaLineasDeVenta.NewRow();

                row["N°"] = i;
                row["Nombre De Producto"] = lineaDeVenta.Producto.Nombre;
                row["Categoria"] = lineaDeVenta.Producto.Categoria.NombreCategoria;
                row["Cantidad"] = lineaDeVenta.Cantidad;
                row["Precio Unitario"] = lineaDeVenta.PrecioUnitario;
                row["Precio Total Del Producto"] = lineaDeVenta.Preciototal;

                tablaLineasDeVenta.Rows.Add(row);

            }

            return tablaLineasDeVenta;
        }

        private void listarLineasDeVenta()
        {
            tablaLineasDeVenta.DataSource = generarTabla();
        }
        private void asignarDatos()
        {

        }

        private void AdministrarComprobante_Load(object sender, EventArgs e)
        {
            listarLineasDeVenta();
            txtNombreCliente.Text = comprobanteDePago.Nombre;
            txtDireccionCliente.Text = comprobanteDePago.Direccion;
            txtxDniCliente.Text = comprobanteDePago.Dni.ToString();
            txtCorreo.Text = comprobanteDePago.Correo;
            lblIgv.Text = comprobanteDePago.Igv.ToString();
            lblPrecioNeto.Text = comprobanteDePago.PrecioNeto.ToString();
            lblPrecioTotal.Text = comprobanteDePago.PrecioTotal.ToString();
            dtpFecha.Value = comprobanteDePago.Fecha;
        }

        private void AdministrarComprobante_FormClosed(object sender, FormClosedEventArgs e)
        {
            mantenedorComprobante.cerrarMenu();
        }

        private void btbGuardar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtNombreCliente.Text))
            {
                txtNombreCliente.Text = " ";
            }
            if (String.IsNullOrEmpty(txtxDniCliente.Text))
            {
                txtxDniCliente.Text = "1";
            }
            if (String.IsNullOrEmpty(txtDireccionCliente.Text))
            {
                txtDireccionCliente.Text = " ";
            }
            if (txtCorreo.Text.Equals("@") || String.IsNullOrEmpty(txtCorreo.Text))
            {
                txtCorreo.Text = " ";
            }
            else
            {

                DialogResult dialogResult = MessageBox.Show("¿Desea guardar el Comprobante?", "Guardar Comprobante", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    comprobanteDePago.Nombre = txtNombreCliente.Text;
                    comprobanteDePago.Direccion = txtDireccionCliente.Text;
                    comprobanteDePago.Dni = long.Parse(txtxDniCliente.Text);
                    comprobanteDePago.Correo = txtCorreo.Text;
                    comprobanteDePago.Fecha = dtpFecha.Value;

                    administracionDatos.editarComprobanteDePago(comprobanteDePago);
                    MessageBox.Show("El Comprobante ha sido guardado correctamente", "Guardar Comprobante", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mantenedorComprobante.listarComprobantes();
                    this.Close();

                }
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("¿Desea Eliminar el comprobante ?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                foreach (LineaDeVenta lineaDeVenta in comprobanteDePago.LineasDeVenta)
                {
                    Producto producto = lineaDeVenta.Producto;
                    producto.Stock = producto.Stock + lineaDeVenta.Cantidad;

                    administracionDatos.aumentarStock(producto);
                    administracionDatos.eliminarLineaDeVenta(lineaDeVenta.IdLineaDeVenta);
                }

                administracionDatos.eliminarComprobanteDePago(comprobanteDePago.IdComprobante);
                mantenedorComprobante.listarComprobantes();
                this.Close();
            }
           
        }

        private void txtxDniCliente_KeyUp(object sender, KeyEventArgs e)
        {
            Validaciones.ValidarCampoEntero((TextBox)sender);
        }
    }
}
