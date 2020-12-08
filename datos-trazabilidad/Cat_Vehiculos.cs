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
    
    public partial class Cat_Vehiculos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cat_Vehiculos()
        {
            this.Cat_Relacion_Participante_Vehiculo = new HashSet<Cat_Relacion_Participante_Vehiculo>();
            this.Cat_Vehiculos_Documentos = new HashSet<Cat_Vehiculos_Documentos>();
            this.Ope_Eventos_Vehiculo_Participante = new HashSet<Ope_Eventos_Vehiculo_Participante>();
        }
    
        public int Vehiculo_Id { get; set; }
        public string NS { get; set; }
        public Nullable<decimal> Año { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Placas { get; set; }
        public string Color_Hex_Rgb { get; set; }
        public string Color_Fondo_Hex_Rgb { get; set; }
        public string Estatus { get; set; }
        public string Notas { get; set; }
        public string Compañia { get; set; }
        public string Numero_Poliza { get; set; }
        public Nullable<System.DateTime> Vigencia_Inicial { get; set; }
        public Nullable<System.DateTime> Vigencia_Final { get; set; }
        public string Usuario_Creo { get; set; }
        public Nullable<System.DateTime> Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public Nullable<System.DateTime> Fecha_Modifico { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cat_Relacion_Participante_Vehiculo> Cat_Relacion_Participante_Vehiculo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cat_Vehiculos_Documentos> Cat_Vehiculos_Documentos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ope_Eventos_Vehiculo_Participante> Ope_Eventos_Vehiculo_Participante { get; set; }
    }
}