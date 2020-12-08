using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Facturacion
{
    public class Cls_Ope_Fac_Facturas_Detalles_Negocio
    {
        public int No_Factura_Det { get; set; }
        public int No_Factura { get; set; }
        public string Serie { get; set; }
        public int Producto_ID { get; set; }
        public decimal Cantidad { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Unidad { get; set; }
        public decimal Porcentaje_Impuesto { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Precio_Unitario { get; set; }
        public decimal Total { get; set; }
        public decimal Iva { get; set; }
        public string Usuario_Creo { get; set; }
        public DateTime Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public DateTime Fecha_Modifico { get; set; }
        public string No_Factura_String { get; set; }
        public string Factura_Serie { get; set; }
        public string Tasa { get; set; }
        public string Clave_Sat_Producto { get; set; }
        public string Clave_Sat_Unidad { get; set; }
        public string Clave_Sat_Impuesto { get; set; }
        public string Porcentaje_IVA_Formato { get; set; }

        public string Impuesto { get; set; }
        public string Tipo_Factor { get; set; }
        public Decimal Precio { get; set; }
        public string Nombre_Producto { get; set; }
        public string UnidadAlm_Nombre { get; set; }
    }
}