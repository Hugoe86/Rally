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
    
    public partial class Ope_Eventos_Vehiculo_Participante
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ope_Eventos_Vehiculo_Participante()
        {
            this.Ope_Eventos_Registro_Tiempo = new HashSet<Ope_Eventos_Registro_Tiempo>();
            this.Ope_Eventos_Sincronizacion = new HashSet<Ope_Eventos_Sincronizacion>();
        }
    
        public int Vehiculo_Participante_Id { get; set; }
        public int Vehiculo_Id { get; set; }
        public int Evento_Id { get; set; }
        public int Categoria_Id { get; set; }
        public int Categoria_Participante_Id { get; set; }
        public Nullable<int> Numero_Registro { get; set; }
        public Nullable<int> Numero_Participante { get; set; }
        public string Estatus { get; set; }
        public Nullable<bool> Revision_Mecanica { get; set; }
        public string Comentario { get; set; }
        public int Participante_Piloto_Id { get; set; }
        public Nullable<bool> Revision_Medica_Piloto { get; set; }
        public string Comentario_Piloto { get; set; }
        public Nullable<int> Participante_Copiloto_Id { get; set; }
        public Nullable<bool> Revision_Medica_Copiloto { get; set; }
        public string Comentario_Copiloto { get; set; }
        public string Usuario_Creo { get; set; }
        public Nullable<System.DateTime> Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public Nullable<System.DateTime> Fecha_Modifico { get; set; }
    
        public virtual Cat_Participantes Cat_Participantes { get; set; }
        public virtual Cat_Participantes Cat_Participantes1 { get; set; }
        public virtual Cat_Vehiculos Cat_Vehiculos { get; set; }
        public virtual Ope_Eventos Ope_Eventos { get; set; }
        public virtual Ope_Eventos_Categorias Ope_Eventos_Categorias { get; set; }
        public virtual Ope_Eventos_Categorias Ope_Eventos_Categorias1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ope_Eventos_Registro_Tiempo> Ope_Eventos_Registro_Tiempo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ope_Eventos_Sincronizacion> Ope_Eventos_Sincronizacion { get; set; }
    }
}
