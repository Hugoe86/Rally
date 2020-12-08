using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Com_Cat_Tipos_Impuestos_Negocio
    {
        public int Tipo_Impuesto_ID { set; get; }
        public string Impuesto { set; get; }
        public string Tasa { set; get; }
        public string Comentarios { set; get; }
        public string Usuario_Creo { set; get; }
        public string Fecha_Creo { set; get; }
        public string Usuario_Modifico { set; get; }
        public string Fecha_Modifico { set; get; }
        public string Clave_Sat { get; set; }
        public string Descripcion { get; set; }
        public string Retencion { get; set; }
        public string Traslado { get; set; }
        public string Local_Federal { get; set; }
        public string Entidad_Aplica { get; set; }

        public int? Cuenta_Contable_ID  { get; set; }
        public string Cuenta_Contable_Nombre { get; set; }
        public int? Tipo_Factor_ID { get; set; }
        public string Tipo_Factor_Nombre { get; set; }

    }
}