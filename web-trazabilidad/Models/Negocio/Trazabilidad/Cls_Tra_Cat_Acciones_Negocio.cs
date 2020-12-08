using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Tra_Cat_Acciones_Negocio
    {
        public int Accion_ID { set; get; }

        public int Empresa_ID { set; get; }

        public int Estatus_ID { set; get; }

        public string Clave { set; get; }

        public string Nombre { set; get; }

        public string Observaciones { set; get; }

        public string Usuario_Creo { set; get; }

        public string Fecha_Creo { set; get; }

        public string Usuario_Modifico { set; get; }

        public string Fecha_Modifico { set; get; }

    }
}