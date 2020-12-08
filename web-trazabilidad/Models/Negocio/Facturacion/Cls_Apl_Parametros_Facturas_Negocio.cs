using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Facturacion
{
    public class Cls_Apl_Parametros_Facturas_Negocio
    {
        public int Parametro_Fac_ID { get; set; }
        public int Empresa_ID { get; set; }
        public string Email_Origen_Correos { get; set; }
        public string Regimen_Fiscal { get; set; }
        public string Clave_Regimen_Fiscal { get; set; }
        public string Version { get; set; }
        public string Codigo_Usuario { get; set; }
        public string Codigo_Usuario_Proveedor { get; set; }
        public string Sucursal_ID { get; set; }
        public string Ambiente_Timbrado { get; set; }
        public string Ruta_Certificado { get; set; }
        public string Ruta_Llave { get; set; }
        public string Ruta_PDF { get; set; }
        public string Ruta_XML { get; set; }
        public int? Aviso_Expira_Vigencia { get; set; }
        public int? Aviso_Expira_Folios { get; set; }
        public string Vigencia_Certificado_Inicio { get; set; }
        public string Vigencia_Certificado_Fin { get; set; }
        public string Password_Llave { get; set; }
        public string Imagen_Factura { get; set; }
        public string Envio_Automatico_Correo { get; set; }
        public int? Quitar_Tiempo_Timbrado { get; set; }
        public string Ruta_Externa_XML { get; set; }
        public string Ruta_Externa_PDF { get; set; }
        public string Imagen_Factura_2 { get; set; }
        public int? Dias_Tolerancia { get; set; }
        public string Imagen_Factura_3 { get; set; }
        public string Imagen_Factura_4 { get; set; }
        public string Permitir_Facturas_Anteriores { get; set; }
        public double? Porcentaje_Retencion_Cedular { get; set; }
        public double? Porcentaje_Retencion_ISR { get; set; }
        public double? Porcentaje_Retencion_IVA { get; set; }
        public double? Porcentaje_Retencion_Flete { get; set; }
        public int? Porcentaje_Efectivo_Fac { get; set; }
        public int Regimen_Fiscal_ID { get; set; }
        public string Usuario_Creo { get; set; }
        public string Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public string Fecha_Modifico { get; set; }
    }
}