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
    
    public partial class Cat_Vehiculos_Documentos
    {
        public int Documento_Id { get; set; }
        public int Vehiculo_Id { get; set; }
        public string Nombre_Documento { get; set; }
        public string Nombre { get; set; }
        public string Ruta { get; set; }
        public string Estatus { get; set; }
        public string Usuario_Creo { get; set; }
        public Nullable<System.DateTime> Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public Nullable<System.DateTime> Fecha_Modifico { get; set; }
    
        public virtual Cat_Vehiculos Cat_Vehiculos { get; set; }
    }
}
