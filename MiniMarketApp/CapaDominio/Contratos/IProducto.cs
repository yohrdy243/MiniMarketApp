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
        List<Producto> listarProdcutosDeCategoria(Categoria categoria);
        Producto buscar(long idProdcuto);
        Producto buscarPorNombre(String nombre);
        void editar(Producto producto);
        void eliminar(long idProducto);
    }
}
