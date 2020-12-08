using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Trazabilidad
{
    public class Cls_Tra_Cat_Punto_Venta_Categorias_Negocio
    {
        public int Categoria_PV_ID { set; get; }
        public string Descripcion { set; get; }
        public int? Categoria_PV_Padre_ID { set; get; }

        //filtros
        public string Categoria_PV_Padre_Nombre { set; get; }
    }
}