using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio.Entidades
{
    public class Categoria
    {
        private long idCategoria;

        public long IdCategoria
        {
            get { return idCategoria; }
            set { idCategoria = value; }
        }

        private String nombreCategoria;
        
        public String NombreCategoria
        {
            get { return nombreCategoria; }
            set { nombreCategoria = value; }
        }


    }
}
