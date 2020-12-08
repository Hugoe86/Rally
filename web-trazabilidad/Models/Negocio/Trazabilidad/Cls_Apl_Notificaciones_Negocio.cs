using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Trazabilidad
{
    public class Cls_Apl_Notificaciones_Negocio
    {
        public string Notificacion_ID { get; set; }
        public string Tipo_Notificacion_ID { get; set; }
        public string Usuario_ID { get; set; }
        public string Mensaje { get; set; }
        public string Url_Notificacion { get; set; }
        public string Icono { get; set; }
        public string Correo_Usuario { get; set; }
    }
}