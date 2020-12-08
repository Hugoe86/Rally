using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Trazabilidad
{
    public class Cls_Tra_Ope_Explosion_Combos_Productos_Detalles_Negocio
    {
        public int No_Exp_Cmb_Prod_Deta { get; set; }
        public int No_Explosion_Combo_Deta { get; set; }
        public int Producto_ID { get; set; }
        public Nullable<decimal> Cantidad { get; set; }
        public Nullable<decimal> Cantidad_Receta { get; set; }
        public Nullable<decimal> Costo_Adicional { get; set; }
        public int No_Explosion_Combo { get; set; }

        public string Nombre { get; set; }

        public Nullable<decimal> Porcentaje_Rendimiento { get; set; }

        public bool Checkbox { get; set; }
    }
}