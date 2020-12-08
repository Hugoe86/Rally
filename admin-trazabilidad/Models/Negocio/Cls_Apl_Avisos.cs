using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Apl_Avisos
    {
        public int Aviso_ID { get; set; }
        public string Mensaje { get; set; }
        public DateTime Fecha_Inicio_Vigencia { get; set; }
        public DateTime Fecha_Fin_Vigencia { get; set; }
    }
}