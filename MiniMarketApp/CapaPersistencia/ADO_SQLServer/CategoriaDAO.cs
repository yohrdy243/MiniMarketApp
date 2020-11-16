using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDominio.Contratos;
using CapaDominio.Entidades;

namespace CapaPersistencia.ADO_SQLServer
{
    public class CategoriaDao : ICategoria
    {
        public Categoria buscarCategoria(long idCategoria)
        {
            throw new NotImplementedException();
        }

        public Categoria buscarCategoriaPorNombre(string nombre)
        {
            throw new NotImplementedException();
        }

        public void crearCategoria(Categoria categoria)
        {
            throw new NotImplementedException();
        }

        public void editarCategoria(Categoria categoria)
        {
            throw new NotImplementedException();
        }

        public void eliminarCategoria(long idCategoria)
        {
            throw new NotImplementedException();
        }

        public List<Categoria> listarCategorias()
        {
            throw new NotImplementedException();
        }
    }
}
