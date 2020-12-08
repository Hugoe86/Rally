using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Trazabilidad
{
    public class Cls_Ope_Promociones
    {
        public int No_Promocion { get; set; }
        public int Empresa_ID { get; set; }
        public int Sucursal_ID { get; set; }
        public int Producto_ID { get; set; }
        public DateTime Fecha_Inicio_Promocion { get; set; }
        public DateTime Fecha_Fin_Promocion { get; set; }
        public int? Porcentaje_Descuento { get; set; }
        public double? Monto_Descuento { get; set; }
        public string Usuario_Creo { get; set; }
        public DateTime Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public DateTime Fecha_Modifico { get; set; }
    }
}