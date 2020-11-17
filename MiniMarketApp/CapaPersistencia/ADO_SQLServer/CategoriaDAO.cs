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
        private GestorSQL gestorSQL;

        public CategoriaDao(IGestorAccesoDatos gestorSQL)
        {
            this.gestorSQL = (GestorSQL)gestorSQL;
        }
        public Categoria buscarCategoria(long idCategoria)
        {
            String query = "select * from categoria"+
                           "where categoria.idCategoria = idCategoria; ";
        }

        public Categoria buscarCategoriaPorNombre(string nombre)
        {
            String query = "select * from categoria"+
                           "where categoria.nombreCategoria = nombreCategoria; ";
        }

        public void crearCategoria(Categoria categoria)
        {
           String query = "insert into categoria(nombreCategoria)"+
                          "values(@nombreCategoria); ";
        }

        public void editarCategoria(Categoria categoria)
        {
           String query = "update categoria"+
                          "set nombreCategoria = @nombreCategoria"+
                          "where categoria.idCategoria = idCategoria; ";
        }

        public void eliminarCategoria(long idCategoria)
        {
            String query = "delete from categoria"+
                           "where categoria.idCategoria = idCategoria";
        }

        public List<Categoria> listarCategorias()
        {
            String query = "select * from categoria;";
        }
    }
}
