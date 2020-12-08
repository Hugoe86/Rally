using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using web_trazabilidad.Models.Ayudante;

namespace web_trazabilidad.Models.Negocio.Operaciones
{
    public class Cls_Ope_Eventos_Negocio : Cls_Auditoria
    {
        public int? Evento_Id { get; set; }
        public String Clave { get; set; }
        public String Nombre { get; set; }
        public String Estatus { get; set; }
        public DateTime? Fecha_Inicio { get; set; }
        public DateTime? Fecha_Fin { get; set; }
        public DateTime? Fecha_Salida { get; set; }
        public Decimal? Recorrido { get; set; }
        public String Punto_Salida { get; set; }
        public String Punto_Meta { get; set; }
        public TimeSpan? Hora_Salida { get; set; }
        public TimeSpan? Intervalo_Salida { get; set; }
        public TimeSpan? Intervalo { get; set; }
        public String Comentarios { get; set; }
        public String Str_Hora_Salida { get; set; }
        public String Str_Intervalo_Salida { get; set; }
        public String Str_Intervalo { get; set; }




        public string tbl_redes_sociales { get; set; }
        public string tbl_redes_sociales_eliminados { get; set; }
    }
}