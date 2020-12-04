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

        public Reportes()
        {
            FabricaAbstracta fabricaAbstracta = FabricaAbstracta.crearInstancia();
            gestorAccesoDatos = fabricaAbstracta.crearGestorAccesoDatos();
            productoService = fabricaAbstracta.crearProductoDao(gestorAccesoDatos);
            categoriaService = fabricaAbstracta.crearCategoriaDao(gestorAccesoDatos);
            lineaDeVentaService = fabricaAbstracta.crearLineaDeVentaDao(gestorAccesoDatos);
            comprobanteDePagoService = fabricaAbstracta.crearComprobanteDePagoDao(gestorAccesoDatos);
        }

        private List<LineaDeVenta> obtenerLineasDeVenta(DateTime fecha)
        {
            gestorAccesoDatos.abrirConexion();
            List<LineaDeVenta> lineasDeVenta = lineaDeVentaService.listarLineasDeVentaDeUnaFecha(fecha);
            gestorAccesoDatos.cerrarConexion();

            gestorAccesoDatos.abrirConexion();
            LineaDeVenta lineaDeVentaTemporal = lineaDeVentaService.obtenerPivote(fecha);
            gestorAccesoDatos.cerrarConexion();

            List<LineaDeVenta> lineasDeVentaProcesadas = new List<LineaDeVenta>();

            if (lineasDeVenta.Count < 1)
            {
                return new List<LineaDeVenta>();
            }

            lineaDeVentaTemporal.Preciototal = 0;
            lineaDeVentaTemporal.Cantidad = 0;
            long i = lineaDeVentaTemporal.Producto.IdProducto;

            foreach (LineaDeVenta aux in lineasDeVenta)
            {

                if (i == aux.Producto.IdProducto)
                {
                    lineaDeVentaTemporal.Preciototal = lineaDeVentaTemporal.Preciototal + aux.Preciototal;
                    lineaDeVentaTemporal.Cantidad = lineaDeVentaTemporal.Cantidad + aux.Cantidad;
                }
                else if (i != aux.Producto.IdProducto)
                {
                    lineaDeVentaTemporal.Producto.IdProducto = i;
                    lineasDeVentaProcesadas.Add(lineaDeVentaTemporal);

                    lineaDeVentaTemporal = aux;
                    i = aux.Producto.IdProducto;
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
        private List<ComprobanteDePago> obtenerComprobantes(DateTime fecha)
        {
            gestorAccesoDatos.abrirConexion();
            List<ComprobanteDePago> comprobantesDePago = comprobanteDePagoService.listarComprobanteDePagoPorFecha(fecha);
            gestorAccesoDatos.cerrarConexion();

            foreach (ComprobanteDePago comprobanteDePago in comprobantesDePago)
            {
                gestorAccesoDatos.abrirConexion();
                comprobanteDePago.LineasDeVenta = lineaDeVentaService.listarLineasDeVentaDeComprobante(comprobanteDePago.IdComprobante);
                

                foreach(LineaDeVenta lineaDeVenta in comprobanteDePago.LineasDeVenta)
                {
                    gestorAccesoDatos.abrirConexion();
                    lineaDeVenta.Producto = productoService.buscar(lineaDeVenta.Producto.IdProducto);
                    gestorAccesoDatos.cerrarConexion();
                }

            }

            return comprobantesDePago;
        }
        public void generarReporte(DateTime fecha)
        {
            List<LineaDeVenta> lineasDeVenta = obtenerLineasDeVenta(fecha);

            Application exportarExcel = new Application();

            exportarExcel.Application.Workbooks.Add(true);

            if (fecha.Equals(DateTime.Now))
            {
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
                    exportarExcel.Cells[1, indiceColumna] = cabecera;
                    indiceColumna++;
                }

                foreach (LineaDeVenta lineaDeVenta in lineasDeVenta)
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

                List<ComprobanteDePago> comprobantesDePago = obtenerComprobantes(fecha);

                foreach (ComprobanteDePago comprobanteDePago in comprobantesDePago)
                {
                    exportarExcel.Application.Worksheets.Add();

                    int nLinea = 1;

                    exportarExcel.Cells[1, 1] = "Nombre";
                    exportarExcel.Cells[1, 2] = comprobanteDePago.Nombre;
                    exportarExcel.Cells[1, 6] = "Dni";
                    exportarExcel.Cells[1, 7] = comprobanteDePago.Dni;
                    exportarExcel.Cells[2, 1] = "Direccion";
                    exportarExcel.Cells[2, 2] = comprobanteDePago.Direccion;
                    exportarExcel.Cells[2, 6] = "Fecha";
                    exportarExcel.Cells[2, 7] = comprobanteDePago.Fecha.ToString("yyyy-M-dd");
                    exportarExcel.Cells[3, 1] = "Correo";
                    exportarExcel.Cells[3, 2] = comprobanteDePago.Correo;

                    exportarExcel.Cells[5, 1] = "N°";
                    exportarExcel.Cells[5, 2] = "Nombre Producto";
                    exportarExcel.Cells[5, 5] = "Cantidad";
                    exportarExcel.Cells[5, 6] = "Precio Unitario";
                    exportarExcel.Cells[5, 7] = "Precio Total";

                    foreach (LineaDeVenta lineaDeVenta in comprobanteDePago.LineasDeVenta)
                    {
                        exportarExcel.Cells[5 + nLinea, 1] = nLinea;
                        exportarExcel.Cells[5 + nLinea, 2] = lineaDeVenta.Producto.Nombre;
                        exportarExcel.Cells[5 + nLinea, 5] = lineaDeVenta.Cantidad;
                        exportarExcel.Cells[5 + nLinea, 6] = lineaDeVenta.PrecioUnitario;
                        exportarExcel.Cells[5 + nLinea, 7] = lineaDeVenta.Preciototal;
                        nLinea++;
                    }

                    exportarExcel.Cells[5 + nLinea + 1, 6] = "Precio Neto";
                    exportarExcel.Cells[5 + nLinea + 2, 6] = "IGV";
                    exportarExcel.Cells[5 + nLinea + 3, 6] = "Precio Total";
                    exportarExcel.Cells[5 + nLinea + 1, 7] = comprobanteDePago.PrecioNeto;
                    exportarExcel.Cells[5 + nLinea + 2, 7] = comprobanteDePago.Igv;
                    exportarExcel.Cells[5 + nLinea + 3, 7] = comprobanteDePago.PrecioTotal;


                }

                exportarExcel.Visible = true;

            }
            else
            {
                List<String> cabeceras = new List<String>();

                cabeceras.Add("Producto");
                cabeceras.Add("Cantidad");
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
                    exportarExcel.Cells[1, indiceColumna] = cabecera;
                    indiceColumna++;
                }

                foreach (LineaDeVenta lineaDeVenta in lineasDeVenta)
                {
                    exportarExcel.Cells[indiceFila, 1] = lineaDeVenta.Producto.Nombre;
                    exportarExcel.Cells[indiceFila, 2] = lineaDeVenta.Cantidad;
                    exportarExcel.Cells[indiceFila, 3] = lineaDeVenta.Producto.PrecioVenta;
                    exportarExcel.Cells[indiceFila, 4] = lineaDeVenta.Producto.PrecioCompra;
                    exportarExcel.Cells[indiceFila, 5] = lineaDeVenta.Producto.PrecioVenta * lineaDeVenta.Cantidad;
                    exportarExcel.Cells[indiceFila, 6] = lineaDeVenta.Cantidad * (lineaDeVenta.Producto.PrecioVenta - lineaDeVenta.Producto.PrecioCompra);

                    ingresoTotal = ingresoTotal + lineaDeVenta.Producto.PrecioVenta * lineaDeVenta.Cantidad;
                    gananciaTotal = gananciaTotal + lineaDeVenta.Cantidad * (lineaDeVenta.Producto.PrecioVenta - lineaDeVenta.Producto.PrecioCompra);

                    indiceFila++;
                }

                exportarExcel.Cells[indiceFila, 5] = ingresoTotal;
                exportarExcel.Cells[indiceFila, 6] = gananciaTotal;

                List<ComprobanteDePago> comprobantesDePago = obtenerComprobantes(fecha);

                foreach (ComprobanteDePago comprobanteDePago in comprobantesDePago)
                {
                    exportarExcel.Application.Worksheets.Add();

                    int nLinea = 1;

                    exportarExcel.Cells[1, 1] = "Nombre";
                    exportarExcel.Cells[1, 2] = comprobanteDePago.Nombre;
                    exportarExcel.Cells[1, 6] = "Dni";
                    exportarExcel.Cells[1, 7] = comprobanteDePago.Dni;
                    exportarExcel.Cells[2, 1] = "Direccion";
                    exportarExcel.Cells[2, 2] = comprobanteDePago.Direccion;
                    exportarExcel.Cells[2, 6] = "Fecha";
                    exportarExcel.Cells[2, 7] = comprobanteDePago.Fecha.ToString("yyyy-M-dd");
                    exportarExcel.Cells[3, 1] = "Correo";
                    exportarExcel.Cells[3, 2] = comprobanteDePago.Correo;

                    exportarExcel.Cells[5, 1] = "N°";
                    exportarExcel.Cells[5, 2] = "Nombre Producto";
                    exportarExcel.Cells[5, 5] = "Cantidad";
                    exportarExcel.Cells[5, 6] = "Precio Unitario";
                    exportarExcel.Cells[5, 7] = "Precio Total";

                    foreach (LineaDeVenta lineaDeVenta in comprobanteDePago.LineasDeVenta)
                    {
                        exportarExcel.Cells[5 + nLinea, 1] = nLinea;
                        exportarExcel.Cells[5 + nLinea, 2] = lineaDeVenta.Producto.Nombre;
                        exportarExcel.Cells[5 + nLinea, 5] = lineaDeVenta.Cantidad;
                        exportarExcel.Cells[5 + nLinea, 6] = lineaDeVenta.PrecioUnitario;
                        exportarExcel.Cells[5 + nLinea, 7] = lineaDeVenta.Preciototal;
                        nLinea++;
                    }

                    exportarExcel.Cells[5 + nLinea + 1, 6] = "Precio Neto";
                    exportarExcel.Cells[5 + nLinea + 2, 6] = "IGV";
                    exportarExcel.Cells[5 + nLinea + 3, 6] = "Precio Total";
                    exportarExcel.Cells[5 + nLinea + 1, 7] = comprobanteDePago.PrecioNeto;
                    exportarExcel.Cells[5 + nLinea + 2, 7] = comprobanteDePago.Igv;
                    exportarExcel.Cells[5 + nLinea + 3, 7] = comprobanteDePago.PrecioTotal;


                }

                exportarExcel.Visible = true;

            }
        }
        private List<LineaDeVenta> obtenerLineasDeVentaEntreFechas(DateTime fecha1,DateTime fecha2)
        {
            gestorAccesoDatos.abrirConexion();
            List<LineaDeVenta> lineasDeVenta = lineaDeVentaService.listarLineasDeVentaEntreFechas(fecha1,fecha2);
            gestorAccesoDatos.cerrarConexion();

            gestorAccesoDatos.abrirConexion();
            LineaDeVenta lineaDeVentaTemporal = lineaDeVentaService.obtenerPivoteEntreFechas(fecha1,fecha2);
            gestorAccesoDatos.cerrarConexion();

            List<LineaDeVenta> lineasDeVentaProcesadas = new List<LineaDeVenta>();

            if (lineasDeVenta.Count < 1)
            {
                return new List<LineaDeVenta>();
            }

            lineaDeVentaTemporal.Preciototal = 0;
            lineaDeVentaTemporal.Cantidad = 0;
            long i = lineaDeVentaTemporal.Producto.IdProducto;

            foreach (LineaDeVenta aux in lineasDeVenta)
            {

                if (i == aux.Producto.IdProducto)
                {
                    lineaDeVentaTemporal.Preciototal = lineaDeVentaTemporal.Preciototal + aux.Preciototal;
                    lineaDeVentaTemporal.Cantidad = lineaDeVentaTemporal.Cantidad + aux.Cantidad;
                }
                else if (i != aux.Producto.IdProducto)
                {
                    lineaDeVentaTemporal.Producto.IdProducto = i;
                    lineasDeVentaProcesadas.Add(lineaDeVentaTemporal);

                    lineaDeVentaTemporal = aux;
                    i = aux.Producto.IdProducto;
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
        private List<ComprobanteDePago> obtenerComprobantesEntreFechas(DateTime fecha1,DateTime fecha2)
        {
            gestorAccesoDatos.abrirConexion();
            List<ComprobanteDePago> comprobantesDePago = comprobanteDePagoService.listarComprobanteDePagoPorFechas(fecha1,fecha2);
            gestorAccesoDatos.cerrarConexion();

            foreach (ComprobanteDePago comprobanteDePago in comprobantesDePago)
            {
                gestorAccesoDatos.abrirConexion();
                comprobanteDePago.LineasDeVenta = lineaDeVentaService.listarLineasDeVentaDeComprobante(comprobanteDePago.IdComprobante);


                foreach (LineaDeVenta lineaDeVenta in comprobanteDePago.LineasDeVenta)
                {
                    gestorAccesoDatos.abrirConexion();
                    lineaDeVenta.Producto = productoService.buscar(lineaDeVenta.Producto.IdProducto);
                    gestorAccesoDatos.cerrarConexion();
                }

            }

            return comprobantesDePago;
        }
        public void generarReporteEntreFechas(DateTime fecha1,DateTime fecha2)
        {
            List<LineaDeVenta> lineasDeVenta = obtenerLineasDeVentaEntreFechas(fecha1,fecha2);

            Application exportarExcel = new Application();

            exportarExcel.Application.Workbooks.Add(true);
            
            List<String> cabeceras = new List<String>();

            cabeceras.Add("Producto");
            cabeceras.Add("Cantidad");
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
                exportarExcel.Cells[1, indiceColumna] = cabecera;
                indiceColumna++;
            }

            foreach (LineaDeVenta lineaDeVenta in lineasDeVenta)
            {
                exportarExcel.Cells[indiceFila, 1] = lineaDeVenta.Producto.Nombre;
                exportarExcel.Cells[indiceFila, 2] = lineaDeVenta.Cantidad;
                exportarExcel.Cells[indiceFila, 3] = lineaDeVenta.Producto.PrecioVenta;
                exportarExcel.Cells[indiceFila, 4] = lineaDeVenta.Producto.PrecioCompra;
                exportarExcel.Cells[indiceFila, 5] = lineaDeVenta.Producto.PrecioVenta * lineaDeVenta.Cantidad;
                exportarExcel.Cells[indiceFila, 6] = lineaDeVenta.Cantidad * (lineaDeVenta.Producto.PrecioVenta - lineaDeVenta.Producto.PrecioCompra);

                ingresoTotal = ingresoTotal + lineaDeVenta.Producto.PrecioVenta * lineaDeVenta.Cantidad;
                gananciaTotal = gananciaTotal + lineaDeVenta.Cantidad * (lineaDeVenta.Producto.PrecioVenta - lineaDeVenta.Producto.PrecioCompra);

                indiceFila++;
            }

            exportarExcel.Cells[indiceFila, 5] = ingresoTotal;
            exportarExcel.Cells[indiceFila, 6] = gananciaTotal;

            List<ComprobanteDePago> comprobantesDePago = obtenerComprobantesEntreFechas(fecha1,fecha2);

            foreach (ComprobanteDePago comprobanteDePago in comprobantesDePago)
            {
                exportarExcel.Application.Worksheets.Add();

                int nLinea = 1;

                exportarExcel.Cells[1, 1] = "Nombre";
                exportarExcel.Cells[1, 2] = comprobanteDePago.Nombre;
                exportarExcel.Cells[1, 6] = "Dni";
                exportarExcel.Cells[1, 7] = comprobanteDePago.Dni;
                exportarExcel.Cells[2, 1] = "Direccion";
                exportarExcel.Cells[2, 2] = comprobanteDePago.Direccion;
                exportarExcel.Cells[2, 6] = "Fecha";
                exportarExcel.Cells[2, 7] = comprobanteDePago.Fecha.ToString("yyyy-M-dd");
                exportarExcel.Cells[3, 1] = "Correo";
                exportarExcel.Cells[3, 2] = comprobanteDePago.Correo;

                exportarExcel.Cells[5, 1] = "N°";
                exportarExcel.Cells[5, 2] = "Nombre Producto";
                exportarExcel.Cells[5, 5] = "Cantidad";
                exportarExcel.Cells[5, 6] = "Precio Unitario";
                exportarExcel.Cells[5, 7] = "Precio Total";

                foreach (LineaDeVenta lineaDeVenta in comprobanteDePago.LineasDeVenta)
                {
                    exportarExcel.Cells[5 + nLinea, 1] = nLinea;
                    exportarExcel.Cells[5 + nLinea, 2] = lineaDeVenta.Producto.Nombre;
                    exportarExcel.Cells[5 + nLinea, 5] = lineaDeVenta.Cantidad;
                    exportarExcel.Cells[5 + nLinea, 6] = lineaDeVenta.PrecioUnitario;
                    exportarExcel.Cells[5 + nLinea, 7] = lineaDeVenta.Preciototal;
                    nLinea++;
                }

                exportarExcel.Cells[5 + nLinea + 1, 6] = "Precio Neto";
                exportarExcel.Cells[5 + nLinea + 2, 6] = "IGV";
                exportarExcel.Cells[5 + nLinea + 3, 6] = "Precio Total";
                exportarExcel.Cells[5 + nLinea + 1, 7] = comprobanteDePago.PrecioNeto;
                exportarExcel.Cells[5 + nLinea + 2, 7] = comprobanteDePago.Igv;
                exportarExcel.Cells[5 + nLinea + 3, 7] = comprobanteDePago.PrecioTotal;


            }

            exportarExcel.Visible = true;

            
        }


    }
}
