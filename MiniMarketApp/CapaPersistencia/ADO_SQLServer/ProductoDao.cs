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
            producto.IdProducto = resultadoSql.GetInt64(0);
            producto.Nombre = resultadoSql.GetString(1);
            producto.Stock = resultadoSql.GetInt32(2);
            producto.PrecioVenta = (float)resultadoSql.GetDouble(3);
            producto.PrecioCompra = (float)resultadoSql.GetDouble(4);
  
            Categoria categoria = new Categoria();
            categoria.IdCategoria = resultadoSql.GetInt64(5);

            producto.Categoria = categoria;
            
            return producto;
        }
        public Producto buscar(long idProducto)
        {
            Producto producto = new Producto();

            String query = "select *from producto where producto.idProducto = " + idProducto +" and producto.estado = 1 ;" ;

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

        public List<Producto> buscarPorNombre(string nombre)
        {
            List<Producto> productos = new List<Producto>();
            String query = "select *from producto where producto.nombre like '%" + nombre + "%' and producto.estado = 1;";

            SqlDataReader resultadoSQL = gestorSQL.ejecutarConsulta(query);
            while (resultadoSQL.Read())
            {
                productos.Add(obtenerProducto(resultadoSQL));
            }
            return productos;
        }

        public void crearProducto(Producto producto)
        {
            String query = "insert into producto(nombre,stock,precioCompra,precioVenta,idCategoria_fk,estado) values(@nombre, @stock, @precioCompra, @precioVenta, @idCategoria_fk,1)";

            SqlCommand sqlCommand;

            sqlCommand = gestorSQL.obtenerComandoSQL(query);

            sqlCommand.Parameters.AddWithValue("@nombre",producto.Nombre);
            sqlCommand.Parameters.AddWithValue("@stock", producto.Stock);
            sqlCommand.Parameters.AddWithValue("@precioCompra", producto.PrecioCompra);
            sqlCommand.Parameters.AddWithValue("@precioVenta", producto.PrecioVenta);
            sqlCommand.Parameters.AddWithValue("@idCategoria_fk", producto.Categoria.IdCategoria);

            sqlCommand.ExecuteNonQuery();


        }

        public void editar(Producto producto)
        {
            String query = "update producto set precioCompra = @precioCompra, precioVenta = @precioVenta, nombre = @nombre, stock = @stock, idCategoria_fk = @idCategoria_fk where producto.idProducto = " + producto.IdProducto + ";";

            SqlCommand sqlCommand;

            sqlCommand = gestorSQL.obtenerComandoSQL(query);

            sqlCommand.Parameters.AddWithValue("@nombre", producto.Nombre);
            sqlCommand.Parameters.AddWithValue("@stock", producto.Stock);
            sqlCommand.Parameters.AddWithValue("@precioCompra", producto.PrecioCompra);
            sqlCommand.Parameters.AddWithValue("@precioVenta", producto.PrecioVenta);
            sqlCommand.Parameters.AddWithValue("@idCategoria_fk", producto.Categoria.IdCategoria);

            sqlCommand.ExecuteNonQuery();
        }

        public void eliminar(long idProducto)
        {
            String query = "update producto set estado = 0 where producto.idProducto = " + idProducto;

            SqlCommand sqlCommand;

            sqlCommand = gestorSQL.obtenerComandoSQL(query);
            sqlCommand.ExecuteNonQuery();
        }
        public List<Producto> listarProductosDeCategoria(long idCategoria)
        {
            List<Producto> productos = new List<Producto>();

            String query = "select * from producto where producto.idCategoria_fk = "+ idCategoria+ "and producto.estado = 1;"; 

            SqlDataReader resultadoSQL = gestorSQL.ejecutarConsulta(query);
            while (resultadoSQL.Read())
            {
                productos.Add(obtenerProducto(resultadoSQL));
            }

            return productos;
        }

        public List<Producto> listarProductos()
        {
            List<Producto> productos = new List<Producto>();

            String query = "select producto.* from producto where producto.estado = 1;;";

            SqlDataReader resultadoSQL = gestorSQL.ejecutarConsulta(query);
            while (resultadoSQL.Read())
            {
                productos.Add(obtenerProducto(resultadoSQL));
            }

            return productos;

        }

        public void aumentarStock(int numero, Producto producto)
        {
            String query = "update producto" +
                             "set" +
                                "stock = @stock" +
                            "where producto.idProducto = " + producto.IdProducto;

            SqlCommand sqlCommand;

            sqlCommand = gestorSQL.obtenerComandoSQL(query);

            sqlCommand.Parameters.AddWithValue("@stock",producto.Stock + numero);

        }

        public void disminuirStock(Producto producto)
        {
            String query = "update producto set stock = @stock where producto.idProducto = "+producto.IdProducto ;

            SqlCommand sqlCommand;

            sqlCommand = gestorSQL.obtenerComandoSQL(query);

            sqlCommand.Parameters.AddWithValue("@stock", producto.Stock);
            sqlCommand.ExecuteNonQuery();
        }
    }
    
}
