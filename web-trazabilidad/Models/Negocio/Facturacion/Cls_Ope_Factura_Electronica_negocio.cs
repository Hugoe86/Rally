using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Facturacion
{
    public class Cls_Ope_Factura_Electronica_Negocio
    {
        public int Empresa_ID { set; get; }
        public int Sucursal_ID { set; get; }
        public string Serie_Relacionada { set; get; }
       public string No_Factura_Relacionada { set; get; }
        public int No_Factura { set; get; }
        public string Serie { set; get; }
        public int? Cliente_ID { set; get; }
        public string Razon_Social { set; get; }
        public string RFC { set; get; }
        public string Pais { set; get; }
        public string Estado { set; get; }
        public string Localidad { set; get; }
        public string Colonia { set; get; }
        public string Ciudad { set; get; }
        public string Calle { set; get; }
        public string Numero_Exterior { set; get; }
        public string Numero_Interior { set; get; }
        public string CP { set; get; }
        public int Dias_Credito { set; get; }
        public decimal Porcentaje_Descuento { set; get; }
        public int Orden_Compra { set; get; }
        public string Forma_Pago { set; get; }
        public string No_Cuenta_Pago { set; get; }
        public string Metodo_Pago { set; get; }
        public string Banco_Pago { set; get; }
        public DateTime? Fecha_Emision { set; get; }
        public DateTime? Fecha_Vencimiento { set; get; }
        public string Fecha_Vencimiento_Text { set; get; }
        public string Tipo_Factura { set; get; }
        public string Tipo_Moneda { set; get; }
        public double? Tipo_Cambio { set; get; }
        public double? Subtotal { set; get; }
        public decimal Sub_Total_Cero { set; get; }
        public double? Retencion_Cedular { set; get; }
        public double? Retencion_Isr { set; get; }
        public double? Retencion_Iva { set; get; }
        public double? Retencion_Flete { set; get; }
        public double? Iva { set; get; }
        public double? Descuento { set; get; }
        public double? Total { set; get; }
        public double? Saldo { set; get; }
        public double? Abono { set; get; }
        public string Pagada { set; get; }
        public string Cancelada { set; get; }
        public string Usuario_Cancelacion { set; get; }
        public DateTime? Fecha_Cancelacion { set; get; }
        public string Motivo_Cancelacion { set; get; }
        public string Comentarios { set; get; }
        public string Certificado { set; get; }
        public string No_Certificado { set; get; }
        public DateTime? Fecha_Creo_XML { set; get; }
        public string Timbre_Version { set; get; }
        public string Timbre_UUID { set; get; }
        public string Timbre_Fecha_Timbrado { set; get; }
        public DateTime? D_Timbre_Fecha_Timbrado { set; get; }
        public string Timbre_Sello_CFD { set; get; }
        public string Timbre_No_Certificado_SAT { set; get; }
        public string Timbre_Sello_SAT { set; get; }
        public string Ruta_Codigo_BD { set; get; }
        public string Usuario_Creo { set; get; }
        public DateTime? Fecha_Creo { set; get; }
        public string Usuario_Modifico { set; get; }
        public DateTime? Fecha_Modifico { set; get; }
        public string Condiciones_Pago { set; get; }
        public DateTime? Fecha_Pago { set; get; }
        public string Estatus { set; get; }
        public string Email { set; get; }
        public string Tipo_Comprobante { get; set; }
        public DateTime Fecha_Creo_Xml_Factura { get; set; }
        public string Fecha_Creo_Xml { get; set; }
        public string Datos_Detalles { get; set; }
        public string Cadena_Original { get; set; }
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
        public string Importe { get; set; }
        public int? No_Embarque { get; set; }
        public string No_Venta { get; set; }

        //campos para busqueda
        public string Fecha_Inicio_Emision { get; set; }
        public string Fecha_Termino_Emision { get; set; }
        public string Fecha_Formato_Creo_XML { get; set; }

        //auxiliar para facturacion

        //nuevos campos facturación
        public string Clave_Metodo_Pago { get; set; }
        public string Uso_CFDI { get; set; }
        public string Clave_Uso_CFDI { get; set; }
        public string Clave_Forma_Pago { get; set; }
        public String UUID_Relacionado { get; set; }
        public String Clave_Tipo_Relacion { get; set; }
        public String Clave_Regimen_Fiscal_Emisor { get; set; }
        public String Tipo_Relacion { get; set; }
        public String No_Factura_Relacionado { get; set; }
        public String Serie_Relacionado { get; set; }

        public String Pais_Receptor { get; set; }
        public String Clave_Pais_Receptor { get; set; }
        public String Timbre_RfcProvCertif { get; set; }

        public string Fecha_Emision_ { get; set; }
        public int Cuenta_Venta_ID { get; set; }

        public int No_Poliza { get; set; }
    }
}