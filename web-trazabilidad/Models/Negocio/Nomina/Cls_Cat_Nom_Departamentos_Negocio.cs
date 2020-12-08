using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Nomina
{
    public class Cls_Cat_Nom_Departamentos_Negocio
    {
        public int Departamento_ID { get; set; }
        public Nullable<int> Empresa_ID { get; set; }
        public Nullable<int> Sucursal_ID { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Comentarios { get; set; }
        public string Estatus { get; set; }

    }
}