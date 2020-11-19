using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using CapaDominio.Entidades;
using CapaDominio.Servicios;
using CapaDominio.Contratos;
using CapaPersistencia.FabricaDatos;

namespace CapaAplicacion.Servicios
{
    public class Reportes
    {
        public void reporteDiario()
        {
            private IGestorAccesoDatos gestorAccesoDatos;
            private IProducto productoService;
            private ICategoria categoriaService;
            private ILineaDeVenta lineaDeVentaService;
            private IComprobanteDePago comprobanteDePagoService;
            private ProcesarComprobante procesarComprobante;

        public Reportes()
        {
            FabricaAbstracta fabricaAbstracta = FabricaAbstracta.crearInstancia();
            gestorAccesoDatos = fabricaAbstracta.crearGestorAccesoDatos();
            productoService = fabricaAbstracta.crearProductoDao(gestorAccesoDatos);
            categoriaService = fabricaAbstracta.crearCategoriaDao(gestorAccesoDatos);
            lineaDeVentaService = fabricaAbstracta.crearLineaDeVentaDao(gestorAccesoDatos);
            comprobanteDePagoService = fabricaAbstracta.crearComprobanteDePagoDao(gestorAccesoDatos);
        }

        private List<LineaDeVenta> obtenerLineasDeVenta()
        {
            List<ComprobanteDePago> comprobantesDePago = comprobanteDePagoService.listarComprobanteDePagoPorFecha(DateTime.Now.Date);

            List<LineaDeVenta> lineasDeVenta = 





        }

        private List<>

        public void repoteDiario() 
        {
            List<LineaDeVenta> lineasDeVenta = 

            Application exportarExcel = new Application();
            exportarExcel.Application.Workbooks.Add(true);

            List<String> cabecera = new List<String>();
            
            cabecera.Add("Producto");
            cabecera.Add("Cantidad");
            cabecera.Add("Stock Restante");
            cabecera.Add("Precio de Venta");
            cabecera.Add("Precio de Compra");
            cabecera.Add("Ganancia");

            int indiceColumna = 0;

            foreach()
            {

            }
        }
        

        

    }
}
