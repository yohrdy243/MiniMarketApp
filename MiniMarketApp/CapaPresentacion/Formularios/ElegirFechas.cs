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
    public partial class ElegirFechas : Form
    {
        private Reportes reportes = new Reportes();
        public ElegirFechas()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int resultado = DateTime.Compare(dtFechaInicio.Value, dtFechaFin.Value);

            if (resultado < 0)
            {
                reportes.generarReporteEntreFechas(dtFechaInicio.Value, dtFechaFin.Value);
            }
            else
            {
                MessageBox.Show("La fecha de inicio debe ser mas antigua que la fecha de fin", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
