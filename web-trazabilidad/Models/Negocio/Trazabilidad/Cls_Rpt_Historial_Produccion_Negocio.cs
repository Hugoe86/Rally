using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Rpt_Historial_Produccion_Negocio
    {
        public int Producto_ID { get; set; }
        public int Ubicacion_ID { get; set; }
        public string Usuario { get; set; }
        public string Fecha_Inicio { get; set; }
        public string Fecha_Termino { get; set; }

    }
}