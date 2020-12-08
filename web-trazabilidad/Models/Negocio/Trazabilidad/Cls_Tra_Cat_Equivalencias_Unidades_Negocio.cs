using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Tra_Cat_Equivalencias_Unidades_Negocio
    {
        public int Empresa_ID { get; set; }
        public int Eq_Unidades_ID { get; set; }
        public int UM_De { get; set; }
        public int UM_A { get; set; }
        public decimal Equivalencia { get; set; }
        public string Usuario_Creo { get; set; }
        public System.DateTime Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public DateTime? Fecha_Modifico { get; set; }

        public string UM_De_Nombre { get; set; }
        public string UM_A_Nombre { get; set; }
        public decimal Conversion { get; set; }
    }
}