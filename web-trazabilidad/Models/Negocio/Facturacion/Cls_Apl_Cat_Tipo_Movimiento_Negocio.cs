using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Facturacion
{
    public class Cls_Apl_Cat_Tipo_Movimiento_Negocio
    {
        public int Tipo_Movimiento_ID { set; get; }
        public string Nombre { set; get; }
        public string Tipo { set; get; }
        public string Estatus { set; get; }
        public string Descripcion { set; get; }
    }
}