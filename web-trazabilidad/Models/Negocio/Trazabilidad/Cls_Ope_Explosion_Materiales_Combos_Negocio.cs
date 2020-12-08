using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Trazabilidad
{
    public class Cls_Ope_Explosion_Materiales_Combos_Negocio
    {
        public int No_Explosion_Material { get; set; }
        public int Empresa_ID { get; set; }
        public int Sucursal_ID { get; set; }
        public int Producto_ID { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int Almacen { get; set; }
        public int Ubicacion_ID { get; set; }
        public int Producto_ID_Contenedor { get; set; }
        public int No_Piezas_Contenedor { get; set; }
        public int Alm_Pro_Ubicacion_ID { get; set; }
        public int Ubicacion_ID_CT { get; set; }
        public string Usuario_Creo { get; set; }
        public DateTime Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public DateTime Fecha_Modifico { get; set; }
    }
}