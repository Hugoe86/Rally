using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Trazabilidad
{
    public class Cls_Embarques_Productos_Negocio
    {
        public Nullable<int> No_Orden_Produccion { get; set; }
        public int Producto_ID { get; set; }
        public Nullable<double> Existencia { get; set; }
        public Nullable<int> Cantidad_Solicitada { get; set; }
        public Nullable<double> Restante { get; set; }
        public Nullable<int> Almacen { get; set; }
        public Nullable<int> Ubicacion_ID { get; set; }
        public string Fecha_Ingreso { get; set; }
        public string Nombre { get; set; }
    }
}