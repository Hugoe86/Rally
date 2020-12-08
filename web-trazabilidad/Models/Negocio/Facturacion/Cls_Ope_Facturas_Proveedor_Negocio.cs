using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Ope_Facturas_Proveedor_Negocio
    {
        public int No_Factura { set; get; }
        public string No_Factura_Proveedor { set; get; }
        public int Empresa_ID { set; get; }
        public int Sucursal_ID { set; get; }
        public int? Proveedor_ID { get; set; }
        public int? Cuenta_Proveedor_ID { get; set; }
        public int? Cuenta_Impuesto_ID { get; set; }
        public int? Cuenta_Compras_ID { get; set; }
        public int? Tipo_Impuesto_ID { get; set; }
        public string Tipo { get; set; }
        public string Fecha { get; set; }
        public string Fecha_Recepcion { get; set; }
        public string Moneda { get; set; }
        public decimal Importe { get; set; }
        public decimal Flete { get; set; }
        public decimal Iva { get; set; }
        public decimal Retencion_IVA { get; set; }
        public decimal Impuesto_Cedular { get; set; }
        public decimal Retencion_Fletes { get; set; }
        public decimal Total { get; set; }
        public decimal Abono { get; set; }
        public decimal Saldo { get; set; }
        public string Pagada { get; set; }
        public string Cancelada { get; set; }
        public string Fecha_Pago { get; set; }
        public int? Forma_Pago { get; set; }
        public string Comentarios { get; set; }
        public decimal Tipo_Cambio { get; set; }
        public int? Contra_Recibo { get; set; }
        public string Usuario_Creo { set; get; }
        public string Fecha_Creo { set; get; }
        public string Usuario_Modifico { set; get; }
        public string Fecha_Modifico { set; get; }
        public decimal Retencion_ISR { set; get; }
        public string Estatus { set; get; }
        public int? Tipo_Movimiento_ID { get; set; }
        public string Ruta_XML { set; get; }
        public string Ruta_PDF { set; get; }
        public string UUID_CFDI { set; get; }
        public string RFC_CFDI { set; get; }
        public bool Factura_Extranjera { set; get; }

        //-----
        public string filtro { set; get; }
    }
}