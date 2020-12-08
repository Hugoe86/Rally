using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Ope_Punto_Reorden_Negocio
    {
        public int No_Punto_Reorden { get; set; }
        public int Producto_ID { get; set; }
        public int Almacen_ID { get; set; }
        public int? Ubicacion_ID { get; set; }
        public int Empresa_ID { get; set; }
        public int? Sucursal_ID { get; set; }
        public decimal Stock_Maximo { get; set; }
        public decimal Stock_Maximo_Almacen { get; set; }
        public decimal Stock_Maximo_Ubicaciones { get; set; }
        public decimal Stock_Maximo_Disponible { get; set; }
        public decimal Stock_Minimo { get; set; }
        public decimal Stock_Minimo_Almacen { get; set; }
        public decimal Stock_Minimo_Ubicaciones { get; set; }
        public decimal Stock_Minimo_Disponible { get; set; }
        public decimal Punto_Reorden { get; set; }
        public decimal Punto_Reorden_Almacen { get; set; }
        public decimal Punto_Reorden_Ubicaciones { get; set; }
        public decimal Punto_Reorden_Disponible { get; set; }
        public string Usuario_Creo { get; set; }
        public DateTime Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public DateTime Fecha_Modifico { get; set; }
        public string ProductosJson { get; set; }
    }
}