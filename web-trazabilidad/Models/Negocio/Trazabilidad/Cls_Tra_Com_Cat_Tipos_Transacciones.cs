using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Tra_Com_Cat_Tipos_Transacciones
    {
        public int Tipo_Transaccion_ID { set; get; }
        public int Estatus_ID { get; set; }
        public string Nombre { get; set; }
        public string Comentarios { get; set; }
        public string Estatus { get; set; }
        public string Usuario_Creo { set; get; }
        public string Fecha_Creo { set; get; }
        public string Usuario_Modifico { set; get; }
        public string Fecha_Modifico { set; get; }
        public int? Cuenta_Contable_ID { set; get; }
        public string Cuenta_Contable_Nombre { set; get; }
    }
}