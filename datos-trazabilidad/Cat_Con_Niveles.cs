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
    
    public partial class Cat_Con_Niveles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cat_Con_Niveles()
        {
            this.Cat_Con_Cuentas_Contables = new HashSet<Cat_Con_Cuentas_Contables>();
            this.Ope_Con_Cuentas_Contables_Centro_Costo = new HashSet<Ope_Con_Cuentas_Contables_Centro_Costo>();
        }
    
        public int Nivel_ID { get; set; }
        public Nullable<int> Empresa_ID { get; set; }
        public Nullable<int> Sucursal_ID { get; set; }
        public string Descripcion { get; set; }
        public int Longitud { get; set; }
        public string Comentarios { get; set; }
        public string Usuario_Creo { get; set; }
        public Nullable<System.DateTime> Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public Nullable<System.DateTime> Fecha_Modifico { get; set; }
        public Nullable<int> Index_Inicio { get; set; }
    
        public virtual Apl_Empresas Apl_Empresas { get; set; }
        public virtual Apl_Sucursales Apl_Sucursales { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cat_Con_Cuentas_Contables> Cat_Con_Cuentas_Contables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ope_Con_Cuentas_Contables_Centro_Costo> Ope_Con_Cuentas_Contables_Centro_Costo { get; set; }
    }
}