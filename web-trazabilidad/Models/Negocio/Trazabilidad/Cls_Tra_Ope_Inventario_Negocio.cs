using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Tra_Ope_Inventario_Negocio
    {
        public int Empresa_ID { get; set; }
        public int No_Inventario { get; set; }
        public int Producto_ID { get; set; }
        public string Producto { get; set; }
        public double Existencias { get; set; }
        public double Cantidad { get; set; }
        public int Empaque_ID { get; set; }
        public string Empaque { get; set; }
        public string No_Lote_Serie { get; set; }
        public string Usuario_Modifico { get; set; }
        public string Fecha_Modifico { get; set; }
        public DateTime Fecha_Ingreso { get; set; }
        public int Transaccion_ID { get; set; }
        public int Ubicacion_ID { get; set; }
        public int Tipo_Transaccion_ID { get; set; }
        public int Estatus_ID { get; set; }
        public string Estatus { get; set; }
        public string Ubicacion { get; set; }
        public string No_Contenedor { get; set; }
        public string Tipo_Producto_Stock { set; get; }
        public int No_Orden_Produccion { set; get; }
        public string Fecha_Inicio { get; set; }
        public string Fecha_Termino { get; set; }
        public int Tipo_Producto_ID { get; set; }
        public string Tipo_Producto { get; set; }
        public string No_Parte_Cliente { get; set; }
        public string No_Serie { get; set; }
        public string Info_No_Inventario { get; set; }
        public string Tiene_Fec_Cad { get; set; }
        public string Fecha_Caducidad { get; set; }

    }
}