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
    public partial class MantenedorComprobanteDePago : Form
    {
        AdministracionDatos administracionDatos = new AdministracionDatos();
        private Boolean menuAbierto = false;
        public MantenedorComprobanteDePago()
        {
            InitializeComponent();
        }

        private void MantenedorComprobanteDePago_Load(object sender, EventArgs e)
        {
            cBOrdenar.DropDownStyle = ComboBoxStyle.DropDownList;
            cBOrdenar.Items.Add("Fecha Antigua a Actual");
            cBOrdenar.Items.Add("Fecha Actual a Antigua");
            cBOrdenar.Items.Add("Nombre del Cliente");
            listarComprobantes();
        }
        private DataTable generarTabla(List<ComprobanteDePago> comprobantesDePago)
        {
            DataTable tablaComprobantes = new DataTable();

            tablaComprobantes.Columns.Add("ID Comprobante");
            tablaComprobantes.Columns.Add("Nombre Del Cliente");
            tablaComprobantes.Columns.Add("DNI");
            tablaComprobantes.Columns.Add("Correo Electronico");
            tablaComprobantes.Columns.Add("Precio Neto");
            tablaComprobantes.Columns.Add("IGV");
            tablaComprobantes.Columns.Add("Precio Total");
            tablaComprobantes.Columns.Add("Fecha");

            foreach (ComprobanteDePago comprobanteDePago in comprobantesDePago)
            {
                DataRow row = tablaComprobantes.NewRow();

                row["ID Comprobante"] = comprobanteDePago.IdComprobante;
                row["Nombre Del Cliente"] = comprobanteDePago.Nombre;
                row["DNI"] = comprobanteDePago.Dni;
                row["Correo Electronico"] = comprobanteDePago.Correo;
                row["Precio Neto"] = comprobanteDePago.PrecioNeto;
                row["IGV"] = comprobanteDePago.Igv;
                row["Precio Total"] = comprobanteDePago.PrecioTotal;
                row["Fecha"] = comprobanteDePago.Fecha.ToString("dd-M-yyyy");
                tablaComprobantes.Rows.Add(row);

            }

            return tablaComprobantes;
        }

        public void listarComprobantes()
        {
            List<ComprobanteDePago> comprobantes = administracionDatos.listarComprobanteDePago();

            tablaComprobantes.DataSource = generarTabla(comprobantes);


        }
        public void listarComprobantesAlphafeticamente()
        {
            List<ComprobanteDePago> comprobantes = administracionDatos.listarComprobanteDePagoAlphabeticamente();

            tablaComprobantes.DataSource = generarTabla(comprobantes);


        }
        public void listarComprobantesAntiguaNuevo()
        {
            List<ComprobanteDePago> comprobantes = administracionDatos.listarComprobanteDePagoAntiguoNuevo();

            tablaComprobantes.DataSource = generarTabla(comprobantes);


        }
        public void listarComprobantesNuevoAntiguo()
        {
            List<ComprobanteDePago> comprobantes = administracionDatos.listarComprobanteDePagoNuevoAntiguo();
            tablaComprobantes.DataSource = generarTabla(comprobantes);


        }

        private void button1_Click(object sender, EventArgs e)
        {
           listarComprobantes();
        }

        private void tablaComprobantes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            long idComprobante = long.Parse(tablaComprobantes.Rows[e.RowIndex].Cells[0].Value.ToString());

            ComprobanteDePago comprobanteDePago = administracionDatos.buscarComprobanteDePago(idComprobante);
            AdministrarComprobante administrarComprobante = new AdministrarComprobante(comprobanteDePago,this);

            if (menuAbierto == false)
            {
                menuAbierto = true;
                administrarComprobante.Show();
            }
        }

        public void cerrarMenu()
        {
            menuAbierto = false;
        }

        private void btnBuscarPorDni_Click(object sender, EventArgs e)
        {
            List<ComprobanteDePago> comprobantesDePago =  administracionDatos.listarComprobanteDePagoPorDni(long.Parse(txtDni.Text));

            tablaComprobantes.DataSource = generarTabla(comprobantesDePago);
        }

        private void btnBuscarPorNombre_Click(object sender, EventArgs e)
        {
            List<ComprobanteDePago> comprobantesDePago = administracionDatos.listarComprobanteDePagoPorNombre(txtNombre.Text);

            tablaComprobantes.DataSource = generarTabla(comprobantesDePago);
        }

        private void btnBuscarPorCategoria_Click(object sender, EventArgs e)
        {
            List<ComprobanteDePago> comprobantesDePago = administracionDatos.listarComprobanteDePagoPorfecha(dtFecha.Value);

            tablaComprobantes.DataSource = generarTabla(comprobantesDePago);
        }

        private void txtDni_KeyUp(object sender, KeyEventArgs e)
        {
            Validaciones.ValidarCampoEntero((TextBox)sender);
        }

        private void btnOrdenar_Click(object sender, EventArgs e)
        {
            switch (cBOrdenar.SelectedItem.ToString())
            {
                case "Fecha Antigua a Actual":
                    listarComprobantesAntiguaNuevo();
                    break;
                case "Fecha Actual a Antigua":
                    listarComprobantesNuevoAntiguo();
                    break;
                case "Nombre del Cliente":
                    listarComprobantesAlphafeticamente();
                    break;
                default:
                    listarComprobantes();
                    break;
        }
        }
    }
}
