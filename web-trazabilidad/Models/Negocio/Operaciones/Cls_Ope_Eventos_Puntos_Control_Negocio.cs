using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Operaciones
{
    public class Cls_Ope_Eventos_Puntos_Control_Negocio
    {
        public int? Punto_Control_Id { get; set; }
        public int? Jornada_Id { get; set; }
        public int? Evento_Id { get; set; }
        public String Clave { get; set; }
        public String Estatus { get; set; }
        public int? Numero { get; set; }
        public DateTime? Fecha { get; set; }
        public String Ubicacion { get; set; }
        public String Renglon { get; set; }
        public String Distancia { get; set; }
        public String Seña { get; set; }
        public TimeSpan? Tiempo_Ideal { get; set; }
        public TimeSpan? Hora_Inicio { get; set; }
        public TimeSpan? Hora_Fin { get; set; }
        public String Str_Tiempo_Ideal { get; set; }
        public string Comentarios { get; set; }
        public string Responsable { get; set; }
        public TimeSpan? Intervalo { get; set; }
        public String Str_Intervalo{ get; set; }
        public String Str_Hora_Inicio { get; set; }
        public String Str_Hora_Fin { get; set; }

        public String Usuario_Cancelo { get; set; }
        public Boolean Sincronizacion { get; set; }

        public string tbl_operadores { get; set; }
        public string tbl_operadores_eliminados { get; set; }
        public string tbl_categoria_tiempoIdeal { get; set; }
        public string tbl_categoria_tiempoIdeal_Eliminar { get; set; }


        public Boolean Repetido { get; set; }//   variable para el valor para saber si ya se encuentra registrado
        public Boolean Error { get; set; }//   variable para el valor para saber si ya se encuentra registrado
        public String Mensaje_Error { get; set; }//   variable para el valor del Mensaje_Error


        public String Tabla_Layout { get; set; }//   variable para el valor del curp   
        public int? Categoria_Id_Layout { get; set; }
        public int? Responsable_Id_Layout { get; set; }

        
    }
}