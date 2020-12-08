using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Trazabilidad
{
    public class Cls_Cat_Formas_Pago_Negocio
    {
        public int Forma_ID { get; set; }
        public int Empresa_ID { get; set; }
        public int Sucursal_ID { get; set; }
        public string Nombre { get; set; }
        public string Clave_Sat { get; set; }
        public string Usuario_Creo { get; set; }
        public DateTime Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public DateTime? Fecha_Modifico { get; set; }
        public string Bancarizado { get; set; }
        public string Numero_Operacion { get; set; }
        public string RFC_Emisor_Cuenta_Ordenante { get; set; }
        public string Cuenta_Ordenante { get; set; }
        public string Patron_Cuenta_Ordenante { get; set; }
        public string RFC_Emisor_Cuenta_Beneficiario { get; set; }
        public string Cuenta_Benenficiario { get; set; }
        public string Patron_Cuenta_Beneficiaria { get; set; }
        public string Nombre_Banco_Emisor_Cuenta_Ordenante_Caso_Extranjero { get; set; }
        public string Fecha_Inicio_Vigencia { get; set; }
        public string Fecha_Fin_Vigencia { get; set; }
        public DateTime? Fecha_Inicio_Vigencia1 { get; set; }
        public DateTime? Fecha_Fin_Vigencia1 { get; set; }
    }
}