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
    public class LineaDeVentaDao : ILineaDeVenta
    {
        private GestorSQL gestorSQL;

        public LineaDeVentaDao(IGestorAccesoDatos gestorSQL)
        {
            this.gestorSQL = (GestorSQL)gestorSQL;
        }

        private LineaDeVenta obtenerLineaDeVenta(SqlDataReader resultadoSql)
        {
            LineaDeVenta lineaDeVenta = new LineaDeVenta();

            lineaDeVenta.IdLineaDeVenta = resultadoSql.GetInt64(0);
            lineaDeVenta.Cantidad = resultadoSql.GetInt32(1);
            lineaDeVenta.PrecioUnitario = float.Parse(resultadoSql.GetDouble(2).ToString());
            lineaDeVenta.Preciototal = float.Parse(resultadoSql.GetDouble(3).ToString());

            Producto producto = new Producto();
            producto.IdProducto = resultadoSql.GetInt64(4);

            ComprobanteDePago comprobanteDePago = new ComprobanteDePago();
            comprobanteDePago.IdComprobante = resultadoSql.GetInt64(5);

            lineaDeVenta.Producto = producto;
            lineaDeVenta.ComprobanteDePago = comprobanteDePago;

            return lineaDeVenta;
        }
        public LineaDeVenta buscarLineaDeVenta(long idLineaDeVenta)
        {
            LineaDeVenta lineasDeVenta = new LineaDeVenta();

            String query = "select *from lineaDeVenta where lineaDeVenta.idLineaDeVenta = " + idLineaDeVenta;

            SqlDataReader resultadoSQL = gestorSQL.ejecutarConsulta(query);
            if (resultadoSQL.Read())
            {
                lineasDeVenta = obtenerLineaDeVenta(resultadoSQL);
            }
            else
            {
                return null;
            }

            return lineasDeVenta;
        }
        public void crearLineaDeVenta(LineaDeVenta lineaDeVenta)
        {

            String query = "insert into lineaDeVenta(cantidad,precioUnitario,precioTotal,idProducto_fk,idComprobante_fk)"+
                           "values(@cantidad, @precioUnitario, @precioTotal, @idProducto_fk, @idComprobante_fk)";

            SqlCommand sqlCommand;

            sqlCommand = gestorSQL.obtenerComandoSQL(query);

            sqlCommand.Parameters.AddWithValue("@cantidad", lineaDeVenta.Cantidad);
            sqlCommand.Parameters.AddWithValue("@precioUnitario", lineaDeVenta.PrecioUnitario);
            sqlCommand.Parameters.AddWithValue("@precioTotal", lineaDeVenta.Preciototal);
            sqlCommand.Parameters.AddWithValue("@idProducto_fk", lineaDeVenta.Producto.IdProducto);
            sqlCommand.Parameters.AddWithValue("@idComprobante_fk", lineaDeVenta.ComprobanteDePago.IdComprobante);
            
            sqlCommand.ExecuteNonQuery();
        }
        public void editarLineaDeVenta(LineaDeVenta lineaDeVenta)
        {
            String query = "update lineaDeVenta set cantidad = @cantidad precioTotal = @precioTotal precioUnitario = @precioUnitario where lineaDeVenta.idLineaDeVenta = "+ lineaDeVenta.IdLineaDeVenta;

            SqlCommand sqlCommand;

            sqlCommand = gestorSQL.obtenerComandoSQL(query);

            sqlCommand.Parameters.AddWithValue("@cantidad", lineaDeVenta.Cantidad);
            sqlCommand.Parameters.AddWithValue("@precioUnitario", lineaDeVenta.PrecioUnitario);
            sqlCommand.Parameters.AddWithValue("@precioTotal", lineaDeVenta.Preciototal);

            sqlCommand.ExecuteNonQuery();
        }
        public void eliminarLineaDeVenta(long idLineaDeVenta)
        {
            String query = "delete from lineaDeVenta where lineaDeVenta.idLineaDeVenta = "+idLineaDeVenta;

            SqlCommand sqlCommand;

            sqlCommand = gestorSQL.obtenerComandoSQL(query);
            sqlCommand.ExecuteNonQuery();
        }
        public List<LineaDeVenta> listarLineasDeVenta()
        {
            List<LineaDeVenta> lineasDeVenta = new List<LineaDeVenta>();
            
            String query = "select * from lineaDeVenta";

            SqlDataReader resultadoSQL = gestorSQL.ejecutarConsulta(query);
            while(resultadoSQL.Read())
            {
                lineasDeVenta.Add(obtenerLineaDeVenta(resultadoSQL));
            }
   

            return lineasDeVenta;

        }
        public List<LineaDeVenta> listarLineasDeVentaDeComprobante(long idcomprobanteDePago)
        {
            List<LineaDeVenta> lineasDeVenta = new List<LineaDeVenta>();

            String query = "select * from lineaDeVenta where lineaDeVenta.idComprobante_fk = " +idcomprobanteDePago;

            SqlDataReader resultadoSQL = gestorSQL.ejecutarConsulta(query);
            while (resultadoSQL.Read())
            {
                lineasDeVenta.Add(obtenerLineaDeVenta(resultadoSQL));
            }


            return lineasDeVenta;

        }
        public List<LineaDeVenta> listarLineasDeVentaDeProducto(long idProducto)
        {
            List<LineaDeVenta> lineasDeVenta = new List<LineaDeVenta>();

            String query = "select * from lineaDeVenta where lineaDeVenta.idProducto_fk = "+idProducto;

            SqlDataReader resultadoSQL = gestorSQL.ejecutarConsulta(query);
            while (resultadoSQL.Read())
            {
                lineasDeVenta.Add(obtenerLineaDeVenta(resultadoSQL));
            }


            return lineasDeVenta;
        }
        public List<LineaDeVenta> listarLineasDeVentaDeUnaFecha(DateTime fecha)
        {
            List<LineaDeVenta> lineasDeVenta = new List<LineaDeVenta>();

            String query = "select lineaDeVenta.* from lineaDeVenta,comprobanteDePago where lineaDeVenta.idComprobante_fk = comprobanteDePago.idComprobante and comprobanteDePago.fecha = '" + fecha.ToString("yyyy-M-dd") + "' order by  lineaDeVenta.idProducto_fk asc";

            
            SqlDataReader resultadoSQL = gestorSQL.ejecutarConsulta(query);

            while (resultadoSQL.Read())
            {
                lineasDeVenta.Add(obtenerLineaDeVenta(resultadoSQL));
            }

            return lineasDeVenta;
        }
        public LineaDeVenta obtenerPivote(DateTime fecha)
        {
            LineaDeVenta lineaDeVenta = new LineaDeVenta();

            String query = "select top 1 lineaDeVenta.* from lineaDeVenta,comprobanteDePago where lineaDeVenta.idComprobante_fk = comprobanteDePago.idComprobante and comprobanteDePago.fecha = '" + fecha.ToString("yyyy-M-dd") + "' order by  lineaDeVenta.idProducto_fk asc";

            SqlDataReader resultadoSQL = gestorSQL.ejecutarConsulta(query);

            if (resultadoSQL.Read())
            {
                lineaDeVenta = obtenerLineaDeVenta(resultadoSQL);
            }

            return lineaDeVenta;
        }
        public List<LineaDeVenta> listarLineasDeVentaEntreFechas(DateTime fecha1, DateTime fecha2)
        {
            List<LineaDeVenta> lineasDeVenta = new List<LineaDeVenta>();

            String query = "select lineaDeVenta.* from lineaDeVenta,comprobanteDePago where lineaDeVenta.idComprobante_fk = comprobanteDePago.idComprobante and comprobanteDePago.fecha BETWEEN '"+ fecha1.ToString("yyy-M-dd") + "' and '" + fecha2.ToString("yyyy-M-dd") +"'  order by  lineaDeVenta.idProducto_fk asc;";

            SqlDataReader resultadoSQL = gestorSQL.ejecutarConsulta(query);

            while (resultadoSQL.Read())
            {
                lineasDeVenta.Add(obtenerLineaDeVenta(resultadoSQL));
            }

            return lineasDeVenta;
        }
        public LineaDeVenta obtenerPivoteEntreFechas(DateTime fecha1, DateTime fecha2)
        {
            LineaDeVenta lineaDeVenta = new LineaDeVenta();

            String query = "select top 1 lineaDeVenta.* from lineaDeVenta,comprobanteDePago where lineaDeVenta.idComprobante_fk = comprobanteDePago.idComprobante and comprobanteDePago.fecha BETWEEN '" + fecha1.ToString("yyy-M-dd") + "' and '" + fecha2.ToString("yyyy-M-dd") + "'  order by  lineaDeVenta.idProducto_fk asc;";

            SqlDataReader resultadoSQL = gestorSQL.ejecutarConsulta(query);

            if (resultadoSQL.Read())
            {
                lineaDeVenta = obtenerLineaDeVenta(resultadoSQL);
            }

            return lineaDeVenta;
        }
    }
}
