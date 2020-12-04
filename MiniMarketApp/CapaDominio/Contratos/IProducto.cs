using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDominio.Entidades;

namespace CapaDominio.Contratos
{
    public interface IProducto
    {
        void crearProducto(Producto producto);
        List<Producto> listarProductos();
        List<Producto> listarProductosAlfabeticamente();
        List<Producto> listarProductosMasVendidos();
        List<Producto> listarProductosDeCategoria(long idCategoria);
        Producto buscar(long idProdcuto);
        List<Producto> buscarPorNombre(String nombre);
        void editar(Producto producto);
        void eliminar(long idProducto);
        void aumentarStock(Producto producto);
        void disminuirStock(Producto producto);

    }
}
