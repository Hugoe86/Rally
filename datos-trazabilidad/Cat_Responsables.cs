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
    
    public partial class Cat_Responsables
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cat_Responsables()
        {
            this.Ope_Eventos_Sincronizacion = new HashSet<Ope_Eventos_Sincronizacion>();
            this.Ope_Eventos_Pnt_Ctrl_Operador = new HashSet<Ope_Eventos_Pnt_Ctrl_Operador>();
        }
    
        public int Responsable_Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Estatus { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Direccion { get; set; }
        public string Colonia { get; set; }
        public string CP { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string Usuario_Creo { get; set; }
        public Nullable<System.DateTime> Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public Nullable<System.DateTime> Fecha_Modifico { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ope_Eventos_Sincronizacion> Ope_Eventos_Sincronizacion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ope_Eventos_Pnt_Ctrl_Operador> Ope_Eventos_Pnt_Ctrl_Operador { get; set; }
    }
}