using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Tra_Cat_Productos_Proveedores_Negocio
    {
        public int Empresa_ID { set; get; }
        public int Proveedor_ID { get; set; }
        public int Producto_ID { get; set; }
        public string Costo { set; get; }
        public string Tiempo_Entrega { set; get; }
        public string Producto { set; get; }
        public string Proveedor { set; get; }
        public int Unidad_Compra_ID { get; set; }
        public string Unidad_Compra { get; set; }
        public bool Obtener_Precio_Producto { get; set; }
    }
}