using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Apl_Cat_Metodo_Pago_Negocio
    {
        public int Metodo_Pago_ID { get; set; }
        public string Nombre { get; set; }
        public string No_Cuenta_Pago { get; set; }
        public string Estatus { get; set; }
        public string Descripcion { get; set; }
        public string Usuario_Creo { get; set; }
        public string Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public string Fecha_Modifico { get; set; }
        public string Clave_SAT { get; set; }
        public string Fecha_Inicio_Vigencia { get; set; }
        public string Fecha_Fin_Vigencia { get; set; }
        public DateTime? Fecha_Inicio_Vigencia1 { get; set; }
        public DateTime? Fecha_Fin_Vigencia1 { get; set; }
        //variable auxiliar
        public decimal Monto { get; set; }
    }
}