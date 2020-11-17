using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio.Entidades
{
    public class Producto
    {
        private long idProducto;
        public long IdProducto
        {
            get { return idProducto; }
            set { idProducto = value; }
        }

        private float precioVenta;
        public float PrecioVenta
        {
            get { return precioVenta; }
            set { precioVenta = value; }
        }

        private float precioCompra;
        public float PrecioCompra
        {
            get { return precioCompra; }
            set { precioCompra = value; }
        }

        private String descripcion;
        public String Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        private int stock;
        public int Stock
        {
            get { return stock; }
            set { stock = value; }
        }

        private String nombre;
        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private Categoria categoria;
        public Categoria Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }


    }
}
