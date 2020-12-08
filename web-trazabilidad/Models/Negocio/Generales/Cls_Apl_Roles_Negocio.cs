using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Generales
{
    public class Cls_Apl_Roles_Negocio
    {
        public int? Rol_ID { get; set; }
        public String Nombre { get; set; }
        public String Descripcion { get; set; }
        public String Estatus { get; set; }
    }

    public class Cls_Apl_Roles_Accesos_Negocio
    {
        public int? Rol_ID { get; set; }
        public int? Menu_ID { get; set; }
        public String Habilitado { get; set; }
        public String Alta { get; set; }
        public String Cambio { get; set; }
        public String Eliminar { get; set; }
        public String Consultar { get; set; }
        public String Cancelar { get; set; }
        public String Cerrar { get; set; }
        public String Reimprimir { get; set; }
    }
}