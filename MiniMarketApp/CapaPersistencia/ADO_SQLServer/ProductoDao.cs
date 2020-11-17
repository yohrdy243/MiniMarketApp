using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDominio.Contratos;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;
using CapaDominio.Entidades;

namespace CapaPersistencia.ADO_SQLServer
{
    public class ProductoDao : IProducto
    {
        private GestorSQL gestorSQL;

        public ProductoDao(IGestorAccesoDatos gestorSQL)
        {
            this.gestorSQL = (GestorSQL)gestorSQL;
        }

        private Producto obtenerProducto(SqlDataReader resultadoSql)
        {
            Producto producto = new Producto();
            producto.Descripcion = resultadoSql.get
            Categoria categoria = new Categoria();
               
            return null;
        }
        public Producto buscar(long idProducto)
        {
            Producto producto = new Producto();

            String query = "select *from producto"+
                           "where producto.idProducto = " + idProducto ;

            SqlDataReader resultadoSQL = gestorSQL.ejecutarConsulta(query);
            if (resultadoSQL.Read())
            {
                producto = obtenerProducto(resultadoSQL);
            }
            else
            {
                return null;
            }

            return producto;

        }

        public Producto buscarPorNombre(string nombre)
        {
            Producto producto = new Producto();
            String query = "select *from producto"+
                            "where producto.nombre = '" + nombre + "'";

            SqlDataReader resultadoSQL = gestorSQL.ejecutarConsulta(query);
            if (resultadoSQL.Read())
            {
                producto = obtenerProducto(resultadoSQL);
            }
            else
            {
                return null;
            }

            return producto;
        }

        public void crearProducto(Producto producto)
        {
            String query = "insert into producto(nombre,descripcion,stock,precioCompra,precioVenta,idCategoria_fk)" +
                           "values(@nombre, @descripcion, @stock, @precioCompra, @precioVenta, @idCategoria_fk)";

            SqlCommand sqlCommand;

            sqlCommand = gestorSQL.obtenerComandoSQL(query);

            sqlCommand.Parameters.AddWithValue("@nombre",producto.Nombre);
            sqlCommand.Parameters.AddWithValue("@descripcion", producto.Descripcion);
            sqlCommand.Parameters.AddWithValue("@stock", producto.Stock);
            sqlCommand.Parameters.AddWithValue("@precioCompra", producto.PrecioCompra);
            sqlCommand.Parameters.AddWithValue("@precioVenta", producto.PrecioVenta);
            sqlCommand.Parameters.AddWithValue("@idCategoria_fk", producto.Categoria.IdCategoria);

            sqlCommand.ExecuteNonQuery();


        }

        public void editar(Producto producto)
        {
            String query = "update producto"+
                            "set"+
                                "precioCompra = @precioCompra,"+
                                "precioVenta = @precioVenta,"+
                                "nombre = @nombre,"+
                                "descripcion = @descripcion,"+
                                "stock = @stock,"+
                                "idCategoria_fk = @idCategoria_fk"+
                             "where producto.idProducto = idProducto";

            SqlCommand sqlCommand;

            sqlCommand = gestorSQL.obtenerComandoSQL(query);

            sqlCommand.Parameters.AddWithValue("@nombre", producto.Nombre);
            sqlCommand.Parameters.AddWithValue("@descripcion", producto.Descripcion);
            sqlCommand.Parameters.AddWithValue("@stock", producto.Stock);
            sqlCommand.Parameters.AddWithValue("@precioCompra", producto.PrecioCompra);
            sqlCommand.Parameters.AddWithValue("@precioVenta", producto.PrecioVenta);
            sqlCommand.Parameters.AddWithValue("@idCategoria_fk", producto.Categoria.IdCategoria);

            sqlCommand.ExecuteNonQuery();
        }

        public void eliminar(long idProducto)
        {
            String query = "delete from producto"+
                           "where producto.idProducto = idProducto; ";
        }
        public List<Producto> listarProductosDeCategoria(Categoria categoria)
        {
            String query = "select * from producto" +
                          "where producto.idCategoria_fk = idCategoria_fk"; ;
        }

        public List<Producto> listarProductos()
        {
            String query = "select *from producto";
        }

        public void aumentarStock(int numero)
        {
            String query = "update producto"+
                             "set"+
                                "stock = @stock"+
                            "where producto.idProducto = idProducto";
        }

        public void disminuirStock(int numero)
        {
            String query = "update producto" +
                              "set" +
                                 "stock = @stock" +
                             "where producto.idProducto = idProducto"; ;
        }


    }
}
