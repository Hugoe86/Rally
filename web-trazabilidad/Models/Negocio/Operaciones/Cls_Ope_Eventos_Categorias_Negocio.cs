using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using web_trazabilidad.Models.Ayudante;

namespace web_trazabilidad.Models.Negocio.Operaciones
{
    public class Cls_Ope_Eventos_Categorias_Negocio : Cls_Auditoria
    {
        public int Categoria_Id { get; set; }
        public int? Evento_Id { get; set; }
        public int? Folio_Inicio { get; set; }
        public int? Folio_Fin { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public int Año_Desde { get; set; }
        public int Año_Hasta { get; set; }
        public string Estatus { get; set; }

    }
}