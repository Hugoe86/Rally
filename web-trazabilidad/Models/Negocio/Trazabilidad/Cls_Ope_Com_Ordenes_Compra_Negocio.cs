using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Ope_Com_Ordenes_Compra_Negocio
    {
        public int Empresa_ID { get; set; }
        public int No_Orden_Compra { get; set; }
        public int Tipo_Impuesto_ID { get; set; }
        public int Proveedor_ID { get; set; }
        public int Estatus_ID { get; set; }
        public DateTime Fecha_Orden { set; get; }
        public DateTime Fecha_Recepcion { set; get; }
        public string Proveedor { get; set; }
        public string Estatus { get; set; }
        public string Usuario_Creo { get; set; }
        public int Tasa { get; set; }
        public string Impuesto { get; set; }
        public string Justificacion { get; set; }
        public string Fecha_Inicio { get; set; }
        public string Fecha_Termino { get; set; }
        public string Tipo_Correo { get; set; }
        public string Correo_Enviado { get; set; }
        public string Motivo_Cancelacion { get; set; }
        public DateTime Fecha_Cancelacion { get; set; }
    }
}