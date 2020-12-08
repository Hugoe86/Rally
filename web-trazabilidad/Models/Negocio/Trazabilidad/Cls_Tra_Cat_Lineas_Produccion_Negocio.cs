using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Tra_Cat_Lineas_Produccion_Negocio
    {
        public int Empresa_ID { set; get; }
        public int Linea_ID { get; set; }
        public int Proceso_ID { get; set; }
        public int Estatus_ID { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Observaciones { get; set; }
        public string Estatus { get; set; }
        public string Proceso { get; set; }
        public string Usuario_Creo { set; get; }
        public string Fecha_Creo { set; get; }
        public string Usuario_Modifico { set; get; }
        public string Fecha_Modifico { set; get; }
    }
}