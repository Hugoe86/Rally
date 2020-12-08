using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using web_trazabilidad.Models.Ayudante;

namespace web_trazabilidad.Models.Negocio.Operaciones
{
    public class Cls_Ope_Eventos_Pnt_Ctrl_Categ_Tiempos_Negocio : Cls_Auditoria
    {
        public int? Relacion_Id { get; set; }

        public int? Punto_Control_Id { get; set; }
        public int? Categoria_Id { get; set; }
        public String Categoria { get; set; } 
        public TimeSpan? Tiempo_Ideal { get; set; }
        public String Str_Tiempo_Ideal { get; set; }
    }
}