using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Facturacion
{
    public class Cls_Apl_Cat_Metodo_Pago_Negocio
    {
        public int Metodo_Pago_ID { set; get; }
        public string Nombre { set; get; }
        public string No_Cuenta_Pago { set; get; }
        public string Estatus { set; get; }
        public string Descripcion { set; get; }
    }
}