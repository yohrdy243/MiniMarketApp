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
using CapaDominio.Servicios;
using CapaPresentacion.Formularios;
using CapaPresentacion.Servicios;

namespace CapaPresentacion
{
    public partial class RegistrarVenta : Form
    {
        private ComprobanteDePago comprobanteDePago = new ComprobanteDePago();
        private List<LineaDeVenta> lineasDeVenta = new List<LineaDeVenta>();
        private ProcesarComprobante procesar = new ProcesarComprobante();
        private AdministracionDatos administracionDatos = new AdministracionDatos();
        public RegistrarVenta()
        {
            InitializeComponent();
            Icon icon = new Icon(System.AppDomain.CurrentDomain.BaseDirectory + @"..\..\Resources\MiniMarketLogo.ico");
            this.Icon = icon;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private DataTable generarTabla(List<LineaDeVenta> lineasDeVenta)
        {
            DataTable tablaProductos = new DataTable();

            tablaProductos.Columns.Add("N°");
            tablaProductos.Columns.Add("Nombre De Producto");
            tablaProductos.Columns.Add("Categoria");
            tablaProductos.Columns.Add("Cantidad");
            tablaProductos.Columns.Add("Precio Unitario");
            tablaProductos.Columns.Add("Precio Total Del Producto");

            int i= 1;
            foreach (LineaDeVenta lineaDeVenta in lineasDeVenta)
            {
                DataRow row = tablaProductos.NewRow();

                row["N°"] = i;
                row["Nombre De Producto"] = lineaDeVenta.Producto.Nombre;
                row["Categoria"] = lineaDeVenta.Producto.Categoria.NombreCategoria;
                row["Cantidad"] = lineaDeVenta.Cantidad;
                row["Precio Unitario"] = lineaDeVenta.PrecioUnitario;
                row["Precio Total Del Producto"] = lineaDeVenta.Preciototal;

                tablaProductos.Rows.Add(row);

            }

            comprobanteDePago.LineasDeVenta = lineasDeVenta;

            comprobanteDePago.procesarComprobante();
            lblPrecioNeto.Text = comprobanteDePago.PrecioNeto.ToString();
            lblIgv.Text = comprobanteDePago.Igv.ToString();
            lblPrecioTotal.Text = comprobanteDePago.PrecioTotal.ToString();
            
            return tablaProductos;
        }
        public void listarLineasDeVenta()
        {
            
            tablaProductos.DataSource = generarTabla(lineasDeVenta);
           
        }
        private void button1_Click(object sender, EventArgs e)
        {
            AgregarProductoForm agregar = new AgregarProductoForm(this);
            agregar.Show();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txtxDniCliente_KeyUp(object sender, KeyEventArgs e)
        {
            Validaciones.ValidarCampoEntero((TextBox)sender);
        }

        public void agregarLineaDeVenta(Producto producto,int cantidad)
        {
            LineaDeVenta lineaDeVenta = new LineaDeVenta();
            lineaDeVenta.Producto = producto;
            lineaDeVenta.Cantidad = cantidad;
            lineaDeVenta = procesar.procesarLineaDeVenta(lineaDeVenta);

            lineasDeVenta.Add(lineaDeVenta);


            if (String.IsNullOrEmpty(txtPaga.Text))
            {
                txtPaga.Text = "0.0";
            }
            else if (float.Parse(txtPaga.Text) - comprobanteDePago.PrecioTotal > 0)
            {
                lblAlerta.Visible = false;
                lblVuelto.Text = (float.Parse(txtPaga.Text) - comprobanteDePago.PrecioTotal).ToString();
            }
            else
            {
                lblAlerta.Visible = true;
            }

            listarLineasDeVenta();
        }

        private void tablaProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int nCelda = int.Parse(tablaProductos.Rows[e.RowIndex].Cells[0].Value.ToString());

            DialogResult dialogResult = MessageBox.Show("¿Desea Eliminar " + tablaProductos.Rows[e.RowIndex].Cells[1].Value.ToString() + "?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                lineasDeVenta.RemoveAt(nCelda);
            }

            if (String.IsNullOrEmpty(txtPaga.Text))
            {
                txtPaga.Text = "0.0";
            }
            else if (float.Parse(txtPaga.Text) - comprobanteDePago.PrecioTotal > 0)
            {
                lblAlerta.Visible = false;
                lblVuelto.Text = (float.Parse(txtPaga.Text) - comprobanteDePago.PrecioTotal).ToString();
            }
            else
            {
                lblAlerta.Visible = true;
            }
            listarLineasDeVenta();
        }

        private void btnGuardarComprobante_Click(object sender, EventArgs e)
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
            if (lineasDeVenta.Count == 0)
            {
                MessageBox.Show("No hay productos seleccionados", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                
                DialogResult dialogResult = MessageBox.Show("¿Desea registrar la Venta?", "Guardar Comprobante", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    comprobanteDePago.Nombre = txtNombreCliente.Text;
                    comprobanteDePago.Direccion = txtDireccionCliente.Text;
                    comprobanteDePago.Dni = long.Parse(txtxDniCliente.Text);
                    comprobanteDePago.Fecha = dtpFecha.Value;

                    administracionDatos.guardarComprobanteDePago(comprobanteDePago);

                    ComprobanteDePago comprobanteDePagoGuardado = administracionDatos.obtenerComprobanteDePagoGuardado();

                    foreach (LineaDeVenta lineaDeVenta in comprobanteDePago.LineasDeVenta)
                    {
                        lineaDeVenta.ComprobanteDePago = comprobanteDePagoGuardado;
                        administracionDatos.guardarLineaDeVenta(lineaDeVenta);
                        lineaDeVenta.Producto.Stock = lineaDeVenta.Producto.Stock - lineaDeVenta.Cantidad;
                        Console.WriteLine("El nuevo stock es : " + lineaDeVenta.Producto.Stock);
                        administracionDatos.disminuirStock(lineaDeVenta.Producto);
                    }
                    MessageBox.Show("El Comprobante ha sido guardado correctamente","Guardar Comprobante", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    comprobanteDePago = new ComprobanteDePago();
                    lineasDeVenta = new List<LineaDeVenta>();
                    listarLineasDeVenta();
                    txtDireccionCliente.Text = "";
                    txtNombreCliente.Text = "";
                    txtxDniCliente.Text = "";
                    txtCorreo.Text = "@";
                    lblIgv.Text = "0.0";
                    lblPrecioNeto.Text = "0.0";
                    lblPrecioTotal.Text = "0.0";
                    lblVuelto.Text = "0.0";
                }
            }

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void txtEntrada_KeyUp(object sender, KeyEventArgs e)
        {
            Validaciones.ValidarCampoDecimal((TextBox)sender);
        }

        private void txtPaga_TextChanged(object sender, EventArgs e)
        {
            
            if(String.IsNullOrEmpty(txtPaga.Text))
            {
                txtPaga.Text = "0.0";
            }
            else if (float.Parse(txtPaga.Text) - comprobanteDePago.PrecioTotal > 0)
            {
                lblAlerta.Visible = false;
                lblVuelto.Text = (float.Parse(txtPaga.Text) - comprobanteDePago.PrecioTotal).ToString();
            }else
            {
                lblAlerta.Visible = true;
            }
        }

        private void RegistrarVenta_Load(object sender, EventArgs e)
        {
            txtPaga.Text = "0.0";
            lblAlerta.Visible = false;
            txtCorreo.Text = "@";
        }
    }
}
