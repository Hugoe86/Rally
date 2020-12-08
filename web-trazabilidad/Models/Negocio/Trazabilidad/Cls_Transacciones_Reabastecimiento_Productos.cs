using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Transacciones_Reabastecimiento_Productos
    {
        public int Producto_ID { get; set; }
        public string Producto { get; set; }
        public string No_Lote_Serie { get; set; }
        public double Cantidad { get; set; }
        public int Ubicacion_ID { get; set; }
        public int Almacen_ID { get; set; }
        public int No_Inventario { get; set; }
         
    }
}