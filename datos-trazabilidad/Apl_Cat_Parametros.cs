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
    
    public partial class Apl_Cat_Parametros
    {
        public int Parametro_ID { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }
        public Nullable<int> Puerto { get; set; }
        public string Host { get; set; }
        public Nullable<bool> UseDefaultCredentials { get; set; }
        public Nullable<bool> EnableSsl { get; set; }
        public string Url_Jira_Service { get; set; }
        public string Usuario_Jira { get; set; }
        public string Password_Jira { get; set; }
        public string Usuario_Creo { get; set; }
        public System.DateTime Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public Nullable<System.DateTime> Fecha_Modifico { get; set; }
        public string Name_Jira_Project { get; set; }
    }
}
