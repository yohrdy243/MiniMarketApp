using CapaDominio.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaPersistencia.ADO_SQLServer;

namespace CapaPersistencia.FabricaDatos
{
    public class FabricaSQLServer : FabricaAbstracta
    {
        public override ICategoria crearCategoriaDao(IGestorAccesoDatos gestorAccesoDatos)
        {
            return new CategoriaDao(gestorAccesoDatos);
        }

        public override IComprobanteDePago crearComprobanteDePagoDao(IGestorAccesoDatos gestorAccesoDatos)
        {
            return new ComprobanteDePagoDao(gestorAccesoDatos);
        }

        public override IGestorAccesoDatos crearGestorAccesoDatos()
        {
            return new GestorSQL();
        }

        public override ILineaDeVenta crearLineaDeVentaDao(IGestorAccesoDatos gestorAccesoDatos)
        {
            return new LineaDeVentaDao(gestorAccesoDatos);
        }

        public override IProducto crearProductoDao(IGestorAccesoDatos gestorAccesoDatos)
        {
            return new ProductoDao(gestorAccesoDatos);
        }
    }
}
