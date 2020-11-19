using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDominio.Entidades;
using CapaDominio.Servicios;
using CapaDominio.Contratos;
using CapaPersistencia.FabricaDatos;

namespace CapaAplicacion.Servicios
{
    public class RegistrarVenta
    {
        private IGestorAccesoDatos gestorAccesoDatos;
        private IProducto productoService;
        private ICategoria categoriaService;
        private ILineaDeVenta lineaDeVentaService;
        private IComprobanteDePago comprobanteDePagoService;
        private ProcesarComprobante procesarComprobante;

        public RegistrarVenta()
        {
            FabricaAbstracta fabricaAbstracta = FabricaAbstracta.crearInstancia();
            gestorAccesoDatos = fabricaAbstracta.crearGestorAccesoDatos();
            productoService = fabricaAbstracta.crearProductoDao(gestorAccesoDatos);
            categoriaService = fabricaAbstracta.crearCategoriaDao(gestorAccesoDatos);
            lineaDeVentaService = fabricaAbstracta.crearLineaDeVentaDao(gestorAccesoDatos);
            comprobanteDePagoService = fabricaAbstracta.crearComprobanteDePagoDao(gestorAccesoDatos);
        }

        public void guardarComprobanteDePago(ComprobanteDePago comprobanteDePago)
        {
            gestorAccesoDatos.iniciarTransaccion();
            comprobanteDePagoService.crearComprobanteDePago(comprobanteDePago);

            foreach(LineaDeVenta lineaDeVenta in comprobanteDePago.LineasDeVenta)
            {
                lineaDeVenta.ComprobanteDePago = comprobanteDePago;
                lineaDeVentaService.crearLineaDeVenta(lineaDeVenta);
            }
            gestorAccesoDatos.terminarTransaccion();
        }

        public List<Producto> listarProductos()
        {
            gestorAccesoDatos.abrirConexion();
            
            List<Producto> productos = productoService.listarProductos();
            
            foreach(Producto producto in productos)
            {
                Categoria categoria = new Categoria();
                categoria = categoriaService.buscarCategoria(producto.Categoria.IdCategoria);
                producto.Categoria = categoria;
            }
            
            gestorAccesoDatos.cerrarConexion();

            return productos;
        }
        public List<Producto> listarProductosDeCategoria(Categoria categoria)
        {
            gestorAccesoDatos.abrirConexion();

            List<Producto> productos = productoService.listarProductosDeCategoria(categoria);

            foreach (Producto producto in productos)
            {
                Categoria categoriaTemporal = new Categoria();
                categoriaTemporal = categoriaService.buscarCategoria(producto.Categoria.IdCategoria);
                producto.Categoria = categoria;
            }
            gestorAccesoDatos.cerrarConexion();
            return productos;
        }

        public ComprobanteDePago procesarComprobanteDePago(ComprobanteDePago comprobanteDePago)
        {
            ComprobanteDePago comprobanteDePagoProcesado = new ComprobanteDePago();
            comprobanteDePagoProcesado.LineasDeVenta = procesarComprobante.procesarLineasDeVenta(comprobanteDePago.LineasDeVenta);
            comprobanteDePago = procesarComprobante.procesarComprobante(comprobanteDePago);

            return comprobanteDePagoProcesado;
        }



    }
}
