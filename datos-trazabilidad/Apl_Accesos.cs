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
    
    public partial class Apl_Accesos
    {
        public int Rol_ID { get; set; }
        public int Menu_ID { get; set; }
        public int Estatus_ID { get; set; }
        public string Habilitado { get; set; }
        public string Alta { get; set; }
        public string Cambio { get; set; }
        public string Eliminar { get; set; }
        public string Consultar { get; set; }
        public string Usuario_Creo { get; set; }
        public string Ip_Creo { get; set; }
        public string Equipo_Creo { get; set; }
        public Nullable<System.DateTime> Fecha_Creo { get; set; }
    
        public virtual Apl_Menus Apl_Menus { get; set; }
    }
}
