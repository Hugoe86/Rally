using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Facturacion
{
    public class Cls_Ope_Fac_Movimientos_Negocio
    {
        public int No_Movimiento { get; set; }
        public int? Banco_ID { get; set; }
        public int? Cliente_ID { get; set; }
        public int? Proveedor_ID { get; set; }
        public int? Tipo_Movimiento_ID { get; set; }
        public int Empresa_ID { get; set; }
        public int Sucursal_ID { get; set; }
        public string Tipo { get; set; }
        public string Estatus { get; set; }
        public string Fecha { get; set; }
        public string Referencia { get; set; }
        public string No_Cheque { get; set; }
        public string No_Factura_Cliente { get; set; }
        public string No_Factura_Proveedor { get; set; }
        public string Serie { get; set; }
        public decimal? Empresa { get; set; }
        public string Forma_pago { get; set; }
        public double? Cantidad { get; set; }
        public string Moneda { get; set; }
        public string Tipo_Cambio { get; set; }
        public string Banco { get; set; }
        public string Concepto { get; set; }
        public decimal? Saldo { get; set; }
        public string Cuenta { get; set; }
        public string Beneficiario { get; set; }
        public Nullable<System.DateTime> Fecha_Deposito { get; set; }
        public string Usuario_Creo { get; set; }
        public Nullable<System.DateTime> Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public Nullable<System.DateTime> Fecha_Modifico { get; set; }
        public string Motivo_Cancelacion { get; set; }

        //datos complementarios para pago de facturas
        public string Facturas_Proveedor { get; set; }
        public string Facturas_Electronicas { get; set; }
        public decimal Total { get; set; }
        public decimal Pago { get; set; }
        public string Fecha_Inicio { get; set; }
        public string Fecha_Termino { get; set; }
    }
}