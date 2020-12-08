using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using web_trazabilidad.Models.Ayudante;

namespace web_trazabilidad.Models.Negocio.Operaciones
{
    public class Cls_Ope_Eventos_Jornadas_Negocio : Cls_Auditoria
    {

        public int? Jornada_Id { get; set; }
        public int? Evento_Id { get; set; }
        public String Clave { get; set; }
        public String Nombre { get; set; }
        public String Estatus { get; set; }
        public String Tipo { get; set; }
        public String Punto_Inicial { get; set; }
        public String Punto_Final { get; set; }
        public DateTime? Fecha_Inicio { get; set; }
        public String Comentarios { get; set; }


        public Boolean Repetido { get; set; }//   variable para el valor para saber si ya se encuentra registrado
        public Boolean Error { get; set; }//   variable para el valor para saber si ya se encuentra registrado
        public String Mensaje_Error { get; set; }//   variable para el valor del Mensaje_Error


        public String Tabla_Layout { get; set; }//   variable para el valor del curp   

    }
}