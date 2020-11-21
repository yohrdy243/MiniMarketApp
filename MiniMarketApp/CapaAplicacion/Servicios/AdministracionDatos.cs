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
    public class AdministracionDatos
    {
        private IGestorAccesoDatos gestorAccesoDatos;
        private IProducto productoService;
        private ICategoria categoriaService;
        private ILineaDeVenta lineaDeVentaService;
        private IComprobanteDePago comprobanteDePagoService;

        public AdministracionDatos()
        {
            FabricaAbstracta fabricaAbstracta = FabricaAbstracta.crearInstancia();
            gestorAccesoDatos = fabricaAbstracta.crearGestorAccesoDatos();
            productoService = fabricaAbstracta.crearProductoDao(gestorAccesoDatos);
            categoriaService = fabricaAbstracta.crearCategoriaDao(gestorAccesoDatos);
            lineaDeVentaService = fabricaAbstracta.crearLineaDeVentaDao(gestorAccesoDatos);
            comprobanteDePagoService = fabricaAbstracta.crearComprobanteDePagoDao(gestorAccesoDatos);
        }
        public List<Producto> listarProductos()
        {
            gestorAccesoDatos.abrirConexion();
            List<Producto> productos = productoService.listarProductos();
            gestorAccesoDatos.cerrarConexion();
            foreach (Producto producto in productos)
            {
                gestorAccesoDatos.abrirConexion();
                producto.Categoria = categoriaService.buscarCategoria(producto.Categoria.IdCategoria);
                gestorAccesoDatos.cerrarConexion();
            }
            
            return productos;
        }
        public List<Producto> listarProductosPorNombre(String nombre)
        {
            gestorAccesoDatos.abrirConexion();
            List<Producto> productos = productoService.buscarPorNombre(nombre);
            gestorAccesoDatos.cerrarConexion();
            foreach (Producto producto in productos)
            {
                gestorAccesoDatos.abrirConexion();
                producto.Categoria = categoriaService.buscarCategoria(producto.Categoria.IdCategoria);
                gestorAccesoDatos.cerrarConexion();
            }

            return productos;
        }

        public List<Producto> listarProductosDeCategoria(long idCategoria)
        {
            gestorAccesoDatos.abrirConexion();
            List<Producto> productos = productoService.listarProductosDeCategoria(idCategoria);
            gestorAccesoDatos.cerrarConexion();
            foreach (Producto producto in productos)
            {
                gestorAccesoDatos.abrirConexion();
                producto.Categoria = categoriaService.buscarCategoria(producto.Categoria.IdCategoria);
                gestorAccesoDatos.cerrarConexion();
            }

            return productos;
        }

        public Producto buscarProducto(long idProducto)
        {
            gestorAccesoDatos.abrirConexion();
            Producto producto = productoService.buscar(idProducto);
            gestorAccesoDatos.cerrarConexion();

            gestorAccesoDatos.abrirConexion();
            producto.Categoria = categoriaService.buscarCategoria(producto.Categoria.IdCategoria);
            gestorAccesoDatos.cerrarConexion();

            return producto;
        }
        public void guardarProducto(Producto producto)
        {
            gestorAccesoDatos.abrirConexion();
            productoService.crearProducto(producto);
            gestorAccesoDatos.cerrarConexion();
        }
        public void editarProducto(Producto producto)
        {
            gestorAccesoDatos.abrirConexion();
            productoService.editar(producto);
            gestorAccesoDatos.cerrarConexion();
        }

        public void disminuirStock(Producto producto)
        {
            gestorAccesoDatos.abrirConexion();
            productoService.disminuirStock(producto);
            gestorAccesoDatos.cerrarConexion();
        }
        public void eliminarProducto(long IdProducto)
        {
            gestorAccesoDatos.abrirConexion();
            productoService.eliminar(IdProducto);
            gestorAccesoDatos.cerrarConexion();
        }
        public List<Categoria> listarCategoria()
        {
            gestorAccesoDatos.abrirConexion();
            List<Categoria> categorias = categoriaService.listarCategorias();
            gestorAccesoDatos.cerrarConexion();

            return categorias;
        }

        public Categoria CategoriaPorNombre(String nombre)
        {
            gestorAccesoDatos.abrirConexion();
            Categoria categoria = categoriaService.buscarCategoriaPorNombre(nombre);
            gestorAccesoDatos.cerrarConexion();

            return categoria;
        }
        public Categoria buscarCategoria(long idCategoria)
        {
            gestorAccesoDatos.abrirConexion();
            Categoria categoria = categoriaService.buscarCategoria(idCategoria);
            gestorAccesoDatos.cerrarConexion();

            return categoria;
        }
        public void guardarCategoria(Categoria categoria)
        {
            gestorAccesoDatos.abrirConexion();
            categoriaService.crearCategoria(categoria);
            gestorAccesoDatos.cerrarConexion();
        }

        public void editarCategoria(Categoria categoria)
        {
            gestorAccesoDatos.abrirConexion();
            categoriaService.editarCategoria(categoria);
            gestorAccesoDatos.cerrarConexion();
        }
        public void eliminarCategoria(long idCategoria)
        {
            gestorAccesoDatos.abrirConexion();
            categoriaService.eliminarCategoria(idCategoria);
            gestorAccesoDatos.cerrarConexion();
        }

        public List<LineaDeVenta> listarLineasDeVenta()
        {
            gestorAccesoDatos.abrirConexion();
            List<LineaDeVenta> lineasDeVenta = lineaDeVentaService.listarLineasDeVenta();

            foreach (LineaDeVenta lineaDeVenta in lineasDeVenta)
            {
                lineaDeVenta.Producto = buscarProducto(lineaDeVenta.Producto.IdProducto);
                lineaDeVenta.ComprobanteDePago = comprobanteDePagoService.buscarComprobanteDePago(lineaDeVenta.ComprobanteDePago.IdComprobante);
            }
            gestorAccesoDatos.cerrarConexion();

            return lineasDeVenta;
        }

        public LineaDeVenta buscarlineaDeVenta(long idLineaDeVenta)
        {
            gestorAccesoDatos.abrirConexion();
            LineaDeVenta lineaDeVenta = lineaDeVentaService.buscarLineaDeVenta(idLineaDeVenta);
            lineaDeVenta.Producto = buscarProducto(lineaDeVenta.Producto.IdProducto);
            lineaDeVenta.ComprobanteDePago = comprobanteDePagoService.buscarComprobanteDePago(lineaDeVenta.ComprobanteDePago.IdComprobante);
            gestorAccesoDatos.cerrarConexion();

            return lineaDeVenta;
        }

        public void guardarLineaDeVenta(LineaDeVenta lineaDeVenta)
        {
            gestorAccesoDatos.abrirConexion();
            lineaDeVentaService.crearLineaDeVenta(lineaDeVenta);
            gestorAccesoDatos.cerrarConexion();
        }
        public void editarLineaDeVenta(LineaDeVenta lineaDeVenta)
        {
            gestorAccesoDatos.abrirConexion();
            lineaDeVentaService.editarLineaDeVenta(lineaDeVenta);
            gestorAccesoDatos.cerrarConexion();
        }
        public void eliminarLineaDeVenta(long idLineaDeVenta)
        {
            gestorAccesoDatos.abrirConexion();
            lineaDeVentaService.eliminarLineaDeVenta(idLineaDeVenta);
            gestorAccesoDatos.cerrarConexion();
        }

        public List<ComprobanteDePago> listarComprobanteDePago()
        {
            gestorAccesoDatos.abrirConexion();
            List<ComprobanteDePago> comprobantesDePago = comprobanteDePagoService.listarComprobanteDePago();
            foreach (ComprobanteDePago comprobanteDePago in comprobantesDePago)
            {
                comprobanteDePago.LineasDeVenta = lineaDeVentaService.listarLineasDeVentaDeComprobante(comprobanteDePago.IdComprobante);
                foreach (LineaDeVenta lineaDeVenta in comprobanteDePago.LineasDeVenta)
                {
                    lineaDeVenta.Producto = buscarProducto(lineaDeVenta.Producto.IdProducto);
                }
            }
            gestorAccesoDatos.cerrarConexion();

            return comprobantesDePago;
        }

        public ComprobanteDePago buscarComprobanteDePago(long idComprobante)
        {
            gestorAccesoDatos.abrirConexion();
            ComprobanteDePago comprobanteDePago = comprobanteDePagoService.buscarComprobanteDePago(idComprobante);
            gestorAccesoDatos.cerrarConexion();

            gestorAccesoDatos.abrirConexion();
            comprobanteDePago.LineasDeVenta = lineaDeVentaService.listarLineasDeVentaDeComprobante(comprobanteDePago.IdComprobante);
            gestorAccesoDatos.cerrarConexion();

            foreach (LineaDeVenta lineaDeVenta in comprobanteDePago.LineasDeVenta)
            {
                gestorAccesoDatos.abrirConexion();
                lineaDeVenta.Producto = buscarProducto(lineaDeVenta.Producto.IdProducto);
                gestorAccesoDatos.cerrarConexion();
            }
            

            return comprobanteDePago;
        }
        public ComprobanteDePago obtenerComprobanteDePagoGuardado()
        {
            gestorAccesoDatos.abrirConexion();
            ComprobanteDePago comprobanteDePago = comprobanteDePagoService.obtenerComprobanteDePagoGuardado();
            gestorAccesoDatos.cerrarConexion();

            return comprobanteDePago;
        }

        public void guardarComprobanteDePago(ComprobanteDePago comprobanteDePago)
        {
            gestorAccesoDatos.abrirConexion();
            comprobanteDePagoService.crearComprobanteDePago(comprobanteDePago);
            gestorAccesoDatos.cerrarConexion();
        } 

        public void editarComprobanteDePago(ComprobanteDePago comprobanteDePago)
        {
            gestorAccesoDatos.abrirConexion();
            comprobanteDePagoService.editarComprobante(comprobanteDePago);
            gestorAccesoDatos.cerrarConexion();
        }

        public void eliminarComprobanteDePago(long idComprobante)
        {
            gestorAccesoDatos.abrirConexion();
            comprobanteDePagoService.eliminarComprobante(idComprobante);
            gestorAccesoDatos.cerrarConexion();
        }


    }
}
