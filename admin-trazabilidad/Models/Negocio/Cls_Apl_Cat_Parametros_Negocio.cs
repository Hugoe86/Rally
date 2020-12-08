using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace admin_trazabilidad.Models.Negocio
{
    public class Cls_Apl_Cat_Parametros_Negocio
    {
        public int Parametro_ID { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }
        public int? Puerto { get; set; }
        public string Host { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public bool EnableSsl { get; set; }
        public string Url_Jira_Service { get; set; }
        public string Usuario_Jira { get; set; }
        public string Password_Jira { get; set; }
        public string Usuario_Creo { get; set; }
        public string Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public string Fecha_Modifico { get; set; }
        public string Name_Jira_Project { get; set; }
    }
}