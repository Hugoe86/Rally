using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Informacion_Inventario_Negocio
    {
        public string No_Inventario { get; set; }
        public string No_Parte { get; set; }
        public string Descripcion { get; set; }
        public string Fecha { get; set; }
        public string Serial { get; set; }
        public string Categoria { get; set; }
        public string Color { get; set; }
        public string No_Parte_Inj { get; set; }
        public string No_Parte_Paint { get; set; }
        public string No_Parte_Cliente { get; set; }
        public string Prefijo_ID { get; set; }
        public string NP_Pintura { get; set; }
        public string Maquina_Inj { get; set; }
        public string NP_Injeccion { get; set; }
        public string Fecha_Hora_Inj { get; set; }
        public string Operador_Inj { get; set; }
        public string Lote_Inj { get; set; }
        public string Tipo { get; set; }
        public string No_Inventario_Pintura { get; set; }
        public string No_Inventario_Injeccion { get; set; }
        public string NP_Inj_Completa { get; set; }
        public string NP_Paint_Completa { get; set; }
    }
}