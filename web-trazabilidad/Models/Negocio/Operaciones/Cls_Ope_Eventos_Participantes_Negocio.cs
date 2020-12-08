using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using web_trazabilidad.Models.Ayudante;

namespace web_trazabilidad.Models.Negocio.Operaciones
{
    public class Cls_Ope_Eventos_Participantes_Negocio : Cls_Auditoria
    {
        public int? No_Vehiculo_Participante_Id { get; set; }
        public int? Vehiculo_Participante_Id { get; set; }
        public int? Participante_Id { get; set; }
        public Boolean Tipo { get; set; }
        public Boolean Revision_Medica { get; set; }
        public String Comentario { get; set; }
    }
}