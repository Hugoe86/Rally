using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using web_trazabilidad.Models.Ayudante;

namespace web_cambios_procesos.Models.Negocio.Operacion.Solicitudes
{
    public class Cls_Ope_Anexos_Negocio : Cls_Auditoria
    {
        //  ids
        public int? Anexo_Id { get; set; }//   variable para el id

        //  datos
        public String Ruta { get; set; }//   variable para el valor de la ruta
        public String Nombre { get; set; }//   variable para el valor del nombre
        public String Tipo { get; set; }//   variable para el valor del tipo

        //  se heredan los valores de auditoria

    }
}