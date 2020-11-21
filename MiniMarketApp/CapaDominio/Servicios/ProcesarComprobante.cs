using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDominio.Entidades;
namespace CapaDominio.Servicios
{
    public class ProcesarComprobante
    {
        public List<LineaDeVenta> procesarLineasDeVenta(List<LineaDeVenta> lineasDeVenta)
        {
            foreach (LineaDeVenta lineaDeVenta in lineasDeVenta)
            {
                lineaDeVenta.calcularPrecioUnitario();
                lineaDeVenta.calcularPrecioTotal();
            }
            return lineasDeVenta;
        }

        public ComprobanteDePago procesarComprobante(ComprobanteDePago comprobanteDePago)
        {
            comprobanteDePago.calcularPrecioNeto();
            comprobanteDePago.calcularIgv();
            comprobanteDePago.calcularPrecioTotal();

            return comprobanteDePago;
        }

        public LineaDeVenta procesarLineaDeVenta(LineaDeVenta lineaDeVenta)
        {
            lineaDeVenta.calcularPrecioUnitario();
            lineaDeVenta.calcularPrecioTotal();   
            return lineaDeVenta;
        }
    }
}
