using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Com_Cat_Causa_Scrap_Negocio
    {
        public int Causa_Scrap_ID { get; set; }
        public int Estatus_ID { get; set; }
        public int Clave { get; set; }
        public string Nombre { get; set; }
        public string Usuario_Creo { get; set; }
        public DateTime Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public Nullable<DateTime> Fecha_Modifico { get; set; }

        
    }
}