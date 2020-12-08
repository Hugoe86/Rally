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
    
    public partial class Apl_Notificaciones
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Apl_Notificaciones()
        {
            this.Apl_Usuarios_Notificaciones = new HashSet<Apl_Usuarios_Notificaciones>();
        }
    
        public int Notificacion_ID { get; set; }
        public int Empresa_ID { get; set; }
        public int Sucursal_ID { get; set; }
        public Nullable<int> Tipo_Notificacion_ID { get; set; }
        public int Usuario_Realiza { get; set; }
        public string Mensaje { get; set; }
        public System.DateTime Fecha_Notificacion { get; set; }
        public string Url_Notificacion { get; set; }
        public string Icono { get; set; }
        public string Usuario_Creo { get; set; }
        public System.DateTime Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public Nullable<System.DateTime> Fecha_Modifico { get; set; }
    
        public virtual Apl_Empresas Apl_Empresas { get; set; }
        public virtual Apl_Sucursales Apl_Sucursales { get; set; }
        public virtual Apl_Usuarios Apl_Usuarios { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Apl_Usuarios_Notificaciones> Apl_Usuarios_Notificaciones { get; set; }
        public virtual Cat_Tipos_Notificaciones Cat_Tipos_Notificaciones { get; set; }
    }
}