using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using web_trazabilidad.Models.Ayudante;

namespace web_trazabilidad.Models.Negocio.Operaciones
{
    public class Cls_Ope_Eventos_Redes_Sociales_Negocio : Cls_Auditoria
    {
        public int? Link_ID { get; set; }
        public int? Evento_Id { get; set; }
        public String Nombre { get; set; }
        public String Link { get; set; }
        public String Estatus { get; set; }
    }
}