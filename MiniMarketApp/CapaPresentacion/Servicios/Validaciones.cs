using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Servicios
{
    class Validaciones
    {
        public static bool ValidarCampoEntero(TextBox CajaDeTexto)
        {
            try
            {
                int d = Convert.ToInt32(CajaDeTexto.Text);
                return true;
            }
            catch (Exception ex)
            {
                CajaDeTexto.Text = "0";
                CajaDeTexto.Select(0, CajaDeTexto.Text.Length);
                return false;
            }
        }
        public static bool ValidarCampoDecimal(TextBox CajaDeTexto)
        {
            try
            {
                decimal d = Convert.ToDecimal(CajaDeTexto.Text);
                return true;
            }
            catch (Exception ex)
            {
                CajaDeTexto.Text = "0";
                CajaDeTexto.Select(0, CajaDeTexto.Text.Length);
                return false;
            }
        }
        public static bool validarMayor(TextBox CajaDeTextoPrecioVenta,TextBox CajaDeTextoPrecioCompra)
        {
            decimal d1 = Convert.ToDecimal(CajaDeTextoPrecioVenta.Text);
            decimal d2 = Convert.ToDecimal(CajaDeTextoPrecioCompra.Text);

            if (d1 < d2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        

    }
}
