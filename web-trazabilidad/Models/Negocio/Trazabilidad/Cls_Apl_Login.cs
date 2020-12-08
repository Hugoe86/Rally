using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Apl_Login
    {
        public string Usuario_ID { set; get; }
        public string Usuario { set; get; }
        public string Password { set; get; }
        public string Empresa_ID { set; get; }
        public string Sucursal { set; get; }
        public string Sucursal_ID { set; get; }
        public string Email { set; get; }
        public string Rol_ID { set; get; }
        public string Default_Admin_Empresa{ set; get; }
        public string Tipo { get; set; }
        public string Direccion_Sucursal { get; set; }
        public string Tel_Sucursal { get; set; }
        public string Email_Sucursal { get; set; }
        public string Direccion_Empresa { get; set; }
        public string Tel_Empresa { get; set; }
        public string Email_Empresa { get; set; }
        public string Empresa { get; set; }
        public string Nombre_Carpeta { get; set; }

    }
}
