using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDominio.Contratos;
using CapaDominio.Entidades;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;

namespace CapaPersistencia.ADO_SQLServer
{
    public class CategoriaDao : ICategoria
    {
        private GestorSQL gestorSQL;

        public CategoriaDao(IGestorAccesoDatos gestorSQL)
        {
            this.gestorSQL = (GestorSQL)gestorSQL;
        }

        private Categoria obtenerCategoria(SqlDataReader resultadoSQL)
        {
            Categoria categoria = new Categoria();

            categoria.IdCategoria = resultadoSQL.GetInt64(0);
            categoria.NombreCategoria = resultadoSQL.GetString(1);

            return categoria;
        }
        public Categoria buscarCategoria(long idCategoria)
        {
            Categoria categoria = new Categoria();

            String query = "select * from categoria"+
                           "where categoria.idCategoria = "+idCategoria;

            SqlDataReader resultadoSQL = gestorSQL.ejecutarConsulta(query);
            if (resultadoSQL.Read())
            {
                categoria = obtenerCategoria(resultadoSQL);
            }
            else
            {
                return null;
            }

            return categoria;
        }

        List<Categoria> ICategoria.buscarCategoriaPorNombre(string nombre)
        {
            List<Categoria> categorias = new List<Categoria>();

            String query = "select * from categoria" +
                           "where categoria.nombreCategoria LIKE '%" + nombre + "%' ";

            SqlDataReader resultadoSQL = gestorSQL.ejecutarConsulta(query);
            while (resultadoSQL.Read())
            {
                categorias.Add(obtenerCategoria(resultadoSQL));
            }

            return categorias;
        }

        public void crearCategoria(Categoria categoria)
        {
           String query = "insert into categoria(nombreCategoria)"+
                          "values(@nombreCategoria); ";

            SqlCommand sqlCommand;

            sqlCommand = gestorSQL.obtenerComandoSQL(query);

            sqlCommand.Parameters.AddWithValue("@nombreCategoria", categoria.NombreCategoria);
            
            sqlCommand.ExecuteNonQuery();
        }

        public void editarCategoria(Categoria categoria)
        {
           String query = "update categoria"+
                          "set nombreCategoria = @nombreCategoria"+
                          "where categoria.idCategoria = "+ categoria.IdCategoria + ";";

            SqlCommand sqlCommand;

            sqlCommand = gestorSQL.obtenerComandoSQL(query);

            sqlCommand.Parameters.AddWithValue("@nombreCategoria",categoria.NombreCategoria);

        }

        public void eliminarCategoria(long idCategoria)
        {
            String query = "delete from categoria"+
                           "where categoria.idCategoria = " + idCategoria;

            SqlCommand sqlCommand;

            sqlCommand = gestorSQL.obtenerComandoSQL(query);
            sqlCommand.ExecuteNonQuery();
        }

        public List<Categoria> listarCategorias()
        {
            List<Categoria> categorias = new List<Categoria>();

            String query = "select * from categoria;";

            SqlDataReader resultadoSQL = gestorSQL.ejecutarConsulta(query);
            while (resultadoSQL.Read())
            {
                categorias.Add(obtenerCategoria(resultadoSQL));
            }

            return categorias;
        }

        
    }
}
