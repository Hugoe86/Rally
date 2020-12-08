using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Trazabilidad
{
    public class Cls_Ope_Pos_Paquetes_Negocio
    {
        public int No_Paquete { get; set; }
        public int Empresa_ID { get; set; }
        public int Sucursal_ID { get; set; }
        public int Producto_ID { get; set; }
        public int? Almacen_Produccion_ID { get; set; }
        public int? Ubicacion_Produccion_ID { get; set; }
        public string Usuario_Creo { get; set; }
        public DateTime? Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public DateTime? Fecha_Modifico { get; set; }
    }
}