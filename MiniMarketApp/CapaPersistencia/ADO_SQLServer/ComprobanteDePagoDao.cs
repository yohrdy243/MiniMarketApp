using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDominio.Contratos;
using CapaDominio.Entidades;

namespace CapaPersistencia.ADO_SQLServer
{
    public class ComprobanteDePagoDao : IComprobanteDePago
    {
        private GestorSQL gestorSQL;

        public ComprobanteDePagoDao (IGestorAccesoDatos gestorSQL)
        {
            this.gestorSQL = (GestorSQL)gestorSQL;
        }
        public ComprobanteDePago buscarComprobanteDePago(long idComprobanteDePago)
        {
            String query = "select *from comprobanteDePago"+
                           "where comprobanteDePago.idComprobante = idComprobante; ";
        }

        public List<ComprobanteDePago> buscarComprobanteDePagoPorDni(long dni)
        {
            String query = "select *from comprobanteDePago"+
                           "where comprobanteDePago.dni = dni; ";
        }

        public List<ComprobanteDePago> buscarComprobanteDePagoPorNombre(string nombre)
        {
            String query = "select *from comprobanteDePago"+
                           "where comprobanteDePago.nombre = nombre;";

        }

        public void crearComprobanteDePago(ComprobanteDePago comprobanteDePago)
        {
            String query = "insert into comprobanteDePago(numeroComprobante,nombre,fecha,direccion,dni,igv,precioNeto,precioTotal"+
                           "values(@numeroComprobante, @nombre, @fecha, @direccion, @dni, @igv, @precioNeto, @precioTotal)";
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
                            "where comprobanteDePago.idComprobante = idComprobante; ";

        }

        public void eliminarComprobante(long idCategoria)
        {
            String query = "delete from comprobanteDePago"+
                           "where comprobanteDePago.idComprobante = idComprobante";
        }

        public List<ComprobanteDePago> listarComprobanteDePago()
        {
            String query = "select *from comprobanteDePago";
        }

        public List<ComprobanteDePago> listarComprobanteDePagoPorFecha(DateTime fecha)
        {
            String query = "select *from comprobanteDePago"+
                           "where comprobanteDePago.fecha = fecha";
        }
    }
}
