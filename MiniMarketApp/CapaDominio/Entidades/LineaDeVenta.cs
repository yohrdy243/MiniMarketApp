using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio.Entidades
{
    public class LineaDeVenta
    {
        private long idLineaDeVenta;
        public long IdLineaDeVenta
        {
            get { return idLineaDeVenta; }
            set { idLineaDeVenta = value; }
        }

        private Producto producto;
        public Producto Producto
        {
            get { return producto; }
            set { producto = value; }
        }

        private int cantidad;
        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        private float precioUnitario;
        public float PrecioUnitario
        {
            get { return precioUnitario; }
            set { precioUnitario = value; }
        }

        private float precioTotal;
        public float Preciototal
        {
            get { return precioTotal; }
            set { precioTotal = value; }
        }

        private ComprobanteDePago comprobanteDePago;

        public ComprobanteDePago ComprobanteDePago
        {
            get { return comprobanteDePago; }
            set { comprobanteDePago = value; }
        }

        public void calcularPrecioUnitario()
        {
            precioUnitario = producto.PrecioVenta;
        }
        public void calcularPrecioTotal()
        {
            precioTotal = precioUnitario * cantidad;
        }
    }
}
