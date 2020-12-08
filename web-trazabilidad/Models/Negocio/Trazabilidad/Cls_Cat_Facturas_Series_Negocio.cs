using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Trazabilidad
{
    public class Cls_Cat_Facturas_Series_Negocio
    {
        public int Serie_ID { get; set; }
        public int Empresa_ID { get; set; }
        public int Sucursal_ID { get; set; }
        public string Serie { get; set; }
        public string Descripcion { get; set; }
        public string Tipo_Serie { get; set; }
        public decimal Total_Series { get; set; }
        public decimal Timbre_Inicial { get; set; }
        public decimal Timbre_Final { get; set; }
        public string Estatus { get; set; }
        public string Tipo_Default { get; set; }
        public int Cancelaciones { get; set; }
        public string Usuario_Creo { get; set; }
        public DateTime Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public DateTime Fecha_Modifico { get; set; }
        public decimal Timbres_Usados { get; set; }
    }
}