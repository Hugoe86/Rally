using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Tra_Ope_Explocion_Materiales_Detalles_Negocio
    {
        public int No_Explosion_Material_Detalle { get; set; }
        public int No_Explosion_Material { get; set; }
        public int Empresa_ID { get; set; }
        public int Producto_lista_ID { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Cantidad_Receta { get; set; }
        public int Producto_ID { get; set; }
        public string Tipo { get; set; }
        public decimal Costo_Extra { get; set; }
        public bool Mostrar_En_Venta { get; set; }

    }
}