using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Tra_Cat_Empaques_Negocio
    {
        public int Empresa_ID { set; get; }
        public int Sucursal_ID { get; set; }
        public int Empaque_ID { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public string Observaciones { get; set; }
        public string Usuario_Creo { set; get; }
        public string Fecha_Creo { set; get; }
        public string Usuario_Modifico { set; get; }
        public string Fecha_Modifico { set; get; }
        public string Clave_Sat { get; set; }
        public DateTime? Fecha_Inicio_Vigencia1 { get; set; }
        public DateTime? Fecha_Fin_Vigencia1 { get; set; }
        public string Fecha_Inicio_Vigencia { get; set; }
        public string Fecha_Fin_Vigencia { get; set; }
    }
}