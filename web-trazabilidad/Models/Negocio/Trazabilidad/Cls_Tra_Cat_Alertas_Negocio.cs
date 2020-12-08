using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Tra_Cat_Alertas_Negocio
    {
        public int Empresa_ID { set; get; }
        public int Alerta_ID { get; set; }
        public int Estatus_ID { get; set; }
        public string Estatus { get; set; }
        public string Descripcion { get; set; }
        public string Usuario_Creo { set; get; }
        public string Fecha_Creo { set; get; }
        public string Usuario_Modifico { set; get; }
        public string Fecha_Modifico { set; get; }
    }
}