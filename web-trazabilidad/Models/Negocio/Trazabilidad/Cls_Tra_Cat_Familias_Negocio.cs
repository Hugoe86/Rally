using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Trazabilidad
{
    public class Cls_Tra_Cat_Familias_Negocio
    {
        public int Familia_ID { get; set; }
        public int Empresa_ID { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Estatus { get; set; }
        public string Usuario_Creo { get; set; }
        public System.DateTime Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public Nullable<System.DateTime> Fecha_Modifico { get; set; }
    }
}