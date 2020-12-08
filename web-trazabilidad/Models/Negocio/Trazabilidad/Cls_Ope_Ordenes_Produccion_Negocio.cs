using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Ope_Ordenes_Produccion_Negocio
    {
        public int No_Orden_Produccion { set; get; }
        public int Empresa_ID { set; get; }
        public int Producto_ID { set; get; }
        public int Empaque_ID { set; get; }
        public int Ubicacion_ID { set; get; }
        public int Unidad_ID { set; get; }
        public decimal Cantidad_Producir { set; get; }
        public decimal Cantidad_Producida { set; get; }
        public string Prioridad { get; set; }
        public DateTime Due_Date { set; get; }
        public string Descripcion { get; set; }
        public string Nota { get; set; }
        public string Motivo_Cancelacion { get; set; }
        public DateTime Fecha_Cancelacion { set; get; }
        public string Estatus { get; set; }
        public string Fecha_Inicio { get; set; }
        public string Fecha_Termino { get; set; }
        public string Producto { get; set; }
        public string Empaque { get; set; }
        public string ubicacion { get; set; }
        public DateTime Fecha_Creo { get; set; }
        public DateTime Fecha_Modifico { get; set; }
        public DateTime Fecha_Ingreso { get; set; }
        public string Usuario_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public double Existenias { get; set; }
        public double Cantidad { get; set; }
        public string Lote_Serie { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string No_Contenedor { get; set; }
        public string Motivo_Suspension { get; set; }
        public DateTime Fecha_Suspension { set; get; }
        public int? Alm_Pro_Ubicacion_ID { set; get; }
        public string Almacen_Produccion { get; set; }
        public string Generar_No_Lote { get; set; }
        public string No_Lote { get; set; }
        public string ControlStock { get; set; }
    }
}