using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDominio.Entidades;
namespace CapaDominio.Contratos
{
    public interface ICategoria
    {
        void crearCategoria(Categoria categoria);
        List<Categoria> listarCategorias();
        Categoria buscarCategoria(long idCategoria);
        Categoria buscarCategoriaPorNombre(string nombre);
        void editarCategoria(Categoria categoria);
        void eliminarCategoria(long idCategoria);
    }
}
