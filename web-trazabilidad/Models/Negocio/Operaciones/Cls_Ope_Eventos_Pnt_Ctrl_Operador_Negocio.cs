using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using web_trazabilidad.Models.Ayudante;

namespace web_trazabilidad.Models.Negocio.Operaciones
{
    public class Cls_Ope_Eventos_Pnt_Ctrl_Operador_Negocio : Cls_Auditoria
    {
        public int? Relacion_Operador_Id { get; set; }
        public int? Responsable_Id { get; set; }
        public int? Punto_Control_Id { get; set; }
        public int? Evento_Id { get; set; }
        public String Estatus { get; set; }
        public String Cargo { get; set; }
        public String Responsable { get; set; }
        public TimeSpan? Hora_Llegada { get; set; }
        public TimeSpan? Hora_Salida { get; set; }
        public String Str_Hora_Llegada { get; set; }
        public String Str_Hora_Salida { get; set; }


    }
}