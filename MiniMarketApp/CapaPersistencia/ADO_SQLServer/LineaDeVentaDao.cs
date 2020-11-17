using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDominio.Contratos;
using CapaDominio.Entidades;

namespace CapaPersistencia.ADO_SQLServer
{
    public class LineaDeVentaDao : ILineaDeVenta
    {
        private GestorSQL gestorSQL;

        public LineaDeVentaDao(IGestorAccesoDatos gestorSQL)
        {
            this.gestorSQL = (GestorSQL)gestorSQL;
        }
        public LineaDeVenta buscarLineaDeVenta(long idLineaDeVenta)
        {
            throw new NotImplementedException();
        }

        public void crearLineaDeVenta(LineaDeVenta lineaDeVenta)
        {
            throw new NotImplementedException();
        }

        public void editarLineaDeVenta(LineaDeVenta lineaDeVenta)
        {
            throw new NotImplementedException();
        }

        public void eliminarLineaDeVenta(long idLineaDeVenta)
        {
            throw new NotImplementedException();
        }

        public List<LineaDeVenta> listarLineasDeVenta()
        {
            throw new NotImplementedException();
        }

        public List<LineaDeVenta> listarLineasDeVentaDeComprobante(ComprobanteDePago comprobanteDePago)
        {
            throw new NotImplementedException();
        }

        public List<LineaDeVenta> listarLineasDeVentaDeProducto(CapaDominio.Entidades.Producto producto)
        {
            throw new NotImplementedException();
        }
    }
}
