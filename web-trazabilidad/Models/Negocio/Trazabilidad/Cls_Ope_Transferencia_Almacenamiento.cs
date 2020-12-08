using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Ope_Transferencia_Almacenamiento
    {
        public int No_Inventario { get; set; }
        public int No_Orden_Produccion { get; set; }
        public int Ubicacion_Ct_Id { get; set; }
        public int Ubicacion_Id { get; set; }
        public int Almacen_ID { get; set; }
        public int Producto_ID { get; set; }
        public string Producto { get; set; }
        public double Cantidad { get; set; }
        public string Codigo { get; set; }
        public string Tipo_Producto { get; set; }
        public string No_Contenedor { get; set; }
        public DateTime Fecha_Ingreso { get; set; }
        public string Estatus { get; set; }
        public string Ubicacion { get; set; }
        public decimal Cant_Producir { get;set; }
        public decimal Cant_Producida { get; set; }
    }
}