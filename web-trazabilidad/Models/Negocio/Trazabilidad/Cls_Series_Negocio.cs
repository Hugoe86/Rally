using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Series_Negocio
    {
        public string No_Serie { get; set; }
        public int No_Inventario { get; set; }

        public double Existencias { get; set; }
    }
}