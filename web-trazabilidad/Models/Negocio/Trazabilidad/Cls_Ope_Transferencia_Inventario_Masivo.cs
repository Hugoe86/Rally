using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Trazabilidad
{
    public class Cls_Ope_Transferencia_Inventario_Masivo
    {
        public int Producto_ID { get; set; }
        public int Almacen_ID { get; set; }
        public int Ubicacion_ID { get; set; }
        public int Almacen_Destino_ID { get; set; } 
        public int Ubicacion_Destino_ID { get; set; }
        public string Producto { get; set; }
        public string Almacen { get; set; }
        public string Ubicacion { get; set; }
        public double Cantidad { get; set; }
        public int No_Inventario { get; set; }
        public DateTime Fecha_Ingreso { get; set; }
        public string tbl_prod { get; set; }
        public string Estatus { get; set; }
        //public int Rol_Calidad { get; set; }
    }
}