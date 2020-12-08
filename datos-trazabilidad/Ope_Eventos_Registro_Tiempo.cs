//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace datos_trazabilidad
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ope_Eventos_Registro_Tiempo
    {
        public int Registro_Id { get; set; }
        public int Evento_Id { get; set; }
        public int Jornada_Id { get; set; }
        public int Categoria_Id { get; set; }
        public int Punto_Control_Id { get; set; }
        public int Vehiculo_Participante_Id { get; set; }
        public Nullable<System.TimeSpan> Tiempo_Ideal { get; set; }
        public Nullable<System.TimeSpan> Tiempo_Ideal_Zero { get; set; }
        public Nullable<System.TimeSpan> Tiempo_Real { get; set; }
        public Nullable<System.TimeSpan> Tiempo_Real_Zero { get; set; }
        public Nullable<int> Puntuacion { get; set; }
        public Nullable<int> Segundos_Diferencia { get; set; }
        public string Observaciones { get; set; }
        public Nullable<int> Usuario_Modifica_Id { get; set; }
        public string Motivo_Cambio { get; set; }
        public string Usuario_Creo { get; set; }
        public Nullable<System.DateTime> Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public Nullable<System.DateTime> Fecha_Modifico { get; set; }
        public Nullable<System.TimeSpan> Tiempo_Registrado { get; set; }
    
        public virtual Ope_Eventos Ope_Eventos { get; set; }
        public virtual Ope_Eventos_Categorias Ope_Eventos_Categorias { get; set; }
        public virtual Ope_Eventos_Jornadas Ope_Eventos_Jornadas { get; set; }
        public virtual Ope_Eventos_Puntos_Control Ope_Eventos_Puntos_Control { get; set; }
        public virtual Ope_Eventos_Vehiculo_Participante Ope_Eventos_Vehiculo_Participante { get; set; }
    }
}
