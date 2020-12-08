using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Trazabilidad
{
    public class Cls_Ope_Embarques_Productos_Negocio
    {
        public int No_Inventario { get; set; }
        public int Empaque_ID { get; set; }
        public int Producto_ID { get; set; }
        public double Existencia { get; set; }
        public string No_Contrarecibo { get; set; }
        public string No_Lote_Serie { get; set; }
        public Nullable<int> Causa_Scrap_ID { get; set; }
        public string No_Contenedor { get; set; }
    }
}