using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Trazabilidad
{
    public class Cls_Ope_Inventario_Productos_Negocio
    {
        public int No_Inventario { get; set; }
        public int Producto_ID { get; set; }
        public double Existencias { get; set; }
        public double Cantidad { get; set; }
        public DateTime Fecha_Ingreso { get; set; }
        public int? Empaque_ID { get; set; }
        public string No_Lote_Serie { get; set; }
        public int? No_Orden_Produccion { get; set; }
        public DateTime? Fecha_Caducidad { get; set; }
        public DateTime Fecha_Inicio { get; set; }
        public DateTime Fecha_Termino { get; set; }
        public int ControlStock_ID { get; set; }
        public int Tipo_Producto_ID { get; set; }
        public string Producto { get; set; }
        public string Tiene_Fec_Cad { get; set; }
        public string Empaque { get; set; }
        public string Tipo_Stock { get; set; }
        public string Tipo_Producto { get; set; }
        public string Estatus { get; set; }

    }
}