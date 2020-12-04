using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio.Entidades
{
    public class ComprobanteDePago
    {
        private long idComprobante;
        public long IdComprobante
        {
            get { return idComprobante; }
            set { idComprobante = value; }
        }

        private long numeroComprobante;
        public long NumeroComprobante
        {
            get { return numeroComprobante; }
            set { numeroComprobante = value; }
        }

        private String nombre;
        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private DateTime fecha;
        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        private String direccion;
        public String Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }

        private long dni;
        public long Dni
        {
            get { return dni; }
            set { dni = value; }
        }

        private float precioTotal;
        public float PrecioTotal
        {
            get { return precioTotal; }
            set { precioTotal = value; }
        }

        private float precioNeto;
        public float PrecioNeto
        {
            get { return precioNeto; }
            set { precioNeto = value; }
        }

        private float igv;
        public float Igv
        {
            get { return igv; }
            set { igv = value; }
        }

        private String correo;

        public String Correo
        {
            get { return correo; }
            set { correo = value; }
        }

        private List<LineaDeVenta> lineasDeVenta;
        public List<LineaDeVenta> LineasDeVenta
        {
            get { return lineasDeVenta; }
            set { lineasDeVenta = value; }
        }
        public void procesarComprobante()
        {
            precioNeto = 0;
            foreach (LineaDeVenta lineaDeVenta in lineasDeVenta)
            {
                precioNeto = precioNeto + lineaDeVenta.Preciototal;
            }

            igv = precioNeto * 0.18f;
            precioNeto = precioNeto - igv;
            precioTotal = precioNeto + igv;
        }

    }
}
