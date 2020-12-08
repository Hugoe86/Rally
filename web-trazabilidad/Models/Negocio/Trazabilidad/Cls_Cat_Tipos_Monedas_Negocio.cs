using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Trazabilidad
{
    public class Cls_Cat_Tipos_Monedas_Negocio
    {
        public int Moneda_ID { get; set; }

        public int Empresa_ID { get; set; }
        public int Sucursal_ID { get; set; }
        public string Nombre { get; set; }
        public string Nombre_Corto { get; set; }
        public string Clave_SAT { get; set; }
        public string Simbolo { get; set; }
        public bool Moneda_Default { get; set; } 
        public string Descripcion { get; set; }
        public string Usuario_Creo { get; set; }
        public DateTime Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public DateTime Fecha_Modifico { get; set; }
        public decimal Cambio { get; set; }
    }
}