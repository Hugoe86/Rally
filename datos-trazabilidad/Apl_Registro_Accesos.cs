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
    
    public partial class Apl_Registro_Accesos
    {
        public int Empresa_ID { get; set; }
        public int Registro_ID { get; set; }
        public int Usuario_ID { get; set; }
        public string Tipo { get; set; }
        public string Usuario_Creo { get; set; }
        public Nullable<System.DateTime> Fecha_Creo { get; set; }
    
        public virtual Apl_Empresas Apl_Empresas { get; set; }
    }
}
