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
    public partial class ElegirFecha : Form
    {
        private Reportes reportes = new Reportes();
        public ElegirFecha()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            reportes.generarReporte(dateTimePicker1.Value);
        }
    }
}
