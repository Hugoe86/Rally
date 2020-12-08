using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Tra_Ope_Explosion_Materiales_Negocio
    {
        public int No_Explosion_Material { get; set; }
        public int Empresa_ID { get; set; }
        public int Producto_ID { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Usuario_Creo { set; get; }
        public string Fecha_Creo { set; get; }
        public string Usuario_Modifico { set; get; }
        public string Fecha_Modifico { set; get; }
        public int? Ubicacion_ID { get; set; }
        public int? Producto_ID_Contenedor { get; set; }
        public int? No_Piezas_Contenedor { get; set; }
        public int? Ubicacion_ID_CT { get; set; }
        public int? Almacen { get; set; }
        public string Almacen_Nombre { get; set; }
        public string Ubicacion { get; set; }
        public int? Alm_Pro_Ubicacion_ID { get; set; }
        public string Centro_Produccion { get; set; }
        public string Almacen_Produccion { get; set; }

    }
}