using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Ope_Detalles_Solicitud
    {
        public int No_Solicitud { get; set; }
        public int No_Solicitud_Deta { get; set; }
        public string Producto { get; set; }
        public string Almacen { get; set; }
        public string Ubicacion { get; set; }
        public int Sol_Almacen_ID { get; set; }
        public int Sol_Ubicacion_ID { get; set; }
        public int Rea_Almacen_ID { get; set; }
        public int Rea_Ubicacion_ID { get; set; }
        public int No_Sol_Ubi_Deta { get; set; }
        public int Producto_ID { get; set; }
        public decimal Stock_Maximo { get; set; }
        public decimal Stock_Minimo { get; set; }
        public decimal Punto_Reorden { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Cant_Existe { get; set; }
        public decimal Cant_Existe_Ubi { get; set; }
        public decimal Cant { get; set; }
        public decimal Cantidad_Surtida { get; set; }
        public string Codigo { get; set; }
        public string Almacen_Reabastece { get; set; }
        public string Ubicacion_Reabastece { get; set; }

    }
}