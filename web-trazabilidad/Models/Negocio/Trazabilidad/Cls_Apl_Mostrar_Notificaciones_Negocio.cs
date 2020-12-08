using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Trazabilidad
{
    public class Cls_Apl_Mostrar_Notificaciones_Negocio
    {
        public string Notificacion_ID { get; set; }
        public string Tipo_Notificacion_ID { get; set; }
        public string Tipo_Notificacion { get; set; }
        public string Mensaje { get; set; }
        public string Icono { get; set; }
        public string Url { get; set; }
        public DateTime Fecha_BD { get; set; }
        public string Fecha { get; set; }
        public string Estatus { get; set; }
    }
}