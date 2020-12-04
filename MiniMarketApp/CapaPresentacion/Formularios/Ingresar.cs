using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            Icon icon = new Icon(System.AppDomain.CurrentDomain.BaseDirectory + @"..\..\Resources\MiniMarketLogo.ico");
            this.Icon = icon;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "1505")
            {
                FormMenú menuMinimarket = new FormMenú();

                menuMinimarket.Show();

                this.Hide();
            }
            else
            {
                if (String.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("No se ingresó Contraseña", "Contraseña Incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("La Contraseña que ingresada es Incorrecta", "Contraseña Incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                   
            }
            
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
