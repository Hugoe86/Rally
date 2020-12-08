using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Facturacion
{
    public class Cls_Ope_Pos_Ventas_Detalles_Negocio
    {
        public int No_Venta_Detalle { set; get; }
        public string No_Venta { set; get; }
        public int? Producto_ID { set; get; }
        public int Empresa_ID { set; get; }
        public int Sucursal_ID { set; get; }
        public int? Cantidad { set; get; }
        public decimal? Precio { get; set; }
        public decimal? Iva { get; set; }
        public decimal? Subtotal { set; get; }
        public decimal? Total { set; get; }
        public double? Descuento { set; get; }
        public string Usuario_Creo { set; get; }
        public DateTime? Fecha_Creo { set; get; }
        public string Usuario_Modifico { set; get; }
        public DateTime? Fecha_Modifico { set; get; }
        public string Nombre_Producto { set; get; }
        public int UnidadAlm_ID { set; get; }
        public string UnidadAlm_Nombre { set; get; }
        public string UnidadAlm_Clave { set; get; }
        public string Codigo { get; set; }
        public string Tasa { get; set; }
        public string Tipo_Factor { get; set; }
        public string Clave_Sat_Producto { get; set; }
        public string Clave_Sat_Unidad { get; set; }
        public string Clave_Sat_Impuesto { get; set; }
        public string Porcentaje_IVA_Formato { get; set; }
    }
}