using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Trazabilidad
{
    public class Cls_Caracteristicas_Productos_Negocio
    {
        public int No_Caracteristica { get; set; }
        public int Empresa_ID { get; set; }
        public int Sucursal_ID { get; set; }
        public int No_Explosion_Material { get; set; }
        public string Caracteristica { get; set; }
    }
}