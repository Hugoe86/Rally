//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace datos_trazabilidad
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tra_Cat_Fases
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tra_Cat_Fases()
        {
            this.Tra_Cat_Procesos = new HashSet<Tra_Cat_Procesos>();
        }
    
        public int Empresa_ID { get; set; }
        public int Fase_ID { get; set; }
        public int Estatus_ID { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Observaciones { get; set; }
        public string Usuario_Creo { get; set; }
        public System.DateTime Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public Nullable<System.DateTime> Fecha_Modifico { get; set; }
    
        public virtual Apl_Empresas Apl_Empresas { get; set; }
        public virtual Tra_Cat_Estatus Tra_Cat_Estatus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tra_Cat_Procesos> Tra_Cat_Procesos { get; set; }
    }
}