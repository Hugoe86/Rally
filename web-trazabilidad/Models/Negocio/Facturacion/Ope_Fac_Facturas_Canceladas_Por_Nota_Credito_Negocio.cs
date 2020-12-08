using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Facturacion
{
    public class Ope_Fac_Facturas_Canceladas_Por_Nota_Credito_Negocio
    {
        public int No_Factura { get; set; }
        public string Serie { get; set; }
        public Nullable<int> Cliente_ID { get; set; }
        public int Empresa_ID { get; set; }
        public int Sucursal_ID { get; set; }
        public string Razon_Social { get; set; }
        public string RFC { get; set; }
        public string Pais { get; set; }
        public string Estado { get; set; }
        public string Localidad { get; set; }
        public string Colonia { get; set; }
        public string Ciudad { get; set; }
        public string Calle { get; set; }
        public string Numero_Exterior { get; set; }
        public string Numero_Interior { get; set; }
        public string CP { get; set; }
        public Nullable<decimal> Dias_Credito { get; set; }
        public Nullable<decimal> Porcentaje_Descuento { get; set; }
        public string Orden_Compra { get; set; }
        public string Forma_Pago { get; set; }
        public string No_Cuenta_Pago { get; set; }
        public string Metodo_Pago { get; set; }
        public string Banco_Pago { get; set; }
        public Nullable<System.DateTime> Fecha_Emision { get; set; }
        public Nullable<System.DateTime> Fecha_Vencimiento { get; set; }
        public string Tipo_Factura { get; set; }
        public string Tipo_Moneda { get; set; }
        public Nullable<decimal> Tipo_Cambio { get; set; }
        public Nullable<decimal> Subtotal { get; set; }
        public Nullable<decimal> Subtotal_Cero { get; set; }
        public Nullable<decimal> Retencion_Cedular { get; set; }
        public Nullable<decimal> Retencion_ISR { get; set; }
        public Nullable<decimal> Retencion_IVA { get; set; }
        public Nullable<decimal> Retencion_Flete { get; set; }
        public Nullable<decimal> IVA { get; set; }
        public Nullable<decimal> Descuento { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<decimal> Saldo { get; set; }
        public Nullable<decimal> Abono { get; set; }
        public string Pagada { get; set; }
        public string Cancelada { get; set; }
        public string Usuario_Cancelacion { get; set; }
        public Nullable<System.DateTime> Fecha_Cancelacion { get; set; }
        public string Motivo_Cancelacion { get; set; }
        public string Comentarios { get; set; }
        public string Certificado { get; set; }
        public string No_Certificado { get; set; }
        public string No_Autorizacion { get; set; }
        public Nullable<short> Anio_Autorizacion { get; set; }
        public string Genero_Informe { get; set; }
        public Nullable<System.DateTime> Fecha_Informe_SAT { get; set; }
        public string Usuario_Informe_SAT { get; set; }
        public Nullable<System.DateTime> Fecha_Creo_XML { get; set; }
        public string Usuario_Autorizo_Regenerar { get; set; }
        public Nullable<System.DateTime> Fecha_Autorizo_Regenerar { get; set; }
        public string Usuario_Autorizo_Informe { get; set; }
        public Nullable<System.DateTime> Fecha_Autorizo_Informe { get; set; }
        public string Timbre_Version { get; set; }
        public string Timbre_UUID { get; set; }
        public Nullable<System.DateTime> Timbre_Fecha_Timbrado { get; set; }
        public string Timbre_Sello_CFD { get; set; }
        public string Timbre_No_Certificado_SAT { get; set; }
        public string Timbre_Sello_SAT { get; set; }
        public string Ruta_Codigo_BD { get; set; }
        public string Usuario_Creo { get; set; }
        public Nullable<System.DateTime> Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public Nullable<System.DateTime> Fecha_Modifico { get; set; }
        public string Condiciones { get; set; }
        public Nullable<System.DateTime> Fecha_Pago { get; set; }
        public string Estatus { get; set; }
        public string Email { get; set; }
        public string Folio_Factura { get; set; }
        public string Timbre_XML { get; set; }
        public string Regimen_Fiscal_Emisor { get; set; }
        public string Razon_Social_Emisor { get; set; }
        public string RFC_Emisor { get; set; }
        public string Pais_Emisor { get; set; }
        public string Estado_Emisor { get; set; }
        public string Colonia_Emisor { get; set; }
        public string Ciudad_Emisor { get; set; }
        public string Calle_Emisor { get; set; }
        public string No_Exterior_Emisor { get; set; }
        public string No_Interior_Emisor { get; set; }
        public string Codigo_Postal_Emisor { get; set; }
        public string Sistema { get; set; }
        public Nullable<decimal> Importe { get; set; }
        public Nullable<int> No_Embarque { get; set; }
        public string No_Venta { get; set; }
        public string UUID_Relacionado { get; set; }
        public string Tipo_Relacion { get; set; }
        public string No_Factura_Relacionada { get; set; }
        public string Uso_CFDI { get; set; }
        public string Timbre_RfcProvCertif { get; set; }
        public string Serie_Relacionada { get; set; }
        public string Clave_Forma_Pago { get; set; }
        public string Tipo_Comprobante { get; set; }
        public string Clave_Metodo_Pago { get; set; }
        public string Tipo { get; set; }
        public string Clave_Uso_Cfdi { get; set; }
        public string Uso_Cfdi { get; set; }
        public string Fecha_Emision_ { get; set; }
        public string Fecha_Creo_Xml { get; set; }
        public string Clave_Tipo_Relacion { get; set; }
    }
}