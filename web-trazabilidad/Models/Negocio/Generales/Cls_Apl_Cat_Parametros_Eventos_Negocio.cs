using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using web_trazabilidad.Models.Ayudante;

namespace web_trazabilidad.Models.Negocio.Generales
{
    public class Cls_Apl_Cat_Parametros_Eventos_Negocio : Cls_Auditoria
    {
        public int? Parametro_ID { get; set; }
        public int? Puntos_Penalizacion { get; set; }
        public string Estatus { get; set; }
}
}