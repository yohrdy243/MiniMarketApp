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

        private int numeroDeLinea;
        public int NumeroDeLinea
        {
            get { return numeroDeLinea; }
            set { numeroDeLinea = value; }
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

        private void calcularPrecioUnitario()
        {
            precioUnitario = producto.PrecioVenta;
        }
        private void calcularPrecioTotal()
        {
            precioTotal = precioUnitario * cantidad;
        }
    }
}
