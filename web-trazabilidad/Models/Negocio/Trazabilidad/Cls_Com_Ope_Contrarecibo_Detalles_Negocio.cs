using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Com_Ope_Contrarecibo_Detalles_Negocio
    {
        //Propiedades para el detalle del contrarecibo
        public int No_Contrarecibo_Detalle { get; set; }
        public int Empresa_ID { get; set; }
        public int No_Contrarecibo { get; set; }
        public string Folio_Factura { get; set; }
        public DateTime Fecha_Factura { get; set; }
        public int Linea { get; set; }
        public int Producto_ID { get; set; }
        public decimal Cantidad_Recibida { get; set; }
        public decimal Costo_Unitario { get; set; }
        public decimal Importe { get; set; }
        public int Empaque_ID { get; set; }
        public string Usuario_Recibio { get; set; }
        public DateTime Fecha_Recepcion { get; set; }
        public decimal Total_Impuestos { get; set; }
        public decimal Total { get; set; }
        public string Observaciones_Linea { get; set; }
        public DateTime? Fecha_Caducidad { get; set; }
        public string Usuario_Creo { get; set; }
        public DateTime Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public DateTime Fecha_Modifico { get; set; }
        public string No_Lote_Serie { get; set; }
        public string No_Contenedor { get; set; }
        public int Tipo_Impuesto_ID { get; set; }
        public decimal Impuesto { get; set; }
        public int UM_De { get; set; }
        public int UM_A { get; set; }
        public double Conversion { get; set; }
        public string datos { get; set; }
        public int Registro_ID { get; set; }
        public decimal? Peso { get; set; }
        public decimal? Longitud { get; set; }
        public decimal? Ancho { get; set; }
        public string Descripcion { get; set; }
        public int No_Orden_Compra_Detalle { get; set; }
        public int No_Orden_Compra { get; set; }
        public string Fecha { get; set; }
        public string Producto { get; set; }
        public decimal Cantidad_Ordenada { get; set; }
    }
}