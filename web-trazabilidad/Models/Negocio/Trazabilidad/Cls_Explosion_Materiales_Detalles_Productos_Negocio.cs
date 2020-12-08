using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Explosion_Materiales_Detalles_Productos_Negocio
    {
        public int No_Explosion_Material_Detalle { get; set; }
        public int No_Explosion_Material { get; set; }
        public int Empresa_ID { get; set; }
        public int Producto_ID { get; set; }
        public int Empaque_ID { get; set; }
        public string Producto { get; set; }
        public string Empaque { get; set; }
        public decimal Cantidad { get; set; }
        public string Control_Stock { get; set; }
        public int Capacidad_Contenedor { get; set; }
        public string Tipo { get; set; }
    }
}