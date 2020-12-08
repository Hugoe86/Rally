using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Ope_Embarques_Detalles_Negocio
    {
        public int No_Embarque_Deta { get; set; }
        public int No_Embarque { get; set; }
        public int Producto_ID { get; set; }
        public string No_Contenedor { get; set; }
        public int? No_Piezas_Contenedor { get; set; }
        public decimal Precio_Unitario { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public string Usuario_Creo { get; set; }
        public DateTime Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public DateTime Fecha_Modifico { get; set; }
        public string Producto { get; set; }
        public int Tipo_Impuesto_ID { get; set; }
        public string Estatus { get; set; }
        public string Motivo_Cancelacion { get; set; }
        public DateTime Fecha_Cancelacion { get; set; }
        public int Usuario_Cancelo_ID { get; set; }
        public int No_Inventario { get; set; }
    }
}