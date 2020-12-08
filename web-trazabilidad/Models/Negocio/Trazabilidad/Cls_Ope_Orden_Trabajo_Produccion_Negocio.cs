using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Ope_Orden_Trabajo_Produccion_Negocio
    {
        public int No_Orden_Produccion { get; set; }
        public string Prioridad { get; set; }
        public string Estatus { get; set; }
        public string Nota { get; set; }
        public string Descripcion { get; set; }
        public string Producto { get; set; }
        public int Producto_ID { get; set; }
        public string Empaque { get; set; }
        public int Empaque_ID { get; set; }
        public string Ubicacion { get; set; }
        public string Ubicacion_ID_CT { get; set; }
        public int Ubicacion_ID { get; set; }
        public string Codigo { get; set; }
        public decimal Cantidad_Producir { get; set; }
        public Nullable<decimal> Cantidad_Producida { get; set; }
        public DateTime Due_Date { get; set; }
        public string No_Contenedor { get; set; }
        public string No_Serie { get; set; }
        public int Contenedor_ID { get; set; }
        public int Capacidad_Contenedor { get; set; }
        public int Cantidad_Actual { get; set; }
        public string Fecha_Envio_Produccion { get; set; }
        public string Usuario_Modifico { get; set; }
    }
}