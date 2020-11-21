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
            gestorAccesoDatos.abrirConexion();
            List<LineaDeVenta> lineasDeVenta = lineaDeVentaService.listarLineasDeVentaDelDia();
            gestorAccesoDatos.cerrarConexion();
            List<LineaDeVenta> lineasDeVentaProcesadas = new List<LineaDeVenta>();

            LineaDeVenta lineaDeVentaTemporal = lineasDeVenta[0];
            
            lineaDeVentaTemporal.Preciototal = 0;
            lineaDeVentaTemporal.Cantidad = 0;

            long i = lineasDeVenta[0].Producto.IdProducto;

            foreach(LineaDeVenta lineaDeVenta in lineasDeVenta)
            {
                if( i == lineaDeVenta.Producto.IdProducto)
                {
                    lineaDeVentaTemporal.Preciototal = lineaDeVentaTemporal.Preciototal + lineaDeVenta.Preciototal;
                    lineaDeVentaTemporal.Cantidad = lineaDeVentaTemporal.Cantidad + lineaDeVenta.Cantidad;
                }
                else if ( i != lineaDeVenta.Producto.IdProducto )
                {
                    lineaDeVentaTemporal.Producto.IdProducto = i;
                    lineasDeVentaProcesadas.Add(lineaDeVentaTemporal);
                    lineaDeVentaTemporal = lineaDeVenta;
                    i = lineaDeVenta.Producto.IdProducto;
                    

                }
                
            }

            lineasDeVentaProcesadas.Add(lineaDeVentaTemporal);

            foreach (LineaDeVenta lineaDeVenta1 in lineasDeVentaProcesadas)
            { 
                gestorAccesoDatos.abrirConexion();
                lineaDeVenta1.Producto = productoService.buscar(lineaDeVenta1.Producto.IdProducto);
                gestorAccesoDatos.cerrarConexion();
            }
            return lineasDeVentaProcesadas;
        }
        public void repoteDiario() 
        {
            List<LineaDeVenta> lineasDeVenta = obtenerLineasDeVenta();

            Application exportarExcel = new Application();

            exportarExcel.Application.Workbooks.Add(true);

            List<String> cabeceras = new List<String>();
            
            cabeceras.Add("Producto");
            cabeceras.Add("Cantidad");
            cabeceras.Add("Stock Restante");
            cabeceras.Add("Precio de Venta");
            cabeceras.Add("Precio de Compra");
            cabeceras.Add("Ingreso del Producto");
            cabeceras.Add("Ganancia");

            int indiceFila = 2;
            int indiceColumna = 1;

            float ingresoTotal = 0;
            float gananciaTotal = 0;

            foreach (String cabecera in cabeceras)
            {
                exportarExcel.Cells[1,indiceColumna] = cabecera;
                indiceColumna++;
            }

            foreach(LineaDeVenta lineaDeVenta in lineasDeVenta)
            {
                exportarExcel.Cells[indiceFila, 1] = lineaDeVenta.Producto.Nombre;
                exportarExcel.Cells[indiceFila, 2] = lineaDeVenta.Cantidad;
                exportarExcel.Cells[indiceFila, 3] = lineaDeVenta.Producto.Stock;
                exportarExcel.Cells[indiceFila, 4] = lineaDeVenta.Producto.PrecioVenta;
                exportarExcel.Cells[indiceFila, 5] = lineaDeVenta.Producto.PrecioCompra;
                exportarExcel.Cells[indiceFila, 6] = lineaDeVenta.Producto.PrecioVenta * lineaDeVenta.Cantidad;
                exportarExcel.Cells[indiceFila, 7] = lineaDeVenta.Cantidad * (lineaDeVenta.Producto.PrecioVenta - lineaDeVenta.Producto.PrecioCompra);
                
                ingresoTotal = ingresoTotal + lineaDeVenta.Producto.PrecioVenta * lineaDeVenta.Cantidad;
                gananciaTotal = gananciaTotal + lineaDeVenta.Cantidad * (lineaDeVenta.Producto.PrecioVenta - lineaDeVenta.Producto.PrecioCompra);
                
                indiceFila++;
            }

            exportarExcel.Cells[indiceFila, 6] = ingresoTotal;
            exportarExcel.Cells[indiceFila, 7] = gananciaTotal;

            exportarExcel.Visible = true;

        }
        

        

    }
}
