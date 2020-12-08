using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Ope_Ordenes_Cliente_Detalles_Negocio
    {
        public int No_Orden_Cliente_Deta { get; set; }
        public int No_Orden_Cliente { get; set; }
        public int Empresa_ID { get; set; }
        public string No_OC_Recibida { get; set; }
        public string Producto_ID { get; set; }
        public DateTime? Fecha_Inicio_Vig { get; set; }
        public DateTime? Fecha_Termina_Vig { get; set; }
        public decimal Precio_Unitario { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public string Observaciones { get; set; }
        public int Acumulado { get; set; }
        public string Usuario_Creo { get; set; }
        public DateTime Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public DateTime Fecha_Modifico { get; set; }
        public string Producto { get; set; }
        public int Cliente_ID { get; set; }
        public string Cliente { get; set; }
        public int Cantidad_Agregada { get; set; }
        public int Cantidad_Embarcada { get; set; }
        public int Cantidad_Pieza_Contenedor { get; set; }    
        public int Tipo_Impuesto_ID { get; set; }
        public int Tasa { get; set; }
        public int Cant_Ord { get; set; }
    }
}