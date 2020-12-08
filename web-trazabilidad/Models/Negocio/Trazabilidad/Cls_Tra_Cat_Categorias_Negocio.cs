using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Tra_Cat_Categorias_Negocio
    {
        public int Categoria_ID { set; get; }
        public string Descripcion { set; get; }
        //public int? Categoria_Padre_ID { set; get; }

        //filtros
        public int? No_Explosion_Combo { set; get; }
        //public string Categoria_Padre_Nombre { set; get; }
    }
}