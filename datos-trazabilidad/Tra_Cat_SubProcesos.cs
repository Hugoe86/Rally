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
    
    public partial class Tra_Cat_SubProcesos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tra_Cat_SubProcesos()
        {
            this.Tra_Cat_Dispositivos = new HashSet<Tra_Cat_Dispositivos>();
            this.Tra_Cat_Lotes = new HashSet<Tra_Cat_Lotes>();
            this.Tra_Cat_SubProcesos1 = new HashSet<Tra_Cat_SubProcesos>();
        }
    
        public int Empresa_ID { get; set; }
        public int Subproceso_ID { get; set; }
        public int Proceso_ID { get; set; }
        public int Estatus_ID { get; set; }
        public Nullable<int> Subproceso_Padre_ID { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Observaciones { get; set; }
        public string Usuario_Creo { get; set; }
        public System.DateTime Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public Nullable<System.DateTime> Fecha_Modifico { get; set; }
    
        public virtual Apl_Empresas Apl_Empresas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tra_Cat_Dispositivos> Tra_Cat_Dispositivos { get; set; }
        public virtual Tra_Cat_Estatus Tra_Cat_Estatus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tra_Cat_Lotes> Tra_Cat_Lotes { get; set; }
        public virtual Tra_Cat_Procesos Tra_Cat_Procesos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tra_Cat_SubProcesos> Tra_Cat_SubProcesos1 { get; set; }
        public virtual Tra_Cat_SubProcesos Tra_Cat_SubProcesos2 { get; set; }
    }
}
