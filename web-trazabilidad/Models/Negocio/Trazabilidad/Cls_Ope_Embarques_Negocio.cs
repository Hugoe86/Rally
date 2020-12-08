using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Ope_Embarques_Negocio
    {
        public int No_Embarque { get; set; }
        public int Empresa_ID { get; set; }
        public int No_Orden_Cliente { get; set; }
        public int Usuario_ID { get; set; }
        public DateTime Fecha_Embarque { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public string No_Placas { get; set; }
        public string Nombre_Operador { get; set; }
        public string Sello { get; set; }
        public string Nombre_Transporte { get; set; }
        public string Numero_Caja { get; set; }
        public string Estatus { get; set; }
        public string Usuario_Creo { get; set; }
        public DateTime Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public DateTime Fecha_Modifico { get; set; }
        public string Datos_Embarque_Detalle { get; set; }
        public string Fecha_Inicio { get; set; }
        public string Fecha_Termino { get; set; }
        public int Cliente_ID { get; set; }
        public string Clientes { get; set; }
        public string Motivo_Cancelacion { get; set; }
    }
}