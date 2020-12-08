using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Cat_Productos_Negocio
    {
        public string Producto_ID { set; get; }
        public string Linea_ID { set; get; }
        public string Laboratorio_ID { set; get; }
        public string Unidad_Medida_ID { set; get; }
        public string Clave { set; get; }
        public string Nombre { set; get; }
        public string Comentarios { set; get; }
        public string Estatus { set; get; }
        public decimal Precio { set; get; }
        public decimal Costo { set; get; }
        public string Codigo { set; get; }
        public decimal Precio_Con_Impuesto { set; get; }
        public decimal Tasa_Impuesto { set; get; }
        public decimal Impuesto2 { set; get; }
        public decimal IVA { set; get; }
        public string Linea { set; get; }
        public string Unidad { set; get; }
        public string Accion { set; get; }
        public decimal Cantidad { set; get; }
        public decimal Total_Cantidad_Productos { set; get; }
        public decimal Subtotal { set; get; }
        public decimal IEPS { set; get; }
        public string Unidad_Compra { get; set; }
        public int Unidad_Compra_ID { get; set; }
        public int Unidad_ID { get; set; }
        public string Ubicacion_ID_CT { get; set; }
        public string Ubicacion { get; set; }
        public int Empaque_ID { get; set; }
        public int Tipo_Impuesto_ID { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Cantidad_Ordenada { get; set; }
        public int? Alm_Pro_Ubicacion_ID { get; set; }
        public string Almacen_Ubicacion { get; set; }
        public string Generar_No_Lote { get; set; }
        public string ControlStock { get; set; }
        public bool Modificar_Imp_OC { get; set; }
        public string Tipo_Impuesto { get; set; }
    }
}