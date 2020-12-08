using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Trazabilidad
{
    public class Cls_Ope_Explosion_Material_Combos_Detalles_Negocio
    {
        public int No_Explosion_Combo_Detalle { get; set; }
        public int No_Explosion_Combo { get; set; }
        public int Categoria_ID { get; set; }
        public int Cantidad { get; set; }
        public string Estatus { get; set; }
        public DateTime Fecha_Inicio_Vigencia { get; set; }
        public DateTime Fecha_Fin_Vigencia { get; set; }
        public string Tipo { get; set; }
        public bool Obtener_Precio_Producto { get; set; }
        public string Lista_Detalles { get; set; }

}
}