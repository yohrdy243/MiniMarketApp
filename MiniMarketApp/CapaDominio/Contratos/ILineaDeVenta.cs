using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDominio.Entidades;

namespace CapaDominio.Contratos
{
    public interface ILineaDeVenta
    {
        void crearLineaDeVenta(LineaDeVenta lineaDeVenta);
        List<LineaDeVenta> listarLineasDeVenta();
        List<LineaDeVenta> listarLineasDeVentaDeComprobante(ComprobanteDePago comprobanteDePago);
        List<LineaDeVenta> listarLineasDeVentaDeProducto(Producto producto);
        LineaDeVenta buscarLineaDeVenta(long idLineaDeVenta);
        void editarLineaDeVenta(LineaDeVenta lineaDeVenta);
        void eliminar(long idLineaDeVenta);
    }
}
