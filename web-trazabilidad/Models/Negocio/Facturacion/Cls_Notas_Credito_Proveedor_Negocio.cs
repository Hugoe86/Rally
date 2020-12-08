using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Facturacion
{
    public class Cls_Notas_Credito_Proveedor_Negocio
    {
        public int No_Nota_Credito { get; set; }
        public int Empresa_ID { get; set; }
        public int Sucursal_ID { get; set; }
        public int No_Factura { get; set; }
        public string No_Nota_Credito_Proveedor { get; set; }
        public double Cantidad { get; set; }
        public string Moneda { get; set; }
        public double Tipo_Cambio { get; set; }
        public string Fecha_Emision { get; set; }
        public string Fecha_Cancelacion { get; set; }
        public string Motivo_Cancelacion { get; set; }
        public string Estatus { get; set; }
        public string Usuario_Creo { get; set; }
        public Nullable<System.DateTime> Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public System.DateTime Fecha_Modifico { get; set; }

        //filtros
        public int Proveedor_ID { get; set; }
    }
}