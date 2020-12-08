using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Com_Ope_Transacciones_Negocio
    {
        public int Empresa_ID { get; set; }
        public int Transaccion_ID { get; set; }
        public int Tipo_Transaccion_ID { get; set; }
        public string Tipo_Transaccion { get; set; }
        public int Transacion_ID_Origen { get; set; }
        public int No_Inventario { get; set; }
        public int Producto_ID { get; set; }
        public string Producto { get; set; }
        public int Estatus_ID { get; set; }
        public string Estatus { get; set; }
        public int Ubicacion_ID { get; set; }
        public int UbicacionDestino { get; set; }
        public string Ubicacion { get; set; }
        public int Empaque_ID { get; set; }
        public string Empaque { get; set; }
        public int Causa_Scrap_ID { get; set; }
        public string Causa { get; set; }
        public double Cantidad { get; set; }
        public int Cantidad_Faltante { get; set; }
        public DateTime Fecha_Transaccion { get; set; }
        public DateTime Fecha_Creo { get; set; }
        public DateTime Fecha_Modifico { get; set; }
        public string Usuario_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public string Observaciones { get; set; }
        public string No_Lote_Serie { get; set; }
        public string No_Contenedor { get; set; }
        public double Cantidad_Contenedor { get; set; }
        public string No_Seriales { set; get; }
        public int Almacen { set; get; }
        public int Almacen_Destino { get; set; }
        public decimal Cant_Trans { get; set; }
        public string Tipo_Stock { get; set; }
        public DateTime Fecha_Caducidad { get; set; }
        public string P_Almacen { get; set; }
        public decimal Apartar { get; set; }
        public decimal? Cant_Apartada { get; set; }
        public decimal? Apartado_Total { get; set; }
        public int No_Proyecto { get; set; }
        public int No_Apartado { get; set; }
        public string Motivo { get; set; }
    }
}