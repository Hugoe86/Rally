using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using web_trazabilidad.Models.Ayudante;

namespace web_trazabilidad.Models.Negocio.Operaciones
{
    public class Cls_Ope_Eventos_Actividades_Negocio : Cls_Auditoria
    {
        public int? Actividad_Id { get; set; }
        public int? Evento_Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Estatus { get; set; }
        public DateTime? Fecha_Inicio { get; set; }
        public DateTime? Fecha_Fin { get; set; }
        public string Comentarios { get; set; }
        
    }
}