using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Ope_Orden_Compra_Negocio
    {
        public string No_Pedido { set; get; }
        public string Proveedor_ID { set; get; }
        public DateTime Fecha_Solicitud { set; get; }
        public decimal Subtotal { set; get; }
        public decimal IEPS { set; get; }
        public decimal Iva { set; get; }
        public decimal Total { set; get; }
        public string Estatus { set; get; }
        public string Observaciones { set; get; }


        public String Elaboro { get; set; }
        public String Proveedor { get; set; }
        public String Exito { get; set; }
        public String Error { get; set; }
        public String Fecha_Inicio { get; set; }
        public String Fecha_Fin { get; set; }
        public String Producto_ID { set; get; }
    }
}