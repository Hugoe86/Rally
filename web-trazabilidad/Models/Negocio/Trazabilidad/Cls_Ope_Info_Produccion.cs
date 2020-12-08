using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Ope_Info_Produccion
    {
        public int Empresa_ID { get; set; }
        public int Sucursal_ID { get; set; }
        public int No_Info_Produccion { get; set; }
        public int No_Orden_Produccion { get; set; }
        public int No_Inventario_Consumido { get; set; }
        public int? No_Inventario_Producido { get; set; }
        public int Producto_ID { get; set; }
        public int? Proveedor_ID { get; set; }
        public double Cantidad_BOM { get; set; }
        public double Cantidad_Consumio_Inventario { get; set; }
        public string Tipo_Stock { get; set; }
        public string No_Lote_Serie { get; set; }
        public string Usuario_Creo { get; set; }
        public DateTime Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public DateTime? Fecha_Modifico { get; set; }

        //otros datos
        public string Codigo_Producto { get; set; }
        public string Fecha_Inicio { get; set; }
        public string Fecha_Termino{ get; set; }
        public string No_Contenedor{ get; set; }
        public int Ubicacion_ID { get; set; }
        public int Almacen_ID { get; set; }
    }
}