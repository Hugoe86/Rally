using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Orden_Produccion_Report
    {
        public int No_Orden_Produccion { get; set; }
        public decimal Cantidad { get; set; }
        public string Estatus { get; set; }
        public DateTime Due_Date { get; set; }
        public string Ubicacion { get; set; }
        public string Descripcion { get; set; }
        public string Nota { get; set; }
        public string Prioridad { get; set; }
        public string Empresa { get; set; }
        public string Producto { get; set; }
        public string Codigo { get; set; }
        public string Empaque { get; set; }
        public byte[] Imagen { get; set; }
    }
}