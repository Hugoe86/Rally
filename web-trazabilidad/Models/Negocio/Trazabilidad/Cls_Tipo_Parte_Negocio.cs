using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Tipo_Parte_Negocio
    {
        public int Codigo_Producto_ID { get; set; }
        public int Producto_ID { get; set; }
        public string Producto { get; set; }
        public string Codigo { get; set; }
        public string Tipo { get; set; }
        public string Usuario_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public DateTime Fecha_Creo { get; set; }
        public DateTime Fecha_Modifico { get; set; }
    }
}