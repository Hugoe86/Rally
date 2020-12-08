using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Tra_Cat_Subprocesos_Negocio
    {
        public int Subproceso_ID { set; get; }

        public int Empresa_ID { set; get; }

        public int Proceso_ID { set; get; }

        public int Estatus_ID { set; get; }

        public Nullable<int> Subproceso_Padre_ID { get; set; }

        public string Clave { set; get; }

        public string Nombre { set; get;}

        public string Observaciones { set; get; }

        public string Usuario_Creo { set; get; }

        public string Fecha_Creo { set; get; }

        public string Usuario_Modifico { set; get; }

        public string Fecha_Modifico { set; get; }
        public string Proceso { set; get; }


    }
}