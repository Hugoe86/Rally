using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Trazabilidad
{
    public class Cls_Rpt_Pedido_Material_Negocio
    {
        public int Producto_ID { get; set; }
        public decimal? Cantidad { get; set; }
        public string Nombre_Producto { get; set; }
        public string Codigo_Producto { get; set; }
        public int Unidad_Almacenamiento_ID { get; set; }
        public string Nombre_Unidad_Almacenamiento { get; set; }
        public int? Almacen_ID { get; set; }
        public string Nombre_Almacen { get; set; }
        public int? Ubicacion_ID { get; set; }
        public string Nombre_Ubicacion { get; set; }
    }
}