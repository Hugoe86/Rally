using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Ope_Consulta_Informacion_Inventario_Negocio
    {
        public string No_Parte { get; set; }
        public string No_Parte_Cliente { get; set; }
        public int Empresa_ID { get; set; }
        public string Tipo { get; set; }
        public string Fecha_Inicio { get; set; }
        public string Fecha_Termino { get; set; }
        public string Operador { get; set; }
        public string Maquina { get; set; }
        public string Nombre_Parte { get; set; }

        public string Serie { get; set; }
        public string Lote { get; set; }
    }
}