using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Orden_Compra_Report
    {
        public int No_Orden_Compra { get; set; }
        public DateTime Fecha_Orden { get; set; }
        public string Justificacion { get; set; }
        public string Impuesto { get; set; }
        public string Empresa { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Producto { get; set; }
        public string Codigo { get; set; }
        public string Proveedor { get; set; }
        public string Empaque { get; set; }
        public string Estatus { get; set; }
        public string Telefono { get; set; }
        public decimal Costo { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Tasa { get; set; }
        public byte[] Imagen { get; set; }
     

    }
}