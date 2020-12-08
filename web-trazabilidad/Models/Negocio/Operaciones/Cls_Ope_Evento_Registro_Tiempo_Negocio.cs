using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using web_trazabilidad.Models.Ayudante;

namespace web_trazabilidad.Models.Negocio.Operaciones
{
    public class Cls_Ope_Evento_Registro_Tiempo_Negocio 
        : Cls_Auditoria
    {
        public int? Registro_Id { get; set; }
        public int? Evento_Id { get; set; }
        public int? Jornada_Id { get; set; }
        public int? Punto_Control_Id { get; set; }
        public int? Vehiculo_Participante_Id { get; set; }
        public TimeSpan? Tiempo_Ideal { get; set; }
        public TimeSpan? Tiempo_Real { get; set; }
        public int? Puntuacion { get; set; }
        public String Observaciones { get; set; }
        public String Motivo_Cambio { get; set; }
        public int? Usuario_Modifica_Id { get; set; }



        public String Punto_Control { get; set; }
        public String str_Tiempo_Ideal { get; set; }
        public String Str_Tiempo_Real { get; set; }
        public String Usuario_Modifica_Registro { get; set; }

        public int? No_Vehiculo { get; set; }
        public string Categoria { get; set; }
        public string Clave_Punto_Control { get; set; }

    }
}