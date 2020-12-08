using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Transaccion_Producto_Almacen_Ubicacion_Inventario_Negocio
    {
        public int Empresa_ID { get; set; }
        public int Sucursal_ID { get; set; }
        public int Almacen_ID { get; set; }
        public int No_Inventario { get; set; }
        public int Producto_ID { get; set; }
        public int Tipo_Transaccion_ID { get; set; }
        public string TipoTransaccion { get; set; }
        public DateTime FechaTransaccion { get; set; }
        public int Ubicacion_ID { get; set; }
        public string Ubicacion { get; set; }
        public int Cantidad { get; set; }
        public string FechaTransaccionStr
        {
            get
            {
                return this.FechaTransaccion.ToString("dd/MM/yyyy HH:mm");
            }
        }
    }
}