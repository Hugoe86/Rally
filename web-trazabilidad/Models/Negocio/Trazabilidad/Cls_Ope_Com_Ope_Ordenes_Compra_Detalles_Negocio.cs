using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Ope_Com_Ope_Ordenes_Compra_Detalles_Negocio
    {
        public int No_Orden_Compra_Detalle { get; set; }
        public int Empresa_ID { get; set; }
        public int No_Orden_Compra { get; set; }
        public int Producto_ID { get; set; }
        public decimal Cantidad_Ordenada { get; set; }
        public decimal? Cantidad_Recibida { get; set; }
        public decimal Costo { get; set; }
        public string Nombre { get; set; }
        public string Producto { get; set; }
        public decimal Iva { get; set; }
        public decimal Cantidad { set; get; }
        public decimal Total_Cantidad_Productos { set; get; }
        public decimal? Subtotal { set; get; }
        public decimal Precio { set; get; }
        public int? Empaque_ID { get; set; }
        public int Tipo_Impuesto_ID { get; set; }
        public string Tipo_Impuesto { get; set; }
        public int Tasa { get; set; }
        public decimal Impuesto { get; set; }
        public string Codigo { get; set; }
        public decimal? Descuento { get; set; }
        public decimal? Cant_Descuento { get; set; }
        public string Tipo_Descuento { get; set; }
        public Nullable<decimal> Total { get; set; }
        public string Observaciones { get; set; }
        public bool Obtener_Precio_Producto { get; set; }
        public string Empaque { get; set; }
        public string Estatus { get; set; }
        public int? No_Req_Detalle { get; set; }
    }
}