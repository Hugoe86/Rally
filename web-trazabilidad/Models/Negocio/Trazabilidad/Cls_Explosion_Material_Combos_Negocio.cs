using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Trazabilidad
{
    public class Cls_Explosion_Material_Combos_Negocio
    {
        public int No_Explosion_Combo { get; set; }
        public int Empresa_ID { get; set; }
        public int Sucursal_ID { get; set; }
        public int Producto_ID { get; set; }
        public int? Almacen_Produccion_ID { get; set; }
        public int? Ubicacion_Produccion_ID { get; set; }
        public string Usuario_Creo { get; set; }
        public DateTime? Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public DateTime? Fecha_Modifico { get; set; }
        public string Nombre { get; set; } 
        public string Codigo { get; set; }
        public string Almacen { get; set; }
        public string Ubicacion { get; set; }
        public string Tipo_Producto { get; set; }
        public string Control_Stock { get; set; }
    }
}