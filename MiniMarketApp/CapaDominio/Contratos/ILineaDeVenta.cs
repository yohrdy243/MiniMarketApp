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
        List<LineaDeVenta> listarLineasDeVentaDeComprobante(long idcomprobanteDePago);
        List<LineaDeVenta> listarLineasDeVentaDeProducto(long idProducto);
        LineaDeVenta buscarLineaDeVenta(long idLineaDeVenta);
        void editarLineaDeVenta(LineaDeVenta lineaDeVenta);
        void eliminarLineaDeVenta(long idLineaDeVenta);
        List<LineaDeVenta> listarLineasDeVentaDelDia();
    }
}
