using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Trazabilidad
{
    public class Cls_Rel_Usuario_Proceso_Negocio
    {
        public int Relacion_ID { get; set; }
        public int Usuario_ID { get; set; }
        public int Proceso_ID { get; set; }
        public string Usuario_Creo { get; set; }
        public DateTime? Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public DateTime? Fecha_Modifico { get; set; }
        public string datos { get; set; }
        public bool Select { get; set; }
        public string Nombre { get; set; }
        public string Comentarios { get; set; }
        public string Proceso { get; set; }
    }
}