using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Tra_Cat_Dispositivos_Negocio
    {
        public int Dispositivo_ID { set; get; }
        public int Empresa_ID { set; get; }
        public int Estatus_ID { set; get; }
        public int Proceso_ID { set; get; }
        public int Subproceso_ID { set; get; }
        public string Numero_Serie { set; get; }
        public string Usuario_Creo { set; get; }
        public string Fecha_Creo { set; get; }
        public string Usuario_Modifico { set; get; }
        public string Fecha_Modifico { set; get; }

        public string Estatus { set; get; }
        public string Proceso { set; get; } 
        public string Subproceso { set; get; }




    }
}