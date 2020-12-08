using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace admin_trazabilidad.Models.Negocio
{
    public class Cls_Apl_Menus_Empresa_Negocio
    {
        public string datos { get; set; }
        public int Menu_Empresa_ID { get; set; }
        public int Empresa_ID { get; set; }
        public int Menu_ID { get; set; }
        public string info { get; set; }
        public string Menu_Descripcion { get; set; }      
        public bool chk { get; set; }
        public string Parent_ID { get; set; }
    }
}