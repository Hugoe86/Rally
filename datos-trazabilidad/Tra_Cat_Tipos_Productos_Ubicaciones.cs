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
    
    public partial class Tra_Cat_Tipos_Productos_Ubicaciones
    {
        public int Tipo_Producto_ID { get; set; }
        public int Ubicacion_ID { get; set; }
        public string Usuario_Creo { get; set; }
        public Nullable<System.DateTime> Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public Nullable<System.DateTime> Fecha_Modifico { get; set; }
    
        public virtual Tra_Cat_Tipos_Productos Tra_Cat_Tipos_Productos { get; set; }
        public virtual Tra_Cat_Ubicaciones Tra_Cat_Ubicaciones { get; set; }
    }
}
