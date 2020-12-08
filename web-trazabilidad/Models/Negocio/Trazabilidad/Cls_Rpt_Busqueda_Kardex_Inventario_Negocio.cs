using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Rpt_Busqueda_Kardex_Inventario_Negocio
    {
        public int Producto_ID { get; set; }
        public int No_Inventario { get; set; }
        public string Fecha_Inicio { get; set; }
        public string Fecha_Termino { get; set; }
 
    }
}