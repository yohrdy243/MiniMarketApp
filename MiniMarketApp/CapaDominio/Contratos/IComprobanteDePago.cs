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
        List<ComprobanteDePago> listarComprobanteDePagoPorFechas(DateTime fecha1,DateTime fecha2);
        List<ComprobanteDePago> buscarComprobanteDePagoPorNombre(String nombre);
        List<ComprobanteDePago> buscarComprobanteDePagoPorDni (long dni);
        List<ComprobanteDePago> listarComprobanteDePagoFechaAntiguaActual();
        List<ComprobanteDePago> listarComprobanteDePagoFechaActualAntigua();
        List<ComprobanteDePago> listarComprobanteDePagoOrdenAlphabetico();
        ComprobanteDePago buscarComprobanteDePago(long idComprobanteDePago);
        void editarComprobante(ComprobanteDePago comprobanteDePago);
        void eliminarComprobante(long idComprobante);
        ComprobanteDePago obtenerComprobanteDePagoGuardado();
        
    }
}
