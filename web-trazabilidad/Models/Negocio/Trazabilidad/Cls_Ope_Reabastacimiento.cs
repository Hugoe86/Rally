using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Ope_Reabastacimiento
    {
        public int No_Solicitud { get; set; }
        public int Sol_Almacen_ID { get; set; }
        public int Rea_Almacen_ID { get; set; }
        public int Rea_Ubicacion_ID { get; set; }
        public string Estatus { get; set; }
        public DateTime  Fecha_Solicitud { get; set; }
        public DateTime Fecha_Surtido { get; set; }
        public int No_Sol_Ubi_Deta { get; set; }
        public int Sol_Ubicacion_ID { get; set; }
        public int Producto_ID { get; set; }
        public int Empaque_ID { get; set; }
        public decimal Cantidad { get; set; }
        public int Stock_Maximo { get; set; }
        public int Stock_Minimo { get; set; }
        public int Punto_Reorden { get; set; }
        public string Sol_Almacen { get; set; }
        public string Sol_Ubicacion { get; set; }
        public decimal Existencia { get; set; }
        public decimal? Cant { get; set; }
        public int Almacen_ID { get; set; }
        public int Ubicacion_ID { get; set; }
    }

}