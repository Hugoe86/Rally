using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Tra_Cat_Turnos_Negocio
    {
        public int Empresa_ID { set; get; }
        public int Turno_ID { get; set; }
        public int Estatus_ID { get; set; }
        public string Nombre { get; set; }
        public string Observaciones { get; set; }
        public string Usuario_Creo { set; get; }
        public string Fecha_Creo { set; get; }
        public string Usuario_Modifico { set; get; }
        public string Fecha_Modifico { set; get; }
        public string Hora_Entrada { set; get; }
        public string Hora_Salida { set; get; }
    }
}