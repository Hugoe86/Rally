using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Facturacion
{
    public class Cls_Cat_Sat_Productos_Negocio
    {
        public int Producto_ID_Sat { get; set; }
        public string Clave_Sat { get; set; }
        public string Descripcion_Sat {get;set;}
        public string Fecha_Inicio_Vigencia { get; set; }
        public string Fecha_Fin_Vigencia { get; set; }
        public DateTime? Fecha_Inicio_Vigencia1 { get; set; }
        public DateTime? Fecha_Fin_Vigencia1 { get; set; }
        public string Incluir_Iva_Traslado { get; set; }
        public string Incluir_Ieps_Traslado { get; set; }
        public string Complemento_Debe_Incluir { get; set; }
    }
}