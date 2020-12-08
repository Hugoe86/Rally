using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Lote_Serie_Negocio
    {
        public int Producto_ID { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string Lote_Serie { get; set; }
        public double Cantidad { get; set; }


    }
}