using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using web_trazabilidad.Models.Ayudante;

namespace web_trazabilidad.Models.Negocio.Catalogos
{
    public class Cls_Cat_Responsables_Negocio : Cls_Auditoria
    {
        public int? Responsable_Id { get; set; }
        public String Clave { get; set; }
        public String Nombre { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String Estatus { get; set; }
        public String Direccion { get; set; }
        public String Colonia { get; set; }
        public String CP { get; set; }
        public String Ciudad { get; set; }
        public String Estado { get; set; }
        public String Telefono { get; set; }
        public String Celular { get; set; }

        

    }
}