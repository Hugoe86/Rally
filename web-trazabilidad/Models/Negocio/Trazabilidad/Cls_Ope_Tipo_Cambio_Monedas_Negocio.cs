using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Trazabilidad
{
    public class Cls_Ope_Tipo_Cambio_Monedas_Negocio
    {
    
        public int No_Tipo_Cambio { get;set;}
        public int Empresa_ID { get;set;}
        public int Sucursal_ID { get; set; }
        public int Moneda_ID { get; set; }
        public int Moneda_A_ID { get; set; }
        public decimal Cambio { get; set; }
        public DateTime Fecha { get; set; }
        public string Estatus { get; set; }
        public string Usuario_Creo { get; set; }
        public DateTime Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public DateTime Fecha_Modifico { get; set; }
        public string Fecha_Busqueda { get; set; }
        public string Datos_Detalles { get; set; }
        //Para consulta select 
        public string Moneda { get; set; }
        public string MonedaConvertir { get; set; }
    }
}