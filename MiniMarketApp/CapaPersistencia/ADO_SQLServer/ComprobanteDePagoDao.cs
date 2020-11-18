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
    public class ComprobanteDePagoDao : IComprobanteDePago
    {
        private GestorSQL gestorSQL;

        public ComprobanteDePagoDao (IGestorAccesoDatos gestorSQL)
        {
            this.gestorSQL = (GestorSQL)gestorSQL;
        }

        private ComprobanteDePago obtenerComprobanteDePago(SqlDataReader resultadoSQL)
        {
            ComprobanteDePago comprobanteDePago = new ComprobanteDePago();

            comprobanteDePago.IdComprobante = resultadoSQL.GetInt32(0);
            comprobanteDePago.NumeroComprobante = resultadoSQL.GetInt32(1);
            comprobanteDePago.Nombre = resultadoSQL.GetString(2);
            comprobanteDePago.Fecha = resultadoSQL.GetDateTime(3);
            comprobanteDePago.Direccion = resultadoSQL.GetString(4);
            comprobanteDePago.Dni = resultadoSQL.GetInt32(5);
            comprobanteDePago.Igv = resultadoSQL.GetFloat(6);
            comprobanteDePago.PrecioNeto = resultadoSQL.GetFloat(7);
            comprobanteDePago.PrecioTotal = resultadoSQL.GetFloat(8);

            return comprobanteDePago; 
        }
        public ComprobanteDePago buscarComprobanteDePago(long idComprobanteDePago)
        {
            ComprobanteDePago comprobanteDePago = new ComprobanteDePago();

            String query = "select *from comprobanteDePago"+
                           "where comprobanteDePago.idComprobante = " + idComprobanteDePago;

            SqlDataReader resultadoSQL = gestorSQL.ejecutarConsulta(query);
            if (resultadoSQL.Read())
            {
                comprobanteDePago = obtenerComprobanteDePago(resultadoSQL);
            }
            else
            {
                return null;
            }

            return comprobanteDePago;
        }

        public List<ComprobanteDePago> buscarComprobanteDePagoPorDni(long dni)
        {
            List<ComprobanteDePago> comprobantesDePago = new List<ComprobanteDePago>();

            String query = "select *from comprobanteDePago"+
                           "where comprobanteDePago.dni = "+dni;

            SqlDataReader resultadoSQL = gestorSQL.ejecutarConsulta(query);
            while (resultadoSQL.Read())
            {
                comprobantesDePago.Add(obtenerComprobanteDePago(resultadoSQL));
            }
            return comprobantesDePago;
        }

        public List<ComprobanteDePago> buscarComprobanteDePagoPorNombre(string nombre)
        {
            List<ComprobanteDePago> comprobantesDePago = new List<ComprobanteDePago>();

            String query = "select *from comprobanteDePago"+
                           "where comprobanteDePago.nombre LIKE '%" + nombre +"%';";

            SqlDataReader resultadoSQL = gestorSQL.ejecutarConsulta(query);
            while (resultadoSQL.Read())
            {
                comprobantesDePago.Add(obtenerComprobanteDePago(resultadoSQL));
            }
            return comprobantesDePago;
        }

        public void crearComprobanteDePago(ComprobanteDePago comprobanteDePago)
        {
            String query = "insert into comprobanteDePago(numeroComprobante,nombre,fecha,direccion,dni,igv,precioNeto,precioTotal"+
                           "values(@numeroComprobante, @nombre, @fecha, @direccion, @dni, @igv, @precioNeto, @precioTotal)";
            
            SqlCommand sqlCommand;

            sqlCommand = gestorSQL.obtenerComandoSQL(query);

            sqlCommand.Parameters.AddWithValue("@numeroComprobante", comprobanteDePago.NumeroComprobante);
            sqlCommand.Parameters.AddWithValue("@nombre", comprobanteDePago.Nombre);
            sqlCommand.Parameters.AddWithValue("@fecha", comprobanteDePago.Fecha);
            sqlCommand.Parameters.AddWithValue("@direccion", comprobanteDePago.Direccion);
            sqlCommand.Parameters.AddWithValue("@dni", comprobanteDePago.Dni);
            sqlCommand.Parameters.AddWithValue("@igv", comprobanteDePago.Igv);
            sqlCommand.Parameters.AddWithValue("@precioNeto", comprobanteDePago.PrecioNeto);
            sqlCommand.Parameters.AddWithValue("@precioTotal", comprobanteDePago.PrecioTotal);

            sqlCommand.ExecuteNonQuery();
        }

        public void editarComprobante(ComprobanteDePago comprobanteDePago)
        {
           String query = "update comprobanteDePago"+
                             "set"+
                                "nombre = @nombre,"+
                                "fecha = @fecha,"+
                                "direccion = @direccion,"+
                                "dni = @dni,"+
                                "igv = @igv,"+
                                "precioNeto = @precioNeto,"+
                                "precioTotal = @precioTotal"+
                            "where comprobanteDePago.idComprobante = " + comprobanteDePago.IdComprobante;

            SqlCommand sqlCommand;

            sqlCommand = gestorSQL.obtenerComandoSQL(query);

            sqlCommand.Parameters.AddWithValue("@nombre", comprobanteDePago.Nombre);
            sqlCommand.Parameters.AddWithValue("@fecha", comprobanteDePago.Fecha);
            sqlCommand.Parameters.AddWithValue("@direccion", comprobanteDePago.Direccion);
            sqlCommand.Parameters.AddWithValue("@dni", comprobanteDePago.Dni);
            sqlCommand.Parameters.AddWithValue("@igv", comprobanteDePago.Igv);
            sqlCommand.Parameters.AddWithValue("@precioNeto", comprobanteDePago.PrecioNeto);
            sqlCommand.Parameters.AddWithValue("@precioTotal", comprobanteDePago.PrecioTotal);

            sqlCommand.ExecuteNonQuery();
        }

        public void eliminarComprobante(long idComprobante)
        {
            String query = "delete from comprobanteDePago"+
                           "where comprobanteDePago.idComprobante = " + idComprobante;

            SqlCommand sqlCommand;

            sqlCommand = gestorSQL.obtenerComandoSQL(query);
            sqlCommand.ExecuteNonQuery();
        }

        public List<ComprobanteDePago> listarComprobanteDePago()
        {
            List<ComprobanteDePago> comprobantesDePago = new List<ComprobanteDePago>();

            String query = "select *from comprobanteDePago";

            SqlDataReader resultadoSQL = gestorSQL.ejecutarConsulta(query);
            while (resultadoSQL.Read())
            {
                comprobantesDePago.Add(obtenerComprobanteDePago(resultadoSQL));
            }
            return comprobantesDePago;
        }

        public List<ComprobanteDePago> listarComprobanteDePagoPorFecha(DateTime fecha)
        {
            List<ComprobanteDePago> comprobantesDePago = new List<ComprobanteDePago>();

            String query = "select *from comprobanteDePago"+
                           "where comprobanteDePago.fecha = "+ fecha;

            SqlDataReader resultadoSQL = gestorSQL.ejecutarConsulta(query);
            while (resultadoSQL.Read())
            {
                comprobantesDePago.Add(obtenerComprobanteDePago(resultadoSQL));
            }
            return comprobantesDePago;


        }
    }
}
