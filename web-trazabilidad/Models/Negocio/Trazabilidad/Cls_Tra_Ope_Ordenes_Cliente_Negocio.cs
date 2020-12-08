using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Tra_Ope_Ordenes_Cliente_Negocio
    {
        public int No_Orden_Cliente { get; set; }
        public int Empresa_ID { get; set; }
        public int Cliente_ID { get; set; }
        public string Estatus { get; set; }
        public string Comentarios { get; set; }
        public string Usuario_Creo { get; set; }
        public DateTime Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public DateTime? Fecha_Modifico { get; set; }
        public string Cliente { get; set; }
        public string Clave { get; set; }
        public string Orden_Detalles { get; set; }
    }
}