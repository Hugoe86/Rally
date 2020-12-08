using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Rpt_Kardex_Inventario_Negocio
    {
        //Datos para el inventario
        public int Producto_ID { get; set; }
        public int No_Inventario { get; set; }


        //Datos para el historial del inventario
        public int Transaccion_ID { get; set; }
        public string Tipo { get; set; }
        public string Fecha { get; set; }
        public double Cantidad_Entrada { get; set; }
        public double Cantidad_Salida { get; set; }
        public string Ubicacion { get; set; }
        public string Observaciones { get; set; }
    }
}