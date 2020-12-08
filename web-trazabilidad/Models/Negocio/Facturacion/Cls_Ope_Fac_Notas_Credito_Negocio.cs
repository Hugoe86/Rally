using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Facturacion
{
    public class Cls_Ope_Fac_Notas_Credito_Negocio
    {
        public int No_Nota_Credito { get; set; }
        public string Serie { get; set; }
        public int? No_Factura { get; set; }
        public int Empresa_ID { get; set; }
        public int Sucursal_ID { get; set; }
        public string Serie_Factura { get; set; }
        public int? Cliente_ID { get; set; }
        public string Razon_Social { get; set; }
        public string RFC { get; set; }
        public string Email { get; set; }
        public string Pais { get; set; }
        public string Estado { get; set; }
        public string Localidad { get; set; }
        public string Colonia { get; set; }
        public string Ciudad { get; set; }
        public string Calle { get; set; }
        public string Numero_Exterior { get; set; }
        public string Numero_Interior { get; set; }
        public string CP { get; set; }
        public string Metodo_Pago { get; set; }
        public string Forma_Pago { get; set; }
        public string Banco_Pago { get; set; }
        public string Fecha_Pago { get; set; }
        public string Fecha_Emision { get; set; }
        public string Tipo_Nota_Credito { get; set; }
        public string Tipo_Moneda { get; set; }
        public double? Tipo_Cambio { get; set; }
        public double? Descuento { get; set; }
        public double? Subtotal { get; set; }
        public double? Subtotal_Cero { get; set; }
        public double? Iva { get; set; }
        public double? Total { get; set; }
        public string Cancelada { get; set; }
        public string Estatus { get; set; }
        public string Usuario_Cancelacion { get; set; }
        public string Fecha_Cancelacion { get; set; }
        public string Motivo_Cancelacion { get; set; }
        public string Comentarios { get; set; }
        public string Certificado { get; set; }
        public string No_Certificado { get; set; }
        public string Timbre_Version { get; set; }
        public string Timbre_XML { get; set; }
        public string Timbre_UUID { get; set; }
        public string Timbre_Fecha_Timbrado { get; set; }
        public string Timbre_Sello_CFD { get; set; }
        public string Timbre_No_Certificado_SAT { get; set; }
        public string Timbre_Sello_SAT { get; set; }
        public string Ruta_Codigo_BD { get; set; }
        public string Usuario_Creo { get; set; }
        public DateTime? Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public string Fecha_Modifico { get; set; }
        public double? Retencion_Cedular { get; set; }
        public double? Retencion_ISR { get; set; }
        public double? Retencion_IVA { get; set; }
        public double? Retencion_Flete { get; set; }

        // //datos complementarios para nota de crédito
        public string Partidas_Nota_Credito { get; set; }
        public string Tipo_Factura { get; set; }
        string Retencion_Cedular_Factura { get; set; }
        string Retencion_ISR_Factura { get; set; }
        string Retencion_IVA_Factura { get; set; }
        string Retencion_Flete_Factura { get; set; }
        public string Clave_Forma_Pago { get; set; }
        public string Tipo_Comprobante { get; set; }
        public string Condiciones_Pago { set; get; }
        public string Clave_Metodo_Pago { get; set; }
        public string Uso_CFDI { get; set; }
        public string Clave_Uso_CFDI { get; set; }
        public String Clave_Regimen_Fiscal_Emisor { get; set; }
        public string Razon_Social_Emisor { get; set; }
        public string RFC_Emisor { get; set; }
        public String UUID_Relacionado { get; set; }
        public String Clave_Tipo_Relacion { get; set; }
        //campos para filtros
        public string Fecha_Inicio_Emision { get; set; }
        public string Fecha_Termino_Emision { get; set; }
        public String Clave_Pais_Receptor { get; set; }
        public String Timbre_RfcProvCertif { get; set; }
        public string Codigo_Postal_Emisor { get; set; }
        public DateTime? Fecha_Creo_XML { get; set; }
        //Valiables de apoyo para el IVA EXENTO. 
        public Boolean IVA_Exento = false;
        //variable fecha para crear el reporte con formato específico
        public string Fecha_Creo_Xml { get; set; }
        public string Tipo_Relacion { get; set; }

        //para el pdf 
        public string Colonia_Emisor { get; set; }
        public string Ciudad_Emisor { get; set; }
        public string Estado_Emisor { get; set; }
        public string Pais_Emisor { get; set; }
        public string Regimen_Fiscal_Emisor { get; set; }
        public string Calle_Emisor { get; set; }
        public string No_Exterior_Emisor { get; set; }
        public string No_Interior_Emisor  { get; set; }
        public string No_Cuenta_Pago { get; set; }
        public int No_Poliza_Factura { get; set; }
    }
}