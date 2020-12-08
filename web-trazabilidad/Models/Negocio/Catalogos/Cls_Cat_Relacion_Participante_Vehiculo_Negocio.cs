using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Catalogos
{
    public class Cls_Cat_Relacion_Participante_Vehiculo_Negocio
    {
        public int? Relacion_Id { get; set; }
        public int? Participante_Id { get; set; }
        public int? Vehiculo_Id { get; set; }


        public String Participante { get; set; }
        public String Vehiculo { get; set; }
    }
}