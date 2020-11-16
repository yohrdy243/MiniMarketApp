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
        public ComprobanteDePago buscarComprobanteDePago(long idComprobanteDePago)
        {
            throw new NotImplementedException();
        }

        public List<ComprobanteDePago> buscarComprobanteDePagoPorDni(long dni)
        {
            throw new NotImplementedException();
        }

        public List<ComprobanteDePago> buscarComprobanteDePagoPorNombre(string nombre)
        {
            throw new NotImplementedException();
        }

        public void crearComprobanteDePago(ComprobanteDePago comprobanteDePago)
        {
            throw new NotImplementedException();
        }

        public void editarComprobante(ComprobanteDePago comprobanteDePago)
        {
            throw new NotImplementedException();
        }

        public void eliminarCategoria(long idCategoria)
        {
            throw new NotImplementedException();
        }

        public List<ComprobanteDePago> listarComprobanteDePago()
        {
            throw new NotImplementedException();
        }

        public List<ComprobanteDePago> listarComprobanteDePagoPorFecha(DateTime fecha)
        {
            throw new NotImplementedException();
        }
    }
}
