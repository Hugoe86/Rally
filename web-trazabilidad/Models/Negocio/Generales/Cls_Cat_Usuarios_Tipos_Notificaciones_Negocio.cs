using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Generales
{
    public class Cls_Cat_Usuarios_Tipos_Notificaciones_Negocio
    {
        public string Tipo_Notificaciones_Usuario_ID { get; set; }
        public string Usuario_ID { get; set; }
        public string Tipo_Notificacion_ID { get; set; }
        public bool Enviar_Correo { get; set; }
        public string Correo { get; set; }
        public string Estatus { get; set; }
        public string Usuario { get; set; }
        public string Tipo_Notificacion { get; set; }
    }
}