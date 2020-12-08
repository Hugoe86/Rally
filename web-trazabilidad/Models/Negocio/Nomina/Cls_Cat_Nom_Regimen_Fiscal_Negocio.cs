using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Nomina
{
    public class Cls_Cat_Nom_Regimen_Fiscal_Negocio
    {
        public int Regimen_Fiscal_ID { get; set; }
        public int Empresa_ID { get; set; }
        public int Sucursal_ID { get; set; }
        public string Clave_SAT { get; set; }
        public string Descripcion { get; set; }
        public string Estatus { get; set; }
        public string Comentarios { get; set; }
        public string Usuario_Creo { get; set; }
        public DateTime? Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public DateTime? Fecha_Modifico { get; set; }
        public string Fisica { get; set; }
        public string Moral { get; set; }
        public string Fecha_Inicio_Vigencia { get; set; }
        public string Fecha_Fin_Vigencia { get; set; }
        public DateTime? Fecha_Fin_Vigencia1 { get; set; }
        public DateTime? Fecha_Inicio_Vigencia1 { get; set; }
    }
}