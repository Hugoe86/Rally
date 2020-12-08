using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Com_Ope_Contrarecibo_Detalles_Grid_Negocio
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
        public string No_Contenedor { get; set; }
        public string No_Lote_Serie { get; set; }
        public DateTime? Fecha_Caducidad { get; set; }
        public string Usuario_Creo { get; set; }
        public DateTime Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public DateTime Fecha_Modifico { get; set; }
        public int Tipo_Impuesto_ID { get; set; }
        public string Impuesto { get; set; }
        public int UM_De_Compra { get; set; }
        public int UM_A_Almacenamiento { get; set; }
        public decimal Conversion { get; set; }
        public int Tasa { get; set; }

        //Propiedades que tiene los detalles de la orden de compra
        public int No_Orden_Compra { get; set; }
        public int No_Orden_Compra_Detalle { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad_Ordenada { get; set; }
        public decimal Costo { get; set; }
        public string Empaque { get; set; }
        public string Empaque_Almacenamiento { get; set; }
        public int Registro_ID { get; set; }
        public double Cantidad_A_Recibir { get; set; }
        public int ControlStock_ID { get; set; }
        public string ControlStock { get; set; }
        public string Tiene_Fecha_Expiracion { get; set; }
        public int Dias_Expiracion { get; set; }
        public decimal Cant_Rec { get; set; }
    }
}