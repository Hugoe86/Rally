using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Trazabilidad
{
    public class Cls_Ope_Pos_Paquetes_Productos_Negocio
    {
        public bool Checkbox { get; set; }
        public int No_Paquete_Producto { get; set; }
        public int No_Paquete_Detalle { get; set; }
        public int No_Paquete { get; set; }
        public int Producto_ID { get; set; }
        public Nullable<decimal> Cantidad { get; set; }
        public Nullable<decimal> Cantidad_Receta { get; set; }
        public Nullable<decimal> Costo_Adicional { get; set; }
        public string Detalles { get; set; }

    }
}