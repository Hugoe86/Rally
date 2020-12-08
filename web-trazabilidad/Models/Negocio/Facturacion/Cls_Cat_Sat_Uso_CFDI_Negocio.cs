using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Facturacion
{
    public class Cls_Cat_Sat_Uso_CFDI_Negocio
    {
        public int Uso_CFDI_ID { get; set; }
        public string Calve_Sat { get; set; }
        public string Descripcion { get; set; }
        public string Fisica { get; set; }
        public string Moral { get; set; }
        public string Fecha_Inicio_Vigencia { get; set; }
        public string Fecha_Fin_Vigencia { get; set; }
        public DateTime? Fecha_Inicio_Vigencia1 { get; set; }
        public DateTime? Fecha_Fin_Vigencia1 { get; set; }
    }
}