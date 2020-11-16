using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDominio.Contratos;

namespace CapaPersistencia.FabricaDatos
{
    public abstract class FabricaAbstracta
    {
        public static FabricaAbstracta crearInstancia()
        {
            return new FabricaSQLServer();
        }
        public abstract ICategoria crearCategoriaDao(IGestorAccesoDatos gestorAccesoDatos);
        public abstract IComprobanteDePago crearComprobanteDePagoDao(IGestorAccesoDatos gestorAccesoDatos);
        public abstract IGestorAccesoDatos crearGestorAccesoDatos();
        public abstract ILineaDeVenta crearLineaDeVentaDao(IGestorAccesoDatos gestorAccesoDatos);
        public abstract IProducto crearProductoDao(IGestorAccesoDatos gestorAccesoDatos);
        
    }
}
