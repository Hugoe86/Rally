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
    
    public partial class Cat_Relacion_Participante_Vehiculo
    {
        public int Relacion_Id { get; set; }
        public Nullable<int> Participante_Id { get; set; }
        public Nullable<int> Vehiculo_Id { get; set; }
    
        public virtual Cat_Participantes Cat_Participantes { get; set; }
        public virtual Cat_Vehiculos Cat_Vehiculos { get; set; }
    }
}