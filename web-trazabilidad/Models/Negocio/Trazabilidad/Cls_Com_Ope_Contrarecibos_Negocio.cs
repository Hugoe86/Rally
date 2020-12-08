using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Com_Ope_Contrarecibos_Negocio
    {
        public int Empresa_ID { get; set; }
        public int? No_Contrarecibo { get; set; }
        public int No_Orden_Compra { get; set; }
        public int Proveedor_ID { get; set; }
        public int Estatus_ID { get; set; }
        public string Estatus_OC { get; set; }
        public string Estatus_CR { get; set; }
        public DateTime Fecha_Recepcion { get; set; }
        public DateTime Fecha_Pago { get; set; }
        public string Observaciones { get; set; }
        public string Usuario_Creo { get; set; }
        public DateTime Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public DateTime Fecha_Modifico { get; set; }
        public DateTime Fecha_Inicio { get; set; }
        public DateTime Fecha_Termino { get; set; }
        public int Sucursal_ID { get; set; }

        //Otros datos para la consulta
        public string Proveedor { get; set; }
        public string Estatus { get; set; }
        public string Fecha_Recepcion_Grid { get; set; }

        public string Empresa { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Fax { get; set; }

        public string Producto { get; set; }
        public string Folio_Factura { get; set; }
        public DateTime Fecha_Factura { get; set; }
        public string Empaque { get; set; }
        public string Cantidad_Recibida { get; set; }
        public string No_Lote_Serie { get; set; }
        public string Usuario_Recibio { get; set; }
        public string No_Contenedor { get; set; }


        //public DateTime Fecha_Inicio { get; set; }
        //public DateTime Fecha_Termino { get; set; }

        //Datos de las partidas a guardar
        public string Datos_Detalles { get; set; }
        public string Datos_Series_Lotes { get; set; }
        public decimal Cantidad_Faltante { get; set; }



    }
}