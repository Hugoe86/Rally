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
    
    public partial class Sys_Accesos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sys_Accesos()
        {
            this.Apl_Pos_Accesos = new HashSet<Apl_Pos_Accesos>();
        }
    
        public int Acceso_ID { get; set; }
        public string Ventana_ID { get; set; }
        public string Nombre_Acceso { get; set; }
        public string Name_Space_Acceso { get; set; }
        public string Nombre_Formulario { get; set; }
        public string Es_Menu { get; set; }
        public string Acceso_Padre_ID { get; set; }
        public Nullable<int> Secuencia { get; set; }
        public string Ruta_Icono { get; set; }
        public string Estatus { get; set; }
        public string Usuario_Creo { get; set; }
        public Nullable<System.DateTime> Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public Nullable<System.DateTime> Fecha_Modifico { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Apl_Pos_Accesos> Apl_Pos_Accesos { get; set; }
    }
}