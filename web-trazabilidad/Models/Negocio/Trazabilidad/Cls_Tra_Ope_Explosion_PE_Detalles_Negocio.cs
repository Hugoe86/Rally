using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Trazabilidad
{
    public class Cls_Tra_Ope_Explosion_PE_Detalles_Negocio
    {
        public int No_Explosion_Material_PE_Deta { get; set; }
        public int No_Explosion_Material { get; set; }
        public int Producto_ID { get; set; }
        public string Estatus { get; set; }
        public DateTime Fecha_Inicio_Vigencia { get; set; }
        public DateTime Fecha_Fin_Vigencia { get; set; }
        public string Producto { get; set; }
        public string Tipo_Producto { get; set; }
        public string Unidad { get; set; }
        public decimal Cantidad_Receta { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Porc_Rendimiento { get; set; }
        public decimal? Costo_Extra { get; set; }
    }
}