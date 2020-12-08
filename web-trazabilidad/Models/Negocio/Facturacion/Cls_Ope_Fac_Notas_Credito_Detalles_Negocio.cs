using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Facturacion
{
    public class Cls_Ope_Fac_Notas_Credito_Detalles_Negocio
    {
        public int No_Nota_Credito_Det { get; set; }
        public int? No_Nota_Credito { get; set; }
        public int? Empresa_ID { get; set; }
        public int? Sucursal_ID { get; set; }
        public string Serie { get; set; }
        public int? No_Factura { get; set; }
        public string Serie_Factura { get; set; }
        public int? Producto_ID { get; set; }
        public double Cantidad { get; set; }
        public string Codigo { get; set; }
        public string Cuenta_Predial { get; set; }
        public string Nombre_Producto { get; set; }
        public string Unidad { get; set; }
        public double Porcentaje_Impuesto { get; set; }
        public double Subtotal { get; set; }
        public double Descuento { get; set; }
        public double Precio { get; set; }
        public double Total_Detalle { get; set; }
        public double Iva { get; set; }
        public string Usuario_Creo { get; set; }
        public string Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public string Fecha_Modifico { get; set; }
        public string Clave_Sat_Producto { get; set; }
        public string Clave_Sat_Unidad { get; set; }
        public string Clave_Sat_Impuesto { get; set; }
        public string Tipo_Factor { get; set; }
        public string Tasa { get; set; }
        public string UnidadAlm_Nombre { set; get; }
    }
}