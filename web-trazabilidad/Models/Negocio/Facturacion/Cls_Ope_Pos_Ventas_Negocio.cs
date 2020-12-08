using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Facturacion
{
    public class Cls_Ope_Pos_Ventas_Negocio
    {
        public string No_Venta { get; set; }
        public int Empresa_ID { get; set; }
        public int Sucursal_ID { get; set; }
        public Nullable<int> Usuario_Autoriza_ID { get; set; }
        public Nullable<int> Motivo_Descuento_ID { get; set; }
        public Nullable<decimal> Subtotal { get; set; }
        public Nullable<decimal> Impuestos { get; set; }
        public Nullable<decimal> Descuentos { get; set; }
        public decimal Total { get; set; }
        public string Estatus { get; set; }
        public string Motivo_Cancelacion { get; set; }
        public Nullable<System.DateTime> Fecha_Tramite { get; set; }
        public string Persona_Tramita { get; set; }
        public string Empresa { get; set; }
        public Nullable<System.DateTime> Fecha_Inicio_Vigencia { get; set; }
        public Nullable<System.DateTime> Fecha_Fin_Vigencia { get; set; }
        public string Aplican_Dias_Festivos { get; set; }
        public string Usuario_Creo { get; set; }
        public System.DateTime Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public Nullable<System.DateTime> Fecha_Modifico { get; set; }
        public string Lugar_Venta { get; set; }
        public Nullable<System.DateTime> Fecha_Cancelacion { get; set; }
        public string Usuario_Cancelo { get; set; }
        public string Correo_Electronico { get; set; }
        public string Telefono { get; set; }
        public string Motivo_Descuento { get; set; }

        //filtros
        public string Fecha_Inicio { get; set; }
        public string Fecha_Termino { get; set; }
    }
}