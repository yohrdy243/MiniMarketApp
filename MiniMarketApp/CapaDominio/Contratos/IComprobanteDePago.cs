using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDominio.Entidades;

namespace CapaDominio.Contratos
{
    public interface IComprobanteDePago
    {
        void crearComprobanteDePago(ComprobanteDePago comprobanteDePago);
        List<ComprobanteDePago> listarComprobanteDePago();
        List<ComprobanteDePago> listarComprobanteDePagoPorFecha(DateTime fecha);
        List<ComprobanteDePago> buscarComprobanteDePagoPorNombre(String nombre);
        List<ComprobanteDePago> buscarComprobanteDePagoPorDni (long dni);
        ComprobanteDePago buscarComprobanteDePago(long idComprobanteDePago);
        void editarComprobante(ComprobanteDePago comprobanteDePago);
        void eliminarCategoria(long idCategoria);
    }
}
