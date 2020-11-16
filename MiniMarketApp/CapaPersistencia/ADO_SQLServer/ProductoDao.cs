using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDominio.Contratos;
using CapaDominio.Entidades;

namespace CapaPersistencia.ADO_SQLServer
{
    public class ProductoDao : IProducto
    {
        public Producto buscar(long idProdcuto)
        {
            throw new NotImplementedException();
        }

        public Producto buscarPorNombre(string nombre)
        {
            throw new NotImplementedException();
        }

        public void crearProducto(Producto producto)
        {
            throw new NotImplementedException();
        }

        public void editar(Producto producto)
        {
            throw new NotImplementedException();
        }

        public void eliminar(long idProducto)
        {
            throw new NotImplementedException();
        }

        public List<Producto> listarProdcutosDeCategoria(Categoria categoria)
        {
            throw new NotImplementedException();
        }

        public List<Producto> listarProductos()
        {
            throw new NotImplementedException();
        }
    }
}
