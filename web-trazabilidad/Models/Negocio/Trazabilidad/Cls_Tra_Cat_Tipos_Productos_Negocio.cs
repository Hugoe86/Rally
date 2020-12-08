using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Tra_Cat_Tipos_Productos_Negocio
    {
        public int Tipo_Producto_ID { set; get; }
        public string Nombre { set; get; }
        public string Usuario_Creo { set; get; }
        public DateTime Fecha_Creo { set; get; }
        public string Usuario_Modifico { set; get; }
        public Nullable<DateTime> Fecha_Modifico { set; get; }
    }
}