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
            List<LineaDeVenta> lineasDeVenta = lineaDeVentaService.listarLineasDeVentaDelDia();
            List<LineaDeVenta> lineasDeVentaProcesadas = new List<LineaDeVenta>();

            LineaDeVenta lineaDeVentaTemporal = lineasDeVenta[0];
            
            lineaDeVentaTemporal.Preciototal = 0;
            lineaDeVentaTemporal.Cantidad = 0;

            long i = lineasDeVenta[0].Producto.IdProducto;

            foreach(LineaDeVenta lineaDeVenta in lineasDeVenta)
            {
                if(i == lineaDeVenta.Producto.IdProducto)
                {
                    lineaDeVentaTemporal.Cantidad = lineaDeVentaTemporal.Cantidad + lineaDeVenta.Cantidad;
                    lineaDeVentaTemporal.Preciototal = lineaDeVentaTemporal.Preciototal + lineaDeVenta.Preciototal;
                }
                else
                {
                    lineaDeVentaTemporal.Producto = productoService.buscar(lineaDeVentaTemporal.Producto.IdProducto);
                    lineasDeVentaProcesadas.Add(lineaDeVentaTemporal);        
                    lineaDeVentaTemporal = lineaDeVenta;
                    i = lineaDeVenta.Producto.IdProducto;
                }
            }

            return lineasDeVentaProcesadas;
        }
        public void repoteDiario() 
        {
            List<LineaDeVenta> lineasDeVenta = obtenerLineasDeVenta();

            Application exportarExcel = new Application();

            exportarExcel.Application.Workbooks.Add(true);

            List<String> cabecera = new List<String>();
            
            cabecera.Add("Producto");
            cabecera.Add("Cantidad");
            cabecera.Add("Stock Restante");
            cabecera.Add("Precio de Venta");
            cabecera.Add("Precio de Compra");
            cabecera.Add("Ingreso del Producto");
            cabecera.Add("Ganancia");

            int nColumnas = 7;
            int nFilas = lineasDeVenta.Count;

            int iCabecera = 0;
            int iLineasDeVenta = 0;

            float ingresoTotal = 0;
            float gananciaTotal = 0;
            for (int indiceColumna = 1; indiceColumna <= nColumnas; indiceColumna++ )
            {
                exportarExcel.Cells[1,indiceColumna] = cabecera[iCabecera];
                iCabecera++;
            }

            for(int indiceFila = 1; indiceFila <= nFilas; indiceFila++)
            {
                exportarExcel.Cells[indiceFila + 1, 1] = lineasDeVenta[iLineasDeVenta].Producto.Nombre;
                exportarExcel.Cells[indiceFila + 1, 2] = lineasDeVenta[iLineasDeVenta].Cantidad;
                exportarExcel.Cells[indiceFila + 1, 3] = lineasDeVenta[iLineasDeVenta].Producto.Stock;
                exportarExcel.Cells[indiceFila + 1, 4] = lineasDeVenta[iLineasDeVenta].Producto.PrecioVenta;
                exportarExcel.Cells[indiceFila + 1, 5] = lineasDeVenta[iLineasDeVenta].Producto.PrecioCompra;
                exportarExcel.Cells[indiceFila + 1, 6] = lineasDeVenta[iLineasDeVenta].Producto.PrecioVenta * lineasDeVenta[iLineasDeVenta].Cantidad;
                exportarExcel.Cells[indiceFila + 1, 7] = lineasDeVenta[iLineasDeVenta].Cantidad * (lineasDeVenta[iLineasDeVenta].Producto.PrecioVenta + lineasDeVenta[iLineasDeVenta].Producto.PrecioCompra);
                
                ingresoTotal = ingresoTotal + lineasDeVenta[iLineasDeVenta].Producto.PrecioVenta * lineasDeVenta[iLineasDeVenta].Cantidad;
                gananciaTotal = gananciaTotal + lineasDeVenta[iLineasDeVenta].Cantidad * (lineasDeVenta[iLineasDeVenta].Producto.PrecioVenta + lineasDeVenta[iLineasDeVenta].Producto.PrecioCompra);
                
                iLineasDeVenta++;
            }

            exportarExcel.Cells[nFilas + 1, 6] = ingresoTotal;
            exportarExcel.Cells[nFilas + 1, 7] = gananciaTotal;

            exportarExcel.Visible = true;

        }
        

        

    }
}
