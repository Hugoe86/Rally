using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Tra_Cat_Tipos_Productos_Ubicaciones_Negocio
    {
        public int Tipo_Producto_ID { get; set; }
        public int Ubicacion_ID { get; set; }
        public string Usuario_Creo { get; set; }
        public Nullable<System.DateTime> Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public Nullable<System.DateTime> Fecha_Modifico { get; set; }
        public string Nombre_Tipo_Producto { get; set; }
        public string Nombre_Ubicacion { get; set; }
    }
}